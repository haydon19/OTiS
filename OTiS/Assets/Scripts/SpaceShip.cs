using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShipType { };

public class SpaceShip : IDamageable<float>, IAttacker<IDamageable<float>> {

    public List<Character> passengers;
    Dictionary<string, int> shipResources = new Dictionary<string, int>();
    private string name;

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

    public SpaceShip(string name, int damage)
    {

        this.Name = name;
        setStat("Fuel", 50);
        setStat("Ammo", 100);
        setStat("Shields", 75);
        setStat("Blast", damage);
        setStat("CargoSpace", 5);

    }

    //Dealing with the player stat dictionary
    public int getStat(string resourceName)
    {
        if (!shipResources.ContainsKey(resourceName))
        {
            shipResources.Add(resourceName, 0);
        }

        //returns the value of the given stat
        return shipResources[resourceName];

    }

    public void setStat(string resourceName, int Value)
    {
        if (shipResources.ContainsKey(resourceName))
        {
            shipResources[resourceName] = Value;

        }
        else
        {
            //If the stat doesnt exist and we are setting it, lets just create it
            shipResources.Add(resourceName, Value);
        }
    }

    public void changeStat(string resourceName, int statChange)
    {
        if (shipResources.ContainsKey(resourceName))
        {
            shipResources[resourceName] = getStat(resourceName) + statChange;
            if (shipResources[resourceName] < 0)
            {
                shipResources[resourceName] = 0;
            }

        }
        else
        {
            //If the stat doesnt exist and we are setting it, lets just create it
            shipResources.Add(resourceName, statChange);
        }
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
}

