using UnityEngine;

namespace HalfBlind.Attributes {
    public class StringButtonAttribute : PropertyAttribute {
        public enum Visibility {
            OnlyPlayMode,
            OnlyEditorMode,
            PlayAndEditorMode,
        }

        public string ActionName { get; }
        public int ExtraSize { get; }
        public Visibility VisibilityType { get; }

        public StringButtonAttribute(string actionName, int extraSize, Visibility visibility = Visibility.PlayAndEditorMode) {
            ActionName = actionName;
            ExtraSize = extraSize;
            VisibilityType = visibility;
        }
    }
}
