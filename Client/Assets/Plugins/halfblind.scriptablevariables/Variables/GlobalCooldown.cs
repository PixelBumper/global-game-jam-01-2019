using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace HalfBlind.ScriptableVariables {
    using HalfBlind.Attributes;
    using HalfBlind.SaveUtils;

    [CreateAssetMenu(fileName = "Global Cooldown", menuName = "Variables/GlobalCooldown")]
    public abstract class GlobalCooldown : ScriptableObject {
        [UniqueIdentifier]
        [SerializeField]
        private string _saveKey;
        [SerializeField, HideInPlayMode]
        private long _InitialCooldownInSeconds;

        [ShowInInspector, HideInEditorMode]
        public long CooldownInSeconds { get; set; }

        public Action OnCooldownStarted;

        public string SaveKey { get { return _saveKey; } }

        protected virtual void OnEnable() {
            CooldownInSeconds = _InitialCooldownInSeconds;
        }

        #region Inspector
        private double RuntimeCooldownInSecondsDouble => CooldownInSeconds;
        [ShowInInspector, HideInEditorMode, HideLabel, ProgressBar(0, "RuntimeCooldownInSecondsDouble")]
        private double RemainingSeconds { get { return GetRemainingSecondsTime(); } }
        #endregion

        public abstract ISave GetSaveHandler();

        public double GetRemainingSecondsTime() {
            var saveSystem = GetSaveHandler();
            if(saveSystem == null) { return 0; }
            long startTimeTicks = GetSavedStartTimeTicks(saveSystem, _saveKey, (double)CooldownInSeconds);
            double elapsedSeconds = (DateTime.Now - new DateTime(startTimeTicks)).TotalSeconds;
            var diffTime = CooldownInSeconds - elapsedSeconds;
            return diffTime > 0 ? diffTime : 0;
        }

        [HideInEditorMode]
        [Button]
        public void StartCooldown() {
            if (IsOnCooldown()) { return; }
            var startTimeTicks = SaveStartTimeTicks(GetSaveHandler(), _saveKey, CooldownInSeconds);
            OnCooldownStarted?.Invoke();
            Debug.Log($"{name} Cooldown Started at:{new DateTime(startTimeTicks)} duration: {CooldownInSeconds}s");
        }

        [HideInEditorMode]
        [Button]
        public bool IsOnCooldown() {
            var startTimeTicks = GetSavedStartTimeTicks(GetSaveHandler(), _saveKey, (double)CooldownInSeconds);
            var isOnCooldown = IsOnCooldown(startTimeTicks, CooldownInSeconds);
            return isOnCooldown;
        }

        public static long SaveStartTimeTicks(ISave saveSystem, string saveKey, long cooldownSeconds) {
            long startTimeTicks = GetSavedStartTimeTicks(saveSystem, saveKey, (double)cooldownSeconds);
            var isOnCooldown = IsOnCooldown(startTimeTicks, cooldownSeconds);
            if (isOnCooldown) {
                Debug.Log($"{nameof(isOnCooldown)}:{isOnCooldown} cooldown started: {new DateTime(startTimeTicks)} and lasts {cooldownSeconds}");
                return startTimeTicks;
            }

            saveSystem.Save<long>(saveKey, DateTime.Now.Ticks);
            return startTimeTicks;
        }

        public static long GetSavedStartTimeTicks(ISave saveSystem, string saveKey, double cooldownSeconds) {
            long lastTime;
            saveSystem.Load<long>(saveKey, out lastTime);
            return lastTime;
        }

        public static bool IsOnCooldown(long startTimeTicks, long cooldownSeconds) {
            TimeSpan timeSpan = DateTime.Now - new DateTime(startTimeTicks);
            return timeSpan.TotalSeconds < cooldownSeconds;
        }
    }
}
