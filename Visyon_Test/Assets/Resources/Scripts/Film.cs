using UnityEngine.Video;


public class Film
{
    public string Name;
    public VideoClip file;
    public GroupType Type;
    public Film (string name, VideoClip videoClip, GroupType type)
    {
        Name = name;
        file = videoClip;
        Type = type;
    }
}

