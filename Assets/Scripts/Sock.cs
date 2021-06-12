// fix sorting problem soon

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sock : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public bool chosen;

    GameManager gameManager;
    Sprite sprite;
    bool dragging = false;
    float distance;

    void Start() {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        sprite = spriteRenderer.sprite;
    }

    void OnMouseDown() {
        if (gameManager.gameRunning) {
            gameObject.transform.SetAsLastSibling();
            
            distance = Vector3.Distance(transform.position, Camera.main.transform.position);
            dragging = true;

            if (chosen) {
                gameManager.SockFound();
            }
        }
    }

    void OnMouseUp() {
        dragging = false;
    }

    void Update() {
        if (dragging) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = rayPoint;
        }
    }

    public void Destroy() {
        Destroy(gameObject);
    }
}
