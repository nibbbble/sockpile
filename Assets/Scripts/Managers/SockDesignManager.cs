using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SockDesignManager : MonoBehaviour
{
    public Sprite[] sockSprites;
    public Sprite sockShadow;
    public GameObject sockParent, sockBase;
    public int sockNumber;
    public float circleRadius = 10f;

    void Start() {
        CreateSocks();
    }

    void CreateSocks() {
        for (int i = 0; i < sockNumber; i++) {
            int randomInt = Random.Range(0, sockSprites.Length);
            CreateSock(sockSprites[randomInt]);
        }
    }

    void CreateSock(Sprite sockSprite) {
        Vector2 position = Random.insideUnitCircle * circleRadius;
        Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        GameObject sock = Instantiate(sockBase, position, rotation);
        sock.name = "Sock";
        sock.transform.parent = sockParent.transform;

        SpriteRenderer sockSpriteRenderer = sock.GetComponent<SpriteRenderer>();
        sockSpriteRenderer.sprite = sockSprite;
        CreateDropShadow(sock);
    }

    void CreateDropShadow(GameObject sock) {
        GameObject dropShadow = Instantiate(sock);
        // Destroy(dropShadow.GetComponent<Sock>());
        // Destroy(dropShadow.GetComponent<Collider2D>());
        dropShadow.name = "Shadow";

        Transform _t = dropShadow.transform;
        _t.SetParent(sock.transform);
        _t.position += new Vector3(.05f, -.05f, 0);

        SpriteRenderer _s = dropShadow.GetComponent<SpriteRenderer>();
        _s.sortingOrder = -1;
        _s.sprite = sockShadow;
        _s.color = new Color(0, 0, 0, 0.25f); // semi black
    }

    void Reset() {
        foreach (Transform child in sockParent.transform) {
            Destroy(child.gameObject);
        }
    }

    void Update() {
        #if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.F12)) {
                Reset();
                CreateSocks();
            }
        #endif
    }
}
