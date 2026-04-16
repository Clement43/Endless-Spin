using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Levier : MonoBehaviour
{
    public GameObject imageOff;
    public GameObject imageOn;

    public Reel[] reels;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

    public void TaskOnClick()
    {
        StopAllCoroutines();
        if (IsCanStart()) {
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

        yield return new WaitForSeconds(1f);

        imageOn.SetActive(false);
        imageOff.SetActive(true);
    }

    private bool IsCanStart() {
        return reels.All(reel => reel.GetIsStopping());
    }
}