using UnityEngine;

public class Linker : MonoBehaviour
{
    [SerializeField]
    Lane primary;

    public Lane Primary
    {
        get
        {
            return primary;
        }
    }

    [SerializeField]
    private Lane secondary;

    public Lane Secondary
    {
        get
        {
            return secondary;
        }
    }

    [SerializeField]
    private Transform linkingPoint;

    [SerializeField]
    public Transform road;

    public void Link(Linker son)
    {
        primary.Next = son.Secondary;
        son.Primary.Next = secondary;

        son.road.transform.position = linkingPoint.position + (son.road.position - son.linkingPoint.position);
    }

    public void OnMouseUp()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            Linker father = hit.transform.GetComponent<Linker>();
            if(father != null && father != this)
            {
                father.Link(this);
            }
        }
    }

}
