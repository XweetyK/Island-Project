using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {
    //SETTINGS------------------------------------------------------------
    public static void SaveSettings(SettingsData data) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/settings.ftw";

        FileStream stream = new FileStream(path, FileMode.Create);

        SettingsData settings = data;
        formatter.Serialize(stream, settings);
        stream.Close();
    }

    public static SettingsData LoadSettings() {

        string path = Application.persistentDataPath + "/settings.ftw";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SettingsData settings = formatter.Deserialize(stream) as SettingsData;
            stream.Close();
            return settings;
        }
        return null;
    }

    //GAME---------------------------------------------------------------
    public static void SaveGame(SaveData data) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/gamedata.ftw";

        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData game = data;
        formatter.Serialize(stream, game);
        stream.Close();
    }

    public static SaveData LoadGame() {
        string path = Application.persistentDataPath + "/gamedata.ftw";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData game = formatter.Deserialize(stream) as SaveData;
            stream.Close();
            return game;
        }
        return null;
    }
}
