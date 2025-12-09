using UnityEngine;
using System.Collections.Generic;

namespace FreeCityAI
{
    public class CircuitLight : MonoBehaviour
    {
        [Header("Light Settings")]
        public Color lightColor = new Color(0f, 1f, 1f, 1f); // Cyan
        public float lightIntensity = 2f;
        public float lightSize = 0.3f;

        [Header("Movement Settings")]
        public float speed = 1f;
        public bool isMoving = true;
        public bool pauseAtNodes = true;
        public float pauseDuration = 0.3f;

        [Header("Visual Effects")]
        public bool enableTrail = true;
        public float trailLength = 0.5f;

        private List<BehaviorNode> nodes;
        private int currentNodeIndex = 0;
        private float journeyProgress = 0f;
        private bool isPaused = false;
        private float pauseTimer = 0f;

        private Light lightComponent;
        private TrailRenderer trailRenderer;
        private SpriteRenderer glowSprite;

        void Awake()
        {
            SetupVisuals();
        }

        void SetupVisuals()
        {
            // Créer un sprite glow pour la lumière
            GameObject glowObj = new GameObject("Glow");
            glowObj.transform.parent = transform;
            glowObj.transform.localPosition = Vector3.zero;

            glowSprite = glowObj.AddComponent<SpriteRenderer>();

            // Créer un sprite circulaire simple
            Texture2D circleTexture = CreateCircleTexture(128);
            Sprite circleSprite = Sprite.Create(
                circleTexture,
                new Rect(0, 0, 128, 128),
                new Vector2(0.5f, 0.5f)
            );

            glowSprite.sprite = circleSprite;
            glowSprite.color = lightColor;
            glowSprite.transform.localScale = Vector3.one * lightSize;

            // Matériel avec glow
            Material glowMat = new Material(Shader.Find("Sprites/Default"));
            glowMat.color = lightColor;
            glowMat.EnableKeyword("_EMISSION");
            glowMat.SetColor("_EmissionColor", lightColor * lightIntensity);
            glowSprite.material = glowMat;

            // Trail
            if (enableTrail)
            {
                trailRenderer = gameObject.AddComponent<TrailRenderer>();
                trailRenderer.time = trailLength;
                trailRenderer.startWidth = lightSize * 0.5f;
                trailRenderer.endWidth = 0.01f;
                trailRenderer.material = glowMat;
                trailRenderer.startColor = lightColor;
                trailRenderer.endColor = new Color(lightColor.r, lightColor.g, lightColor.b, 0f);
            }

            // Light component (optionnel pour 3D)
            lightComponent = gameObject.AddComponent<Light>();
            lightComponent.type = LightType.Point;
            lightComponent.color = lightColor;
            lightComponent.intensity = lightIntensity;
            lightComponent.range = lightSize * 2f;
        }

        Texture2D CreateCircleTexture(int size)
        {
            Texture2D texture = new Texture2D(size, size, TextureFormat.RGBA32, false);
            Color[] pixels = new Color[size * size];

            Vector2 center = new Vector2(size / 2f, size / 2f);
            float radius = size / 2f;

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    float distance = Vector2.Distance(new Vector2(x, y), center);
                    float alpha = 1f - Mathf.Clamp01(distance / radius);
                    alpha = Mathf.Pow(alpha, 2f); // Falloff plus doux
                    pixels[y * size + x] = new Color(1f, 1f, 1f, alpha);
                }
            }

            texture.SetPixels(pixels);
            texture.Apply();
            return texture;
        }

        public void Initialize(List<BehaviorNode> circuitNodes)
        {
            nodes = circuitNodes;
            if (nodes != null && nodes.Count > 0)
            {
                transform.position = nodes[0].transform.position;
            }
        }

        void Update()
        {
            if (!isMoving || nodes == null || nodes.Count == 0)
                return;

            if (isPaused)
            {
                pauseTimer += Time.deltaTime;
                if (pauseTimer >= pauseDuration)
                {
                    isPaused = false;
                    pauseTimer = 0f;
                }
                return;
            }

            MoveAlongCircuit();
        }

        void MoveAlongCircuit()
        {
            BehaviorNode currentNode = nodes[currentNodeIndex];
            BehaviorNode nextNode = currentNode.nextNode;

            if (nextNode == null)
                return;

            // Progression le long du segment
            journeyProgress += Time.deltaTime * speed;

            // Position interpolée
            transform.position = Vector3.Lerp(
                currentNode.transform.position,
                nextNode.transform.position,
                journeyProgress
            );

            // Highlight du nœud actuel
            currentNode.SetHighlight(journeyProgress < 0.5f);
            nextNode.SetHighlight(journeyProgress >= 0.5f);

            // Arrivé au nœud suivant
            if (journeyProgress >= 1f)
            {
                journeyProgress = 0f;
                currentNodeIndex = (currentNodeIndex + 1) % nodes.Count;

                if (pauseAtNodes)
                {
                    isPaused = true;
                }
            }
        }

        public void StopMovement()
        {
            isMoving = false;
        }

        public void ResumeMovement()
        {
            isMoving = true;
        }

        public void SetSpeed(float newSpeed)
        {
            speed = newSpeed;
        }
    }
}
