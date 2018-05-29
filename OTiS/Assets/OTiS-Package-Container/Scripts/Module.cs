using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ModuleType { Weapon, Shields, Thrust, Radar };
public enum ShipWeaponType { Cannon, Laser, Missles };
public abstract class Module {

    protected Dictionary<string, int> statBonuses;
    public  Module()
    {
       
    }

    public virtual Module Clone()
    {
        return this;
    }

    public virtual void Apply(SpaceShip ship)
    {
        foreach(KeyValuePair<string, int> item in statBonuses)
        {
            ship.changeStat(item.Key, item.Value);
        }
    }

    public virtual void Remove(SpaceShip ship)
    {
        foreach (KeyValuePair<string, int> item in statBonuses)
        {
            ship.changeStat(item.Key, -item.Value);
        }
    }

}

public class WeaponModule : Module
{
    public Attack attack;
    public ShipWeaponType type;
    //YOW DUN DUN DUN, DUN DUN DUN
    public WeaponModule(ShipWeaponType type, Attack a, Dictionary<string, int> statUps)
    {
        this.type = type;
        attack = a;
    }

    public override void Apply(SpaceShip ship)
    {
        base.Apply(ship);
        ship.Attacks.Add(attack);
    }

    public override void Remove(SpaceShip ship)
    {
        base.Remove(ship);
        ship.Attacks.Remove(attack);
    }

}
