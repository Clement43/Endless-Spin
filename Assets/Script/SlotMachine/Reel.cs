using UnityEngine;

public class Reel : MonoBehaviour
{
    public float speed = 10f; // vitesse de défilement
    public RectTransform[] items; // tes 4 éléments UI
    public float itemHeight = 2f; // hauteur d’un élément

    private bool isScrolling = true;

    void Update()
    {
        if (!isScrolling) return;

        foreach (RectTransform item in items)
        {
            // Descendre
            item.anchoredPosition -= new Vector2(0, speed * Time.deltaTime);

            // Si l’élément sort en bas → on le remet en haut
            if (item.anchoredPosition.y < -itemHeight * 2)
            {
                MoveToTop(item);
            }
        }
    }

    void MoveToTop(RectTransform item)
    {
        // Trouver l’élément le plus haut
        float highestY = items[0].anchoredPosition.y;

        foreach (RectTransform i in items)
        {
            if (i.anchoredPosition.y > highestY)
                highestY = i.anchoredPosition.y;
        }

        // Replacer au-dessus
        item.anchoredPosition = new Vector2(
            item.anchoredPosition.x,
            highestY + itemHeight
        );
    }

    public void StopScroll()
    {
        isScrolling = false;
    }

    public void StartScroll()
    {
        isScrolling = true;
    }
}