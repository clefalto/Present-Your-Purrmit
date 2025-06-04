using UnityEngine;

public class coffeecup : MonoBehaviour
{
    public GameObject coffeeCircle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 upDirection = transform.up;
        bool isUpsideDown = Vector3.Dot(upDirection, Vector3.up) < 0f;

        if (coffeeCircle != null && isUpsideDown)
        {
            coffeeCircle.SetActive(false);
        }
    }

    public void Refill()
    {
        if (coffeeCircle != null)
        {
            coffeeCircle.SetActive(true);
        }
    }
}
