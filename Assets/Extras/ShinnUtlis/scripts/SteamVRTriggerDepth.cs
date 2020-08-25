using UnityEngine;
#if SteamVRv15 
using Valve.VR;
#endif

namespace Shinn.VR
{
    public class SteamVRTriggerDepth : MonoBehaviour
    {
#if SteamVRv15
        private SteamVR_TrackedObject trackedObject;
        private SteamVR_Controller.Device device;
#endif
        public Animation HandAnim;
        public string PlayClipName = "Armature|hol";

        private void Start()
        {
#if SteamVRv15
            trackedObject = GetComponent<SteamVR_TrackedObject>();
#endif
        }

        private void Update()
        {
#if SteamVRv15
            device = SteamVR_Controller.Input((int)trackedObject.index);
            float triggerAxis = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).x;
            PlaySignleFrame(triggerAxis);
#endif
        }

        private void PlaySignleFrame(float _frame)
        {
            HandAnim[PlayClipName].time = _frame;
            HandAnim.Play(PlayClipName);
        }
    }
}
