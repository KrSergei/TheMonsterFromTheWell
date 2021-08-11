using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private bool _isTurnToRight = true;
    [SerializeField] private bool _isTarget = false;  //Флаг, что есть цель в зоне видимости
    [SerializeField] private bool isCicle = false;    //Флаг  движения обратную сторону по точкам или по кругу

    public GameObject spotStorage;              //Объект точек патрулирования
    MovementSpotStorage movementSpotStorage;

    
    private int _countMovementSpot;

    private Transform _targetToAttack;       //Цель для поражения
    private Transform _destination;          //Пункт назначения
    private int _currentSpot;                //Текущая точка
    private float _minDistanceForChangeTargetSpot = .2f; //Минимальная дистанция, на которой происходит смена точки передвижения
    private bool _forward;                   //Направление движения

    private void Awake()
    {
        movementSpotStorage = spotStorage.GetComponent<MovementSpotStorage>();
    }

    void Start()
    {
        _forward = true;
        _currentSpot = 0;                            //Установка текущей точки передвижения 
        _destination = movementSpotStorage.GetNextMovementSpot(_currentSpot);     //Установка целейвой точки передвижения
        _countMovementSpot = movementSpotStorage.GetSpotsLength();
    }
   
    void Update()
    {
        ChoiceTargeToMovement();
        ChangeSpotForMovement();
    }

    /// <summary>
    /// Выбор цели движения - точка патрулирования или цель атаки
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
    /// Установка флага, что цель найдена или нет
    /// </summary>
    /// <param name="value"></param>
    public void SetTargetValue(bool value) => _isTarget = value;

    /// <summary>
    /// Получения  позиции цели
    /// </summary>
    /// <param name="target"></param>
    public void SetTargetPosition(GameObject target) => _targetToAttack = target.transform;

    /// <summary>
    /// Передвижение к текущей цели
    /// </summary>
    /// <param name="currentDestination"></param>
    private void DoMove(Transform currentDestination)
    {
        transform.position = Vector2.MoveTowards(transform.position, currentDestination.position, _speed * Time.deltaTime);
    }

    /// <summary>
    /// Поворот в зависимости от направления движения к цели
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
    /// Определения направления в зависимости от цели
    /// </summary>
    /// <returns></returns>
    private bool GetDirection(Transform currentTarget)
    {
        return (currentTarget.position.x <= transform.position.x) ? false : true; 
    }
    private void ChangeSpotForMovement()
    {
        //Проверка на расстояние между объектом и целевой точкой, если оно меньше, чем минимальное расстояние, то переключение точки передвижения
        if (Vector2.Distance(transform.position, _destination.position) <= _minDistanceForChangeTargetSpot)
        {
            if (_forward)
                _currentSpot++;
            else
                _currentSpot--;

            //Проверка за выход предела массива точек и состояние флага цикличности
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
