using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�൱�ڸ�Ԥ�����װһЩ����
public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;//ͨ��������ʳ������ֺ;���
    private IKitchenObjectParant kitchenObjectParant;//��ʳ�����ĸ���̨��ͬʱҲ��Ϊʳ��任��̨��׼��
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    //����ʳ���������
    public void SetKitchenObjectParant(IKitchenObjectParant kitchenObjectParant)//��̬ʵ�ָ�����ʵ�ǽӿڶ���ֻ��ClearCounter��playerʵ����IKitchenObjectParant�ӿ�
    {
        if (this.kitchenObjectParant != null)//�����̨֮ǰ��ʳ��,֮ǰ��̨��ʳ�����ݻ���
        {
            this.kitchenObjectParant.ClearKitchenObject();
        }
        this.kitchenObjectParant = kitchenObjectParant;
        if(kitchenObjectParant.HasKitchenObject())//����¹�̨����ʳ��Ļ����ܷ���
        {
            Debug.LogError("�Ѿ���ʳ����");
        }
        else
        {
            kitchenObjectParant.SetKitchenObject(this);
            transform.parent = kitchenObjectParant.GetKitchenObjectFollowTransform();//��δ��뻹�ᵼ��λ�õĸı�
            transform.localPosition = Vector3.zero;
        }
    }
    public IKitchenObjectParant GetKitchenObjectParant()
    {
        return kitchenObjectParant;
    }
    public void DestorySelf()
    {
        kitchenObjectParant.ClearKitchenObject();
        Destroy(gameObject);

    }

    public bool TryGetPlate(out PlateKichenObject plateKichenObject)
    {
        if (this is PlateKichenObject)
        {
            plateKichenObject = this as PlateKichenObject;
            return true;
        }
        else
        {
            plateKichenObject = null;
            return false;
        }

         
    }

    //ת��ʳ��,��һ��������ʳ��ڶ���������counter����player
    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSo,IKitchenObjectParant kitchenObjectParant)
    {
        Transform KitchenObjectTransform = Instantiate(kitchenObjectSo.prefab);
        KitchenObject kitchenObject = KitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParant(kitchenObjectParant);
        return kitchenObject;
    }
}
