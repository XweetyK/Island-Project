using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DolphinMov : MonoBehaviour {

	[SerializeField]float _swimVel;
	[SerializeField]float _maxDistance;
	[SerializeField]float _freq;
	[SerializeField]float _magnitude;
	[SerializeField]float _swimZoneMin;
	[SerializeField]float _swimZoneMax;

	float _randZ;
	float _randTimer;
	float _timer;
	bool _moving;

	void Start () {
		_timer = 0;
		_randTimer = Random.Range (0.5f, 5.0f);
		_moving = false;
		Invoke ("StartMovement", _randTimer);
	}

	void Update () {
		if (_moving) {
			_timer += Time.deltaTime;
		}
		transform.Translate (_swimVel * Time.deltaTime, 0, 0);
		transform.position = transform.position + (transform.up * Mathf.Sin (_timer * _freq) * _magnitude);
		if (transform.position.x <= _maxDistance) {
			_randZ = Random.Range (_swimZoneMin, _swimZoneMax);
			transform.position = new Vector3 (500, 0, _randZ);
		}
	}
	void StartMovement(){
		_moving = true;
	}
}
