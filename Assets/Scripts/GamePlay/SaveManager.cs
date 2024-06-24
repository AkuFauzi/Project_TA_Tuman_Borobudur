using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    private const string _nameFile = "GameData";
    public static LocalColletion Local;
    public static void Initialize()
    {
        Application.quitting += SaveData;
        LoadData();
    }
    private static void LoadData()
    {
        string _filePath = Application.persistentDataPath + $"/{_nameFile}.json";
       

        if (File.Exists(_filePath))
        {
            string _data = File.ReadAllText(_filePath);
            Local = JsonUtility.FromJson<LocalColletion>(_data);
            Debug.Log(_filePath);
        }
        else
        {
            Local = new LocalColletion();
        }
    }

    private static void SaveData()
    {
        string _filePath = Application.persistentDataPath + $"/{_nameFile}.json";
        string _data = JsonUtility.ToJson(Local);
        File.WriteAllText(_filePath, _data);
    }

    public class LocalColletion
    {
        public int currentAmmo = 50;
        public int itemCount = 0;
        public bool[] buku = new bool[8];
     
        public Vector3 playerPosition = new Vector3(4.5f, 8, 226);
        public LocalColletion() { }
    }
}