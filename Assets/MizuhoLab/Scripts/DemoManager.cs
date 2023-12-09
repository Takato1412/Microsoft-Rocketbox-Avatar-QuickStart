using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoManager : MonoBehaviour
{
    public GameObject maleAvatar;
    public GameObject femaleAvatar;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SwitchAvatar()
    {
        if (maleAvatar.activeInHierarchy)
        {
            maleAvatar.SetActive(false);
            femaleAvatar.SetActive(true);
        }
        else if (femaleAvatar.activeInHierarchy)
        {
            maleAvatar.SetActive(true);
            femaleAvatar.SetActive(false);
        }
    }

    public void CalibrateAvatar()
    {
        maleAvatar.GetComponent<VRIKScalerCalibrator>().CalibrateByHMD();
        femaleAvatar.GetComponent<VRIKScalerCalibrator>().CalibrateByHMD();
        //GameObject.Find("AvatarCalibrateButton").SetActive(false);
    }
}
