using UnityEngine;

public class ScoreHoles : MonoBehaviour
{
    public int holePoints = 2;
    public GameInteract GameInteract;
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameInteract.ScorePoints(holePoints);
    }
}
