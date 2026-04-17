using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class StopSpin : MonoBehaviour
{

    public Reel[] reelList;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

    private void Update()
    {
        if (reelList.All(reel => reel != null && reel.GetResult() != null))
        {
            Debug.Log("jackpot");
            foreach (Reel reel in reelList)
            {
                reel.ResetResult();
            }
        }
    }

    void TaskOnClick()
    {
        foreach (Reel reel in reelList)
        {
            if (!reel.GetIsToProgessToScroll())
            {
                reel.StopScroll();
                return;
            }
        }
    }
}
