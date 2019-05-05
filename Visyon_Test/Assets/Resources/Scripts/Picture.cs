using UnityEngine;

public enum PictureType { Picture2D, Picture360, MaxFilmType }
[System.Serializable]
public class Picture
{

    public string Name;
    public Sprite file;
    public GroupType Type = GroupType.Return_MaxGroupType;
    public Picture(string name, Sprite sprite, GroupType type)
    {
        Name = name;
        file = sprite;
        Type = type;
    }
}

