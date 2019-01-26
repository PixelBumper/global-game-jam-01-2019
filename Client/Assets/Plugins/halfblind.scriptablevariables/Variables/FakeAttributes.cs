#if !ODIN
using System;

namespace Sirenix.OdinInspector {
    public class HideInPlayMode : Attribute { }
    public class HideInEditorMode : Attribute { }
    public class ShowInInspector : Attribute { }
    public class Button : Attribute { }
    public class HideLabel : Attribute { }
    public class ProgressBar : Attribute {
        public ProgressBar(int myint, string mystring) {
        }
    }
}
#endif