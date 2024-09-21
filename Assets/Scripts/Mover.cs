using System.Collections;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 3;

    private Coroutine _movingProcess;

    public void MoveTo(Vector3 targetPosition)
    {
        _movingProcess = StartCoroutine(MovingProcess(targetPosition));
    }

    private IEnumerator MovingProcess(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) != 0)
        {
             transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);        
             yield return null;
        }     
    }

    public void StopMoving()
    {
        StopCoroutine(_movingProcess);
    }
}
