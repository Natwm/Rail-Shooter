using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    int GetDamage(float damage);
    
    bool GetKill();

    bool IsDead();


}
