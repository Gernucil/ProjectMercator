using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Player_Input)), RequireComponent(typeof(Player_Move)), RequireComponent(typeof(Player_CameraMove)), RequireComponent(typeof(Player_Interact))]
public class Player : MonoBehaviour
{
    public PlayerSettings settings;
        
    private void Awake()
    {
        settings = (PlayerSettings)Resources.Load("MovementSettings");        
    }

}
