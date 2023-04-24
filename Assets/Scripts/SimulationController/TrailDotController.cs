using UnityEngine;

public class TrailDotController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    public float TimeToFadeAway = 10;

    private Color currentColor = new Color(1, 1, 1, 1);

    void Update()
    {
        currentColor.a -= Time.deltaTime / TimeToFadeAway;
        spriteRenderer.color = currentColor;

        if(currentColor.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
