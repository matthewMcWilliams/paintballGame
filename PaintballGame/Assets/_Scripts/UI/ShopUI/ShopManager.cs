using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ShopManager : Singleton<ShopManager>
{
    public UnityEvent<ItemData> OnItemSelected;

    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private RectTransform _itemList;
    [SerializeField] private List<ItemData> _items = new List<ItemData>();

    private void Start()
    {
        SetupShop();
    }

    [ContextMenu("Setup")]
    public void SetupShop()
    {
        while (_itemList.childCount > 0)
        {
            DestroyImmediate(_itemList.GetChild(0).gameObject);
        }

        foreach (var item in _items)
        {
            var instanciatedObject = Instantiate(_itemPrefab, _itemList);
            instanciatedObject.GetComponent<ItemListItem>().Item = item;
        }
    }

    public void SelectItem(ItemData item)
    {
        OnItemSelected?.Invoke(item);
    }
}
