using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraitInfoPanel : MonoBehaviour {

    public Text traitName;
    public Text traitDescription;

    public void setTrait(CharacterTrait newTrait)
    {
        traitName.text = newTrait.Name;
        traitDescription.text = newTrait.Description;
    }


}
