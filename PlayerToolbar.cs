using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public Transform Playercam;

    public int Currentslot = 0;
    private int Maxitems = 5;
    private void Update()
    {
       if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Currentslot = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Currentslot = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Currentslot = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Currentslot = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Currentslot = 4;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (Currentslot < items.Count)
            {
                dropitem();
            }
        }
    }

    public List<Itemdata> items = new List<Itemdata>();

    public int maxitems = 5;

    public void Additem(Itemdata item)
    {
        if (items.Count < Maxitems)
        {
            items.Add(item);
        }
        else
        { 
            items[Currentslot] = item;
        }
    }

    public void Removeitem(int slot)
    {
        {
            if (slot >= 0 && slot < items.Count)
            {
                items.RemoveAt(slot);
                
            }
            else
            {
                Debug.Log("Cannot remove item. Slot " + slot + " is empty.");
            }
        }

    }
    public void dropitem()
    {
        if (Currentslot >=0 && Currentslot < items.Count)
        {
            Itemdata item = items[Currentslot];
            Vector3 dropPosition = Playercam.position + Playercam.forward * 5f;

            if (dropPosition.y <1f)
            {
                dropPosition.y = 1f;
            }

            item.itemprefab.transform.position = dropPosition;

            item.itemprefab.SetActive(true);
            
            items.RemoveAt(Currentslot);

            

           
        }
     }
}


