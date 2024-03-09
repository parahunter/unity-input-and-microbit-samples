using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

[RequireComponent(typeof(SerialDecoder))]
public class SerialReciever : MonoBehaviour
{

    public string recievedTransmission;
    private  SerialDecoder decoder;

    private void Awake()
    {
        decoder = GetComponent<SerialDecoder>();
    }

    // Invoked when a line of data is received from the serial device.
    void OnMessageArrived(string msg)
    {
        Debug.Log(msg);
        recievedTransmission = msg;
        ParseString(msg);
    }
    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device connected" : "Device disconnected");
    }


    public void ParseString(string input)
    {
        decoder.Parse(input);
    }

    [ContextMenu("Parse")]
    public void TestString()
    {
        ParseString(recievedTransmission);
    }
    
}
