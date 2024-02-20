using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class Communicator : ScriptableObject
{

    private static Communicator _instance;
    private static Communicator instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = ScriptableObject.CreateInstance<Communicator>();

            }
            return _instance;
        }
    }
    private Dictionary<Type, object> singletonInstances = new Dictionary<Type, object>();


    public static void Connect(object target)
    {
        instance._Connect(target);
    }

    private void _Connect(object target)
    {
        ScriptableObject connectObject;

        try
        {
            foreach (FieldInfo fInfo in target.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                foreach (RegistAttribute aInfo in fInfo.GetCustomAttributes(typeof(RegistAttribute), false))
                {
                    if (fInfo.FieldType.IsPrimitive || fInfo.FieldType == typeof(string))
                    {
                        continue;
                    }
                    else
                    {
                        connectObject = _Get(fInfo.FieldType);
                        fInfo.SetValue(target, connectObject);
                    }


                }
            }
        }
        catch (System.Exception )
        {

        }
        finally
        {

        }
    }

    private ScriptableObject _Get(Type type)
    {
        ScriptableObject t;
        if (!singletonInstances.ContainsKey(type))
        {
            var newType = _New(type);
            singletonInstances.Add(type, newType);
            _Connect(newType);
        }

        t = (ScriptableObject)singletonInstances[type];
        return t;
    }

    private ScriptableObject _New(Type type, string scope = "")
    {
        string typeString = type.ToString();
        ScriptableObject returnSO = ScriptableObject.CreateInstance(typeString); ;
        return returnSO;
    }

}
