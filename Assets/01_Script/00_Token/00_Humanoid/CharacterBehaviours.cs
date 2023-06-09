using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBehaviours : TokenBehaviours,IObserver<float>
{
    [SerializeField] private List<BodyPartBehaviours> _ListOfBodyPart;

    #region Base Unity Methods
    private void Start()
    {
        _ListOfBodyPart.ForEach((part) => part.AddObserver(this));
    }
    
    #endregion

    #region Abstract Methods

    protected abstract void Hide();
    
    protected abstract void Attack();
    
    protected abstract void Reload();

    #endregion


    #region Observer Subject

    public void GetNotify(float param)
    {
        GetDamage(param);
    }

    #endregion
    
}
