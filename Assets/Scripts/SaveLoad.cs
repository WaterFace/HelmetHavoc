using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoad : MonoBehaviour {

    public static Progress progress = new Progress();

    public static void SetProgress(int prog)
    {
        if (progress.progress < prog)
        {
            progress.progress = prog;
            Save();
        }
    }

    public static void Save()
    {
        var bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/save.rm");
        bf.Serialize(file, progress);
        file.Close();
    }

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/save.rm"))
        {
            var bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/save.rm", FileMode.Open);
            SaveLoad.progress = (Progress)bf.Deserialize(file);
        }
    }
}

[System.Serializable]
public class Progress
{
    public int progress;

    public Progress()
    {
        progress = 0;
    }
}
