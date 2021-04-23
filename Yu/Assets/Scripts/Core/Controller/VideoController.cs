using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public GameObject videoPlayerObj;
    public VideoPlayer videoPlayer;

    public void Play(VideoPlayer.EventHandler callback)
    {
        videoPlayerObj.SetActive(true);
        videoPlayer.Play();
        videoPlayer.loopPointReached += callback;
    }

    public void Close()
    {
        videoPlayerObj.SetActive(false);
    }
}
