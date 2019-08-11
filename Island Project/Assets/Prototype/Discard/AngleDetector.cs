using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleDetector : MonoBehaviour {
	[SerializeField]private Transform _cam;
	private Vector2 _camChara;
	private Vector2 _charaFront;
	private float _escalar;
	private float _abs1;
	private float _abs2;
	private float _angle;
	private Vector3 _relPoint;

	void Update()
	{
		CheckAngle ();
		CheckDir();

	}
	void CheckAngle(){
		_charaFront = new Vector2 (transform.forward.x, transform.forward.z);
		_camChara = new Vector2 (transform.position.x - _cam.position.x, transform.position.z - _cam.position.z);
		_escalar= (_charaFront.x*_camChara.x)+(_charaFront.y*_camChara.y);
		_abs1 = Mathf.Sqrt (Mathf.Pow (_charaFront.x, 2f) + Mathf.Pow (_charaFront.y, 2f));
		_abs2 = Mathf.Sqrt (Mathf.Pow (_camChara.x, 2f) + Mathf.Pow (_camChara.y, 2f));
		_angle =  (Mathf.Acos (_escalar / (_abs1 * _abs2)));
		Debug.Log (_angle);
	}
	void CheckDir(){
		_relPoint = transform.InverseTransformPoint(_cam.position);
		if (_relPoint.x < 0.0)
			Debug.Log ("left");
		else if (_relPoint.x > 0.0)
			Debug.Log ("right");
	}

}
