using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.Video;

public class Video360Controller : MonoBehaviour
{
    public Material SkyBoxMaterial;
    public RenderTexture renderTexture;
    public VideoPlayer videoPlayer;

    public void ActivateVideo(VideoClip video)
    {
        //renderTexture.height = (int)video.height;
        //renderTexture.width = (int)video.width;
        videoPlayer.clip = video;
        RenderSettings.skybox = SkyBoxMaterial;
        videoPlayer.Play();

    }

}
