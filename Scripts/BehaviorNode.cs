using UnityEngine;

namespace FreeCityAI
{
    public class BehaviorNode : MonoBehaviour
    {
        [Header("Node Settings")]
        public string nodeName = "ACTION";
        public int nodeIndex = 0;

        [Header("Visual Settings")]
        public Color nodeColor = new Color(0.2f, 0.6f, 0.8f, 1f);
        public float nodeSize = 0.5f;

        [Header("Connection")]
        public BehaviorNode nextNode;
        public LineRenderer connectionLine;

        [Header("Explosion Settings")]
        public bool canExplode = true;
        public Color[] ribbonColors = new Color[]
        {
            new Color(1f, 0.4f, 0.7f, 1f),    // Rose
            new Color(1f, 0.9f, 0.2f, 1f),    // Jaune
            new Color(0.3f, 0.7f, 1f, 1f),    // Bleu
            new Color(1f, 0.5f, 0.2f, 1f),    // Orange
            new Color(0.7f, 0.3f, 1f, 1f)     // Violet
        };

        private MeshRenderer meshRenderer;
        private Material nodeMaterial;

        void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                nodeMaterial = meshRenderer.material;
                nodeMaterial.color = nodeColor;
            }
        }

        void Start()
        {
            SetupConnectionLine();
        }

        void SetupConnectionLine()
        {
            if (nextNode != null && connectionLine == null)
            {
                GameObject lineObj = new GameObject($"Line_{nodeName}_to_{nextNode.nodeName}");
                lineObj.transform.parent = transform;

                connectionLine = lineObj.AddComponent<LineRenderer>();
                connectionLine.startWidth = 0.05f;
                connectionLine.endWidth = 0.05f;
                connectionLine.positionCount = 2;
                connectionLine.useWorldSpace = true;

                // Matériel simple pour la ligne
                connectionLine.material = new Material(Shader.Find("Sprites/Default"));
                connectionLine.startColor = new Color(0.3f, 0.8f, 0.9f, 0.6f);
                connectionLine.endColor = new Color(0.3f, 0.8f, 0.9f, 0.6f);

                UpdateConnectionLine();
            }
        }

        void Update()
        {
            UpdateConnectionLine();
        }

        void UpdateConnectionLine()
        {
            if (connectionLine != null && nextNode != null)
            {
                connectionLine.SetPosition(0, transform.position);
                connectionLine.SetPosition(1, nextNode.transform.position);
            }
        }

        public void SetHighlight(bool highlighted)
        {
            if (nodeMaterial != null)
            {
                if (highlighted)
                {
                    nodeMaterial.EnableKeyword("_EMISSION");
                    nodeMaterial.SetColor("_EmissionColor", nodeColor * 2f);
                }
                else
                {
                    nodeMaterial.DisableKeyword("_EMISSION");
                }
            }
        }

        public void TriggerExplosion()
        {
            if (!canExplode) return;

            RibbonExplosion explosion = GetComponent<RibbonExplosion>();
            if (explosion != null)
            {
                explosion.Explode();
            }
        }
    }
}
