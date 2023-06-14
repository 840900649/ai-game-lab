using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using UnityEngine;

public class MovePlayer : MonoBehaviour {
    // 设置游戏对象的速度
    public float speed = 10.0f;
    public BoxCollider2D limitBox;       // 移动的边界框


    public Component Effect;
    private float lastUpdatedTime;
    public Vector3 padding = new Vector3(2f, 2f, 0);
    private void Start() {

    }
    void Update() {
        if (!GlobalConfig.AllowMove) {
            return;
        }
        Move();
    }

    void Move() {

        //获取键盘上的输入值（W、A、S、D或方向键）
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        Debug.Log(moveX+":"+ moveY);

        Vector3 position = transform.position;
        AddEffect(position);

        // 计算位置
        GlobalConfig.X = speed * moveX * Time.deltaTime;
        GlobalConfig.Y = speed * moveY * Time.deltaTime;
        position.x += GlobalConfig.X;
        position.y += GlobalConfig.Y;
        var bound = new Bounds(position + padding, transform.localScale);
        if (limitBox) {
            float topBound = limitBox.bounds.size.y / 2f- padding.y;
            float bottomBound = -limitBox.bounds.size.y / 2f + padding.y;
            float leftBound = -limitBox.bounds.size.x / 2f + padding.x;
            float rightBound = +limitBox.bounds.size.x / 2f -padding.x;

            position.x = Mathf.Clamp(position.x, leftBound, rightBound);
            position.y = Mathf.Clamp(position.y, bottomBound, topBound);
            // 应用位置变化

        }
        transform.position = position;
    }

    private void AddEffect(Vector3 point) {
        if (Effect == null) {
            return;
        }
        if (Time.time >= (lastUpdatedTime + 1f)) {
            // 更新计时器
            lastUpdatedTime = Time.time;
        }
        else {
            return;
        }
        var effect = Instantiate(Effect, point, Quaternion.identity);

        StartCoroutine(DestroyObjectAfterDelay(1f, effect));
    }
    IEnumerator DestroyObjectAfterDelay(float time, Component component) {
        // 等待指定时间（time 秒）
        yield return new WaitForSeconds(time);

        // 销毁游戏对象
        Destroy(component.gameObject);
    }
}
