using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Listener : ScriptableObject    
{

    public Dictionary<string, Delegate> eventTable = new Dictionary<string, Delegate>();

    private static Listener _instance;
    public static Listener instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = CreateInstance<Listener>();

            }
            return _instance;
        }
    }

    private void AddListener(string eventType, Action function)
    {
        if (!eventTable.ContainsKey(eventType))
        {
            eventTable.Add(eventType, null);
        }
        eventTable[eventType] = (Action)eventTable[eventType] + function;
        
    }

    public void Broadcast(string eventType)
    {

        Delegate d;
        if (eventTable.TryGetValue(eventType, out d))
        {
            Action callback = d as Action;
            if (callback != null)
            {
                callback();
            }
            else
            {
                Debug.LogWarning("No " + eventType + " Event !!");
            }
        }

    }

    public void AddListener(object target)
    {
        try
        {
            foreach (MethodInfo mInfo in target.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                foreach (ListenerAttribute aInfo in mInfo.GetCustomAttributes(typeof(ListenerAttribute), false))
                {
                    Delegate d = Delegate.CreateDelegate(typeof(Action), target, mInfo, false);
                    AddListener(aInfo.msg, (Action)d);
                }
            }

        }
        catch
        {

        }
    }

}
