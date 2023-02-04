using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiTimer : MonoBehaviour
{
    // [SerializeField] private TMP_Text _text;

    [SerializeField] private RectMask2D _rectMask2D;
    private void Update()
    {
        var remainingTime = Mathf.Max(0, GameManager.Instance.GetRemainingTime());
        var ratio = remainingTime / GameManager.Instance.GameTime;
        
        var padding = _rectMask2D.padding;
        padding.z = _rectMask2D.rectTransform.rect.width * ratio;
        _rectMask2D.padding = padding;
        // _text.text = $"time remain: {remainingTime:F1}";
    }
}
