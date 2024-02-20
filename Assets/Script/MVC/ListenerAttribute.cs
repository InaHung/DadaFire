using System;
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class ListenerAttribute : Attribute
{

    public string msg;
    public ListenerAttribute(string msg)
    {
        this.msg = msg;
    }
}
