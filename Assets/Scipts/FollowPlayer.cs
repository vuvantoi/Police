using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // Tham chiếu đến ô tô
    public Vector3 offset; // Vị trí cách biệt giữa camera và ô tô
    public float followSpeed = 5f; // Tốc độ theo dõi

    // Start is called before the first frame update
    void Start()
    {
        // Thiết lập offset để camera ở phía sau và trên ô tô
        offset = new Vector3(0, 20, -40); // Điều chỉnh vị trí này nếu cần
    }

    // LateUpdate được gọi sau Update
    void LateUpdate()
    {
        Follow();
    }

    void Follow()
    {
        // Tính toán vị trí mới của camera
        Vector3 targetPosition = player.position + player.TransformDirection(offset);

        // Cập nhật vị trí camera với tốc độ mượt mà
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Làm cho camera quay theo hướng ô tô
        transform.rotation = Quaternion.Lerp(transform.rotation, player.rotation, followSpeed * Time.deltaTime);
    }
}
