using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 3;
    private float v ,h;
    private SpriteRenderer sr;
    public Sprite[] tankSprites;
    public GameObject bulletPrefab;
    public GameObject boomPrefab;
    private Vector3 bulletEulerAngels;
    //子弹发射的CD
    private float timeVal;
    //敌人定时朝向
    private float timeValDirection=4;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //发射子弹的间隔
        if (timeVal >= 3)
        {
            AttackTank();
        }
        else
        {
            timeVal += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        MoveTank();

    }

    //Tank的移动方法
    private void MoveTank()
    {
        if (timeValDirection >= 4)
        {
            int num = Random.Range(0, 8);
            if (num > 5)
            {
                v = -1;
                h = 0;
            }
            else if (num == 0)
            {
                v = 1;
                h = 0;
            }
            else if (num > 0 && num < 3)
            {
                v = 0;
                h = -1;
            }
            else if (num >= 3 && num < 5)
            {
                v = 0;
                h = 1;
            }
            timeValDirection = 0;
        }
        else {
            timeValDirection += Time.fixedDeltaTime;
        }
        
        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (h < 0)
        {
            sr.sprite = tankSprites[3];
            bulletEulerAngels = new Vector3(0, 0, 90);
        }
        else if (h > 0)
        {
            sr.sprite = tankSprites[1];
            bulletEulerAngels = new Vector3(0, 0, -90);
        }
        if (h != 0)
        {
            return;
        }
        
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (v < 0)
        {
            sr.sprite = tankSprites[2];
            bulletEulerAngels = new Vector3(0, 0, 180);
        }
        else if (v > 0)
        {
            sr.sprite = tankSprites[0];
            bulletEulerAngels = new Vector3(0, 0, 0);
        }
    }

    //Tank的攻击方法
    private void AttackTank()
    {

        Instantiate(bulletPrefab, transform.position, rotation: Quaternion.Euler(transform.eulerAngles + bulletEulerAngels));
        timeVal = 0;
       
    }

    //Tank的死亡
    private void Die()
    {
        //爆炸特效
        Instantiate(boomPrefab, transform.position, transform.rotation);
        //销毁
        Destroy(gameObject);
    }
}
