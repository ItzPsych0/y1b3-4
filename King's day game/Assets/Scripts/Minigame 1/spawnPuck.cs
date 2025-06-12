using UnityEngine;

public class spawnPuck : MonoBehaviour
{

    public GameObject puckPrefab;
    public Transform puckSpawnPoint;
    public GameObject currentPuck;
    public bool puckInTrigger = false;
    public GameInteract gameInteract;

    //Spawns the Puck prefab at a specific spawn point
    public void SpawnPuck()
    {
        currentPuck = Instantiate(puckPrefab, puckSpawnPoint.position, puckSpawnPoint.rotation);
        puckInTrigger = true; //Sets flag to true since the puck spawns inside this trigger
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Puck"))
        {
            puckInTrigger = false;
        }
    }
    public Rigidbody GetCurrentPuckRigidbody()
    {
        return currentPuck?.GetComponent<Rigidbody>();
    }
}
