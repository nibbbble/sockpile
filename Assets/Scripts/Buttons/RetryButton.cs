using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RetryButton : MenuButton
{
    public override void OnClick() {
        TransitionManager._.FadeIn(scene);
    }
}
