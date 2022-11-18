using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatf : Platform
{
    public float moveSpeed;
    private bool canMoveLeft;
    private bool canMoveRight;

    protected override void Start()
    {
        base.Start();

        float randcheck = Random.Range(0, 1f);
        if (randcheck <= 0.5f)
        {
            canMoveLeft = true;
            canMoveRight = false;
        }
        else
        {
            canMoveLeft = false;
            canMoveRight = true;
        }
    }

    private void FixedUpdate()
    {
        float curSpeed = 0;

        if (!rb) return;

        if(canMoveLeft) curSpeed = -moveSpeed;
        else if (canMoveRight) curSpeed = moveSpeed;

        rb.velocity = new Vector2(curSpeed, 0);    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameTag.LeftCorner.ToString()))
        {
            canMoveLeft = false;
            canMoveRight = true;
        }
        else if(collision.CompareTag(GameTag.RightCorner.ToString()))
        {
            canMoveLeft = true;
            canMoveRight = false;
        }
    }
}
