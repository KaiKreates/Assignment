using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAnimation : MonoBehaviour
{
    private void Start()
    {
        transform.DORotate(new Vector3(0, 0, -360), 2).SetLoops(-1, LoopType.Restart).SetRelative();
    }
}
