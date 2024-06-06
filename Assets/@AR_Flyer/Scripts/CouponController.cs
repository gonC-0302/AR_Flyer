using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CouponController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayRotateJump());
    }

    private IEnumerator PlayRotateJump()
    {
        while(true)
        {
            gameObject.transform.DOLocalRotate(new Vector3(0, 540 , 0),0.5f,RotateMode.FastBeyond360).SetEase(Ease.OutCirc);
            gameObject.transform.DOLocalJump(gameObject.transform.localPosition, 0.05f, 1,1f);
            yield return new WaitForSeconds(5f);
        }
    }
}
