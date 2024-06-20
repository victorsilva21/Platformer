using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private CharacterAnimation _chaAnim;
    [SerializeField] private float _speed = 0.15f;
    [SerializeField] private float _jumpStrenght = 1;
    private bool _onGround = true;
    private bool _doubleJump = true;
    private bool _doubleJumpAction = false;
    private Vector2 _movement;
    private bool _jump = false;
    public float _scaleX = 1;
    private GameObject _platform;

    // Start is called before the first frame update
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _scaleX = gameObject.transform.localScale.x;

    }
    private void Update()
    {
        RunInput();
        JumpInput();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RunMovement();
        Jump();
    }
    private void RunInput()
    {
        _movement = new Vector2(Input.GetAxis("Horizontal"), 0).normalized;

    }
    private void JumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Input.GetAxis("Vertical") < 0 && _platform != null)
        {
            _platform.GetComponent<BoxCollider2D>().isTrigger = true;
            _chaAnim.Jump();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && _onGround)
        {
            _jump = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && _doubleJump)
        {
            _doubleJumpAction = true;

        }
        if (Input.GetKeyUp(KeyCode.Space))

        {
            _jump = false;
            _doubleJumpAction = false;
            if (_rb.velocity.y > 4)
                _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y / 2);
        }
    }
    void RunMovement()
    {

        if (_rb.velocity.x < 6 && _rb.velocity.x > -6)
            _rb.AddForce(new Vector2(_movement.x * _speed * Time.deltaTime, 0), ForceMode2D.Force);
        if (_movement.magnitude > 0 && _onGround)
        {
            _chaAnim.Run();
        }
        else if (_onGround)
        {
            _chaAnim.Idle();
        }

        if (_movement.x < 0)
        {
            gameObject.transform.localScale = new Vector2(-_scaleX, gameObject.transform.localScale.y);
        }
        else if (_movement.x > 0)
        {
            gameObject.transform.localScale = new Vector2(_scaleX, gameObject.transform.localScale.y);
        }
    }
    void Jump()
    {
        if (_jump && _onGround)
        {
            _onGround = false;
            _rb.AddForce(Vector2.up * _jumpStrenght * Time.deltaTime, ForceMode2D.Impulse);
            _chaAnim.Jump();


        }
        if (_doubleJumpAction && _doubleJump)
        {
            _onGround = false;
            _rb.velocity = new Vector2(_rb.velocity.x, 0);

            _rb.AddForce(Vector2.up * _jumpStrenght * Time.deltaTime, ForceMode2D.Impulse);
            _chaAnim.DoubleJump();
            _doubleJump = false;
        }




    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 3 || (other.gameObject.layer == 6 && _rb.velocity.y < 0.5f))
        {
            _onGround = true;
            _doubleJump = true;
            _chaAnim.CancelJump();
            if (other.gameObject.layer == 6)
            {
                _platform = other.gameObject;
            }
        }

    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == 3 || other.gameObject.layer == 6)
        {
            _onGround = false;
            _chaAnim.Jump();
            if (other.gameObject.layer == 6 && _platform != null)
            {
                _platform.GetComponent<BoxCollider2D>().isTrigger = false;
                _platform = null;
            }
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)
        {
            _platform.GetComponent<BoxCollider2D>().isTrigger = false;
            _platform = null;
        }
    }

}
