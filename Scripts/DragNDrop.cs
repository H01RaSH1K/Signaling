using UnityEngine;

[RequireComponent (typeof(Collider2D))]
public class DragAndDrop2D : MonoBehaviour
{
    private Vector3 _offset;

    private void OnMouseDown()
    {
        _offset = transform.position - GetMouseWorldPos();
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + _offset;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}