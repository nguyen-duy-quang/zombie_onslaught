using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Animations.Rigging;

public class GameManager : MonoBehaviour
{
    public Rig rig;

    // Mobile
    public bool mobileInputs;

    private void Start()
    {
        ResetWeightRig();
    }

    public void ResetWeightRig()
    {
        if(rig != null)
        {
            rig.weight = 0.0f;
        }
    }

    // Khóa con trỏ chuột
    public void LockCursor()
    {
        if(mobileInputs == false)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }    
    }

    // Mở con trỏ chuột
    public void UnlockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void SwitchGameScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
