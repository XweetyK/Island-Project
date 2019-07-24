using UnityEngine;
using System.Collections;

public class PlayerMov : MonoBehaviour {
    [SerializeField] private Transform _cameraSystem;

	[SerializeField]private float _animationWaitTime;
	[SerializeField]private int _waterRateMin;
	[SerializeField]private int _waterRateMax;

	ParticleSystem _water;
	[SerializeField]ParticleSystem _splash;

    [SerializeField] float _movSpeed;
    [SerializeField] float _rotSpeed;

    [SerializeField] int _layerMask;
    Vector3 MovementDirection;
    Rigidbody _rb;
    float _angle;

    float _currentSpeed;

	bool _isRunning;
	bool _isWalking;
	bool _isSit;
	bool _isCrouch;
	bool _isJesus;

	float _timer = 0;
	bool _counting = false;

	Animator _animator;
    RaycastHit hit;

    void Start () {
		_isWalking = _isRunning = _isCrouch = _isSit = false;
		_animator = GetComponent<Animator> ();
		_water = GetComponent<ParticleSystem> ();
        _rb = gameObject.GetComponent<Rigidbody>();
        _isJesus = false;
	}

	void Update () {
		KeyInput ();
		Movement ();
		UpdateAnimator ();
		AnimTimer ();
		WaterAnim ();
	}

    void FixedUpdate() {
        Vector3 gravity = -9.8f * 2.0f * Vector3.up;
        _rb.AddForce(gravity, ForceMode.Acceleration);
    }

    void Movement(){
        _currentSpeed = _isRunning ? _movSpeed * 2 : _movSpeed;
		if (_counting==false || _isSit==true) {
            MovementDirection = (Input.GetAxis("Horizontal") * _cameraSystem.right + Input.GetAxis("Vertical") * _cameraSystem.forward) * _currentSpeed;
            _rb.velocity = new Vector3(MovementDirection.x, _rb.velocity.y, MovementDirection.z);

            if (_rb.velocity != Vector3.zero) {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(new Vector3(_rb.velocity.x, 0, _rb.velocity.z)), Time.deltaTime * _rotSpeed);
            }
        }
	}

	void KeyInput(){

		if (Input.GetButton ("Shift")) {
			_isRunning = true;
		} else {
			_isRunning = false;
		}

        if (Input.GetAxis ("Horizontal") != 0 || Input.GetAxis ("Vertical") != 0) {
			_isWalking = true;
			if (_isSit) {
				_isSit = false;
				_counting = true;
			}
		} else {
			_isWalking = false;
		}

        if (Input.GetButtonDown ("One")) {
			_isSit = true;
			_counting = true;
		}

        if (Input.GetButtonDown("Submit")) {
            if (Physics.Raycast(transform.position+ new Vector3(0.0f,2.0f,0.0f), transform.TransformDirection(Vector3.forward), out hit, 3.0f, _layerMask)) {
                hit.collider.gameObject.GetComponent<DialogTrigger>().Trigger();
            }
        }
		//if (Input.GetButton ("Shift")) {
		//	_isCrouch = true;
		//} else {
		//	_isCrouch = false;
		//}

	}
    void OnDrawGizmos() {
        Gizmos.DrawRay(transform.position + new Vector3(0.0f, 2.0f, 0.0f), transform.forward * 3.0f);
    }

    void UpdateAnimator(){
		_animator.SetBool ("Walking", _isWalking);
		_animator.SetBool ("Running", _isRunning);
		_animator.SetBool ("Crouch", _isCrouch);
		_animator.SetBool ("Sitting", _isSit);
	}

	void AnimTimer(){
		switch (_counting) {
		case true:
			_timer += 1 * Time.deltaTime;
			if (_timer >= _animationWaitTime) {
				_counting = false;
			}
			break;
		case false:
			_timer = 0;
			break;
		}
	}

	void WaterAnim(){
		var _emission = _water.emission;
		if (_isJesus) {
			if (_isRunning) {
				_emission.rateOverTime = _waterRateMax;
				if (_water.isPlaying == false) {
					_water.Play ();
				}
			}
			if (_isWalking && _isRunning == false) {
				if (_water.isPlaying == false) {
					_water.Play ();
				}
				_emission.rateOverTime = _waterRateMin;
			}
			if (_isWalking == true) {
				if (_splash.isPlaying == false) {
					_splash.Play ();
				}
			}
			if (_isWalking == false) {
				_emission.rateOverTime = 2;
				_splash.Stop ();
			}
		} else {
			_water.Stop ();
			_splash.Stop ();
		}
	}

	void OnTriggerEnter(Collider water) {
		if (water.tag == "Water") {
			_isJesus = true;
		} else {
			_isJesus = false;
		}
	}
}