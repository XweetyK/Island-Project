using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public string[] _key;
    public bool[] _value;
    public float[] _playerPos;
    public float[] _playerRot;
    public int[] _stats;
    public string _sceneName;

    public SaveData(Dictionary<string, bool> events, Transform player, int[] stats, string scene) {
        int cont = 0;
        if (events != null) {
        _key = new string[events.Count];
        _value = new bool[events.Count];
            foreach (KeyValuePair<string, bool> item in events) {
                _key[cont] = item.Key;
                _value[cont] = item.Value;
                cont++;
            }
        }
        _playerPos = new float[3];
        if (player != null) {
            _playerPos[0] = player.position.x;
            _playerPos[1] = player.position.y;
            _playerPos[2] = player.position.z;

            _playerRot = new float[4];
            _playerRot[0] = player.rotation.x;
            _playerRot[1] = player.rotation.y;
            _playerRot[2] = player.rotation.z;
            _playerRot[3] = player.rotation.w;
        }
        if (stats != null) {
            _stats = stats;
        }

        _sceneName = scene;
    }

}
