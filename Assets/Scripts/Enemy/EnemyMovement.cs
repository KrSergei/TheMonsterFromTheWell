using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private bool _isTurnToRight = true;
    [SerializeField] private bool _isTarget = false;  //����, ��� ���� ���� � ���� ���������
    [SerializeField] private bool isCicle = false;    //����  �������� �������� ������� �� ������ ��� �� �����

    public GameObject spotStorage;              //������ ����� ��������������
    MovementSpotStorage movementSpotStorage;

    
    private int _countMovementSpot;

    private Transform _targetToAttack;       //���� ��� ���������
    private Transform _destination;          //����� ����������
    private int _currentSpot;                //������� �����
    private float _minDistanceForChangeTargetSpot = .2f; //����������� ���������, �� ������� ���������� ����� ����� ������������
    private bool _forward;                   //����������� ��������

    private void Awake()
    {
        movementSpotStorage = spotStorage.GetComponent<MovementSpotStorage>();
    }

    void Start()
    {
        _forward = true;
        _currentSpot = 0;                            //��������� ������� ����� ������������ 
        _destination = movementSpotStorage.GetNextMovementSpot(_currentSpot);     //��������� �������� ����� ������������
        _countMovementSpot = movementSpotStorage.GetSpotsLength();
    }
   
    void Update()
    {
        ChoiceTargeToMovement();
        ChangeSpotForMovement();
    }

    /// <summary>
    /// ����� ���� �������� - ����� �������������� ��� ���� �����
    /// </summary>
    private void ChoiceTargeToMovement()
    {
        if (!_isTarget)
        {
            DoMove(_destination);
            TurnSpriteRenderInRightDirection(_destination);
        } 
        else
        {
            DoMove(_targetToAttack);
            TurnSpriteRenderInRightDirection(_targetToAttack);
        }
    }

    /// <summary>
    /// ��������� �����, ��� ���� ������� ��� ���
    /// </summary>
    /// <param name="value"></param>
    public void SetTargetValue(bool value) => _isTarget = value;

    /// <summary>
    /// ���������  ������� ����
    /// </summary>
    /// <param name="target"></param>
    public void SetTargetPosition(GameObject target) => _targetToAttack = target.transform;

    /// <summary>
    /// ������������ � ������� ����
    /// </summary>
    /// <param name="currentDestination"></param>
    private void DoMove(Transform currentDestination)
    {
        transform.position = Vector2.MoveTowards(transform.position, currentDestination.position, _speed * Time.deltaTime);
    }

    /// <summary>
    /// ������� � ����������� �� ����������� �������� � ����
    /// </summary>
    private void TurnSpriteRenderInRightDirection(Transform currentTarget)
    {
        if ((!GetDirection(currentTarget) && _isTurnToRight) || (GetDirection(currentTarget) && !_isTurnToRight))
        {
            transform.localScale *= new Vector2(-1, 1);
            _isTurnToRight = !_isTurnToRight;
        }
    }

    /// <summary>
    /// ����������� ����������� � ����������� �� ����
    /// </summary>
    /// <returns></returns>
    private bool GetDirection(Transform currentTarget)
    {
        return (currentTarget.position.x <= transform.position.x) ? false : true; 
    }
    private void ChangeSpotForMovement()
    {
        //�������� �� ���������� ����� �������� � ������� ������, ���� ��� ������, ��� ����������� ����������, �� ������������ ����� ������������
        if (Vector2.Distance(transform.position, _destination.position) <= _minDistanceForChangeTargetSpot)
        {
            if (_forward)
                _currentSpot++;
            else
                _currentSpot--;

            //�������� �� ����� ������� ������� ����� � ��������� ����� �����������
            if (_currentSpot >= _countMovementSpot && isCicle) 
                _currentSpot = 0;
            else if (_currentSpot >= _countMovementSpot && !isCicle)
            {
                _forward = false;
                _currentSpot = _countMovementSpot - 2;
            }
            else if (_currentSpot < 0)
            {
                _forward = true;
                _currentSpot = 1;
            }
             _destination = movementSpotStorage.GetNextMovementSpot(_currentSpot);
        }
    }
}
