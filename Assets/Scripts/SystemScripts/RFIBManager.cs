using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

// 編號規則:
// 系統編號 此欄空白 方塊種類 編號+上下 方向

public class RFIBManager : MonoBehaviour
{
    RFIBricks_Cores RFIB;
    public GameParameter gameParameter;

    public Dictionary<string, bool> tagSensing;

    #region RFIB parameter
    readonly short[] EnableAntenna = {1, 2, 3, 4};       // reader port
    readonly string ReaderIP = "192.168.1.96";           // 到時再說
    readonly double ReaderPower = 32, Sensitive = -70;   // 功率, 敏感度
    readonly bool Flag_ToConnectTheReade = false;        // false就不會連reader

    readonly bool showSysMesg = true;
    readonly bool showReceiveTag = true;
    readonly bool showDebugMesg = true;

    readonly string sysTagBased = "8940 0000";           // 允許的系統編號

    readonly int refreshTime = 600;                      // clear beffer
    readonly int disappearTime = 400;                    // id 消失多久才會的消失
    readonly int delayForReceivingTime = 200;            // 清空之後停多久才收id

    #endregion

    void Start()
    {
        #region Set RFIB Parameter
        RFIB = new RFIBricks_Cores(ReaderIP, ReaderPower, Sensitive, EnableAntenna, Flag_ToConnectTheReade);
        RFIB.setShowSysMesg(showSysMesg);
        RFIB.setShowReceiveTag(showReceiveTag);
        RFIB.setShowDebugMesg(showDebugMesg);

        RFIB.setSysTagBased(sysTagBased);
        RFIB.setAllowBlockType(RFIBParameter.AllowBlockType);

        RFIB.setRefreshTime(refreshTime);
        RFIB.setDisappearTime(disappearTime);
        RFIB.setDelayForReceivingTime(delayForReceivingTime);

        // 開始接收ID前要將地板配對
        BoardMapping();

        RFIB.startReceive();
        RFIB.startToBuild();
        RFIB.printNoiseIDs();

        #endregion

        tagSensing = new Dictionary<string, bool>();

        foreach (var dic in gameParameter.unitDic)
        {
            tagSensing.Add(dic.Key, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        RFIB.statesUpdate();
        SenseID();
        KeyPressed();
    }

    // 在開始接收ID前，這邊要將接收到的地板ID進行配對編號。
    private void BoardMapping()
    {
        //  [04]   | 0004 0104  ..   ..   ..   ..   ..  0704 0804
        //  [03]   | 0003 0103  ..   ..   ..   ..   ..  0703 0803
        //  [02]   | 0002 0102  ..   ..   ..   ..   ..  0702 0802
        //  [01]   | 0001 0101  ..   ..   ..   ..   ..  0701 0801
        //  [00]   | 0000 0100  ..   ..   ..   ..   ..  0700 0800
        //-------／-----------------------------------------------
        //   y ／x | [00] [01] [02] [03] [04] [05] [06] [07] [08] 

        for (int i = 0; i < RFIBParameter.blockNum; i++)
        {
            string pos = "0" + (i % RFIBParameter.stageCol).ToString() + "0" + (i / RFIBParameter.stageCol).ToString();
            RFIB.setBoardBlockMappingArray(i, pos);
        }
    }

    public void SenseID()
    {
        foreach (var dic in gameParameter.unitDic)
        {
            if (RFIB.IfContainTag(dic.Key))
            {
                tagSensing[dic.Key] = true;
            }
            else
            {
                tagSensing[dic.Key] = false;
            }
        }
    }

    public void KeyPressed()
    {
        if (Input.GetKeyUp("1"))
            ChangeTestTag("8940 0000 2222 0002 0001");
        if (Input.GetKeyUp("2"))
            ChangeTestTag("8940 0000 2222 0102 0001");
        if (Input.GetKeyUp("3"))
            ChangeTestTag("8940 0000 2222 0202 0001");
        if (Input.GetKeyUp("4"))
            ChangeTestTag("8940 0000 2222 0302 0001");
        if (Input.GetKeyUp("5"))
            ChangeTestTag("8940 0000 2222 0402 0001");
        if (Input.GetKeyUp("6"))
            ChangeTestTag("8940 0000 2222 0502 0001");
        if (Input.GetKeyUp("7"))
            ChangeTestTag("8940 0000 2222 0602 0001");
        if (Input.GetKeyUp("8"))
            ChangeTestTag("8940 0000 2222 0702 0001");
        if (Input.GetKeyUp("9"))
            ChangeTestTag("8940 0000 2222 0802 0001");
        if (Input.GetKeyUp("q"))
            ChangeTestTag("8940 0000 2222 0001 0001");
        if (Input.GetKeyUp("w"))
            ChangeTestTag("8940 0000 2222 0101 0001");
        if (Input.GetKeyUp("e"))
            ChangeTestTag("8940 0000 2222 0201 0001");
        if (Input.GetKeyUp("r"))
            ChangeTestTag("8940 0000 2222 0301 0001");
        if (Input.GetKeyUp("t"))
            ChangeTestTag("8940 0000 2222 0401 0001");
        if (Input.GetKeyUp("y"))
            ChangeTestTag("8940 0000 2222 0501 0001");
        if (Input.GetKeyUp("u"))
            ChangeTestTag("8940 0000 2222 0601 0001");
        if (Input.GetKeyUp("i"))
            ChangeTestTag("8940 0000 2222 0701 0001");
        if (Input.GetKeyUp("o"))
            ChangeTestTag("8940 0000 2222 0801 0001");
        if (Input.GetKeyUp("a"))
            ChangeTestTag("8940 0000 2222 0000 0001");
        if (Input.GetKeyUp("s"))
            ChangeTestTag("8940 0000 2222 0100 0001");
        if (Input.GetKeyUp("d"))
            ChangeTestTag("8940 0000 2222 0200 0001");
        if (Input.GetKeyUp("f"))
            ChangeTestTag("8940 0000 2222 0300 0001");
        if (Input.GetKeyUp("g"))
            ChangeTestTag("8940 0000 2222 0400 0001");
        if (Input.GetKeyUp("h"))
            ChangeTestTag("8940 0000 2222 0500 0001");
        if (Input.GetKeyUp("j"))
            ChangeTestTag("8940 0000 2222 0600 0001");
        if (Input.GetKeyUp("k"))
            ChangeTestTag("8940 0000 2222 0700 0001");
        if (Input.GetKeyUp("l"))
            ChangeTestTag("8940 0000 2222 0800 0001");


        if (Input.GetKeyDown("1"))
            ChangeTestTag("8940 0000 2222 0002 0001");
        if (Input.GetKeyDown("2"))
            ChangeTestTag("8940 0000 2222 0102 0001");
        if (Input.GetKeyDown("3"))
            ChangeTestTag("8940 0000 2222 0202 0001");
        if (Input.GetKeyDown("4"))
            ChangeTestTag("8940 0000 2222 0302 0001");
        if (Input.GetKeyDown("5"))
            ChangeTestTag("8940 0000 2222 0402 0001");
        if (Input.GetKeyDown("6"))
            ChangeTestTag("8940 0000 2222 0502 0001");
        if (Input.GetKeyDown("7"))
            ChangeTestTag("8940 0000 2222 0602 0001");
        if (Input.GetKeyDown("8"))
            ChangeTestTag("8940 0000 2222 0702 0001");
        if (Input.GetKeyDown("9"))
            ChangeTestTag("8940 0000 2222 0802 0001");
        if (Input.GetKeyDown("q"))
            ChangeTestTag("8940 0000 2222 0001 0001");
        if (Input.GetKeyDown("w"))
            ChangeTestTag("8940 0000 2222 0101 0001");
        if (Input.GetKeyDown("e"))
            ChangeTestTag("8940 0000 2222 0201 0001");
        if (Input.GetKeyDown("r"))
            ChangeTestTag("8940 0000 2222 0301 0001");
        if (Input.GetKeyDown("t"))
            ChangeTestTag("8940 0000 2222 0401 0001");
        if (Input.GetKeyDown("y"))
            ChangeTestTag("8940 0000 2222 0501 0001");
        if (Input.GetKeyDown("u"))
            ChangeTestTag("8940 0000 2222 0601 0001");
        if (Input.GetKeyDown("i"))
            ChangeTestTag("8940 0000 2222 0701 0001");
        if (Input.GetKeyDown("o"))
            ChangeTestTag("8940 0000 2222 0801 0001");
        if (Input.GetKeyDown("a"))
            ChangeTestTag("8940 0000 2222 0000 0001");
        if (Input.GetKeyDown("s"))
            ChangeTestTag("8940 0000 2222 0100 0001");
        if (Input.GetKeyDown("d"))
            ChangeTestTag("8940 0000 2222 0200 0001");
        if (Input.GetKeyDown("f"))
            ChangeTestTag("8940 0000 2222 0300 0001");
        if (Input.GetKeyDown("g"))
            ChangeTestTag("8940 0000 2222 0400 0001");
        if (Input.GetKeyDown("h"))
            ChangeTestTag("8940 0000 2222 0500 0001");
        if (Input.GetKeyDown("j"))
            ChangeTestTag("8940 0000 2222 0600 0001");
        if (Input.GetKeyDown("k"))
            ChangeTestTag("8940 0000 2222 0700 0001");
        if (Input.GetKeyDown("l"))
            ChangeTestTag("8940 0000 2222 0800 0001");

        #region Information
        if (Input.GetKeyUp(";"))
        {
            string[] tags = RFIB.GetTags();
            for (int i = 0; i < tags.Length; i++)
                Debug.Log(tags[i]);
        }
        if (Input.GetKeyUp("."))
        {
            RFIB.printAllReceivedIDs();
            RFIB.printNoiseIDs();
        }

        #endregion
    }

    public void ChangeTestTag(string tag)
    {
        if (!RFIB.IfContainTag(tag))
            RFIB._Testing_AddHoldingTag(tag);
        else
            RFIB._Testing_RemoveHoldingTag(tag);
    }
}
