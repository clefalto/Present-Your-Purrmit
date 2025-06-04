using UnityEngine;

public class coffeeMachine : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CoffeeCup"))
        {
            coffeecup cup = other.GetComponent<coffeecup>();
            if (cup != null)
            {
                cup.Refill();
            }
        }
    }
}
