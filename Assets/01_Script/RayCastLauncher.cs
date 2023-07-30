using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastLauncher : MonoBehaviour
{
    RaycastHit hit;
    [SerializeField] float rayCastDistance = 2f; // Define the length of the ray here
    [SerializeField] LayerMask collisionLayer; // Define the layer(s) to perform the raycast against
    public Color eraseColor;
    [SerializeField] [Range(1, 500)] private float size;
    [SerializeField] [Range(-1, 1)] private float strength;

    void Update()
    {
        // Cast a ray from the game object's position in the up direction
        Vector3 rayDirection = gameObject.transform.up;
        Vector3 rayStartPos = gameObject.transform.position;
        Vector3 rayEndPos = rayStartPos + rayDirection * rayCastDistance;


        // Perform the raycast
        if (Physics.Raycast(rayStartPos, rayDirection, out hit, rayCastDistance, collisionLayer))
        {
            hit.transform.gameObject.GetComponent<DrawWithMouse>().drawOnMesh(eraseColor, strength, size, new Vector4(hit.textureCoord.x, hit.textureCoord.y, 0, 0));
            Debug.DrawLine(rayStartPos, rayEndPos, Color.red, 3f);
            
            // Draw the hit point in yellow
            //Debug.DrawRay(rayStartPos, rayDirection * hit.distance, Color.yellow, 3f);
        }
        else
        {
            Debug.DrawLine(rayStartPos, rayEndPos, Color.yellow, 3f);

        }
    }
}
