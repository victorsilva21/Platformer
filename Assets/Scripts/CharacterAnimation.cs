using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rb;

    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Gravity();
    }
    private void Gravity()
    {
        _animator.SetFloat("ySpeed", _rb.velocity.y);
    }

    public void Idle()
    {
        _animator.SetBool("run", false);
    }
    public void Run()
    {
        _animator.SetBool("run", true);
    }
    public void Jump()
    {
        _animator.SetBool("onGround", false);
    }
    public void DoubleJump()
    {
        _animator.SetTrigger("doubleJump");

    }
    public void CancelJump()
    {
        _animator.SetBool("onGround", true);
        _animator.ResetTrigger("doubleJump");
    }
}
