using UnityEngine;

public class RayCastFromCamera : MonoBehaviour
{
    RaycastHit hit;
    [SerializeField] float rayCastDistance = 2f; // Define the length of the ray here
    [SerializeField] LayerMask collisionLayer; // Define the layer(s) to perform the raycast against
    public Color eraseColor;
    [SerializeField] [Range(1, 500)] private float size;
    [SerializeField] [Range(-100, 100)] private float strength;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Perform the raycast
            if (Physics.Raycast(ray, out hit, rayCastDistance, collisionLayer))
            {
                hit.transform.gameObject.GetComponent<DrawWithMouse>().drawOnMesh(eraseColor, strength, size, new Vector4(hit.textureCoord.x, hit.textureCoord.y, 0, 0));

                // Handle the hit point here (you can use hit.point)
                Debug.DrawLine(ray.origin, hit.point, Color.yellow, 3f);
            }
            else
            {
                // If the raycast doesn't hit anything, you can do something else
                // For example, you can draw a debug line to the raycast endpoint
                Vector3 raycastEndPoint = ray.GetPoint(rayCastDistance);
                Debug.DrawLine(ray.origin, raycastEndPoint, Color.green, 3f);
            }
        }
    }
}
