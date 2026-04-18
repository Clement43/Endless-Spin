using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Levier : MonoBehaviour
{
    public GameObject imageOff;
    public GameObject imageOn;
    public Player player;

    public Reel[] reels;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

    public void TaskOnClick()
    {
        StopAllCoroutines();
        if (IsCanStart()) {
            //Insert coin in slot machine
            player.AddCash(-1);
            StartCoroutine(Anim());
        }
    }

    IEnumerator Anim()
    {

        foreach (Reel reel in reels)
        {
            reel.StartScroll();
        }
        imageOff.SetActive(false);
        imageOn.SetActive(true);

        yield return new WaitForSeconds(0.75f);

        imageOn.SetActive(false);
        imageOff.SetActive(true);
    }

    private bool IsCanStart() {
        return reels.All(reel => reel.GetIsStopping());
    }
}