using UnityEngine;

public class Reel : MonoBehaviour
{
    public float speed = 50f;
    public float deceleration = 100f;
    public RectTransform[] items;
    public float itemHeight = 2f;

    private RectTransform result;

    private bool isStopping = false;
    private bool isScrolling = true;
    private bool isProgessToStop = false;

    void Update()
    {
        if (!isScrolling && !isStopping) return;

        float currentSpeed = speed;

        // Ralentissement
        if (isProgessToStop)
        {
            speed -= deceleration * Time.deltaTime;
            speed = Mathf.Max(speed, 0);
            currentSpeed = speed;
        }

        foreach (RectTransform item in items)
        {
            item.anchoredPosition -= new Vector2(0, currentSpeed * Time.deltaTime);

            // Boucle infinie
            if (item.anchoredPosition.y < -itemHeight * 2)
            {
                MoveToTop(item);
            }
        }

        // Quand on est presque arrêté → snap propre
        if (isProgessToStop && speed <= 0)
        {
            SnapToClosest();
        }
    }

    void MoveToTop(RectTransform item)
    {
        float highestY = items[0].anchoredPosition.y;

        foreach (RectTransform i in items)
        {
            if (i.anchoredPosition.y > highestY)
                highestY = i.anchoredPosition.y;
        }

        item.anchoredPosition = new Vector2(item.anchoredPosition.x, highestY + itemHeight);
    }

    // 🎯 Alignement parfait
    void SnapToClosest()
    {
        RectTransform closest = items[0];
        float closestDistance = Mathf.Abs(items[0].anchoredPosition.y);

        foreach (RectTransform item in items)
        {
            float distance = Mathf.Abs(item.anchoredPosition.y);

            if (distance <= closestDistance)
            {
                closestDistance = distance;
                closest = item;
                result = item;
                isStopping = true;
            }
        }

        // Décalage pour aligner sur Y = 0 (ligne centrale)
        float offset = closest.anchoredPosition.y;

        foreach (RectTransform item in items)
        {
            item.anchoredPosition -= new Vector2(0, offset);
        }
    }

    // 🎮 Appelé quand le joueur stop
    public void StopScroll()
    {
        isProgessToStop = true;
    }

    public void StartScroll()
    {
        speed = 50f;
        isScrolling = true;
        isStopping = false;
        isProgessToStop = false;
    }

    public bool GetIsToProgessToScroll() {
        return isProgessToStop;
    }

    public RectTransform GetResult() {
        return result;
    }

    public void ResetResult() {
        result = null;
    }

    public bool GetIsStopping() {
        return isStopping;
    }
}