using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StatInfoObject : MonoBehaviour {
    Image sprite;
    Text description;

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

    public void Init()
    {

        Description = this.GetComponentInChildren<Text>();


        //Sprite = this.GetComponentInChildren<Image>();
    }
}
