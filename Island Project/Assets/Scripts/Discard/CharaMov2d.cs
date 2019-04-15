using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaMov2d : MonoBehaviour {

	[SerializeField] GameObject _glitchMask;
	[SerializeField] SpriteRenderer _glitch;
	private Transform _cam;
	private Animator _chara;
	private Vector3 _glitchFrontPos;
	private Vector3 _glitchLeftPos;
	private Color _a1;
	private Color _a0;
	private Vector3 _relPoint;

	void Awake(){
		_cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
		_chara = gameObject.GetComponent<Animator> ();
		_glitchFrontPos= new Vector3 (0.1f, 4.2f, 0.0f);
		_glitchLeftPos= new Vector3 (-0.6f, 4.2f, 0.0f);
		_a1 = Color.white;
		_a0 = new Color (0, 0, 0, 0);
	}

	void Update(){
		transform.rotation = _cam.rotation;
		CheckPos ();
	}

	void CheckPos (){
		_relPoint = transform.parent.InverseTransformPoint(_cam.position);
		if (_relPoint.z > 0 && _relPoint.x < _relPoint.z && _relPoint.x > -(_relPoint.z)) {
			_chara.SetBool ("_front", true);
			_chara.SetBool ("_back", false);
			_chara.SetBool ("_left", false);
			_chara.SetBool ("_right", false);
			_glitch.color = _a1;
			_glitchMask.transform.localPosition = _glitchFrontPos;
		}
		if (_relPoint.z < 0 && _relPoint.x > _relPoint.z && _relPoint.x < -(_relPoint.z)) {
			_chara.SetBool ("_front", false);
			_chara.SetBool ("_back", true);
			_chara.SetBool ("_left", false);
			_chara.SetBool ("_right", false);
			_glitch.color = _a0;
		}
		if (_relPoint.x > 0 && _relPoint.z < _relPoint.x && _relPoint.z > -(_relPoint.x)) {
			_chara.SetBool ("_front", false);
			_chara.SetBool ("_back", false);
			_chara.SetBool ("_left", true);
			_chara.SetBool ("_right", false);
			_glitch.color = _a0;
		}
		if (_relPoint.x < 0 && _relPoint.z > _relPoint.x && _relPoint.z < -(_relPoint.x)) {
			_chara.SetBool ("_front", false);
			_chara.SetBool ("_back", false);
			_chara.SetBool ("_left", false);
			_chara.SetBool ("_right", true);
			_glitch.color = _a1;
			_glitchMask.transform.localPosition = _glitchLeftPos;
		}
		if (_chara.GetBool ("IsTalking") == true) {
			_glitchMask.transform.localPosition = _glitchFrontPos;
		}
	}
}
