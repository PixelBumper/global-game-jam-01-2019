using UnityEngine;
using UnityEngine.UI;
using XNode;

namespace HalfBlind.Nodes {
    [CreateNodeMenu("Actions/Load/" + nameof(LoadThreatImages), "Load", "Image", "Sprite")]
    public class LoadThreatImages : FlowNode {
        [Input] public string SpriteName;
        public Image ImageTarget;
        public Image IconImageTarget;

        public override void ExecuteNode() {
            var spriteName = GetInputValue(nameof(SpriteName), SpriteName);
            ImageTarget.sprite = Resources.Load<Sprite>(spriteName.ToLower()+"_threat");
            IconImageTarget.sprite = Resources.Load<Sprite>(spriteName.ToLower()+"_icon");
            if(ImageTarget.sprite == null) {
                Sprite transparent = Resources.Load<Sprite>("transparent");
                ImageTarget.sprite = transparent;
                IconImageTarget.sprite = transparent;
            }
        }

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port) {
            return null; // Replace this
        }
    }
}
