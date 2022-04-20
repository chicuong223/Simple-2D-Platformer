using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public void StartBtnClick()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void QuitBtnClick()
    {
        Application.Quit();
    }
}
