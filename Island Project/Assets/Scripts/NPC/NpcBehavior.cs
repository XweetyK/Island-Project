using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcBehavior : MonoBehaviour {
    public enum State { ROAMING, SITTING, IDLE, DANCE, CHEER, SING }
    [SerializeField] private State _state;
    [SerializeField] private bool _talking;
    [SerializeField] private bool _fem;
    private Animator _anim;
    private NavMeshAgent _nma;
    float _timer=0;

    private void Start() {
        _anim = gameObject.GetComponent<Animator>();
        _nma = gameObject.GetComponent<NavMeshAgent>();
        if (_talking && _state == State.IDLE) {
            TalkingBehavior();
        }
    }
    void Update() {
        Movement();
        UpdateAnim();
    }
    private void Movement() {
        if (_state == State.ROAMING) {
            _timer += Time.deltaTime;
            if (_timer > 10.0f) {
                _nma.SetDestination(RandomWalk(gameObject.transform.position));
                _timer = 0;
            }
        }
    }
    private void UpdateAnim() {
        switch (_state) {
            case State.ROAMING:
                if (_nma.velocity.x != 0 && _nma.velocity.z != 0) {
                    if (_fem) {
                        _anim.SetBool("FemWalk", true);
                    } else {
                        _anim.SetBool("Walk", true);
                    }
                } else { _anim.SetBool("Walk", false); _anim.SetBool("FemWalk", false); }
                break;
            case State.SITTING:
                if (_fem) {
                    _anim.SetBool("SitFem", true);
                } else {
                    _anim.SetBool("SitMale", true);
                }
                break;
            case State.IDLE:
                break;
            case State.SING:
                _anim.SetBool("Sing", true);
                break;
            case State.CHEER:
                _anim.SetBool("Cheer", true);
                break;
            case State.DANCE:
                _anim.SetBool("Dance", true);
                break;

        }
    }
    private void TalkingBehavior() {
        int _rand = Random.Range(0, 5);
        switch (_rand) {
            case 0:
                Invoke("TalkingBehavior", 5.0f);
                break;
            case 1:
                _anim.SetTrigger("Talk1");
                Invoke("TalkingBehavior", 4.0f);
                break;
            case 2:
                _anim.SetTrigger("Talk2");
                Invoke("TalkingBehavior", 20.0f);
                break;
            case 3:
                _anim.SetTrigger("Talk3");
                Invoke("TalkingBehavior", 5.0f);
                break;
            case 4:
                _anim.SetTrigger("Talk4");
                Invoke("TalkingBehavior", 8.0f);
                break;
            case 5:
                _anim.SetTrigger("Talk5");
                Invoke("TalkingBehavior", 20.0f);
                break;
        }
    }

    private Vector3 RandomWalk(Vector3 areaPos) {
        Vector3 _dest = new Vector3(
               Random.Range(areaPos.x - 30, areaPos.x + 30),
               areaPos.y + 2,
               Random.Range(areaPos.z - 30, areaPos.z + 30));

        return _dest;
    }
}
