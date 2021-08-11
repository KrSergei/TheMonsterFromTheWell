using System.Collections;
using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField] private bool _followToPlayer = false;
    private Transform _holdPosition;
    public float _dumping = 1.5f;
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_followToPlayer)
        {
            StartCoroutine(Move());
        }
    }

    public void SetHoldPosition(Transform position)
    {
        _holdPosition = position;
    }

    public Transform GetHoldPosition()
    {
        return _holdPosition;
    }

    IEnumerator Move()
    {
        Vector3 currentPosition = Vector3.Lerp(transform.position, _holdPosition.position, _dumping * Time.deltaTime);
        transform.position = currentPosition;
        yield return null;
    }

    public void StartFollowToPlayer(Transform position)
    {
        _followToPlayer = true;
        SetHoldPosition(position);
        StartPlayAnimationIsRaised();
    }

    private void StartPlayAnimationIsRaised()
    {
        anim.SetBool("IsRaised", _followToPlayer);
    }

    public void StoptFollowToPlayer()
    {
        _followToPlayer = false;
        StopCoroutine(Move());
        SetHoldPosition(transform);
        StopPlayAnimationIsRaised();
    }

    public bool SetFlagFollowToPlayer()
    {
        return _followToPlayer;
    }

    public void StopPlayAnimationIsRaised()
    {
        anim.SetBool("IsRaised", _followToPlayer);
    }
}
