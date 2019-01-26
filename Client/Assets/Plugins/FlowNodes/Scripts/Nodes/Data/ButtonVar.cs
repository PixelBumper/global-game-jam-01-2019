﻿using UnityEngine.UI;
using XNode;

namespace Data.UnityNative {
    [CreateNodeMenu("Variables/ButtonVar", "Button", "Variable")]
    public class ButtonVar : MonoNode {
        public Button Value;
        [Output] public Button Output;

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port) {
            if (port.fieldName == nameof(Output)) {
                return Value;
            }
            return null; // Replace this
        }
    }
}

