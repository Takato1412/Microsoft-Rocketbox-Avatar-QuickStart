using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRIKTargetGenerator : MonoBehaviour
{
    public Transform OVRCameraRig = default;

    public RootMotion.FinalIK.VRIK vrik = default;

    private Transform headTarget = default;
    private Transform leftHandTarget = default;
    private Transform rightHandTarget = default;
    private Transform pelvisTarget = default;
    private Transform leftFootTarget = default;
    private Transform rightFootTarget = default;

    string[] targetNameList = { "HeadTarget", "LeftHandTarget", "RightHandTarget", "PelvisTarget", "LeftFootTarget", "RightFootTarget" };
    Dictionary<string, string> targetParentList = new Dictionary<string, string>{
        { "HeadTarget", "TrackingSpace/CenterEyeAnchor/"},
        { "LeftHandTarget", "TrackingSpace/LeftHandAnchor/"},
        { "RightHandTarget", "TrackingSpace/RightHandAnchor/"},
        { "PelvisTarget", "TrackingSpace/"},
        { "LeftFootTarget", "TrackingSpace/"},
        { "RightFootTarget", "TrackingSpace/"},
    };

    //ハンドトラッキングの種類：Oculus touch コントローラ vs Quest Hand Tracking
    public enum HandTrackingType
    {
        OculusTouch, QuestHandTracking
    }
    public HandTrackingType handTrackingType = HandTrackingType.OculusTouch;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("Generate(Update) Targets in OVRCameraRig")]
    public void GenerateTarget()
    {
        Dictionary<string, Vector3> targetLocalPositionList = new Dictionary<string, Vector3>
        {
            { "HeadTarget", new Vector3(0, -0.12f, -0.1f)},
            { "LeftHandTarget", handTrackingType==HandTrackingType.OculusTouch? new Vector3(-0.04f, -0.02f, -0.1f): Vector3.zero },
            { "RightHandTarget", handTrackingType==HandTrackingType.OculusTouch? new Vector3(0.04f, -0.02f, -0.1f): Vector3.zero },
            { "PelvisTarget", new Vector3(0,0,0)},
            { "LeftFootTarget", new Vector3(0,0,0)},
            { "RightFootTarget", new Vector3(0,0,0)},
        };
        Dictionary<string, Vector3> targetLocalEulerAnglesList = new Dictionary<string, Vector3>
        {
            { "HeadTarget", new Vector3(0, -90f, -90f)},
            { "LeftHandTarget", handTrackingType==HandTrackingType.OculusTouch? new Vector3(-280f, 180f, 90f): new Vector3(180f, 0f, 180f)},
            { "RightHandTarget", handTrackingType==HandTrackingType.OculusTouch? new Vector3(280f, 0f, 90f): new Vector3(180f, 0f, 0f)},
            { "PelvisTarget", new Vector3(0,0,0)},
            { "LeftFootTarget", new Vector3(0,0,0)},
            { "RightFootTarget", new Vector3(0,0,0)},
        };

        for (int i = 0; i < targetNameList.Length; i++)
        {
            var targetName = targetNameList[i];
            var targetParent = targetParentList[targetName];
            var target = OVRCameraRig.Find(targetParent + targetName);
            if (target == null)
            {
                target = new GameObject(targetName).transform;
                target.SetParent(OVRCameraRig.Find(targetParent));
            }
            else
            {
                Debug.Log(targetName + " already exists.");
            }
            target.localPosition = targetLocalPositionList[targetName];
            target.localEulerAngles = targetLocalEulerAnglesList[targetName];
        }


    }

    [ContextMenu("Destroy Targets from OVRCameraRig")]
    public void DeleteTargets()
    {
        for (int i = 0; i < targetNameList.Length; i++)
        {
            var targetName = targetNameList[i];
            var targetParent = targetParentList[targetName];
            var target = OVRCameraRig.Find(targetParent + targetName);
            if (target == null)
            {
                Debug.Log(targetParent + targetName + " is not there");
            }
            else
            {
                DestroyImmediate(target.gameObject);
            }
        }
    }

    public void GetTargets()
    {
        var targetName = "HeadTarget";
        headTarget = OVRCameraRig.Find(targetParentList[targetName] + targetName);
        targetName = "LeftHandTarget";
        leftHandTarget = OVRCameraRig.Find(targetParentList[targetName] + targetName);
        targetName = "RightHandTarget";
        rightHandTarget = OVRCameraRig.Find(targetParentList[targetName] + targetName);
        targetName = "PelvisTarget";
        pelvisTarget = OVRCameraRig.Find(targetParentList[targetName] + targetName);
        targetName = "LeftFootTarget";
        leftFootTarget = OVRCameraRig.Find(targetParentList[targetName] + targetName);
        targetName = "RightFootTarget";
        rightFootTarget = OVRCameraRig.Find(targetParentList[targetName] + targetName);

    }

    [ContextMenu("Register Targets to VRIK")]
    public void RegisterTargets()
    {
        //targetのtransformを取得
        GetTargets();

        //targetをvrikに登録
        vrik.solver.spine.headTarget = headTarget;
        vrik.solver.spine.pelvisTarget = pelvisTarget;
        vrik.solver.leftArm.target = leftHandTarget;
        vrik.solver.rightArm.target = rightHandTarget;
        vrik.solver.leftLeg.target = leftFootTarget;
        vrik.solver.rightLeg.target = rightFootTarget;

        //重みを設定
        vrik.solver.spine.pelvisPositionWeight = 0f;
    }
}
