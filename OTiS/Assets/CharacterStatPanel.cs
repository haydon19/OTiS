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

    void Awake()
    {
        if (instance == null)
            //...set this one to be it...
            instance = this;
        //...otherwise...
        else if (instance != this)
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);
        Debug.Log("Start thing");

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
            newItem.Init();
            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
            //If the stat doesnt exist and we are setting it, lets just create it
            StatObjectList.Add(statName, newItem);
            StatObjectList[statName].Description.text = statName + ": " + statValue;
        }
    }
}
