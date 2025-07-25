using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandle : MonoBehaviour
{
    private static readonly int isMoving = Animator.StringToHash("isMove");
    private static readonly int isDamage = Animator.StringToHash("isDamage");

    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Move(Vector2 obj)
    {
        animator.SetBool(isMoving, obj.magnitude > 0.5f);
    }

    public void Damage() 
    {
        animator.SetBool(isDamage, true);
    }
    public void InvincibilityEnd()
    {
        animator.SetBool(isDamage, false);
    }
}
