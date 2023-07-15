using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(CharacterBehaviours))]
public class CharacterShootingSystem : MonoBehaviour
{
    private CharacterBehaviours _TokenBehaviours;

    private void Start()
    {
        _TokenBehaviours = transform.GetComponent<CharacterBehaviours>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.nearClipPlane;
        
            Ray ShootingRay = Camera.main.ScreenPointToRay(mousePosition);

            if(_TokenBehaviours.CurrentWeapon != null)
                _TokenBehaviours.CurrentWeapon.Shoot(transform.position,ShootingRay);
        }
    }
}
