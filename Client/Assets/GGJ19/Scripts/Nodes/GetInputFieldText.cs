using UnityEngine.UI;
using XNode;

namespace GGJ19.Scripts.Nodes
{
    [CreateNodeMenu(nameof(GetInputFieldText), "input", "field", "text")]
    public class GetInputFieldText : FlowNode
    {
        [Input]
        public InputField inputField;

        [Output]
        public string inputFieldText;
        
        public override object GetValue(NodePort port)
        {
            return null;
        }

        public override void ExecuteNode()
        {
            
        }

        public override void TriggerFlow()
        {
            if(inputField != null && inputField.text != null && inputField.text != "")
            {
                string text = inputField.text;
                FlowUtils.TriggerFlow(Outputs, nameof(text));
            }
        }
    }
}