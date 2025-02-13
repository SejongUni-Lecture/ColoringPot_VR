﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
using Valve.VR.Extras;

public class QuitDialog : MonoBehaviour
{
    public SteamVR_LaserPointer leftPointer;
    public SteamVR_LaserPointer rightPointer;
    public SteamVR_Action_Boolean touchPadAction;
    private int tick;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.localScale = new Vector3(0, 0, 0);
        tick = System.Environment.TickCount;

        leftPointer.PointerClick += PointerClick;
        rightPointer.PointerClick += PointerClick;
    }

    // Update is called once per frame
    void Update()
    {
        int curTick = System.Environment.TickCount;
        if (curTick - tick >= 200)
        {
            tick = curTick;

            //QuitDialog 출력 - MoldingScene/ ColoringScene / GalleryScene에서 왼손 입력
            bool touchPadValue = touchPadAction.GetState(SteamVR_Input_Sources.LeftHand);
            if (touchPadValue && UIManager.instance.isUIopen == false)
            {
                if (this.transform.localScale.Equals(new Vector3(0, 0, 0)))
                {
                    showMenu();
                }
                else
                {
                    hideMenu();
                }
            }
        }
    }

    private void PointerClick(object sender, PointerEventArgs e)
    {
        if (e.target.name == "QuitYesBtn")
        {
            //씬 변동 없이 하던 작업 중단

            //QuitDialog 숨기기
            hideMenu();
            //MenuDialog 실행
            MenuDialog.instance.showMenu();

            // 효과음 재생
            EffectManager.instance.Play("button6");
        }
        else if (e.target.name == "QuitNoBtn")
        {
            hideMenu();

            // 효과음 재생
            EffectManager.instance.Play("button6");
        }
    }

    private void showMenu()
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
}
