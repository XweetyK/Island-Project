using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

	[SerializeField] private Text _nameTxt;
	[SerializeField] private Text _dialogTxt;
	[SerializeField] private Animator _anim;
	private Queue <string> _chats;

	void Start(){
		_chats = new Queue<string> ();
	}
	public void StartDialog(Dialog dial){
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
	}
}
