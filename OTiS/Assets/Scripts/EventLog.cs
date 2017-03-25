using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventLog : MonoBehaviour {
    public LogItemObject logItemProto;
    //List<LogItem> logItemList;
    public static EventLog instance;

    public void Start()
    {
        if (instance == null)
            //...set this one to be it...
            instance = this;
        //...otherwise...
        else if (instance != this)
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);

        //logItemList = new List<LogItem>();
    }



    public void newLogItem(string desc, EventType type)
    {
        LogItemObject newItem = Instantiate(logItemProto, transform.position, transform.rotation, transform) as LogItemObject;
        
        newItem.Description.text = desc;
        
        switch (type)
        {
            case(EventType.Combat): 
            newItem.Sprite.color = Color.red;
                break;
            case (EventType.Death):
                newItem.Sprite.color = Color.red;
                break;
            case (EventType.Greeting):
                newItem.Sprite.color = Color.blue;
                break;
            case (EventType.EnemyShip):
                newItem.Sprite.color = Color.magenta;
                break;
            case (EventType.SpaceCombat):
                newItem.Sprite.color = Color.magenta;
                break;
        }
        
        LayoutRebuilder.ForceRebuildLayoutImmediate(this.GetComponent<RectTransform>());
       
        //logItemList.Add(newItem);

    }

    /*
    public void newLogItem()
    {
        Event eventObject = new Event("Event " + this.transform.childCount, this.transform.childCount);
        LogItem newItem = Instantiate(logItemProto, transform.position, transform.rotation, transform) as LogItem;
        LayoutRebuilder.ForceRebuildLayoutImmediate(this.GetComponent<RectTransform>());
        newItem.Description.text = eventObject.Name + " " + eventObject.Summary;
        logItemList.Add(newItem);

    }
    */
}
