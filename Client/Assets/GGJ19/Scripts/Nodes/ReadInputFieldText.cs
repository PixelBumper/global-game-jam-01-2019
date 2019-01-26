using GGJ19.Scripts.GameLogic;
using UnityEngine;
using UnityEngine.UI;
using XNode;

namespace GGJ19.Scripts.Nodes
{
    [CreateNodeMenu(nameof(ReadInputFieldText), "Unique", "Identifier")]
    public class ReadInputFieldText  : MonoNode
    {
        [Input]
        public InputField inputField;

        [Output]
        public string fieldText;

        public override object GetValue(NodePort port)
        {
            if(inputField != null)
            {
                return inputField.text;
            }

            return "";
        }

    }
}