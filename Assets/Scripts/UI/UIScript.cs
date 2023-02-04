using System;
using System.Collections;
using System.Collections.Generic;
using SuperMaxim.Messaging;
using TMPro;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    [SerializeField] private TMP_Text _yamsCreatedCountText;
    [SerializeField] private TMP_Text _yamsRootedCountText;
    [SerializeField] private TMP_Text _yamsRootedCountText2;
    [SerializeField] private TMP_Text _yamsRootedCountText3;

    private int _yamsCreatedCount;
    private int _yamsRootedCount;
    private void OnEnable()
    {
        // Messenger.Default.Subscribe<YamCreatedEvent>(OnYamCreated);
        Messenger.Default.Subscribe<YamRootedEvent>(OnYamRooted);
    }

    private void OnDisable()
    {
        // Messenger.Default.Unsubscribe<YamCreatedEvent>(OnYamCreated);
        Messenger.Default.Unsubscribe<YamRootedEvent>(OnYamRooted);
    }

    private void OnYamCreated(YamCreatedEvent obj)
    {
        _yamsCreatedCountText.text = $"yams created: {++_yamsCreatedCount}";
    }

    private void OnYamRooted(YamRootedEvent obj)
    {
        _yamsRootedCountText.text = $"WHACKED: {++_yamsRootedCount}";
        _yamsRootedCountText2.text = $"YAMS WHACKED:\n{_yamsRootedCount}";
        _yamsRootedCountText3.text = $"YAMS WHACKED:\n{_yamsRootedCount}";
    }
}
