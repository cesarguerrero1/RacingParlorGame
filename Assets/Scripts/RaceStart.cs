using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceStart : MonoBehaviour
{
    public GameObject RacerManagerObject;
    public GameObject BettorManagerObject;
    private bool raceHappening = false;
    // Start is called before the first frame update
    public void ManagerStartRace(){
        //We need to start the race via this method
        
        RacerManagerObject.GetComponent<RaceManager>().StartRace();
        //Turn off the button!
        this.gameObject.GetComponent<Button>().interactable = false;
    }

    void Update(){
        raceHappening = RacerManagerObject.GetComponent<RaceManager>().LiveRace;
        if(raceHappening){
            this.gameObject.GetComponent<Button>().interactable = false;
        }else{
            //Check to see if all bets have been made so we can make the button interactable.
            if(BettorManagerObject.GetComponent<BettingManager>().AllBetsMade()){
                this.gameObject.GetComponent<Button>().interactable = true;
            }
        }
    }

}
