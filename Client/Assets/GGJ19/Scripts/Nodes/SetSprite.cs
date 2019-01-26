using UnityEngine;
using XNode;

namespace GGJ19.Scripts.Nodes
{
    [CreateNodeMenu(nameof(SetSprite), "Sprite")]
    public class SetSprite : FlowNode
    {
        [Input] public SpriteRenderer Target;
        [Input] public Sprite MySprite;

        public override void ExecuteNode() {
            var target = GetInputValue(nameof(Target), Target);
            var sprite = GetInputValue(nameof(MySprite), MySprite);
            target.sprite = sprite;
        }

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port) {
            return null; // Replace this
        }
    }
}