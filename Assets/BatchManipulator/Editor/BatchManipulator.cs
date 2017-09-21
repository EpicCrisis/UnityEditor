using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class BatchManipulator : EditorWindow
{

	string objectName;
	bool selectFoldout;
	bool posFoldout;
	Vector3 positionChange;

	[MenuItem ("SuperTools/ Batch Manipulator")]
	static void ShowWindow ()
	{
		EditorWindow.GetWindow (typeof(BatchManipulator), false, "Batch Manipulator");
	}

	void OnGUI ()
	{
		//Selection tools
		selectFoldout = EditorGUILayout.Foldout (selectFoldout, "Selection Tool");
		if (selectFoldout == true) {
			EditorGUI.indentLevel++;

			objectName = EditorGUILayout.TextField ("Object Name", objectName);
			if (GUILayout.Button ("Select")) {
				SelectChildObjects ();
			}

			EditorGUI.indentLevel--;
		}

		//Position tools
		posFoldout = EditorGUILayout.Foldout (posFoldout, "Positioning Tool");
		if (posFoldout == true) {
			EditorGUI.indentLevel++;

			positionChange = EditorGUILayout.Vector3Field ("Position Change", positionChange);
			if (GUILayout.Button ("Move")) {
				MoveObjects ();
			}

			EditorGUI.indentLevel--;
		}
	}

	void SelectChildObjects ()
	{
		List <GameObject> objs = new List<GameObject> ();

		//Get all child transforms
		Transform[] transforms = Selection.activeTransform.GetComponentsInChildren<Transform> ();

		//Filter out the names and add the relevant transform into the list
		foreach (Transform t in transforms) {
			if (t.name.Contains (objectName)) {
				objs.Add (t.gameObject);
			}
		}

		//Convert the list to an array
		GameObject[] goArray = new GameObject[objs.Count];

		for (int i = 0; i < objs.Count; i++) {
			goArray [i] = objs [i];
		}

		//Pass the array to active selected objects
		Selection.objects = goArray;
	}

	void MoveObjects ()
	{
		Transform[] trans = Selection.GetTransforms (SelectionMode.Unfiltered);

		for (int i = 0; i < trans.Length; i++) {
			trans [i].position += positionChange;
		}

//		for (int i = 0; i < goArray.Length; i++) {
//			goArray [i].transform.position += positionChange;
//		}
	}
}
