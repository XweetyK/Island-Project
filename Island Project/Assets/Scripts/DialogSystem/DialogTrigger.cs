using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour {

	public Dialog _dialog;
	private Animator _anim;
	void Start(){
		_anim = gameObject.GetComponent<Animator> ();
	}

	public void Trigger(){
		FindObjectOfType<DialogManager> ().StartDialog (_dialog,_anim);
	}
}
