using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class SimpleMerchantHelper : MonoBehaviour {

    [Header("Add gameobject with simpleManager script attached.")]
    public GameObject simpleManager;//this will be the same for all of your merchants

    private int pageNumber = 0;

    public void Buy()
    {
        string itemName = EventSystem.current.currentSelectedGameObject.name;

        int cost = 0; //gameObject.GetComponent<SimpleMerchant>().items[r].cost;
        int stock = 0; //gameObject.GetComponent<SimpleMerchant>().items[r].stock
        for (int r = 0; r < gameObject.GetComponent<SimpleMerchant>().items.Length; r++)
        {    //searches SimpleMerchant for an item with the same name, sets cost stock
            if (gameObject.GetComponent<SimpleMerchant>().items[r].item.ToString() == itemName)
            {
                cost = gameObject.GetComponent<SimpleMerchant>().items[r].cost;
                stock = gameObject.GetComponent<SimpleMerchant>().items[r].stock;

                if (simpleManager.GetComponent<SimplePlayerInventory>().currency >= cost && stock > 0)
                {
                    for (int i = 0; i < simpleManager.GetComponent<SimplePlayerInventory>().inventory.Length; i++)
                    {
                        //searches SimplePlayerInventory for an item with the same name
                        if (EventSystem.current.currentSelectedGameObject.name 
                            == simpleManager.GetComponent<SimplePlayerInventory>().inventory[i].itemName)
                        {

                            simpleManager.GetComponent<SimplePlayerInventory>().inventory[i].itemInventory += 1;

                            gameObject.GetComponent<SimpleMerchant>().items[r].stock = stock - 1;

                            simpleManager.GetComponent<SimplePlayerInventory>().currency -= cost;

                            //connects to the SimplePlayerInventory script on the SimpleManager
                            simpleManager.GetComponent<SimplePlayerInventory>().currencyUI.text 
                                = simpleManager.GetComponent<SimplePlayerInventory>().currency.ToString();
                        }
                    }
                }
            }
        }

        //decrease shop inventory
        //update ui

}

    public void Sell()
    {
        string itemName = EventSystem.current.currentSelectedGameObject.name;

        int value = 0; //gameObject.GetComponent<SimpleMerchant>().items[r].value;
        int playerInventory = 0; //gameObject.GetComponent<SimplePlayerInventory>().inventory[r].itemInventory

        for (int r = 0; r < gameObject.GetComponent<SimpleMerchant>().items.Length; r++)
        {    //searches SimpleMerchant for an item with the same name, sets cost stock
            if (gameObject.GetComponent<SimpleMerchant>().items[r].item.ToString() == itemName)
            {
                value = gameObject.GetComponent<SimpleMerchant>().items[r].value;
                playerInventory = simpleManager.GetComponent<SimplePlayerInventory>().inventory[r].itemInventory;

                if (playerInventory > 0)
                {
                    for (int i = 0; i < simpleManager.GetComponent<SimplePlayerInventory>().inventory.Length; i++)
                    {
                        //searches SimplePlayerInventory for an item with the same name
                        if (EventSystem.current.currentSelectedGameObject.name 
                            == simpleManager.GetComponent<SimplePlayerInventory>().inventory[i].itemName)

                        {

                            simpleManager.GetComponent<SimplePlayerInventory>().inventory[i].itemInventory -= 1;

                            gameObject.GetComponent<SimpleMerchant>().items[r].stock += 1;


                            simpleManager.GetComponent<SimplePlayerInventory>().currency += value;

                            //connects to the SimplePlayerInventory script on the SimpleManager
                            simpleManager.GetComponent<SimplePlayerInventory>().currencyUI.text =
                                simpleManager.GetComponent<SimplePlayerInventory>().currency.ToString();
                        }
                    }
                }
            }
        }
    }

    public void Next()
    {
        /*
         * 
         */
        //pageNumber = pageNumber + 1;
        //gameObject.GetComponent<SimpleMerchant>().SetMenu(pageNumber);
        Debug.Log(gameObject.GetComponent<SimpleMerchant>().howManyPagesPublic);
    }

    public void Previous()
    {
        Debug.Log("Test Previous.");
    }

}
