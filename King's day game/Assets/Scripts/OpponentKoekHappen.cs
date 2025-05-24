using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class OpponentKoekHappen : MonoBehaviour
{
    public GameObject koek;
    public List<Transform> spawnPoints;
    int lastIndex = -1;
    public Vector3 target;
    float jumpForce = 7.5f;
    Rigidbody rb;

    public float moveSpeed = 3f;

    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = new Vector3(target.x , 0, target.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    public void SpawnAtRandomPoint()
    {
        if (spawnPoints.Count == 0) return;

        int newIndex;
        do
        {
            newIndex = Random.Range(0, spawnPoints.Count);
        }
        while (newIndex == lastIndex && spawnPoints.Count > 1);

        Instantiate(koek, spawnPoints[newIndex].position, Quaternion.identity);
        lastIndex = newIndex;
    }

    public void Jump()
    {
        rb.linearVelocity += new Vector3(0, jumpForce, 0);
    }
}
