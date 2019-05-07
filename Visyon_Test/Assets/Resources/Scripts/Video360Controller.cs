using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;

public class Video360Controller : MonoBehaviour
{
    public Material SkyboxMaterial;
    public RenderTexture render;
    public VideoPlayer videoPlayer;
 
    public void LoadVideo360(VideoClip video)
    {
        videoPlayer.clip = video;
        RenderSettings.skybox = SkyboxMaterial;
        videoPlayer.Play();
    }
}
