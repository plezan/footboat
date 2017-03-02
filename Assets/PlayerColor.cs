using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerColor : NetworkBehaviour {
     Color[] playerColors = {
        new Color(255, 0, 0),
        new Color(0, 0, 255),
        new Color(0, 255, 0),
        new Color(255, 255, 0),
        new Color(255, 0, 255),
        new Color(0, 255, 255),
    };

    [SyncVar(hook = "OnColorUpdate")]
    public int color = -1;

    void OnColorUpdate(int color)
    {
            PlayerColor pl = this.GetComponent<PlayerColor>();
            SpriteRenderer spr = this.GetComponent<SpriteRenderer>();
            if (color < 0 || color > 5)
            {
                color = Random.Range(0, 5);
            }
            spr.color = playerColors[color];
    }

	// Use this for initialization
	void Start () {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        int nbPlayer = players.GetLength(0);
        print("nb de Joueurs : " + nbPlayer);

        foreach (var player in players)
        {
            PlayerColor pl = player.GetComponent<PlayerColor>();
            SpriteRenderer spr = player.GetComponent<SpriteRenderer>();
            if (pl.color < 0 || pl.color > 5)
            {
                pl.color = Random.Range(0, 5);
            }
            spr.color = playerColors[pl.color];
        }   
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
