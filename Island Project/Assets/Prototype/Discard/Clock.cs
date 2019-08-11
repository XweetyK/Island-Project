using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour {

	[SerializeField]float _spe;
	[SerializeField]GameObject _sun;
	private Text _text;

	private int[] _clock;
	private float _secF;
	private int _min;
	private int _hour;
	private float _angle;

	void Awake(){
		_clock = new int[]{ 0, 0 };	
		_text = gameObject.GetComponent<Text> ();

	}
	void Update ()
	{
		_secF += _spe*Time.deltaTime;

		if (_secF > 1.0f) {
			_secF = 0;
			_min += 1;
		}
		if (_min > 59) {
			_min = 0;
			_hour += 1;
			Sun ();
		}
		if (_hour > 23) {
			_min = 0;
			_hour = 0;
		}

		_clock [0] = _hour;
		_clock [1] = _min;

		DisplayTime ();
	}

	private void Sun(){
		_angle = _sun.transform.rotation.x+15.0f;
		_sun.transform.Rotate (_angle, 0, 0);
	}
	private void DisplayTime(){
		if (_hour < 10) {
			if (_min < 10) {
				_text.text = "0" + _clock[0].ToString () + ":0" + _clock[1].ToString ();
			} else {
				_text.text = "0" + _clock[0].ToString () + ":" + _clock[1].ToString ();
			}
		} 
		else if (_min < 10) {
			_text.text = _clock[0].ToString () + ":0" + _clock[1].ToString ();
		}
		else {
			_text.text = _clock[0].ToString () + ":" + _clock[1].ToString ();
		}
	}
}
