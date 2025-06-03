using UnityEngine;

public class minigame1 : MonoBehaviour
{


    public int Score = 0;

    private void Start()
    {
    }


    public void ScorePoints(int points)
    {
        Score += points;
        
        Debug.Log("Scored a total of " + Score + " points!");



    }

   
}
