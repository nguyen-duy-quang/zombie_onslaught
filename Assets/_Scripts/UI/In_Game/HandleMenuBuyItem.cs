using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleMenuBuyItem : MonoBehaviour
{
    public GameManager gameManager;

    public Button buttonClose;

    public SaveCurrency saveCurrency;

    public CinemachineBrain cinemachineBrain;
    public UICurrency uICurrency;

    public GameMenuBase gameMenuBase;

    /*private void Start()
    {
        cinemachineBrain.enabled = false;
        buttonClose.onClick.AddListener(CloseMenuBuyItem);
        gameManager.PauseGame();
    }*/

    private void Start()
    {
        cinemachineBrain.enabled = false;
        buttonClose.onClick.AddListener(CloseMenuBuyItem);

        gameManager.PauseGame();
        // Đặt trạng thái menu mua item là đang hiển thị khi Start
        gameMenuBase.SetMenuBuyItemVisible(true);
    }


    private void CloseMenuBuyItem()
    {
        gameObject.SetActive(false);
        gameManager.LockCursor();
        gameManager.ResumeGame();
        if(gameManager.mobileInputs == false)
        {
            cinemachineBrain.enabled = true;
        }    

        AudioManager._instance.ButtonClickSound();
        gameMenuBase.SetMenuBuyItemVisible(false);
    }

    public void HandleBuyItem(ref int numberOfItem, int maxNumberOfItem, int price, ref int currency)
    {
        if(numberOfItem < maxNumberOfItem && price <= currency)
        {
            numberOfItem += 1;
            currency -= price;

            uICurrency.DisplayCurrencyAccount();
            AudioManager._instance.ButtonClickSound();
            saveCurrency.SaveData();
        }
    }    
}
