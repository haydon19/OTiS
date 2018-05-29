using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventLog : MonoBehaviour {
    public LogItemObject logItemProto;
    //List<LogItem> logItemList;
    public static EventLog instance;
    public Color defaultColor = Color.black;

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



    public void newLogItem(string desc)
    {
        LogItemObject newItem = Instantiate(logItemProto, transform.position, transform.rotation, transform) as LogItemObject;
        
        newItem.Description.text = desc;
        newItem.Sprite.color = defaultColor;
        
        
        LayoutRebuilder.ForceRebuildLayoutImmediate(this.GetComponent<RectTransform>());
        //yield return new WaitForSeconds(1);
        //logItemList.Add(newItem);

    }



    public void newLogItem(string desc, Color color)
    {
        LogItemObject newItem = Instantiate(logItemProto, transform.position, transform.rotation, transform) as LogItemObject;

        newItem.Description.text = desc;
        newItem.Sprite.color = color;


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
