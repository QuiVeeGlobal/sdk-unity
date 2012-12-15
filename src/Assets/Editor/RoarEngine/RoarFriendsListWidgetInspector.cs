using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(RoarFriendsListWidget))]
public class RoarFriendsListWidgetInspector : RoarUIWidgetInspector
{
	
	private SerializedProperty entryBounds;
	private SerializedProperty entrySpacing;
	
	protected override void OnEnable ()
	{
		base.OnEnable ();
		
		entryBounds = serializedObject.FindProperty("entryBounds");
		entrySpacing = serializedObject.FindProperty("entrySpacing");
	}
	
	protected override void DrawGUI()
	{
		base.DrawGUI();
		
		// rendering properties
		Comment("Friends item presentation.");
		EditorGUILayout.PropertyField(entryBounds);
		EditorGUILayout.PropertyField(entrySpacing);
	}
}
