using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class interactibleExplosifObstacle : LimitedUseInteractableObject
{
    [SerializeField] private float m_ExplosifRadius;
    [SerializeField] private float m_MaxDistance;
    [SerializeField] private LayerMask m_TargetedLayer;
    
    [SerializeField][Min(1)] private int m_damage;
    
    public override int GetDamage(float damage)
    {
        _LifePoint--;
        if (IsDead())
            base.GetKill();
        
        return (int)_LifePoint;
    }
    
    protected override bool IsDead()
    {
        gameObject.SetActive(false);
        Destroy(this.gameObject,2f);
        RaycastHit[] targetedElement = Physics.SphereCastAll(transform.position,m_ExplosifRadius,Vector3.forward, m_MaxDistance, m_TargetedLayer);
        
        targetedElement.ToList().ForEach(ApplyDamage);
        return true;
    }

    private void ApplyDamage(RaycastHit target)
    {
        if (!target.collider.gameObject.TryGetComponent(out TokenBehaviours token))
            return;

        
        token.GetDamage(m_damage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position,m_ExplosifRadius);
    }
}
