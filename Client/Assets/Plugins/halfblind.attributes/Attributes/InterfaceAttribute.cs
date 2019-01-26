using System;
using UnityEngine;

namespace HalfBlind.Attributes {
    public class InterfaceAttribute : PropertyAttribute {
        public readonly Type MyType;

        public InterfaceAttribute(Type type) {
            MyType = type;
        }
    }
}
