using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//public enum OptionType {  };


    public enum OptionTag { Avoid, Blast, Land, Board, Thrusters, Comms, Intimidate, Recruit, Gossip, Scan, Bypass, Negotiate, Flee };

[System.Serializable]
public class Option {

    [SerializeField]
    OptionTag oTag;
    [SerializeField]
    [TextArea(5,3)]
    string description;


    //List<Character> actors;



    public string Description
    {
        get
        {
            return description;
        }

        set
        {
            description = value;
        }
    }


 

    public OptionTag OType
    {
        get
        {
            return oTag;
        }

        set
        {
            oTag = value;
        }
    }




}
