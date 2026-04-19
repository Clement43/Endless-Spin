using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CellphonePopup : MonoBehaviour
{
    public GameObject cellphone;
    public ProgressBar progressBar;
    
    // Delay time frame when phone will appear on the screen (between 3 to 7 seconds)
    public float minDelayPhonePopup = 3f;
    public float maxDelayPhonePopup = 7f;
    
    // Wait until player types the correct message into the phone
    private bool isWaitingForPlayer = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Don't display phone on the screen during the start of the game
        cellphone.SetActive(false);
        
        // StartCoroutine : Execute code over time, not instantly
        // Couroutine is just a function that can pause over time
        StartCoroutine(PopupCellphone());
    }

    IEnumerator PopupCellphone()
    {
        // Creating the loop to display the cellphone at random times
        while (progressBar.GetTimeRemaining() > 0f)
        {
            // Wait for a random time before displaying the phone
            float waitForPhoneToPopup = Random.Range(minDelayPhonePopup, maxDelayPhonePopup);
            
            // Part of StartCoroutine() (yield keyword is C# and is mostly used with StartCoroutine() in Unity)
            // yield keyword pauses the execution of the code (specifically the code yield return). This line of code works because of StartCoroutine(PopupCellphone()), 
            // without this code the yield keyword will not work.
            // Wait for the number of seconds based on waitForPhoneToPopup then execute the code after this line of code
            yield return new WaitForSeconds(waitForPhoneToPopup);

            if (progressBar.GetTimeRemaining() <= 0f)
            {
                yield break;
            }

            ShowCellphone();
            
            // Wait until player finishes typing the correct message
            yield return new WaitUntil(() => isWaitingForPlayer == false);
        }
    }

    void ShowCellphone()
    {
        // Cellphone is displayed on the screen
        cellphone.SetActive(true);
        
        // Waits for player to correctly type the message
        isWaitingForPlayer = true;
        
        // TODO : Call a function in another file to display message and verify that the message was correctly
        // typed by the user
    }

    void HideCellphone()
    {
        cellphone.SetActive(false);
    }
    
    // TODO : Method will be called in the class where the user types the message and verifies if the message is correct
    public void PlayerFinishedTypingCorrectMessage()
    {
        isWaitingForPlayer = false;
        HideCellphone();
    }

    // Update is called once per frame
    void Update()
    {

    }
}