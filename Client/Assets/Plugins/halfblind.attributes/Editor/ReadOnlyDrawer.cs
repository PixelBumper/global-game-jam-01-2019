using UnityEditor;
using UnityEngine;

namespace HalfBlind.Attributes {
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label) {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, prop);
            GUI.enabled = true;
        }
    }
}
