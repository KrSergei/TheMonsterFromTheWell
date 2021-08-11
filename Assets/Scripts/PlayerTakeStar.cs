using UnityEngine;

public class PlayerTakeStar : MonoBehaviour
{
    public Transform holdPositionForStar;
    public GameObject UIManager;
    public GameObject worldBuilder;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Star>() != null)
        {
            collision.gameObject.GetComponent<Star>().StartFollowToPlayer(holdPositionForStar);
            SaveTakenObject(collision.gameObject);
            StartTimerUphill();
            ActivateFinishZone();
        } 
    }

    private void ActivateFinishZone()
    {
        worldBuilder.GetComponent<OnFinishZone>().ActivateFinishZone();
    }

    private void SaveTakenObject(GameObject takenObject)
    {
        GetComponent<Player>().SetReferenceOnTakenObj(takenObject);
    }

    private void StartTimerUphill()
    {
        UIManager.GetComponent<UITimerControl>().StartTimerUphill();
    }
}
