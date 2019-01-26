namespace HalfBlind.ScriptableVariables {
    using UnityEngine;

    public abstract class ScriptableVariable : ScriptableObject {
        public abstract object GetValue();
        public abstract void FromString(string value);
    }

    public abstract class ScriptableVariable<T> : ScriptableVariable {
        [SerializeField, Sirenix.OdinInspector.HideInPlayMode]
        protected T _initialValue;

        private T _runtimeValue;

        [Sirenix.OdinInspector.ShowInInspector, Sirenix.OdinInspector.HideInEditorMode]
        public virtual T Value {
            get { return _runtimeValue; }
            set { _runtimeValue = value; }
        }

        protected virtual void OnEnable() {
            Value = _initialValue;
        }

        public override object GetValue() {
            return Value;
        }
    }
}
