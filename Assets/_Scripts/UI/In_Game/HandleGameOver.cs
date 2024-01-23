using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleGameOver : MonoBehaviour
{
    public UIGameOver UIgameOver;
    public ScriptableObjectLogoVictoryDefeat victoryGame;
    public ScriptableObjectLogoVictoryDefeat defeatGame;

    public CinemachineBrain cinemachineBrain;

    public GameObject player;
    private CharacterLocomotion characterLocomotion;
    private CharacterAiming characterAiming;
    private ActiveWeapon activeWeapon;
    private ReloadWeapon reloadWeapon;
    private Animator animator;

    public GameObject[] zombiesManager;

    public TimeSliderController timeSliderController;

    public GameManager gameManager;

    public HandleCurrency handleCurrency;

    public GameMenuBase gameMenuBase;

    private void Start()
    {
        if(player != null)
        {
            characterLocomotion = player.GetComponent<CharacterLocomotion>();
            characterAiming = player.GetComponent<CharacterAiming>();
            activeWeapon = player.GetComponent <ActiveWeapon>();
            reloadWeapon = player.GetComponent<ReloadWeapon>();
            animator = player.GetComponent<Animator>();
        }    
    }

    public void VictoryGame()
    {
        GameOver();
        UIgameOver.Victory(victoryGame.endGameLogo);

        foreach (GameObject zombieManager in zombiesManager)
        {
            zombieManager.SetActive(false);
        }    
        
        HandleCurrency();

        AudioManager._instance.VictoryGameSound();
    }

    public void DefeatGame()
    {
        GameOver();
        timeSliderController.StopTime();

        UIgameOver.Defeat(defeatGame.endGameLogo);

        AudioManager._instance.DefeatGameSound();

        if (player != null)
        {
            player.SetActive(false);
        }
    }

    private void GameOver()
    {
        cinemachineBrain.enabled = false;

        if (characterLocomotion != null && characterAiming != null && activeWeapon != null && reloadWeapon != null && animator != null)
        {
            characterLocomotion.enabled = false;
            characterAiming.enabled = false;
            activeWeapon.enabled = false;
            reloadWeapon.enabled = false;
            animator.enabled = false;
        }    

        timeSliderController.enabled = false;

        gameManager.UnlockCursor();

        // Không cho phép pause game
        gameMenuBase.SetMenuBuyItemVisible(true);
    }

    private void HandleCurrency()
    {
        handleCurrency.HandleMoney();
        handleCurrency.HandleGem();
    }    
}
