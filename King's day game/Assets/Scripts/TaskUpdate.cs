using UnityEngine;
using TMPro;

public class TaskUpdate : MonoBehaviour
{
    public TextMeshProUGUI task1;
    public TextMeshProUGUI task1Hint;
    public GameObject done;
    Tasks tasks;

    private void Start()
    {
        tasks = FindFirstObjectByType<Tasks>();
    }
    private void OnDestroy()
    {
        task1.text = $"<s>Buy some books</s>";
        task1Hint.text = $"<s>Current objective: look around the market place for books</s>";
        TaskManager.quest1Complete = true;
        tasks.TaskUpdated();
        done.SetActive(true);
    }
}
