using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
    [SerializeField] Animator _anim;
    [SerializeField] float _animLenght;
    bool _active;
    bool _stopped = false;

    private void Update() {
        if (Input.GetButtonDown("Esc") && !_active && GameManager.Instance.Pausable) {
            _stopped = true;
            Invoke("ZaWarudo", _animLenght);
            StartPause();
        }
    }

    void StartPause() {
        _anim.SetBool("Paused", true);
        _active = true;
    }
    public void StopPause() {
        _anim.SetBool("Paused", false);
        _stopped = false;
        ZaWarudo();
        _active = false;
    }
    void ZaWarudo() {
        if (_stopped) {
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
        }
    }
}
