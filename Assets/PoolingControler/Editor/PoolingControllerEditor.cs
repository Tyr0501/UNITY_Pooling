using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CustomEditor(typeof(ParentSpawnController))]
public class PoolingControllerEditor : Editor
{
    override public void OnInspectorGUI()
    {
        var myScript = target as ParentSpawnController;
        myScript.namePooling = EditorGUILayout.TextField("Name Pooling:", myScript.namePooling);
        EditorGUILayout.LabelField("Prefab Object:", EditorStyles.boldLabel);
        myScript.prefabObj = EditorGUILayout.ObjectField(myScript.prefabObj, typeof(GameObject), true) as GameObject;
        myScript.testSpawns = GUILayout.Toggle(myScript.testSpawns, "Test spawns:");

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Target:", EditorStyles.boldLabel);
        myScript.isLimitedCameraArea = GUILayout.Toggle(myScript.isLimitedCameraArea, "Limited Camera Area:");
        if (!myScript.isLimitedCameraArea)
        {
            myScript.isCollide = GUILayout.Toggle(myScript.isCollide, "Collide:");
            if (myScript.isCollide)
            {
                myScript.nameDestinationTag = EditorGUILayout.TextField("Name destination tag:", myScript.nameDestinationTag);
            }
            else
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Life distance:", EditorStyles.boldLabel);
                myScript.x = GUILayout.Toggle(myScript.x, "X");
                myScript.y = GUILayout.Toggle(myScript.y, "Y");
                myScript.limitedDistance = EditorGUILayout.FloatField("Limited distance:", myScript.limitedDistance);
            }
        }
    }
}
