using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class interact : MonoBehaviour
{
    public cameraMovement cameraMovement;
    public Camera mainCamera;
    public Transform talkWithNPC;
    public Transform seeWares;
    public Transform playerCam;
    public GameObject speech;
    public GameObject poor;

    float cameraXRotation = 0f;
    public float cameraYRotation = 0f;

    bool talking = false;
    bool browsing = false;
    bool playerInTrigger = false;

    private Light lightPoint;

    private void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            talking = !talking;

            if (talking && !browsing)
            {
                cameraMovement.target = talkWithNPC;
                GameObject.FindWithTag("Player").GetComponent<movement>().enabled = false;
                GameObject.FindWithTag("Player").GetComponent<MeshRenderer>().enabled = false;

                Cursor.lockState = CursorLockMode.None;
                speech.SetActive(true);
            }
            else if(!talking || browsing)
            {
                GameObject.FindWithTag("Player").GetComponent<movement>().enabled = true;
                GameObject.FindWithTag("Player").GetComponent<MeshRenderer>().enabled = true;
                cameraMovement.target = playerCam;
                Cursor.lockState = CursorLockMode.Locked;
                speech.SetActive(false);
                poor.SetActive(false);
                browsing = false;
                talking = false;
                if (lightPoint != null)
                {
                    lightPoint.enabled = false;
                }
                cameraMovement.interacting = false;
            }
        }

        if(talking)
        {
            cameraXRotation = 0;
            cameraMovement.transform.position = talkWithNPC.transform.position;
            cameraMovement.transform.localRotation = Quaternion.Euler(cameraXRotation, cameraYRotation, 0);
            cameraMovement.interacting = true;
        }

        if (browsing)
        {
            Browsing();
            cameraMovement.transform.position = seeWares.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInTrigger = false;
        }
    }

    public void SeeWares()
    {
        browsing = true;
        talking = false;
        cameraMovement.target = seeWares;
    }

    void Browsing()
    {
        cameraXRotation = 20;
        cameraMovement.transform.localRotation = Quaternion.Euler(cameraXRotation, cameraYRotation, 0);
        speech.SetActive(false);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        LayerMask layerMask = LayerMask.GetMask("Wares");
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            lightPoint = hit.transform.gameObject.GetComponentInChildren<Light>(true);
            lightPoint.enabled = true;
            float cost = lightPoint.GetComponentInParent<Value>().value;

            if (Input.GetMouseButtonDown(0))
            {
                if (moneyManager.cashAmount >= cost)
                {
                    moneyManager.cashAmount -= cost;
                    Destroy(lightPoint.transform.parent.gameObject);
                }

                else
                {
                    poor.SetActive(true);
                    TextMeshProUGUI poorText = poor.GetComponentInChildren<TextMeshProUGUI>();
                    poorText.text = $"Sorry, you need €{cost} to buy this.";
                }
            }
        }
        else
        {
            if (lightPoint != null)
            {
                lightPoint.enabled = false;
                poor.SetActive(false);
            }
        }

    }

}
