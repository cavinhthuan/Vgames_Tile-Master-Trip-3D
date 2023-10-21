using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;
    public static LevelManager Instance
    {

        get => _instance;
    }

    void Awake()
    {

        if(_instance == null)
        {
            _instance = this;
            return;
        }
        Destroy(this.gameObject);
    }
    public int currentLevel;
    [SerializeField] private List<Level> levels;

    int NextLevelCallCount = 0;
    private void OnEnable()
    {
        this.RegisterListener(EventID.NextLevel, (param) => _instance.NextLevel());
        this.RegisterListener(EventID.OnReplay, (param) => _instance.Replay());
    }

    public void NextLevel()
    {
        StartGame(GameManager.Instance.dataHolder.datas.NextLevel());
    }
    private void Start()
    {
        _instance.LoadLevels();
        _instance.StartGame();
    }

    private void LoadLevels()
    {
        levels = new List<Level>();
        levels.AddRange(GameManager.Instance.dataHolder.configs.Levels);
        currentLevel = GameManager.Instance.dataHolder.datas.CurrentLevel;
    }

    private void StartGame(int currentLevel)
    {
        if(currentLevel < levels.Count)
        {
            GameManager.Instance.holder.Clear();
            var timer = GameManager.Instance.countDown;
            timer.timeLeft = levels[currentLevel].time;
            timer.isActivated = true;
            this.currentLevel = currentLevel;
            var swap = GameManager.Instance.swapController;
            StartCoroutine(swap.Swap(levels[currentLevel], true));
        }
        else
        {
            this.PostEvent(EventID.RichAllLevel, null);
        }
    }
    public void StartGame()
    {
        StartGame(GameManager.Instance.dataHolder.datas.CurrentLevel);
    }

    public void Replay()
    {
        StartGame();
    }
}