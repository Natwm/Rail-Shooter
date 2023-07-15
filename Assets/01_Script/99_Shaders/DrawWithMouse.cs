using System.Collections;
using UnityEngine;

public class DrawWithMouse : Singleton<DrawWithMouse>
{
    [SerializeField] private GameObject raycastObject;
    [SerializeField] private Shader drawShader;
    [SerializeField] private Vector2Int renderTextureSize = new Vector2Int(1024, 1024);
    private Material drawMaterial;
    private Material currentMaterial;
    public RenderTexture splatMap;
    public Color eraseColor;
    public int rayCastDistance = 2;
    
    [SerializeField][Range(1, 500)] private float size;
    [SerializeField][Range(-1, 1)] private float strength;
    public Material eraseMaterial; // The material with the erase shader
    public Texture2D textureToErase; // The texture to be erased

    public LayerMask collisionLayer;
    private void Start()
    {
        drawMaterial = new Material(drawShader);
        currentMaterial = GetComponent<MeshRenderer>().material;
        splatMap = new RenderTexture(renderTextureSize.x, renderTextureSize.y, 0, RenderTextureFormat.ARGBFloat);
        currentMaterial.SetTexture("_SplatMap", splatMap);
        
    }
    private void Update()
    {
        //addDirtyness();
        removeDirtyness(eraseColor, raycastObject, drawMaterial, strength, size, splatMap);
    }
    WaitForEndOfFrame frameEnd = new WaitForEndOfFrame();
    private void LateUpdate()
    {
        //getPercentage();
/*        float a = CountErasedPixels();
        Debug.Log(a);*/
    }
    public void addDirtyness(GameObject obj, float size)
    {
        AddDirtOnsurface(eraseColor, obj, drawMaterial, -strength, size, splatMap);
    }

    public void AddDirtOnsurface(Color color, GameObject raycastObject, Material drawMaterial, float strength, float size, RenderTexture splatMap)
    {
        RaycastHit hit;
        Debug.DrawRay(raycastObject.transform.localPosition, -VectorsMethods.GetDirection(raycastObject.transform.position, Vector3.up), Color.red, 1000);

        if (Physics.Raycast(raycastObject.transform.localPosition, -VectorsMethods.GetDirection(raycastObject.transform.position,Vector3.up), out hit, Mathf.Infinity,
                collisionLayer))
        {
            Debug.DrawRay(raycastObject.transform.localPosition, -VectorsMethods.GetDirection(raycastObject.transform.position, Vector3.up), Color.yellow, 1000);
            drawMaterial.SetVector("_Coordinate", new Vector4(hit.textureCoord.x, hit.textureCoord.y, 0, 0));

            drawMaterial.SetColor("_Color", color);
            drawMaterial.SetFloat("_Strength", strength);
            drawMaterial.SetFloat("_Size", size);
            RenderTexture temp = RenderTexture.GetTemporary(splatMap.width, splatMap.height, 0, RenderTextureFormat.ARGBFloat);
            Graphics.Blit(splatMap, temp);
            Graphics.Blit(temp, splatMap, drawMaterial);

            RenderTexture.ReleaseTemporary(temp);
        }
    }
    
    public void removeDirtyness(Color color, GameObject raycastObject, Material drawMaterial, float strength, float size, RenderTexture splatMap)
    {
        RaycastHit hit;
        Debug.DrawRay(raycastObject.transform.position, raycastObject.transform.up, Color.red, 1000);

        if (Physics.Raycast(raycastObject.transform.position, raycastObject.transform.up, out hit,rayCastDistance,
                collisionLayer))
        {
            Debug.DrawRay(raycastObject.transform.position, raycastObject.transform.up, Color.yellow, 1000);
            drawMaterial.SetVector("_Coordinate", new Vector4(hit.textureCoord.x, hit.textureCoord.y, 0, 0));

            drawMaterial.SetColor("_Color", color);
            drawMaterial.SetFloat("_Strength", strength);
            drawMaterial.SetFloat("_Size", size);
            RenderTexture temp = RenderTexture.GetTemporary(splatMap.width, splatMap.height, 0, RenderTextureFormat.ARGBFloat);
            Graphics.Blit(splatMap, temp);
            Graphics.Blit(temp, splatMap, drawMaterial);

            RenderTexture.ReleaseTemporary(temp);
        }
    }
    public float CalculateErasedPercentage(RenderTexture splatMap, Color eraseColor)
    {
        RenderTexture.active = splatMap;

        // Read the pixels from the RenderTexture
        Texture2D texture = new Texture2D(splatMap.width, splatMap.height);
        texture.ReadPixels(new Rect(0, 0, splatMap.width, splatMap.height), 0, 0);
        texture.Apply();

        // Count the pixels that match the erase color
        Color[] pixels = texture.GetPixels();
        int totalPixels = pixels.Length;
        int erasedPixels = 0;
        for (int i = 0; i < totalPixels; i++)
        {
            if (pixels[i] == eraseColor)
            {
                erasedPixels++;
            }
        }

        // Calculate the percentage of erased dirtiness
        float percentage = (float)erasedPixels / totalPixels * 100f;

        // Clean up resources
        RenderTexture.active = null;
        Destroy(texture);

        return percentage;
    }
    public float checkPercentage()
    {
        return CalculateErasedPercentage(splatMap, eraseColor);
    }


}
