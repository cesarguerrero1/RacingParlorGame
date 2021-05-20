using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RacerScript : MonoBehaviour
{

    //Basic Racer Information
    public string RacerName;
    private float RunSpeed = .015625f;
    private float TrackLength;
    private float BadStartThreshold;

    //Active Race Variables
    private bool RaceWinner = false;
    private bool IsRunning = false;
    private bool BadStart = false;
    private int BadStartAffectedTime = 75;
    private float BadStartRanNum;
    private int BadStartCounter = 0;

    //Handle the racers location
    public Transform CurrentPos;
    private Vector3 StartingPosition;
    private Vector3 Location;

    //Label each Racer
    public TMP_Text RacerLabel;
    
    // Start is called before the first frame update
    void Awake(){
        RacerLabel.text = RacerName;
    }

    void Start(){
        //We just need to handle letting the critter know where home is at all times.
        StartingPosition = CurrentPos.position;
    }

    // Update is called once per frame
    void Update()
    {   
        //Recall that UNLESS you call a return, the Update Loop will run to completion
        if(IsRunning){
            if(BadStart == true){
                BadStartCounter++;
                float distanceMoved = Random.value * .001f;
                Location.x += distanceMoved;
                CurrentPos.position = Location;
            }else{
                float distanceMoved = Random.value * RunSpeed;
                Location.x += distanceMoved;
                CurrentPos.position = Location;
            }
            //After the movement check some conditions
            if(BadStartCounter == BadStartAffectedTime){
                BadStart = false;
            }

            if(Location.x >= TrackLength){
                RaceWinner = true;
                return;
            }
        }
    }

    public void TakePosition(){
        //Send the charactr back to the starting line and reset all data values.
        CurrentPos.position = StartingPosition;
        Location = CurrentPos.position;

        //Reset the racers to the start before every race and clear any racer conditions
        BadStart = false;
        RaceWinner = false;

        BadStartRanNum = Random.value * 100f;
        if(BadStartRanNum <= BadStartThreshold){
            BadStart = true;
        }
        //Make the racers start running!
        IsRunning = true;
    }

    public void Setup(float trackLength, float badStartThreshold){
        //This sets the base values for the racer. Called via the racemanager.
        this.TrackLength = trackLength;
        this.BadStartThreshold = badStartThreshold;
    }

    public bool WonRace(){
        return RaceWinner;
    }
    public void SetRun(bool isRunning){
        this.IsRunning = isRunning;
    }
}
