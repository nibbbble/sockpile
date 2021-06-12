using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SockManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject sockBase;
    public Sprite[] sockSprites;
    public Sprite[] hardSockSprites;
    public GameObject sockParent;
    public Sprite sockShadow;
    public int sockSpawnRate = 5;
    public float circleRadius = 10f;
    List<Sprite> sockSpritesList;
    int sockSpritesIndex;

    void Start() {
        sockSpritesList = new List<Sprite>();
        foreach (Sprite sprite in sockSprites) {
            sockSpritesList.Add(sprite);
        }
        sockSpritesIndex = 0;
        
        NewRound(true, 7); // lol
    }

    public void NewRound(bool start, int score) {
        if (!start)
            if (score % 2 == 0)
                sockSpawnRate++;

        if (score % 4 == 0) {
            if (sockSpritesIndex < hardSockSprites.Length) {
                sockSpritesList.Add(hardSockSprites[sockSpritesIndex]);
                sockSpritesIndex++;
            }
        }
        
        foreach (Transform child in sockParent.transform) {
            Destroy(child.gameObject);
        }
        
        Sprite chosenSock = ChooseSock();
        gameManager.UpdateFrame(chosenSock);
        SpawnSocks(sockSpawnRate, chosenSock);
    }

    Sprite ChooseSock() {
        Sprite chosenSock = sockSpritesList[Random.Range(0, sockSpritesList.Count)];
        return chosenSock;
    }

    void SpawnSocks(int spawnRate, Sprite chosenSock) {
        List<Sprite> newSockSprites = new List<Sprite>();
        foreach (Sprite sprite in sockSpritesList) {
            if (sprite != chosenSock)
                newSockSprites.Add(sprite);
        }
        
        // spawn the chosen sock
        SpawnSock(chosenSock, true);

        // then spawn all socks!
        for (int i = 0; i < spawnRate; i++) {
            Sprite randomSockSprite = newSockSprites[Random.Range(0, newSockSprites.Count)];
            SpawnSock(randomSockSprite, false);
        }
    }

    void SpawnSock(Sprite _sock, bool chosen) {
        GameObject sock = sockBase;
        // uniform distribution
        // https://forum.unity.com/threads/centre-of-sphere-for-random-insideunitsphere.83824/
        // Vector2 randomCircle = Random.insideUnitCircle * circleRadius;
        // Vector2 position = new Vector2(Mathf.Sqrt(randomCircle.x), Mathf.Sqrt(randomCircle.y));
        Vector2 position = Random.insideUnitCircle * circleRadius;
        Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));

        GameObject spawnedSock = Instantiate(sock, position, rotation);
        spawnedSock.name = "Sock";

        GameObject spawnedSockParent = sockParent;
        SpriteRenderer spawnedSockSprite = spawnedSock.GetComponent<SpriteRenderer>();
        spawnedSockSprite.sprite = _sock;
        spawnedSock.transform.SetParent(spawnedSockParent.transform);
        
        if (chosen) {
            Sock spawnedSockScript = spawnedSock.GetComponent<Sock>();
            spawnedSockScript.chosen = true;
        }
    }

    public Sprite GetSockShadow() {
        return sockShadow;
    }
}
