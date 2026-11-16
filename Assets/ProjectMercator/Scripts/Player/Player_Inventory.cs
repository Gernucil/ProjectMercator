using UnityEngine;

public class Player_Inventory : MonoBehaviour
{
    private GameObject[] Invetory;
    public int _crystalCount;

    [SerializeField] private Transform _itemSocket;
    private void Start()
    {
        Invetory = new GameObject[1];
    }
    public void StoreItem(GameObject storedItem)
    {
        Invetory[0] = storedItem;
        Invetory[0].transform.position = _itemSocket.position;
        Invetory[0].transform.rotation = _itemSocket.rotation;
        Invetory[0].transform.parent = _itemSocket;
        var itemRB = Invetory[0].GetComponent<Rigidbody>();
        itemRB.useGravity = false;
        itemRB.freezeRotation = true;
        itemRB.linearVelocity = Vector3.zero;
    }
    public void GiveItem()
    {

    }
    public void DropItem()
    {
        var itemRB = Invetory[0].GetComponent<Rigidbody>();
        itemRB.useGravity = true;
        itemRB.freezeRotation = false;
        Invetory[0].transform.parent = null;    
    }

    public void GetCrystalInInventory(int incomingCrystal)
    {
        _crystalCount += incomingCrystal;
        Debug.Log($"Кристалл подобран, текущее количество кристаллов {_crystalCount}");
    }
}
