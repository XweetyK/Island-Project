using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

	[SerializeField] private Text _nameTxt;
	[SerializeField] private Text _dialogTxt;
	private Animator _talkAnim;
	private Animator _anim;
	private Queue <string> _chats;

	void Awake(){
		_anim = GameObject.FindGameObjectWithTag ("TextBox").GetComponent<Animator>();
	}
	void Start(){
		_chats = new Queue<string> ();
	}
	public void StartDialog(Dialog dial,Animator talkAnim){
		_talkAnim = talkAnim;
		switch (dial._face) {
		case Face.SmileTalk:
			_talkAnim.SetBool ("IsTalking", true);
			break;
		}
		_anim.SetBool ("IsOpen", true);
		_nameTxt.text = dial._name;
		_chats.Clear ();

		foreach (string chat in dial._chat) {
			_chats.Enqueue (chat);
		}

		DisplayNext ();
	}
	public void DisplayNext(){
		if (_chats.Count == 0) {
			EndDialog ();
			return;
		}
		string _chat = _chats.Dequeue ();
		_dialogTxt.text = _chat;
	}
	void EndDialog(){
		_anim.SetBool ("IsOpen", false);
		_talkAnim.SetBool ("IsTalking", false);
	}
}
