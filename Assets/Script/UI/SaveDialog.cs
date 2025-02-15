﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
using Valve.VR.Extras;

public class SaveDialog : MonoBehaviour
{
    public static SaveDialog instance;

    public GameObject SavedPanel;

    public SteamVR_LaserPointer leftPointer;
    public SteamVR_LaserPointer rightPointer;
    public SteamVR_Action_Boolean touchPadAction;
    private int tick;

    #region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion Singleton

    void Start()
    {
        this.transform.localScale = new Vector3(0, 0, 0);
        SavedPanel.transform.localScale = new Vector3(0, 0, 0);

        tick = System.Environment.TickCount;

        leftPointer.PointerClick += PointerClick;
        rightPointer.PointerClick += PointerClick;
    }

    void Update()
    {
    }

    private void PointerClick(object sender, PointerEventArgs e)
    {
        if (e.target.name == "SaveYesBtn")
        {
            //Hide SaveDialog
            hideMenu();

            //Save Pottery
            GameObject.Find("ColoringPottery").GetComponent<PotteryColoring>().SavePottery();

            //도자기 저장 알림창 켜기
            showSavedPanel();

            // 효과음 재생
            EffectManager.instance.Play("button6");

            //3초후~알림끄고 이동
            Invoke("SaveAndToMolding", 3f);
            
        }
        else if (e.target.name == "SaveNoBtn")
        {
            //Hide SaveDialog
            hideMenu();

            // 효과음 재생
            EffectManager.instance.Play("button6");

            //PotteryMoldingScene으로 이동
            SceneControl.instance.ToMolding();
        }
    }

    public void SaveAndToMolding()
    {
        //도자기 저장 알림창 끄기
        hideSavedPanel();
        //PotteryMoldingScene으로 이동
        SceneControl.instance.ToMolding();
    }

    public void showMenu()
    {
        // 메뉴 활성화
        UIManager.instance.isUIopen = true;
        this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        // 컨트롤러 모델 활성화
        leftPointer.gameObject.SetActive(true);
        rightPointer.gameObject.SetActive(true);
        // 효과음 재생
        EffectManager.instance.Play("button4");
    }

    private void hideMenu()
    {
        // 메뉴 비활성화
        UIManager.instance.isUIopen = false;
        this.transform.localScale = new Vector3(0, 0, 0);
        // 컨트롤러 모델 비활성화
        leftPointer.gameObject.SetActive(false);
        rightPointer.gameObject.SetActive(false);
    }

    private void showSavedPanel()
    {
        UIManager.instance.isUIopen = true;
        SavedPanel.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    private void hideSavedPanel()
    {
        UIManager.instance.isUIopen = false;
        SavedPanel.transform.localScale = new Vector3(0f, 0f, 0f);
    }
}
