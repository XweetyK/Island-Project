using UnityEngine;
using System.Collections;

public class PlayerMov : MonoBehaviour {

	[SerializeField]private float _walkSpeed;
	[SerializeField]private float _runSpeed;
	[SerializeField]private float _turnSmoothTime;
	[SerializeField]private float _speedSmoothTime;
	[SerializeField]private float _animationWaitTime;
	[SerializeField]private int _waterRateMin;
	[SerializeField]private int _waterRateMax;

	ParticleSystem _water;
	[SerializeField]ParticleSystem _splash;


	float _turnSmoothVel;
	float _speedSmoothVel;
	float _currentSpeed;

	bool _isRunning;
	bool _isWalking;
	bool _isSit;
	bool _isCrouch;
	bool _isJesus;

	float _timer = 0;
	bool _counting = false;

	Animator _animator;

	void Start () {
		_isWalking = _isRunning = _isCrouch = _isSit = false;
		_animator = GetComponent<Animator> ();
		_water = GetComponent<ParticleSystem> ();
		_isJesus = false;
	}

	void Update () {
		KeyInput ();
		Movement ();
		UpdateAnimator ();
		AnimTimer ();
		WaterAnim ();
	}

	void Movement(){
		if (_counting==false || _isSit==true) {
			Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
			Vector2 inputDir = input.normalized;

			if (inputDir != Vector2.zero) {
				float targetRotation = Mathf.Atan2 (inputDir.x, inputDir.y) * Mathf.Rad2Deg;
				transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref _turnSmoothVel, _turnSmoothTime);
			}

			float targetSpeed = ((_isRunning) ? _runSpeed : _walkSpeed) * inputDir.magnitude;
			_currentSpeed = Mathf.SmoothDamp (_currentSpeed, targetSpeed, ref _speedSmoothVel, _speedSmoothTime);
			transform.Translate (transform.forward * _currentSpeed * Time.deltaTime, Space.World);
		}
	}

	void KeyInput(){

		if (Input.GetButton ("Space")) {
			_isRunning = true;
		} else {
			_isRunning = false;
		}
		if (Input.GetButton ("Shift")) {
			_isCrouch = true;
		} else {
			_isCrouch = false;
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