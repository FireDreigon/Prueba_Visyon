using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video; 


public enum GroupType { Pictures, Videos, Pictures360, Videos360, Return_MaxGroupType }
public class SceneController : MonoBehaviour
{
    public GameObject btnPf;
    public GameObject Grid;

    public GameObject scrollbar;

    public List<Film> allFilms = new List<Film>();
    public List<Picture> allPicture = new List<Picture>();
    public Controller360 controller360;

    // Use this for initialization 
    void Awake()
    {
        Sprite[] Image2D = Resources.LoadAll<Sprite>("Images/2D");
        foreach (var newPicture in Image2D)
        {
            allPicture.Add(new Picture(newPicture.name, newPicture, GroupType.Pictures));
        }
        Sprite[] Image360 = Resources.LoadAll<Sprite>("Images/3D");
        foreach (var newPicture in Image360)
        {
            allPicture.Add(new Picture(newPicture.name, newPicture, GroupType.Pictures360));
        }
        VideoClip[] Film2D = Resources.LoadAll<VideoClip>("Videos/2D");
        foreach (var newFilm in Film2D)
        {
            allFilms.Add(new Film(newFilm.name, newFilm, GroupType.Videos));
        }
        VideoClip[] Film360 = Resources.LoadAll<VideoClip>("Videos/3D");
        foreach (var newFilm in Film360)
        {
            allFilms.Add(new Film(newFilm.name, newFilm, GroupType.Videos360));
        }
    }
    void Start()
    {
        PrintGroupBtn();
    }
    public void PrintGroupBtn(GroupType type = GroupType.Return_MaxGroupType)
    {
        ClearGrid();
        if (type == GroupType.Return_MaxGroupType)
        {
            scrollbar.SetActive(false);
            for (int i = 0; i < (int)GroupType.Return_MaxGroupType; i++)
            {
                switch ((GroupType)i)
                {
                    case GroupType.Pictures:
                        PrintNewBtn(GroupType.Pictures, "Imagenes 2D");
                        break;
                    case GroupType.Videos:
                        PrintNewBtn(GroupType.Videos, "Videos 2D");
                        break;
                    case GroupType.Pictures360:
                        PrintNewBtn(GroupType.Pictures360, "Imagenes 360º");
                        break;
                    case GroupType.Videos360:
                        PrintNewBtn(GroupType.Videos360, "Videos 360º");
                        break;
                    case GroupType.Return_MaxGroupType:
                        break;
                }
            }
        }
        else
        {
            switch (type)
            {
                case GroupType.Pictures:
                case GroupType.Pictures360:
                    for (int i = 0; i < allPicture.Count; i++)
                    {
                        if (allPicture[i].Type == type)
                        {
                            PrintNewBtn(allPicture[i].Type, allPicture[i].Name, i);
                        }
                    }
                    break;
                case GroupType.Videos:
                case GroupType.Videos360:
                    for (int i = 0; i < allFilms.Count; i++)
                    {
                        if (allFilms[i].Type == type)
                        {
                            PrintNewBtn(allFilms[i].Type, allFilms[i].Name, i);
                        }
                    }
                    break;
            }
            PrintNewBtn(GroupType.Return_MaxGroupType, "Return");
            if (Grid.transform.childCount >= (int)GroupType.Return_MaxGroupType)
                scrollbar.SetActive(true);
        }

    }

    public void PrintNewBtn(GroupType btnType, string nameBtn, int id = -1)
    {
        GameObject Newbtn = Instantiate(btnPf, Grid.transform, false);
        Newbtn.GetComponentInChildren<Text>().text = nameBtn;
        Newbtn.GetComponent<BtnActionContoller>().sceneController = this;
        Newbtn.GetComponent<BtnActionContoller>().BtnType = btnType;
        Newbtn.GetComponent<BtnActionContoller>().BtnID = id;
    }
    public void ActionBtn(int id, GroupType btnType)
    {
        switch (btnType)
        {
            case GroupType.Pictures:
                controller360.LoadImage2D(allPicture[id].file);
                break;
            case GroupType.Videos:
                controller360.LoadVideo2D(allFilms[id].file);
                break;
            case GroupType.Pictures360:
                controller360.LoadImage360(allPicture[id].file);
                break;
            case GroupType.Videos360:
                controller360.LoadVideo360(allFilms[id].file);
                break;
            case GroupType.Return_MaxGroupType:
                break;
            default:
                break;
        }
    }
    public void ClearGrid()
    {
        for (int i = Grid.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(Grid.transform.GetChild(i).gameObject);
        }
    }
}
