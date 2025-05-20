using UnityEngine;

public class giveTarget : MonoBehaviour
{
    [SerializeField] OpponentKoekHappen opponentKoekHappen;
    public bool koekSpotted = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Koek"))
        {
            opponentKoekHappen.target = transform.position;
            koekSpotted = true;
        }

        if (other.CompareTag("Opponent") && koekSpotted)
        {
            opponentKoekHappen.Jump();
        }
    }
}
