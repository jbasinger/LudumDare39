using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnvironmentBuilder))]
public class EnvironmentBuilderEditor : Editor {

	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();
//		EnvironmentBuilder env = (EnvironmentBuilder)target;
//		if (GUILayout.Button ("Build Environment")) {
//			env.BuildEnvironment ();
//		}
//		if (GUILayout.Button ("Clear Environment")) {
//			env.ClearEnvironment ();
//		}
	}
}