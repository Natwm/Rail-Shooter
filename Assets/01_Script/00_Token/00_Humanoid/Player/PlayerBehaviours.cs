using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviours : CharacterBehaviours
{
    
    protected override int GetDamage(float damage)
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
}
