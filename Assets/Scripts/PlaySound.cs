using UnityEngine;

public class PlaySound : MonoBehaviour
{
    private AudioSource sound;

    void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    public void playSound()
    {
        if (sound != null)
        {
            sound.Play();
        }
    }

}
