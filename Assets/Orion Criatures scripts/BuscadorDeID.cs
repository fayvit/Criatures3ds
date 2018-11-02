using UnityEngine;
using System.Reflection;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class BuscadorDeID
{

    public static string GetUniqueID(GameObject G,string id)
    {
#if UNITY_EDITOR
        PropertyInfo inspectorModeInfo =
    typeof(SerializedObject).GetProperty("inspectorMode", BindingFlags.NonPublic | BindingFlags.Instance);

        SerializedObject serializedObject = new SerializedObject(G.transform);
        inspectorModeInfo.SetValue(serializedObject, InspectorMode.Debug, null);

        SerializedProperty localIdProp =
            serializedObject.FindProperty("m_LocalIdentfierInFile");   //note the misspelling!

        int localId = localIdProp.intValue;

        return localId.ToString();
#endif
        return id;
    }

    public static void SetUniqueIdProperty(Object o,string id,string nomeProperty)
    {
#if UNITY_EDITOR
        Debug.Log("aqui");
        PropertyInfo inspectorModeInfo =
    typeof(SerializedObject).GetProperty("inspectorMode", BindingFlags.NonPublic | BindingFlags.Instance);

        SerializedObject serializedObject = new SerializedObject(o);
        inspectorModeInfo.SetValue(serializedObject, InspectorMode.Debug, null);

        SerializedProperty localIdProp =
            serializedObject.FindProperty(nomeProperty);

        Debug.Log(localIdProp.stringValue);
        localIdProp.stringValue = id;
#endif
    }

        
}