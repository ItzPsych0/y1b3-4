using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KoekhapManager : MonoBehaviour
{
    public GameObject koek;
    public List<Transform> spawnPoints;
    int lastIndex = -1;

    public static float yourScore = 0;
    public static float opponentsScore = 0;
    public GameObject minigameUI;
    public TextMeshProUGUI score;
    public TextMeshProUGUI score1;

    void OnEnable()
    {
        SpawnAtRandomPoint();
        minigameUI.SetActive(true);
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

    public void UpdateScore()
    {
        score.text = $"Score: {yourScore.ToString()}/5";
        score1.text = $"Score: {opponentsScore.ToString()}/5";
    }
}
