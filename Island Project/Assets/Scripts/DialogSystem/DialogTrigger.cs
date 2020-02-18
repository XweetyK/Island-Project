using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DialogTrigger : MonoBehaviour {
    [SerializeField] NavMeshAgent _nma;
    [SerializeField] Transform _camRefPoint;
	public Dialog _dialog;
    DialogManager _manager;

    public void Start() {
        _manager = FindObjectOfType<DialogManager>();
        _dialog.Trigger = this;
    }

    private void Update() {
        if (_dialog.deactivateByEvent != null) {
            foreach (string key in _dialog.deactivateByEvent) {
                if (EventManager.Instance.GetEvent(key)) {
                    this.enabled = false;
                    break;
                }
            }
        } 
    }
    public void Trigger(){
        _manager.StartDialog(_dialog, _nma == null ? null : _nma, _camRefPoint);
	}
}
