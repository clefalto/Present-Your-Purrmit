using UnityEngine;

public class PaperManager : MonoBehaviour
{
    private static PaperManager _instance;

    public static PaperManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
    
    public GameObject paperDropLocation;
    public GameObject paperSubmittingZone;
    
    private PaperSubmittingZone psz;
    
    private GameObject lastSummonedPaper;
    
    void Start()
    {
        psz = paperSubmittingZone.GetComponent<PaperSubmittingZone>();
    }

    void Update()
    {
        if (psz.isTriggering)
        {
            // check if it's like stamped
            // try get component for funsies
            if (psz.collidingBody.TryGetComponent<Permit>(out Permit p))
            {
                switch (p.stampStatus) 
                {
                    case Permit.StampStatus.accepted:
                        NPCManager.Instance.SendVerdictToNPC(true);
                        Destroy(p.gameObject);
                        break;
                    case Permit.StampStatus.rejected:
                        NPCManager.Instance.SendVerdictToNPC(false);
                        Destroy(p.gameObject);
                        break;
                    case Permit.StampStatus.unstamped:
                        p.transform.position = paperDropLocation.transform.position;
                        // don't destroy the go in this case... because we just need to move it to the right place
                        break;
                }
            }
        }
    }
    
    public void SpawnPapers(GameObject paperPrefab)
    {
        var pp = Instantiate(paperPrefab);
        pp.transform.position = paperDropLocation.transform.position;
        lastSummonedPaper = paperPrefab;
    }
}
