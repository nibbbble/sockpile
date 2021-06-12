using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager _;

    void Awake() {
        _ = this;
    }

    public GameObject fadeOut, fadeIn;
    string sceneName;

    public void FadeIn(string name) {
        sceneName = name;
        fadeIn.SetActive(true);
    }

    public void NextScene() {
        SceneManager.LoadScene(sceneName);
    }
}
