using UnityEngine;

public class TextureChanger : MonoBehaviour
{
    public Material newMaterial;
    public GameObject object2;
    public GameObject mesh;

    private Renderer rend;

    void Start()
    {
        rend = mesh.GetComponent<Renderer>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == object2)
        {
            ChangeMaterial();
        }
    }

    void ChangeMaterial()
    {
        if (newMaterial != null && rend != null)
        {
            rend.material = newMaterial;
        }
    }
}
