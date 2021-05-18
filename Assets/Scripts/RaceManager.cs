using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceManager : MonoBehaviour
{
    //This holds all of our Racers so we can access each of them
    public GameObject RacerParentObject;
    public Button RaceButton;
    private float runSpeed = .015625f;
    private float trackLength = 7.25f;
    private List<GameObject> racers = new List<GameObject>();

    //Called before Start()
    void Awake(){

        //Parse our Racers Parent into a list of the child game objects. 
        Transform RacerParentObjectAccessor = RacerParentObject.GetComponent<Transform>();
        for(int i = 0; i < RacerParentObjectAccessor.childCount; i++){
            racers.Add(RacerParentObjectAccessor.GetChild(i).gameObject);
        }

        foreach(GameObject racer in racers){
            RacerScript RacerScript = racer.GetComponent<RacerScript>();
            RacerScript.currentPos = racer.GetComponent<Transform>();
            RacerScript.trackLength = trackLength;
            RacerScript.runSpeed = runSpeed;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject racer in racers){
            //Check to see if someone won!
            RacerScript RacerScript = racer.GetComponent<RacerScript>();
            if(RacerScript.raceWinner == true){
                foreach(var racerOverride in racers){
                    //Turn off the animatio and turn off the running!
                    racerOverride.GetComponent<Animator>().enabled = false;
                    racerOverride.GetComponent<RacerScript>().enabled = false;
                }
                //Enable the button so we can race again!
                RaceButton.GetComponent<Button>().interactable = true;
                return;
            }
        }
    }

    public void StartRace(){
        //Once that is done then we go through each racer and alter some items
        foreach(GameObject racer in racers){
            racer.GetComponent<Animator>().enabled = true;
            RacerScript RacerScript = racer.GetComponent<RacerScript>();
            RacerScript.enabled = true;
            RacerScript.TakeYourPosition();
        }

    }

}
