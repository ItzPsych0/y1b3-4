using UnityEngine;

//This class is responsible for scoring an ammount of points when something enters its trigger
public class ScoreHoles : MonoBehaviour
{
    public int holePoints;
    public GameInteract GameInteract;
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameInteract.ScorePoints(holePoints);
    }
}
