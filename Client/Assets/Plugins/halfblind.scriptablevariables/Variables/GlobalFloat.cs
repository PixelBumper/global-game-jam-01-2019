namespace HalfBlind.ScriptableVariables {
    using UnityEngine;

    [CreateAssetMenu(fileName = "MyFloat", menuName = "Variables/Float")]
    public class GlobalFloat : ScriptableVariable<float> {
        public override void FromString(string value) {
            float result;
            if (float.TryParse(value, out result)) {
                Value = result;
            }
        }
    }
}
