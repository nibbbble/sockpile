using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public SockManager sockManager;
    public GameOverManager gameOverManager;
    public Image frame;
    public Slider timer;
    public Text scoreText;
    public Text countdownText;
    public GameObject countdownBackground;
    public bool gameRunning;
    public bool debugMode;
    public float time;
    public float maxTime = 5f;
    int score;

    AudioManager _audio;
    public AudioData sfxCountdown, sfxRight, musicInGame;

    void Start() {
        _audio = AudioManager.i;
        
        score = 0;
        gameRunning = false;
        
        #if UNITY_EDITOR
            if (debugMode)
                maxTime = Mathf.Infinity;
        #endif
        timer.maxValue = maxTime;
        timer.value = timer.maxValue;

        // switch this out with countdown l8r
        // GameStart();
        StartCoroutine(Countdown());    
    }

    IEnumerator UpdateTimer() {
        for (time = maxTime; time > 0; time -= Time.deltaTime) {
            timer.value = time;
            yield return null;
        }

        time = 0f;
        GameOver();
    }

    IEnumerator Countdown() {
        yield return new WaitForSeconds(1f);
        
        _audio.Play(sfxCountdown);
        
        for (float t = 3; t > 0; t -= Time.deltaTime) {
            countdownText.text = string.Format("{0}...", Mathf.Ceil(t));
            yield return null;
        }

        GameStart();
    }

    void GameStart() {
        gameRunning = true;

        countdownBackground.SetActive(false);
        countdownText.text = "GO!";
        
        Animator countdownTextAnimator = countdownText.gameObject.GetComponent<Animator>();
        countdownTextAnimator.SetBool("CountdownFinished", true);
        
        StartCoroutine(UpdateTimer());

        _audio.Play(musicInGame);
    }

    void GameOver() {
        gameRunning = false;
        gameOverManager.GameOver(score);
    }
    
    public void UpdateFrame(Sprite currentSock) {
        frame.sprite = currentSock;
    }

    public void SockFound() {
        _audio.Play(sfxRight);
        
        score++;
        sockManager.NewRound(false, score);

        if (score % 10 == 0 && maxTime > 3f) {
            maxTime -= 1f;
            // Debug.Log(maxTime);
        }

        time += 2f;
        if (time > maxTime) {
            time = maxTime;
        }
    }

    void Update() {
        #if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.F3))
                PlayerPrefs.DeleteAll();
            
            if (Input.GetKeyDown(KeyCode.F2))
                time = 0;

            if (Input.GetKeyDown(KeyCode.F1))
                SockFound();
        #endif
    }
}
