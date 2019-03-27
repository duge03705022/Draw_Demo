using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public static class RFIBParameter
{
    public static readonly int stageRow = 5;
    public static readonly int stageCol = 9;
    public static readonly int maxHight = 3;

    public static readonly int blockNum = stageRow * stageCol;

    public static readonly int touchRow = 15;
    public static readonly int touchCol = 27;
    public static readonly int notTouchGap = 30;
    public static readonly int maxTouch = 20;
    // 允許甚麼編號被接受
    public static readonly string[] AllowBlockType = {       
        "9999",     // 99 floor
        "7101",     // 71 cut
        "7201",     // 72 cook
        "8001",     // 80 loopBase
        "9001",     // 90 numPad
        "8301"      // 83 loop3
	};

    // RFIB_ID對應的instance_ID
    public static int SearchCard(string idStr)
    {
        switch (idStr)
        {
            case "7101": return 0;      // 71 cut
            case "7201": return 1;      // 72 cook
            case "8001": return 2;      // 80 loopBase
            case "9001": return 3;      // 90 numPad
            case "8301": return 4;      // 83 loop3

            case "0000": return -1;
        }
        return -1;
    }
}
