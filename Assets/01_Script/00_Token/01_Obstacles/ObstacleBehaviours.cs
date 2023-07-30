using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviours : TokenBehaviours
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Override

    public override int GetDamage(float damage)
    {
        _LifePoint -= damage;
        if (IsDead())
            GetKill();

        return (int)damage;
    }

    protected override bool GetKill()
    {
        print("dead");
        return true;
    }

    protected override bool IsDead()
    {
        return _LifePoint <= 0;
    }

    #endregion

}
