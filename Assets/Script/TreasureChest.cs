using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class TreasureChest : IPoolable
{
    public Action<Vector3> onGetTreasure;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="weapon")
        {
            onGetTreasure(transform.position);
            Dispose();
        }
    }
}
