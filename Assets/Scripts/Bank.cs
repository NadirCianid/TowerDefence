using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{
    [SerializeField] int _startingBalance = 100;
    [SerializeField] int _currentBalance;
    public int CurrentBalance { get {return _currentBalance;}}
    UIManager _UIManager;
    private void Awake() {
        _currentBalance = _startingBalance;
        _UIManager = FindObjectOfType<UIManager>();
        _UIManager.UpdateUI(_currentBalance);
    }

    public void Deposit(int _amount)
    {
        _currentBalance += _amount;
        _UIManager.UpdateUI(_currentBalance);
    }
    public void Withdraw(int _amount)
    {
        _currentBalance -= _amount;
        _UIManager.UpdateUI(_currentBalance);

        if(_currentBalance < 0) 
        {
            Debug.Log("Game Over!");
            _UIManager.UpdateUI(0);
        }
    }
}
