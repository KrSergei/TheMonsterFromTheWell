using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishZoneCheckPlayer : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
            collision.gameObject.GetComponent<Player>().SetEndGame();
    }

}
