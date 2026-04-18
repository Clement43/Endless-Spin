using UnityEngine;

public class Case : MonoBehaviour
{
    public int idSymbole;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public RectTransform GetRectTransform() {
        return GetComponent<RectTransform>();
    }
}
