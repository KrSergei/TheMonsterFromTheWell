using UnityEngine;

public class LockedTarget : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            GetComponentInParent<EnemyMovement>().SetTargetValue(true);
            GetComponentInParent<EnemyMovement>().SetTargetPosition(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            GetComponentInParent<EnemyMovement>().SetTargetValue(false);
        }
    }
}
