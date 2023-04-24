using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private const float MaxZoom = 100;

    private new Transform transform;
    private new Camera camera;

    private bool ActiveMove;
    private Vector3 MoveVector;

    private void Start()
    {
        transform = gameObject.transform;
        camera = Camera.main;
    }

    private void Update()
    {
        if (true == ActiveMove)
        {
            transform.position += MoveVector * camera.orthographicSize * Time.deltaTime;
        }
    }

    private void OnMove(InputValue inputValue)
    {
        Vector2 input = inputValue.Get<Vector2>();

        ActiveMove = input != Vector2.zero; //false if no input

        MoveVector = input;
    }

    private void OnZoom(InputValue inputValue)
    {
        float scrollValue = -Mathf.Sign(inputValue.Get<Vector2>().y);

        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize + scrollValue, 1f, MaxZoom);
    }
}
