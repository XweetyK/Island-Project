using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ZoneEvents {
    public GameObject eventObject;
    public string eventTrigger;
    public bool shouldActivate;
}

public class ZoneEventsController : MonoBehaviour {
    public ZoneEvents[] _zoneEvents;

    public static ZoneEventsController Instance { get; private set; }
    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }

    private void Update() {
        CheckEvents();
    }

    private void CheckEvents() {
        foreach (var item in _zoneEvents) {
            if (EventManager.Instance.GetEvent(item.eventTrigger)) {
                item.eventObject.SetActive(item.shouldActivate);
            }
        }
    }
}
