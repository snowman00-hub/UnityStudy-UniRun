using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    private float width;

    private void Start()
    {
        var sr = GetComponent<SpriteRenderer>();
        width = sr.sprite.rect.width / sr.sprite.pixelsPerUnit;
    }

    private void Update()
    {
        if(transform.position.x < -width)
        {
            transform.position += new Vector3(width * 2, 0f, 0f);
        }
    }
}
