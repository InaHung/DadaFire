using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IProxy : ScriptableObject
{


    public void Broadcast(string name)
    {
        Listener.instance.Broadcast(name);
    }
}
