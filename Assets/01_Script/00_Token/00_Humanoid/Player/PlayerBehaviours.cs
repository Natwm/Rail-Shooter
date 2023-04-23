using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerBehaviours : CharacterBehaviours
{
    public TMPro.TMP_Text weaponInfo;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _CurrentWeapon = _ListOfWeapon[1];
            weaponInfo.text = "Current weapon : Instantiate a bullet";
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _CurrentWeapon = _ListOfWeapon[0];
            weaponInfo.text = "Current weapon : RayCast";
        }
    }

    #region Override Method
    
    public override int GetDamage(float damage)
    {
        if (damage < 0)
            return -1;
        
        _LifePoint -= damage;
        
        if (IsDead())
            GetKill();
        
        return (int)damage;    
    }
    

    protected override bool GetKill()
    {
        print("isDead");
        return true;
    }

    protected override bool IsDead()
    {
        return _LifePoint < 0;
    }

    protected override void Hide()
    {
        throw new System.NotImplementedException();
    }

    protected override void Attack()
    {
        throw new System.NotImplementedException();
    }

    protected override void Reload()
    {
        throw new System.NotImplementedException();
    }
    
    #endregion
    
}
