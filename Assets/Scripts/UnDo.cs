using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnDo : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.Rigist(gameObject);
    }

    private void OnDestroy()
    {
        GameManager.Instance.Remove(gameObject);
    }
}
