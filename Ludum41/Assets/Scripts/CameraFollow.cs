using UnityEngine;

public class CameraFollow : MonoBehaviour
{

  public Transform target;

  public float smoothSpeed = 0.125f;
  public Vector3 offset;
  public float scale = 1;
  public float scrollSpeed;

  private void Start()
  {
    offset = target.position - transform.position;
  }

  void FixedUpdate()
  {
    Vector3 smoothedPosition = Vector3.Lerp(transform.position, target.position - offset, smoothSpeed);
    transform.position = new Vector3(smoothedPosition.x, target.transform.localScale.y + 10, smoothedPosition.z);
    //transform.RotateAround(target.position, Vector3.up, 1);
    transform.LookAt(target);
  }

  public Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
  {
    Vector3 dir = point - pivot; // get point direction relative to pivot
    dir = Quaternion.Euler(angles) * dir; // rotate it
    point = dir + pivot; // calculate rotated point
    return point; // return it
  }

  public void Scroll(float _scroll) {
    offset += transform.forward * _scroll * scrollSpeed;
  }

  public void Rotate(float hmm)
  {

    Vector3 newOffset = RotatePointAroundPivot(transform.position, target.position, new Vector3(0,hmm*5,0));
    offset = target.position - newOffset;
    //transform.LookAt(target);
  }
}
