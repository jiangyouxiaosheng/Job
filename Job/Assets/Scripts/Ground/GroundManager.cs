using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;

public class GroundManager : MonoBehaviour
{

    public static GroundManager instance;


    public int groundSpeed;//ƽ̨�ٶ�
    public int moveGroundSpeed;//���ʹ��ٶ�

    //�����
    Queue<GameObject> groundQueue;
    public List<GameObject> groundPrefabList = new List<GameObject>();
    private Transform thisTransform;
    int size = 10;

    //��ʱ��
    float timer = 1f;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
        thisTransform =  transform;
        Init(thisTransform);
    }



    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            float x;
            x = Random.Range(-8, 8);
            PreparedObject(new Vector3(x,transform.position.y,transform.position.z));
          
            timer = 1f;
        }
    }


    #region �ذ����ɶ����
    //����Ԥ������� ��������
    GameObject Copy()
    {
        int index;
        index = Random.Range(0,groundPrefabList.Count);
        var copy = GameObject.Instantiate(groundPrefabList[index], thisTransform);
        copy.SetActive(false);
        return copy;
    }
    //��ʼ������� �����ж������
    public void Init(Transform parent)
    {
        groundQueue = new Queue<GameObject>();
        this.thisTransform = parent;
        
        for (int i = 0; i < size; i++)
        {
            groundQueue.Enqueue(Copy());
        }
    }

    //�ӳ���ȡ�����õĶ���
    GameObject AvailableObject()
    {
        GameObject avaliableObject = null;
        if (groundQueue.Count > 0) //&& !queue.Peek().activeSelf
        {
            avaliableObject = groundQueue.Dequeue();
        }
        else
        {
            avaliableObject = Copy();
        }

        //queue.Enqueue(avaliableObject);
        return avaliableObject;
    }

    //���ÿ��õĶ���
    public GameObject PreparedObject()
    {
        GameObject proparedObject = AvailableObject();

        proparedObject.SetActive(true);

        return proparedObject;
    }
    public GameObject PreparedObject(Vector3 position)
    {
        GameObject proparedObject = AvailableObject();

        proparedObject.SetActive(true);
        proparedObject.transform.position = position;

        return proparedObject;
    }


    //�����ù��Ķ��󷵻ض����(��������д��ͬһ������)
    public void Return(GameObject gameObject)
    {
        groundQueue.Enqueue(gameObject);
    }

    #endregion
}
