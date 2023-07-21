using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayersLeftUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private void Update()
    {
        if (GameManager.Instance == null || _text == null)
            return;
        _text.text = GameManager.Instance.PlayerText;
    }
}
