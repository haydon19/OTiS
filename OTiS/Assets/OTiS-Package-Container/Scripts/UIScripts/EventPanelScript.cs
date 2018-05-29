using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventPanelScript : MonoBehaviour {
    public static EventPanelScript instance;
    public Text LightYearCounter;
    public Text eventDesciption;
    public Image eventImage;
    public OptionMenuController optionsMenu;
    public EventLog eventLog;
    Situation currentEvent;

    void Awake()
    {
        if (instance == null)
            //...set this one to be it...
            instance = this;
        //...otherwise...
        else if (instance != this)
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);

    }



    public void SetEvent(Situation newEvent)
    {
        currentEvent = newEvent;
        eventDesciption.text = currentEvent.Summary;
        eventImage.sprite = newEvent.sprite;
    }

    string formatEventText()
    {
        return null;
    }

}
