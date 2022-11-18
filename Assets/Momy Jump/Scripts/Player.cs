using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce;
    public float moveSpeed;
    private Platform platformLanded;
    private float movingLimitX;

    private Rigidbody2D rb;

    public Platform PlatformLanded { get => platformLanded; set => platformLanded = value; }
    public float MovingLimitX { get => movingLimitX; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MoveHandle();
    }

    public void jump()
    {
        if (!GameManager.Ins || GameManager.Ins.State != GameState.Playing) return;

        if (!rb || rb.velocity.y > 0 || !platformLanded) return;

        if(platformLanded is BreakablePlatf)
        {
            platformLanded.PlatformAction();
        }

        rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        if (AudioController.Ins)
            AudioController.Ins.PlaySound(AudioController.Ins.jump);
    }

    private void MoveHandle()
    {
        if(!GamepadController.Ins || !rb || !GameManager.Ins || GameManager.Ins.State != GameState.Playing) return;

        if(GamepadController.Ins.CanMoveLeft)
            rb.velocity = new Vector2(-moveSpeed,rb.velocity.y);
        
        else if(GamepadController.Ins.CanMoveRight)        
            rb.velocity = new Vector2(moveSpeed,rb.velocity.y);
        
        else 
            rb.velocity = new Vector2(0,rb.velocity.y);

        movingLimitX = Helper.Get2DCamSize().x / 2;

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -movingLimitX, movingLimitX),
            transform.position.y,
            transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameTag.Collectable.ToString()))
        {
            var collectable = collision.GetComponent<Collectable>();
            if(collectable)
            {
                collectable.Trigger();
            }
        }
    }
}
