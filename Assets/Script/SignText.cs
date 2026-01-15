using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SignText : MonoBehaviour
{
    //playerが看板に触れたときにテキストウィンドウに表示する用
    
    //private string[] signText={"空が　　　いる","　　　　　　　いる祠",　"がかかっている","檻に　　　　いる"};
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private string words = "りんご,バナナ,みかん";
    private string[] wordArray;
    [SerializeField] Text text;
    private int count;

    public TextMeshProUGUI textUI;

    private string[] currentTexts;
    private int index;
    private bool isActive;

    void Update()
    {
        // if(Input.GetMouseButtonDown(0))
        // {
        //     SetText();
        //     count++;
        // }
        if (!isActive) return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            index++;
            if (index < currentTexts.Length)
            {
                textUI.text = currentTexts[index];
            }
            else
            {
                EndText();
            }
        }
    }

    public void StartText(TextScenario scenario)
    {
        currentTexts = scenario.texts;
        index = 0;
        isActive = true;
        textUI.gameObject.SetActive(true);
        textUI.text = currentTexts[0];
    }

    void EndText()
    {
        isActive = false;
        textUI.gameObject.SetActive(false);
    }
    // void SetText()
    // {
    //     if(count<3)
    //     {
    //         wordArray = words.Split(',');
    //         text.text = text.text + wordArray[count] + "\n";
    //     }
 
    // }
}
