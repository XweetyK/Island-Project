using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SettingsData
{
    public float music;
    public float sfx;

    public SettingsData (float musicVol, float sfxVol) {
        music = musicVol;
        sfx = sfxVol;
    }
}
