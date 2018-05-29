using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType { Ballistic, Plasma, Ray, Energy };

public class Attack  {

    int min, max;
    public Attack(int min, int max)
    {
        this.min = min;
        this.max = max;
    }

}
