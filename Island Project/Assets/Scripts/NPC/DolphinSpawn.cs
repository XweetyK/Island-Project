using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DolphinSpawn : MonoBehaviour {

	[SerializeField]GameObject _prefab;

	[SerializeField]float _swimZoneMin;
	[SerializeField]float _swimZoneMax;

	GameObject[] _dolphins;
	float _randZ;
	float _randX;
	float _randScale;

	void Awake () {
		_dolphins = new GameObject[30];
		for (int i = 0; i < 10; i++) {
			_randZ= Random.Range (_swimZoneMin, _swimZoneMax);
			_randX= Random.Range (500.0f, 750.0f);
			_randScale= Random.Range (0.3f, 1.0f);
			_dolphins [i] = Instantiate (_prefab);
			_dolphins [i].transform.position = new Vector3 (_randX, 3.0f, _randZ);
			_dolphins [i].transform.localScale = new Vector3 (_randScale, _randScale, _randScale);
		}
	}
}
