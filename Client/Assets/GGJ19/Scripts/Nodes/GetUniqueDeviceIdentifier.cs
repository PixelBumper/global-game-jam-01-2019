using GGJ19.Scripts.GameLogic;
using XNode;

namespace GGJ19.Scripts.Nodes
{
    [CreateNodeMenu(nameof(GetUniqueDeviceIdentifier), "Unique", "Identifier")]
    public class GetUniqueDeviceIdentifier : MonoNode
    {
        [Output]
        public string deviceId;
        
        public override object GetValue(NodePort port)
        {
            return port.fieldName == nameof(deviceId) ? GameLogicManager.instance.PlayerId : null;
        }
    }
}