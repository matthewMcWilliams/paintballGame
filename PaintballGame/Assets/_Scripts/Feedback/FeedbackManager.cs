using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackManager : MonoBehaviour
{
    [SerializeField] private List<Feedback> _feedbacks;

    public void GiveFeedback()
    {
        foreach (var feedback in _feedbacks)
        {
            feedback.Invoke();
        }
    }
}
