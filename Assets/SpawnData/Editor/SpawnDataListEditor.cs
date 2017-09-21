using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(SpawnDataList))]
public class SpawnDataListEditor : Editor
{

	SerializedProperty prop;

	public override void OnInspectorGUI ()
	{
		
		serializedObject.Update ();

		EditorGUILayout.LabelField ("Spawn Data", EditorStyles.boldLabel);

		//Get SerializedProperty
		prop = serializedObject.FindProperty ("spawnDatas");

		//Display size of the list
		EditorGUILayout.PropertyField (prop.FindPropertyRelative ("Array.size"));

		EditorGUILayout.BeginHorizontal ();
		EditorGUILayout.LabelField ("Level", GUILayout.Width (40));
		EditorGUILayout.LabelField ("Enemy", GUILayout.Width (200));
		EditorGUILayout.LabelField ("Time To Spawn", GUILayout.Width (120));
		EditorGUILayout.EndHorizontal ();

		for (int i = 0; i < prop.arraySize; i++) {
			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.PropertyField (prop.GetArrayElementAtIndex (i).FindPropertyRelative ("level"), GUIContent.none, GUILayout.Width (40));
			EditorGUILayout.PropertyField (prop.GetArrayElementAtIndex (i).FindPropertyRelative ("enemy"), GUIContent.none, GUILayout.Width (200));
			EditorGUILayout.PropertyField (prop.GetArrayElementAtIndex (i).FindPropertyRelative ("timeUntilSpawn"), GUIContent.none, GUILayout.Width (120));

			if (GUILayout.Button ("+", EditorStyles.miniButtonLeft, GUILayout.MaxWidth (30))) {
				AddEntry (i);
			}
			if (GUILayout.Button ("-", EditorStyles.miniButtonMid, GUILayout.MaxWidth (30))) {
				DeleteEntry (i);
			}
			if (GUILayout.Button ("<", EditorStyles.miniButtonMid, GUILayout.MaxWidth (30))) {
				ShiftEntryUp (i);
			}
			if (GUILayout.Button (">", EditorStyles.miniButtonMid, GUILayout.MaxWidth (30))) {
				ShiftEntryDown (i);
			}

			EditorGUILayout.EndHorizontal ();
		}

		serializedObject.ApplyModifiedProperties ();

	}

	void AddEntry (int index)
	{
		Debug.Log ("Add");
		prop.InsertArrayElementAtIndex (index);
	}

	void DeleteEntry (int index)
	{
		Debug.Log ("Delete");

		bool choice = EditorUtility.DisplayDialog ("Warning", "You Are About To Delete, Proceed?", "Yes", "No");
		if (choice == true) {
			prop.DeleteArrayElementAtIndex (index);
		}
	}

	void ShiftEntryUp (int index)
	{
		prop.MoveArrayElement (index, index - 1);
	}

	void ShiftEntryDown (int index)
	{
		prop.MoveArrayElement (index, index + 1);
	}
}
