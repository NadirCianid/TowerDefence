using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{
    GameManager _gameManager;
    UIManager _UIManager;

    [SerializeField] int _startingBalance = 100;
    [SerializeField] int _currentBalance;
  
    public int CurrentBalance { get {return _currentBalance;}}
    
    void Awake() {
        _currentBalance = _startingBalance;
        _UIManager = FindObjectOfType<UIManager>();
        _gameManager = FindObjectOfType<GameManager>();
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
            _gameManager.ReloadScene();
            _UIManager.UpdateUI(0);
        }
    }
}
