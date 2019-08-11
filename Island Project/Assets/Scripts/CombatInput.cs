using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatInput : MonoBehaviour {

    enum InputCombo { NONE, ATTACK, SPECIAL, DEFENSE, RUN, ITEM };

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
    //private int[] _runCombo = new int[3] { 2, 2, 2 };
    //private int[] _itemCombo = new int[3] { 5, 5, 5 };
    private int[] _actualCombo = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    private int _cont = 0;
    private bool _blocked = false;
    private int _levelLimit;
    InputCombo _iCombo = InputCombo.NONE;
    private bool _miss = false;
    private bool _endTurn = false;
    private float _time;
    private bool _firstCommand = false;

    private Color _opacityBack;
    private Color _opacityHeld;

    private void Start() {
        _opacityBack = _opacityHeld = Color.white;
        _opacityBack.a = _arrowOpacity;
        _time = _limitTime;
        _endTurn = false;
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
        InputCheck();
        if (_firstCommand) {
            Timer();
        }
    }

    void InputCheck() {
        if (_cont == _level) {
            _blocked = true;
        }
        if (_blocked == false) {
            if (Input.GetButtonDown("Up")) {
                _actualCombo[_cont] = 1;
                _firstCommand = true;
                ComboCheck();
            }
            if (Input.GetButtonDown("Down")) {
                _actualCombo[_cont] = 2;
                _firstCommand = true;
                ComboCheck();
            }
            if (Input.GetButtonDown("Left")) {
                _actualCombo[_cont] = 3;
                _firstCommand = true;
                ComboCheck();
            }
            if (Input.GetButtonDown("Right")) {
                _actualCombo[_cont] = 4;
                _firstCommand = true;
                ComboCheck();
            }
        } else {
            Send();
        }

        if (Input.GetButtonDown("Space")) {
            Send();
        }
    }

    void ComboCheck() {
        switch (_actualCombo[0]) {
            case 0:
                _iCombo = InputCombo.NONE;
                _UiAnimator.SetBool("Attack", false);
                _UiAnimator.SetBool("Defense", false);
                _UiAnimator.SetBool("Special", false);
                break;
            case 1:
                _iCombo = InputCombo.ATTACK;
                _UiAnimator.SetBool("Attack", true);
                break;
            case 2:
                _iCombo = InputCombo.RUN;
                break;
            case 3:
                _iCombo = InputCombo.SPECIAL;
                _UiAnimator.SetBool("Special", true);
                break;
            case 4:
                _iCombo = InputCombo.DEFENSE;
                _UiAnimator.SetBool("Defense", true);
                break;
        }
        switch (_iCombo) {
            case InputCombo.NONE:
                break;
            case InputCombo.ATTACK:
                if (_actualCombo[_cont] == _attackCombo[_cont]) {
                    ArrowColorHeld();
                    _cont++;
                } else { Missed(); }
                break;
            case InputCombo.DEFENSE:
                if (_actualCombo[_cont] == _defenseCombo[_cont]) {
                    ArrowColorHeld();
                    _cont++;
                } else { Missed(); }
                break;
            case InputCombo.SPECIAL:
                if (_actualCombo[_cont] == _specialCombo[_cont]) {
                    ArrowColorHeld();
                    _cont++;
                } else { Missed(); }
                break;
        }
    }

    void Missed() {
        _cont = 0;
        ArrowColorMissed();
        _firstCommand = false;
        _UiAnimator.SetBool("Attack", false);
        _UiAnimator.SetBool("Defense", false);
        _UiAnimator.SetBool("Special", false);
        _UiAnimator.SetBool("EndTurn", true);
        _endTurn = true;
        for (int i = 0; i < 10; i++) {
            _actualCombo[i] = 0;
        }
        _miss = false;
    }

    void Timer() {
        if (_time > 0) {
            _time -= Time.deltaTime;
        } else { Missed(); }
        _timerBarFill.fillAmount = ((_time * 100) / _limitTime)*0.01f;
    }

    public void NewTurn() {
        _blocked = false;
        _endTurn = false;
        _time = _limitTime;
        _firstCommand = false;
        _UiAnimator.SetBool("EndTurn", false);
        _timerBarFill.fillAmount = ((_time * 100) / _limitTime) * 0.01f;
        for (int i = 0; i < 10; i++) {
            _attackArrows[i].sprite = _arrowCommon;
            _specialArrows[i].sprite = _arrowCommon;
            _defenseArrows[i].sprite = _arrowCommon;
        }
    }

    //void EndTurn() {
    //    _endTurn = true;
    //    _time = _limitTime;
    //}

    void Send() {
        ArrowColorSent();
        _UiAnimator.SetBool("Attack", false);
        _UiAnimator.SetBool("Defense", false);
        _UiAnimator.SetBool("Special", false);
        _UiAnimator.SetBool("EndTurn", true);
        _firstCommand = false;
        _blocked = false;
        _endTurn = true;
    }

    void ArrowColorHeld() {
        switch (_iCombo) {
            case InputCombo.ATTACK:
                _attackArrows[_cont].color = _opacityHeld;
                break;
            case InputCombo.SPECIAL:
                _specialArrows[_cont].color = _opacityHeld;
                break;
            case InputCombo.DEFENSE:
                _defenseArrows[_cont].color = _opacityHeld;
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

    public bool EndTurn {
        get { return _endTurn; }
    }
}
