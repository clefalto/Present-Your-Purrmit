using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class NPCManager : MonoBehaviour
{
    private static NPCManager _instance;

    public static NPCManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
    
    public List<NPC> NPCs;
    int n;
    
    private int activeNPCIndex = 0;
    private NPC activeNPC;
    
    public Button debugAcceptButton;
    public Button debugRejectButton;
    
    private int rejectedCount = 0;
    
    public string continuedScene;
    public string firedScene;

    void Start()
    {
        foreach (NPC npc in NPCs)
        {
            npc.gameObject.SetActive(false);
        }
        
        SendNextNPC();
        n = NPCs.Count;
    }

    private void SendNextNPC()
    {
        debugAcceptButton.onClick.RemoveAllListeners();
        debugRejectButton.onClick.RemoveAllListeners();
        
        activeNPC = NPCs[activeNPCIndex];
        activeNPC.gameObject.SetActive(true);
        
        debugAcceptButton.onClick.AddListener(delegate {activeNPC.GiveVerdict(true);});
        debugRejectButton.onClick.AddListener(delegate {activeNPC.GiveVerdict(false);});
        
        activeNPC.numRejected = rejectedCount;
        activeNPC.total = n - 1; // number of guys minus one because of the last one not counting
        
        activeNPC.StartGoing();
        activeNPCIndex++;
    }

    public void OnNPCLeave(bool wasRejected)
    {
        if (wasRejected)
        {
            rejectedCount++;
        }
        if (activeNPCIndex < n)
        {
            SendNextNPC();
        }
        else
        {
            Debug.Log("all done!");
            // the game is over.

            if (rejectedCount == 0)
            {
                SceneManager.LoadScene(firedScene, LoadSceneMode.Single);
            }
            else
            {
                SceneManager.LoadScene(continuedScene, LoadSceneMode.Single);
            }
        }
    }

    public void SendVerdictToNPC(bool verdict) 
    {
        activeNPC.GiveVerdict(verdict);
    }
    
}
