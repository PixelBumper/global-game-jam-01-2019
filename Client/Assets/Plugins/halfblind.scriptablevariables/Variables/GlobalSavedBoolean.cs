namespace HalfBlind.ScriptableVariables {
    using HalfBlind.Attributes;
    using HalfBlind.SaveUtils;
    using UnityEngine;

    [CreateAssetMenu(fileName = "SavedBoolean", menuName = "Variables/SavedBoolean")]
    public abstract class GlobalSavedBoolean : GlobalBoolean {
        [UniqueIdentifier]
        [SerializeField]
        private string _saveKey;

        public abstract ISave GetSaveHandler();

        public override bool Value {
            get {
                var outValue = false;
                if (Application.isPlaying) {
                    var _saveSystem = GetSaveHandler();
                    if (_saveSystem != null && _saveSystem.Load<bool>(_saveKey, out outValue)) {
                        return outValue;
                    }
                }

                return _initialValue;
            }
            set {
                if (Application.isPlaying) {
                    var _saveSystem = GetSaveHandler();
                    if (_saveSystem != null) {
                        _saveSystem.Save<bool>(_saveKey, value);
                    }
                }
            }
        }
    }
}
