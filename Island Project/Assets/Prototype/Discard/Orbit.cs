using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {
	[SerializeField]private float _speed;
	[SerializeField]private bool _xAxis;
	[SerializeField]private bool _yAxis;
	[SerializeField]private bool _zAxis;
	void Update () {
		if (_zAxis) {
			transform.Rotate (0, 0, _speed * Time.deltaTime);
		}
		if(_yAxis){
			transform.Rotate (0, _speed * Time.deltaTime, 0);
		}
		if(_xAxis){
			transform.Rotate (_speed * Time.deltaTime, 0, 0);
		}
	}
}
