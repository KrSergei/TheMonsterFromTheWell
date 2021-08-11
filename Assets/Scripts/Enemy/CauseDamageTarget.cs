using UnityEngine;

public class CauseDamageTarget : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            collision.gameObject.GetComponent<Player>().CauseDamage();
        }
    }
}
