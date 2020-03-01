using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogTrigger))]
[RequireComponent(typeof(BoxCollider))]

public class InSceneTeleporter : MonoBehaviour {

    [SerializeField] Transform _destination;
    [SerializeField] bool _isActive;
    [SerializeField] string _enableTrigger;
    Animator _canvas;
    Transform _player;
    Transform _camSys;
    DialogTrigger _dt;
    bool _init = false;


    private void Start() {
        _canvas = GameObject.FindGameObjectWithTag("TransitionCanvas").GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _camSys = GameObject.FindGameObjectWithTag("CameraSystem").transform;
        _dt = GetComponent<DialogTrigger>();
    }

    private void Update() {
        if (!string.IsNullOrWhiteSpace(_enableTrigger)) {
            _isActive = EventManager.Instance.GetEvent(_enableTrigger);
        }
        if (_isActive) {
            _dt.enabled = false;
        } else {
            _dt.enabled = true;
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject == GameObject.FindGameObjectWithTag("Player")) {
            if (_isActive) {
                _init = false;
                _canvas.SetTrigger("Door");
                GameManager.Instance.PlayerInput = false;
                Invoke("Teleport", 0.5f);
            }
        }
    }

    void Teleport() {
        if (!_init) {
            _player.position = _destination.position;
            _player.rotation = _destination.rotation;
            _camSys.rotation = _destination.rotation;
            _init = true;
            Invoke("Teleport",0.3f);
        }
        GameManager.Instance.PlayerInput = true;
    }
}
