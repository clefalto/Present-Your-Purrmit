using System;
using UnityEngine;

public class PaperSubmittingZone : MonoBehaviour
{
    public bool isTriggering = false;
    public Collider collidingBody; 

    public void OnTriggerEnter(Collider other)
    {
        isTriggering = true;
        collidingBody = other;
    }

    public void OnTriggerExit(Collider other)
    {
        isTriggering = false;
    }
}
