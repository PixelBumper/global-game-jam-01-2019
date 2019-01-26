using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace HalfBlind.Attributes {
    [CustomPropertyDrawer(typeof(StringButtonAttribute))]
    public class StringButtonDrawer : PropertyDrawer {
        StringButtonAttribute _attribute { get { return attribute as StringButtonAttribute; } }

        // Here you can define the GUI for your property drawer. Called by Unity.
        public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label) {
            if (_attribute.VisibilityType == StringButtonAttribute.Visibility.OnlyEditorMode && Application.isPlaying) {
                return;
            }

            if (_attribute.VisibilityType == StringButtonAttribute.Visibility.OnlyPlayMode && !Application.isPlaying) {
                return;
            }

            position.height += _attribute.ExtraSize;
            if (GUI.Button(position, _attribute.ActionName)) {
                var propertyOwner = GetParent(prop);
                var propertyOwnerType = propertyOwner.GetType();
                var loadRegionDataFunc = propertyOwnerType.GetMethod(_attribute.ActionName,
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                loadRegionDataFunc.Invoke(propertyOwner, new object[] { });
            }
        }

        public static object GetParent(SerializedProperty prop) {
            var path = prop.propertyPath.Replace(".Array.data[", "[");
            object obj = prop.serializedObject.targetObject;
            var elements = path.Split('.');
            foreach (var element in elements.Take(elements.Length - 1)) {
                if (element.Contains("[")) {
                    var elementName = element.Substring(0, element.IndexOf("["));
                    var index =
                        Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                    obj = GetValue(obj, elementName, index);
                } else {
                    obj = GetValue(obj, element);
                }
            }
            return obj;
        }

        public static object GetValue(object source, string name) {
            if (source == null)
                return null;
            var type = source.GetType();
            var f = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (f == null) {
                var p = type.GetProperty(name,
                    BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (p == null)
                    return null;
                return p.GetValue(source, null);
            }
            return f.GetValue(source);
        }

        public static object GetValue(object source, string name, int index) {
            var enumerable = GetValue(source, name) as IEnumerable;
            var enm = enumerable.GetEnumerator();
            while (index-- >= 0)
                enm.MoveNext();
            return enm.Current;
        }
    }
}
