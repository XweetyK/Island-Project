using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

	[SerializeField] private Text _nameTxt;
	[SerializeField] private Text _dialogTxt;
    [SerializeField] private Image _faceBox;
    [SerializeField] private FacesReferences _facesRef;

	private Animator _anim;
	private Queue <string> _chats;
	private Queue <string> _names;
	private Queue <Sprite> _faces;
    bool _init = false;

	void Awake(){
		_anim = gameObject.GetComponent<Animator>();
	}
	void Start(){
		_chats = new Queue<string> ();
		_names = new Queue<string> ();
		_faces = new Queue<Sprite> ();
	}
	public void StartDialog(Dialog dial){
        if (!_init) {
            _init = true;
            _anim.SetBool("openBox", true);
            _names.Clear();
            _chats.Clear();

            for (int i = 0; i < dial.conversations.Length; i++) {
                _names.Enqueue(dial.conversations[i]._name);
                _chats.Enqueue(dial.conversations[i]._chat);
                _faces.Enqueue(_facesRef.GetFace(dial.conversations[i]._character, dial.conversations[i]._face));
            }
        }
        DisplayNext ();
	}
	public void DisplayNext(){
		if (_chats.Count == 0) {
			EndDialog ();
			return;
		}
		string chat = _chats.Dequeue ();
		string name = _names.Dequeue ();
		Sprite face = _faces.Dequeue ();
		_dialogTxt.text = chat;
		_nameTxt.text = name;
		_faceBox.sprite = face;
	}
	void EndDialog(){
        _init = false;
		_anim.SetBool ("openBox", false);
	}
}
