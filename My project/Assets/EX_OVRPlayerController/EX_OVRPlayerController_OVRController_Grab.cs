using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EX_OVRPlayerController_OVRController_Grab : MonoBehaviour
{
    public Transform OVRController_R;
    public GameObject Target;

    void Update()
    {
        if (Target && OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
        {
            Target.transform.position = OVRController_R.position + OVRController_R.forward * 0.3f;
            Target.transform.rotation = OVRController_R.rotation;
        }
    }
}
