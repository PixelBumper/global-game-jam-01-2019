using GGJ19.Scripts.GameLogic;
using XNode;

namespace GGJ19.Scripts.Nodes
{
    [CreateNodeMenu(nameof(GetThisPlayerEmoji), "emoji")]
    public class GetThisPlayerEmoji : MonoNode
    {
        [Input] public PlayerEmojiProvider playerEmojiProvider;
        [Output] public string emojiString;
        
        public override object GetValue(NodePort port)
        {
            if (port.fieldName == nameof(emojiString))
            {
                var theEmojiProvider = GetInputValue(nameof(playerEmojiProvider), playerEmojiProvider);
                return theEmojiProvider != null ? theEmojiProvider.thisGlobalString.Value : null;
            }

            return null;
        }
    }
}