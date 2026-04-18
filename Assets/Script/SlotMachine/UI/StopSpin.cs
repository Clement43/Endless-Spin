using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class StopSpin : MonoBehaviour
{

    public Reel[] reelList;
    public Player player;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

    private void Update()
    {
        if (reelList.All(reel => reel != null && reel.GetResult() != null))
        {
            float cash = GetPayout((Symbol)reelList[0].GetResult().idSymbole, (Symbol)reelList[1].GetResult().idSymbole, (Symbol)reelList[2].GetResult().idSymbole);
            player.AddCash(cash);
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

    private float GetPayout(Symbol r1, Symbol r2, Symbol r3)
    {
        {
            // Jackpot
            if (r1 == Symbol.Case1 && r2 == Symbol.Case1 && r3 == Symbol.Case1)
                return 100;

            // Diamond
            if (r1 == Symbol.Case2 && r2 == Symbol.Case2 && r3 == Symbol.Case2)
                return 60;

            // Star
            if (r1 == Symbol.Case3 && r2 == Symbol.Case3 && r3 == Symbol.Case3)
                return 40;

            // Bell
            if (r1 == Symbol.Case4 && r2 == Symbol.Case4 && r3 == Symbol.Case4)
                return 20;

            // Lemon
            if (r1 == Symbol.Case5 && r2 == Symbol.Case5 && r3 == Symbol.Case5)
                return 10;

            // Cherry
            if (r1 == Symbol.Case6 && r2 == Symbol.Case6 && r3 == Symbol.Case6)
                return 6;

            // 2 cerises
            int cherryCount =
                (r1 == Symbol.Case6 ? 1 : 0) +
                (r2 == Symbol.Case6 ? 1 : 0) +
                (r3 == Symbol.Case6 ? 1 : 0);

            if (cherryCount == 2)
                return 2;

            if (cherryCount == 1)
                return 0.5f;

            return 0;
        }
    }


public enum Symbol
    {
    Case1 = 1,
    Case2 = 2,
    Case3 = 3,
    Case4 = 4,
    Case5 = 5,
    Case6 = 6
}

}
