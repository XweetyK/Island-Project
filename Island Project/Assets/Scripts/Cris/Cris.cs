using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cris : MonoBehaviour
{
    enum State {CITYANNOYED, BEACHSEARCHING, TOTHEMUSEUM};
    [SerializeField] State _state;
    [SerializeField] Transform _target;
    [SerializeField] GameObject _oldDialogue;
    [SerializeField] DialogTrigger _newDialogue;
    NavMeshAgent _nma;
    Animator _anim;
    float _time = 0;

    private void Start() {
        if (_state == State.CITYANNOYED) {
            if (EventManager.Instance.GetEvent("CrisAnnoyed")) {
                Destroy(this.gameObject);
            }
        }
        _nma = gameObject.GetComponent<NavMeshAgent>();
        _anim = gameObject.GetComponent<Animator>();
    }

    private void Update() {
        switch (_state) {
            case State.CITYANNOYED:
                if (EventManager.Instance.GetEvent("CrisLeaveCity")) {
                    _nma.SetDestination(_target.position);
                    if (_nma.remainingDistance > 0 && _nma.remainingDistance < 0.005f) {
                        EventManager.Instance.UpdateEvent("CrisAnnoyed", true);
                    }
                }
                if (EventManager.Instance.GetEvent("CrisAnnoyed")) {
                    Destroy(this.gameObject);
                }
                break;
            case State.BEACHSEARCHING:
                if (EventManager.Instance.GetEvent("CrisSearching")) {
                    _time += Time.deltaTime;
                    if (_time > 10) {
                        _nma.SetDestination(RandomWalk(gameObject.transform.position));
                        _time = 0;
                    }
                    if (EventManager.Instance.GetEvent("FoundItem")) {
                        Destroy(_oldDialogue);
                        _newDialogue.enabled = true;
                    }
                }
                break;
            case State.TOTHEMUSEUM:
                if (EventManager.Instance.GetEvent("ToMuseum")) {
                    _nma.SetDestination(_target.position);
                    EventManager.Instance.UpdateEvent("InTheMuseum", true);
                    if (_nma.remainingDistance > 0 && _nma.remainingDistance < 0.005f) {
                        Destroy(this.gameObject);
                    }
                }
                if (EventManager.Instance.GetEvent("MuseumStart")) {
                    Destroy(this.gameObject);
                }
                break;
        }
        if (_nma.velocity.x > 0 || _nma.velocity.z > 0) {
            _anim.SetBool("_walk", true);
        } else {
            _anim.SetBool("_walk", false);
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
