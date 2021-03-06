﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ZoneEvents {
    public GameObject eventObject;
    public string mission;
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
    private void Start() {
        if (EventManager.Instance.GetEvent("loadedGame")) {
            GameObject.FindObjectOfType<PlayerMov>().transform.position = new Vector3(SaveGameManager.Instance.gameData._playerPos[0], SaveGameManager.Instance.gameData._playerPos[1], SaveGameManager.Instance.gameData._playerPos[2]);
            CharacterStats.Instance.SetStats(SaveGameManager.Instance.gameData._stats);
            EventManager.Instance.UpdateEvent("loadedGame", false);
        }
    }

    private void Update() {
        CheckEvents();
    }

    private void CheckEvents() {
        foreach (var item in _zoneEvents) {
            if (EventManager.Instance.GetEvent(item.eventTrigger)) {
                if (item.eventObject != null) {
                    item.eventObject.SetActive(item.shouldActivate);
                }
                if (!string.IsNullOrWhiteSpace(item.mission)) {
                    EventManager.Instance.UpdateMission(item.mission);
                }
            }
        }
    }
}
