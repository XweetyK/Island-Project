using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcBehavior : MonoBehaviour
{
    public enum State {ROAMING, SITTING, IDLE, DANCE, CHEER, SING}
    [SerializeField] private State _state;
    [SerializeField] private bool _talking;
    [SerializeField] private bool _fem;
    private Animator _anim;
    private NavMeshAgent _nma;

    private void Start() {
        _anim = GetComponent<Animator>();
    }
    void Update()
    {
        UpdateAnim();
    }
    private void UpdateAnim() {
        switch (_state) {
            case State.ROAMING:
                if (_nma.velocity.x > 0 && _nma.velocity.z > 0) {
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
                if (_talking) {
                    _anim.SetBool("Talk", true);
                }
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
}
