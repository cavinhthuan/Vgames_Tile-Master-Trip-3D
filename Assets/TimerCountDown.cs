using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerCountDown : MonoBehaviour
{
    public float timeLeft = ConstantVariables.TIME_LEFT;
    public bool isTimeUp = ConstantVariables.IS_TIMEUP;
    public bool isActivated = ConstantVariables.IS_ACTIVATE;
    public Action begin;

    private void Start()
    {
        if(UiManager.Instance._uiSceneStored.TryGetValue(ConstantVariables.GAME_SCENE, out SceneBase value))
        {
            if(value is GameScene gameScene)
            {
                timerText = gameScene.txt_time;
            }
            else
            {
                Debug.LogError("GameScene not found");
            }
        }
        else
        {
            Debug.LogError("GameScene not found");
        }
    }
    private void OnEnable()
    {
        begin += Invoke;
    }
    private void OnDisable()
    {
        begin -= Invoke;
    }

    public void Invoke()
    {
        if(isActivated)
        {
            timeLeft -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(timeLeft / 60);
            int seconds = Mathf.FloorToInt(timeLeft % 60);
            SetTime(minutes, seconds);
        }
    }
    void Update()
    {
        begin();
    }

    public void SetTime(int minute, int second)
    {
        timerText.text = string.Format(ConstantVariables.TIME_FORMAT, minute, second);

        if(timeLeft <= 0)
        {
            timerText.text = ConstantVariables.TIME_UP;
            this.PostEvent(EventID.TimeUp);
            isActivated = false;
        }
    }
    // Start is called before the first frame update
    private Timer timer;
    public TextMeshProUGUI timerText;
}
