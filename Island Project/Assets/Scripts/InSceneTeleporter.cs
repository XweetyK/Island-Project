using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogTrigger))]
[RequireComponent(typeof(BoxCollider))]

public class InSceneTeleporter : MonoBehaviour {

    [SerializeField] Transform _destination;
    [SerializeField] bool _isActive;
    Transform _player;
    Transform _camSys;
    DialogTrigger _dt;


    private void Start() {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _camSys = GameObject.FindGameObjectWithTag("CameraSystem").transform;
        _dt = GetComponent<DialogTrigger>();
    }

    private void Update() {
        if (_isActive) {
            _dt.enabled = false;
        } else {
            _dt.enabled = true;
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject == GameObject.FindGameObjectWithTag("Player")) {
            if (_isActive) {
                ///------------transition-----------------
                _player.position = _destination.position;
                _player.rotation = _destination.rotation;
                _camSys.rotation = _destination.rotation;
            }
        }
    }  
}
