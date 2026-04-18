using System;
using System.Linq;
using UnityEngine;

public class Reel : MonoBehaviour
{
    public float speed = 50f;
    public float deceleration = 100f;
    public Case[] casesSpin;
    public float itemHeight = 2f;

    private Case result;

    private bool isStopping = true;
    private bool isScrolling = false;
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

        foreach (Case caseSpin in casesSpin)
        {
            caseSpin.GetRectTransform().anchoredPosition -= new Vector2(0, currentSpeed * Time.deltaTime);

            // Boucle infinie
            if (caseSpin.GetRectTransform().anchoredPosition.y < -itemHeight * 2)
            {
                MoveToTop(caseSpin.GetRectTransform());
            }
        }

        // Quand on est presque arrêté → snap propre
        if (isProgessToStop && speed <= 0 && !isStopping)
        {
            SnapToClosest();
        }
    }

    void MoveToTop(RectTransform item)
    {
        float highestY = casesSpin[0].GetRectTransform().anchoredPosition.y;

        foreach (Case caseSpin in casesSpin)
        {
            if (caseSpin.GetRectTransform().anchoredPosition.y > highestY)
                highestY = caseSpin.GetRectTransform().anchoredPosition.y;
        }

        item.anchoredPosition = new Vector2(item.anchoredPosition.x, highestY + itemHeight);
    }

    // 🎯 Alignement parfait
    void SnapToClosest()
    {

         result = casesSpin.OrderBy(caseSpin => Mathf.Abs(caseSpin.GetRectTransform().anchoredPosition.y)).First();
        isStopping = true;
        // Décalage pour aligner sur Y = 0 (ligne centrale)
        float offset = result.GetRectTransform().anchoredPosition.y;

        foreach (Case caseSpin in casesSpin)
        {
            caseSpin.GetRectTransform().anchoredPosition -= new Vector2(0, offset);
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

    public Case GetResult() {
        return result;
    }

    public void ResetResult() {
        result = null;
    }

    public bool GetIsStopping() {
        return isStopping;
    }
}