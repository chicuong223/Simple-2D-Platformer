using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    private bool GameIsPaused;
    public GameObject pauseMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        GameIsPaused = false;
        pauseMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI?.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);

        Time.timeScale = 0;
        GameIsPaused = true;
    }

    public void Quit()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        //PlayerMovement.Instance = null;
        SceneManager.LoadScene("Menu");
    }
}
