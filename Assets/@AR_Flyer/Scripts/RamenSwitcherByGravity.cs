using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RamenSwitcherByGravity : MonoBehaviour
{
    [SerializeField] private GameObject _ramenHorizon, _ramenVerticle;
    [SerializeField] private Transform _lightTran;
    // Start is called before the first frame update
    void Start()
    {
        if (GravitySensor.current != null)
        {
            InputSystem.EnableDevice(GravitySensor.current);
        }
        CheckGravity();
    }

    private void CheckGravity()
    {
        var gravitySensor = GravitySensor.current;
        if (gravitySensor != null)
        {
            Vector3 gravity = gravitySensor.gravity.ReadValue();
            // 水平
            if (gravity.y >= -0.8f)
            {
                _ramenHorizon.SetActive(true);
                _ramenVerticle.SetActive(false);
                _lightTran.localRotation = Quaternion.Euler(90, 0, 0);
                    }
            // 垂直
            else
            {
                _ramenHorizon.SetActive(false);
                _ramenVerticle.SetActive(true);
                _lightTran.localRotation = Quaternion.Euler(140, 0, 0);

            }
        }
        // 垂直
        else
        {
            _ramenHorizon.SetActive(false);
            _ramenVerticle.SetActive(true);
            _lightTran.localRotation = Quaternion.Euler(140, 0, 0);

        }
    }
}
