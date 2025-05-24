using UnityEngine;

public class Koek : MonoBehaviour
{
    [SerializeField] KoekhapManager koekhapManager;
    [SerializeField] OpponentKoekHappen opponentKoekHappen;

    private void Start()
    {
        koekhapManager = FindFirstObjectByType<KoekhapManager>();
        opponentKoekHappen = FindFirstObjectByType<OpponentKoekHappen>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            KoekhapManager.yourScore += 1;
            koekhapManager.UpdateScore();
            koekhapManager.SpawnAtRandomPoint();
            Destroy(gameObject);
        }
        if(other.CompareTag("Opponent"))
        {
            KoekhapManager.opponentsScore += 1;
            koekhapManager.UpdateScore();
            opponentKoekHappen.SpawnAtRandomPoint();
            Destroy(gameObject);
        }
    }
}
