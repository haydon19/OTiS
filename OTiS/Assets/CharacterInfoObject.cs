using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class CharacterInfoObject : MonoBehaviour, IPointerClickHandler
{
    Image sprite;
    Text description;
    Character character;

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

    public Character Character
    {
        get
        {
            return character;
        }

        set
        {
            character = value;
        }
    }

    public void Awake()
    {

        Description = this.GetComponentInChildren<Text>();


        Sprite = this.GetComponentInChildren<Image>();
    }



    public void OnPointerClick(PointerEventData eventData)
    {

        // foreach (KeyValuePair<string, int> attachStat in character.Stats)

        CharacterInfoPanel.instance.ActiveCharacter = character;

        
    }
}
