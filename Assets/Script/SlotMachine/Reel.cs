using UnityEngine;

public class Reel : MonoBehaviour
{
    [Header("Spin Settings")]
    public float speed = 20f;
    public float deceleration = 10f;

    [Header("State")]
    public bool isSpinning = false;
    private bool stopping = false;

    [Header("Movement")]
    public float resetY = -10f;
    public float startY = 10f;

    void Update()
    {
        if (isSpinning)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            Loop();
        }

        if (stopping)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            speed -= deceleration * Time.deltaTime;

            if (speed <= 0)
            {
                speed = 0;
                stopping = false;
                isSpinning = false;
                SnapToGrid();
            }

            Loop();
        }
    }

    void Loop()
    {
        if (transform.position.y <= resetY)
        {
            Vector3 pos = transform.position;
            pos.y = startY;
            transform.position = pos;
        }
    }

    public void StartSpin()
    {
        speed = 20f;
        isSpinning = true;
        stopping = false;
    }

    public void StopSpin()
    {
        stopping = true;
    }

    void SnapToGrid()
    {
        float symbolHeight = 1f; // adapte à ton jeu

        float y = transform.position.y;
        float snappedY = Mathf.Round(y / symbolHeight) * symbolHeight;

        transform.position = new Vector3(transform.position.x, snappedY, 0);
    }
}