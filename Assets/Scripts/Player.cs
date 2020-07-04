using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 3;
    private SpriteRenderer sr;
    private bool isDefended=true;
    private float defendTimeVal=3;
    public Sprite[] tankSprites;
    public GameObject bulletPrefab;
    public GameObject boomPrefab;
    public GameObject defendEffectPrefab;
    private Vector3 bulletEulerAngels;
    //子弹发射的CD
    private float timeVal;

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
        //无敌时间
        if (isDefended) {
            defendEffectPrefab.SetActive(true);
            defendTimeVal -= Time.deltaTime;
            if (defendTimeVal<=0) {
                isDefended = false;
                defendEffectPrefab.SetActive(false);
            }
        }
        //发射子弹的CD
        if (timeVal >= 0.4f) {
            AttackTank();
        } else{
            timeVal += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        MoveTank();
       
    }

    //Tank的移动方法
    private void MoveTank() {
        float h = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (h < 0)
        {
            sr.sprite = tankSprites[3];
            bulletEulerAngels = new Vector3(0,0,90);
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
        float v = Input.GetAxisRaw("Vertical");
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
    private void AttackTank() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Instantiate(bulletPrefab,transform.position, rotation: Quaternion.Euler(transform.eulerAngles+bulletEulerAngels));
            timeVal = 0;
        }
    }

    //Tank的死亡
    private void Die() {
        if (isDefended) {
            return;
        }
        //爆炸特效
        Instantiate(boomPrefab,transform.position,transform.rotation );
        //销毁
        Destroy(gameObject);
    }
}
