using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    private SpriteRenderer sr;
    public Sprite brokenSprite;
    public GameObject boomPrefab;
    // Start is called before the first frame update
    void Start()
    {
        sr= GetComponent<SpriteRenderer>();
    }

    public void Die() {
        //爆炸特效
        sr.sprite = brokenSprite;
        Instantiate(boomPrefab, transform.position, transform.rotation);
    }
}
