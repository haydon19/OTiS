﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class OptionObjectButton : MonoBehaviour, IPointerClickHandler {

    Image sprite;
    Text description;
    Option option;

    public Text Description
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

    public Image Sprite
    {
        get
        {
            return sprite;
        }

        set
        {
            sprite = value;
        }
    }

    public Option Option
    {
        get
        {
            return option;
        }

        set
        {
            option = value;
        }
    }

    public void Awake()
    {

        Description = this.GetComponentInChildren<Text>();

        //Debug.Log(this.GetComponentInChildren<Image>());
        //Sprite = this.GetComponentInChildren<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        option.OptionChosen();
        //throw new NotImplementedException();
    }
}
