using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButton : MenuButton
{
    public override void OnClick() {
        TransitionManager._.FadeIn(scene);
    }
}
