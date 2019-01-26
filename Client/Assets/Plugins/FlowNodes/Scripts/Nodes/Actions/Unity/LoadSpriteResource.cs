﻿using UnityEngine;
using XNode;

namespace HalfBlind.Nodes {
    [CreateNodeMenu("Actions/Load/" + nameof(LoadSpriteResource), "Load", "Image", "Sprite")]
    public class LoadSpriteResource : FlowNode {
        [Input] public string SpriteName;
        [Output] public Sprite Result;

        public override void ExecuteNode() {
            var spriteName = GetInputValue(nameof(SpriteName), SpriteName);
            Result = Resources.Load<Sprite>(spriteName.ToLower());
        }

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port) {
            if(port.fieldName == nameof(Result)) {
                return Result;
            }
            return null; // Replace this
        }
    }
}
