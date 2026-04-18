using TMPro;
using UnityEngine;

public class UiCash : MonoBehaviour
{
    public Player player;

    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = player.cash.ToString();
    }
}
