using TMPro;
using UnityEngine;

public class QuestUpdate : MonoBehaviour
{
    public TextMeshProUGUI task4Hint;
    Tasks tasks;
    QuestGiver questGiver;
    private void Start()
    {
        tasks = FindFirstObjectByType<Tasks>();
        questGiver = FindFirstObjectByType<QuestGiver>();
    }
    private void OnDestroy()
    {
        task4Hint.text = $"Current objective: Give the cube to the guy";
        tasks.TaskUpdated();
        questGiver.QuestUpdate();
    }
}
