using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private TextMeshProUGUI livesText;

    private void Update()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {PlayerMovement.Instance.score}"; 
        }
        if(livesText != null)
        {
            livesText.text = $"X {PlayerMovement.Instance.lives}";
        }
    }
}
