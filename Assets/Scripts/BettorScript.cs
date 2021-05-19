using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BettorScript : MonoBehaviour
{
    public string Name;
    private int bankAccount;

    //Each player will have two labels attached to them. How much they have and each current bet instance.
    public TMP_Text PlayerBank;
    public TMP_Text PlayerBet;
    

    void Bet(){
        //Player should take money from his bank account and bet it
    }

    public void SetupBankAccount(int bankAccountAmount){
        //Allow the BettingManager to setup and account for the player and dole out money.
        bankAccount = bankAccountAmount;
        //Setup the betting parlor labels
        PlayerBank.text = $"{Name} has ${bankAccount} in their betting wallet.";
        PlayerBet.text  = $"Awaiting {Name}'s bet...";
    }

    public int currentBankamount(){
        return bankAccount;
    }
}
