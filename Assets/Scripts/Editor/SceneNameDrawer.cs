using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SceneNameAttribute))]
public class SceneNameDrawer : PropertyDrawer {

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

		if (property.propertyType != SerializedPropertyType.String) {
			EditorGUI.LabelField(position, label.text, "Use SceneName with strings.");
			return;
		}

		SceneAsset oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(property.stringValue);

		property.serializedObject.Update();
		EditorGUI.BeginChangeCheck();

		SceneAsset newScene = EditorGUI.ObjectField(position, label, oldScene, typeof(SceneAsset), false) as SceneAsset;

		if (EditorGUI.EndChangeCheck()) {
			property.stringValue = AssetDatabase.GetAssetPath(newScene);
		}
		property.serializedObject.ApplyModifiedProperties();
	}
}
