using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicController : MonoBehaviour {
    [SerializeField] float _animDuration;
    [SerializeField] string _event;
    [SerializeField] string _activatedBy;
    [SerializeField] bool _startCombatAtEnd;
    [SerializeField] EnemyMov _enemy;
    [SerializeField] float _combatStart;
    [SerializeField] Canvas _playerCanvas;
    [SerializeField] InCutsceneDialog _dial;
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
        if (!string.IsNullOrWhiteSpace(_activatedBy)) {
            if (EventManager.Instance.GetEvent(_activatedBy)) {
                if (!_active) {
                    if (_dial != null) {
                        _dial.StartDialog();
                    }
                    _dir.Play();
                    _playerCanvas.enabled = false;
                    GameManager.Instance.Pausable = false;
                    GameManager.Instance.PlayerInput = false;
                    GameManager.Instance.PlayerInteract = false;
                    if (_enemy != null) {
                        _enemy.gameObject.SetActive(true);
                    }
                    _active = true;
                }
            }
        } else {
            if (!_active) {
                if (_dial != null) {
                    _dial.StartDialog();
                }
                _dir.Play();
                _playerCanvas.enabled = false;
                GameManager.Instance.Pausable = false;
                GameManager.Instance.PlayerInput = false;
                GameManager.Instance.PlayerInteract = false;
                if (_enemy!=null) {
                    _enemy.gameObject.SetActive(true);
                }
                _active = true;
            }
        }
        if (_active) {
            if (!EventManager.Instance.GetEvent(_event)) {
                _duration += Time.deltaTime;
                if (_startCombatAtEnd) {
                    if (_duration >= _combatStart) {
                        GameManager.Instance.ChangeGameMode(GameManager.GameMode.COMBAT);
                        GameManager.Instance.SetActualEnemy(_enemy);
                    }
                }
                if (_duration >= _animDuration) {
                    _playerCanvas.enabled = true;
                    GameManager.Instance.Pausable = true;
                    GameManager.Instance.PlayerInput = true;
                    GameManager.Instance.PlayerInteract = true;
                    EventManager.Instance.UpdateEvent(_event, true);
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
