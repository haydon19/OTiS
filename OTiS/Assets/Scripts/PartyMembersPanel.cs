using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyMembersPanel : MonoBehaviour {


    PartyMembersPanel instance;



        // Use this for initialization
        void Start()
        {
            if (instance == null)
                //...set this one to be it...
                instance = this;
            //...otherwise...
            else if (instance != this)
                //...destroy this one because it is a duplicate.
                Destroy(gameObject);



        }


    }
