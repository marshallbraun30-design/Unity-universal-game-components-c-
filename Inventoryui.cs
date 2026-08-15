using TMPro;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class Inventoryui : MonoBehaviour
{
    public Inventory inventory;
    public TMP_Text[] slots;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInv();
    }
    private void UpdateInv()
    {
        for (int i=0; i<slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].text = inventory.items[i].itemname;
            }
            else
            {
                slots[i].text = "Empty";
            }
        
        
            
        }

    }
    
}
