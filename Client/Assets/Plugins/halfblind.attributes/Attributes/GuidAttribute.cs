using System;
using UnityEngine;

namespace HalfBlind.Attributes {
    public class GuidAttribute : PropertyAttribute {
        public Type AttributeConstrain { get; private set; }
        public GuidAttribute(Type constrain = null) {
            AttributeConstrain = constrain;
        }
    }
}
