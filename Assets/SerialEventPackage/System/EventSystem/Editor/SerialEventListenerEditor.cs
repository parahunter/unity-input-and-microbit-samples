using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using MA.Events;

#if UNITY_EDITOR
[CustomEditor(typeof(SerialListener))]
public class SerialEventListenerEditor : Editor
{


    public string[] strings = new string[]{"Hello","Goodbye","Test"};

    public override void OnInspectorGUI()
    {
        

        SerialListener script = (SerialListener)target;

        //script.dataType = (SerialTagsScriptableObject.SerialTag.DataType)EditorGUILayout.EnumPopup("Data Type", script.dataType);
        


        if(script.selectedTag > script.serialTags.tags.Count - 1)
        {
            script.selectedTag = script.serialTags.tags.Count - 1;
        }

        script.selectedTag = EditorGUILayout.Popup(script.selectedTag, script.serialTags.GetStringArray());

        SerialTagsScriptableObject.SerialTag.DataType selectedType = script.serialTags.tags[script.selectedTag].dataType;

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Use Visual Scripting");
        script.visualScriptingCompatible = EditorGUILayout.Toggle(script.visualScriptingCompatible);
        EditorGUILayout.EndHorizontal();
        
        if(script.visualScriptingCompatible )
        {
            return;
        }

        switch (selectedType)
        {
            case SerialTagsScriptableObject.SerialTag.DataType.Float:
                SerializedProperty floatEventProperty = serializedObject.FindProperty("floatEventResponse");
                EditorGUILayout.PropertyField(floatEventProperty,true);

                break;


            case SerialTagsScriptableObject.SerialTag.DataType.Int:
                SerializedProperty intEventProperty = serializedObject.FindProperty("intEventResponse");
                EditorGUILayout.PropertyField(intEventProperty, true);

                break;


            case SerialTagsScriptableObject.SerialTag.DataType.Bool:
                SerializedProperty boolEventProperty = serializedObject.FindProperty("boolEventResponse");
                EditorGUILayout.PropertyField(boolEventProperty, true);
                break;


            case SerialTagsScriptableObject.SerialTag.DataType.String:
                SerializedProperty stringEventProperty = serializedObject.FindProperty("stringEventResponse");
                EditorGUILayout.PropertyField(stringEventProperty, true);
                break;


            case SerialTagsScriptableObject.SerialTag.DataType.Vector3:
                SerializedProperty vectorEventProperty = serializedObject.FindProperty("vector3EventResponse");
                EditorGUILayout.PropertyField(vectorEventProperty, true);

                break;


            default:
                break;
        }

        serializedObject.ApplyModifiedProperties();
        //base.OnInspectorGUI();
    }
}
#endif