using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KoekhapManager : MonoBehaviour
{
    [SerializeField] OpponentKoekHappen opponentKoekHappen;
    public GameObject koek;
    public List<Transform> spawnPoints;
    int lastIndex = -1;

    public static float yourScore = 0;
    public static float opponentsScore = 0;
    public GameObject minigameUI;
    public GameObject controls;

    public GameObject victory;
    public GameObject defeat;

    public TextMeshProUGUI score;
    public TextMeshProUGUI score1;

    public Transform player;
    public Transform opponent;
    public Transform playerSpawnpoint;
    public Transform opponentSpawnpoint;

    bool hasTriggered = false;

    void OnEnable()
    {
        controls.SetActive(true);
        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
        victory.SetActive(false);
        defeat.SetActive(false);
        yourScore = 0;
        opponentsScore = 0;
        UpdateScore();
        player.transform.position = playerSpawnpoint.transform.position;
        opponent.transform.position = opponentSpawnpoint.transform.position;
        GameObject[] prefabs = GameObject.FindGameObjectsWithTag("Koek");
        foreach (GameObject go in prefabs)
        {
            Destroy(go);
        }
    }
    private void Update()
    {
        if (yourScore >= 10 && !hasTriggered)
        {
            hasTriggered = true;
            Time.timeScale = 0f;
            victory.SetActive(true);
            minigameUI.SetActive(false);
            moneyManager.cashAmount += 5;
        }
        if (opponentsScore >= 10)
        {
            Time.timeScale = 0f;
            defeat.SetActive(true);
            minigameUI.SetActive(false);
        }
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
        score.text = $"Score: {yourScore.ToString()}/10";
        score1.text = $"Score: {opponentsScore.ToString()}/10";
    }

    public void StartGame()
    {
        controls.SetActive(false);
        Time.timeScale = 1f;
        minigameUI.SetActive(true);
        SpawnAtRandomPoint();
        opponentKoekHappen.SpawnAtRandomPoint();
        hasTriggered = false;
    }

    public void RestartGame()
    {
        yourScore = 0;
        opponentsScore = 0;
        UpdateScore();
        player.transform.position = playerSpawnpoint.transform.position;
        opponent.transform.position = opponentSpawnpoint.transform.position;
        defeat.SetActive(false);
        controls.SetActive(true);
        Time.timeScale = 0f;
        GameObject[] prefabs = GameObject.FindGameObjectsWithTag("Koek");
        foreach (GameObject go in prefabs)
        {
            Destroy(go);
        }
    }
}
