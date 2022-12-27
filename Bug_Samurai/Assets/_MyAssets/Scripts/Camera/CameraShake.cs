using Cinemachine;
//using PSmash.Combat;
using System.Collections;
using UnityEngine;

namespace PSmash.Core
{
    public class CameraShake : MonoBehaviour
    {
        [SerializeField] float shakeTime = 1;
        [SerializeField] float maxAmplitud = 2;


        private void OnEnable() {
            PlayerCombat.OnSheatAttackDeliverDamage += PlayCameraShake;    
        }
        public void PlayCameraShake()
        {
            print("Shake Camera");
            StartCoroutine(CameraShakeCO());
        }

        IEnumerator CameraShakeCO()
        {
            //print("Splash Effects On");
            CinemachineVirtualCamera cam = GetComponent<CinemachineVirtualCamera>();
            CinemachineBasicMultiChannelPerlin noise = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            noise.m_AmplitudeGain = maxAmplitud;

            yield return new WaitForSeconds(0.25f);
            float timer = shakeTime;
            while (noise.m_AmplitudeGain > 0)
            {
                float deltaGain = maxAmplitud*Time.deltaTime/shakeTime;
                if(noise.m_AmplitudeGain - deltaGain <0) noise.m_AmplitudeGain = 0;
                else noise.m_AmplitudeGain -= deltaGain;
                timer -= Time.deltaTime;
                print(timer);
                yield return new WaitForEndOfFrame();
            }
        }
    }

}
