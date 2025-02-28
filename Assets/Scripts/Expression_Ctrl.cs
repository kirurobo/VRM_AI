﻿using TMPro;
using UnityEngine;
using UniVRM10;
using System.Linq;

public class Expression_Ctrl : MonoBehaviour
{
    //このスクリプトをVRMBlinker.csやuLipSync等のスクリプトを共存させるには"\Assets\VRM10\Runtime\IO\Vrm10Importer.cs"のExpression.OverrideBlink（以下略）を全部書き換える必要がある。
    public Vrm10Instance Controller;
    public string Emote;
    public float count;
    public float emote_Weight;
    public CallVoice CallVoice;
    public TextMeshProUGUI Emo;
    private void Awake()
    {
        if (Controller == null)
        {
            Controller = GetComponent<Vrm10Instance>();
        }
        CallVoice = this.gameObject.GetComponent<CallVoice>();

        GameObject Emo_UI = GameObject.Find("Emo_UI");
        Emo = Emo_UI.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (UImanager.talking)
        {
            Emote = EditorRunTerminal.Emo;
            emote_Weight = EditorRunTerminal.Emo_Weight;
            Emocon();
            Countup();
            var emo_ws = emote_Weight.ToString();
            Emo.text = Emote + ":" + emo_ws;
        }
        else
        {
            Emo.text = null;
            emote_Weight = 0;
            Emocon();
            Countdown();
        }
        string[] emolist = new string[] { "Happy", "Angry", "Sad", "Relaxed", "Surprised" };

        if (!emolist.Contains(Emote))
        {
            Emo.text = null;
        }
    }

    public void Emocon()
    {
        // 連想配列を使って、Emoteの文字列に対応するExpressionPresetを取得するといいと言われたが実装方法がわからんので後回し
        if (Emote == "Relaxed")
        {
            Controller.Runtime.Expression.SetWeight(ExpressionKey.CreateFromPreset(ExpressionPreset.relaxed), count);
        }
        if (Emote == "Happy")
        {
            Controller.Runtime.Expression.SetWeight(ExpressionKey.CreateFromPreset(ExpressionPreset.happy), count);
        }
        if (Emote == "Angry")
        {
            Controller.Runtime.Expression.SetWeight(ExpressionKey.CreateFromPreset(ExpressionPreset.angry), count);
        }
        if (Emote == "Sad")
        {
            Controller.Runtime.Expression.SetWeight(ExpressionKey.CreateFromPreset(ExpressionPreset.sad), count);
        }
        if (Emote == "Surprised")
        {
            Controller.Runtime.Expression.SetWeight(ExpressionKey.CreateFromPreset(ExpressionPreset.surprised), count);
        }
    }
    void Countup()
    {
        if (count < emote_Weight)
        {
            count += 0.05f;
        }
    }
    void Countdown()
    {
        if (count > emote_Weight)
        {
            count -= 0.05f;
            if (count == 0)
            {
                Emote = null;
            }
        }
    }
}




