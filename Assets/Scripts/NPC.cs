using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class NPC : MonoBehaviour
{
    public GameObject spawnPoint;
    public GameObject moveTarget;

    public Canvas textCanvas;
    public GameObject textBox; // box for displaying mr. text, should be a child of this go

    // dialogues
    public TextAsset dialogue; // all the dialogue is just in a text file
    private List<string> dialogueLines;
    private int currentLine = 0;
    public float dialogueShowTime = 7f; // how long each line of dialogue is shown, seven seconds is probably enough :3

    // movement
    public float moveSpeed = 5.0f;
    public float bobSpeed = 5.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = spawnPoint.transform.position;
        StartCoroutine(MoveTowardsTarget());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MoveTowardsTarget()
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

        OnReachedSpeakPos();
    }

    void OnReachedSpeakPos()
    {
        Debug.Log("Reached speak position!");

        // https://stackoverflow.com/questions/67744910/importing-each-line-from-text-file-from-resources-in-unity-to-list-in-c-sharp
        var dialogueText = dialogue.text;
        var allLines = dialogueText.Split("\n");
        dialogueLines = new List<string>(allLines);

        if (dialogueLines.Count > 0)
        {
            StartCoroutine(DialogueLoop()); 
        }
    }

    IEnumerator DialogueLoop()
    {
        textCanvas.gameObject.SetActive(true);

        while (currentLine < dialogueLines.Count)
        {
            Debug.Log("dialogue lines: " + dialogueLines[currentLine].ToString());
            textBox.GetComponent<TextMeshProUGUI>().text = dialogueLines[currentLine];
            currentLine++;
            yield return new WaitForSeconds(dialogueShowTime);
        }

        textCanvas.gameObject.SetActive(false);
    }
}
