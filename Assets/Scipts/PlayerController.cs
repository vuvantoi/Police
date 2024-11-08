using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Tốc độ di chuyển
    public float jumpForce = 5f; // Lực nhảy
    public float rotateSpeed = 50f;
    private Rigidbody rb;
    private bool isGrounded;
    public GameObject cuaLeft;
    public GameObject cuaRight;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        // Lấy input từ bàn phím
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Tạo vector di chuyển
        Vector3 movement = new Vector3(0, 0, moveVertical) * moveSpeed * Time.deltaTime;
        Vector3 rotate;

        // Điều chỉnh góc quay dựa vào hướng di chuyển
        if (moveVertical > 0) // Tiến
        {
            rotate = new Vector3(0, moveHorizontal, 0) * rotateSpeed * Time.deltaTime;
        }
        else if (moveVertical < 0) // Lùi
        {
            rotate = new Vector3(0, -moveHorizontal, 0) * rotateSpeed * Time.deltaTime; // Đánh lái ngược lại
        }
        else
        {
            rotate = Vector3.zero; // Không quay nếu không di chuyển
        }

        // Cập nhật vị trí player
        transform.Translate(movement);
        if (moveVertical != 0)
        {
            transform.Rotate(rotate);
        }
    }

    void Jump()
    {
        // Kiểm tra xem player có trên mặt đất không
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; // Đánh dấu là không còn trên mặt đất
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Kiểm tra va chạm với mặt đất
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Đánh dấu là trên mặt đất
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Mo_Cua"))
        {
            StartCoroutine(RotateDoor(cuaLeft.transform, -90f, 1f)); // Mở cửa trái
            StartCoroutine(RotateDoor(cuaRight.transform, 90f, 1f)); // Mở cửa phải
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Mo_Cua"))
        {
            StartCoroutine(RotateDoor(cuaLeft.transform, 90f, 1f)); // Đóng cửa trái
            StartCoroutine(RotateDoor(cuaRight.transform, -90f, 1f)); // Đóng cửa phải
        }
    }

    private IEnumerator RotateDoor(Transform door, float angle, float duration)
    {
        Quaternion startRotation = door.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, angle, 0);
        float time = 0;

        while (time < duration)
        {
            door.rotation = Quaternion.Slerp(startRotation, endRotation, time / duration);
            time += Time.deltaTime;
            yield return null; // Chờ một frame
        }

        door.rotation = endRotation; // Đảm bảo cửa kết thúc ở góc chính xác
    }
}
