using UnityEngine;
using UnityEngine.UI;

public class Tasks : MonoBehaviour
{
    public Animator animator;
    float timer = 0f;
    public GameObject taskbar;
    public GameObject updateProgress;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(animator.GetBool("itsTime") == false)
        {
            timer += Time.deltaTime;
        }

        if(timer >= 4f)
        {
            animator.SetBool("itsTime", true);
            timer = 0f;
        }
    }

    public void TaskUpdated()
    {
        taskbar.SetActive(false);
        updateProgress.SetActive(true);
        animator.SetBool("itsTime", false);
    }
}
