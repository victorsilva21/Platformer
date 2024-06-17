using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private CharacterAnimation _chaAnim;
    [SerializeField] private float _speed = 0.15f;
    [SerializeField] private float _jumpStrenght = 1;
    private bool _onGround = true;
    private Vector2 _movement;
    private bool _jump = false;
    public float _scaleX = 1;

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
        _movement = new Vector2(Input.GetAxis("Horizontal"), 0) * _speed * Time.deltaTime * 10;

    }
    private void JumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jump = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))

        {
            _jump = false;
        }
    }
    void RunMovement()
    {

        _rb.velocity = new Vector2(_movement.x, _rb.velocity.y);
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
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 3)
        {
            _onGround = true;
            _chaAnim.CancelJump();
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == 3)
        {
            _onGround = false;
            _chaAnim.Jump();
        }
    }
}
