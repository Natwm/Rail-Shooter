using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartBehaviours : TokenBehaviours
{
    private List<IObserver<float>> _ListofObserver = new List<IObserver<float>>();

    [SerializeField] [Min(1)] protected float _DamageMultiplicator;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GetDamage(2);
        }
    }

    #region Observer

    public void NotifyObserver(float param)
    {
        _ListofObserver.ForEach((observer) => observer.GetNotify(param));
    }

    public void AddObserver(IObserver<float> subject)
    {
        _ListofObserver.Add(subject);
    }

    public void RemoveObserver(IObserver<float> subject)
    {
        _ListofObserver.Remove(subject);
    }

    #endregion


    #region Override Methods

    protected override int GetDamage(float damage)
    {
        if (damage < 0)
            return -1;

        float value = damage * _DamageMultiplicator;

        NotifyObserver(value);

        return 1;
    }

    protected override bool GetKill()
    {
        throw new NotImplementedException();
    }

    protected override bool IsDead()
    {
        throw new NotImplementedException();
    }

    #endregion
}