using UnityEngine;


[CreateAssetMenu(fileName = "PlayerMovementSettings", menuName = "ScriptableObjects/PlayerMovementSettings")]
public sealed class PlayerMovementSettings : ScriptableObject
{
    public float Speed = 6.0f;
    public float JumpSpeed = 8.0f;
    public float Gravity = 20.0f;

    public float XSensitivity = 2f;
    public float YSensitivity = 2f;
    public float MinimumX = -90F;
    public float MaximumX = 90F;
}