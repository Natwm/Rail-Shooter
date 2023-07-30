using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObstacleBehaviours : ObstacleBehaviours
{
    protected override bool GetKill()
    {
        gameObject.SetActive(false);
        return true;
    }
}
