using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatInput : MonoBehaviour {
	
	enum InputCombo{NONE, ATTACK, SPECIAL, DEFENSE, RUN, ITEM};

	[SerializeField] int _level;
	[SerializeField] private float _limitTime;

	private int[] _attackCombo = new int[10]{ 1, 1, 2, 1, 3, 4, 3, 4, 2, 2 };
	private int[] _specialCombo = new int[10]{ 3, 3, 4, 2, 2, 1, 3, 1, 3, 4 };
	private int[] _defenseCombo = new int[10]{ 4, 4, 1, 1, 4, 4, 3, 3, 2, 2 };
	private int[] _runCombo = new int[3]{ 2, 2, 2 };
	private int[] _itemCombo = new int[3]{ 5, 5, 5 };
	private int[] _actualCombo = new int[10]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

	private int _cont = 0;
	private bool _blocked =false;
	private int _levelLimit;
	InputCombo _iCombo = InputCombo.NONE;
	private bool _miss = false;
	private bool _endTurn = false;
	float _timer;

	void Update () {
		InputCheck ();
		ComboCheck ();
		Timer ();
		if (_timer >= _limitTime) {
			EndTurn ();
		}
	}

	void InputCheck() {

		if (_cont == _levelLimit) {
			_blocked = true;
		}
		if(_blocked==false){
			if (Input.GetButtonDown ("Up")) {
				_actualCombo [_cont] = 1;
			}
			if (Input.GetButtonDown ("Down")) {
				_actualCombo [_cont] = 2;
			}
			if (Input.GetButtonDown ("Left")) {
				_actualCombo [_cont] = 3;
			}
			if (Input.GetButtonDown ("Right")) {
				_actualCombo [_cont] = 4;
			}
		}		

		if (Input.GetButtonDown("Space")) {
			Send ();
		}
	}

	void ComboCheck(){
		switch (_actualCombo[0]) {
		case 1:
			_iCombo = InputCombo.ATTACK;
			break;
		case 3:
			_iCombo = InputCombo.SPECIAL;
			break;
		case 4:
			_iCombo = InputCombo.DEFENSE;
			break;
		case 2:
			_iCombo = InputCombo.RUN;
			break;
		case 5:
			_iCombo = InputCombo.ITEM;
			break;
		}

		switch (_iCombo) {
		case InputCombo.ATTACK:
			if (_actualCombo [_cont] == _attackCombo [_cont]) {
				_cont++;
			} else {
				_miss = true;
			}
			break;
		case InputCombo.SPECIAL:
			if (_actualCombo [_cont] == _specialCombo [_cont]) {
				_cont++;
			} else {
				_miss = true;
			}
			break;
		case InputCombo.DEFENSE:
			if (_actualCombo [_cont] == _defenseCombo [_cont]) {
				_cont++;
			} else {
				_miss = true;
			}
			break;
		case InputCombo.RUN:
			if (_actualCombo [_cont] == _runCombo [_cont]) {
				_cont++;
			} else {
				_miss = true;
			}
			break;
		case InputCombo.ITEM:
			if (_actualCombo [_cont] == _itemCombo [_cont]) {
				_cont++;
			} else {
				_miss = true;
			}
			break;
		}
	}

	void Missed(){
		if (_miss==true) {
			_cont = 0;
			for (int i = 0; i < 10; i++) {
				_actualCombo [i] = 0;
			}
			_miss = false;
		}
	}

	void Timer(){
		_timer += 1 * Time.deltaTime;	
	}

	void EndTurn(){
		_endTurn = true;
	}

	void Send(){
		
	}
}
