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
}
