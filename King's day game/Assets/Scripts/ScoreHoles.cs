using UnityEngine;

public class ScoreHoles : MonoBehaviour
{
    public int holePoints = 2;
    public minigame1 minigame1;
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        minigame1.ScorePoints(holePoints);
    }
}
