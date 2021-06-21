using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Persistance : MonoBehaviour
{
    public static Persistance Instance;

    public string Name;

    public HighScore HS;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeName(string newName)
    {
        Name = newName;
    }

    [System.Serializable]
    public class HighScore
    {
        public string name;
        public int score;
    }

    public void AddHighScore(int score, string name)
    {
        string path = Application.persistentDataPath + "/highscores.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            HighScore highScore = JsonUtility.FromJson<HighScore>(json);

            if (score > highScore.score)
            {
                highScore.score = score;
                highScore.name = name;

                HS = highScore;

                WriteHighScore(name, score);
            }

        }
    }


    public void WriteHighScore(string name, int score)
    {
        HighScore hs = new HighScore
        {
            name = name,
            score = score,
        };

        string path = Application.persistentDataPath + "/highscores.json";

        string json = JsonUtility.ToJson(hs);

        File.WriteAllText(path, json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/highscores.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            HighScore highScore = JsonUtility.FromJson<HighScore>(json);

            HS = highScore;
        }
        else
        {
            WriteHighScore("", 0);
        }
    }


}
