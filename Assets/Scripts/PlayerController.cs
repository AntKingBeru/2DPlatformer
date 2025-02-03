using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _playerRb;
    private Animator _animator;
    private float _speed;
    private bool _isGrounded = true;
    // Start is called before the first frame update
    private void Start()
    {
        _playerRb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _speed = 0.15f;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float xPos = transform.position.x;
        float yPos = transform.position.y;

        if (Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector2(xPos-_speed, yPos);
            transform.localScale = new Vector2(-1f, 1f);
            _animator.SetBool("isWalking", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector2(xPos+_speed, yPos);
            transform.localScale = new Vector2(1f, 1f);
            _animator.SetBool("isWalking", true);
        }
        else
        {
            _animator.SetBool("isWalking", false);
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && _isGrounded)
        {
            _playerRb.AddForce(new Vector2(0, 500));
            _isGrounded = false;
            _animator.SetBool("isJumping", true);
            _animator.SetBool("isGrounded", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            if(other.contacts[0].normal.y == 1)
            {
                _isGrounded = true;
                _animator.SetBool("isJumping", false);
                _animator.SetBool("isGrounded", true);
            }
        }
    }
}
