using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacerScript : MonoBehaviour
{

    //Basic Racer Information
    public string racerName;
    private float runSpeed;
    private float trackLength;
    private float badStartThreshold;

    //Active Race Variables
    private bool raceWinner = false;
    private bool isRunning = false;
    private bool badStart = false;
    private float badStartRanNum;
    private int counter = 0;

    //Handle the racers location
    public Transform currentPos;
    private Vector3 startingPosition;
    private Vector3 Location;
    
    // Start is called before the first frame update
    void Start()
    {
        //We just need to handle letting the critter know where home is at all times.
        startingPosition = currentPos.position;
    }

    // Update is called once per frame
    void Update()
    {   
        //Recall that UNLESS you call a return, the Update Loop will run to completion
        if(isRunning){
            if(badStart == true){
                counter++;
                float distanceMoved = Random.value * .001f;
                Location.x += distanceMoved;
                currentPos.position = Location;
            }else{
                float distanceMoved = Random.value * runSpeed;
                Location.x += distanceMoved;
                currentPos.position = Location;
            }
        }

        if(counter == 75){
            badStart = false;
        }

        if(Location.x >= trackLength){
            raceWinner = true;
            return;
        }
    }

    //NOTE! This MUST be called via the "RACE" button for each dino.
    public void TakeYourPosition(){
        //Send the charactr back to the starting line  and rest all data values.
        currentPos.position = startingPosition;
        Location = currentPos.position;

        //Reset the racers to the start before every race and clear any racer conditions
        badStart = false;
        raceWinner = false;

        badStartRanNum = Random.value * 100f;
        if(badStartRanNum <= badStartThreshold){
            badStart = true;
        }
        //Make the racers start running!
        isRunning = true;
    }

    public void RaceSetup(float runSpeed, float trackLength, float badStartThreshold){
        //This sets the base values for the racer. Called via the racemanager.
        this.runSpeed = runSpeed;
        this.trackLength = trackLength;
        this.badStartThreshold = badStartThreshold;
    }

    public bool WonRace(){
        return raceWinner;
    }
}
