using System.Collections;
using UnityEngine;

public class DrawWithMouse : MonoBehaviour
{
    [SerializeField] private Shader drawShader;
    [SerializeField] private Shader paintShader;
    [SerializeField] private Vector2Int renderTextureSize = new Vector2Int(1024, 1024);
    private Material drawMaterial;
    private Material currentMaterial;
    private RenderTexture splatMap;
    public LayerMask collisionLayer;
    private void Awake()
    {
/*        Material material = new Material(paintShader);
        gameObject.GetComponent<Renderer>().material = material;*/
    }
    private void Start()
    {
        drawMaterial = new Material(drawShader);
        currentMaterial = GetComponent<MeshRenderer>().material;
        // Create a new unique RenderTexture for each object
        splatMap = new RenderTexture(renderTextureSize.x, renderTextureSize.y, 0, RenderTextureFormat.ARGBFloat);
        splatMap.enableRandomWrite = true;
        splatMap.Create();
        Debug.Log("RenderTexture Instance ID: " + splatMap.GetInstanceID());
        Debug.Log("Object Instance ID: " + gameObject.GetInstanceID());
        currentMaterial.SetTexture("_SplatMap", splatMap);
        
    }
    public void drawOnMesh(Color color, float strength, float size, Vector4 hit)
    {
            drawMaterial.SetVector("_Coordinate", hit);
            drawMaterial.SetColor("_Color", color);
            // strength goes from -x to x and set if it erase or add
            drawMaterial.SetFloat("_Strength", strength);
            drawMaterial.SetFloat("_Size", size);
            splatMap.Create();
            RenderTexture temp = RenderTexture.GetTemporary(splatMap.width, splatMap.height, 0, RenderTextureFormat.ARGBFloat);
            temp.enableRandomWrite = true;
            temp.Create();
            Graphics.Blit(splatMap, temp);
            Graphics.Blit(temp, splatMap, drawMaterial);

            RenderTexture.ReleaseTemporary(temp);
    }
}
