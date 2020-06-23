using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {
    [SerializeField] string _sceneName;
    private bool _init=false;
    void Awake()
    {
        _init = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            if (!_init) {
                _init = true;
                SceneLoader.Instance.LoadScene(_sceneName);
            }
        }
    }
    public void Teleport() {
        if (!_init) {
            _init = true;
            SceneLoader.Instance.LoadScene(_sceneName);
        }
    }
    public void Quit() {
        Application.Quit();
    }
}
