using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour {

	private NavMeshAgent _nma;
	[SerializeField]Transform _target;

	void Awake(){
		_nma = GetComponent<NavMeshAgent> ();
	}
	void Update(){
		_nma.destination = _target.position;
	}
}
