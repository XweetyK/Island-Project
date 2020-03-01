using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cris : MonoBehaviour
{
    [SerializeField] Transform _target;
    NavMeshAgent _nma;

    private void Start() {
        if (EventManager.Instance.GetEvent("CrisAnnoyed")) {
            Destroy(this.gameObject);
        }
        _nma = gameObject.GetComponent<NavMeshAgent>();
    }

    private void Update() {
        if (EventManager.Instance.GetEvent("CrisLeaveCity")) {
            _nma.SetDestination(_target.position);
            if (_nma.remainingDistance <= 0.07) {
                EventManager.Instance.UpdateEvent("CrisAnnoyed", true);
                EventManager.Instance.UpdateMission("Investigate whats wrong with the ORDER.");
            }
        }
        if (EventManager.Instance.GetEvent("CrisAnnoyed")) {
            Destroy(this.gameObject);
        }
    }
}
