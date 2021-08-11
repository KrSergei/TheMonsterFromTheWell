using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float _dumping = 1.5f;
    public float _targetPosYForLockCamera = -111f;
    public bool isPlayerAlive = true;
    private float _positionCameraX = 0f;
    private float _positionCameraZ = 10f;
    [SerializeField] private bool _trackingTarget = true;

    void Start()
    {
        transform.position = new Vector3()
        {
            x = _positionCameraX,
            y = target.position.y,
            z = target.position.z - _positionCameraZ,
        };
    }
        
    void Update()
    {
        if (isPlayerAlive)
        {
            CheckTargetPosYForLockOrUnlockCamera();
            if (_trackingTarget)
            {
                Vector3 playerTransform = new Vector3()
                {
                    x = _positionCameraX,
                    y = target.position.y,
                    z = target.position.z - _positionCameraZ,
                };
                Vector3 currentPosition = Vector3.Lerp(transform.position, playerTransform, _dumping * Time.deltaTime);
                transform.position = currentPosition;
            }
        }
    }
    
    public void SetStatusPlayerAliveOrNot(bool value)
    {
        isPlayerAlive = value;
    } 

    private void CheckTargetPosYForLockOrUnlockCamera()
    {
        if ((transform.position.y <= _targetPosYForLockCamera))
            _trackingTarget = false;
        if (target.position.y > _targetPosYForLockCamera)
            _trackingTarget = true;
    }
}
