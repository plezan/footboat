using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ballStats : NetworkBehaviour {
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
        ballStats bs = this.GetComponent<ballStats>();
        SpriteRenderer spr = this.GetComponent<SpriteRenderer>();
        if (color < 0 || color > 5)
        {
            spr.color = Color.white;
        }
        else
        {
            spr.color = playerColors[color];
        }
        
    }

private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerColor pl = collision.gameObject.GetComponent<PlayerColor>();

            color = pl.color;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
