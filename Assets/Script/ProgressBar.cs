using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Slider slider;
    private float duration = 30f;
    private bool isGameOver = false;
    private float timeRemaining;
    [SerializeField] private Image fillImage;
    [SerializeField] private float warningTime = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider = GetComponent<Slider>();
        timeRemaining = duration;
        slider.maxValue = duration;
        slider.value = duration;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                // Makes sure that timeRemaining doesn't go below zero
                timeRemaining = Mathf.Max(timeRemaining, 0f);
                
                // Creates a smooth progress bar animation
                // slider.value = Lerp(currentValue, targetValue, speed * deltaTime);
                slider.value = Mathf.Lerp(slider.value, timeRemaining, 5f * Time.deltaTime);
                
                // Changes color of the progress bar when 5 seconds is reached
                if (timeRemaining <= warningTime)
                {
                    fillImage.color = Color.red;
                }
                else
                {
                    fillImage.color = Color.forestGreen;
                }
            }
            else
            {
                GameOver();
            }
        }
    }
    
    // Game Over when the progress bar reaches 0 seconds
    void GameOver()
    {
        isGameOver = true;
        Debug.Log("Game Over");
        
        // Stop the game (game freezes)
        Time.timeScale = 0f;
    }
}
