using UnityEngine;

[CreateAssetMenu(fileName = "MovementSettings", menuName = "Scriptable Objects/MovementSettings")]
public class PlayerSettings : ScriptableObject
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;
    public float interactDistance = 3f;
    public LayerMask interactLayer;
}
