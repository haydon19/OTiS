﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {
    private Enemy enemy;


    public Enemy(string name, int ID, int strength, int smarts, int agility) : base(name, ID, strength, smarts, agility) {


    }

    public override float Damage(float damageTaken)
    {
        setStat("Health", getStat("Health") - (int)damageTaken);
        if (getStat("Health") <= 0)
        {
            setStat("Health", 0);
            Dead = true;
        }
        EnemyInfoPanel.instance.updateCharacterInfo(this);
        
        return damageTaken;
    }

}