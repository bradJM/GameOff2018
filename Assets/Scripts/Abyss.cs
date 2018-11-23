using UnityEngine;

public class Abyss : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        var body = other.GetComponent<Rigidbody2D>();
        body.transform.position = new Vector2(-3f, -1f);
        body.velocity = new Vector2(0f, 0f);
    }
}
