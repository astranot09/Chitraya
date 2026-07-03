using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovementStat", menuName = "Scriptable Objects/PlayerMovementStat")]
public class PlayerMovementStat : ScriptableObject
{
    [Header("Run")]
    public float maxRunSpeed;

    [Header("Jump")]
    public LayerMask groundLayer;
    public float jumpForced;

    [Header("Hanging")]
    public LayerMask wallLayer;
    public float hangingTime;

    [Header("Dash")]
    public float dashingSpeed;
    public float dashingTime;

    
}
