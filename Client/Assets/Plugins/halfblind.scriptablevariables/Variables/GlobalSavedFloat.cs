namespace HalfBlind.ScriptableVariables {
    using HalfBlind.Attributes;
    using HalfBlind.SaveUtils;
    using UnityEngine;

    [CreateAssetMenu(fileName = "SavedFloat", menuName = "Variables/Saved/Float")]
    public abstract class GlobalSavedFloat : GlobalFloat {
        [UniqueIdentifier]
        [SerializeField]
        private string _saveKey;

        public abstract ISave GetSaveHandler();

        public override float Value {
            get {
                var outValue = 0.0f;
                if (Application.isPlaying) {
                    var _saveSystem = GetSaveHandler();
                    if(_saveSystem != null && _saveSystem.Load<float>(_saveKey, out outValue)) {
                        return outValue;
                    }
                }

                return _initialValue;
            }
            set {
                if (Application.isPlaying) {
                    var _saveSystem = GetSaveHandler();
                    if(_saveSystem != null) {
                        _saveSystem.Save<float>(_saveKey, value);
                    }
                }
            }
        }
    }
}
