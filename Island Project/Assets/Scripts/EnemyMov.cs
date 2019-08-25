using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMov : MonoBehaviour
{
    [SerializeField] Animator _anim;
    [SerializeField] float _maxDistance;
    private GameObject _playerPos;
    private Vector3 _LocalPlayerPos;
    private Vector3 _LocalPos;
    private float _distance;
    private enum State {IDLE, PATROL, ALERT, DETECTED, CONNECTED};
    private State _state;

    void Start(){
        _playerPos = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() {

        switch (_state) {
            case State.IDLE:
                break;
            case State.PATROL:
                break;
            case State.ALERT:
                break;
            case State.DETECTED:
                break;
            case State.CONNECTED:
                break;
        }
        DetectPlayer();
    }

    private bool DetectPlayer() {
        _LocalPlayerPos= transform.InverseTransformPoint(_playerPos.transform.position);
        _LocalPos = transform.InverseTransformPoint(transform.position);
        _distance = Vector3.Distance(transform.position, _playerPos.transform.position);

        if (_LocalPlayerPos.z > 0 && _LocalPlayerPos.x > (-_LocalPlayerPos.z) && _LocalPlayerPos.x < _LocalPlayerPos.z) {
            Debug.Log("in area!");
            if (_distance < _maxDistance) {
                Debug.LogWarning("Detected!!!");
                return true;
            } else { return false; }
        } else { return false; }
    }
}
