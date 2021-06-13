// fix sorting problem soon

// https://answers.unity.com/questions/598492/how-do-you-set-an-order-for-2d-colliders-that-over.html

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sock : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public bool chosen;

    GameManager gameManager;
    SockManager sockManager;
    AudioManager _audio;
    Sprite sprite;
    bool dragging = false;
    // float distance;
    Vector3 distance;
    RaycastHit2D hit;
    // Collider2D hit;
    // RaycastHit2D[] hits;

    void Start() {
        GameObject gameController = GameObject.FindWithTag("GameController");
        gameManager = gameController.GetComponentInChildren<GameManager>();
        sockManager = gameController.GetComponentInChildren<SockManager>();
        _audio = AudioManager.i;

        sprite = spriteRenderer.sprite;
        CreateDropShadow();
    }

    // ;-;
    void CreateDropShadow() {
        GameObject dropShadow = Instantiate(gameObject, transform.position, transform.rotation);
        Destroy(dropShadow.GetComponent<Sock>());
        Destroy(dropShadow.GetComponent<Collider2D>());
        dropShadow.name = "Shadow";

        Transform _t = dropShadow.transform;
        _t.SetParent(gameObject.transform);
        _t.position += new Vector3(.05f, -.05f, .05f);

        SpriteRenderer _s = dropShadow.GetComponent<SpriteRenderer>();
        _s.sortingOrder--;
        _s.sprite = sockManager.GetSockShadow();
        _s.color = new Color(0, 0, 0, 0.25f); // semi black
    }

    void OnMouseDown() {
        if (gameManager.gameRunning) {
            if (hit) {
                if (hit.transform == transform) {
                    int highestOrder = sockManager.GetHighestOrder();
                    
                    gameObject.transform.SetAsLastSibling();
                    gameObject.transform.position = new Vector3(
                        transform.position.x,
                        transform.position.y,
                        -(highestOrder / 2)
                    );
                    spriteRenderer.sortingOrder = highestOrder;
                    sockManager.Reorder(transform);

                    if (chosen) {
                        gameManager.SockFound();
                    } else {
                        _audio.Play("Wrong", AudioData.SoundType.SFX);
                    }

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

                    sockManager.LastClickedSock(transform);
                }
            }

            // LMAOOOOOOOOOOOOOOOOOOOOOOOO
            // Transform _hit;
            // foreach (RaycastHit2D hit in hits) {
            //     if (hit.transform.TryGetComponent<Sock>(out Sock sock)) {
            //         if (hit.transform.GetSiblingIndex() == hit.transform.parent.childCount - 1) {
            //             // _hit = hit.transform;
            //             // Drag(_hit);
            //         } else {
            //             hit.transform.SetAsLastSibling();
            //         }
            //         _hit = hit.transform;
            //         Drag(_hit);
            //     }
            // }
        }
    }

    // void Drag(Transform sock) {
    //     sock.transform.SetAsLastSibling();

    //     if (chosen) {
    //         gameManager.SockFound();
    //     } else {
    //         _audio.Play("Wrong", AudioData.SoundType.SFX);
    //     }

    //     Vector3 mousePos = Input.mousePosition;
    //     Vector3 distanceToScreen = Camera.main.WorldToScreenPoint(sock.transform.position);
    //     distance = Camera.main.ScreenToWorldPoint(
    //         new Vector3(
    //             mousePos.x, 
    //             mousePos.y,
    //             distanceToScreen.z
    //         )
    //     ) - sock.transform.position;
    //     dragging = true;
    // }

    void OnMouseUp() {
        dragging = false;
    }

    // https://answers.unity.com/questions/33719/how-to-apply-offset-to-touch-position-when-draggin.html
    // https://forum.unity.com/threads/how-to-drag-object-on-plan-x-z-without-mouse-center-offset.350137/
    void Update() {
        hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        // if (hit) {
        //     Debug.Log(hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite);
        //     Debug.DrawRay(hit.transform.position, Vector3.forward * 100, Color.red);
        // }
        // hit = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            
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
}
