using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "GameDatas", menuName = "GameDatas", order = 0), Serializable]
public class GameDatas : ScriptableObject
{
    [SerializeField] private int _currentLevel;
    [SerializeField] private int _star;

    public void Save()
    {
        lock(this)
        {
            using(FileStream file = File.Open(Application.persistentDataPath + ConstantVariables.DATA_DIR, FileMode.Create))
            {
                var newData = this.ToString();
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(file, newData);
                file.Close();
            }
        }

    }

    public void Load()
    {

        using(FileStream file = File.Open(Application.persistentDataPath + ConstantVariables.DATA_DIR, FileMode.OpenOrCreate))
        {
            lock(file)
            {
                if(file.Length > 0)
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    string newData = (string)bf.Deserialize(file);
                    file.Close();
                    JsonUtility.FromJsonOverwrite(newData, this);
                }
                else
                {
                    file.Close();
                }
            }
        }
    }

    [DoNotSerialize]
    public int CurrentLevel
    {
        get => _currentLevel;
    }
    [DoNotSerialize]
    public int Star
    {
        get => _star;
    }

    public void IncreaseStar(int star)
    {
        this._star += star;
        Save();
    }
    public void DecreaseStar(int star)
    {
        this._star -= star;
        Save();
    }

    public int NextLevel()
    {
        ++_currentLevel;
        Save();
        return _currentLevel;
    }

    public void UpdateCurrentLevel(int lv)
    {
        this._currentLevel = lv;
        Save();
    }

    public override string ToString()
    {
        return JsonUtility.ToJson(this);
    }
}