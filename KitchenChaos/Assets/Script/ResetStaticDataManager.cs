using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStaticDataManager : MonoBehaviour
{
    private void Awake()//awake�������������ʱִ�У�������Ŀ����ʱִ��
    {
        CuttingCounter.ResetStaticData();
        BaseCounter.ResetStaticData();
    }
}
