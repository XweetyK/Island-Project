using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour {

	public Dialog _dialog;
    DialogManager _manager;

    public void Start()
    {
        _manager = FindObjectOfType<DialogManager>();
    }
    public void Trigger(){
		_manager.StartDialog(_dialog);
	}
}
