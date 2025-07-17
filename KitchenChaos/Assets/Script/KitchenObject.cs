using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�൱�ڸ�Ԥ�����װһЩ����
public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;//ͨ��������ʳ������ֺ;���
    private ClearCounter clearCounter;//��ʳ�����ĸ���̨��ͬʱҲ��Ϊʳ��任��̨��׼��
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }
    public void SetClearCounter(ClearCounter clearCounter)
    {
        if (this.clearCounter != null)//�����̨֮ǰ��ʳ��,֮ǰ��̨��ʳ�����ݻ���
        {
            Debug.Log("1");
            this.clearCounter.ClearKitchenObject();
        }
        this.clearCounter = clearCounter;
        if(clearCounter.HasKitchenObject())//����¹�̨����ʳ��Ļ����ܷ���
        {
            Debug.LogError("�Ѿ���ʳ����");
        }
        clearCounter.SetKitchenObject(this);
        transform.parent = clearCounter.GetKitchenObjectFollowTransform();//��δ��뻹�ᵼ��λ�õĸı�
        transform.localPosition = Vector3.zero;
    }
    public ClearCounter GetClearCounter()
    {
        return clearCounter;
    }
}
