using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProxy : IProxy
{
    public Transform playerTransform;
    public float playerSpeedPercentage;
    public int increseHp;
    public void SetPlayerTransform(Transform transform)
    {
        playerTransform = transform;
    }
    public void SetPlayerSpeed(float speed)
    {
        playerSpeedPercentage = speed;
        Broadcast(PlayerEvent.ON_SET_PLAYER_MOVESPEED);
    }
    public void SetPlayerHp(int hp)
    {
        increseHp = hp;
        Broadcast(PlayerEvent.ON_SET_PLAYER_HP);
    }
    
}
