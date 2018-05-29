using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatSetterPanel : MonoBehaviour {

    public StatSetterObject proto;
    Dictionary<string, StatSetterObject> statObjectList;
    public static StatSetterPanel instance;

    public Dictionary<string, StatSetterObject> StatObjectList
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

    public void Awake()
    {
        if (instance == null)
            //...set this one to be it...
            instance = this;
        //...otherwise...
        else if (instance != this)
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);

        statObjectList = new Dictionary<string, StatSetterObject>();

    }

    public void initStats(List<string> stats)
    {
        foreach (string stat in stats)
        {

                StatSetterObject newItem = Instantiate(proto, transform.position, transform.rotation, transform) as StatSetterObject;
                //newItem.Init();
                LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
                //If the stat doesnt exist and we are setting it, lets just create it
                StatObjectList.Add(stat, newItem);
                StatObjectList[stat].statName.text = stat;
                StatObjectList[stat].UpdateStat(CharacterCreationPanel.DEFAULT_STAT_VALUE);

        }
    }

}
