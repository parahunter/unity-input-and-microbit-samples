using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialDebugger : MonoBehaviour
{
    public float recievedFloat;
    public int recievedInt;
    public string recievedString;
    public bool recievedBool;
    public Vector3 recievedVector;
    public void RecieveFloat(float value)
    {
        recievedFloat = value;
    }
    public void RecieveInt(int value)
    {
        recievedInt = value;
    }
    public void RecieveString(string value)
    {
        recievedString = value;
    }
    public void RecieveBool(bool value)
    {
        recievedBool = value;
    }
    public void RecieveVector(Vector3 value)
    {
        recievedVector = value;
    }
}
