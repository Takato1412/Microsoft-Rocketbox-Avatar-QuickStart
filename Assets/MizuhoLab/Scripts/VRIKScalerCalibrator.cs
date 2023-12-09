using UnityEngine;

[RequireComponent(typeof(VRIKScaler))]
public class VRIKScalerCalibrator : MonoBehaviour
{
    // Model
    [SerializeField, Range(0.0f, 3.0f)]
    float modelHeight = 1.8f;
    [SerializeField, Range(0.0f, 3.0f)]
    float modelEyeHeight = 1.5f;
    [SerializeField, Range(0.0f, 3.0f)]
    float modelPelvisHeight = 0.8f;

    // User
    [SerializeField, Range(0.0f, 3.0f)]
    public float userHeight = 1.8f;

    // HMD
    [SerializeField] Transform hmd = default;
    [SerializeField] Transform pelvis = default;

    // Scaler
    VRIKScaler scaler;

    // Mode
    public enum CalibrateMode
    {
        UseHeight, UseHMD
    }
    [SerializeField] public CalibrateMode mode = CalibrateMode.UseHeight;

    void Awake()
    {
        scaler = GetComponent<VRIKScaler>();
    }

    
    public void Calibrate()
    {
        if (scaler == null) { scaler = GetComponent<VRIKScaler>(); }

        if (scaler == null)
        { 
            Debug.LogError("Cannot calibrate. Please add VRIK Scaler.");
            return;
        }

        switch (mode)
        {
            case CalibrateMode.UseHeight:
                scaler.scale = userHeight / modelHeight;
                break;
            case CalibrateMode.UseHMD:
                scaler.scale = hmd.position.y / modelEyeHeight;
                break;
            default:
                break;
        }

        Debug.Log("VRIK Calibrate. Scale is " + scaler.scale);
    }

    public void Reset()
    {
        if (scaler == null) { scaler = GetComponent<VRIKScaler>(); }

        if (scaler == null)
        {
            Debug.LogError("Cannot calibrate. Please add VRIK Scaler.");
            return;
        }

        scaler.scale = 1f;
    }

    /// <summary>
    /// Scene ビューに Gismo を表示します
    /// </summary>
    void OnDrawGizmosSelected()
    {
        switch (mode)
        {
            case CalibrateMode.UseHeight:
                Gizmos.DrawWireCube(transform.position + Vector3.up * (modelHeight / 2f), new Vector3(0.2f, modelHeight, 0.2f));
                break;
            case CalibrateMode.UseHMD:
                Gizmos.DrawWireCube(transform.position + Vector3.up * (modelEyeHeight / 2f), new Vector3(0.2f, modelEyeHeight, 0.2f));
                break;
            default:
                break;
        }
        //Gizmos.DrawWireCube(transform.position + Vector3.up * (modelPelvisHeight / 2f), new Vector3(0.2f, modelPelvisHeight, 0.2f));
    }

    public void CalibrateByHeight()
    {
        mode = CalibrateMode.UseHeight;
        Calibrate();
    }

    public void CalibrateByHMD()
    {
        mode = CalibrateMode.UseHMD;
        Calibrate();
    }
}
