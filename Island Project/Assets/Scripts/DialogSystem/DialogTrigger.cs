using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour {

	public Dialog _dialog;

	public void Trigger(){
		FindObjectOfType<DialogManager> ().StartDialog (_dialog);
	}
}
