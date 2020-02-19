using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicController : MonoBehaviour {
    [SerializeField] float _animDuration;
    [SerializeField] string _event;
    PlayableDirector _dir;
    float _duration = 0;

    void Start() {
        _dir = gameObject.GetComponent<PlayableDirector>();
        if (EventManager.Instance.GetEvent(_event)) {
            Destroy(this.gameObject);
        } else { _dir.Play(); }
    }

    void Update() {
        if (!EventManager.Instance.GetEvent(_event)) {
            _duration += Time.deltaTime;
            if (_duration >= _animDuration) {
                EventManager.Instance.UpdateEvent(_event, true);
                Destroy(this.gameObject);
            }
        }
    }
}
