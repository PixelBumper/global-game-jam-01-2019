using UnityEditor;
using UnityEngine;

namespace HalfBlind.Attributes {
    [CustomPropertyDrawer(typeof(GuidAttribute))]
    public class GuidReferenceDrawer : PropertyDrawer {
        // Here you can define the GUI for your property drawer. Called by Unity.
        public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label) {
            var guidAttribute = attribute as GuidAttribute;
            var objectType = guidAttribute.AttributeConstrain != null ? guidAttribute.AttributeConstrain : typeof(UnityEngine.Object);
            var guid = prop.stringValue;
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var currentObject = AssetDatabase.LoadAssetAtPath<Object>(path);
            var result = EditorGUI.ObjectField(position, currentObject, objectType, false);
            prop.stringValue = result != null ? AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(result)) : string.Empty;
        }
    }
}
