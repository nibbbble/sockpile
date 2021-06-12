// fix sorting problem soon

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sock : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public bool chosen;

    GameManager gameManager;
    SockManager sockManager;
    Sprite sprite;
    bool dragging = false;
    // float distance;
    Vector3 distance;

    void Start() {
        GameObject gameController = GameObject.FindWithTag("GameController");
        gameManager = gameController.GetComponentInChildren<GameManager>();
        sockManager = gameController.GetComponentInChildren<SockManager>();

        sprite = spriteRenderer.sprite;
        CreateDropShadow();
    }

    // ;-;
    void CreateDropShadow() {
        GameObject dropShadow = Instantiate(gameObject);
        Destroy(dropShadow.GetComponent<Sock>());
        Destroy(dropShadow.GetComponent<Collider2D>());
        dropShadow.name = "Shadow";

        Transform _t = dropShadow.transform;
        _t.SetParent(gameObject.transform);
        _t.position += new Vector3(.05f, -.05f, 0);

        SpriteRenderer _s = dropShadow.GetComponent<SpriteRenderer>();
        _s.sortingOrder--;
        _s.sprite = sockManager.GetSockShadow();
        _s.color = new Color(0, 0, 0, 0.25f); // semi black
    }

    void OnMouseDown() {
        if (gameManager.gameRunning) {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit) {
                if (hit.transform == transform) {
                    gameObject.transform.SetAsLastSibling();

                    Vector3 mousePos = Input.mousePosition;
                    Vector3 distanceToScreen = Camera.main.WorldToScreenPoint(transform.position);
                    distance = Camera.main.ScreenToWorldPoint(
                        new Vector3(
                            mousePos.x, 
                            mousePos.y,
                            distanceToScreen.z
                        )
                    ) - transform.position;
                    dragging = true;

                    if (chosen) {
                        gameManager.SockFound();
                    }
                }
            }
        }
    }

    void OnMouseUp() {
        dragging = false;
    }

    // https://answers.unity.com/questions/33719/how-to-apply-offset-to-touch-position-when-draggin.html
    // https://forum.unity.com/threads/how-to-drag-object-on-plan-x-z-without-mouse-center-offset.350137/
    void Update() {
        if (dragging) {
            Vector3 mousePos = Input.mousePosition;
            Vector3 distanceToScreen = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 currentPos = Camera.main.ScreenToWorldPoint(
                new Vector3(mousePos.x, mousePos.y, distanceToScreen.z)
            );
            transform.position = new Vector3(
                currentPos.x - distance.x,
                currentPos.y - distance.y,
                transform.position.z
            );
        }
    }

    public void Destroy() {
        Destroy(gameObject);
    }
}
