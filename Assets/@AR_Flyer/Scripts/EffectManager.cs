using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.XR.ARFoundation;
using UnityEngine.InputSystem;

public class EffectManager : MonoBehaviour
{
    [SerializeField] private Material _dissolveMat;
    [SerializeField] private Material _glitchMat;

    private Vector3 _targetVec = new Vector3(0,0.5f,0);
    [SerializeField] private ARTrackedImageManager _aRTrackedImageManager;
    [SerializeField] private Text sensorLabel;

    void Start()
    {
        _aRTrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
        _glitchMat.SetFloat("Vector1_2fa229cb86ea44b9a09c3c44e372c1f9", 100);
        _dissolveMat.SetVector("_DissolveOffest", Vector3.zero);
        if (GravitySensor.current != null)
        {
            InputSystem.EnableDevice(GravitySensor.current);
        }

    }

    private void Update()
    {
        var gravitySensor = GravitySensor.current;
        if (gravitySensor != null)
        {
            var enabled = gravitySensor.enabled;
            Vector3 gravity = gravitySensor.gravity.ReadValue();
            sensorLabel.text = $"enabled: {enabled}\n" +
                $"Gravity\nX={gravity.x:#0.00} Y={gravity.y:#0.00} Z={gravity.z:#0.00}";
        }
    }

    private void PlayDissolveEffect()
    {
        DOVirtual.Vector3(
  from: Vector3.zero,//Tween開始時の値
  to: _targetVec,//終了時の値
  duration: 10f,//Tween時間
                //値が変わった時の処理
  onVirtualUpdate: (tweenValue) => {
      //Debug.Log($"値が変化 : {tweenValue}");
      _dissolveMat.SetVector("_DissolveOffest", tweenValue);
    
  }
);
    }

    private void PlayGlitchEffect()
    {

        _glitchMat.SetFloat("Vector1_2fa229cb86ea44b9a09c3c44e372c1f9", 0);
        DOVirtual.Float(
  from: 100,//Tween開始時の値
  to: 0,//終了時の値
  duration: 5,//Tween時間
              //値が変わった時の処理
  onVirtualUpdate: (tweenValue) => {
      //Debug.Log($"値が変化 : {tweenValue}");
      _glitchMat.SetFloat("Vector1_2fa229cb86ea44b9a09c3c44e372c1f9", tweenValue);
  }
);
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        if(eventArgs.added.Count > 0)
        {
            PlayDissolveEffect();
            PlayGlitchEffect();
            Debug.Log("検出成功");
        }
    }

   
}
