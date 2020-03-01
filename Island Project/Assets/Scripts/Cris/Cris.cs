using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cris : MonoBehaviour
{
    [SerializeField] Transform _target;
    NavMeshAgent _nma;
    Animator _anim;

    private void Start() {
        if (EventManager.Instance.GetEvent("CrisAnnoyed")) {
            Destroy(this.gameObject);
        }
        _nma = gameObject.GetComponent<NavMeshAgent>();
        _anim = gameObject.GetComponent<Animator>();
    }

    private void Update() {
        if (EventManager.Instance.GetEvent("CrisLeaveCity")) {
            _nma.SetDestination(_target.position);
            if (_nma.remainingDistance > 0 && _nma.remainingDistance < 0.005f) {
                EventManager.Instance.UpdateEvent("CrisAnnoyed", true);
                EventManager.Instance.UpdateMission("Investigate whats wrong with the ORDER.");
            }
        }
        if (EventManager.Instance.GetEvent("CrisAnnoyed")) {
            Destroy(this.gameObject);
        }
        if (_nma.velocity.x > 0 || _nma.velocity.z > 0) {
            _anim.SetBool("_walk", true);
        } else {
            _anim.SetBool("_walk", false);
        }
    }
}
