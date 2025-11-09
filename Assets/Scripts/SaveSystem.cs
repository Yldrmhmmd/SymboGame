using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void Save(int topTime, bool fullscreen, int musicVolume, int effectsVolume)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/saved.symbo";
        FileStream stream = new FileStream(path, FileMode.Create);

        Save data = new Save(topTime, fullscreen, musicVolume, effectsVolume);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static Save Load()
    {
        string path = Application.persistentDataPath + "/saved.symbo";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Save data = formatter.Deserialize(stream) as Save;
            stream.Close();

            return data;
        }
        else
            return null;
    }
}