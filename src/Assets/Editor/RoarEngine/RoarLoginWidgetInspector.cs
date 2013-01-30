using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(RoarLoginWidget))]
public class RoarLoginWidgetInspector : RoarUIWidgetInspector
{
	private SerializedProperty enableOnAwake;
	private SerializedProperty whenToFetch;
	private SerializedProperty howOftenToFetch;
	
	private SerializedProperty loginLeftOffset;
	private SerializedProperty loginTopOffset;
	private SerializedProperty loginBoxSpacing;
	private SerializedProperty passwordBoxSpacing;
	private SerializedProperty labelWidth;
	private SerializedProperty statusWidth;
	private SerializedProperty fieldWidth;
	private SerializedProperty buttonWidth;
	private SerializedProperty buttonHeight;
	private SerializedProperty buttonSpacing;
	private SerializedProperty buttonStyle;
	private SerializedProperty boxStyle;
	private SerializedProperty loginLabelStyle;
	private SerializedProperty statusStyle;
	
	private SerializedProperty allowFacebook;
	private SerializedProperty facebookApplicationID;
	
	
	protected override void OnEnable ()
	{
		base.OnEnable ();
		
		enableOnAwake = serializedObject.FindProperty("enableOnAwake");
		
		loginLeftOffset = serializedObject.FindProperty("leftOffset");
		loginTopOffset = serializedObject.FindProperty("topOffset");

		loginBoxSpacing = serializedObject.FindProperty("loginBoxSpacing");
		passwordBoxSpacing = serializedObject.FindProperty("passwordBoxSpacing");
		labelWidth = serializedObject.FindProperty("labelWidth");
		statusWidth = serializedObject.FindProperty("statusWidth");
		fieldWidth = serializedObject.FindProperty("fieldWidth");
		buttonWidth = serializedObject.FindProperty("buttonWidth");
		buttonHeight = serializedObject.FindProperty("buttonHeight");
		buttonSpacing = serializedObject.FindProperty("buttonSpacing");
		buttonStyle = serializedObject.FindProperty("buttonStyle");
		boxStyle = serializedObject.FindProperty("boxStyle");
		loginLabelStyle = serializedObject.FindProperty("loginLabelStyle");
		statusStyle = serializedObject.FindProperty("statusStyle");
		
		allowFacebook = serializedObject.FindProperty("allowFacebookLogin");
		facebookApplicationID = serializedObject.FindProperty("facebookApplicationID");
	}
	
	protected override void DrawGUI()
	{
		base.DrawGUI();

		// rendering properties
		EditorGUILayout.PropertyField(enableOnAwake);
		Comment("Login presentation.");
		EditorGUILayout.PropertyField(loginLeftOffset);
		EditorGUILayout.PropertyField(loginTopOffset);
		EditorGUILayout.PropertyField(statusWidth);
		EditorGUILayout.PropertyField(labelWidth);
		EditorGUILayout.PropertyField(fieldWidth);
		EditorGUILayout.PropertyField(buttonWidth);
		EditorGUILayout.PropertyField(buttonHeight);
		Comment("Spacing");
		EditorGUILayout.PropertyField(loginBoxSpacing);
		EditorGUILayout.PropertyField(passwordBoxSpacing);
		EditorGUILayout.PropertyField(buttonSpacing);
		Comment("Styles");
		EditorGUILayout.PropertyField(statusStyle);
		EditorGUILayout.PropertyField(loginLabelStyle);
		EditorGUILayout.PropertyField(boxStyle);
		EditorGUILayout.PropertyField(buttonStyle);
		
		EditorGUILayout.PropertyField(allowFacebook, new GUIContent("Allow Facebook Login"), null );
		if(allowFacebook.boolValue)
		{
			Comment("Needed for oAuthToken login");
			EditorGUILayout.PropertyField(facebookApplicationID);
		}
	}
}
