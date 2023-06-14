using UnityEngine;

public class CameraController : MonoBehaviour {
    // Start is called before the first frame update
    public Transform target;

    public BoxCollider2D limitBox;       // ������ӷ�Χ�ı߽��

    private Vector3 velocity = Vector3.zero;
    private Camera mainCamera;

    // �� Start() ��ʼ������ͷ�ҵ������
    void Start() {
        mainCamera = Camera.main;
    }

    // ʹ�� LateUpdate() ת��Ϊȷ������λ���Ѹ��º��ٸ��������λ��
    void LateUpdate() {
        if (target) {
            // �������Ӧ���ƶ����µ�λ��
            Vector3 targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);
            Vector3 newPos = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, 0.1f);

            // ������Ŀ��ӷ�Χ���Ƶ�����֮��
            if (limitBox) {
                float vertExtent = mainCamera.orthographicSize;
                float horzExtent = vertExtent * mainCamera.aspect;

                float leftBound = limitBox.bounds.min.x + horzExtent;
                float rightBound = limitBox.bounds.max.x - horzExtent;
                float bottomBound = limitBox.bounds.min.y + vertExtent;
                float topBound = limitBox.bounds.max.y - vertExtent;

                newPos.x = Mathf.Clamp(newPos.x, leftBound, rightBound);
                newPos.y = Mathf.Clamp(newPos.y, bottomBound, topBound);
            }

            // ����λ��Ӧ�������
            transform.position = newPos;
        }
    }
}
