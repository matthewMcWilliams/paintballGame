using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartUI : NetworkBehaviour
{
    [SerializeField, Min(0)] private float _showTextLength = 7f;
    [SerializeField] private TextMeshProUGUI _text;
    private bool _showingFinishText = false;
    private int _finishPlace = -1;


    public override void OnStartClient()
    {
        GameManager.Instance.OnLocalPlayerFinish += ShowFinishText;
    }

    private void ShowFinishText(int place)
    {
        _showingFinishText = true;
        _finishPlace = place;
        Invoke(nameof(StopShowingText), _showTextLength);
    }

    private void StopShowingText()
    {
        _showingFinishText = false;
    }

    private void Update()
    {
        if (!NetworkServer.active)
        {
            return;
        }

        if ((!GameManager.Instance.PlayersInPosition || GameManager.Instance.CountdownFinished)
            && !_showingFinishText)
        {
            _text.gameObject.SetActive(false);
            return;
        }

        _text.gameObject.SetActive(true);
        if (_showingFinishText)
        {
            _text.text = $"{FormatNumber(_finishPlace)} place";
            return;
        }
        _text.text = GameManager.Instance.CountdownTime.ToString();
    }

    private static string FormatNumber(int n)
    {
        return (n % 10) switch
        {
            1 when n != 11 => $"{n}st",
            2 when n != 12 => $"{n}nd",
            3 when n != 13 => $"{n}rd",
            _ => $"{n}th" 
        };
    }
}
