using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speedMovement = 10f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _ratioForceWhenToAir = 0.5f;
    [SerializeField] private bool _isGrounded = false;
    [SerializeField] private bool _isTurnToLeft = true;
    [SerializeField] private bool _canControl = true;
    [SerializeField] private Joystick _joystick;

    public Transform groundChecker;
    public float checkGroundRadius = 0.5f;
    public LayerMask groundMask;

    private Rigidbody2D _rb;
    private Animator _animator;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (_canControl)
        {
            Jump();
            Move();
        }
    }

    private void Jump()
    {
        if (_isGrounded)
        {
            if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.Space))
            {
                _isGrounded = false;
                PlayOfMotionAnimation("Jump");
                _rb.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    private void Move()
    {
        //float directionSide = Input.GetAxisRaw("Horizontal");
        float directionSide = _joystick.Horizontal;

        if (_isGrounded)
        {
            if(_joystick.Horizontal != 0)
            {
                TurnSpriteRenderInRightDirection(directionSide);
                PlayOfMotionAnimation("Walk");
                _rb.AddForce(transform.right * _speedMovement * directionSide * Time.deltaTime);
            }
            else
            {
                SetVelosityXToZero();
                StopOfMotionAnimation("Walk");
            }

            //if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            //{
            //    TurnSpriteRenderInRightDirection(directionSide);              
            //    PlayOfMotionAnimation("Walk");
            //    _rb.AddForce(transform.right * _speedMovement * directionSide * Time.deltaTime);
            //}
            //else
            //{
            //    SetVelosityXToZero();
            //    StopOfMotionAnimation("Walk");
            //}
        }
        else
        {
            TurnSpriteRenderInRightDirection(directionSide);
            StopOfMotionAnimation("Walk");
            //Уменьшение кооэффициента ускорения, если персонаж в воздухе
            _rb.AddForce(transform.right * _speedMovement * _ratioForceWhenToAir * directionSide * Time.deltaTime);
        }
    }

    /// <summary>
    /// Метод поворота игрока в нужную сторону в зависимости от направления
    /// </summary>
    /// <param name="direction"></param>
    private void TurnSpriteRenderInRightDirection(float direction)
    {
        if ((direction < 0 && !_isTurnToLeft) || (direction > 0 && _isTurnToLeft))
        {
            transform.localScale *= new Vector2(-1, 1);
            SetVelosityXToZero();
            _isTurnToLeft = !_isTurnToLeft;
        }
    }

    private void SetVelosityXToZero()
    {
        float velocityY = _rb.velocity.y;
        _rb.velocity = new Vector2(0f, velocityY);
    }

    private void SetVelosityYToZero()
    {
        float velocityX = _rb.velocity.x;
        _rb.velocity = new Vector2(velocityX, 0f);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Ground>() != null)
        {
            if (CheckGround())
            {
                _isGrounded = true;
                PlayOfMotionAnimation("OnGround");
                SetVelosityYToZero();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Ground>() != null)
        {
            _isGrounded = false;
            StopOfMotionAnimation("OnGround");
        }
    }

    public void PlayOfMotionAnimation(string nameAnimation)
    {
        _animator.SetBool(nameAnimation, true);
    }

    public void StopOfMotionAnimation(string nameAnimation)
    {
        _animator.SetBool(nameAnimation, false);
    }

    private bool CheckGround()
    {
        bool isGround = Physics2D.OverlapCircle(groundChecker.position, checkGroundRadius, groundMask);
        return isGround;
    }

    public bool CheckFalldownOrNot()
    {
        return (_rb.velocity.y <= 0) ? true : false;  
    }

    public void SetNotControl()
    {
        _canControl = false;
        StopOfMotionAnimation("Walk");
    }
}
