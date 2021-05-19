using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceManager : MonoBehaviour
{
    //This holds all of our Racers so we can access each of them
    public GameObject RacerParentObject;
    public Button RaceButton;
    public float racerRunSpeed;
    public float racerTrackLength;
    public float racerBadStartThreshold;
    private List<GameObject> racers = new List<GameObject>();

    //Called before Start()
    void Awake(){

        //Parse our Racers Parent into a list of the child game objects. 
        Transform RacerParentObjectAccessor = RacerParentObject.GetComponent<Transform>();
        for(int i = 0; i < RacerParentObjectAccessor.childCount; i++){
            racers.Add(RacerParentObjectAccessor.GetChild(i).gameObject);
        }

        //Activate the racers running animation and ensure the racer knows where it exists in space.
        foreach(GameObject racer in racers){
            RacerScript RacerScript = racer.GetComponent<RacerScript>();
            RacerScript.currentPos = racer.GetComponent<Transform>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject racer in racers){
            //Check to see if someone won!
            bool wonRace = racer.GetComponent<RacerScript>().WonRace();
            if(wonRace){
                //A racer won! So stop the race for EVERYONE.
                foreach(var racerOverride in racers){
                    racerOverride.GetComponent<Animator>().enabled = false;
                    racerOverride.GetComponent<RacerScript>().enabled = false;
                }
                return;
            }
        }
    }

    public void StartRace(){
        //Once that is done then we go through each racer and alter some items
        foreach(GameObject racer in racers){
            //Start the animator, activate the script, and call some methods
            racer.GetComponent<Animator>().enabled = true;
            RacerScript RacerScript = racer.GetComponent<RacerScript>();
            RacerScript.enabled = true;
            RacerScript.TakeYourPosition();
            RacerScript.RaceSetup(racerRunSpeed, racerTrackLength, racerBadStartThreshold);
        }
    }

}
