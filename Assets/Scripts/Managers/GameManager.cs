using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public SockManager sockManager;
    public Image frame;
    public Slider timer;
    public Text scoreText;
    public bool gameRunning;
    public float time;
    public float maxTime = 5f;
    int score;

    void Start() {
        score = 0;

        timer.maxValue = maxTime;
        timer.value = timer.maxValue;

        // switch this out with countdown l8r
        GameStart();        
    }

    IEnumerator UpdateTimer() {
        for (time = maxTime; time > 0; time -= Time.deltaTime) {
            timer.value = time;
            // Debug.Log(time);
            yield return null;
        }

        time = 0f;
        GameOver();
    }

    void Countdown() {
        
    }

    void GameStart() {
        gameRunning = true;
        StartCoroutine(UpdateTimer());
    }

    void GameOver() {
        gameRunning = false;
    }
    
    public void UpdateFrame(Sprite currentSock) {
        frame.sprite = currentSock;
    }

    public void SockFound() {
        score++;
        sockManager.NewRound(false, score);

        time += 2f;
        if (time > maxTime) {
            time = maxTime;
        }
    }
}
