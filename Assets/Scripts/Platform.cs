using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform collectableSpawnPoint;

    private int id;

    protected Player player;
    protected Rigidbody2D rb;

    public int Id { get => id; set => id = value; }

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        if (!GameManager.Ins) return; 

        player = GameManager.Ins.player;

        if(collectableSpawnPoint)
            GameManager.Ins.SpawnCollectable(collectableSpawnPoint);
    }

    public virtual void PlatformAction()
    {

    }
}
