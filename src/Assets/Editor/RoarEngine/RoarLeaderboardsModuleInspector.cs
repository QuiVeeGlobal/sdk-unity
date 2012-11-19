using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(RoarLeaderboardsModule))]
public class RoarLeaderboardsModuleInspector : RoarModuleInspector
{	
	private SerializedProperty defaultLeaderboard;
	
	protected override void OnEnable()
	{
		base.OnEnable();
		showBoundsConstraintSettings = false;
		
		defaultLeaderboard = serializedObject.FindProperty("defaultLeaderboard");
	}

	protected override void DrawGUI()
	{
		base.DrawGUI();
		
		// 
		Comment("Leaderboards settings.");
		EditorGUILayout.PropertyField(defaultLeaderboard);
	}
}
