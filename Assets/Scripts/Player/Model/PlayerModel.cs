using UnityEngine;

public class PlayerModel
{
    public float Velocity = 5f;
    public float InputX;
    public float InputZ;
    public Vector3 DesiredMoveDirection;
    public bool BlockRotationPlayer = false;
    public float DesiredRotationSpeed = 0.1f;
    public float Speed;
    public float AllowPlayerRotation = 0.1f;

    public bool IsGrounded;
    public float VerticalVelocity;

    // Animation Smoothing
    public float HorizontalAnimSmoothTime = 0.2f;
    public float VerticalAnimTime = 0.2f;
    public float StartAnimTime = 0.3f;
    public float StopAnimTime = 0.15f;
}
