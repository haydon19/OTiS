using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterStatPanel : MonoBehaviour {
    /*
   public CharacterInfoObject logItemProto;
   Dictionary<string, CharacterInfoObject> logItemList = new Dictionary<string, CharacterInfoObject>();
   public static CharacterInfoPanel instance;
   */
    public StatInfoObject statObjectProto;

    Dictionary<string, StatInfoObject> statObjectList;
    public static CharacterStatPanel instance;

    public Dictionary<string, StatInfoObject> StatObjectList
    {
        get
        {
            return statObjectList;
        }

        set
        {
            statObjectList = value;
        }
    }

    //public void showCharacter

    public void Start()
    {
        statObjectList = new Dictionary<string, StatInfoObject>();
    }

    public void updateStatInfo(string stat, string value)
    {
        StatInfoObject statInfo = StatObjectList[stat];
        statInfo.Description.text = stat + ": " + value;
    }


    public void setStat(string statName, string statValue)
    {
        if (StatObjectList.ContainsKey(statName))
        {
            StatObjectList[statName].Description.text = statName + ": " + statValue;

        }
        else
        {
            StatInfoObject newItem = Instantiate(statObjectProto, transform.position, transform.rotation, transform) as StatInfoObject;
            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
            //If the stat doesnt exist and we are setting it, lets just create it
            StatObjectList.Add(statName, newItem);
            StatObjectList[statName].Description.text = statName + ": " + statValue;
        }
    }
}
