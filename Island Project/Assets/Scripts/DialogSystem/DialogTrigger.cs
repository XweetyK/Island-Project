using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DialogTrigger : MonoBehaviour {
    [SerializeField] NavMeshAgent _nma;
    [SerializeField] Transform _camRefPoint;
	public Dialog _dialog;
    DialogManager _manager;

    public void Start()
    {
        _manager = FindObjectOfType<DialogManager>();
    }
    public void Trigger(){
        _manager.StartDialog(_dialog, _nma == null ? null : _nma, _camRefPoint);
	}
}
