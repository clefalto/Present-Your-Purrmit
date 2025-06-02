using UnityEngine;

public class PaperManager : MonoBehaviour
{
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
            // if its stamped with APPROVED
            NPCManager.Instance.SendVerdictToNPC(true);
            
            // if it's not stamped
            // respawn the paper so the player doesn't get softlocked
            
        }
    }
    
    public void SpawnPapers(GameObject paperPrefab)
    {
        var pp = Instantiate(paperPrefab);
        pp.transform.position = paperDropLocation.transform.position;
        lastSummonedPaper = paperPrefab;
    }
}
