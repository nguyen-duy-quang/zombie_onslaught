using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnclickQuitGame : MonoBehaviour
{
    public Button quitGameButton;
    public GameManager gameManager;

    private void Start()
    {
        quitGameButton.onClick.AddListener(QuitGame);
    }

    private void QuitGame()
    {
        gameManager.QuitGame();

        AudioManager._instance.ButtonClickSound();
    }    
}
