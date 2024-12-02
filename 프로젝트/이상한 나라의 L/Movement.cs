using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    Animator animator;
    bool checkWalk;
    bool isFront, isBack;

    private Vector2 moveDirection;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Move(float speed)
    {
        animator.SetFloat("MoveDirection", moveDirection.x);

        transform.Translate(moveDirection * speed * Time.unscaledDeltaTime);
    }

    private void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        
        moveDirection = new Vector2(input.x, input.y);
    }
}
