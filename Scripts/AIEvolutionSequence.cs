using UnityEngine;
using System.Collections;

namespace FreeCityAI
{
    /// <summary>
    /// Contrôle la séquence complète d'évolution de l'IA
    /// comme dans Free Guy - de la boucle contrainte à l'explosion de possibilités
    /// </summary>
    public class AIEvolutionSequence : MonoBehaviour
    {
        [Header("References")]
        public BehaviorTreeCircuit behaviorCircuit;
        public CircuitLight circuitLight;
        public Camera mainCamera;

        [Header("Sequence Settings")]
        public float normalLoopDuration = 5f; // Durée avant l'évolution
        public bool autoTrigger = true;

        [Header("Camera Animation")]
        public bool animateCamera = true;
        public Vector3 cameraStartPosition = new Vector3(0, 0, -10);
        public Vector3 cameraEvolutionPosition = new Vector3(2, 0, -10);
        public float cameraTransitionDuration = 1.5f;

        [Header("Effects")]
        public bool enableScreenShake = true;
        public float shakeIntensity = 0.2f;
        public float shakeDuration = 0.5f;

        [Header("Audio (Optional)")]
        public AudioSource audioSource;
        public AudioClip normalLoopSound;
        public AudioClip evolutionSound;

        private bool hasEvolved = false;
        private float timer = 0f;
        private Vector3 originalCameraPosition;

        void Start()
        {
            Initialize();
        }

        void Initialize()
        {
            if (behaviorCircuit == null)
                behaviorCircuit = GetComponent<BehaviorTreeCircuit>();

            if (circuitLight == null)
                circuitLight = FindObjectOfType<CircuitLight>();

            if (mainCamera == null)
                mainCamera = Camera.main;

            if (mainCamera != null)
            {
                originalCameraPosition = mainCamera.transform.position;
            }

            // Jouer le son de la boucle normale
            if (audioSource != null && normalLoopSound != null)
            {
                audioSource.clip = normalLoopSound;
                audioSource.loop = true;
                audioSource.Play();
            }
        }

        void Update()
        {
            if (hasEvolved) return;

            // Input manuel
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E))
            {
                TriggerEvolution();
            }

            // Auto-trigger
            if (autoTrigger)
            {
                timer += Time.deltaTime;
                if (timer >= normalLoopDuration)
                {
                    TriggerEvolution();
                }
            }
        }

        public void TriggerEvolution()
        {
            if (hasEvolved) return;

            hasEvolved = true;
            StartCoroutine(EvolutionSequence());
        }

        IEnumerator EvolutionSequence()
        {
            Debug.Log("🚀 AI Evolution Triggered!");

            // 1. Ralentir la lumière du circuit
            if (circuitLight != null)
            {
                float originalSpeed = circuitLight.speed;
                float slowdownDuration = 0.5f;
                float elapsed = 0f;

                while (elapsed < slowdownDuration)
                {
                    elapsed += Time.deltaTime;
                    circuitLight.SetSpeed(Mathf.Lerp(originalSpeed, 0f, elapsed / slowdownDuration));
                    yield return null;
                }

                circuitLight.StopMovement();
            }

            // 2. Petit délai dramatique
            yield return new WaitForSeconds(0.3f);

            // 3. Screen shake
            if (enableScreenShake && mainCamera != null)
            {
                StartCoroutine(CameraShake());
            }

            // 4. Déclencher l'explosion des rubans
            if (behaviorCircuit != null)
            {
                behaviorCircuit.TriggerEvolution();
            }

            // 5. Son de l'évolution
            if (audioSource != null)
            {
                audioSource.Stop();
                if (evolutionSound != null)
                {
                    audioSource.PlayOneShot(evolutionSound);
                }
            }

            // 6. Animation de la caméra
            if (animateCamera && mainCamera != null)
            {
                yield return StartCoroutine(AnimateCamera());
            }

            Debug.Log("✨ AI Evolution Complete!");
        }

        IEnumerator CameraShake()
        {
            Vector3 originalPos = mainCamera.transform.position;
            float elapsed = 0f;

            while (elapsed < shakeDuration)
            {
                elapsed += Time.deltaTime;

                float x = Random.Range(-1f, 1f) * shakeIntensity;
                float y = Random.Range(-1f, 1f) * shakeIntensity;

                mainCamera.transform.position = originalPos + new Vector3(x, y, 0);

                yield return null;
            }

            mainCamera.transform.position = originalPos;
        }

        IEnumerator AnimateCamera()
        {
            Vector3 startPos = mainCamera.transform.position;
            Vector3 targetPos = cameraEvolutionPosition;
            float elapsed = 0f;

            while (elapsed < cameraTransitionDuration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / cameraTransitionDuration;

                // Ease out
                t = 1f - Mathf.Pow(1f - t, 3f);

                mainCamera.transform.position = Vector3.Lerp(startPos, targetPos, t);

                yield return null;
            }

            mainCamera.transform.position = targetPos;
        }

        [ContextMenu("Test Evolution Sequence")]
        public void TestEvolution()
        {
            TriggerEvolution();
        }

        [ContextMenu("Reset Sequence")]
        public void ResetSequence()
        {
            hasEvolved = false;
            timer = 0f;

            if (circuitLight != null)
            {
                circuitLight.ResumeMovement();
                circuitLight.SetSpeed(1f);
            }

            if (mainCamera != null)
            {
                mainCamera.transform.position = originalCameraPosition;
            }

            // Régénérer le circuit
            if (behaviorCircuit != null)
            {
                behaviorCircuit.GenerateCircuit();
            }
        }

        void OnGUI()
        {
            // UI simple pour debug
            GUIStyle style = new GUIStyle(GUI.skin.box);
            style.fontSize = 14;
            style.normal.textColor = Color.white;

            GUI.Box(new Rect(10, 10, 300, 80), "", style);

            GUILayout.BeginArea(new Rect(20, 20, 280, 60));

            if (!hasEvolved)
            {
                GUILayout.Label("🤖 AI Status: NORMAL LOOP", style);
                if (autoTrigger)
                {
                    float timeLeft = normalLoopDuration - timer;
                    GUILayout.Label($"Evolution in: {timeLeft:F1}s", style);
                }
                GUILayout.Label("Press SPACE to trigger evolution", style);
            }
            else
            {
                GUILayout.Label("✨ AI Status: EVOLVED", style);
                GUILayout.Label("Press R to reset", style);
            }

            GUILayout.EndArea();

            // Reset avec R
            if (hasEvolved && Input.GetKeyDown(KeyCode.R))
            {
                ResetSequence();
            }
        }
    }
}
