using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStaticDataManager : MonoBehaviour
{
    private void Awake()//awake是这个场景启动时执行，不是项目启动时执行
    {
        CuttingCounter.ResetStaticData();
        BaseCounter.ResetStaticData();
    }
}
