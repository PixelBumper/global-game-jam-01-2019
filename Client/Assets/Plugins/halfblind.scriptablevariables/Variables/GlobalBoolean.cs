namespace HalfBlind.ScriptableVariables {
    using UnityEngine;

    [CreateAssetMenu(fileName = "GlobalBoolean", menuName = "Variables/Boolean")]
    public class GlobalBoolean : ScriptableVariable<bool> {
        public override void FromString(string value) {
            bool result;
            if (bool.TryParse(value, out result)) {
                Value = result;
            }
        }
    }
}
