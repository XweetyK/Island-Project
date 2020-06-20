using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {
    private Dictionary<string, bool> events = new Dictionary<string, bool>();
    private string _currentMission="";

    public static EventManager Instance { get; private set; }
    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(this.gameObject);
        }
        events.Add("loadedGame", false);
        events.Add("initIntro", false);
        events.Add("cityIntro", false);
        events.Add("crisDialog1", false);
        events.Add("crisDialog2", false);
        events.Add("orderInit", false);
        events.Add("orderInitEnd", false);
        events.Add("DJCoffeeChat", false);
        events.Add("FuckedItUp", false);
        events.Add("CrisLeaveCity", false);
        events.Add("MuseumStart", false);
        events.Add("Ephiphany", false);
        events.Add("CrisAnnoyed", false);
        events.Add("NewMission", false);
        events.Add("MuseumDoor", false);
        events.Add("CrisSearching", false);
        events.Add("ToMuseum", false);
        events.Add("InTheMuseum", false);
        events.Add("FoundItem", false);
    }

    public void UpdateEvent(string key, bool value) {
        events[key] = value;
    }
    public bool GetEvent(string key) {
        return (events[key]);
    }
    public void UpdateMission(string mission) {
        _currentMission = mission;
        if (PlayerCanvasUpdater.Instance != null) {
            PlayerCanvasUpdater.Instance.NewMission();
        }
    }
    public string GetMission {
        get { return _currentMission; }
    }
    public void ResetEvents() {
        Dictionary<string, bool> temp = new Dictionary<string, bool>();
        foreach (string key in events.Keys) {
            temp.Add(key, false);
        }
        events = temp;
    }
    public Dictionary<string, bool> Events{
        get { return events; }
    }
}
