using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InCutsceneDialog : MonoBehaviour
{
    [SerializeField] Text _text;
    [SerializeField] Text _shadow;
    [SerializeField] float[] _timeStamps;
    [SerializeField] string[] _dialogues;
    int _count = 0;
    float _time = 0;

    void Start() {
        DialogueCutscene();
    }

    private void Update() {
        _time += Time.deltaTime;
        if (_time >= _timeStamps[_count]) {
            if (_count < _timeStamps.Length) {
                DialogueCutscene();
            }
        }
    }

    void DialogueCutscene() {
        _text.text = _dialogues[_count];
        _shadow.text = _dialogues[_count];
        _count++;
    }
}
