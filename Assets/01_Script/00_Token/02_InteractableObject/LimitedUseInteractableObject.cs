using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedUseInteractableObject : InteractableObject
{
    // Start is called before the first frame update
    public override int GetDamage(float damage)
    {
        base.GetDamage(0);
        _LifePoint--;
        if (IsDead())
            GetKill();
        
        return (int)_LifePoint;
    }

    protected override bool GetKill()
    {
        gameObject.SetActive(false);

        return true;
    }

    protected override bool IsDead()
    {
        return _LifePoint <= 0;
    }
}
