using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour
{
    public RFIBManager rFIBManager;
    public GameParameter gameParameter;

    public GameObject unitParent;
    public GameObject unitPrefab;
    public string tagPrefix;
    public string tagSufix;

    private GameObject[,] units;

    // Start is called before the first frame update
    void Start()
    {
        units = new GameObject[gameParameter.drawCol, gameParameter.drawrow];

        for (int i = 0; i < gameParameter.drawCol; i++)
        {
            for (int j = 0; j < gameParameter.drawrow; j++)
            {
                units[i, j] = Instantiate(unitPrefab, unitParent.transform);
                units[i, j].transform.localPosition = new Vector3(i - gameParameter.drawCol / 2, j - gameParameter.drawrow / 2, 0);
                units[i, j].transform.localScale = new Vector3(0.95f, 0.95f, 0.95f);
                gameParameter.unitDic.Add(tagPrefix + i.ToString().PadLeft(2, '0') + j.ToString().PadLeft(2, '0') + tagSufix, units[i, j]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        SenceID();
    }

    public void SenceID()
    {
        foreach (var dic in gameParameter.unitDic)
        {
            if (rFIBManager.tagSensing[dic.Key])
            {
                dic.Value.GetComponent<Unit>().SetColor(new Color(0f, 0f, 0f));
            }
            else
            {
                dic.Value.GetComponent<Unit>().SetColor(new Color(1f, 1f, 1f));
            }
        }
    }
}
