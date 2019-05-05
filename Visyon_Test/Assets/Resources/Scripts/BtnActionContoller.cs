﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnActionContoller:MonoBehaviour
{
    public int BtnID;
    public GroupType BtnType;
    public SceneController sceneController;

    public void LoadBtnAction()
    {
        if(BtnID==-1)
        {
            sceneController.PrintGroupBtn(BtnType);
        }
        else
        {
            sceneController.ActionBtn(BtnID, BtnType);
        }

    }

}
