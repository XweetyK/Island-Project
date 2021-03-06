using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchFace : MonoBehaviour {

	[SerializeField] private float _normal;
	[SerializeField] private float _altered;
	[SerializeField] private bool _isAlt;
	private int _rand;
	private float _timer;
	private SpriteRenderer _sprite;

	void Start(){
		_sprite = GetComponent<SpriteRenderer> ();
		_timer = 0;
		_rand = 0;
	}
	void Update () {
		_rand = Random.Range (1, 3);

		switch (_isAlt) {
		case true:
			_timer += Time.deltaTime * _altered;
			if (_timer > 1.0f) {
				_timer = 0;
				Flip ();
			}
			break;
		case false:
			_timer += Time.deltaTime * _normal;
			if (_timer > 1.0f) {
				_timer = 0;
				Flip ();
			}
			break;
		}
	}
	private void Flip(){
		switch (_rand) {
		case 1:
			switch (_sprite.flipX) {
			case true:
				_sprite.flipX = false;
				break;
			case false:
				_sprite.flipX = true;
				break;
			}
			break;
		case 2:
			switch (_sprite.flipY) {
			case true:
				_sprite.flipY = false;
				break;
			case false:
				_sprite.flipY = true;
				break;
			}
			break;
		}
	}
	public bool Altered{
		get{return _isAlt;}
		set{_isAlt = value;}
	}
}
