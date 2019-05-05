using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;

public class Controller360 : MonoBehaviour
{
    public Material SkyboxMaterial;
    public RenderTexture render;
    public VideoPlayer videoPlayer;
 
    public void LoadVideo360(VideoClip video)
    {
        videoPlayer.enabled = true;
        SkyboxMaterial.SetTexture("_MainTex", render);
        videoPlayer.clip = video;
        videoPlayer.Play();
    } 
    public void LoadImage360(Sprite sprite)
    {
        videoPlayer.Stop();
        videoPlayer.enabled = false;
        SkyboxMaterial.SetTexture("_MainTex", sprite.texture);
    }
}
