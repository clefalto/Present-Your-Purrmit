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
        
        var complongles = GetComponents<TextureChanger>();
        // gee i sure hope this doesn't create any race conditions!
        if (complongles[0].forReject)
        {
            complongles[0].object2 = rejectStamp;
            complongles[1].object2 = acceptStamp;
        }
        else
        {
            complongles[0].object2 = acceptStamp;
            complongles[1].object2 = rejectStamp;
        }
        
        Debug.Log(rejectStamp);
        Debug.Log(acceptStamp);
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
