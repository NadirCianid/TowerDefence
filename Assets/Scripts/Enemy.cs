using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   Bank _bank;
   [SerializeField] int _rewardAmount = 25;
   [SerializeField] int _stealAmount = 25;

   void Awake() 
   {
        _bank = FindObjectOfType<Bank>();
   }

   public void RewardGold()
   {
        if(_bank == null) return;

        _bank.Deposit(_rewardAmount);
   }

   public void StealGold()
   {
        if(_bank == null) return;

        _bank.Withdraw(_stealAmount);
   }
}
