using UnityEngine;
using UnityEngine.UI;

public class moneyManager : MonoBehaviour
{
    Text cashText;
    public static float cashAmount = 2f;
    // Start is called before the first frame update
    void Start()
    {
        cashText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cashText != null)
            cashText.text = cashAmount.ToString();
    }
}
