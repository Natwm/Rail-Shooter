using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterShootingSystem : MonoBehaviour
{
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     if(Input.GetKeyDown(KeyCode.A))
         Shoot();
    }

    private void Shoot()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        
        Ray ShootingRay = Camera.main.ScreenPointToRay(mousePosition);

        RaycastHit hitData;
        
        if (Physics.Raycast(ShootingRay, out hitData, Mathf.Infinity))
        {

            print("You hit something");

        }

        Debug.DrawRay(ShootingRay.origin,ShootingRay.direction * 1000,
            hitData.collider == null ? Color.green : Color.red,10f);
    }
}
