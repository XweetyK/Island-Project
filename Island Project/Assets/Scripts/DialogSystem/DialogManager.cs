using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

    [SerializeField] private Text _nameTxt;
    [SerializeField] private Text _dialogTxt;
    [SerializeField] private Image _faceBox;
    [SerializeField] private FacesReferences _facesRef;
    [SerializeField] private GameObject interactPromptBox;

    private Animator _anim;
    private Queue<string> _chats;
    private Queue<string> _names;
    private Queue<Sprite> _faces;
    private DialogTrigger[] _activate;
	private DialogTrigger[] _deactivate;
    bool _init = false;

    public static DialogManager Instance { get; private set; }
    void Awake() {
        if (Instance == null) { Instance = this; } else { Debug.Log("Warning: multiple " + this + " in scene!"); }

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
            PromptActive(false);
            _anim.SetBool("openBox", true);
            _names.Clear();
            _chats.Clear();

            for (int i = 0; i < dial.conversations.Length; i++) {
                _names.Enqueue(dial.conversations[i]._name);
                _chats.Enqueue(dial.conversations[i]._chat);
                _faces.Enqueue(_facesRef.GetFace(dial.conversations[i]._character, dial.conversations[i]._face));
            }
            if (dial.activateChat.Length>0) {
                _activate = dial.activateChat;
            }
            if (dial.deactivateChat.Length>0) {
                _deactivate = dial.deactivateChat;
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
        PromptActive(false);
        Invoke("PostDialogEvents", 0.2f);
	}

    void PostDialogEvents() {
        if (_activate != null) {
            foreach (DialogTrigger log in _activate) {
                log.enabled = true;
            }
        }
        if (_deactivate != null) {
            foreach (DialogTrigger log in _deactivate) {
                log.enabled = false;
            }
        }
        _activate = null;
        _deactivate = null;
    }

    public void PromptActive(bool active){
        if (active == true && _init == true)
            return;
        if (interactPromptBox){
            interactPromptBox.SetActive(active);
        }
    }
}
