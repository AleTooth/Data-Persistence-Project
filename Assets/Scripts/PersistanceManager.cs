using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PersistanceManager : MonoBehaviour
{
    public static PersistanceManager Instance;

    public string playerName;
    public int playerScore;
    public string inputName;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadLastScore();
    }

    [System.Serializable]
    class LastScoreData
    {
        public string playerName;
        public int playerScore;
    }

    public void StoreLastScore(string inputName, int score)
    {
        LastScoreData data = new LastScoreData();
        data.playerName = inputName;
        data.playerScore = score;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadLastScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            LastScoreData data = JsonUtility.FromJson<LastScoreData>(json);

            playerName = data.playerName;
            playerScore = data.playerScore;
        }
    }

}
