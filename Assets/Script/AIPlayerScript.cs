using UnityEngine;
using UnityEngine.UI;

public class AIPlayerScript : MonoBehaviour
{
    //AIに書かせたPlayerScript最適化版()
    // ===============================
    // 基本参照
    // ===============================
    [SerializeField] Camera fpsCam;
    [SerializeField] float distance = 0.8f;

    public static AIPlayerScript instance;

    // ===============================
    // 視線・インタラクト
    // ===============================
    public GameObject seeObjects;
    private Outline currentOutline;

    private bool isGetItem;
    private bool isTereport;
    private bool isTextWindowActive;
    public bool canMove = false;

    // ===============================
    // インベントリ
    // ===============================
    public int maxitemCount = 5;
    public GameObject[] itemObjects;
    public Image[] itemsrot;
    public Text[] itemsrotChildrenText;

    private int itemCounts;
    private int activeItemIndex;
    private int maxActiveItemIndex;

    // ===============================
    // UI・テキスト
    // ===============================
    public Image TextWindow;
    public GameObject bookchildrenBotten;

    public TextScenario scenario;
    public TextScenario[] hintscenarios;
    public TextManager textManager;

    // ===============================
    // その他
    // ===============================
    public float sensitivity = 1f;
    private float mouseScrollDelta;

    private Vector3 PlayerPosition;
    private Vector3 initialPosition;

    // ===============================
    // Start
    // ===============================
    void Start()
    {
        instance = this;

        itemObjects = new GameObject[maxitemCount];
        itemsrotChildrenText = new Text[itemsrot.Length];

        for (int i = 0; i < itemsrot.Length; i++)
        {
            itemsrot[i].color = new Color(1, 1, 1, 0);
            itemsrotChildrenText[i] = itemsrot[i].GetComponentInChildren<Text>();
            itemsrotChildrenText[i].color = new Color(0, 0, 0, 0);

            if (itemsrot[i].TryGetComponent(out Outline o))
            {
                
            }
        }

        PlayerPosition = transform.position;
        initialPosition = transform.position;

        scenario = hintscenarios[0];
        TextWindow.enabled = true;
        textManager.StartText(scenario);

        bookchildrenBotten.SetActive(false);
    }

    // ===============================
    // Update
    // ===============================
    void Update()
    {
        HandleReset();
        HandleRaycast();
        HandleInteractInput();
        HandleInventoryScroll();
        UpdateInventoryUI();
    }

    // ===============================
    // Reset
    // ===============================
    void HandleReset()
    {
        if (!Input.GetKeyDown(KeyCode.R)) return;

        transform.position = PlayerPosition;
        PlayerPosition = initialPosition;
    }

    // ===============================
    // Raycast & Outline
    // ===============================
    void HandleRaycast()
    {
        RaycastHit hit;
        bool isHit = Physics.Raycast(
            fpsCam.transform.position,
            fpsCam.transform.forward,
            out hit,
            distance
        );

        Debug.DrawRay(
            fpsCam.transform.position,
            fpsCam.transform.forward * distance,
            Color.red
        );

        GameObject next = isHit && hit.collider.tag != "Untagget"
            ? hit.collider.gameObject
            : null;

        UpdateSeeObject(next);
    }

    void UpdateSeeObject(GameObject next)
    {
        if (seeObjects == next) return;

        // 以前のアウトラインOFF
        if (currentOutline != null)
            currentOutline.enabled = false;

        seeObjects = next;
        currentOutline = null;

        isGetItem = false;
        isTereport = false;
        isTextWindowActive = false;

        if (seeObjects == null) return;

        if (seeObjects.TryGetComponent(out Outline o))
        {
            currentOutline = o;
            currentOutline.enabled = true;
        }

        switch (seeObjects.tag)
        {
            case "item":
                isGetItem = true;
                break;

            case "tereport":
                isTereport = true;
                break;

            case "Hint":
                isTextWindowActive = true;
                TextWindow.enabled = true;
                break;
        }
    }

    // ===============================
    // Interaction
    // ===============================
    void HandleInteractInput()
    {
        if (!Input.GetKeyDown(KeyCode.E)) return;
        if (seeObjects == null) return;

        if (isGetItem) HandleGetItem();
        else if (isTereport) HandleTeleport();
        else if (isTextWindowActive) HandleHint();
    }

     void HandleGetItem()
    {
    //     if (itemCounts >= maxitemCount) return;

    //     switch (seeObjects.name)
    //     {
    //         case "Book":
    //             AddItem(GameManager.Instance.book.sprite, true);
    //             break;

    //         case "Pickaxe":
    //             AddItem(GameManager.Instance.pickaxe.sprite, false);
    //             break;
    //     }

    //     Destroy(seeObjects);
    //     UpdateSeeObject(null);
    }

    void AddItem(Sprite sprite, bool enableBookMode)
    {
        itemObjects[itemCounts] = seeObjects;
        itemsrot[itemCounts].sprite = sprite;
        itemsrot[itemCounts].color = Color.white;
        itemsrotChildrenText[itemCounts].color = Color.black;

        if (enableBookMode)
            GameManager.Instance.IsBookmodeenable = true;

        itemCounts++;
        maxActiveItemIndex = Mathf.Min(itemCounts, maxitemCount);
    }

    void HandleTeleport()
    {
        PlayerPosition = transform.position;

        switch (seeObjects.name)
        {
            case "CastleGate":
                transform.position = new Vector3(303.7f, 0f, 875f);
                break;

            case "SanctuaryGate":
                transform.position = new Vector3(508f, 15.5f, -1190f);
                break;
        }

        UpdateSeeObject(null);
    }

    void HandleHint()
    {
        switch (seeObjects.name)
        {
            case "Hint1": scenario = hintscenarios[1]; break;
            case "Hint2": scenario = hintscenarios[2]; break;
            case "Hint3": scenario = hintscenarios[3]; break;
        }

        TextWindow.enabled = true;
        textManager.StartText(scenario);
        bookchildrenBotten.SetActive(true);
    }

    // ===============================
    // Inventory Scroll
    // ===============================
    void HandleInventoryScroll()
    {
        mouseScrollDelta = Input.mouseScrollDelta.y * sensitivity;

        if (mouseScrollDelta == 0 || maxActiveItemIndex == 0) return;

        activeItemIndex += (int)mouseScrollDelta;
        activeItemIndex = Mathf.Clamp(activeItemIndex, 0, maxActiveItemIndex - 1);
    }

    void UpdateInventoryUI()
    {
        for (int i = 0; i < maxActiveItemIndex; i++)
        {
            itemsrotChildrenText[i].color =
                (i == activeItemIndex) ? Color.yellow : Color.black;
        }
    }
}
