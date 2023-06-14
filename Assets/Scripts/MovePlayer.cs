using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using UnityEngine;

public class MovePlayer : MonoBehaviour {
    // ������Ϸ������ٶ�
    public float speed = 10.0f;
    public BoxCollider2D limitBox;       // �ƶ��ı߽��


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

        //��ȡ�����ϵ�����ֵ��W��A��S��D�������
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        Debug.Log(moveX+":"+ moveY);

        Vector3 position = transform.position;
        AddEffect(position);

        // ����λ��
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
            // Ӧ��λ�ñ仯

        }
        transform.position = position;
    }

    private void AddEffect(Vector3 point) {
        if (Effect == null) {
            return;
        }
        if (Time.time >= (lastUpdatedTime + 1f)) {
            // ���¼�ʱ��
            lastUpdatedTime = Time.time;
        }
        else {
            return;
        }
        var effect = Instantiate(Effect, point, Quaternion.identity);

        StartCoroutine(DestroyObjectAfterDelay(1f, effect));
    }
    IEnumerator DestroyObjectAfterDelay(float time, Component component) {
        // �ȴ�ָ��ʱ�䣨time �룩
        yield return new WaitForSeconds(time);

        // ������Ϸ����
        Destroy(component.gameObject);
    }
}
