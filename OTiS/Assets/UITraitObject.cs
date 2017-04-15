using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class UITraitObject : MonoBehaviour, IPointerEnterHandler
{

    public Text traitName;
    public CharacterTrait trait;
    public Toggle traitToggle;
    // Use this for initialization

    void Awake()
    {

        traitName = GetComponentInChildren<Text>();
        traitToggle = GetComponentInChildren<Toggle>();
        //traitToggle.OnPointerClick.

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TraitSelectionPanel.instance.traitInfo.setTrait(trait);
    }

    

}
