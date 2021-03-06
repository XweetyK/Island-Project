﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue {
    public string _name;
    public Characters _character;
    public int _face;
    [TextArea(3, 10)]
    public string _chat;
}
[System.Serializable]
public class Dialog {
    public Dialogue[] conversations;
    [Header("Post Dialogue Chat Events")]
    public DialogTrigger[] deactivateChat;
    public DialogTrigger[] activateChat;
    public string[] activateEvents;
    public string[] deactivateByEvent;
    public string mission;
    public bool heal;
    public bool save;
    private DialogTrigger _trigger;
    public DialogTrigger Trigger {
        get { return (_trigger); }
        set { _trigger = value; }
    }
}
