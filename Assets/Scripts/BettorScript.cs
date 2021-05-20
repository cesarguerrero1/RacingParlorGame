using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BettorScript : MonoBehaviour
{
    public string Name;
    private int BankAccount;
    private string PickedRacer;
    private int PlacedBetAmount;
    private bool PlacedBet = false;

    //Each player will have two labels attached to them. How much they have and each current bet instance.
    public TMP_Text PlayerBank;
    public TMP_Text PlayerBet;
    

    public void Bet(int betAmount, string racerName){
        //Tie the bet  mount and chosen racer to the player
        this.PlacedBetAmount = betAmount;
        this.PickedRacer = racerName;

        //How much did the player bet and on what racer.
        BankAccount -= betAmount;
        PlayerBank.text = $"{Name} has ${BankAccount} in their betting wallet.";
        PlayerBet.text  = $"{Name} bet ${betAmount} on {racerName} to win.";
        //This player can no longer place a bet for this round.
        PlacedBet = true;
    }

    public void SetupBankAccount(int bankAccountAmount){
        //Allow the BettingManager to setup and account for the player and dole out money.
        BankAccount = bankAccountAmount;
        //Setup the betting parlor labels
        PlayerBank.text = $"{Name} has ${BankAccount} in their betting wallet.";
        PlayerBet.text  = $"Awaiting {Name}'s bet...";
    }

    public bool CheckBet(){
        //Return whether or not  the player has placed a bet.
        return PlacedBet;
    }
    public string ChosenRacer(){
        return PickedRacer;
    }

    public int BetAmount(){
        return PlacedBetAmount;
    }

    public void SettleAccount(int owedAmount){
        BankAccount += owedAmount;

        //Update player labels
        PlayerBank.text = $"{Name} has ${BankAccount} in their betting wallet.";
        PlayerBet.text  = $"Awaiting {Name}'s bet...";

        //The player should now be able to bet again!
        PlacedBet = false;

    }
}
