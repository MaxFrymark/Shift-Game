using UnityEngine;

public class OffsetScrolling : MonoBehaviour
{
    [SerializeField] private Renderer rend;
    [SerializeField] private float scrollSpeed = 8f;

    private Vector2 savedOffset;

    void Start()
    {
        rend.sharedMaterial.mainTexture.wrapMode = TextureWrapMode.Repeat;
    }
    
    void Update()
    {
        float x = Mathf.Repeat(Time.time * scrollSpeed, 1);
        Vector2 offset = new Vector2(x, 0);
        rend.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
