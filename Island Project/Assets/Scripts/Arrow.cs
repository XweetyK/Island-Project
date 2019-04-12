using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

	enum Facing {UP, DOWN, LEFT, RIGHT};
	[SerializeField] private Facing _facing;

	Animator _animator;
	RectTransform _transform;

	private float _timer; //Para limitar el uso de la flecha
	private float _respawnTimer;

	bool _pressed;
	bool _missed;
	bool _sent;
	bool _deactivated;

	float _posX;
	float _posY;
	[SerializeField] float _limitUp;
	[SerializeField] float _limitDown;
	[SerializeField] float _limitLeft;
	[SerializeField] float _limitRight;

	void Start () {
		_transform = gameObject.GetComponent<RectTransform> ();
		_animator = gameObject.GetComponent<Animator> ();
		_pressed = _missed = _sent = _deactivated = false;
		_timer = 0;
		Respawn ();
	}

	void Update () {
		Timer ();
		Movement ();
		if (_deactivated == false) {
			if (_timer > 1.0f) {
				if (_missed == false) {
					Missed ();
				} else {
					_missed = false;
					Respawn ();
				}
			}
		}
		AnimUpdate ();

		if (_missed == true && _timer > 1.0f) {
			_deactivated = true;
			Respawn ();
		}
	}

	private void Movement(){
		switch (_facing) {
		case Facing.UP:
			if (Input.GetButton ("Up") && _missed == false) {
				if (_pressed == false) {
					_timer = 0;
				}
				_pressed = true;
			} else {
				_pressed = false;
			}
			break;
		case Facing.DOWN:
			if (Input.GetButton ("Down") && _missed == false) {
				if (_pressed == false) {
					_timer = 0;
				}
				_pressed = true;
			} else {
				_pressed = false;
			}
			break;
		case Facing.LEFT:
			if (Input.GetButton ("Left") && _missed == false) {
				if (_pressed == false) {
					_timer = 0;
				}
				_pressed = true;
			} else {
				_pressed = false;
			}
			break;
		case Facing.RIGHT:
			if (Input.GetButton ("Right") && _missed == false) {
				if (_pressed == false) {
					_timer = 0;
				}
				_pressed = true;
			} else {
				_pressed = false;
			}
			break;
		}
		if (_pressed == true && Input.GetButtonDown ("Space") && _missed == false) {
			_sent = true;
		}
	}

	void Timer(){
		_timer += 1 * Time.deltaTime;

		Debug.Log (_timer);
	}

	void Missed(){
		if (_missed == false) {
			_timer = 0;
		}
		_missed = true;
		_pressed = false;
	}

	void AnimUpdate(){
		_animator.SetBool ("Pressed", _pressed);
		_animator.SetBool ("Missed", _missed);
		_animator.SetBool ("Sent", _sent);

	}

	void Respawn(){
		_pressed = _missed = _sent = _deactivated = false;
		_posX = Random.Range (_limitLeft, _limitRight);
		_posY = Random.Range (_limitUp, _limitDown);

		_transform.localPosition = new Vector3 (_posX, _posY, 0);
	}
}
