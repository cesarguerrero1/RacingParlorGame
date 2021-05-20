using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RaceManager : MonoBehaviour
{
    //This holds all of our Racers so we can access each of them
    public GameObject RacerParentObject;
    private List<GameObject> Racers = new List<GameObject>();

    //Betting Manager
    public GameObject BettingManager;
    
    //Racer assigned variables
    public float RacerTrackLength;
    public float RacerBadStartThreshold;
    
    //Is there a race going on?
    public bool LiveRace = false;

    //Grab the UI Labels
    public TMP_Dropdown RacerDropdownSelector;
    public TMP_Text RaceResults;


    //Called before Start()
    void Awake(){

        //Parse our Racers Parent into a list of the child game objects. 
        Transform RacerParentObjectAccessor = RacerParentObject.GetComponent<Transform>();
        for(int i = 0; i < RacerParentObjectAccessor.childCount; i++){
            Racers.Add(RacerParentObjectAccessor.GetChild(i).gameObject);
        }

        //Ensure the racer knows where it exists in space and give then setup values
        foreach(GameObject racer in Racers){
            RacerScript racerScript = racer.GetComponent<RacerScript>();
            racerScript.CurrentPos = racer.GetComponent<Transform>();
            RacerDropdownSelector.options.Add(new TMP_Dropdown.OptionData(){text = racerScript.RacerName});
            racerScript.Setup(RacerTrackLength, RacerBadStartThreshold);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(LiveRace){
            foreach(GameObject racer in Racers){
            //Check to see if someone won!
                bool wonRace = racer.GetComponent<RacerScript>().WonRace();
                if(wonRace){
                    //Tell the game who won!
                    RaceResults.text = $"{racer.GetComponent<RacerScript>().RacerName} has won the race!";
                    //A racer won! So stop the race for EVERYONE.
                    foreach(var racerOverride in Racers){
                        racerOverride.GetComponent<Animator>().enabled = false;
                        racerOverride.GetComponent<RacerScript>().SetRun(false);
                    }
                    //Tell the  manager a race is no longer happening
                    LiveRace = false;
                    //Tell the parlor who won and tell them to settle bets
                    BettingManager.GetComponent<BettingManager>().WinningRacer = racer.GetComponent<RacerScript>().RacerName;
                    BettingManager.GetComponent<BettingManager>().SettleBets();
                    return;
                }
            }
        }
    }

    public void StartRace(){
        //Wipe out the race label as we are about to start another race
        RaceResults.text = "";
        //Tell the betting manager no one won
        BettingManager.GetComponent<BettingManager>().WinningRacer = "";
        //The race has started 
        LiveRace = true;
        //Once that is done then we go through each racer and alter some items
        foreach(GameObject racer in Racers){
            //Start the animator, activate the script, and call some methods
            racer.GetComponent<Animator>().enabled = true;
            racer.GetComponent<RacerScript>().TakePosition();
        }
    }

}
