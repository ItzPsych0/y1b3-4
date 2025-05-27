using UnityEngine;

public class spawnPuck : MonoBehaviour
{

    public GameObject puckPrefab;
    public Transform puckSpawnPoint;
    public GameObject currentPuck;
    public bool puckInTrigger = false;
    public GameInteract gameInteract;

    public void SpawnPuck()
    {
        currentPuck = Instantiate(puckPrefab, puckSpawnPoint.position, puckSpawnPoint.rotation);
        puckInTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Puck"))
        {
            puckInTrigger = false;
        }
    }
}
