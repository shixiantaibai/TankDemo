using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Birth : MonoBehaviour
{
    public GameObject playerPrefab;
    public bool creatPlayer;
    public GameObject[] enemyPrefabList;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("TankBirth",1f);
        Destroy(gameObject,1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TankBirth() {
        if (creatPlayer)
        {
            Instantiate(playerPrefab, transform.position, Quaternion.identity);
        }
        else {
            int num = Random.Range(0,2);
            Instantiate(enemyPrefabList[num], transform.position, Quaternion.identity);
        }
        
    }
}
