namespace HalfBlind.ScriptableVariables {
    using HalfBlind.Attributes;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "OnGameEvent", menuName = "GameEvents/ScriptableEvent")]
    public class ScriptableGameEvent : ScriptableObject {
        private HashSet<Action> _callbacks = new HashSet<Action>();
        [SerializeField, StringButton(nameof(SendEvent), 50, StringButtonAttribute.Visibility.OnlyPlayMode)]
        private string _hiddenButton;

        private void OnEnable() {
            _callbacks.Clear();
        }

        private void OnDisable() {
            _callbacks.Clear();
        }

        public virtual void SendEvent() {
            foreach (var callback in _callbacks) {
                callback();
            }
        }

        public void AddListener(Action callback) {
            _callbacks.Add(callback);
        }

        public void RemoveListener(Action callback) {
            _callbacks.Remove(callback);
        }
    }
}
