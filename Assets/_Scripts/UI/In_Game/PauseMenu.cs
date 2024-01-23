using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : GameMenuBase
{
    public Button resume;
    public Button home;
    public Button restart;
    public Button quit;

    private void Start()
    {
        resume.onClick.AddListener(ClickResume);
        home.onClick.AddListener(ClickHome);
        restart.onClick.AddListener(ClickRestart);
        quit.onClick.AddListener(ClickQuit);
    }
}
