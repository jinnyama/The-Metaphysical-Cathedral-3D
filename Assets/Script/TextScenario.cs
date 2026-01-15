using UnityEngine;

[CreateAssetMenu(menuName = "Text/Scenario")]
public class TextScenario : ScriptableObject
{
    [TextArea(3, 10)]
    public string[] texts;
    public string scenarioTitle;
}
