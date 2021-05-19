using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BettingManager : MonoBehaviour
{
    public GameObject PlayerParentObject;
    private int startingBankAmountUSD = 500;
    private List<GameObject> Bettors = new List<GameObject>();

    //Dropdown Selector
    public GameObject DropdownSelector;

    void Awake(){
        //Setup all of the the bettors
        Transform PlayerParentObjectAccessor = PlayerParentObject.GetComponent<Transform>();
        for(int i = 0; i < PlayerParentObjectAccessor.childCount; i++){
            Bettors.Add(PlayerParentObjectAccessor.GetChild(i).gameObject);
        }

        foreach(GameObject Bettor in Bettors){
            //Grab their script and assign values as needed
            BettorScript BettorScript = Bettor.GetComponent<BettorScript>();
            BettorScript.SetupBankAccount(startingBankAmountUSD);
        }
    }

     // Start is called before the first frame update


}
