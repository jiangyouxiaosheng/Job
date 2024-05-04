using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;

public class GroundManager : MonoBehaviour
{

    public static GroundManager instance;


    public int groundSpeed;//平台速度
    public int moveGroundSpeed;//传送带速度

    //对象池
    Queue<GameObject> groundQueue;
    public List<GameObject> groundPrefabList = new List<GameObject>();
    private Transform thisTransform;
    int size = 10;

    //计时器
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


    #region 地板生成对象池
    //复制预制体对象 加入对象池
    GameObject Copy()
    {
        int index;
        index = Random.Range(0,groundPrefabList.Count);
        var copy = GameObject.Instantiate(groundPrefabList[index], thisTransform);
        copy.SetActive(false);
        return copy;
    }
    //初始化对象池 将所有对象入队
    public void Init(Transform parent)
    {
        groundQueue = new Queue<GameObject>();
        this.thisTransform = parent;
        
        for (int i = 0; i < size; i++)
        {
            groundQueue.Enqueue(Copy());
        }
    }

    //从池中取出可用的对象
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

    //启用可用的对象
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


    //让已用过的对象返回对象池(可与启用写在同一函数中)
    public void Return(GameObject gameObject)
    {
        groundQueue.Enqueue(gameObject);
    }

    #endregion
}
