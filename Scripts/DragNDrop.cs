using UnityEngine;

[RequireComponent (typeof(Collider2D))]
public class DragAndDrop2D : MonoBehaviour
{
    private Vector3 _offset;

    void OnMouseDown()
    {
        _offset = transform.position - GetMouseWorldPos();
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + _offset;
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}