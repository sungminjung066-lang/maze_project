using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EX_OVRPlayerController_OVRController_Teleport : MonoBehaviour
{
    public GameObject OVRPlayerController;
    public Transform OVRController_L;

    Vector3 TeleportPoint;
    bool isTeleporting;

    LineRenderer LineRenderer;
    public Material RayMatDef, RayMatHit;
    float lineRendererAdjust = 0.03f;
    public int rayLength = 100; // ray�� �����ϴ� �Ÿ�

    void Start()
    {
        LineRenderer = GetComponent<LineRenderer>();
        if (LineRenderer == null)
        {
            LineRenderer = gameObject.AddComponent<LineRenderer>();
        }
        LineRenderer.material = RayMatDef;
        LineRenderer.startWidth = 0.01f;
        LineRenderer.endWidth = 0.01f;
    }

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(OVRController_L.position, OVRController_L.forward, out hit, rayLength))
        {
            LineRenderer.material = RayMatHit;
            LineRenderer.SetPosition(0, OVRController_L.transform.position + OVRController_L.transform.forward * lineRendererAdjust);
            LineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            LineRenderer.material = RayMatDef;
            LineRenderer.SetPosition(0, OVRController_L.transform.position + OVRController_L.transform.forward * lineRendererAdjust);
            LineRenderer.SetPosition(1, OVRController_L.transform.position + OVRController_L.transform.forward * rayLength);
        }

        if (hit.collider != null && OVRInput.Get(OVRInput.RawButton.LIndexTrigger) && hit.collider.tag == "Teleportable")
        {
            //var player = OVRPlayerController.GetComponentInChildren<OVRPlayerController>();
            //player.enabled = false;
            //OVRPlayerController.transform.position = hit.collider.transform.position + Vector3.up * 3f;
            //player.enabled = true;
            isTeleporting = true;
            TeleportPoint = hit.point + Vector3.up * 2f;
        }

        if (isTeleporting)
        {
            var player = OVRPlayerController.GetComponentInChildren<OVRPlayerController>();
            player.enabled = false;
            OVRPlayerController.transform.position = Vector3.Lerp(OVRPlayerController.transform.position, TeleportPoint, 0.1f);
            player.enabled = true;

            float dist = Vector3.Distance(OVRPlayerController.transform.position, TeleportPoint);
            if (dist < 0.1f)
            {
                isTeleporting = false;
            }
        }
    }
}
