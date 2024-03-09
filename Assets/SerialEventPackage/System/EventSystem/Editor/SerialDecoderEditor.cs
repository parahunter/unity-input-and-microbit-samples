using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.TerrainTools;

#if UNITY_EDITOR
[CustomEditor(typeof(SerialDecoder))]
public class SerialDecoderEditor : Editor
{
    bool foldout = true;


    
    public override void OnInspectorGUI()
    {
        SerialDecoder serialDecoder = (SerialDecoder)target;

        // create a style based on the default label style
        GUIStyle myStyle = new GUIStyle(GUI.skin.button);
        // do whatever you want with this style, e.g.:
        myStyle.alignment = TextAnchor.MiddleRight;

        GUIStyle popupStyle = new GUIStyle(GUI.skin.button);
        popupStyle.alignment = TextAnchor.MiddleCenter;
        

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Split Serial Input at Char:");
        serialDecoder.idEndChar = EditorGUILayout.TextField(serialDecoder.idEndChar);
        EditorGUILayout.EndHorizontal();

        foldout = EditorGUILayout.Foldout(foldout,"Serial Tags");

        if (foldout)
        {
            for (int i = 0; i < serialDecoder.tags.tags.Count; i++)
            {
                GUILayout.BeginVertical("GroupBox");
                EditorGUILayout.BeginHorizontal();
                serialDecoder.tags.tags[i].dataType = (SerialTagsScriptableObject.SerialTag.DataType)EditorGUILayout.EnumPopup(serialDecoder.tags.tags[i].dataType,GUILayout.MinWidth(50f));
                serialDecoder.tags.tags[i].identifier = EditorGUILayout.TextField(serialDecoder.tags.tags[i].identifier,GUILayout.MaxWidth(500));
                if (GUILayout.Button("i",myStyle, GUILayout.MaxWidth(18)))
                {
                    serialDecoder.tags.tags[i].infoFoldout = !serialDecoder.tags.tags[i].infoFoldout;
                }
                
                EditorGUILayout.EndHorizontal();
                if (serialDecoder.tags.tags[i].infoFoldout)
                {
                    EditorGUILayout.Space(10);
                    EditorGUILayout.LabelField("Example Input: " + serialDecoder.tags.tags[i].identifier + serialDecoder.idEndChar + valueExample(serialDecoder.tags.tags[i].dataType));
                }
                GUILayout.EndVertical();
            }
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space(25);
            if (GUILayout.Button("+", GUILayout.MinWidth(40)))
            {
                serialDecoder.tags.tags.Add(new SerialTagsScriptableObject.SerialTag());
            }
            if(GUILayout.Button("-", GUILayout.MinWidth(40)))
            {
                serialDecoder.tags.tags.RemoveAt(serialDecoder.tags.tags.Count-1);
            }
            EditorGUILayout.EndHorizontal();
            
        }


        //base.OnInspectorGUI();
    }

    private string valueExample(SerialTagsScriptableObject.SerialTag.DataType typeOfData)
    {
        switch (typeOfData)
        {
            case SerialTagsScriptableObject.SerialTag.DataType.Float:
                return "1.067";
            case SerialTagsScriptableObject.SerialTag.DataType.Int:
                return "45";
            case SerialTagsScriptableObject.SerialTag.DataType.Bool:
                return "true";
            case SerialTagsScriptableObject.SerialTag.DataType.String:
                return "Hello World!";
            case SerialTagsScriptableObject.SerialTag.DataType.Vector3:
                return "13,7,4.3";
            default:
                return "";
        }
    }
}
#endif