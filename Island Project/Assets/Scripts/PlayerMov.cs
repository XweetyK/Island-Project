using UnityEngine;
using System.Collections;

public class PlayerMov : MonoBehaviour {
    [SerializeField] private Transform _cameraSystem;

    [SerializeField] private float _animationWaitTime;
    [SerializeField] private int _waterRateMin;
    [SerializeField] private int _waterRateMax;

    ParticleSystem _water;
    [SerializeField] ParticleSystem _splash;

    [SerializeField] float _movSpeed;
    [SerializeField] float _rotSpeed;

    [SerializeField] string _interactLayer = "Interactable";
    Vector3 MovementDirection;
    Rigidbody _rb;
    float _angle;

    float _currentSpeed;

    bool _isRunning;
    bool _isWalking;
    bool _isJesus;

    int _layerMask;

    //Movement Input
    float _movX;
    float _movY;

    Animator _animator;
    RaycastHit hit;

    void Start() {
        _isWalking = _isRunning = false;
        _animator = GetComponent<Animator>();
        _water = GetComponent<ParticleSystem>();
        _rb = gameObject.GetComponent<Rigidbody>();
        _isJesus = false;
        _layerMask = LayerMask.GetMask(_interactLayer);
    }

    void Update() {
        KeyInput();
        Movement();
        UpdateAnimator();
        WaterAnim();
    }

    void FixedUpdate() {
        Vector3 gravity = -9.8f * 2.0f * Vector3.up;
        _rb.AddForce(gravity, ForceMode.Acceleration);
    }

    void Movement() {
        _currentSpeed = _isRunning ? _movSpeed * 2 : _movSpeed;
        MovementDirection = (_movX * _cameraSystem.right + _movY * _cameraSystem.forward) * _currentSpeed;
        _rb.velocity = new Vector3(MovementDirection.x, _rb.velocity.y, MovementDirection.z);
        _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _currentSpeed);

        if (_rb.velocity.x != 0 && _rb.velocity.z != 0) {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(new Vector3(_rb.velocity.x, 0, _rb.velocity.z)), Time.deltaTime * _rotSpeed);
        }
    }

    void KeyInput() {
        //Mov
        if (GameManager.Instance.PlayerInput) {
            _movX = Input.GetAxis("Horizontal");
            _movY = Input.GetAxis("Vertical");
        } else {
            _movX = 0.0f;
            _movY = 0.0f;
        }
        if (Input.GetButton("Shift") && GameManager.Instance.PlayerInput) {
            _isRunning = true;
        } else {
            _isRunning = false;
        }
        if ((_movX != 0 || _movY != 0) && GameManager.Instance.PlayerInput) {
            _isWalking = true;
        } else {
            _isWalking = false;
        }

        if (Physics.Raycast(transform.position + new Vector3(0.0f, 2.0f, 0.0f), transform.TransformDirection(Vector3.forward), out hit, 5.0f, _layerMask)) {
            foreach (var item in hit.collider.gameObject.GetComponents<DialogTrigger>()) {
                if (item.enabled) {
                    Debug.Log("dialog enabled");
                    if (Input.GetButtonDown("Submit")) {
                        item.Trigger();
                    }
                    FindObjectOfType<DialogManager>().PromptActive(true);
                } else {
                    Debug.Log("dialog disabled");
                }
            }
        } else {
            FindObjectOfType<DialogManager>().PromptActive(false);
        }
    }
    void OnDrawGizmos() {
        Gizmos.DrawRay(transform.position + new Vector3(0.0f, 2.0f, 0.0f), transform.forward * 5.0f);
    }

    void UpdateAnimator() {
        _animator.SetBool("Walking", _isWalking);
        _animator.SetBool("Running", _isRunning);
    }

    void WaterAnim() {
        var _emission = _water.emission;
        if (_isJesus) {
            if (_isRunning) {
                _emission.rateOverTime = _waterRateMax;
                if (_water.isPlaying == false) {
                    _water.Play();
                }
            }
            if (_isWalking && _isRunning == false) {
                if (_water.isPlaying == false) {
                    _water.Play();
                }
                _emission.rateOverTime = _waterRateMin;
            }
            if (_isWalking == true) {
                if (_splash.isPlaying == false) {
                    _splash.Play();
                }
            }
            if (_isWalking == false) {
                _emission.rateOverTime = 2;
                _splash.Stop();
            }
        } else {
            _water.Stop();
            _splash.Stop();
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