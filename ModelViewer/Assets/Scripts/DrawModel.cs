using UnityEngine;

public class DrawModel : MonoBehaviour
{
    [SerializeField] public Camera mainCamera;
    [SerializeField] public Texture2D texture;
    [SerializeField] public Color brushColor = Color.red;
    [SerializeField] public float brushSize = 10f;

    private Renderer renderer;
    private Color[] originalPixels;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        originalPixels = texture.GetPixels();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                // Calculate UV coordinates of the hit point
                Vector2 uv = hit.textureCoord;
                uv.x *= texture.width;
                uv.y *= texture.height;

                // Update the pixel color in the texture
                for (int x = (int)(uv.x - brushSize / 2); x < (int)(uv.x + brushSize / 2); x++)
                {
                    for (int y = (int)(uv.y - brushSize / 2); y < (int)(uv.y + brushSize / 2); y++)
                    {
                        if (x >= 0 && x < texture.width && y >= 0 && y < texture.height)
                        {
                            int index = y * texture.width + x;
                            texture.SetPixel(x, y, brushColor);
                            Debug.Log("APPLYING COLOR");
                        }
                    }
                }

                // Apply the modified texture to the object
                texture.Apply();
                
                

                // Assign the updated texture to the object's material
                renderer.material.mainTexture = texture;
            }
        }
    }

    private void OnDestroy()
    {
        // Restore the original texture pixels when the script is destroyed
        texture.SetPixels(originalPixels);
        texture.Apply();
    }
}