using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public abstract class CharacterBehaviours : TokenBehaviours,IObserver<float>
{
    [BoxGroup("Body")][SerializeField] private List<BodyPartBehaviours> _ListOfBodyPart;

    [BoxGroup("Weapon")][SerializeField] protected List<WeaponScriptableObject> _ListOfWeapon;

    [BoxGroup("Weapon")][SerializeField] protected WeaponScriptableObject _CurrentWeapon;

    
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

    public void OnNotify(float param)
    {
        GetDamage(param);
    }

    #endregion

}
