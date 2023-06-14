using UnityEngine;

public class CameraController : MonoBehaviour {
    // Start is called before the first frame update
    public Transform target;

    public BoxCollider2D limitBox;       // 相机可视范围的边界框

    private Vector3 velocity = Vector3.zero;
    private Camera mainCamera;

    // 在 Start() 初始化摄像头找到主相机
    void Start() {
        mainCamera = Camera.main;
    }

    // 使用 LateUpdate() 转换为确保对象位置已更新后再更新相机的位置
    void LateUpdate() {
        if (target) {
            // 计算相机应该移动的新的位置
            Vector3 targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);
            Vector3 newPos = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, 0.1f);

            // 将相机的可视范围限制到矩形之内
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

            // 将新位置应用于相机
            transform.position = newPos;
        }
    }
}
