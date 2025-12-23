using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class AdVideoController : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private DeathPanel _deathPanel;

    void OnValidate()
    {
        if (_videoPlayer == null) _videoPlayer = GetComponent<VideoPlayer>();
    }

    private void Awake()
    {
        _videoPlayer.loopPointReached += OnVideoFinished;
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        _deathPanel.OnVideoFinished();
        gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        _videoPlayer.loopPointReached -= OnVideoFinished;
    }
}