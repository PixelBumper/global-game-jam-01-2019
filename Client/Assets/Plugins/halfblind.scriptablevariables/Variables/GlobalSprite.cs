namespace HalfBlind.ScriptableVariables {
    using UnityEngine;

    [CreateAssetMenu(fileName = "GlobalSprite", menuName = "Variables/Sprite")]
    public class GlobalSprite : ScriptableVariable<Sprite> {
        public override void FromString(string value) {
        }
    }
}
