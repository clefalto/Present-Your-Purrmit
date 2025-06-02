using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPC : MonoBehaviour
{
    public bool isFinalNPC = false;
    
    public GameObject spawnLocation;
    public GameObject dialogueLocation;
    public GameObject endLocation;

    private GameObject target;

    public Canvas textCanvas;
    public GameObject textBox; // box for displaying mr. text, should be a child of this go
    
    public List<AudioClip> audioClips;
    
    private AudioSource audioSource;

    // dialogues
    bool spoken = false;
    bool rejected = false; // USED ONLY TO PASS INFO TO MANAGER WHEN I LEAVE YES IT'S BAD PRACTICE
    public TextAsset initialDialogue; // plays when the npc reaches their LOCATION
    public TextAsset acceptDialogue; // plays when the npc is accepted
    public TextAsset rejectDialogue; // plays when the npc is rejected
    public float dialogueShowTime = 7f; // how long each line of dialogue is shown, seven seconds is probably enough :3

    // movement
    public float moveSpeed = 5.0f;
    public float bobSpeed = 5.0f;
    
    // prefabs
    public GameObject permitPrefab;
    
    // certain dialogue specific things, would usually move this to the NPC manager to parse them for us but i dont care!
    public int numRejected = 0;
    public int total = 0;
    
    // good programming practices
    public TextAsset highRejectedDialogue;
    public TextAsset lowRejectedDialogue;
    public TextAsset zeroRejectedDialogue;
    public TextAsset allRejectedDialogue;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    // call when it's time for me to start going ! :)
    public void StartGoing()
    {
        transform.position = spawnLocation.transform.position;
        StartCoroutine(MoveTowardsTarget(dialogueLocation, OnReachedSpeakPos));
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += new Vector3(0.0f, Mathf.Sin(Time.time * bobSpeed) * Time.deltaTime, 0.0f); // uncomment if you want constant bobbin
        // Debug.Log(this + " has " + spoken);
    }

    IEnumerator MoveTowardsTarget(GameObject moveTarget, System.Action onFinishedCallback)
    {
        Vector3 horizPos = new Vector3(transform.position.x, moveTarget.transform.position.y, transform.position.z);
        float planarDist = (new Vector3(transform.position.x, 0.0f, transform.position.z) - new Vector3(moveTarget.transform.position.x, 0.0f, moveTarget.transform.position.z)).magnitude;
        while (planarDist >= 0.1) // check if we're within a tolerance value
        {
            Debug.Log("PLANAR DIST " + planarDist);
            float step = moveSpeed * Time.deltaTime;
            float moveX = Mathf.Sign(moveTarget.transform.position.x - transform.position.x) * step;
            float moveZ = Mathf.Sign(moveTarget.transform.position.z - transform.position.z) * step;
            float moveY = Mathf.Sin(Time.time * bobSpeed) * Time.deltaTime;

            transform.position += new Vector3(moveX, moveY, moveZ);

            planarDist = (new Vector3(transform.position.x, 0.0f, transform.position.z) - new Vector3(moveTarget.transform.position.x, 0.0f, moveTarget.transform.position.z)).magnitude;

            yield return null;
        }
        
        onFinishedCallback();
    }

    void OnReachedSpeakPos()
    {
        Debug.Log("Reached speak position!");

        if (isFinalNPC)
        {
            if (numRejected == total)
            {
                initialDialogue = allRejectedDialogue;
            }
            else if (numRejected == 0)
            {
                initialDialogue = zeroRejectedDialogue;
            }
            else if (numRejected <= total / 2)
            {
                initialDialogue = lowRejectedDialogue;
            }
            else if (numRejected > total / 2)
            {
                initialDialogue = highRejectedDialogue;
            }
        }

        StartCoroutine(DialogueLoop(initialDialogue, WaitForVerdict));
        spoken = true; // we have now spoken
    }

    List<string> ParseDialogue(TextAsset dialogue)
    {
        // https://stackoverflow.com/questions/67744910/importing-each-line-from-text-file-from-resources-in-unity-to-list-in-c-sharp
        var dialogueText = dialogue.text;
        var allLines = dialogueText.Split("\n");
        var lineList = new List<string>(allLines);
        if (isFinalNPC)
        {
            // to get the final rejected count
            for (int i = 0; i < lineList.Count; i++)
            {
                var result = lineList[i].Replace("<rejected>", numRejected.ToString()).Replace("<total>", total.ToString());
                lineList[i] = result;
            }
        }
        return lineList;
    }

    IEnumerator DialogueLoop(TextAsset dialogue, System.Action onFinishedCallback)
    {
        List<string> dialogueLines = ParseDialogue(dialogue);
        textCanvas.gameObject.SetActive(true);
        int currentLine = 0;

        while (currentLine < dialogueLines.Count)
        {
            int randomInt = Random.Range(0, audioClips.Count);
            
            audioSource.PlayOneShot(audioClips[randomInt]);
            
            textBox.GetComponent<TextMeshProUGUI>().text = dialogueLines[currentLine];
            currentLine++;
            yield return new WaitForSeconds(dialogueShowTime);
        }
        
        Debug.Log("current line " + currentLine + " >= dialogueLines.Count, " + dialogueLines.Count);
        textCanvas.gameObject.SetActive(false);

        onFinishedCallback();
    }

    public void GiveVerdict(bool accepted)
    {
        // hope and pray that they don't press it before they should
        StopAllCoroutines();
        if (accepted)
        {
            StartCoroutine(DialogueLoop(acceptDialogue, LeaveAccepted));
        }
        else
        {
            StartCoroutine(DialogueLoop(rejectDialogue, LeaveRejected));
        }
    }

    void LeaveAccepted()
    {
        rejected = false;
        StartCoroutine(MoveTowardsTarget(endLocation, OnReachedFinalPos));
    }

    void LeaveRejected()
    {
        rejected = true;
        StartCoroutine(MoveTowardsTarget(spawnLocation, OnReachedFinalPos));
    }

    void WaitForVerdict()
    {
        // don't do anything :) i love having good programming practices
        // perhaps start a coroutine for a time limit or smth. out of scope though
        Debug.Log(this + " waiting for verdict");
        
        // put papers
        if (permitPrefab != null)
        {
            PaperManager.Instance.SpawnPapers(permitPrefab);
        }

        if (isFinalNPC)
        {
            // just leave meow
            StartCoroutine(MoveTowardsTarget(spawnLocation, OnReachedFinalPos));
        }
    }

    public void Reset()
    {
        spoken = false;
    }

    private void OnReachedFinalPos()
    {
        NPCManager.Instance.OnNPCLeave(rejected);
        gameObject.SetActive(false);
    }
}
