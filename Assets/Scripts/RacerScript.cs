using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacerScript : MonoBehaviour
{

    public string racerName;
    public float runSpeed;
    public float trackLength;
    public bool isRunning = false;
    private Vector3 startingPosition;
    private Vector3 Location;
    public Transform currentPos;
    private  bool badStart = false;
    public bool raceWinner = false;
    private  float badStartThreshold = 5f;
    private float badStartRanNum;
    private int counter = 0;

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

        if(counter == 50){
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
        isRunning = true;
    }
}
