using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Player health things")]
    private float playerHealth = 200f;
    public float presentHealth;

    public PlayerHealth UIPlayerHealth;

    [Header("GameOver")]
    public HandleGameOver handleGameOver;

    private void Start()
    {
        presentHealth = playerHealth;
    }

    public void playerHitDamage(float takeDamge)
    {
        presentHealth -= takeDamge;
        UIPlayerHealth.ReducePlayerHealth(takeDamge);

        if (presentHealth <= 0)
        {
            PlayerDie();
        }    
    }

    private void PlayerDie()
    {
        Destroy(gameObject, 1.0f);

        GameOver();
    }

    private void GameOver()
    {
        handleGameOver.DefeatGame();
    }
}
