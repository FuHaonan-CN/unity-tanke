using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // 属性值
    public float moveSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 沿着自身方向移动
        transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Tank":
                break;
            case "Home":
                break;
            case "Enemy":
                break;
            case "Wall":
                break;
            case "Barrier":
                break;
        }
    }
}
