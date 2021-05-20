using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BettingManager : MonoBehaviour
{
    
    public GameObject PlayerParentObject;
    private List<GameObject> Bettors = new List<GameObject>();
    public int StartingBankAmountUSD;
    //Who won the race?
    public string WinningRacer;
    //UI Elements
    public TMP_Dropdown BettorDropdownSelector;
    public TMP_Dropdown RacerDropdownSelector;
    public TMP_InputField BetInputField;
    public TMP_Text BetSubmission;
    public Button BetButton;

    void Awake(){
        //Setup all of the the bettors
        Transform PlayerParentObjectAccessor = PlayerParentObject.GetComponent<Transform>();
        for(int i = 0; i < PlayerParentObjectAccessor.childCount; i++){
            Bettors.Add(PlayerParentObjectAccessor.GetChild(i).gameObject);
        }

        foreach(GameObject bettor in Bettors){
            //Make sure each bettor starts with the same amount of money.
            BettorScript bettorScript = bettor.GetComponent<BettorScript>();
            bettorScript.SetupBankAccount(StartingBankAmountUSD);
            //Grabbing each bettor as an option in the dropwdown.
            BettorDropdownSelector.options.Add(new TMP_Dropdown.OptionData(){text = bettorScript.Name});
        }   
    }

    public void checkBettor(){
        BetSubmission.text = "";
        //Check to see if the better has already placed a bet or not.
        string currentBettor = BettorDropdownSelector.options[BettorDropdownSelector.value].text;
        for(int i = 0; i < Bettors.Count; i++){
            BettorScript bettor = Bettors[i].GetComponent<BettorScript>();
            if(bettor.Name == currentBettor){
                //If the bettor matches the dropdown value, then we need to see if the player has placed a bet!
                if(bettor.CheckBet()){
                    //If the bettor has already bet, then turn off the submit a bet button
                    BetButton.interactable = false;
                }else{
                    BetButton.interactable = true;
                }
            }
        }
    }

    public void submitBet(){
        //Grab the player who placed the bet, the racer they picked, and update his placeholders
        string currentBettor = BettorDropdownSelector.options[BettorDropdownSelector.value].text;
        string chosenRacer = RacerDropdownSelector.options[RacerDropdownSelector.value].text;
        int betAmount = int.Parse(BetInputField.text);
        if(betAmount < 5){
            BetSubmission.text = "You did not bet the minimum of $5. Please try again.";
        }else{
            //Bet is over 5 so allow the submission
            for(int i = 0; i < Bettors.Count; i++){
                BettorScript bettor = Bettors[i].GetComponent<BettorScript>();
                if(bettor.Name == currentBettor){
                    bettor.Bet(betAmount, chosenRacer);
                    //Now use check bettor to turn off the button for this bettor
                    this.checkBettor();
                }
            }
            BetSubmission.text = "Your bet was accepted! Good Luck!";
        }
    }

    public void SettleBets(){
        //For each player, reconcile their bet.
        for(int i = 0; i < Bettors.Count; i++){
                BettorScript bettor = Bettors[i].GetComponent<BettorScript>();
                //Check to see if the racer even picked the winning racer
                if(bettor.ChosenRacer() == WinningRacer){
                    int doubleWinnings = bettor.BetAmount() * 2;
                    bettor.SettleAccount(doubleWinnings);
                }else{
                    bettor.SettleAccount(0);
                }
        }
    }

    public bool AllBetsMade(){
        for(int i = 0; i < Bettors.Count; i++){
            BettorScript bettor = Bettors[i].GetComponent<BettorScript>();
            //Check to see if the racer has place a bet
            if(bettor.CheckBet()){
                continue;
            }else{
                return false;
            }
        }
        return true;
    }
}
