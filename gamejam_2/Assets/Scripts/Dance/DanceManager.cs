﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DanceManager : MonoBehaviour
{
    public Text timeText;
    public ComboCounter comboCounterItem;
    private int _totalPoints;
    private BattleProgress bp;
    private int comboCounter = 0;
    private float _startTime;

    public static DanceManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _startTime = Time.time;
        bp = FindObjectOfType<BattleProgress>();
        bp.SetDiff(0);
        _totalPoints = 0;
        comboCounterItem.SetProgress(0);
        StartCoroutine(UpdateTime());
        LoadLevel(1);
	}

    private IEnumerator UpdateTime()
    {
        var timePassed = 0;
        while (timePassed <= 30.0f)
        {
            timePassed = (int)Mathf.Floor(Time.time - _startTime);
            timeText.text = String.Format("0:{0:00}", (30 - timePassed)).ToString();
            yield return null;
        }
        timeText.text = "0:00";
    }

    private void LoadLevel(int level)
    {
        TextAsset levelAsset = Resources.Load<TextAsset>("Levels/" + level);
        var data = JsonUtility.FromJson<LevelData>(levelAsset.text);
        var go = new GameObject();
        go.AddComponent<BubbleGenerator>().Init(data.bubbles);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("dance");
    }

    public void ProcessAction(ActionResult result, Vector2 pos)
    {
        FlyingText rft = FlyingText.Spawn(pos);
        rft.SetText(result.successGrade.ToString() + "!");

        FlyingText pft = FlyingText.Spawn(pos);
        pft.Delay(0.15f);
        pft.SetText("+" + result.points);

        if (result.successGrade < ActionSuccessGrade.Good)
        {
            comboCounter = 0;
        }
        else
        {
            comboCounter++;
        }
        comboCounterItem.SetProgress(comboCounter / 3.0f);

        if (comboCounter >= 3)
        {
            result.points = (int)(result.points * 1.2f) + (comboCounter - 3) * 2;
            FlyingText ft = FlyingText.Spawn(pos, 0.3f);
            if (comboCounter > 3)
                ft.SetText("COMBO x" + (comboCounter - 2) + "!!");
            else
                ft.SetText("COMBO!");
        }

        _totalPoints += result.points;

        bp.SetDiff(_totalPoints);
    }

    public void OnMiss()
    {
        Debug.LogError(Input.mousePosition);
        ProcessAction(new ActionResult
        {
            points = 0,
            successGrade = ActionSuccessGrade.Fail
        },
        ConvertInputPos());
    }

    Vector2 ConvertInputPos()
    {
        return new Vector2(
            Input.mousePosition.x * (1280.0f / Screen.width),
            Input.mousePosition.y * (800.0f / Screen.height));
    }
}
