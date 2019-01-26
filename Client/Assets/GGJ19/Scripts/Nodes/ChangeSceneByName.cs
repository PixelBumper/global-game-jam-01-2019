using UnityEngine.SceneManagement;
using XNode;

[CreateNodeMenu(nameof(ChangeSceneByName), "Scene", "Change", "Load")]
public class ChangeSceneByName : FlowNode
{
    [Input]
    public string sceneName;

    public override object GetValue(NodePort port)
    {
        return null;
    }

    public override void ExecuteNode()
    {
        var inputSceneName = GetInputValue(nameof(sceneName), sceneName);
        SceneManager.LoadScene(inputSceneName);
    }
}
