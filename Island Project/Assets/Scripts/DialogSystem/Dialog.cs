using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Face{SmileTalk,NeutralTalk};

[System.Serializable]
public class Dialog {
	
	public string _name;
	public Face _face;
	[TextArea(3,10)]
	public string[] _chat;
}
