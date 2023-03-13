using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _Animator;
    private SpriteRenderer _Renderer;

    private void Awake()
    {
        _Animator = GetComponent<Animator>();
        _Renderer = GetComponent<SpriteRenderer>();
    }
    
    

    public void MoveAnimation(bool _is_Move, bool _move_Back, bool _move_Up)
    {
        _Animator.SetBool("IsMove", _is_Move);
        _Animator.SetBool("MoveBack", _move_Back);
        _Animator.SetBool("MoveUp", _move_Up);
    }

    public void ChangeDirection(float _end_Pos)
    {
        if (transform.position.x > _end_Pos)
            _Renderer.flipX = true;
        else
        {
            _Renderer.flipX = false;
        }

        if (transform.position.y > _end_Pos)
        {
            
        }
    }
    
}
