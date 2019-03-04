using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace Shinn.VR
{
    public class SteamVRTriggerDepth : MonoBehaviour
    {
        private SteamVR_TrackedObject trackedObject;
        private SteamVR_Controller.Device device;

        public Animation HandAnim;
        public string PlayClipName = "Armature|hol";

        private void Start()
        {
            trackedObject = GetComponent<SteamVR_TrackedObject>();
        }

        private void Update()
        {
            device = SteamVR_Controller.Input((int)trackedObject.index);
            float triggerAxis = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).x;

            PlaySignleFrame(triggerAxis);
        }

        private void PlaySignleFrame(float _frame)
        {
            HandAnim[PlayClipName].time = _frame;
            HandAnim.Play(PlayClipName);
        }
    }
}
