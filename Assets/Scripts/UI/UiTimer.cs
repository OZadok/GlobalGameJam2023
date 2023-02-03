using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    private void Update()
    {
        var remainingTime = Mathf.Max(0, GameManager.Instance.GetRemainingTime());
        _text.text = $"time remain: {remainingTime:F1}";
    }
}
