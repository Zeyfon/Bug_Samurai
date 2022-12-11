using System.Collections;
using UnityEngine;

namespace Systems.SceneManagement
{
    public class Portal : MonoBehaviour, IInteractable
    {

        enum DestinationIdentifier
        {
            A,B,C,D,E,F,G,H,I,J,K,L,M,N,R,S,O,P,Q,
        }

        [Header("Scene Transition")]
        //[SerializeField] int sceneToLoad=1;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] bool isActivatedByButtonPress;


        [Header("FX")]
        [SerializeField] AudioClip teleportAudio;
        [SerializeField] float teleportVolume = 0.5f;
        [SerializeField] GameObject teleportVFX;
        [SerializeField] Transform teleportVFXOrigin;

        // [Header("Fading Settings")]
        // [SerializeField] float fadeInTime = 2f;
        // [SerializeField] float fadeOutTime = 1f;
        // [SerializeField] float fadeWaitTime = 1f;

        Transform spawnPoint;
        private void Start(){
            spawnPoint = GetComponentInChildren<SpawnPointIdentifier>().transform;
        }

        public delegate void SceneTransition(bool isEnabled);
        public static event SceneTransition OnPortalTriggered;

        private void OnTriggerEnter2D(Collider2D other)
        {
            print("Portal Triggered");

            if (other.CompareTag("Player") && !isActivatedByButtonPress)
            {
                print("Player detected in this portal "+ gameObject.name);
                PlaySFX();
                PlayVFX();
                StartCoroutine(Transition());
            }
        }

        public Transform Interact(){
            print("Player wants to interact with portal");
            PlaySFX();
            PlayVFX();
            return transform;
        }

        void PlaySFX(){
            GetComponent<AudioSource>().PlayOneShot(teleportAudio, teleportVolume);
        }

        void PlayVFX(){
            if(teleportVFX == null || teleportVFXOrigin == null) return;
            CreateVFXGameObject(teleportVFX, teleportVFXOrigin);
        }


    void CreateVFXGameObject(GameObject vfxTemplate, Transform originTransform){
        GameObject vfx = GameObject.Instantiate(vfxTemplate, originTransform.position, originTransform.rotation);
        vfx.transform.localScale = originTransform.localScale;
        StartCoroutine(DestroyObject(vfx));
    }
    IEnumerator DestroyObject(GameObject vfx){
        ParticleSystem particles = vfx.GetComponent<ParticleSystem>();
        while(particles.isPlaying){
            yield return null;
        }
        Destroy(vfx);
    }
        public void PlayerInkoveTransition(){
            StartCoroutine(Transition());
        }

        IEnumerator Transition()
        {
            // if(sceneToLoad < 0)
            // {
            //     Debug.LogError(" Scene to Load not set");
            //     yield break;
            // }
            // DontDestroyOnLoad(gameObject);

            // UIFader fader = FindObjectOfType<UIFader>();
            // print(fader.gameObject.name);
            // if(OnPortalTriggered != null)
            // {
            //     OnPortalTriggered(false);
            // }
            // else
            // {
            //     Debug.LogWarning("Portal cannot disable current Player controller");
            // }
            // yield return fader.FadeOut(fadeOutTime);

            
            //SavingWrapper savingWrapper = GameObject.FindObjectOfType<SavingWrapper>();
            //savingWrapper.Save();

            ////////////////////////////////////////////////////// NEXT SCENE LOADING /////////////////////////////////////////

            //yield return SceneManager.LoadSceneAsync(sceneToLoad);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // if (OnPortalTriggered != null)
            //     OnPortalTriggered(false);
            // else
            //     Debug.LogWarning("Portal cannot disable new Player controller");

            //print("Other Scene Loaded");
            //savingWrapper.Load();
            Portal otherPortal = GetOtherPortal();

            if(otherPortal != null)
            {
                UpdatePlayerPosition(otherPortal);

                //savingWrapper.Save();

                // yield return new WaitForSeconds(fadeWaitTime);

                // yield return fader.FadeIn(fadeInTime);

                // if (OnPortalTriggered != null)
                //     OnPortalTriggered(true);
                // else
                //     Debug.LogWarning("Portal cannot enable new Player controller");
            }
            else{
                Debug.LogWarning("No Portal with needed Identifier was found");
            }
            //else
            //{
            //    Destroy(FindObjectOfType<SavingWrapper>().transform.parent.gameObject, 0.1f);
            //}
            //Destroy(gameObject);
            yield return null;
        }

        Portal GetOtherPortal()
        {
            foreach(Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if(portal.destination != this.destination) continue;
                return portal;
            }
            return null;
        }

        void UpdatePlayerPosition(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            //print(otherPortal.gameObject.name + "  " + otherPortal.spawnPoint.position);
            player.transform.position = otherPortal.spawnPoint.position;
            //TODO 
            //We need to properly set the orientation of the player accordingly to the spawn orientation
            //This must be done within the Player Movemenet Script using the Flip Method
        }
    }
}

