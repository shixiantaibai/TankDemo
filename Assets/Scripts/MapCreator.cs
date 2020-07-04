using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    //初始化地图上的预设体
    //老家，红墙，障碍，出生效果，河流，草，空气墙
    public GameObject[] MapItem;
    //确定某一位置是否存在东西
    private List<Vector3> itemPosition = new List<Vector3>();

    private void Awake()
    {
        //老家的创建
        CreatMap(MapItem[0],new Vector3(0,-8,0),Quaternion.identity);
        //周围红墙的创建
        CreatMap(MapItem[1], new Vector3(-1, -8, 0), Quaternion.identity);
        CreatMap(MapItem[1], new Vector3(1, -8, 0), Quaternion.identity);
        for (int i=-1; i<2; i++) {
            CreatMap(MapItem[1], new Vector3(i, -7, 0), Quaternion.identity);
        }

        for (int i = 0; i <= 35; i++)
        {
            CreatMap(MapItem[1], CreatPositionRandom(), Quaternion.identity);
        }
        //地图空气墙的创建
        for (int i=-11;i<12;i++) {
            CreatMap(MapItem[6], new Vector3(i, 9, 0), Quaternion.identity);
            CreatMap(MapItem[6], new Vector3(i, -9, 0), Quaternion.identity);
        }
        for (int i = -9; i < 10; i++)
        {
            CreatMap(MapItem[6], new Vector3(-11, i, 0), Quaternion.identity);
            CreatMap(MapItem[6], new Vector3(11, i, 0), Quaternion.identity);
        }
        //地图其他资源的创建
        for (int i = 0; i <=20; i++) {
            CreatMap(MapItem[2], CreatPositionRandom(), Quaternion.identity);
            CreatMap(MapItem[4], CreatPositionRandom(), Quaternion.identity);
            CreatMap(MapItem[5], CreatPositionRandom(), Quaternion.identity);
        }
        //玩家的创建
        GameObject player =Instantiate(MapItem[3], new Vector3(-2, -8, 0), Quaternion.identity);
        player.GetComponent<Birth>().creatPlayer = true;
        //敌人的创建
        GameObject Enmey_1 = Instantiate(MapItem[3], new Vector3(-8, 8, 0), Quaternion.identity);
        GameObject Enmey_2 = Instantiate(MapItem[3], new Vector3(8, 8, 0), Quaternion.identity);
        Enmey_1.GetComponent<Birth>().creatPlayer = false;
        Enmey_2.GetComponent<Birth>().creatPlayer = false;
    }

    private void CreatMap(GameObject mapitem,Vector3 mapPosition,Quaternion mapQuaternion) {
        GameObject mapItemObject = Instantiate(mapitem, mapPosition, mapQuaternion);
        mapItemObject.transform.SetParent(gameObject.transform);
        itemPosition.Add(mapPosition);
    }

    //地图产生随机的物体
    private Vector3 CreatPositionRandom() {
        while (true) {
            Vector3 cpr = new Vector3(Random.Range(-9,10),Random.Range(-7,8),0);
            if (!HasItem(cpr)) {
                return cpr;
            }
            
        }
    }
    //判断随机坐标是否存在物体
    private bool HasItem(Vector3 cpr) {
        for (int i=0;i<itemPosition.Count;i++) {
            if (cpr==itemPosition[i]) {
                return true;
            }
        }
        return false;
    }
}
