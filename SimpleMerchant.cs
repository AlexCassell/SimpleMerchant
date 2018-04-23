//Alex Cassell
//https://alexcassell.com
//https://simulism.net

using System;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class SimpleItemStructure
{
    [Header("Select item from the list(list set in SimpleInventory.cs)")]
    public SimpleInventoryItems item;

    [Header("Price user has to pay for item.")]
    public int cost;

    [Header("Limit items available?")]
    public bool useLimits = true;
    //unchecking this to false will cause script to ignore next two vairables
    //leaving either of next two variables empty is the same as unchecking this variable

    [Header("How many items NPC has to sell.")]
    public int stock;
    public int resetTime;//port stock replenishes after X days

    [Header("Price NPC will to pay for item.")]
    public int value;

    [Header("Unit of mearsurement.")]
    public string units; //ie "Crates of"

    [Header("Set Icon for this Item.")]
    public Sprite itemIcon;//icon associated with item to be shown.  //to not use this feature just leave this empty
}
[System.Serializable]
public class SimpleUiElements
{
    //this is set for each item slots in your inventory menu system
    [Header("Add your UI objects")]
    public GameObject itemCard;// the UI element that houses all other UI elements for that item
    public Text itemNameText;//Name of item.
    public Text costText;//Price user has to pay for item. Leave blank if you do not want item sold at this merchant
    public Text stockText;//How many items NPC has to sell.
    public Text playerstockText;//How many items player has to sell.
    public Text valueText;//Price NPC will to pay for item.Leave blank if you do not want item bought at this merchant
    public GameObject itemIconMenu;//UI element where icon is to be shown.
    public GameObject buyButton;//buy button
    public GameObject sellButton;//buy button
}

public class SimpleMerchant : MonoBehaviour{
   
    [Header("Set amount of items this merchant will sell.")]
    public SimpleItemStructure[] items;
    [SerializeField]
    private string merchantName;

    [Header("Menu")]
    [SerializeField]
    private GameObject merchantMenu;//merchant menu, this will probably be the same for all merchants. Unless you have multiple menus

    [Header("Change cameras when NPC is clicked.")]
    [SerializeField]
    private bool useMerchantCamera = true;
    //unchecking this to false will cause script to ignore next two vairables
    //leaving either of next two variables emtpy is the same as unchecking this variable

    [SerializeField]
    private GameObject merchantCamera;//camera aligned with merchant
    [SerializeField]
    private GameObject player;//player will be deactivated while merchantCamera is active

    private bool closeEnough; //set by trigger when player is close enough to be allowed to click merchant

    [Header("How many Items per page")]
    [SerializeField]
    private int itemsPerPage = 5;

    [Header("Set Size to amount of items listed per page in your menu.")]
    [SerializeField]
    private SimpleUiElements[] uiElements;
    [SerializeField]
    private Text merchantNameText;//to display merchant name - leave merchantName blank to ignore this feature

    [Header("Set Navigation buttons.")]
    [SerializeField]
    public GameObject rightButton;
    [SerializeField]
    public GameObject leftButton;

    private int currentPage = 0;

    public int howManyPagesPublic;
    //private int itemsOnLastPage;


    //https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnTriggerEnter.html
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "player")//checks to make sure trigger was set by a player 
        {
            closeEnough = true; //when true the player can click the npc and open their menu
        }
    }

    //https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnTriggerExit.html
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "player")//checks to make sure trigger was set by a player
        {
            closeEnough = false;//when false the player can not click the npc
        }
    }

    //https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnMouseDown.html
    void OnMouseDown()
    {
        if (closeEnough) //remove this if statement if you do not want to use the trigger
        {
            //set menu
            //open menu
        }
    }

    public void SetMenu(int pageNumber)
    {
        //set currency while in editor// is able to edit the currencyUI Text set in SimplePlayerInventory by getting it from SimpleMerchantHelper
        gameObject.GetComponent<SimpleMerchantHelper>().simpleManager.GetComponent<SimplePlayerInventory>().currencyUI.text
            = gameObject.GetComponent<SimpleMerchantHelper>().simpleManager.GetComponent<SimplePlayerInventory>().currency.ToString();
       
        //changes items shown based on pageNumber
       int page = pageNumber * itemsPerPage;



        double howManyPages = Mathf.Ceil(items.Length / itemsPerPage); //divides amount of items by itemsPerPage then rounds up to the nearest integer
        howManyPagesPublic = Convert.ToInt32(howManyPages);
        /*
          This will be used in later versions
      int itemsOnLastPage = Math.Abs(items.Length - itemsPerPage);//subtracts items from itemsPerPage then gets the absolute value to see how many items are on the last page
      */

        int uiLocation = 0;
        //this loop sets page's icons
        for (int i = page; i - page < uiElements.Length; i++)//############make this check Length vs page size
        {
            uiElements[uiLocation].itemIconMenu.GetComponent<Image>().sprite =
                items[i].itemIcon;

            uiLocation++;
        }
        //this loop sets the menu for this merchant
        if (merchantName.Length > 0)//sets name of merchant in UI unless merchantName was left empty
        {
            merchantNameText.text = merchantName;
        }

        uiLocation = 0;

        for (int i = page; i - page < itemsPerPage; i++)//items.Length or page size
            {
                //makes sure the first letter of the item is capitalized. 
                string upperCased = System.Globalization.CultureInfo.InvariantCulture.TextInfo.ToTitleCase(items[i].item.ToString());

                //replace upperCased with items[i].item.ToString(), if you do not want to use this feature.
                uiElements[uiLocation].itemNameText.text = items[i].units + " " + upperCased;

                //sets cost text element
                uiElements[uiLocation].costText.text = items[i].cost.ToString();

                uiElements[uiLocation].stockText.text = items[i].stock.ToString();

                uiElements[uiLocation].valueText.text = items[i].value.ToString();

                uiElements[uiLocation].playerstockText.text
                    = gameObject.GetComponent<SimpleMerchantHelper>().simpleManager.GetComponent<SimplePlayerInventory>().inventory[i].itemInventory.ToString();

                uiElements[uiLocation].buyButton.name
                    = gameObject.GetComponent<SimpleMerchantHelper>().simpleManager.GetComponent<SimplePlayerInventory>().inventory[i].itemName.ToString();

                uiElements[uiLocation].sellButton.name
                    = gameObject.GetComponent<SimpleMerchantHelper>().simpleManager.GetComponent<SimplePlayerInventory>().inventory[i].itemName.ToString();

                uiLocation++;
            }
    }
}


