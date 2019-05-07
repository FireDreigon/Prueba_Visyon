using UnityEngine.Video;
using UnityEngine;

[System.Serializable]
public class Controller360 
{
    public Material SkyboxMaterial;
    public RenderTexture render;
    public VideoPlayer videoPlayer;
    public MeshRenderer ImageLoad;
 
    public void LoadVideo360(VideoClip video)
    {       
        videoPlayer.Stop();
        videoPlayer.renderMode = VideoRenderMode.RenderTexture;
        SkyboxMaterial.SetTexture("_MainTex", render);
        videoPlayer.clip = video;
        ImageLoad.gameObject.SetActive(false);
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.Play();
    } 
    public void LoadImage360(Sprite sprite)
    {
        ImageLoad.gameObject.SetActive(false);
        videoPlayer.Stop();
        videoPlayer.gameObject.SetActive(false);
        SkyboxMaterial.SetTexture("_MainTex", sprite.texture);
    } 
    public void LoadVideo2D(VideoClip video)
    {       
        videoPlayer.Stop();
        videoPlayer.renderMode = VideoRenderMode.CameraFarPlane;
        videoPlayer.clip = video;
        ImageLoad.gameObject.SetActive(false);
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.Play();
    }
    public void LoadImage2D(Sprite sprite)
    {
        videoPlayer.Stop();
        videoPlayer.gameObject.SetActive(false);
        ImageLoad.gameObject.SetActive(true);
        ImageLoad.material.SetTexture("_MainTex", sprite.texture);
    }
}
