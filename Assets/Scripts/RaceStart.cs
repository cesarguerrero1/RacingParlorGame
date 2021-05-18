using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceStart : MonoBehaviour
{
    public GameObject RacerObject;
    // Start is called before the first frame update
    public void SetupRace(){
        //We need to start the race via this method
        
        //Enable the Race Manager Script!
        RacerObject.GetComponent<RaceManager>().StartRace();
        //Turn off the button!
        this.gameObject.GetComponent<Button>().interactable = false;
    }
}
