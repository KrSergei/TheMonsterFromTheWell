using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _throwUpForce = 5f;
    [SerializeField] private float _gravityForce = 5f;
    [SerializeField] private GameObject prefabTakenObj;

    public GameObject mainCamera;
    public GameObject UIManager;
    PlayerMovement _playerMovement;

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }


    /// <summary>
    /// Имитация падения при столкновении с врагом
    /// </summary>
    public void CauseDamage()
    {
        SetInactiveCollider();
        OnGameOverActivity();
        OffFollowCamera();
        ThrowUP();
        SetGravityForce();
    }

    /// <summary>
    /// Отключение следования камеры за персонажем
    /// </summary>
    public void OffFollowCamera()
    {
        mainCamera.transform.GetComponent<CameraFollow>().SetStatusPlayerAliveOrNot(false);
    }

    public void OnGameOverActivity()
    {
        if(prefabTakenObj !=null)
            prefabTakenObj.GetComponent<Star>().StoptFollowToPlayer();
        StopTimer();
        ShowGameOverMenu(false);
    }

    private void ThrowUP()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * _throwUpForce, ForceMode2D.Impulse);
    }

    public void SetInactiveCollider()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void SetGravityForce()
    {
        GetComponent<Rigidbody2D>().gravityScale = _gravityForce;
    }

    public void SetReferenceOnTakenObj(GameObject obj)
    {
        prefabTakenObj = obj;
    }

    public void SetEndGame()
    {
        _playerMovement.PlayOfMotionAnimation("EndGame");
        _playerMovement.SetNotControl();
        ShowGameOverMenu(true);
        StopTimer();
    }

    private void ShowGameOverMenu(bool value)
    {
        UIManager.GetComponent<UIControl>().ShowGameOverMenu(value);
    }

    private void StopTimer()
    {
        UIManager.GetComponent<UITimerControl>().StopTimer();
    }
}
