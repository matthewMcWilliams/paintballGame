using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetItemDescription : MonoBehaviour
{
    public void SetName(ItemData item)
    {
        GetComponent<TextMeshProUGUI>().text = item.Description;
    }
}
