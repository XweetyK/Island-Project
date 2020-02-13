using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    [SerializeField] GameObject _hero;
    [SerializeField] GameObject _cris;
    [SerializeField] Canvas _canvas;
    [SerializeField] Transform _initialTarget;
    [SerializeField] GameObject[] _delete;
    [SerializeField] Text _typewritter;
    [SerializeField] float _charTime;
    [SerializeField] PlayableDirector _director;
    string _currentText;
    string[] _introduction = {
        " Welcome to " + System.Environment.NewLine + "[P A R A D I S E]" + System.Environment.NewLine + "         ",
        " A safe place," + System.Environment.NewLine +" nothing will hurt you here."+ System.Environment.NewLine + "         ",
        " I hope you enjoy your stay." + System.Environment.NewLine +"         ",
        " After all..."+ System.Environment.NewLine + "I'm sure you won't need to go back." + System.Environment.NewLine + "         ",
        " But first, let me ask" + System.Environment.NewLine +" you a question:" + System.Environment.NewLine + "                          ",
        " Do you think that..." };

    private void Start()
    {
        if (!EventManager.Instance._initIntro) {
            StartCoroutine(Writting());
            _hero.transform.position = _initialTarget.position;
            _hero.transform.rotation = _initialTarget.rotation;
            _hero.SetActive(false);
            _cris.SetActive(false);
            _canvas.enabled = false;
            //GameManager.Instance.PlayerInput = false;

            Invoke("StartGame", 46.2f);
        } else {
            _director.Stop();
            foreach (GameObject dlt in _delete) {
                Destroy(dlt);
            }
            _delete = null;
        }
    }
    private void Update() {
        if (Input.GetButtonDown("Submit")) {
            CancelInvoke();
            StartGame();
        }
    }
    IEnumerator Writting() {
        if (!EventManager.Instance._initIntro) {
            for (int i = 0; i < _introduction.Length; i++) {
                for (int j = 0; j < _introduction[i].Length; j++) {
                    _currentText = _introduction[i].Substring(0, j);
                    if (_typewritter != null) {
                        _typewritter.text = _currentText;
                    }
                    yield return new WaitForSeconds(_charTime);
                }
            }
        }
    }

    void StartGame() {
        if (!EventManager.Instance._initIntro) {
            _director.Stop();
            Destroy(_director.gameObject);
            foreach (GameObject dlt in _delete) {
                Destroy(dlt);
            }
            _delete = null;
            _canvas.enabled = true;
            _hero.SetActive(true);
            _cris.SetActive(true);
            GameManager.Instance.PlayerInput = false;
            EventManager.Instance._initIntro = true;
        }
    }
}
