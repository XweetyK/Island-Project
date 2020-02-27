using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SaveGameUpdater : MonoBehaviour
{
    [SerializeField] Button _loadGame;

    private void Update() {
        if (File.Exists(Application.persistentDataPath + "/gamedata.ftw")) {
            _loadGame.interactable = true;
        } else {
            _loadGame.interactable = false;
        }
    }
}
