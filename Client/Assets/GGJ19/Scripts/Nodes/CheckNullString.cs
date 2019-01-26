using GGJ19.Scripts.GameLogic;
using UnityEngine.UI;
using XNode;

namespace GGJ19.Scripts.Nodes
{
    [CreateNodeMenu(nameof(CheckNullString), "check", "string", "null")]
    public class CheckNullString : FlowNode
    {
        [Input]
        public string inputString;

        [Output]
        public string validString;
        
        public override object GetValue(NodePort port)
        {
            return null;
        }

        public override void ExecuteNode()
        {
            
        }

        public override void TriggerFlow()
        {
            if (!string.IsNullOrEmpty(inputString))
            {
                FlowUtils.TriggerFlow(Outputs, nameof(inputString));
            }
        }
    }
}