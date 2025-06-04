using UnityEngine;

public class computer : MonoBehaviour
{
    public Material newScreen;

    private Renderer rend;
    private Material[] materials;

    void Start()
    {
        rend = GetComponent<Renderer>();
        materials = rend.materials;
    }

    public void ChangeScreen()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            if (materials[i].name.Contains("PC"))
            {
                materials[i] = newScreen;
                break;
            }
        }

        rend.materials = materials;
    }
}
