using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : TokenBehaviours
{
    public List<IObserver<float>> triggeredObject = new List<IObserver<float>>();

    public void AddTriggeredElement(IObserver<float> obj)
    {
        if(!triggeredObject.Contains(obj))
            triggeredObject.Add(obj);
    }
    
    #region Override
    public override int GetDamage(float damage)
    {
        triggeredObject[0].GetNotify(0);
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
