using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class SceneLoader : MonoBehaviour {
    [SerializeField] Animator _anim;
    [SerializeField] Image _bar;
    [SerializeField] Text _portal;
    private AsyncOperation _operation;
    private string _sceneName;

    public static SceneLoader Instance { get; private set; }
    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    public void LoadScene(string sceneName) {
        _bar.fillAmount = 0;
        switch (sceneName) {
            case "FirstCity":
                _portal.text = "Now entering [C I T Y]";
                _sceneName = "City";
                break;
            case "FirstBeach2":
                _portal.text = "Now entering [B E A C H]";
                _sceneName = "Beach2";
                break;
            case "City":
                _portal.text = "Now returning to [P A R A D I S E]";
                _sceneName = sceneName;
                break;
            case "Beach2":
                _portal.text = "Now returning to [P A R A D I S E]";
                _sceneName = sceneName;
                break;
            case "NewGame":
                _portal.text = "Now entering [P A R A D I S E]";
                EventManager.Instance.ResetEvents();
                _sceneName = "Beach2";
                break;
            case "MainMenu":
                _portal.text = "You're now leaving [P A R A D I S E]";
                _sceneName = "MainMenu";
                break;
            default:
                _portal.text = "Now entering [? ? ?]";
                break;
        }
        Invoke("InitAnimation", 1.0f);
        _anim.SetBool("Loading", true);

    }

    private void InitAnimation() {
        StartCoroutine(BeginLoad(_sceneName));
        _sceneName = null;
    }
    private IEnumerator BeginLoad(string sceneName) {

        Scene scene = SceneManager.GetSceneByName(sceneName);
        if ((scene != null) && (!scene.isLoaded)) {
            _operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

            while (!_operation.isDone) {
                UpdateProgressUI(_operation.progress);
                yield return null;
            }
            UpdateProgressUI(_operation.progress);
        }
    }

    private void UpdateProgressUI(float progress) {
        _bar.fillAmount = progress;
    }
}
