using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetItemName : MonoBehaviour
{
    public void SetName(ItemData item)
    {
        GetComponent<TextMeshProUGUI>().text = item.Name;
    }
}
