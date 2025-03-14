using UnityEngine;
using TMPro;

public class ItemTooltip : MonoBehaviour
{
    public GameObject tooltipPanel;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDescriptionText;

    private static ItemTooltip instance;
    private bool isHoveringItem = false; // ✅ Tracks if cursor is inside an inventory item

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        tooltipPanel.SetActive(false);
    }

    void Update()
    {
        if (isHoveringItem)
        {
            Vector2 offset = new Vector2(-27, 27);
            tooltipPanel.transform.position = Input.mousePosition + (Vector3)offset;
        }
    }

    public static void ShowTooltip(string itemName, string description)
    {
        if (instance == null) return;

        instance.isHoveringItem = true; // ✅ Mark that the cursor is inside an inventory item
        instance.tooltipPanel.SetActive(true);
        instance.itemNameText.text = itemName;
        instance.itemDescriptionText.text = description;
    }

    public static void HideTooltip()
    {
        if (instance == null) return;

        instance.isHoveringItem = false; // ✅ Mark that the cursor is no longer inside an inventory item
        instance.tooltipPanel.SetActive(false);
    }
}
