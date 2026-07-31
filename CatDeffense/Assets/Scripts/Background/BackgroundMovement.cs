using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    private Material material2;
    public float pace = 0.1f;

    private void Awake()
    {
        material2 = GetComponent<SpriteRenderer>().material;
    }
 
    void Update()
    {
        material2.SetTextureOffset("_MainTex", new Vector2(Time.time * pace, 0));
    }
}

