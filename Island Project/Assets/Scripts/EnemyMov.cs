using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMov : MonoBehaviour {
    [SerializeField] float _maxDistance;
    [SerializeField] GameObject _head;
    [SerializeField] float _runningSpeed;
    private Animator _anim;
    private GameObject _playerPos;
    private enum State { PATROL, ALERT, DETECTED, CONNECTED };
    private State _state;
    private NavMeshAgent _nma;
    private float _origSpeed;
    private Vector3 _lastSeen;

    private bool _seen = false;

    private Vector3 _targetDist;
    private Vector3 _orderHead;
    public float _angle;
    public float _distance;

    private int _randAnim;
    float _timerAnim=0;
    private int _randAlert;
    float _timerAlert = 0;

    private int _randDet;
    float _timerDet = 0;


    void Start() {
        _playerPos = GameObject.FindGameObjectWithTag("Player");
        _anim = gameObject.GetComponent<Animator>();
        _nma = gameObject.GetComponent<NavMeshAgent>();
        _state = State.PATROL;
        _origSpeed = _nma.speed;
    }

    void Update() {
        TestAngle();
        Detector();
        Animations();
    }

    private void TestAngle() {
        _targetDist = _playerPos.transform.position - _head.transform.position;
        _targetDist.y = 0;
        _orderHead = _head.transform.forward;
        _orderHead.y = 0;
        _angle = Vector3.Angle(_orderHead, _targetDist);
        _distance = Vector3.Distance(_playerPos.transform.position, gameObject.transform.position);
    }

    private void Detector() {
        if (_angle < 45.0f) {
            //Area de vision------------------------------------
            if (_distance < _maxDistance) {
                _state = State.DETECTED;
                _seen = true;
            } else if (_seen) {
                _state = State.ALERT;
            } else { _state = State.PATROL; }
        }
    }
    private void Animations() {
        switch (_state) {
            case State.PATROL:
                Debug.Log("patrol");
                PatrolBehavior();
                break;
            case State.ALERT:
                Debug.Log("alert");
                AlertBehavior();
                break;
            case State.DETECTED:
                Debug.Log("detected");
                DetectedBehavior();
                break;
            case State.CONNECTED:
                break;
        }
        if (_nma.velocity == Vector3.zero) {
            _anim.SetBool("_walking", false);
        } else {
            _anim.SetBool("_walking", true);
        }
    }
    private void PatrolBehavior() {
        _timerAnim += Time.deltaTime;

        if (_timerAnim > 5) {
            _timerAnim = 0;
            _randAnim = Random.Range(1, 100);
        }

        if (_randAnim > 30 && _randAnim < 45) {
            _randAnim = -1;
            _nma.isStopped = true;
            _anim.SetTrigger("_looking");
        } else if (_randAnim > 0 && _randAnim <= 30) {
            _randAnim = -1;
            _nma.isStopped = true;
        } else if (_randAnim>=45){
            _randAnim = -1;
            _anim.SetBool("_walking", true);
            _nma.isStopped = false;

            _nma.SetDestination(RandomWalk(gameObject.transform.position));
        }
    }
    private void AlertBehavior() {
        _timerDet = 0;
        _timerAlert += Time.deltaTime;
        _nma.speed = _origSpeed;
        if (_timerAlert % 2 == 0) {
            _randAlert = Random.Range(1, 100);
        }
        if (_nma.velocity == Vector3.zero) {
            _anim.SetTrigger("_searching");
        }
        //if (_randAlert > 0 && _randAlert < 75) {
        //    Debug.Log("lookin'");
        //    _nma.isStopped = false;
        //    _nma.SetDestination(RandomWalk(_lastSeen));
        //}
        if (_timerAlert > 14) {
            _timerAlert = 0;
            _seen = false;
            _state = State.PATROL;
        }
    }
    private void DetectedBehavior() {
        _timerAlert = 0;
        _timerDet += Time.deltaTime;
        _nma.isStopped = false;
        _nma.speed = _runningSpeed;
        _nma.SetDestination(_playerPos.transform.position);

        if (_timerDet > 10) {
            _nma.speed = Mathf.Lerp(_runningSpeed, _runningSpeed+3, Time.deltaTime);
        }
        if (Vector3.Distance(gameObject.transform.position, _playerPos.transform.position) < 4) {
            _anim.SetTrigger("_attack2");
        }
        _lastSeen = _playerPos.transform.position;
    }

    private Vector3 RandomWalk(Vector3 areaPos) {
        Vector3 _dest = new Vector3(
               Random.Range(areaPos.x - 30, areaPos.x + 30),
               areaPos.y + 2,
               Random.Range(areaPos.z - 30, areaPos.z + 30));

        return _dest;
    }
}