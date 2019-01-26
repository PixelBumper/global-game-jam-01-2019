namespace HalfBlind.ScriptableVariables {
    using UnityEngine;

    [CreateAssetMenu(fileName = "MyString", menuName = "Variables/String")]
    public class GlobalString : ScriptableVariable<string> {
        public override void FromString(string value) {
            Value = value;
        }
    }
}
