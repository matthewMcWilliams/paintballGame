using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemListItem : MonoBehaviour
{
	private TextMeshProUGUI _text;

	private ItemData _item;

	public ItemData Item
	{
		get { return _item; }
		set 
		{
			_item = value; 
			_textProp.text = _item.Name;
		}
	}

    private TextMeshProUGUI _textProp { 
		get 
		{ 
			return _text ?? GetComponentInChildren<TextMeshProUGUI>(); 
		} 
		set => _text = value; 
	}


    public void SelectItem()
    {
		ShopManager.Instance.SelectItem(Item);
    }
}
