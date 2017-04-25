using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class UITraitObject : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{

    public Text traitName;
    public Trait trait;
    public Toggle traitToggle;
    // Use this for initialization

    void Awake()
    {

        traitName = GetComponentInChildren<Text>();
        traitToggle = GetComponentInChildren<Toggle>();
        traitToggle.interactable = false;
        
        //traitToggle.OnPointerClick
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TraitSelectionPanel.instance.traitInfo.setTrait(trait);
    }

    public void CheckboxValueChanged(bool isOn)
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!traitToggle.isOn)
        {
            if (TraitSelectionPanel.instance.traitsChosen < TraitSelectionPanel.MAX_TRAITS)
            {
                TraitSelectionPanel.instance.traitsChosen += 1;
                TraitSelectionPanel.instance.selectTrait(trait);
                traitToggle.isOn = true;
                foreach (StatBonus sb in trait.StatBonuses)
                {
                    StatSetterPanel.instance.StatObjectList[sb.StatName].AddStatBonus(sb);
                }
            }
        }
        else
        {
            if (TraitSelectionPanel.instance.traitsChosen > 0)
            {
                TraitSelectionPanel.instance.traitsChosen += -1;
                TraitSelectionPanel.instance.selectTrait(trait);
                traitToggle.isOn = false;
                foreach (StatBonus sb in trait.StatBonuses)
                {
                    StatSetterPanel.instance.StatObjectList[sb.StatName].RemoveStatBonus(sb);
                }
            }
        }

    }
}
