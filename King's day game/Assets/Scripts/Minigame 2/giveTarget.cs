using UnityEngine;
using UnityEngine.Rendering;

public class giveTarget : MonoBehaviour
{
    [SerializeField] OpponentKoekHappen opponentKoekHappen;
    public bool koekSpotted = false;
    float timer;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Koek"))
        {
            opponentKoekHappen.target = transform.position;
            koekSpotted = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Opponent") && koekSpotted)
        {
            timer += Time.deltaTime;
            if(timer >= 0.5f)
            {
                opponentKoekHappen.Jump();
                timer = 0f;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Koek"))
        {
            koekSpotted = false;
        }
    }
}
