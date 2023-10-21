using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
public class ModalController : MonoBehaviour
{
    private static ModalController _instance;
    public static ModalController Instance
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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Turn(ModalType modalType)
    {

        switch(modalType)
        {
            case ModalType.Win:
            openModel(modal_Win);
            break;
            case ModalType.Lose:
            openModel(modal_Lose);
            break;
            case ModalType.ComingSoon:
            openModel(modal_ComingSoon);
            break;
        }
        this.PostEvent(EventID.OnModalOpen, modalType);
    }

    public void openModel(GameObject modal)
    {
        Close();
        modal.SetActive(true);
        modal_current = modal;
    }

    public void Close()
    {
        if(modal_current != null)
        {
            modal_current.SetActive(false);
            modal_current = null;
            this.PostEvent(EventID.OnModalClose, null);
            return;
        }
    }
    public void OnRePlay()
    {
        Close();
        this.PostEvent(EventID.OnReplay, null);
    }
    public void OnNextLevel()
    {
        Close();
        this.PostEvent(EventID.NextLevel, null);
    }

    public void OnHome()
    {
        Close();
        this.PostEvent(EventID.OnHome, ConstantVariables.MAIN_SCENE);
    }

    public void openModelMessage(string message)
    {
        openModel(modal_Message);
        txt_message.text = message;
    }

    public enum ModalType
    {
        Win,
        Lose,
        ComingSoon
    }

    public GameObject modal_Win;
    public GameObject modal_Lose;
    public GameObject modal_ComingSoon;
    public GameObject modal_Message;
    public TextMeshProUGUI txt_message;
    [SerializeField] private GameObject modal_current;
}
