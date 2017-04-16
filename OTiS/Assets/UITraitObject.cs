using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class UITraitObject : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{

    public Text traitName;
    public CharacterTrait trait;
    public Toggle traitToggle;
    // Use this for initialization

    void Awake()
    {

        traitName = GetComponentInChildren<Text>();
        traitToggle = GetComponentInChildren<Toggle>();
        traitToggle.onValueChanged.AddListener(CheckboxValueChanged);
        //traitToggle.OnPointerClick
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TraitSelectionPanel.instance.traitInfo.setTrait(trait);
    }

    public void CheckboxValueChanged(bool isOn)
    {

        TraitSelectionPanel.instance.traitsChosen += isOn ? 1 : -1;
        TraitSelectionPanel.instance.selectTrait();

    }

    public void OnPointerClick(PointerEventData eventData)
    {

        traitToggle.isOn = !traitToggle.isOn;
        
    }
}
