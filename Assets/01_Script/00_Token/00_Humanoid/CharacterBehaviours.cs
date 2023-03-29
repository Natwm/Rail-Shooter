using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBehaviours : TokenBehaviours,IObserver<float>
{
    [SerializeField] private List<BodyPartBehaviours> _ListOfBodyPart;

    private void Start()
    {
        _ListOfBodyPart.ForEach((part) => part.AddObserver(this));
    }

    protected abstract void Hide();
    
    protected abstract void Attack();
    
    protected abstract void Reload();
    
    public void GetNotify(float param)
    {
        GetDamage(param);
    }
}
