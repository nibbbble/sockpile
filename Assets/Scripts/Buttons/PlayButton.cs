using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MenuButton
{
    public override void OnClick() {
        TransitionManager._.FadeIn(scene);
    }
}
