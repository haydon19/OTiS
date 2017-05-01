using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {
    private Enemy enemy;


    public Enemy(string name, int ID, int strength, int Mind, int agility, int piloting) : base(name, ID, strength, Mind, agility, piloting) {


    }

    public override int Damage(int damageTaken)
    {
        setStat("Health", getStat("Health") - damageTaken);
        if (getStat("Health") <= 0)
        {
            setStat("Health", 0);
            Dead = true;
        }
        EnemyInfoPanel.instance.updateCharacterInfo(this);
        
        return damageTaken;
    }

}
