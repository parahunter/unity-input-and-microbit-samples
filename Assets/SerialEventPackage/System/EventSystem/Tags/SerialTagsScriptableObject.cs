using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Serial Tag Array", menuName = "Serial Tag Array")]
public class SerialTagsScriptableObject : ScriptableObject
{
    [SerializeField]
    public List<SerialTag> tags;
    


    [System.Serializable]
    public class SerialTag
    {
        [SerializeField]
        public string identifier;
        [System.Serializable]
        public enum DataType { Float, Int, Bool, String, Vector3 }
        [SerializeField]
        public DataType dataType;

        #if UNITY_EDITOR
        public bool infoFoldout;
        #endif
        public SerialTag(string identifier = "New tag", DataType dataType = DataType.Float)
        {
            this.identifier = identifier;
            this.dataType = dataType;
        }
    }

    public string[] GetStringArray()
    {
        string[] tagsNames = new string[tags.Count];
        for (int i = 0; i < tags.Count; i++)
        {
            tagsNames[i] = tags[i].identifier;
        }
        return tagsNames;
    }
}
