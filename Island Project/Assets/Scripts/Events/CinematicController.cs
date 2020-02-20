using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicController : MonoBehaviour {
    [SerializeField] float _animDuration;
    [SerializeField] string _event;
    [SerializeField] string _activatedBy;
    PlayableDirector _dir;
    float _duration = 0;
    bool _active = false;

    void Start() {
        _dir = gameObject.GetComponent<PlayableDirector>();
        if (EventManager.Instance.GetEvent(_event)) {
            Destroy(this.gameObject);
        }
    }

    void Update() {
        if (_activatedBy != "no") {
            if (EventManager.Instance.GetEvent(_activatedBy)) {
                if (!_active) {
                    _dir.Play();
                    _active = true;
                }
            }
        } else {
            if (!_active) {
                _dir.Play();
                _active = true;
            }
        }
        if (_active) {
            if (!EventManager.Instance.GetEvent(_event)) {
                _duration += Time.deltaTime;
                if (_duration >= _animDuration) {
                    EventManager.Instance.UpdateEvent(_event, true);
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
