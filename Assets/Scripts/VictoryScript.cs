using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictoryScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI livesText;

    private void Start()
    {
        scoreText.text = $"Total score: {PlayerMovement.score}";
        livesText.text = $"Lives: {PlayerMovement.lives}";
    }
}
