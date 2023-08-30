using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static float volume = 1F; 

    private float score;
    // look at my
    // ENCAPSULATION
    public float Score {
        get {
            return score;
        }
        set {
            if (0F <= value && value <= 2918F)
            {
                score = value;
            }
            else
            {
                Debug.LogError("One must imagine Sisyphus happy"); // He'll never reach the top of Olympus
                SadEnding();
            }
        }
    }

    // Class made for saving and loading data
    [System.Serializable]
    public class SaveData
    {
        public float score;

        public SaveData()
        {
            score = GameManager.instance.Score;
        }
        // ABSTRACTION
        public void Save()
        {
            string json = JsonUtility.ToJson(new SaveData());
            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

            Debug.Log($"saved {json} at " + Application.persistentDataPath + "/savefile.json");
        }
        // ABSTRACTION
        public float Load()
        {
            string path = Application.persistentDataPath + "/savefile.json";
            
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                SaveData data = JsonUtility.FromJson<SaveData>(json);
                score = data.score;
                GameManager.instance.Score = score;
                return score;

                Debug.Log($"loaded {json} from " + Application.persistentDataPath + "/savefile.json");
            }
            else
            {
                Debug.LogError("No file at " + Application.persistentDataPath + "/savefile.json ;(");
                return 0F;
            }
        }
    }

    // if two game managers, bad
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            OnGameStart();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // This function is called in the first game manager
    private void OnGameStart()
    {
        new SaveData().Load();
    }

    private void SadEnding()
    {
        //TODO
        Application.Quit();
    }
}
