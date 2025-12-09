using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FreeCityAI
{
    /// <summary>
    /// Script utilitaire pour setup automatique de la scène Free City AI
    /// </summary>
    public class FreeCityAISetup : MonoBehaviour
    {
#if UNITY_EDITOR
        [MenuItem("GameObject/Free City AI/Setup Complete Scene", false, 10)]
        public static void SetupCompleteScene()
        {
            // 1. Créer le circuit principal
            GameObject circuitObj = new GameObject("BehaviorCircuit");
            circuitObj.transform.position = Vector3.zero;

            BehaviorTreeCircuit circuit = circuitObj.AddComponent<BehaviorTreeCircuit>();
            circuit.nodeSpacing = 1.5f;
            circuit.circuitRadius = 3f;
            circuit.useCircularLayout = false;

            // 2. Créer la lumière du circuit
            GameObject lightObj = new GameObject("CircuitLight");
            lightObj.transform.position = Vector3.zero;

            CircuitLight circuitLight = lightObj.AddComponent<CircuitLight>();
            circuitLight.lightColor = new Color(0f, 1f, 1f, 1f); // Cyan
            circuitLight.speed = 1f;
            circuitLight.pauseAtNodes = true;
            circuitLight.pauseDuration = 0.3f;

            // 3. Ajouter le contrôleur de séquence
            AIEvolutionSequence sequence = circuitObj.AddComponent<AIEvolutionSequence>();
            sequence.behaviorCircuit = circuit;
            sequence.circuitLight = circuitLight;
            sequence.mainCamera = Camera.main;
            sequence.normalLoopDuration = 5f;
            sequence.autoTrigger = true;

            // 4. Générer le circuit
            circuit.GenerateCircuit();

            // 5. Initialiser la lumière avec les nœuds
            circuitLight.Initialize(circuit.nodes);

            // 6. Configurer la caméra si possible
            if (Camera.main != null)
            {
                Camera.main.transform.position = new Vector3(0, 0, -10);
                Camera.main.orthographic = true;
                Camera.main.orthographicSize = 6;
                Camera.main.backgroundColor = new Color(0.1f, 0.1f, 0.15f);
            }

            // 7. Sélectionner le circuit dans la hiérarchie
            Selection.activeGameObject = circuitObj;

            Debug.Log("✅ Free City AI Scene setup complete!");
            Debug.Log("Press PLAY and then SPACE to trigger the AI evolution!");
        }

        [MenuItem("GameObject/Free City AI/Add Behavior Circuit Only", false, 11)]
        public static void CreateBehaviorCircuit()
        {
            GameObject circuitObj = new GameObject("BehaviorCircuit");
            circuitObj.transform.position = Vector3.zero;

            BehaviorTreeCircuit circuit = circuitObj.AddComponent<BehaviorTreeCircuit>();
            circuit.nodeSpacing = 1.5f;

            Selection.activeGameObject = circuitObj;

            Debug.Log("✅ Behavior Circuit created. Right-click on BehaviorTreeCircuit component and select 'Generate Circuit'");
        }

        [MenuItem("GameObject/Free City AI/Add Circuit Light", false, 12)]
        public static void CreateCircuitLight()
        {
            GameObject lightObj = new GameObject("CircuitLight");
            lightObj.transform.position = Vector3.zero;

            CircuitLight circuitLight = lightObj.AddComponent<CircuitLight>();
            circuitLight.lightColor = new Color(0f, 1f, 1f, 1f);

            Selection.activeGameObject = lightObj;

            Debug.Log("✅ Circuit Light created. Assign the nodes list in the Inspector.");
        }
#endif

        [ContextMenu("Setup Scene (Runtime)")]
        public void SetupSceneRuntime()
        {
            Debug.LogWarning("Scene setup should be done in Edit Mode. Use the GameObject menu: GameObject → Free City AI → Setup Complete Scene");
        }
    }

#if UNITY_EDITOR
    /// <summary>
    /// Custom Inspector pour faciliter le setup
    /// </summary>
    [CustomEditor(typeof(BehaviorTreeCircuit))]
    public class BehaviorTreeCircuitEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            BehaviorTreeCircuit circuit = (BehaviorTreeCircuit)target;

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Quick Actions", EditorStyles.boldLabel);

            if (GUILayout.Button("🔄 Generate Circuit", GUILayout.Height(30)))
            {
                circuit.GenerateCircuit();
                EditorUtility.SetDirty(circuit);
            }

            if (GUILayout.Button("✨ Test Evolution", GUILayout.Height(30)))
            {
                if (Application.isPlaying)
                {
                    circuit.TriggerEvolution();
                }
                else
                {
                    Debug.LogWarning("Enter Play Mode to test evolution");
                }
            }

            EditorGUILayout.Space();
            EditorGUILayout.HelpBox(
                "1. Click 'Generate Circuit' to create nodes\n" +
                "2. Enter Play Mode\n" +
                "3. Press SPACE or wait for auto-trigger",
                MessageType.Info
            );
        }
    }

    [CustomEditor(typeof(AIEvolutionSequence))]
    public class AIEvolutionSequenceEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            AIEvolutionSequence sequence = (AIEvolutionSequence)target;

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Playback Controls", EditorStyles.boldLabel);

            if (!Application.isPlaying)
            {
                EditorGUILayout.HelpBox("Enter Play Mode to use playback controls", MessageType.Info);
                return;
            }

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("▶️ Trigger Evolution", GUILayout.Height(30)))
            {
                sequence.TriggerEvolution();
            }

            if (GUILayout.Button("🔄 Reset", GUILayout.Height(30)))
            {
                sequence.ResetSequence();
            }

            EditorGUILayout.EndHorizontal();
        }
    }
#endif
    }
}
