using UnityEngine;
using TMPro;

public class ItemTooltip : MonoBehaviour
{
    public GameObject tooltipPanel;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDescriptionText;

    private static ItemTooltip instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        tooltipPanel.SetActive(false);
    }

    public static void ShowTooltip(string itemName, string description, Vector2 position)
    {
        if (instance == null) return;

        instance.tooltipPanel.SetActive(true);
        instance.itemNameText.text = itemName;
        instance.itemDescriptionText.text = description;
        instance.tooltipPanel.transform.position = position;
    }

    public static void HideTooltip()
    {
        if (instance == null) return;
        instance.tooltipPanel.SetActive(false);
    }
}
