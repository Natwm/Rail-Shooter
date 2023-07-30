using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeredObjectBehaviours : TokenBehaviours, IObserver<float>
{
    [SerializeField] private List<InteractableObject> linkedObject;

    private void Start()
    {
        if(linkedObject != null && linkedObject.Count > 0 )
            linkedObject.ForEach(item => item.AddTriggeredElement(this));
    }

    public void GetNotify(float param)
    {
        characterRenderer.gameObject.SetActive(true);
    }
    
    #region Override

    public override int GetDamage(float damage)
    {
        print("Can't be dammaged");
        return 1;
        
    }

    protected override bool GetKill()
    {
        print("Can't be destroy");
        return true;
    }

    protected override bool IsDead()
    {
        print("cant Be dead");
        return true;

    }
    #endregion
}
