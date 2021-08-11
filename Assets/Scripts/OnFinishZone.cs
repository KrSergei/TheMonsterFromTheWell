using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnFinishZone : MonoBehaviour
{
    public GameObject finishZone;

    public void ActivateFinishZone()
    {
        if (CheckActiveOrNotFinishZone())
            finishZone.SetActive(true);
    }

    private bool CheckActiveOrNotFinishZone()
    {
       return (finishZone.activeInHierarchy) ? false : true;
    }
}
