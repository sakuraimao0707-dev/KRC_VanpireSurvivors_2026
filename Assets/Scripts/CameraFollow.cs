using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // ’Ç‚¢‚©‚¯‚é‘ŠŽè
    Vector3 offset;          // ‘ŠŽè‚Æ‚Ì‹——£

    void Start()
    {
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        transform.position = target.position + offset;
    }

}
