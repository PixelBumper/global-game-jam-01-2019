﻿using UnityEngine.UI;
using XNode;

namespace Events.UnityNative {
    [CreateNodeMenu("UI/Events/" + nameof(OnButtonClicked), "Button", "Clicked")]
    public class OnButtonClicked : EventNode {
        [Input] public Button MyButton;

        // Use this for initialization
        protected override void Init() {
            base.Init();
            var thebutton = GetInputValue(nameof(MyButton), MyButton);
            thebutton?.onClick.AddListener(OnMyButtonClicked);
        }

        private void OnMyButtonClicked() {
            TriggerFlow();
        }

        private void OnDestroy() {
            var thebutton = GetInputValue(nameof(MyButton), MyButton);
            thebutton?.onClick.RemoveListener(OnMyButtonClicked);
        }

        public override object GetValue(NodePort port) {
            return null;
        }
    }
}
