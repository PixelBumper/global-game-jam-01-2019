using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace HalfBlind.Attributes {
    [CustomPropertyDrawer(typeof(TypeAttribute))]
    public class TypeDrawer : PropertyDrawer {
        // These constants describe the height of the help box and the text field.
        const int helpHeight = 30;
        const int textHeight = 16;

        string[] values;
        List<Type> allSubTypes = new List<Type>();

        // Provide easy access to the RegexAttribute for reading information from it.
        TypeAttribute typeAttribute { get { return ((TypeAttribute)attribute); } }// Here you must define the height of your property drawer. Called by Unity.
        public override float GetPropertyHeight(SerializedProperty prop, GUIContent label) {
            if (IsValid(prop))
                return base.GetPropertyHeight(prop, label);
            else
                return base.GetPropertyHeight(prop, label) + helpHeight;
        }

        // Here you can define the GUI for your property drawer. Called by Unity.
        public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label) {
            if (allSubTypes.Count == 0) {
                allSubTypes = TypesUtils.GetAllNonAbstractSubtypesOf(typeAttribute.MyType).ToList();
                allSubTypes.AddRange(TypesUtils.GetAllTypesImplementing(typeAttribute.MyType).ToList());
                values = allSubTypes.Select(o => o.Name).ToArray();
            }

            // Adjust height of the text field
            Rect textFieldPosition = position;
            textFieldPosition.height = textHeight;
            DrawPopup(textFieldPosition, prop, label);

            // Adjust the help box position to appear indented underneath the text field.
            Rect helpPosition = EditorGUI.IndentedRect(position);
            helpPosition.y += textHeight;
            helpPosition.height = helpHeight;
            DrawHelpBox(helpPosition, prop);
        }

        void DrawPopup(Rect position, SerializedProperty prop, GUIContent label) {
            // Draw the text field control GUI.
            EditorGUI.BeginChangeCheck();
            var resultType = allSubTypes.Find(x => x.AssemblyQualifiedName == prop.stringValue);

            var selectedIndex = resultType != null ? System.Array.IndexOf(values, resultType.Name) : 0;
            selectedIndex = selectedIndex < 0 ? 0 : selectedIndex;
            selectedIndex = EditorGUI.Popup(position, prop.displayName, selectedIndex, values);

            if (EditorGUI.EndChangeCheck()) {
                resultType = allSubTypes.Find(x => x.Name == values[selectedIndex]);
            }

            prop.stringValue = resultType.AssemblyQualifiedName;
        }

        private void DrawTextField(Rect position, SerializedProperty prop, GUIContent label) {
            // Draw the text field control GUI.
            EditorGUI.BeginChangeCheck();
            string value = EditorGUI.TextField(position, label, prop.stringValue);
            if (EditorGUI.EndChangeCheck()) {
                prop.stringValue = value;
            }
        }

        private void DrawHelpBox(Rect position, SerializedProperty prop) {
            // No need for a help box if the pattern is valid.
            if (IsValid(prop))
                return;

            EditorGUI.HelpBox(position, "error", MessageType.Error);
        }

        // Test if the propertys string value matches the regex pattern.
        private bool IsValid(SerializedProperty prop) {
            return true;// Regex.IsMatch(prop.stringValue, regexAttribute.pattern);
        }
    }
}
