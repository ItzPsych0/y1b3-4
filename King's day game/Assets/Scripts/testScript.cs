using System.Security.Cryptography;
using UnityEngine;

public class testScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            moneyManager.cashAmount += 5;
            Destroy(gameObject);
        }

    }
}
