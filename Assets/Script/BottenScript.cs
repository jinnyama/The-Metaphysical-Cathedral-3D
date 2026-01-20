using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BottenScript : MonoBehaviour
{
    public int quiznumber;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PushBotton()
    {
        switch (quiznumber)
        {
            case 0 when GameManager.Instance.Gamemode == "bookmode": 
                TextScript.Instance.choisetext = "晴れて";
                TextScript.Instance.diarytext[quiznumber].color = Color.yellow;
                break;
            case 1 when GameManager.Instance.Gamemode == "bookmode":
                TextScript.Instance.choisetext = "文字が書かれて";
                TextScript.Instance.diarytext[quiznumber].color = Color.yellow;
                break;
            case 2 when GameManager.Instance.Gamemode == "bookmode":
                TextScript.Instance.choisetext = "虹";
                TextScript.Instance.diarytext[quiznumber].color = Color.yellow;
                break;
            case 3 when GameManager.Instance.Gamemode == "bookmode":
                TextScript.Instance.choisetext = "聖典";
                TextScript.Instance.signtext.color = Color.yellow;
                break;
            case 4 when GameManager.Instance.Gamemode == "bookmode":
                TextScript.Instance.choisetext = "囲われている";
                TextScript.Instance.diarytext[quiznumber].color = Color.yellow;               
                break;
            case 5 when GameManager.Instance.Gamemode == "bookmode":
                TextScript.Instance.choisetext = "";
                TextScript.Instance.diarytext[quiznumber].color = Color.yellow;
                break;
            default:
                break;
        }
    }
    public void PushStartButton()
    {
        SceneManager.LoadScene("PlayScene");
    }

}
