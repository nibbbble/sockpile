using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsButton : MenuButton
{
    public GameObject creditsScreen;
    bool open;

    void Start() {
        creditsScreen.SetActive(false);
        open = false;

        button.onClick.AddListener(OnClick);
    }
    
    public override void OnClick() {
        if (!open) {
            open = true;
        } else {
            open = false;
        }
        creditsScreen.SetActive(open);
    }
}
