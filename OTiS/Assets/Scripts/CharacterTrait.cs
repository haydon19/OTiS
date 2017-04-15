using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TraitType { Engineer };

public class CharacterTrait {

    string name;
    string description;

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    public string Description
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

    public CharacterTrait(string name, string description)
    {
        this.name = name;
        this.description = description;
    }
}
