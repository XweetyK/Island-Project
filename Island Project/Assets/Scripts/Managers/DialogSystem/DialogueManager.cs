using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
    [SerializeField] FacesReferences _facesRef;
    Text _dialogueBox;
    Text _name;
    Image _face;

    void Start() {
        _dialogueBox = GameObject.FindGameObjectWithTag("DialogueBox").GetComponent<Text>();
        _name = GameObject.FindGameObjectWithTag("NameBox").GetComponent<Text>();
        _face = GameObject.FindGameObjectWithTag("FaceBox").GetComponent<Image>();
    }

    void Update() {

    }

    void Trigger() {
        
    }
}
