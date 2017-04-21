using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraitInfoPanel : MonoBehaviour {

    public Text traitName;
    public Text traitDescription;

    public void setTrait(Trait newTrait)
    {
        traitName.text = newTrait.Name;
        traitDescription.text = newTrait.Description;
        foreach(StatBonus sb in newTrait.StatBonuses)
        {
            traitDescription.text += "\n+" + sb.BonusValue + " " + sb.StatName;
        }
    }


}
