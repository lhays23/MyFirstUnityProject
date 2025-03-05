using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject menuUI;
    private bool isPaused = false;

    void Start()
    {
        if (menuUI == null)
        {
            menuUI = GameObject.Find("MainMenuCanvas"); // ✅ Auto-find the menu if not assigned
        }
        menuUI.SetActive(false); // ✅ Hide menu at start
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        menuUI.SetActive(true); // ✅ Show menu
        Time.timeScale = 0f; // ✅ Pause game
        isPaused = true;
    }

    public void ResumeGame()
    {
        menuUI.SetActive(false); // ✅ Hide menu
        Time.timeScale = 1f; // ✅ Resume game
        isPaused = false;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit(); // ✅ Quit game (only works in built version)
    }
}
