using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public Text gameOverScoreText;
    public string[] randomMessages;
    
    public void GameOver(int score) {
        gameOverScreen.SetActive(true);

        int localPersonalBest = PlayerPrefs.GetInt("Personal Best", 0);
        string message = "";
        if (score > localPersonalBest) {
            localPersonalBest = score;
            PlayerPrefs.SetInt("Personal Best", score);
            message = "NEW PERSONAL BEST!";
        } else {
            message = randomMessages[Random.Range(0, randomMessages.Length)];
        }

        if (score == 1) {
            gameOverScoreText.text = string.Format(
                "YOU'VE JOINED... {0} PAIR!\n\nPERSONAL BEST: {1}\n\n* {2} *", 
                score, localPersonalBest, message
            );
        } else {
            gameOverScoreText.text = string.Format(
                "YOU'VE JOINED... {0} PAIRS!\n\nPERSONAL BEST: {1}\n\n* {2} *", 
                score, localPersonalBest, message
            );
        }
        
    }
}
