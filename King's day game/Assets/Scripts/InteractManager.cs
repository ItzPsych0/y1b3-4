using UnityEngine;
using TMPro;

public class InteractManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    Transform currentAnchor;
    interact activeInteract;
    public cameraMovement cameraMovement;
    public GameObject speech;

    public void Dialogue(interact activeStall)
    {
        dialogueText.text = activeStall.dialogueText;
        currentAnchor = activeStall.seeWares.transform;
        activeInteract = activeStall;
        speech.SetActive(true);
    }

    public void SeeWares()
    {
        activeInteract.browsing = true;
        activeInteract.talking = false;
        cameraMovement.target = currentAnchor;
    }

}
