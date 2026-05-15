using UnityEngine;
using UnityEngine.UI;

public class GuidanceScript : MonoBehaviour
{
    public static GuidanceScript instance;
    public string guidanceText; // ガイダンステキストを格納する変数
    public Text guidanceUIText; // UI上のテキストコンポーネント
    public string previousGuidanceText; // 前回のガイダンステキストを格納する変数

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (guidanceText != previousGuidanceText.text)
        {
            guidanceUIText.text = guidanceText;
            previousGuidanceText.text = guidanceText;
        }
    }
}
