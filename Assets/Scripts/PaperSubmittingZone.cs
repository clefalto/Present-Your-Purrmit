using System;
using UnityEngine;

public class PaperSubmittingZone : MonoBehaviour
{
    public bool isTriggering = false;
    public Collider collidingBody;

    public void OnTriggerStay(Collider other)
    {
        if (other)
        {
            if (other.gameObject.CompareTag("Permit"))
            {
                isTriggering = true;
                collidingBody = other;
            }
        }
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Permit"))
        {
            isTriggering = true;
            collidingBody = other;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Permit"))
        {
            isTriggering = false;
            collidingBody = null;
        }
    }
}
