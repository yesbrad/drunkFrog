using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [Header("Player UI")]
    [SerializeField] TMPro.TextMeshProUGUI ppText;
    [SerializeField] TMPro.TextMeshProUGUI currentItemText;
    [SerializeField] TMPro.TextMeshProUGUI currentCashText;

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
        currentCashText.SetText($"Cash: ${cash}");
    }
}
