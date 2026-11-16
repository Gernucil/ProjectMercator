using UnityEngine;

public class Vehicle_InOut : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform _entryPoint;
    
    public void Interact(GameObject interacting)
    {
        interacting.transform.SetPositionAndRotation(_entryPoint.position, Quaternion.identity);
    }

}
