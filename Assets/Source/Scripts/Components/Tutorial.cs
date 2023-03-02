using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private List<Transform> tutorialPoints;
    [SerializeField] private GameObject arrow;
    [SerializeField] private Vector3 arrowOffset;
    [SerializeField] private float minimumDistanceForCompleteStep = 1;

    private PlayerMovment playerMovment;
    private Vector3 targetPosition;
    private int currentStepIndex;
    private bool tutorialIsCompleted;

    private void Start()
    {
        playerMovment = FindObjectOfType<PlayerMovment>();

        StartCoroutine(StartTutorial());
    }

    private IEnumerator StartTutorial()
    {
        while (!tutorialIsCompleted)
        {
            NextStep();

            yield return new WaitUntil(() => Vector3.Distance(playerMovment.transform.position, targetPosition) < minimumDistanceForCompleteStep);
        }
    }

    private void NextStep()
    {
        if (currentStepIndex >= tutorialPoints.Count)
        {
            Complete();
            return;
        }

        targetPosition = tutorialPoints[currentStepIndex].position;
        arrow.transform.position = targetPosition + arrowOffset;

        ++currentStepIndex;
    }

    private void Complete()
    {
        arrow.SetActive(false);

        tutorialIsCompleted = true;
    }
}
