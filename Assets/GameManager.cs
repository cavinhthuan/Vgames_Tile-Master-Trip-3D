using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using static SceneController;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public CubeModel cubeModel;
    public TimerCountDown countDown;
    public SwapController swapController;
    public UiManager UI;
    public LevelManager lvManager;
    public HolderObject holder;

    public GameObject Keeper;
    public GameObject Holder;
    public GameObject BoolObjects;
    public DataHolder dataHolder;
}
