using UnityEditor;
using UnityEngine;

namespace HalfBlind.Attributes {
    [CustomPropertyDrawer(typeof(InterfaceAttribute))]
    public class InterfaceDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label) {
            var interfaceAttribute = attribute as InterfaceAttribute;
            if (!interfaceAttribute.MyType.IsInterface) {
                EditorGUI.HelpBox(position, "Interface Attribute requires to have a type of interface", MessageType.Error);
                return;
            }

            Color previousColor = GUI.backgroundColor;
            GUI.backgroundColor = Color.yellow;
            var previousObject = prop.objectReferenceValue;
            EditorGUI.ObjectField(position, prop);
            GUI.backgroundColor = previousColor;
            if(prop.objectReferenceValue != null) {
                var gameobject = prop.objectReferenceValue as GameObject;
                if (gameobject) {
                    prop.objectReferenceValue = gameobject.GetComponent(interfaceAttribute.MyType);
                }

                if (!interfaceAttribute.MyType.IsAssignableFrom(prop.objectReferenceValue.GetType())) {
                    prop.objectReferenceValue = previousObject;
                }
            }
        }
    }
}
