using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    public int speedMultiplier = 10;
    public int rotationMultiplier = 10;
    public int boostTrust = 10;
    public bool isFwd;
    public bool isDrifting;
    private bool isTurning;
    private float rotation = 0;
    public float rMult = 1;

    int currentAnimationSpeed = 0;
    Animator animator;
   
    // Use this for initialization
    void Start () {
		animator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
        {
            return;
        }

        
        Rigidbody2D rb = this.GetComponent<Rigidbody2D>();

        rMult =rb.GetRelativePointVelocity(Vector2.zero).magnitude * Time.deltaTime;
        if (Input.GetAxis("Vertical") != 0)
        {
            rb.AddRelativeForce(new Vector2( 0, Input.GetAxis("Vertical")) * Time.deltaTime * speedMultiplier *1000);
            if (Input.GetAxis("Vertical")>0 && Input.GetAxis("Jump") <1)
            {
                changeSpeed(1);
            }
            else if(Input.GetAxis("Vertical") < 0 && Input.GetAxis("Jump") < 1)
            {
                changeSpeed(-1);
            }
        }else if (Input.GetAxis("Jump") < 1)
        {
            changeSpeed(0);
        }
        if (Input.GetAxis("Jump")>0)
        {
            changeSpeed(2);
            rb.AddRelativeForce(new Vector2(0,Time.deltaTime * speedMultiplier * 100),ForceMode2D.Impulse);
        }

        rotation = Vector2.Angle(rb.GetRelativePointVelocity(Vector2.zero), rb.GetRelativeVector(Vector2.up));
        if (rotation < 90)
        {
            isFwd = true;
            if (rotation > 45)
            {
                isDrifting = true;
            }
            else
            {
                isDrifting = false;
            }
        }
        else if (rotation > 90)
        {
            isFwd = false;
            if (rotation < 135)
            {
                isDrifting = true;
            }
            else
            {
                isDrifting = false;
            }
        }

        if (isDrifting)
        {
            rMult = rMult/3;
        }

        if (isFwd)
        {
            rMult = 0 - rMult;
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            rb.AddTorque(rotationMultiplier* rMult * Input.GetAxis("Horizontal"));
            if (!isTurning)
            {
                animator.SetBool("turning", true);
                isTurning = true;
            }
            
        }
        else if(isTurning)
        {
            animator.SetBool("turning", false);
            isTurning = false;
        }
    }


    void changeSpeed(int speed)
    {
        if (currentAnimationSpeed == speed)
            return;
        switch (speed)
        {
            case -1:
                animator.SetInteger("speed", -1);
                break;

            case 0:
                animator.SetInteger("speed", 0);
                break;

            case 1:
                animator.SetInteger("speed", 1);
                break;

            case 2:
                animator.SetInteger("speed", 2);
                break;
        }
        currentAnimationSpeed = speed;
    }
}
