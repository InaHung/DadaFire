using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ICommand : ScriptableObject
{

    public virtual void Init()
    {
        Communicator.Connect(this);
        Listener.instance.AddListener(this);
        InitComplete();
    }

    public abstract void InitComplete(); 



    public void Broadcast(string name)
    {
        Listener.instance.Broadcast(name);
    }
}
