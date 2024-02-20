using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    public int damage;
    private PlayerView player;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "player")
        {
           player = collision.transform.GetComponent<PlayerView>();

        }
    }
    private void Update()
    {
        if(player!=null)
        {
            player.PlayerDamage(damage);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        player = null;
        //123
    }
}
