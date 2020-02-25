using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DJ : MonoBehaviour
{
    Animator _anim;
    enum State {IDLE, DANCING, WAVE, SIT };
    [SerializeField] State _state;

    private void Start() {
        _anim = gameObject.GetComponent<Animator>();
        if (_state==State.DANCING) {
            _anim.SetBool("Dancing", true);
        }
        if (_state==State.SIT) {
            _anim.SetBool("Sit", true);
        }
    }
    private void Update() {
        if (_state == State.WAVE) {
            int rand = Random.Range(0, 100);
            if (rand > 73) {
                _anim.SetTrigger("Wave");
            }
        }
    }
}
