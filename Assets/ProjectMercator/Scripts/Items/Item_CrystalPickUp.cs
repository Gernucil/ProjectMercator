using UnityEngine;

public class Item_CrystalPickUp : MonoBehaviour, IInteractable
{
    public void Interact(GameObject interacting)
    {
        var inventory = interacting.GetComponent<Player_Inventory>();
        inventory.GetCrystalInInventory(1); // передаём чисто единицу, потому что при подборе кристалл то один, логично? логично
        gameObject.SetActive(false);
    }
}
