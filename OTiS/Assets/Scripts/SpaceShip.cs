using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShipState { Travelling, Lightspeed, Idle, Drifting };

public class SpaceShip : IDamageable<float>, IAttacker<IDamageable<float>>, ISubject {

    public List<Character> passengers;
    Dictionary<string, int> shipResources = new Dictionary<string, int>();
    private string name;
    ShipState state;

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

    public Dictionary<string, int> ShipResources
    {
        get
        {
            return shipResources;
        }

        set
        {
            shipResources = value;
        }
    }

    public ShipState State
    {
        get
        {
            return state;
        }

        set
        {
            state = value;
        }
    }

    public SpaceShip(string name, int damage)
    {

        this.Name = name;
        State = ShipState.Travelling;

        setStat("Fuel", 50);
        setStat("Ammo", 100);
        setStat("Shields", 75);
        setStat("Blast", damage);
        setStat("CargoSpace", 5);

    }

    //Dealing with the player stat dictionary
    public int getStat(string resourceName)
    {
        if (!ShipResources.ContainsKey(resourceName))
        {
            ShipResources.Add(resourceName, 0);
        }

        //returns the value of the given stat
        return ShipResources[resourceName];

    }

    public void setStat(string resourceName, int Value)
    {
        if (ShipResources.ContainsKey(resourceName))
        {
            ShipResources[resourceName] = Value;


        }
        else
        {
            //If the stat doesnt exist and we are setting it, lets just create it
            ShipResources.Add(resourceName, Value);
        }
        /*
        Debug.Log(GameControllerScript.instance.party.ToString());
        if (this == GameControllerScript.instance.party.ship)
        {
            PartyInfoPanel.instance.setStat(resourceName, shipResources[resourceName].ToString());
        }
        */
    }

    public void changeStat(string resourceName, int statChange)
    {
        if (ShipResources.ContainsKey(resourceName))
        {
            ShipResources[resourceName] = getStat(resourceName) + statChange;
            if (ShipResources[resourceName] < 0)
            {
                ShipResources[resourceName] = 0;
            }

        }
        else
        {
            //If the stat doesnt exist and we are setting it, lets just create it
            ShipResources.Add(resourceName, statChange);
        }
        /*
        if (this == GameControllerScript.instance.party.ship)
        {
             PartyInfoPanel.instance.setStat(resourceName, shipResources[resourceName].ToString());
        }
        */
    }

    public virtual float Damage(float damageTaken)
    {
        changeStat("Shields", -(int)damageTaken);
        if(getStat("Shields") <= 0)
        {
            setStat("Shields", 0);
            //GameControllerScript.instance.ship = null;
        }
        return damageTaken;
    }

    public float Attack(IDamageable<float> target)
    {
        changeStat("Ammo", -getStat("Blast"));
       return target.Damage(getStat("Blast"));
    }

    public string SubjectReference()
    {
        return name;
    }
}

