using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public abstract class TokenBehaviours : MonoBehaviour
{
    [SerializeField] protected float _LifePoint;
    [BoxGroup("Render")] [SerializeField] protected MeshRenderer characterRenderer;

    private IDamageable m_DamageableImplementation;
    
    #region Abstract Methods
    public abstract int GetDamage(float damage);

    protected abstract bool GetKill();

    protected abstract bool IsDead();
    
    #endregion
}
