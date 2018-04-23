using UnityEngine;

//this class causes the SetMenu fuction to be run while in the editor
[ExecuteInEditMode]
public class SimpleMerchantEditor : MonoBehaviour
{
    [SerializeField]
    private int pageNumber = 0;

    void Update()
    {
        gameObject.GetComponent<SimpleMerchant>().SetMenu(pageNumber);// change this number if you would like to see another page in the editor
    }
}