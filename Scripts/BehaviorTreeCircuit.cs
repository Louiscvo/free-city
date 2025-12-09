using UnityEngine;
using System.Collections.Generic;

namespace FreeCityAI
{
    public class BehaviorTreeCircuit : MonoBehaviour
    {
        [Header("Circuit Setup")]
        public string[] nodeNames = new string[]
        {
            "SLEEP",
            "WAKE UP",
            "BANK TELLER",
            "MEET BUDDY",
            "WALK TO WORK",
            "COFFEE BREAK",
            "LUNCH",
            "GO HOME"
        };

        [Header("Layout Settings")]
        public float circuitRadius = 3f;
        public float nodeSpacing = 1f;
        public bool useCircularLayout = false;

        [Header("Node Prefab")]
        public GameObject nodePrefab;

        [Header("References")]
        public List<BehaviorNode> nodes = new List<BehaviorNode>();
        public CircuitLight circuitLight;

        [Header("Explosion Control")]
        public bool hasEvolved = false;
        public KeyCode explosionKey = KeyCode.Space;

        void Start()
        {
            if (nodes.Count == 0)
            {
                GenerateCircuit();
            }

            if (circuitLight != null)
            {
                circuitLight.Initialize(nodes);
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(explosionKey) && !hasEvolved)
            {
                TriggerEvolution();
            }
        }

        [ContextMenu("Generate Circuit")]
        public void GenerateCircuit()
        {
            // Clear existing nodes
            foreach (Transform child in transform)
            {
                DestroyImmediate(child.gameObject);
            }
            nodes.Clear();

            // Create nodes
            for (int i = 0; i < nodeNames.Length; i++)
            {
                Vector3 position = CalculateNodePosition(i);
                GameObject nodeObj = CreateNode(nodeNames[i], i, position);
                BehaviorNode node = nodeObj.GetComponent<BehaviorNode>();
                nodes.Add(node);
            }

            // Connect nodes in a loop
            for (int i = 0; i < nodes.Count; i++)
            {
                int nextIndex = (i + 1) % nodes.Count;
                nodes[i].nextNode = nodes[nextIndex];
            }
        }

        Vector3 CalculateNodePosition(int index)
        {
            if (useCircularLayout)
            {
                // Layout circulaire
                float angle = (index / (float)nodeNames.Length) * 2f * Mathf.PI;
                float x = Mathf.Cos(angle) * circuitRadius;
                float y = Mathf.Sin(angle) * circuitRadius;
                return transform.position + new Vector3(x, y, 0);
            }
            else
            {
                // Layout horizontal avec retour en boucle
                int nodesPerRow = Mathf.CeilToInt(nodeNames.Length / 2f);

                if (index < nodesPerRow)
                {
                    // Ligne du haut (gauche à droite)
                    return transform.position + new Vector3(index * nodeSpacing, 1f, 0);
                }
                else
                {
                    // Ligne du bas (droite à gauche)
                    int reverseIndex = nodeNames.Length - index - 1;
                    return transform.position + new Vector3(reverseIndex * nodeSpacing, -1f, 0);
                }
            }
        }

        GameObject CreateNode(string name, int index, Vector3 position)
        {
            GameObject nodeObj;

            if (nodePrefab != null)
            {
                nodeObj = Instantiate(nodePrefab, position, Quaternion.identity, transform);
            }
            else
            {
                // Créer un nœud simple si pas de prefab
                nodeObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                nodeObj.transform.position = position;
                nodeObj.transform.parent = transform;
                nodeObj.transform.localScale = Vector3.one * 0.5f;
            }

            nodeObj.name = $"Node_{index:00}_{name}";

            // Ajouter ou configurer le composant BehaviorNode
            BehaviorNode behaviorNode = nodeObj.GetComponent<BehaviorNode>();
            if (behaviorNode == null)
            {
                behaviorNode = nodeObj.AddComponent<BehaviorNode>();
            }

            behaviorNode.nodeName = name;
            behaviorNode.nodeIndex = index;

            // Ajouter le composant d'explosion
            if (nodeObj.GetComponent<RibbonExplosion>() == null)
            {
                nodeObj.AddComponent<RibbonExplosion>();
            }

            return nodeObj;
        }

        public void TriggerEvolution()
        {
            hasEvolved = true;

            // Déclencher l'explosion sur tous les nœuds avec un délai progressif
            for (int i = 0; i < nodes.Count; i++)
            {
                int index = i; // Capture pour la closure
                float delay = i * 0.1f; // 0.1s entre chaque nœud
                Invoke(nameof(ExplodeNode) + index, delay);

                // Alternative: utiliser une coroutine
                StartCoroutine(ExplodeNodeDelayed(nodes[index], delay));
            }
        }

        System.Collections.IEnumerator ExplodeNodeDelayed(BehaviorNode node, float delay)
        {
            yield return new WaitForSeconds(delay);
            node.TriggerExplosion();
        }

        [ContextMenu("Test Evolution")]
        public void TestEvolution()
        {
            TriggerEvolution();
        }
    }
}
