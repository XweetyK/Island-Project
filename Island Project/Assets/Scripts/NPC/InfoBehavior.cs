﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InfoBehavior : MonoBehaviour {
    enum State { IDLE, COFFEE, WALKING }
    enum Activity { WORKING, BREAK }
    [SerializeField] Transform[] _targets;
    [SerializeField] DialogTrigger _dialogTrigger;
    Animator _anim;
    NavMeshAgent _nma;
    Dialogue[] _tired;
    Dialogue[] _backtowork;
    Dialogue[] _idleInfo;
    Dialogue[] _restingCafe;
    Dialogue[] _crying;
    int _randChat = 3;
    int _randCrying = 0;
    bool _activeCrying = false;
    float _timer = 0;
    float _coffeeTime = 0;
    int _actualTarget = 0;
    State _state = State.IDLE;
    Activity _act = Activity.WORKING;


    private void Awake() {
        _anim = gameObject.GetComponent<Animator>();
        _nma = gameObject.GetComponent<NavMeshAgent>();
    }

    private void Start() {
        _tired = new Dialogue[1];
        _backtowork = new Dialogue[1];
        _idleInfo = new Dialogue[2];
        _restingCafe = new Dialogue[3];
        _crying = new Dialogue[3];

        SetCharacterDialogue(_tired);
        SetCharacterDialogue(_backtowork);
        SetCharacterDialogue(_idleInfo);
        SetCharacterDialogue(_restingCafe);
        SetCharacterDialogue(_crying);
        DialoguesStartUp();
        UpdateDialog(_idleInfo);
    }

    void UpdateDialog(Dialogue[] dial) {
        _dialogTrigger._dialog.conversations = dial;
    }

    private void Update() {
        Behavior();
        UpdateAnim();
        UpdateRandomThought();
    }

    void Behavior() {
        switch (_state) {
            case State.IDLE:
                _timer += Time.deltaTime;
                if (_timer > 5) {
                    _randCrying = Random.Range(1, 100);
                    _randChat = Random.Range(1, 7);
                    _timer = 0;
                }
                if (_randCrying > 95 && _activeCrying != true) {
                    UpdateDialog(_crying);
                    Crying();
                }
                if (_randCrying < 70) {
                    UpdateDialog(_idleInfo);
                }
                break;
            case State.COFFEE:
                _coffeeTime += Time.deltaTime;
                UpdateDialog(_restingCafe);
                transform.rotation = _targets[3].transform.rotation;
                if (_coffeeTime >= 180) {
                    _state = State.WALKING;
                }
                break;
            case State.WALKING:
                switch (_act) {
                    case Activity.WORKING:
                        if (_nma.remainingDistance == 0.0f) {
                            UpdateDialog(_tired);
                            if (_actualTarget < _targets.Length - 1) {
                                _actualTarget++;
                                _nma.SetDestination(_targets[_actualTarget].position);
                            }
                        }
                        if (Vector3.Distance(transform.position, _targets[3].position)<0.8f) {
                            _state = State.COFFEE;
                            _act = Activity.BREAK;
                        }
                        break;
                    case Activity.BREAK:
                        _coffeeTime = 0;
                        _activeCrying = false;
                        if (_nma.remainingDistance < 0.008f) {
                            UpdateDialog(_backtowork);
                            if (_actualTarget > 1) {
                                _actualTarget--;
                                _nma.SetDestination(_targets[_actualTarget].position);
                            }
                            if (Vector3.Distance(transform.position, _targets[0].position) < 0.8f) {
                                _state = State.IDLE;
                                _act = Activity.WORKING;
                                _randCrying = 0;
                            }
                        }
                        break;
                }
                break;
        }
    }

    void SetCharacterDialogue(Dialogue[] dial) {
        for (int i = 0; i < dial.Length; i++) {
            dial[i] = new Dialogue();
            dial[i]._name = "INFO";
            dial[i]._character = Characters.ORDER;
            dial[i]._face = 10;
            dial[i]._chat = "";
        }
    }

    void DialoguesStartUp() {
        _tired[0]._chat = "I'm really tired, I need a break...";
        _backtowork[0]._chat = "Ready to go back to work!";
        _idleInfo[0]._chat = "Do you know that you can change your viewing area with your mouse?";
        _idleInfo[1]._chat = "Try to take a look at the [CITY] by pressing the right button or scroll to get a better perspective!";
        _restingCafe[0]._chat = "You know, it's not easy to talk to people.";
        _restingCafe[1]._chat = "I always end up panicking.";
        _restingCafe[2]._chat = "Luckily I'm allowed to take a coffee break everytime that happens.";
        _crying[0]._chat = "I can't anymore, this is too hard for me...";
        _crying[1]._chat = "This will end soon INFO... just wait a bit more...";
        _crying[2]._chat = "I need a coffee or I'll break soon...";
    }

    void UpdateRandomThought() {
        switch (_randChat) {
            case 1:
                _idleInfo[0]._chat = "Do you know that you can change your viewing area with your mouse?";
                _idleInfo[1]._chat = "Try to take a look at the [CITY] by pressing the right button or scroll to get a better perspective!";
                break;
            case 2:
                _idleInfo[0]._chat = "[PARADISE] has guest from all over the world!";
                _idleInfo[1]._chat = "That's amazing, don't you think?";
                break;
            case 3:
                _idleInfo[0]._chat = "My brother ORDER can be a little clumsy sometimes, you can avoid them easily if you behave correctly.";
                _idleInfo[1]._chat = "But you know, it's rude to avoid people.";
                break;
            case 4:
                _idleInfo[0]._chat = "Leveling up can make fighting easier.";
                _idleInfo[1]._chat = "But you don't need that here. [PARADISE] is a safe place, remember?";
                break;
            case 5:
                _idleInfo[0]._chat = "Some Godesses statues in [PARADISE] can grant you a blessing.";
                _idleInfo[1]._chat = "Money? Health maybe? You'll never know!";
                break;
            case 6:
                _idleInfo[0]._chat = "The Museum is currently under maintenance. It seems that a bug is causing some problems there.";
                _idleInfo[1]._chat = "I hope it's not a cockroach!";
                break;
            case 7:
                _idleInfo[0]._chat = "I wonder if that guy is ok... I've never seen someone with a broken avatar...";
                _idleInfo[1]._chat = "Huh? Ignore me, I was just thinking out loud haha.";
                break;
        }
    }

    void UpdateAnim() {
        if (_nma.velocity != Vector3.zero) {
            _anim.SetBool("Walking", true);
        } else {
            _anim.SetBool("Walking", false);
        }
        if (_state == State.COFFEE) {
            _anim.SetBool("Sitting", true);
        } else {
            _anim.SetBool("Sitting", false);
        }
    }
    void Crying() {
        if (!_activeCrying) {
            _anim.SetTrigger("Crying");
            Invoke("Crying", 6.5f);
            _activeCrying = true;
        } else {
            _state = State.WALKING;
        }
    }
}
