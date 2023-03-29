using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TokenBehaviours : MonoBehaviour
{
    [SerializeField] protected float _LifePoint;
    private IDamageable m_DamageableImplementation;
    protected abstract int GetDamage(float damage);

    protected abstract bool GetKill();

    protected abstract bool IsDead();
}
