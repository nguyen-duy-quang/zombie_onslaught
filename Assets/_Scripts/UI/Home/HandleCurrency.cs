using UnityEngine;

public class HandleCurrency : MonoBehaviour
{
    public ScriptableObjectUITrophies scriptableObjectUITrophies1;
    public ScriptableObjectUITrophies scriptableObjectUITrophies2;

    public SaveCurrency saveCurrency;

    public void HandleMoney()
    {
        saveCurrency.money.currency += scriptableObjectUITrophies1.quantity;

        saveCurrency.SaveData();
    }

    public void HandleGem()
    {
        saveCurrency.gem.currency += scriptableObjectUITrophies2.quantity;

        saveCurrency.SaveData();
    }
}
