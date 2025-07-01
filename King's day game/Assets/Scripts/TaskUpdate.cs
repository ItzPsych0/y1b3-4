using UnityEngine;
using TMPro;

public class TaskUpdate : MonoBehaviour
{
    public TextMeshProUGUI task;
    public TextMeshProUGUI taskHint;
    public GameObject done;
    Tasks tasks;
    [TextArea] public string taskText;
    [TextArea] public string taskHintText;

    private void Start()
    {
        tasks = FindFirstObjectByType<Tasks>();
    }
    private void OnDestroy()
    {
        if (!Application.isPlaying || TaskManager.quest1Complete == true)
        {
            return;
        }

        task.text = taskText;
        taskHint.text = taskHintText;
        TaskManager.quest1Complete = true;
        tasks.TaskUpdated();
        done.SetActive(true);
    }
}
