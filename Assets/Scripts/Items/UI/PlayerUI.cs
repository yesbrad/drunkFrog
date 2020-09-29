using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] TMPro.TextMeshProUGUI ppText;
    [SerializeField] TMPro.TextMeshProUGUI currentItemText;
    [SerializeField] TMPro.TextMeshProUGUI currentCashText;

    [Header("House Inventory")]
    [SerializeField] TMPro.TextMeshProUGUI funText;
    [SerializeField] TMPro.TextMeshProUGUI alcoholText;
    [SerializeField] TMPro.TextMeshProUGUI waterText;
    [SerializeField] TMPro.TextMeshProUGUI foodText;

    [Header("Settings UI TODO")]
    [SerializeField] TMPro.TextMeshPro settingsText;

    public void SetPP (string pp)
    {
        ppText.SetText($"PP: {pp}");
    }

    public void SetCurrentItem (string name)
    {
        currentItemText.SetText($"{name}");
    }

    public void SetCash (int cash)
    {
        currentCashText.SetText($"${cash}");
    }

    public void SetHouseInventoryStats(List<HouseInventory.Category> categories)
    {
        for (int i = 0; i < categories.Count; i++)
        {
            if (categories[i].catagory == AIStatTypes.Boardness)
                funText.SetText(categories[i].Amount.ToString());

            if (categories[i].catagory == AIStatTypes.Hunger)
                foodText.SetText(categories[i].Amount.ToString());

            if (categories[i].catagory == AIStatTypes.Thirst)
                waterText.SetText(categories[i].Amount.ToString());

            if (categories[i].catagory == AIStatTypes.Soberness)
                alcoholText.SetText(categories[i].Amount.ToString());
        }
    }
}
