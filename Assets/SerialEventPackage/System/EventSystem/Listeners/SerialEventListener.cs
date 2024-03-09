using UnityEngine;
using UnityEngine.Events;
using Unity.VisualScripting;

namespace MA.Events
{
    public abstract class SerialGameEventListener<T0,T1, E> : MonoBehaviour,
        ISerialGameEventListener<T0,string> where E : SerialGameEvent<T0,string>
    {
        [SerializeField] private E gameEvent;
        [SerializeField] private string eventIdentifier;
        public E GameEvent { get { return gameEvent; } set { gameEvent = value; } }

        public SerialTagsScriptableObject serialTags;
        public int selectedTag = 0;

        public bool visualScriptingCompatible = false;
        public SerialTagsScriptableObject.SerialTag.DataType dataType;
        [SerializeField]
        public FloatEvent floatEventResponse;
        public IntEvent intEventResponse;
        public BoolEvent boolEventResponse;
        public StringEvent stringEventResponse;
        public Vector3Event vector3EventResponse;

        private void OnEnable()
        {
            if(gameEvent == null) { return; }

            GameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            if(gameEvent == null) { return; }

            GameEvent.UnregisterListener(this);
        }

        public void OnEventRaised(T0 identifier, string value)
        {
            //Debug.Log("SerialEventListener: " + "recieved " +  identifier + " + " + value);
            

            switch (serialTags.tags[selectedTag].dataType)
            {
                case SerialTagsScriptableObject.SerialTag.DataType.Float:
                    if (identifier.ToString() != serialTags.tags[selectedTag].identifier)
                        return;

                    Debug.Log("SerialEventListener: recieved id matches '" + identifier.ToString() + "' with a value of '" + value + "' and a type of 'Float'") ;

                    Debug.Log("value has been parsed as: " + float.Parse(value));
                    float parsedfloat = float.Parse(value);

                    if (visualScriptingCompatible)
                    {
                        CustomEvent.Trigger(this.gameObject, identifier.ToString(), parsedfloat);
                        return;
                    }

                    floatEventResponse.Invoke(parsedfloat);
                    break;


                case SerialTagsScriptableObject.SerialTag.DataType.Int:
                    if (identifier.ToString() != serialTags.tags[selectedTag].identifier)
                        return;
                    Debug.Log("SerialEventListener: recieved id matches '" + identifier.ToString() + "' with a value of '" + value + "' and a type of 'Int'");

                    Debug.Log("value has been parsed as: " + int.Parse(value));

                    int parsedInt = int.Parse(value);

                    if (visualScriptingCompatible)
                    {
                        CustomEvent.Trigger(this.gameObject, identifier.ToString(), parsedInt);
                        return;
                    }

                    intEventResponse.Invoke(parsedInt);
                    break;


                case SerialTagsScriptableObject.SerialTag.DataType.Bool:

                    if (identifier.ToString() != serialTags.tags[selectedTag].identifier)
                        return;
                    Debug.Log("SerialEventListener: recieved id matches '" + identifier.ToString() + "' with a value of '" + value + "' and a type of 'Bool'");

                    bool parsedBool = false;

                    if (value.ToString().ToLower() == "true")
                    {
                        Debug.Log("value has been parsed as: " + true);
                        parsedBool = true;
                        break;
                    }
                    else if(value.ToString().ToLower() == "false")
                    {
                        Debug.Log("value has been parsed as: " + false);
                        parsedBool = false;
                        break;
                    }

                    if (visualScriptingCompatible)
                    {
                        CustomEvent.Trigger(this.gameObject, identifier.ToString(), parsedBool);
                        return;
                    }

                    Debug.Log("value has been parsed as: " + bool.Parse(value));
                    boolEventResponse.Invoke(parsedBool);
                    //boolEventResponse.Invoke(bool.Parse(value));
                    break;


                case SerialTagsScriptableObject.SerialTag.DataType.String:
                    if (identifier.ToString() != serialTags.tags[selectedTag].identifier)
                        return;
                    Debug.Log("SerialEventListener: recieved id matches '" + identifier.ToString() + "' with a value of '" + value + "' and a type of 'String'");

                    Debug.Log("value remains unparsed");

                    if (visualScriptingCompatible)
                    {
                        CustomEvent.Trigger(this.gameObject, identifier.ToString(), value);
                        return;
                    }

                    stringEventResponse.Invoke(value);
                    break;


                case SerialTagsScriptableObject.SerialTag.DataType.Vector3:

                    if (identifier.ToString() != serialTags.tags[selectedTag].identifier)
                        return;
                    Debug.Log("SerialEventListener: recieved id matches '" + identifier.ToString() + "' with a value of '" + value + "' and a type of 'Vector3'");

                    value = value.Replace("(", "");
                    value = value.Replace(")", "");

                    string[] vecArr = value.Split(',');
                    if(vecArr.Length < 2) return;

                    Vector3 parsedVector = new Vector3(float.Parse(vecArr[0]), float.Parse(vecArr[1]), float.Parse(vecArr[2]));


                    Debug.Log("value has been parsed as: " + parsedVector);

                    if (visualScriptingCompatible)
                    {
                        CustomEvent.Trigger(this.gameObject, identifier.ToString(), parsedVector);
                        return;
                    }

                    vector3EventResponse.Invoke(parsedVector);
                    break;


                default:
                    break;
            }
        }

        public GameObject GetGameobject()
        {
            return this.gameObject;
        }
    }
}

[SerializeField]
[System.Serializable]
public class FloatEvent : UnityEvent<float> { }
[System.Serializable]
public class IntEvent : UnityEvent<int> { }
[System.Serializable]
public class BoolEvent : UnityEvent<bool> { }
[System.Serializable]
public class Vector3Event : UnityEvent<Vector3> { }
[System.Serializable]
public class StringEvent : UnityEvent<string> { }
