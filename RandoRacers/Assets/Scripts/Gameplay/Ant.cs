using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ant : MonoBehaviour {
    private Vector3 _StartPos;
    [SerializeField]
    private Vector3[] _ActionPosition;

    void Awake()
    {
        _StartPos = transform.localPosition;
    }

    public IEnumerator AntMove()
    {
        int i = 0;

        Vector3 newPos = _ActionPosition[i];

        while (Vector3.Distance(transform.localPosition, newPos) > 2)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, newPos, Time.deltaTime);
            if (Vector3.Distance(transform.localPosition, newPos) < 5)
            {
                i++;
                if (i >= _ActionPosition.Length)
                {
                    transform.localPosition = _StartPos;
                    yield break;
                }

                newPos = _ActionPosition[i];
            }
            yield return null;
        }
    }
}
