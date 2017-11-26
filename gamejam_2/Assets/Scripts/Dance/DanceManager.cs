﻿using System;
using System.Collections;
 using System.Collections.Generic;
 using System.Linq;
using DefaultNamespace;
 using DefaultNamespace.Dance.Animations;
 using UnityEngine;
 using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DanceManager : MonoBehaviour
{
    public Text timeText;
    public ComboCounter comboCounterItem;
    public AnimationContainer _animations;
    private BattleProgress bp;
    private int comboCounter = 0;
    private float _startTime;
    private float roundTime = 60.0f;

    public static DanceManager Instance;

    private RectTransform mainCanvas;
    public RectTransform MainCanvas
    {
        get
        {
            if (mainCanvas == null)
            {
                mainCanvas = GameObject.Find("MainCanvas").GetRectTransform();
            }
            return mainCanvas;
        }
    }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _startTime = Time.time;
        bp = FindObjectOfType<BattleProgress>();
        bp.SetDiff(0);
        comboCounterItem.SetProgress(0);
        StartCoroutine(UpdateTime());
        LoadLevel(1);
    }

    private IEnumerator UpdateTime()
    {
        var timePassed = 0;
        while ((timePassed = (int)Mathf.Floor(Time.time - _startTime)) <= roundTime)
        {
            timeText.text = String.Format("0 : {0:00}", (roundTime - timePassed)).ToString();
            yield return null;
        }
        timeText.text = "0 : 00";
        StartCoroutine(FinishLevel());
    }

    private IEnumerator FinishLevel()
    {
        yield return new WaitForSeconds(1);
        GameObject go;
        if (bp.diff > 0)
        {
            var o = Resources.Load<GameObject>("Prefabs/Win");
            go = GameObject.Instantiate(o) as GameObject;
            go.transform.SetParent(MainCanvas);
            go.transform.localPosition = Vector3.zero;
            go.GetRectTransform().anchoredPosition = Vector2.zero;
            go.transform.localScale = Vector3.one;
        }
        else
        {
            var o = Resources.Load<GameObject>("Prefabs/Lose");
            go = GameObject.Instantiate(o) as GameObject;
            go.transform.SetParent(MainCanvas);
            go.transform.localPosition = Vector3.zero;
            go.GetRectTransform().anchoredPosition = Vector2.zero;
            go.transform.localScale = Vector3.one;
        }
        var text = go.transform.Find("restart").GetComponent<Text>();
        text.text = "restart in 3...";
        yield return new WaitForSeconds(1);
        text.text = "restart in 2..";
        yield return new WaitForSeconds(1);
        text.text = "restart in 1";
        yield return new WaitForSeconds(1);

        var infos = GameObject.FindObjectsOfType<GamePlayer>();
        foreach (var i in infos)
        {
            if (i.isLocalPlayer && i.isServer)
                LobbyManager.Instance.ServerChangeScene(LobbyManager.Instance.lobbyScene);
        }
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

    private Dictionary<ActionSuccessGrade, string> texts = new Dictionary<ActionSuccessGrade, string>
    {
        {ActionSuccessGrade.Perfect, "IMPRESSIVE!!!"},
        {ActionSuccessGrade.Good, "SUPER!"},
        {ActionSuccessGrade.Ok, "NICE"},
        {ActionSuccessGrade.Fail, "OOPS!"},
    };

    public void ProcessAction(ActionResult result, Vector2 pos)
    {
        FlyingText rft = FlyingText.Spawn(pos);
        var text = result.successGrade.ToString() + "!";
        if (texts.ContainsKey(result.successGrade))
            text = texts[result.successGrade];
        rft.SetText(text);

        var delay = 0.15f;
        if (result.points > 0)
        {
            FlyingText pft = FlyingText.Spawn(pos);
            pft.Delay(delay);
            delay += 0.15f;
            pft.SetText("+" + result.points);
        }

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
            result.points = (int)(result.points * 2f) + (comboCounter - 3) * 20;
            FlyingText ft = FlyingText.Spawn(pos, delay);
            if (comboCounter > 3)
                ft.SetText("COMBO x" + (comboCounter - 2) + "!!");
            else
                ft.SetText("COMBO!");
        }

        var infos = GameObject.FindObjectsOfType<GamePlayer>();
        foreach (var i in infos)
        {
            i.ChangeScore(result.points);
        }
//        if (GamePlayer.Instance != null)
//        {
//             GamePlayer.Instance.DeltaPoints(result.points);
//        }
//       
        if (result.successGrade != ActionSuccessGrade.Fail)
        {
            _animations.MySuccessAnimation();
        }
    }

    public void OnMiss()
    {
        ProcessAction(new ActionResult
        {
            points = 0,
            successGrade = ActionSuccessGrade.Fail
        },
        Utils.ConvertInputPos(Input.mousePosition));
        _animations.MyFailAnimation();
    }

    public void SetProgress(int progressDiff)
    {
        bp.SetDiff(progressDiff);
    }

    public void Update()
    {
        var infos = GameObject.FindObjectsOfType<GamePlayer>();
        var myInfo = infos.FirstOrDefault(i => i.isLocalPlayer);
        var otherInfo = infos.FirstOrDefault(i => !i.isLocalPlayer);
        if (bp != null)
        {
            var myScore = myInfo == null ? 0 : myInfo.score;
            var enemyScore = otherInfo == null ? 0 : otherInfo.score;
            bp.SetDiff(myScore - enemyScore);
            bp.myPoints.text = myScore.ToString();
            bp.enemyPoints.text = enemyScore.ToString();
        }
        _animations.UpdateRandom();
    }
}
