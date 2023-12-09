using RootMotion.FinalIK;
using UnityEngine;

[RequireComponent(typeof(VRIK))]
public class VRIKScaler : MonoBehaviour
{
    [SerializeField, Range(0.1f, 5f)]
    public float scale = 1f;

    VRIK vrik;

    Vector3 initialScale;
    IKSolverVR.Locomotion initialLoco;

    void Awake()
    {
        vrik = GetComponent<VRIK>();
        initialScale = transform.localScale;

        initialLoco = new IKSolverVR.Locomotion();
        var vrikLoco = vrik.solver.locomotion;

        initialLoco.footDistance = vrikLoco.footDistance;
        initialLoco.stepThreshold = vrikLoco.stepThreshold;
        initialLoco.maxVelocity = vrikLoco.maxVelocity;
        initialLoco.stepHeight = new AnimationCurve(vrikLoco.stepHeight.keys);
        initialLoco.heelHeight = new AnimationCurve(vrikLoco.heelHeight.keys);
    }

    void Update()
    {
        transform.localScale = initialScale * scale;

        var vrikLoco = vrik.solver.locomotion;

        vrikLoco.footDistance = initialLoco.footDistance * scale;
        vrikLoco.stepThreshold = initialLoco.stepThreshold * scale;
        vrikLoco.maxVelocity = initialLoco.maxVelocity * scale;

        for (var i = 0; i < vrikLoco.stepHeight.keys.Length; i++)
        {
            var newKey = initialLoco.stepHeight.keys[i];
            newKey.value *= scale;
            vrikLoco.stepHeight.MoveKey(i, newKey);
        }

        for (var i = 0; i < vrikLoco.heelHeight.keys.Length; i++)
        {
            var newKey = initialLoco.heelHeight.keys[i];
            newKey.value *= scale;
            vrikLoco.heelHeight.MoveKey(i, newKey);
        }
    }
}