using UnityEngine.UI;

public class DeathMenu : GameMenuBase
{
    public Button home;
    public Button restart;
    public Button quit;

    private void Start()
    {
        home.onClick.AddListener(ClickHome);
        restart.onClick.AddListener(ClickRestart);
        quit.onClick.AddListener(ClickQuit);

        SetMenuBuyItemVisible(true);
    }
}

