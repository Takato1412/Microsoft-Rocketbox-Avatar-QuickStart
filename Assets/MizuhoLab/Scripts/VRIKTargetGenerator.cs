using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static VRIKTargetGenerator;

public class VRIKTargetGenerator : MonoBehaviour
{
    [SerializeField] Transform OVRCameraRig = default;

    public enum HandTrackingType
    {
        OculusTouch, QuestHandTracking
    }
    [SerializeField] HandTrackingType handTrackingType = HandTrackingType.OculusTouch;

    [SerializeField] RootMotion.FinalIK.VRIK vrik = default;

    public enum IKTarget
    {
        Head, LeftHand, RightHand, Pelvis, LeftFoot, RightFoot
    }

    // IK Target Transform
    Transform headTarget = default;
    Transform leftHandTarget = default;
    Transform rightHandTarget = default;
    Transform pelvisTarget = default;
    Transform leftFootTarget = default;
    Transform rightFootTarget = default;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("Generate (Update) Targets in OVRCameraRig")]
    public void GenerateTargets()
    {
        for (int i = 0; i < Enum.GetValues(typeof(IKTarget)).Length; i++)
        {
            IKTarget target = (IKTarget) Enum.ToObject(typeof(IKTarget), i);
            var targetObjectName = GetTargetObjectName(target);
            var targetParentPath = GetTargetParentPath(target);
            var targetTransform = OVRCameraRig.Find(targetParentPath + targetObjectName);
            if (targetTransform == null)
            {
                Debug.Log(targetObjectName + " is generated.");
                targetTransform = new GameObject(targetObjectName).transform;
                targetTransform.SetParent(OVRCameraRig.Find(targetParentPath));
            }
            else
            {
                Debug.Log(targetObjectName + " already exists. Then, the position and rotation are just updated.");
            }
            targetTransform.localPosition = GetTargetLocalPosition(target);
            targetTransform.localEulerAngles = GetTargetLocalEulerAngles(target);
        }
    }

    [ContextMenu("Remove Targets from OVRCameraRig")]
    public void RemoveTargets()
    {
        for (int i = 0; i < Enum.GetValues(typeof(IKTarget)).Length; i++)
        {
            IKTarget target = (IKTarget) Enum.ToObject(typeof(IKTarget), i);
            string targetObjectName = GetTargetObjectName(target);
            string targetParentPath = GetTargetParentPath(target);
            Transform targetTransform = OVRCameraRig.Find(targetParentPath + targetObjectName);
            if (targetTransform == null)
            {
                Debug.LogWarning(targetParentPath + targetObjectName + " is not there and cannot be removed.");
            }
            else
            {
                Debug.Log(targetParentPath + targetObjectName + " is removed.");
                DestroyImmediate(targetTransform.gameObject);
            }
        }
    }

    [ContextMenu("Register Targets to VRIK")]
    public void RegisterTargets()
    {
        IKTarget target;

        // Head
        target = IKTarget.Head;
        headTarget = OVRCameraRig.Find(GetTargetParentPath(target) + GetTargetObjectName(target));
        if (headTarget != null)
        {
            vrik.solver.spine.headTarget = headTarget;
        }
        else
        {
            Debug.LogWarning(GetTargetObjectName(target) + " does not exist and cannot be assigned to VRIK.");
        }
        
        // Pelvis
        target = IKTarget.Pelvis;
        pelvisTarget = OVRCameraRig.Find(GetTargetParentPath(target) + GetTargetObjectName(target));
        if (pelvisTarget != null)
        {
            vrik.solver.spine.pelvisTarget = pelvisTarget;
        }
        else
        {
            Debug.LogWarning(GetTargetObjectName(target) + " does not exist and cannot be assigned to VRIK.");
        }

        // Left Hand
        target = IKTarget.LeftHand;
        leftHandTarget = OVRCameraRig.Find(GetTargetParentPath(target) + GetTargetObjectName(target));
        if (leftHandTarget != null)
        {
            vrik.solver.leftArm.target = leftHandTarget;
        }
        else
        {
            Debug.LogWarning(GetTargetObjectName(target) + " does not exist and cannot be assigned to VRIK.");
        }

        // Right Hand
        target = IKTarget.RightHand;
        rightHandTarget = OVRCameraRig.Find(GetTargetParentPath(target) + GetTargetObjectName(target));
        if (rightHandTarget != null)
        {
            vrik.solver.rightArm.target = rightHandTarget;
        }
        else
        {
            Debug.LogWarning(GetTargetObjectName(target) + " does not exist and cannot be assigned to VRIK.");
        }

        // Left Foot
        target = IKTarget.LeftFoot;
        leftFootTarget = OVRCameraRig.Find(GetTargetParentPath(target) + GetTargetObjectName(target));
        if (leftFootTarget != null)
        {
            vrik.solver.leftLeg.target = leftFootTarget;
        }
        else
        {
            Debug.LogWarning(GetTargetObjectName(target) + " does not exist and cannot be assigned to VRIK.");
        }

        // Right Foot
        target = IKTarget.RightFoot;
        rightFootTarget = OVRCameraRig.Find(GetTargetParentPath(target) + GetTargetObjectName(target));
        if (rightFootTarget != null)
        {
            vrik.solver.rightLeg.target = rightFootTarget;
        }
        else
        {
            Debug.LogWarning(GetTargetObjectName(target) + " does not exist and cannot be assigned to VRIK.");
        }
    }

    /// <summary>
    /// 生成するターゲットの名前を定義します
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    private string GetTargetObjectName(IKTarget target)
    {
        switch (target)
        {
            case IKTarget.Head:
                return "HeadTarget";
            case IKTarget.LeftHand:
                return "LeftHandTarget";
            case IKTarget.RightHand:
                return "RightHandTarget";
            case IKTarget.Pelvis:
                return "PelvisTarget";
            case IKTarget.LeftFoot:
                return "LeftFootTarget";
            case IKTarget.RightFoot:
                return "RightFootTarget";
            default:
                Debug.LogWarning("VRIKTargetGenerator.GetTargetObjectName(): not defined ");
                return "Target";
        }
    }

    /// <summary>
    /// 生成するターゲットのパス（OVRCameraRig以下のどこに生成するか）を定義します
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    private string GetTargetParentPath(IKTarget target)
    {
        switch (target)
        {
            case IKTarget.Head:
                return "TrackingSpace/CenterEyeAnchor/";
            case IKTarget.LeftHand:
                return "TrackingSpace/LeftHandAnchor/";
            case IKTarget.RightHand:
                return "TrackingSpace/RightHandAnchor/";
            case IKTarget.Pelvis:
                return "TrackingSpace/";
            case IKTarget.LeftFoot:
                return "TrackingSpace/";
            case IKTarget.RightFoot:
                return "TrackingSpace/";
            default:
                Debug.LogWarning("VRIKTargetGenerator.GetTargetObjectName(): not defined ");
                return "TrackingSpace/";
        }
    }

    /// <summary>
    /// 生成するターゲットの localPosition を定義します
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    private Vector3 GetTargetLocalPosition(IKTarget target)
    {
        switch (target)
        {
            case IKTarget.Head:
                return new Vector3(0, -0.12f, -0.1f);
            case IKTarget.LeftHand:
                if (handTrackingType == HandTrackingType.OculusTouch)
                {
                    return new Vector3(-0.04f, -0.02f, -0.1f);
                }
                else if (handTrackingType == HandTrackingType.QuestHandTracking)
                {
                    return Vector3.zero;
                }
                else
                {
                    Debug.LogWarning("VRIKTargetGenerator.GetTargetLocalPosition(): Undefined HandTrackingType");
                    return Vector3.zero;
                }
            case IKTarget.RightHand:
                if (handTrackingType == HandTrackingType.OculusTouch)
                {
                    return new Vector3(0.04f, -0.02f, -0.1f);
                }
                else if (handTrackingType == HandTrackingType.QuestHandTracking)
                {
                    return Vector3.zero;
                }
                else
                {
                    Debug.LogWarning("VRIKTargetGenerator.GetTargetLocalPosition(): Undefined HandTrackingType");
                    return Vector3.zero;
                }
            case IKTarget.Pelvis:
                return Vector3.zero;
            case IKTarget.LeftFoot:
                return Vector3.zero;
            case IKTarget.RightFoot:
                return Vector3.zero;
            default:
                Debug.LogWarning("VRIKTargetGenerator.GetTargetLocalPosition(): Undefined IKTarget");
                return Vector3.zero;
        }
    }

    /// <summary>
    /// 生成するターゲットの localEulerAngles を定義します
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    private Vector3 GetTargetLocalEulerAngles(IKTarget target)
    {
        switch (target)
        {
            case IKTarget.Head:
                return new Vector3(0, -90f, -90f);
            case IKTarget.LeftHand:
                if (handTrackingType == HandTrackingType.OculusTouch)
                {
                    return new Vector3(-280f, 180f, 90f);
                }
                else if (handTrackingType == HandTrackingType.QuestHandTracking)
                {
                    return new Vector3(180f, 0f, 180f);
                }
                else
                {
                    Debug.LogWarning("VRIKTargetGenerator.GetTargetLocalEulerAngles(): Undefined HandTrackingType");
                    return Vector3.zero;
                }
            case IKTarget.RightHand:
                if (handTrackingType == HandTrackingType.OculusTouch)
                {
                    return new Vector3(280f, 0f, 90f);
                }
                else if (handTrackingType == HandTrackingType.QuestHandTracking)
                {
                    return new Vector3(180f, 0f, 0f);
                }
                else
                {
                    Debug.LogWarning("VRIKTargetGenerator.GetTargetLocalEulerAngles(): Undefined HandTrackingType");
                    return Vector3.zero;
                }
            case IKTarget.Pelvis:
                return Vector3.zero;
            case IKTarget.LeftFoot:
                return Vector3.zero;
            case IKTarget.RightFoot:
                return Vector3.zero;
            default:
                Debug.LogWarning("VRIKTargetGenerator.GetTargetLocalEulerAngles(): Undefined IKTarget");
                return Vector3.zero;
        }
    }
}