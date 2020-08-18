using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [Header("Player UI")]
    [SerializeField] TMPro.TextMeshProUGUI ppText;
    [SerializeField] TMPro.TextMeshProUGUI currentItemText;

    [Header("Settings UI TODO")]
    [SerializeField] TMPro.TextMeshPro settingsText;

    public void SetPP (string pp)
    {
        ppText.SetText($"PP: {pp}");
    }

    public void SetCurrentItem (string name)
    {
        currentItemText.SetText($"Item: {name}");
    }
}
