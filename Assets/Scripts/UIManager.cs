using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _balanceUI;

    public void UpdateUI(int _currentBalance)
    {
        _balanceUI.text = "Gold: "+ _currentBalance.ToString();
    }
}
