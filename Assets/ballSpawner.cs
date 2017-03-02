using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ballSpawner : NetworkBehaviour {

    public GameObject ballPrefab;
    private GameObject ball;

    public override void OnStartServer()
    {
        removeBalls();
        var spawnPosition = new Vector2(Random.Range(-8f, 8f), Random.Range(-8f, 8f));
        ball = (GameObject)Instantiate(ballPrefab, spawnPosition,Quaternion.Euler(Vector2.zero));
        NetworkServer.Spawn(ball);
    }

    public void removeBalls()
    {
        Destroy(ball);
    }
}
