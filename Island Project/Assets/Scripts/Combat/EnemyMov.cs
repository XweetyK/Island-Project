using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMov : MonoBehaviour {
    [SerializeField] float _maxDistance;
    [SerializeField] GameObject _head;
    [SerializeField] float _runningSpeed;
    [SerializeField] bool _justAnim;
    [SerializeField] Material _mat;
    [SerializeField] SkinnedMeshRenderer _mesh;
    private Animator _anim;
    private GameObject _playerPos;
    private enum State { PATROL, ALERT, DETECTED, DEAD, COMBAT };
    private State _state;
    private NavMeshAgent _nma;
    private float _origSpeed;
    private Vector3 _lastSeen;

    private bool _seen = false;
    private bool _alreadyDead = false;

    private Vector3 _targetDist;
    private Vector3 _orderHead;
    public float _angle;
    public float _distance;
    bool _groovin = false;

    private int _randAnim;
    float _timerAnim = 0;
    private int _randAlert;
    float _timerAlert = 0;

    private int _randDet;
    float _timerDet = 0;
    bool _isStop = false;

    bool _init = false;

    BoxCollider _Collider;

    void Start() {
        _playerPos = GameObject.FindGameObjectWithTag("Player");
        _anim = gameObject.GetComponent<Animator>();
        _nma = gameObject.GetComponent<NavMeshAgent>();
        _state = State.PATROL;
        _origSpeed = _nma.speed;
        _init = true;
        _mesh.materials[0] = _mat;
        GameManager.Instance.AddEnemy(this.gameObject);
    }

    void Update() {
        TestAngle();
        PlayerDetector();
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

    private void PlayerDetector() {
        if (_angle < 45.0f && _state != State.DEAD) {
            //Area de vision------------------------------------
            if (_distance < _maxDistance && !_groovin) {
                _state = State.DETECTED;
                _seen = true;
            } else if (_seen) {
                _state = State.ALERT;
            } else { _state = State.PATROL; }
        }
    }

    private void Animations() {
        if (!_justAnim) {
            switch (_state) {
                case State.PATROL:
                    PatrolBehavior();
                    break;
                case State.ALERT:
                    AlertBehavior();
                    break;
                case State.DETECTED:
                    DetectedBehavior();
                    break;
                case State.COMBAT:
                    Debug.Log("Combat");
                    break;
                case State.DEAD:
                    _anim.SetBool("_walking", false);
                    _anim.SetBool("_running", false);
                    _anim.SetBool("_dead", true);
                    _anim.SetBool("_dissolve", true);
                    _nma.isStopped = true;
                    break;
            }
        }
        if (_nma.velocity == Vector3.zero) {
            _anim.SetBool("_walking", false);
            _anim.SetBool("_running", false);

        } else {
            switch (_state) {
                case State.DETECTED:
                    _anim.SetBool("_walking", false);
                    _anim.SetBool("_running", true);
                    break;
                default:
                    _anim.SetBool("_walking", true);
                    _anim.SetBool("_running", false);
                    break;
            }
        }
    }
    private void PatrolBehavior() {
        _timerAnim += Time.deltaTime;
        if (_groovin) {
            if (_timerAnim > 15) {
                _timerAnim = 0;
                _randAnim = Random.Range(1, 100);
                _groovin = false;
            }
        } else {
            if (_timerAnim > 5) {
                _timerAnim = 0;
                _randAnim = Random.Range(1, 100);
                _groovin = false;
            }
        }

        if (_randAnim >= 30 && _randAnim < 60) {
            _randAnim = -1;
            _nma.isStopped = true;
            _anim.SetTrigger("_looking");
        } else if (_randAnim > 0 && _randAnim < 30) {
            _randAnim = -1;
            _nma.isStopped = true;
        } else if (_randAnim >= 60 && _randAnim < 90) {
            _randAnim = -1;
            _anim.SetBool("_walking", true);
            _nma.isStopped = false;

            _nma.SetDestination(RandomWalk(gameObject.transform.position));
        } else if (_randAnim >= 90) {
            _groovin = true;
            _randAnim = -1;
            _nma.isStopped = true;
            _anim.SetTrigger("_dance");
        }
    }
    private void AlertBehavior() {
        SfxManager.Instance.Stop(this.gameObject.GetComponent<AudioSource>());
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
        _nma.speed = _runningSpeed;
        _nma.SetDestination(_playerPos.transform.position);
        SfxManager.Instance.Player(this.gameObject.GetComponent<AudioSource>(), SFX.COMBAT, 0, false);
        if (!_isStop) {
            _nma.isStopped = false;
        }

        if (_timerDet > 10) {
            _nma.speed = Mathf.Lerp(_runningSpeed, _runningSpeed + 5, Time.deltaTime);
        }
        _lastSeen = _playerPos.transform.position;
    }

    public void Dead() {
        SfxManager.Instance.Stop(this.gameObject.GetComponent<AudioSource>());
        if (!_alreadyDead) {
            transform.LookAt(_playerPos.transform);
        }
        if (!_justAnim) {
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        GameManager.Instance.RemoveEnemy(this.gameObject);
        _state = State.DEAD;
        Debug.Log("Dead!");
        Invoke("Dead", 3.5f);
        if (_alreadyDead) {
            Destroy(this.gameObject);
        }
        _alreadyDead = true;
    }

    private Vector3 RandomWalk(Vector3 areaPos) {
        SfxManager.Instance.Stop(this.gameObject.GetComponent<AudioSource>());
        Vector3 _dest = new Vector3(
               Random.Range(areaPos.x - 30, areaPos.x + 30),
               areaPos.y + 2,
               Random.Range(areaPos.z - 30, areaPos.z + 30));

        return _dest;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            GameManager.Instance.ChangeGameMode(GameManager.GameMode.COMBAT);
            GameManager.Instance.SetActualEnemy(this);
            _state = State.COMBAT;
        }
    }

    public void IsFrozen(bool froze) {
        if (froze) {
            _isStop = true;
            _nma.isStopped = true;
            _anim.enabled = false;
        } else {
            _isStop = false;
            _nma.isStopped = false;
            _anim.enabled = true;
        }
    }
    private void OnEnable() {
        if (_init) {
            IsFrozen(false);
        }
    }
}