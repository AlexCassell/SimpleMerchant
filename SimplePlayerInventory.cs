using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

//https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/enum
public enum SimpleInventoryItems//get rid of this, in places of an array, use an in edit loop to fill array for user to fill in
{
    outOfStock,
    //vegetables
    rice, grain, corn, carrots, strawberries, oranges, grapes, avocados, lemons,
    //weapons
    cannons, cannonballs, muskets, bullets, swords, bombs,
    //luxury items
    jewelry, clothing, trinkets, dolls, rum, coral,
    //animal products
    ham, chicken, beef, bearskin, cowskin, fish, shrimp, lobster, feathers

        /*next version will:
         * alphabatize this list unless this causes issues
         * will add a space preceding any capital letters
         */
}

//The next 20+ lines give the ability to create read only variables in the inspector.
//https://answers.unity.com/questions/489942/how-to-make-a-readonly-property-in-inspector.html
public class ReadOnlyAttribute : PropertyAttribute
{

}

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property,
                                            GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position,
                               SerializedProperty property,
                               GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}
//^^

[System.Serializable]
public class SimpleInventory
{  
    [ReadOnly] public string itemName; //this is set by script  while in the editor
    [Header("Starting Player Inventory")]
    public int itemInventory;//set to how many of an item the player starts with- this is where the amount in the inventory is stored
}

public class SimplePlayerInventory : MonoBehaviour
{
    [Header("Maximum Items a Player can carry")]
    [SerializeField]
    private int inventoryMax;//set how many items the player can hold when they start the game

    [Header("Set Players Starting Currency")]
    public int currency; //set how much of your currency the player starts the game with
    [Header("Set where you are displaying player currency amount")]
    [SerializeField]
    public Text currencyUI; //updates player's currency's UI element

    public SimpleInventory[] inventory = new SimpleInventory[inventoryItemsSize];
    private static int inventoryItemsSize = Enum.GetNames(typeof(SimpleInventoryItems)).Length;

    private static int size = System.Enum.GetValues(typeof(SimpleInventoryItems)).Length;

    public static object[] simpleInventory;

    public void SetInventory()
    {
        inventoryItemsSize = Enum.GetNames(typeof(SimpleInventoryItems)).Length;
        for (int i = 0; i < inventoryItemsSize; i++)
        {
            inventory[i].itemName = Enum.GetNames(typeof(SimpleInventoryItems))[i].ToString();
        }
    }


}
