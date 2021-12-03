using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent
{
    private Vector2 workspace;
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }
    public Rigidbody2D RB2D { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        FacingDirection = 1;
        RB2D = GetComponentInParent<Rigidbody2D>();
    }
    public void LogicUpdate()
    {

        CurrentVelocity = RB2D.velocity;
    }
    // sets velocity in different types
    #region
    /// <summary>
    /// change x and y velociy diognal
    /// </summary>
    /// <param name="velocity"></param>
    /// <param name="angle"></param>
    /// <param name="direction"></param>
    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        RB2D.velocity = workspace;
        CurrentVelocity = workspace;
    }

    /// <summary>
    /// change x and y velocity horizontal
    /// </summary>
    /// <param name="velocity"></param>
    /// <param name="direction"></param>
    public void SetVelocity(float velocity, Vector2 direction)
    {
        workspace = direction * velocity;
        RB2D.velocity = workspace;
        CurrentVelocity = workspace;
    }

    /// <summary>
    /// change only x dont touch y
    /// </summary>
    /// <param name="velocity"></param>
    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        RB2D.velocity = workspace;
        CurrentVelocity = workspace;

    }

    /// <summary>
    /// change only y dont touch x
    /// </summary>
    /// <param name="velocity"></param>
    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        RB2D.velocity = workspace;
        CurrentVelocity = workspace;

    }

    /// <summary>
    /// sets velocity if needed
    /// </summary>
    public void SetVelocityZero()
    {
        RB2D.velocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
    }

    #endregion
    private void Flip()
    {
        FacingDirection *= -1;
        RB2D.transform.Rotate(0.0f, 180.0f, 0.0f);

    }
    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }

    }
}
