using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InfoBehavior : MonoBehaviour
{
    enum State {IDLE, COFFEE, WALKING }
    enum Activity {WORKING, BREAK }
    [SerializeField] Animator _anim;
    [SerializeField] Transform _coffeeTarget;
    [SerializeField] Transform _workTarget;
    [SerializeField] DialogTrigger _dialog;
    [SerializeField] GameObject _mesh;
    [SerializeField] GameObject _sittingMesh;
    NavMeshAgent _nma;
    Dialogue[] _tired;
    Dialogue[] _backtowork;
    Dialogue[] _idleInfo;
    Dialogue[] _restingCafe;
    Dialogue[] _crying;
    int _randChat = 0;
    int _randCrying = 0;
    State _state = State.IDLE;
    Activity _act = Activity.WORKING;

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
    }
    void UpdateDialog() {
        _dialog._dialog.conversations = new Dialogue[3];
    }

    void SetCharacterDialogue(Dialogue[] dial) {
        foreach (Dialogue item in dial) {
            item._name = "INFO";
            item._character = Characters.ORDER;
            item._face = 10;
            item._chat = "";
        }
    }

    void Behavior() {
        switch (_state) {
            case State.IDLE:
                _randCrying = Random.Range(1, 100);
                if (_randCrying > 90) {
                    _state = State.WALKING;
                }
                break;
            case State.COFFEE:
                _mesh.SetActive(false);
                _sittingMesh.SetActive(true);
                break;
            case State.WALKING:
                switch (_act) {
                    case Activity.WORKING:
                        _nma.SetDestination(_coffeeTarget.position);
                        if (_nma.remainingDistance<=0.5f) {
                            _act = Activity.BREAK;
                            _state = State.COFFEE;
                        }
                        break;
                    case Activity.BREAK:
                        _nma.SetDestination(_workTarget.position);
                        if (_nma.remainingDistance <= 0.5f) {
                            _act = Activity.WORKING;
                            _state = State.IDLE;
                        }
                        break;
                }
                break;
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
    }
}
