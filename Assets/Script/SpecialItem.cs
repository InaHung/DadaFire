using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SpecialItem : MonoBehaviour
{
    public SpecialItemType itemType;
    public Action<SpecialItemType> onEatSpecailItem;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="player")
        {
            onEatSpecailItem(itemType);
            Destroy(transform.gameObject);
        }
    }
}
public enum SpecialItemType
{
    magnet,
    bomb,
    meat,
}
