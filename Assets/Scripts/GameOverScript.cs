using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public void QuitToMenu()
    {
        PlayerMovement.lives = 3;
        PlayerMovement.score = 0;
        SceneManager.LoadScene("Menu");
    }

    public void TryAgain()
    {
        PlayerMovement.lives = 3;
        PlayerMovement.score = 0;
        SceneManager.LoadScene("Level 1");
    }
}
