using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviours : CharacterBehaviours
{
    [SerializeField] private TMPro.TextMeshPro textInfo;
    
    #region Override Method
    
    public override int GetDamage(float damage)
    {
        if(damage < 0)
            Debug.LogError("damage can't be negatif");

        _LifePoint -= damage;

        textInfo.text = "Life : " + _LifePoint;
        
        if (IsDead())
        {
            GetKill();
        }
        
        return (int)damage;
        //throw new System.NotImplementedException();
    }

    protected override bool GetKill()
    {
        characterRenderer.material.color = Color.red;
        textInfo.text = "dead";
        Destroy(gameObject,1f);
        return true;
    }

    protected override bool IsDead()
    {
        return _LifePoint <= 0;
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
