using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGameManager : MonoBehaviour {
    public SaveData gameData;
    public static SaveGameManager Instance { get; private set; }
    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(this.gameObject);
        }
        if (SaveSystem.LoadGame() != null) {
            gameData = new SaveData(null,null,null, "scene");
        } else { gameData = SaveSystem.LoadGame(); }
    }
}
