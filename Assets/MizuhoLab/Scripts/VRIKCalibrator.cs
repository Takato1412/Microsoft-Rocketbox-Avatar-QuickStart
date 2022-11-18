using UnityEngine;

[RequireComponent(typeof(VRIKScaler))]
class VRIKCalibrator : MonoBehaviour
{
    [SerializeField]
    float modelHeight = 1.8f;
    [SerializeField, Range(0.5f, 2.5f)]
    float modelEyeHeight = 1.5f;
    [SerializeField]
    float modelPelvisHeight = 0.8f;

    public Transform hmd = default;
    public Transform pelvis = default;

    VRIKScaler scaler;

    void Awake()
    {
        scaler = GetComponent<VRIKScaler>();
    }

    
    public void Calibrate()
    {
        if (scaler == null) { scaler = GetComponent<VRIKScaler>(); }
        scaler.scale = hmd.position.y / modelEyeHeight;
        Debug.Log("calibrate on stand, scale is " + scaler.scale);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position + Vector3.up * (modelEyeHeight / 2f), new Vector3(0.2f, modelEyeHeight, 0.2f));
        Gizmos.DrawWireCube(transform.position + Vector3.up * (modelPelvisHeight / 2f), new Vector3(0.2f, modelPelvisHeight, 0.2f));
        Gizmos.DrawWireCube(transform.position + Vector3.up * (modelHeight / 2f), new Vector3(0.2f, modelHeight, 0.2f));
    }

    public void CalibrateOnSit()
    {
        scaler.scale = (hmd.position.y - pelvis.position.y) / (modelEyeHeight - modelPelvisHeight);
        Debug.Log("calibrate on sit, scale is " + scaler.scale);
    }

    public void CalibrateByHeight(float height)
    {
        scaler.scale = height / modelHeight;
    }
}
