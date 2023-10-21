using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static SceneController;

public class SceneController : MonoBehaviour
{
    private static SceneController _instance;
    public DataHolder dataHolder;
    public string currentScene;
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

    private void Start()
    {
        this.RegisterListener(EventID.OnHome, param => LoadScene(param as string));
        LoadScene(dataHolder.configs.StartScene);
    }

    public static SceneController Instance
    {
        get => _instance;
    }

    public void LoadScene(string sceneName)
    {
        var currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        if(currentScene != sceneName)
        {
            this.currentScene = sceneName;
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
        this.PostEvent(EventID.OnSceneLoaded, sceneName);
    }
}
