using MA.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SerialReciever))]
public class SerialDecoder : MonoBehaviour
{
    public SerialStringEvent serialEvent;
    public SerialTagsScriptableObject tags;
    public string idEndChar;

    public void Parse(string input)
    {
        string[] splitArr = input.Split(idEndChar);

        if (!CheckTag(splitArr[0]))
            return;

        serialEvent.Raise(splitArr[0], splitArr[1]);
        Debug.Log("SerialDecoder: " + "parsed " + splitArr[0] + " + " + splitArr[1]);

    }

    private bool CheckTag(string tag)
    {
        for (int i = 0; i < tags.tags.Count; i++)
        {
            if (tags.tags[i].identifier == tag)
                return true;
        }

        return false;
    }

   
}
