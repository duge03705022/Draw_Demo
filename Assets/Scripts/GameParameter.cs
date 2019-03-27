using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameParameter : MonoBehaviour
{
    public int drawCol;
    public int drawrow;

    public Dictionary<string, GameObject> unitDic;

    // Start is called before the first frame update
    void Start()
    {
        unitDic = new Dictionary<string, GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
