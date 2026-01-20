using UnityEngine;

public class HintBook : MonoBehaviour
{
    public TextScenario scenario;
    public TextManager textManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textManager.StartText(scenario);
        }
    }
}
