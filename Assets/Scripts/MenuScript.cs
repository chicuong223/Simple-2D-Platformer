using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    private void Start()
    {
        if(PlayerMovement.Instance != null)
        {
            PlayerMovement.Instance = null;
        }
    }
    public void StartBtnClick()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitBtnClick()
    {
        Application.Quit();
    }
}
