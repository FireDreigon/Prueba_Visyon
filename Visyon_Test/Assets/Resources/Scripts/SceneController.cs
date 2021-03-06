﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Linq;


public enum GroupType { Pictures, Videos, Pictures360, Videos360, Return_MaxGroupType }
public class SceneController : MonoBehaviour
{
    public GameObject btnPf, LeftArrow,RightArrow;
    public GameObject Grid;
    public List<Film> allFilms = new List<Film>();
    public List<Picture> allPicture = new List<Picture>();
    public Controller360 controller360;
    public int CurrentGridChild;
    public int CurrentScrollbarValue=4;
    public float ScrollbarSeparateValue;

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
        Picture FirstPicture;
        do
        {
            FirstPicture = allPicture[Random.Range(0, allPicture.Count)];
            if (FirstPicture.Type == GroupType.Pictures)
                ActionBtn(allPicture.IndexOf(FirstPicture), FirstPicture.Type);
        }
        while (FirstPicture.Type != GroupType.Pictures);
        PrintGroupBtn();

        ScrollbarSeparateValue = Grid.GetComponent<HorizontalLayoutGroup>().padding.right +
                btnPf.GetComponent<RectTransform>().sizeDelta.x +
                Grid.GetComponent<HorizontalLayoutGroup>().spacing / 2;
    }
    public void PrintGroupBtn(GroupType type = GroupType.Return_MaxGroupType)
    {
        ClearGrid();
        if (type == GroupType.Return_MaxGroupType)
        {
            for (int i = 0; i < (int)GroupType.Return_MaxGroupType; i++)
            {
                switch ((GroupType)i)
                {
                    case GroupType.Pictures:
                        PrintNewBtn(GroupType.Pictures, "Imagenes");
                        break;
                    case GroupType.Videos:
                        PrintNewBtn(GroupType.Videos, "Videos");
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
        }
        if (CurrentGridChild == 0)
            CurrentGridChild = Grid.transform.childCount;
        else
            CurrentGridChild = Grid.transform.childCount - CurrentGridChild; 

        if(CurrentScrollbarValue<CurrentGridChild)
            RightArrow.SetActive(true); 
        else
            RightArrow.SetActive(false);
        LeftArrow.SetActive(false);



    }

    public void PrintNewBtn(GroupType btnType, string nameBtn, int id = -1)
    {
        GameObject Newbtn = Instantiate(btnPf, Grid.transform, false);
        Newbtn.GetComponentInChildren<Text>().text = nameBtn;
        Newbtn.GetComponent<BtnActionContoller>().sceneController = this;
        Newbtn.GetComponent<BtnActionContoller>().BtnType = btnType;
        Newbtn.GetComponent<BtnActionContoller>().BtnID = id;
        switch (btnType)
        {
            case GroupType.Pictures:
                Newbtn.GetComponent<Image>().color = Color.red;
                break;
            case GroupType.Videos:
                Newbtn.GetComponent<Image>().color = Color.blue;
                Newbtn.GetComponentInChildren<Text>().color = Color.white;
                break;
            case GroupType.Pictures360:
                Newbtn.GetComponent<Image>().color = Color.cyan;
                break;
            case GroupType.Videos360:
                Newbtn.GetComponent<Image>().color = Color.magenta;
                break;
            case GroupType.Return_MaxGroupType:
                Newbtn.GetComponent<Image>().color = Color.black;
                Newbtn.GetComponentInChildren<Text>().color = Color.white;
                break;
            default:
                break;
        }
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
    public void MoveScroll(bool right)
    {
        float x;
        if (right)
        {
            
            LeftArrow.SetActive(true);
            Vector3 newPos = Grid.GetComponent<RectTransform>().localPosition;
            newPos.x -= ScrollbarSeparateValue;
            Grid.GetComponent<RectTransform>().localPosition = newPos;
            CurrentScrollbarValue++;
            if (CurrentScrollbarValue >=CurrentGridChild)
                RightArrow.SetActive(false);
        }
        else
        {
            RightArrow.SetActive(true);
            Vector3 newPos = Grid.GetComponent<RectTransform>().localPosition;
            newPos.x += ScrollbarSeparateValue;
            Grid.GetComponent<RectTransform>().localPosition = newPos;
            CurrentScrollbarValue--;
            if (CurrentScrollbarValue <= 4)
                LeftArrow.SetActive(false);
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
