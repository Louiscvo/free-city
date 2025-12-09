using UnityEngine;
using System.Collections.Generic;

namespace FreeCityAI
{
    public class RibbonExplosion : MonoBehaviour
    {
        [Header("Ribbon Settings")]
        public int ribbonCount = 5;
        public float ribbonLength = 3f;
        public float ribbonSpeed = 2f;
        public float ribbonWidth = 0.1f;

        [Header("Explosion Pattern")]
        public float spreadAngle = 60f; // Angle de dispersion (vers la droite principalement)
        public Vector3 explosionDirection = Vector3.right;

        [Header("Animation")]
        public float explosionDuration = 2f;
        public AnimationCurve speedCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        public AnimationCurve widthCurve = AnimationCurve.Linear(0, 1, 1, 0.3f);

        [Header("Colors")]
        public Color[] ribbonColors = new Color[]
        {
            new Color(1f, 0.4f, 0.7f, 1f),    // Rose
            new Color(1f, 0.9f, 0.2f, 1f),    // Jaune
            new Color(0.3f, 0.7f, 1f, 1f),    // Bleu
            new Color(1f, 0.5f, 0.2f, 1f),    // Orange
            new Color(0.7f, 0.3f, 1f, 1f)     // Violet
        };

        [Header("Visual Effects")]
        public bool enableGlow = true;
        public float glowIntensity = 2f;
        public bool enableWave = true;
        public float waveFrequency = 2f;
        public float waveAmplitude = 0.3f;

        private List<Ribbon> ribbons = new List<Ribbon>();
        private bool isExploding = false;
        private float explosionTimer = 0f;

        private class Ribbon
        {
            public LineRenderer lineRenderer;
            public Vector3 direction;
            public Color color;
            public float speed;
            public float phase; // Pour l'animation en vague
            public List<Vector3> points = new List<Vector3>();
        }

        public void Explode()
        {
            if (isExploding) return;

            isExploding = true;
            explosionTimer = 0f;

            CreateRibbons();
        }

        void CreateRibbons()
        {
            // Nettoyer les anciens rubans
            foreach (var ribbon in ribbons)
            {
                if (ribbon.lineRenderer != null)
                    Destroy(ribbon.lineRenderer.gameObject);
            }
            ribbons.Clear();

            // Créer les nouveaux rubans
            for (int i = 0; i < ribbonCount; i++)
            {
                Ribbon ribbon = new Ribbon();

                // Direction avec spread
                float angleOffset = Random.Range(-spreadAngle / 2f, spreadAngle / 2f);
                Quaternion rotation = Quaternion.Euler(0, 0, angleOffset);
                ribbon.direction = rotation * explosionDirection.normalized;

                // Variation de vitesse
                ribbon.speed = ribbonSpeed * Random.Range(0.8f, 1.2f);

                // Couleur
                ribbon.color = ribbonColors[i % ribbonColors.Length];

                // Phase aléatoire pour l'animation
                ribbon.phase = Random.Range(0f, Mathf.PI * 2f);

                // Créer le LineRenderer
                GameObject ribbonObj = new GameObject($"Ribbon_{i}");
                ribbonObj.transform.parent = transform;
                ribbonObj.transform.localPosition = Vector3.zero;

                ribbon.lineRenderer = ribbonObj.AddComponent<LineRenderer>();
                SetupLineRenderer(ribbon.lineRenderer, ribbon.color);

                ribbons.Add(ribbon);
            }
        }

        void SetupLineRenderer(LineRenderer lr, Color color)
        {
            lr.useWorldSpace = true;
            lr.positionCount = 0;

            // Largeur
            lr.startWidth = ribbonWidth;
            lr.endWidth = ribbonWidth * 0.3f;

            // Matériel
            Material ribbonMat = new Material(Shader.Find("Sprites/Default"));
            ribbonMat.color = color;

            if (enableGlow)
            {
                ribbonMat.EnableKeyword("_EMISSION");
                ribbonMat.SetColor("_EmissionColor", color * glowIntensity);
            }

            lr.material = ribbonMat;

            // Couleurs avec gradient
            Gradient gradient = new Gradient();
            gradient.SetKeys(
                new GradientColorKey[] {
                    new GradientColorKey(color, 0f),
                    new GradientColorKey(color, 1f)
                },
                new GradientAlphaKey[] {
                    new GradientAlphaKey(1f, 0f),
                    new GradientAlphaKey(0f, 1f)
                }
            );
            lr.colorGradient = gradient;

            // Qualité
            lr.numCornerVertices = 5;
            lr.numCapVertices = 5;
        }

        void Update()
        {
            if (!isExploding) return;

            explosionTimer += Time.deltaTime;
            float progress = Mathf.Clamp01(explosionTimer / explosionDuration);

            UpdateRibbons(progress);

            if (explosionTimer >= explosionDuration)
            {
                // Fin de l'explosion
                isExploding = false;
            }
        }

        void UpdateRibbons(float progress)
        {
            foreach (var ribbon in ribbons)
            {
                // Ajouter un nouveau point
                float currentSpeed = ribbon.speed * speedCurve.Evaluate(progress);
                Vector3 basePosition = transform.position + ribbon.direction * currentSpeed * explosionTimer;

                // Ajouter une ondulation si activé
                if (enableWave)
                {
                    float waveOffset = Mathf.Sin(explosionTimer * waveFrequency + ribbon.phase) * waveAmplitude;
                    Vector3 perpendicular = Vector3.Cross(ribbon.direction, Vector3.forward);
                    basePosition += perpendicular * waveOffset;
                }

                ribbon.points.Add(basePosition);

                // Limiter le nombre de points pour éviter les performances dégradées
                int maxPoints = Mathf.CeilToInt(ribbonLength / (currentSpeed * Time.deltaTime + 0.001f));
                while (ribbon.points.Count > maxPoints)
                {
                    ribbon.points.RemoveAt(0);
                }

                // Mettre à jour le LineRenderer
                ribbon.lineRenderer.positionCount = ribbon.points.Count;
                ribbon.lineRenderer.SetPositions(ribbon.points.ToArray());

                // Mise à jour de la largeur avec la courbe
                float width = ribbonWidth * widthCurve.Evaluate(progress);
                ribbon.lineRenderer.startWidth = width;
                ribbon.lineRenderer.endWidth = width * 0.3f;
            }
        }

        [ContextMenu("Test Explosion")]
        public void TestExplosion()
        {
            Explode();
        }

        void OnDestroy()
        {
            // Nettoyer les rubans
            foreach (var ribbon in ribbons)
            {
                if (ribbon.lineRenderer != null)
                    Destroy(ribbon.lineRenderer.gameObject);
            }
            ribbons.Clear();
        }
    }
}
