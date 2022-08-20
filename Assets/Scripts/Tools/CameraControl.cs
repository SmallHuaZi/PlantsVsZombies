using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private float MouseOffset;//鼠标偏移量
    private float CameraWidth;

    public Camera _Camera { get; set; }
    public SpriteRenderer _Background { get; set; }
    public float Limit_Left { get; set; }
    public float Limit_Right { get; set; }
    public float ControlSpeed { get; set; }

    private void Start()
    {
        this._Camera = Camera.main;
        this._Background = this.GetComponent<SpriteRenderer>();
        this.CameraWidth = this._Camera.orthographicSize * this._Camera.aspect;//相机宽度的一半
        this.ControlSpeed = 1;
        float CameraWidth = this._Camera.orthographicSize * this._Camera.aspect;
        this.Limit_Left = this._Background.transform.position.x - this._Background.bounds.size.x / 2 + CameraWidth;
        this.Limit_Right = this._Background.transform.position.x + this._Background.bounds.size.x / 2 - CameraWidth;
        this._Camera.transform.position = new Vector3(this.Limit_Left, this._Camera.transform.position.y, -10);

    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MouseOffset -= Input.GetAxis("Mouse X") * ControlSpeed;
            MouseOffset = Mathf.Clamp(MouseOffset, this.Limit_Left, this.Limit_Right);
            _Camera.transform.position = new Vector3(MouseOffset, _Camera.transform.position.y, _Camera.transform.position.z);
        }
    }

}

