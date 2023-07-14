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
    

    public void StartMove()
    {
        _Animator.SetBool("IsMove", true);
    }

    public void StopMove()
    {
        _Animator.SetBool("IsMove", false);
        
        _Animator.SetBool("MoveBack", false);
        _Animator.SetBool("MoveUp", false);
        _Animator.SetBool("MoveDown", false);
    }

    public void MoveRight(float _end_Pos)
    {
        _Renderer.flipX = true;
        _Animator.SetBool("MoveBack", true);
        
        _Animator.SetBool("MoveUp", false);
        _Animator.SetBool("MoveDown", false);
        
        
    }
    
    public void MoveLeft(float _end_Pos)
    {
        _Renderer.flipX = false;
        _Animator.SetBool("MoveBack", true);
        
        _Animator.SetBool("MoveUp", false);
        _Animator.SetBool("MoveDown", false);
    }
    
    public void MoveUp(float _end_Pos)
    {
        _Animator.SetBool("MoveUp", true);
        
        _Animator.SetBool("MoveBack", false);
        _Animator.SetBool("MoveDown", false);
    }
    
    public void MoveDown(float _end_Pos)
    {
        _Animator.SetBool("MoveDown", true);
        
        _Animator.SetBool("MoveBack", false);
        _Animator.SetBool("MoveUp", false);
    }
    
}
