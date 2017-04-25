using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraitSelectionPanel : MonoBehaviour {

    public static TraitSelectionPanel instance;
    public TraitListPanel traitList;
    public TraitInfoPanel traitInfo;
    public Text traitsRemaining;
    public const int MAX_TRAITS = 2;
    public int traitsChosen;

    public void Start()
    {
        if (instance == null)
            //...set this one to be it...
            instance = this;
        //...otherwise...
        else if (instance != this)
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);

        populateTraitPanel();
        traitsRemaining.text = traitsChosen + "/" + MAX_TRAITS + " traits selected.";
    }

    public void Init()
    {
        

    }

    public void selectTrait(Trait trait)
    {
        traitsRemaining.text = traitsChosen + "/" + MAX_TRAITS + " traits selected.";
       // AddStatBonus(trait);
    }



    public void populateTraitPanel()
    {
        foreach(KeyValuePair<string, Trait> trait in GameData.instance.traitDictionary)
        {
            traitList.addTraitItem(trait.Value);
        }

        traitInfo.setTrait(traitList.traits[0].trait);
    }



}
