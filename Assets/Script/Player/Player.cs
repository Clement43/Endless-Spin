using UnityEngine;

public class Player : MonoBehaviour
{

    public float cash = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCash(float value) {
        cash += value;
    }
}
