using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGameOver : MonoBehaviour
{
    public Image imageEndGameLogo;

    public GameObject trophies;
    public GameObject deathMenu;

    public float victoryTime;
    public float defeatTime;

    public void Victory(Sprite endGameLogo)
    {
        Invoke(nameof(GameOverInterface), 1f);
        LoadImageEndGameLogo(endGameLogo);
        Invoke(nameof(TurnOffVictoryInterface), victoryTime);
    }

    public void Defeat(Sprite endGameLogo)
    {
        Invoke(nameof(GameOverInterface), 2.5f);
        LoadImageEndGameLogo(endGameLogo);
        Invoke(nameof(TurnOffDefeatInterface), defeatTime);
    }

    private void LoadImageEndGameLogo(Sprite endGameLogo)
    {
        imageEndGameLogo.sprite = endGameLogo;
    }

    private void GameOverInterface()
    {
        gameObject.SetActive(true);
    }


    private void TurnOffVictoryInterface()
    {
        trophies.SetActive(true);
    }

    private void TurnOffDefeatInterface()
    {
        deathMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}