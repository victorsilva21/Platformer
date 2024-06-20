using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    private float _timerTotalValue = 1;
    private float _timer = 1;
    private CinemachineVirtualCamera _cinemachine;
    // Start is called before the first frame update
    void Start()
    {
        _cinemachine = gameObject.GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer <= 0)
        {
            _cinemachine.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset = new Vector3(0, -2, 0);
        }
        else
        {
            _cinemachine.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset = new Vector3(0, 1.5f, 0);
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            _timer -= Time.deltaTime;
        }
        else
        {
            _timer = _timerTotalValue;
        }
    }
}
