using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface IAttacker<T> {

    int Attack(T target);

}


public interface IDamageable<T>
{
    int Damage(T damageTaken);
}

public interface ISubject
{
    string SubjectReference();
}