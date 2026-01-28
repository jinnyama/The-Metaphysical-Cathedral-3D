using UnityEngine;


[CreateAssetMenu(menuName = "Text/Scenario")]
public class TextScenario : ScriptableObject
{//シナリオ用ScriptableObject
    [TextArea(3, 10)]
    public string[] texts;
    public int changeableindex;
}
