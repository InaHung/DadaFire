using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandController : MonoBehaviour
{

    public List<ICommand> commands;

    protected void Awake()
    {

        for (int i = 0; i < commands.Count; i++)
        {
            commands[i].Init();
        }

    }
}