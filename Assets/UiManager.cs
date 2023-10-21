using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static ModalController;
using static SceneController;

public class UiManager : MonoBehaviour
{
    private static UiManager _instance;
    public static UiManager Instance
    {
        get => _instance;
    }

    private void Start()
    {
        _uiSceneStored = new Dictionary<string, SceneBase>();
        Scenes.ForEach(scene => _uiSceneStored.Add(scene.name, scene));
    }

    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }
        Destroy(this.gameObject);
    }
    private void OnEnable()
    {
        this.RegisterListener(EventID.OnSceneLoaded, (param) => _instance.OnSceneLoaded(param as string));
        this.RegisterListener(EventID.TimeUp, (param) => _instance.OnTimeUp());
        this.RegisterListener(EventID.OnModalOpen, (param) => _instance.OnModalOpen((ModalType)param));
        this.RegisterListener(EventID.OnModalClose, (param) => _instance.OnModalClose());
        this.RegisterListener(EventID.OnSolved, (param) => _instance.OnSolved());
        this.RegisterListener(EventID.RichAllLevel, (param) => _instance.OnRichAllLevel());
        this.RegisterListener(EventID.HolderFullSlot, (param) => _instance.OnHolderFullSlot());
    }

    private void OnModalClose()
    {
        GameManager.Instance.countDown.isActivated = true;
    }

    private void OnHolderFullSlot()
    {
        ModalController.Instance.Turn(ModalType.Lose);
    }

    private void OnRichAllLevel()
    {
        ModalController.Instance.Turn(ModalType.ComingSoon);
    }

    private void OnSolved()
    {
        ModalController.Instance.Turn(ModalType.Win);
    }


    private void OnModalOpen(ModalType modalType)
    {
        GameManager.Instance.countDown.isActivated = false;
    }
    private void OnTimeUp()
    {
        ModalController.Instance.Turn(ModalType.Lose);
    }

    public void OnSceneLoaded(string arg0)
    {
        if(_currentScene != null)
        {
            _currentScene.Close();
            _currentScene = null;
        }
        _uiSceneStored[arg0].gameObject.SetActive(true);
        _currentScene = _uiSceneStored[arg0].LoadScene();
    }

    public List<SceneBase> Scenes;
    public Dictionary<string, SceneBase> _uiSceneStored;
    private SceneBase _currentScene;
}

