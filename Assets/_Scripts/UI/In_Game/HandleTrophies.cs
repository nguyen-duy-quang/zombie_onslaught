using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleTrophies : MonoBehaviour
{
    public GameManager gameManager;

    public string sceneName;

    // Mobile
    public Button openSceneButton;

    private void Start()
    {
        if(openSceneButton != null)
        {
            openSceneButton.onClick.AddListener(ReturnToHome);
        }    
    }

    private void Update()
    {
        SwitchToMainGameScene();
    }

    private void SwitchToMainGameScene()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ReturnToHome();
        }
    }    

    public void ReturnToHome()
    {
        gameManager.SwitchGameScene(sceneName);

        AudioManager._instance.ButtonClickSound();
    }    
}
