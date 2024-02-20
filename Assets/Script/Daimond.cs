using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Daimond : IPoolable
{
    public int score;
    public Action<Daimond> OnDisposeDaimond;
    public DaimondType daimondType;
    public float moveSpeed;
    public bool eatMagnet;
    private Transform playerTransform;
    public float originSpeed;
    private void Awake()
    {
        originSpeed = moveSpeed;
    }
    private void Update()
    {
        if(eatMagnet)
        {
            MoveToPlayer(playerTransform.position);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "player")
        {
            Dispose();
            OnDisposeDaimond(this);
            eatMagnet = false;
        }

    }
    public void MoveToPlayer(Vector3 movePosition)
    {
        transform.position = Vector2.MoveTowards(transform.position, movePosition,moveSpeed*Time.deltaTime);
    }
    public void GetPlayerTransform(Transform transform)
    {
        playerTransform = transform;
    }

    

}
public enum DaimondType
{
    greenDaimond,
    blueDaimond,
    yellowDaimond,

}
