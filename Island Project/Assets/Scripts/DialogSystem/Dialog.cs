using System.Collections;
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
}
