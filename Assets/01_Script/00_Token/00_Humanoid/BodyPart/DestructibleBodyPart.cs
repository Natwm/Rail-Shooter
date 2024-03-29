using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleBodyPart : BodyPartBehaviours
{
    [SerializeField] private float _LifePoint;

    
    #region Override Method
    public override int GetDamage(float damage)
    {
        bool doGetDamage = base.GetDamage(damage) > 0;

        if (!doGetDamage)
            return -1;

        if (IsDead())
            GetKill();

        return 1;
    }

    protected override bool GetKill()
    {
        return _LifePoint < 0;
    }
    #endregion
    
    
}
