using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionUpdater : MonoBehaviour
{
    Text _missionText;
    Text _missionTextPause;
    Animator _anim;

    public static MissionUpdater Instance { get; private set; }
    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }

    void Update(){
        if (_missionText != null || _missionTextPause != null) {
            _missionText.text = EventManager.Instance.GetMission;
            _missionTextPause.text = EventManager.Instance.GetMission;
        }
    }

    public void NewMission() {
        _anim.SetTrigger("Mission");
    }
}
