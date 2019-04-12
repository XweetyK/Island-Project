using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	[SerializeField] Transform _target;
	[SerializeField] float _smooth;
	[SerializeField] Vector3 _offset;


	void LateUpdate () {
		Vector3 lastPos = _target.position + _offset;
		Vector3 smoothPos = Vector3.Lerp (transform.position, lastPos, _smooth*Time.deltaTime);
		transform.position = smoothPos;

	}
}
