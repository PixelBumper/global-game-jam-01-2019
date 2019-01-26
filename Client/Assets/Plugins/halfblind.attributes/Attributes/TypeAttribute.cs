using System;
using UnityEngine;

namespace HalfBlind.Attributes {
    public class TypeAttribute : PropertyAttribute {
        public readonly Type MyType;

        public TypeAttribute(Type type) {
            MyType = type;
        }
    }
}
