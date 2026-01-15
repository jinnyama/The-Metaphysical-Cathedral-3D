using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    public TextMeshProUGUI textUI;

    public TextManager instance; 

    private string[] texts;
    private int index;
    private bool isActive;
    void Start()
    {
        instance = this;
        textUI.gameObject.SetActive(false);
        //isActive = false;
    }

    void Update()
    {
        if (!isActive) return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            index++;

            if (index < texts.Length)
            {
                textUI.text = texts[index];
            }
            else
            {
                EndText();
            }
        }
    }

    public void StartText(TextScenario scenario)
    {
        texts = scenario.texts;
        index = 0;
        isActive = true;

        textUI.gameObject.SetActive(true);
        textUI.text = texts[0];
    }

    void EndText()
    {
        isActive = false;
        textUI.gameObject.SetActive(false);
    }
}
