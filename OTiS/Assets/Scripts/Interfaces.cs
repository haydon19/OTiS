using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface IAttacker<T> {

    float Attack(T target);

}


public interface IDamageable<T>
{
    float Damage(T damageTaken);
}

public interface ISubject
{
    string SubjectReference();
}