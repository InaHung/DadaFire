using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IMediator : ScriptableObject
{

    private void Awake()
    {
        Listener.instance.AddListener(this);
    }

    public void Broadcast(string name)
    {
        Listener.instance.Broadcast(name);
    }

    public abstract void Register(IView view);

}
