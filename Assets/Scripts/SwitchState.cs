using System.Collections.Generic;
using UnityEngine;

public class SwitchState : MonoBehaviour {
    Animator animator;
    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        bool w = moveY > 0;
        bool a = moveX < 0;
        bool s = moveY < 0;
        bool d = moveX > 0;

        if (d && s) {//右下移动
            animator.Play("RunDownRight");
            SetDirection();
        }
        else if (d&& w) {
            animator.Play("RunTopRight");
            SetDirection();
        }
        else if (a &&s) {//左下移动
            animator.Play("RunDownRight");
            SetDirection(true);
        }
        else if (a && w) {
            animator.Play("RunTopRight");
            SetDirection(true);
        }
        else if (w) {
            animator.Play("RunTop");
            SetDirection();
        }
        else if (s) {
            animator.Play("RunDown");
            SetDirection();
        }
        else if (a) {
            animator.Play("RunRight");
            SetDirection(true);
        }
        else if (d) {
            animator.Play("RunRight");
            SetDirection();
        }
    }
    void SetDirection(bool rever = false) {
        var x = Mathf.Abs(transform.localScale.x);
        float scaleX = rever ? -x : x;
        // 设置对象的X轴缩放比例为2
        transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
    }
}
