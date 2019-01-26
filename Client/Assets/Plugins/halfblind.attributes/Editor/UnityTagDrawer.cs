using UnityEditor;
using UnityEngine;

namespace HalfBlind.Attributes
{
    [CustomPropertyDrawer(typeof(UnityTagAttribute))]
    public class UnityTagDrawer : PropertyDrawer
    {
        // These constants describe the height of the help box and the text field.
        const int helpHeight = 30;
        const int textHeight = 16;

        // Provide easy access to the RegexAttribute for reading information from it.
        UnityTagAttribute castedAttribute { get { return ((UnityTagAttribute)attribute); } }// Here you must define the height of your property drawer. Called by Unity.
        public override float GetPropertyHeight(SerializedProperty prop, GUIContent label)
        {
            if (IsValid(prop))
            {
                return base.GetPropertyHeight(prop, label);
            }
            else
            {
                return base.GetPropertyHeight(prop, label) + helpHeight;
            }
        }

        // Here you can define the GUI for your property drawer. Called by Unity.
        public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
        {
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

        void DrawPopup(Rect position, SerializedProperty prop, GUIContent label)
        {
            // Draw the text field control GUI.
            EditorGUI.BeginChangeCheck();
            var tagStr = prop.stringValue;
            tagStr = tagStr == string.Empty ? "Untagged" : tagStr;
            tagStr = EditorGUI.TagField(position, prop.displayName, tagStr);
            if (EditorGUI.EndChangeCheck())
            {
                prop.stringValue = tagStr;
            }
        }

        void DrawHelpBox(Rect position, SerializedProperty prop)
        {
            // No need for a help box if the pattern is valid.
            if (IsValid(prop))
            {
                return;
            }

            EditorGUI.HelpBox(position, "error", MessageType.Error);
        }

        // Test if the propertys string value matches the regex pattern.
        bool IsValid(SerializedProperty prop)
        {
            return true;
        }
    }
}
