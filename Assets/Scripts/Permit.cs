using UnityEngine;
using UnityEngine.Serialization;

public class Permit : MonoBehaviour
{
    public enum StampStatus
    {
        unstamped,
        accepted,
        rejected
    };
    
    public StampStatus stampStatus = StampStatus.unstamped;

    public GameObject rejectStamp;
    public GameObject acceptStamp;

    void Start()
    {
        rejectStamp = GameObject.FindWithTag("RejectStamp");
        acceptStamp = GameObject.FindWithTag("AcceptStamp");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == rejectStamp)
        {
            stampStatus = StampStatus.rejected;
        }
        else if (collision.gameObject == acceptStamp)
        {
            stampStatus = StampStatus.accepted;
        }
    }
}
