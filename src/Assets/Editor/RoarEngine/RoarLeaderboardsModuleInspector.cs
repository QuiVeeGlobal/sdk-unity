using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(RoarLeaderboardsModule))]
public class RoarLeaderboardsModuleInspector : RoarModuleInspector
{	
	private SerializedProperty defaultLeaderboardId;
	
	protected override void OnEnable()
	{
		base.OnEnable();
		showBoundsConstraintSettings = false;
		
		defaultLeaderboardId = serializedObject.FindProperty("defaultLeaderboardId");
	}

	protected override void DrawGUI()
	{
		base.DrawGUI();
		
		// 
		Comment("Leaderboards settings.");
		EditorGUILayout.PropertyField(defaultLeaderboardId);
	}
}
