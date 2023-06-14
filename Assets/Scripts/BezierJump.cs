using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class BezierJump : MonoBehaviour {
    public float speed = 5.0f; 
    public float duration = 0.01f; // ��Ծ����ʱ�䡣
    public Component Effect; 
  
    private void Start() {
        
    }
    private void Update() {
     
       
        if (!GlobalConfig.AllowMove) {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            GlobalConfig.AllowMove = false;
            StartJump();
        }
    }

    private void StartJump() {

       
        var endPosition = transform.position + new Vector3(speed*GlobalConfig.X, speed * GlobalConfig.Y, 0); // �յ�λ��Ϊ��ʼλ�ü���Ԥ��ֵ��
        transform.position = endPosition;
        GlobalConfig.AllowMove = true;
       // AddEffect(endPosition);
    } 
     
}
