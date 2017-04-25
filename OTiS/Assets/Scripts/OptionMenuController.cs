﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenuController : MonoBehaviour {

    public OptionObjectButton proto;
    //Dictionary<string, OptionObjectButton> currentOptions;
    public static OptionMenuController instance;


    public void Start()
    {
        if (instance == null)
            //...set this one to be it...
            instance = this;
        //...otherwise...
        else if (instance != this)
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);

        //CurrentOptions = new Dictionary<string, OptionObjectButton>();
    }

    public void addOptionItems(Event eventObject)
    {
        foreach (Option o in eventObject.Options)
        {
            OptionObjectButton newOption = Instantiate(proto, transform.position, transform.rotation, transform) as OptionObjectButton;
            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
            newOption.Description.supportRichText = true;
            newOption.Description.text = o.Description;
            newOption.Option = o;
            switch(o.Type){
                case (OptionType.Blast):
                        newOption.Sprite.color = Color.red;
                        break;
                case (OptionType.Avoid):
                    newOption.Sprite.color = Color.green;
                    break;
                case (OptionType.Gossip):
                    newOption.Sprite.color = Color.grey;
                    break;
                case (OptionType.Recruit):
                    newOption.Sprite.color = Color.magenta;
                    break;
            }

            //CurrentOptions.Add(o.Description, newOption);

        }
    }

    public void clearOptions()
    {
        var children = new List<GameObject>();
        foreach (Transform child in transform) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));
    }

   

}