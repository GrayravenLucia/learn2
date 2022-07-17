using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using TMPro;

public class StartManager : MonoBehaviour
{

    public static StartManager instance;
    public string currentName;
    public InputField inputField;
    public TextMeshProUGUI hiScore;
    public string saveName;
    // Start is called before the first frame update
    void Start()
    {
        inputField = GameObject.Find("Name").GetComponent<InputField>();
        hiScore = GameObject.Find("BestText").GetComponent<TextMeshProUGUI>();
        if (File.Exists(Application.persistentDataPath + "/savefile.json"))
        {
            hiScore.text = $"{currentName} :  {MainManager.highScore}";
        }
            

    }

    

    private void Awake()
    {
        instance = this;
        
        DontDestroyOnLoad(instance);

        LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Rename(string name)
    {
        name = inputField.textComponent.text;
        currentName = name;
        
    }

    public void Load()
    {
        SceneManager.LoadScene(1);
    }

    [Serializable]
    public class PlayerData
    {
        public int saveHiScore;
        public string name;
    }

    public void SaveData()
    {
        PlayerData data = new PlayerData();
        data.saveHiScore = MainManager.highScore;
        data.name = currentName;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            MainManager.highScore = data.saveHiScore;
            currentName = data.name;

        }
    }
}
