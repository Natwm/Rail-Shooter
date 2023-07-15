using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBehaviours : TokenBehaviours,IObserver<float>
{
    [SerializeField] private List<BodyPartBehaviours> _ListOfBodyPart;

    [SerializeField] protected List<WeaponScriptableObject> _ListOfWeapon;

    [SerializeField] protected WeaponScriptableObject _CurrentWeapon;

    
    public WeaponScriptableObject CurrentWeapon => _CurrentWeapon;

    #region Base Unity Methods
    private void Start()
    {
        _ListOfBodyPart.ForEach((part) => part.AddObserver(this));
        if (_CurrentWeapon == null)
            _CurrentWeapon = _ListOfWeapon[0];
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
