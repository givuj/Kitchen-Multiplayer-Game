using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallBack : MonoBehaviour
{
    //���ؽ���ʱ�ĳ�����ͨ�����ؽ���ֻά��һ��
    private bool isFirstUpdate = true;
    private void Update()
    {
        if(isFirstUpdate)
        {
            isFirstUpdate = false;
            Loader.LoaderCallback();
        }
    }
}
