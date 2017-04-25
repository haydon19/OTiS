using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TraitListPanel : MonoBehaviour {

    public UITraitObject traitItemProto;
    public List<UITraitObject> traits = new List<UITraitObject>();

    public void addTraitItem(Trait trait)
    {
        UITraitObject newItem = Instantiate(traitItemProto, transform.position, transform.rotation, transform) as UITraitObject;

        newItem.traitName.text = trait.Name;
        newItem.trait = trait;
        newItem.traitToggle.isOn = false;
        traits.Add(newItem);
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }


    public List<Trait> getSelectedTraits()
    {
        List<Trait> traitList = new List<Trait>();

        foreach(UITraitObject tObj in traits)
        {
            if (tObj.traitToggle.isOn)
            {
                traitList.Add(tObj.trait);
            }
        }

        return traitList;
    }

}
