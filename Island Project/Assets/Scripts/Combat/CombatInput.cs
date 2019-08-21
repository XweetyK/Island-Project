using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatInput : MonoBehaviour {

    enum InputCombo { NONE, ATTACK, SPECIAL, DEFENSE, RUN, ITEM };
    enum State { STARTTURN, COMBO, SENT, MISSED, ENDTURN};
    [SerializeField] int _level;
    [SerializeField] private float _limitTime;
    [SerializeField] private Sprite _arrowFilled;
    [SerializeField] private Sprite _arrowCommon;
    [SerializeField] private Image[] _attackArrows;
    [SerializeField] private Image[] _defenseArrows;
    [SerializeField] private Image[] _specialArrows;
    [SerializeField] private Image _timerBarFill;
    [SerializeField] [Range(0.0f, 1.0f)] private float _arrowOpacity;
    [SerializeField] Animator _UiAnimator;

    private int[] _attackCombo = new int[10] { 1, 1, 2, 1, 3, 4, 3, 4, 2, 2 };
    private int[] _specialCombo = new int[10] { 3, 3, 4, 2, 2, 1, 3, 1, 3, 4 };
    private int[] _defenseCombo = new int[10] { 4, 4, 1, 1, 4, 4, 3, 3, 2, 2 };
    private int[] _actualCombo = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    InputCombo _iCombo = InputCombo.NONE;
    State _state = State.STARTTURN;
    private int _cont = 0;
    private bool _blocked = false;
    private int _levelLimit;
    private float _time;

    private Color _opacityBack;
    private Color _opacityHeld;

    private void Start() {
        _time = _limitTime;
        _opacityBack = _opacityHeld = Color.white;
        _opacityBack.a = _arrowOpacity;

        if (_level < 10) {
            for (int i = _level; i < 10; i++) {
                _attackArrows[i].gameObject.SetActive(false);
                _defenseArrows[i].gameObject.SetActive(false);
                _specialArrows[i].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < _level; i++) {
            _attackArrows[i].color = _opacityBack;
            _specialArrows[i].color = _opacityBack;
            _defenseArrows[i].color = _opacityBack;
        }
    }

    void Update() {
        if (_state==State.STARTTURN || _state==State.COMBO) {
            InputCheck();
        }

        UpdateArrows();
        Debug.Log(_state);
        if (Input.GetButton("Cancel")) {
            NewTurn();
            Debug.Log("check");
        }
        switch (_state) {
            case State.COMBO:
                Timer();
                break;
            case State.SENT:
                Send();
                break;
            case State.MISSED:
                Missed();
                break;
            case State.ENDTURN:
                break;
        }
    }

    void InputCheck() {
        if (_cont == _level) {
            _blocked = true;
        }
        if (_blocked == false) {
            if (Input.GetButtonDown("Up")) {
                _actualCombo[_cont] = 1;
                _state = State.COMBO;
                ComboCheck();
            }
            if (Input.GetButtonDown("Down")) {
                _actualCombo[_cont] = 2;
                _state = State.COMBO;
                ComboCheck();
            }
            if (Input.GetButtonDown("Left")) {
                _actualCombo[_cont] = 3;
                _state = State.COMBO;
                ComboCheck();
            }
            if (Input.GetButtonDown("Right")) {
                _actualCombo[_cont] = 4;
                _state = State.COMBO;
                ComboCheck();
            }
        } else {
            _state = State.SENT;
        }

        if (Input.GetButtonDown("Space")) {
            _state = State.SENT;
        }
    }

    void ComboCheck() {
        switch (_actualCombo[0]) {
            case 0:
                _iCombo = InputCombo.NONE;
                break;
            case 1:
                _iCombo = InputCombo.ATTACK;
                break;
            case 2:
                _iCombo = InputCombo.RUN;
                break;
            case 3:
                _iCombo = InputCombo.SPECIAL;
                break;
            case 4:
                _iCombo = InputCombo.DEFENSE;
                
                break;
        }
        switch (_iCombo) {
            case InputCombo.ATTACK:
                if (_actualCombo[_cont] == _attackCombo[_cont]) {
                    _cont++;
                } else { _state = State.MISSED; }
                break;
            case InputCombo.DEFENSE:
                if (_actualCombo[_cont] == _defenseCombo[_cont]) {
                    _cont++;
                } else { _state = State.MISSED; }
                break;
            case InputCombo.SPECIAL:
                if (_actualCombo[_cont] == _specialCombo[_cont]) {
                    _cont++;
                } else { _state = State.MISSED; }
                break;
        }
    }

    void Missed() {
        _cont = 0;
        for (int i = 0; i < 10; i++) {
            _actualCombo[i] = 0;
        }
        _state = State.ENDTURN;
    }

    void Timer() {
        if (_time > 0) {
            _time -= Time.deltaTime;
        } else { Missed(); }
        
    }

    public void NewTurn() {
        _blocked = false;
        _state = State.STARTTURN;
        _time = _limitTime;
    }

    void Send() {
        _blocked = false;
        _cont = 0;
        _state = State.ENDTURN;
    }

    void ArrowColorHeld() {
        switch (_iCombo) {
            case InputCombo.ATTACK:
                _attackArrows[_cont - 1].color = _opacityHeld;
                break;
            case InputCombo.SPECIAL:
                _specialArrows[_cont - 1].color = _opacityHeld;
                break;
            case InputCombo.DEFENSE:
                _defenseArrows[_cont - 1].color = _opacityHeld;
                break;
        }
    }
    void ArrowColorMissed() {
        switch (_iCombo) {
            case InputCombo.ATTACK:
                for (int i = 0; i < _level; i++) {
                    _attackArrows[i].color = _opacityBack;
                }
                break;
            case InputCombo.SPECIAL:
                for (int i = 0; i < _level; i++) {
                    _specialArrows[i].color = _opacityBack;
                }
                break;
            case InputCombo.DEFENSE:
                for (int i = 0; i < _level; i++) {
                    _defenseArrows[i].color = _opacityBack;
                }
                break;
        }
    }
    void ArrowColorSent() {
        switch (_iCombo) {
            case InputCombo.ATTACK:
                for (int i = 0; i < _cont; i++) {
                    _attackArrows[i].sprite = _arrowFilled;
                }
                break;
            case InputCombo.SPECIAL:
                for (int i = 0; i < _cont; i++) {
                    _specialArrows[i].sprite = _arrowFilled;
                }
                break;
            case InputCombo.DEFENSE:
                for (int i = 0; i < _cont; i++) {
                    _defenseArrows[i].sprite = _arrowFilled;
                }
                break;
        }
    }
    void ArrowColorEnd() {
        switch (_iCombo) {
            case InputCombo.ATTACK:
                for (int i = 0; i < _level; i++) {
                    _attackArrows[i].color = _opacityBack;
                }
                break;
            case InputCombo.SPECIAL:
                for (int i = 0; i < _level; i++) {
                    _specialArrows[i].color = _opacityBack;
                }
                break;
            case InputCombo.DEFENSE:
                for (int i = 0; i < _level; i++) {
                    _defenseArrows[i].color = _opacityBack;
                }
                break;
        }
    }

    void UpdateArrows() {
        switch (_state) {
            case State.STARTTURN:
                _UiAnimator.SetBool("EndTurn", false);
                _UiAnimator.SetBool("Attack", false);
                _UiAnimator.SetBool("Defense", false);
                _UiAnimator.SetBool("Special", false);
                for (int i = 0; i < 10; i++) {
                    _attackArrows[i].sprite = _arrowCommon;
                    _specialArrows[i].sprite = _arrowCommon;
                    _defenseArrows[i].sprite = _arrowCommon;
                }
                break;
            case State.COMBO:
                switch (_iCombo) {
                    case InputCombo.NONE:
                        _UiAnimator.SetBool("Attack", false);
                        _UiAnimator.SetBool("Defense", false);
                        _UiAnimator.SetBool("Special", false);
                        break;
                    case InputCombo.ATTACK:
                        _UiAnimator.SetBool("Attack", true);
                        break;
                    case InputCombo.SPECIAL:
                        _UiAnimator.SetBool("Special", true);
                        break;
                    case InputCombo.DEFENSE:
                        _UiAnimator.SetBool("Defense", true);
                        break;
                }
                ArrowColorHeld();
                break;

            case State.SENT:
                _UiAnimator.SetBool("Attack", false);
                _UiAnimator.SetBool("Defense", false);
                _UiAnimator.SetBool("Special", false);
                _UiAnimator.SetBool("EndTurn", true);
                ArrowColorSent();
                break;

            case State.MISSED:
                _UiAnimator.SetBool("Attack", false);
                _UiAnimator.SetBool("Defense", false);
                _UiAnimator.SetBool("Special", false);
                _UiAnimator.SetBool("EndTurn", true);
                ArrowColorMissed();
                break;

            case State.ENDTURN:
                ArrowColorEnd();
                break;

            default:
                break;
        }

        _timerBarFill.fillAmount = ((_time * 100) / _limitTime) * 0.01f;
    }

    public bool EndTurn {
        get {
            if (_state == State.ENDTURN) {
                return true;
            } else { return false; }
        }
    }
}
