using UnityEngine;

//this class causes the SetInventory fuction to be run while in the editor
[ExecuteInEditMode]
public class SimpleGameManagerEditor : MonoBehaviour
{
    void Update()
    {
        gameObject.GetComponent<SimplePlayerInventory>().SetInventory();
    }
}