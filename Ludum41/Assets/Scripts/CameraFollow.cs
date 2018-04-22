using UnityEngine;

public class CameraFollow : MonoBehaviour
{

  public Transform target;

  public float smoothSpeed = 0.125f;
    public float rotationAngle = 0;
    public float tiltAngle = 30;
  public float distance = 10;
  public float scale = 1;
  public float scrollSpeed = 1;
  public float rotSpeed = 2;
    Player player;

  private void Start()
  {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

  void FixedUpdate()
  {
        Vector3 offset = CalcOffset();
        Vector3 unsmoothedPosition = target.position - offset * Mathf.Pow(player.size, .5f) * distance;
        float range = (unsmoothedPosition - transform.position).sqrMagnitude;
        if (range < .001)
        {
            transform.position = unsmoothedPosition;
        }
        else
        {
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, unsmoothedPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }

        transform.LookAt(target);
    }

    Vector3 CalcOffset()
    {
        Vector3 offset = Quaternion.AngleAxis(rotationAngle, Vector3.up) * Vector3.forward;
        Vector3 cross = Vector3.Cross(offset, Vector3.down);
        Vector3 offset2 = Quaternion.AngleAxis(tiltAngle, cross) * offset;

        return offset2;
    }

  public void Scroll(float _scroll) {
        distance -= _scroll * scrollSpeed;
  }

  public void Rotate(float hmm)
  {
        rotationAngle -= hmm * rotSpeed;
  }
}
