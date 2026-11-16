using UnityEngine;

public class Item_PickUp : MonoBehaviour, IInteractable
{
    public void Interact(GameObject interacting)
    {
        var inventory = interacting.GetComponent<Player_Inventory>();
        inventory.StoreItem(gameObject);
    }
}
