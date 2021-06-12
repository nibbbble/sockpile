using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public void FadeOut() {
        gameObject.SetActive(false);
    }

    public void FadeIn() {
        TransitionManager._.NextScene();
    }
}
