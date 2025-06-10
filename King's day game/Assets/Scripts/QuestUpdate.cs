using TMPro;
using UnityEngine;

public class QuestUpdate : MonoBehaviour
{
    public TextMeshProUGUI task4Hint;
    Tasks tasks;
    QuestGiver questGiver;
    public bool questTriggered;
    private void Start()
    {
        tasks = FindFirstObjectByType<Tasks>();
        questGiver = FindFirstObjectByType<QuestGiver>();
        questTriggered = false;
    }
    private void OnDestroy()
    {
        if (!Application.isPlaying || TaskManager.quest4Complete == true) return;

        task4Hint.text = $"Current objective: Give the present to the guy";
        questGiver.QuestUpdate();
        questGiver.cost = GetComponent<Value>().value;
    }
}
