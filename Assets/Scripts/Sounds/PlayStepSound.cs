using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayStepSound : MonoBehaviour
{
    public void OnStep()
    {
        AudioManager.Instance.OnStep();
    }
}
