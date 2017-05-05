using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShipState { Travelling, Lightspeed, Idle, Drifting };

public class SpaceShip : IDamageable<int>, IAttacker<IDamageable<int>>, ISubject {

    public List<Character> passengers;
    Dictionary<string, int> shipResources = new Dictionary<string, int>();
    private string name;
    ShipState state;
    int regenRate = 3;
    int toRegen = 0;
    List<Module> modules;
    List<Attack> attacks;

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

    public int ToRegen
    {
        get
        {
            return toRegen;
        }

        set
        {
            toRegen = value;
        }
    }

    public int RegenRate
    {
        get
        {
            return regenRate;
        }

        set
        {
            regenRate = value;
        }
    }

    public List<Attack> Attacks
    {
        get
        {
            return attacks;
        }

        set
        {
            attacks = value;
        }
    }

    public SpaceShip(string name, int damage)
    {

        this.Name = name;
        State = ShipState.Travelling;

        setStat("Fuel", 50);
        setStat("Ammo", 100);
        setStat("Shields", 20);
        setStat("MaxShields", 20);
        setStat("Hull", 20);
        setStat("Blast", damage);
        setStat("ModuleSlots", 4);


    }

    public bool addModule(Module m)
    {
        if (modules.Count < getStat("ModuleSlots"))
        {
            modules.Add(m);
            return true;
        }

        return false;
    }

    public List<Module> getModules()
    {
        return modules;
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
        //

        

        
    }
    public void Regenerate()
    {
        setStat("Shields", getStat("MaxShields"));
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
        //
        
        
    }

    public virtual int Damage(int damageTaken)
    {
        int hulldamage = damageTaken - getStat("Shields");

        if (getStat("Shields") > 0)
        {
            changeStat("Shields", -(int)damageTaken);
            EventLog.instance.newLogItem(name + " has taken " + damageTaken + " damage to shields.");
        }
        if (getStat("Shields") <= 0)
        {
            
            if (hulldamage > 0)
            {
                changeStat("Hull", -hulldamage);
                EventLog.instance.newLogItem(name + " has taken " + hulldamage + " damage to it's hull.");
            }
            //GameControllerScript.instance.ship = null;
        }
        return damageTaken;
    }

    public int Attack(IDamageable<int> target)
    {
        changeStat("Ammo", -getStat("Blast"));
       return target.Damage(getStat("Blast"));
    }

    public string SubjectReference()
    {
        return name;
    }
}

