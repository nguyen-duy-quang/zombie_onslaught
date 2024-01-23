using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuBase : MonoBehaviour, IGameMenu
{
    [SerializeField] protected GameManager gameManager;
    [SerializeField] protected string homeScene;
    [SerializeField] protected string currentScene;

    protected int keyPressCount = 0;
    [SerializeField] protected GameObject pauseMenu;

    // Thêm biến để kiểm tra trạng thái của menu
    private bool isMenuVisible = false;

    // Trong GameMenuBase
    private bool isMenuBuyItemVisible = false;
    /*
        private void Update()
        {
            // Kiểm tra nếu phím P được nhấn
            if (Input.GetKeyDown(KeyCode.P))
            {
                keyPressCount++;

                bool isPause = keyPressCount % 2 == 1;

                // Kiểm tra xem trạng thái menu đã thay đổi hay chưa
                if (isPause != isMenuVisible)
                {
                    TogglePauseMenu(isPause);
                }
            }
        }*/

    private void Update()
    {
        // Kiểm tra nếu phím P được nhấn
        if (Input.GetKeyDown(KeyCode.P))
        {
            keyPressCount++;

            bool isPause = keyPressCount % 2 == 1;

            // Kiểm tra xem trạng thái menu đã thay đổi hay chưa và menu mua item không hiển thị
            if (isPause != isMenuVisible && !isMenuBuyItemVisible)
            {
                TogglePauseMenu(isPause);
            }
        }
    }

    // Thêm một phương thức để cập nhật trạng thái của menu mua item
    public void SetMenuBuyItemVisible(bool isVisible)
    {
        isMenuBuyItemVisible = isVisible;
    }

    private void TogglePauseMenu(bool isPause)
    {
        if (isPause)
        {
            Pause();
        }
        else
        {
            gameManager.ResumeGame();
            gameManager.LockCursor();
            pauseMenu.SetActive(false);
        }

        // Cập nhật trạng thái của biến isMenuVisible
        isMenuVisible = pauseMenu.activeSelf; // Sử dụng trạng thái thực tế của pauseMenu
    }

    public void Pause()
    {
        gameManager.PauseGame();
        gameManager.UnlockCursor();
        pauseMenu.SetActive(true);
    }    

    public void ClickResume()
    {
        // Khi click vào nút Resume, ẩn menu
        TogglePauseMenu(false);
        ClickingSound();
    }

    public void ClickHome()
    {
        gameManager.SwitchGameScene(homeScene);
        gameManager.ResumeGame();
        ClickingSound();
    }

    public void ClickRestart()
    {
        gameManager.SwitchGameScene(currentScene);
        gameManager.ResumeGame();
        ClickingSound();
    }

    public void ClickQuit()
    {
        gameManager.QuitGame();
        ClickingSound();
    }

    private void ClickingSound()
    {
        AudioManager._instance.ButtonClickSound();
    }    
}

/*
 * tôi thấy rằng khi tôi nhấn nhấn vào phím P lần 1 (isPause == true) thì sẽ thực thi pauseMenu.SetActive(true); sau đó tôi nhấn vào button (được gán sự kiện ClickResume()) thì sẽ thực thi  pauseMenu.SetActive(false); nhưng sau đó nếu tôi nhấn phím P thì lại thực thi  pauseMenu.SetActive(false); và tôi phải nhấn phím P 1 lần nữa thì mới lại thực thi pauseMenu.SetActive(true); đúng chứ?
 * Vậy nên tôi muốn nếu tôi nhấn phím P lần 1 (isPause == true) thì sẽ thực thi pauseMenu.SetActive(true); sau đó tôi nhấn vào button (được gán sự kiện ClickResume()) thì sẽ thực thi  pauseMenu.SetActive(false); nhưng sau đó nếu tôi nhấn phím P thì lại thực thi  pauseMenu.SetActive(true);. vậy phải làm sao?
 */