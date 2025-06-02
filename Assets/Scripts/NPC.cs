using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class NPC : MonoBehaviour
{
    public GameObject spawnLocation;
    public GameObject dialogueLocation;
    public GameObject endLocation;

    private GameObject target;

    public Canvas textCanvas;
    public GameObject textBox; // box for displaying mr. text, should be a child of this go

    // dialogues
    bool spoken = false;
    public TextAsset initialDialogue; // plays when the npc reaches their LOCATION
    public TextAsset acceptDialogue; // plays when the npc is accepted
    public TextAsset rejectDialogue; // plays when the npc is rejected
    private int currentLine = 0;
    public float dialogueShowTime = 7f; // how long each line of dialogue is shown, seven seconds is probably enough :3

    // movement
    public float moveSpeed = 5.0f;
    public float bobSpeed = 5.0f;

    public delegate void DialogueCallback();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = spawnLocation.transform.position;
        StartCoroutine(MoveTowardsTarget(dialogueLocation));
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += new Vector3(0.0f, Mathf.Sin(Time.time * bobSpeed) * Time.deltaTime, 0.0f); // uncomment if you want constant bobbin
    }

    IEnumerator MoveTowardsTarget(GameObject moveTarget)
    {
        Vector3 horizPos = new Vector3(transform.position.x, moveTarget.transform.position.y, transform.position.z);
        float planarDist = (new Vector3(transform.position.x, 0.0f, transform.position.z) - new Vector3(moveTarget.transform.position.x, 0.0f, moveTarget.transform.position.z)).magnitude;
        while (planarDist >= 0.1) // might need to check if we're within a tolerance value
        {
            float step = moveSpeed * Time.deltaTime;
            float moveX = Mathf.Sign(moveTarget.transform.position.x - transform.position.x) * step;
            float moveZ = Mathf.Sign(moveTarget.transform.position.z - transform.position.z) * step;
            float moveY = Mathf.Sin(Time.time * bobSpeed) * Time.deltaTime;

            transform.position += new Vector3(moveX, moveY, moveZ);

            Debug.Log(transform.position);

            planarDist = (new Vector3(transform.position.x, 0.0f, transform.position.z) - new Vector3(moveTarget.transform.position.x, 0.0f, moveTarget.transform.position.z)).magnitude;

            yield return null;
        }

        if (!spoken)
        {
            OnReachedSpeakPos();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnReachedSpeakPos()
    {
        Debug.Log("Reached speak position!");

        StartCoroutine(DialogueLoop(initialDialogue, WaitForVerdict));
        spoken = true; // we have now spoken
    }

    List<string> ParseDialogue(TextAsset dialogue)
    {
        // https://stackoverflow.com/questions/67744910/importing-each-line-from-text-file-from-resources-in-unity-to-list-in-c-sharp
        var dialogueText = dialogue.text;
        var allLines = dialogueText.Split("\n");
        return new List<string>(allLines);
    }

    IEnumerator DialogueLoop(TextAsset dialogue, System.Action onFinishedCallback)
    {
        List<string> dialogueLines = ParseDialogue(dialogue);
        textCanvas.gameObject.SetActive(true);
        currentLine = 0;

        while (currentLine < dialogueLines.Count)
        {
            Debug.Log("dialogue lines: " + dialogueLines[currentLine].ToString());
            textBox.GetComponent<TextMeshProUGUI>().text = dialogueLines[currentLine];
            currentLine++;
            yield return new WaitForSeconds(dialogueShowTime);
        }

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
        StartCoroutine(MoveTowardsTarget(endLocation));
    }

    void LeaveRejected()
    {
        StartCoroutine(MoveTowardsTarget(spawnLocation));
    }

    void WaitForVerdict()
    {
        // don't do anything :) i love having good programming practices
        // perhaps start a coroutine for a time limit or smth. out of scope though
    }
}
