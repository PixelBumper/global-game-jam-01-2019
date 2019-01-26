using GGJ19.Scripts.GameLogic;
using XNode;

namespace GGJ19.Scripts.Nodes
{
    [CreateNodeMenu(nameof(GetUniqueDeviceIdentifier), "Unique", "Identifier")]
    public class GetUniqueDeviceIdentifier : FlowNode
    {
        [Output]
        public string deviceId;
        
        public override object GetValue(NodePort port)
        {
            return null;
        }

        public override void ExecuteNode()
        {
            
        }

        public override void TriggerFlow()
        {
            deviceId = GameLogicManager.instance.PlayerId;
            FlowUtils.TriggerFlow(Outputs, nameof(deviceId));
        }
    }
}