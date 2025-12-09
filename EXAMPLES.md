# Exemples d'Utilisation - Free City AI

## 📋 Table des Matières

1. [Configuration Basique](#configuration-basique)
2. [Configurations Avancées](#configurations-avancées)
3. [Cas d'Usage](#cas-dusage)
4. [Intégrations](#intégrations)
5. [Variantes Visuelles](#variantes-visuelles)

---

## Configuration Basique

### Exemple 1 : Circuit Simple avec 4 Nœuds

```csharp
// Dans BehaviorTreeCircuit
nodeNames = new string[] {
    "IDLE",
    "PATROL",
    "ALERT",
    "RETURN"
};

nodeSpacing = 2f;
useCircularLayout = true;
circuitRadius = 2.5f;
```

**Résultat** : Petit circuit circulaire parfait pour un NPC simple

---

### Exemple 2 : Routine Quotidienne Complète

```csharp
// Routine réaliste d'un PNJ
nodeNames = new string[] {
    "6AM - WAKE UP",
    "7AM - BREAKFAST",
    "8AM - COMMUTE",
    "9AM - WORK START",
    "12PM - LUNCH",
    "1PM - WORK",
    "5PM - COMMUTE HOME",
    "6PM - DINNER",
    "7PM - RELAX",
    "8PM - HOBBY",
    "10PM - SLEEP"
};

useCircularLayout = false; // Layout horizontal pour voir la timeline
nodeSpacing = 1.2f;
```

**Résultat** : Timeline horizontale d'une journée complète

---

## Configurations Avancées

### Exemple 3 : Explosion Spectaculaire

```csharp
// Dans RibbonExplosion de chaque nœud
ribbonCount = 10;           // Plus de rubans
ribbonLength = 5f;          // Rubans plus longs
ribbonSpeed = 3f;           // Plus rapide
spreadAngle = 90f;          // Dispersion large

enableWave = true;
waveFrequency = 3f;         // Ondulation rapide
waveAmplitude = 0.5f;       // Grande amplitude

glowIntensity = 4f;         // Glow intense
```

**Résultat** : Explosion massive et spectaculaire

---

### Exemple 4 : Style Minimaliste

```csharp
// Configuration subtile et élégante
ribbonCount = 3;
ribbonLength = 2f;
ribbonSpeed = 1f;
spreadAngle = 30f;          // Dispersion étroite

enableWave = false;         // Pas d'ondulation
ribbonWidth = 0.05f;        // Rubans fins

// Couleurs monochromes
ribbonColors = new Color[] {
    new Color(0.8f, 0.8f, 1f, 1f),    // Bleu clair
    new Color(0.6f, 0.6f, 0.9f, 1f),  // Bleu moyen
    new Color(0.4f, 0.4f, 0.8f, 1f)   // Bleu foncé
};
```

**Résultat** : Effet discret et professionnel

---

### Exemple 5 : Animation Séquentielle

```csharp
// Dans AIEvolutionSequence - modifier la coroutine

IEnumerator ExplodeNodeDelayed(BehaviorNode node, float delay)
{
    yield return new WaitForSeconds(delay);

    // Flash du nœud avant explosion
    node.SetHighlight(true);
    yield return new WaitForSeconds(0.1f);

    // Son par nœud
    if (audioSource != null && explosionSound != null)
        audioSource.PlayOneShot(explosionSound, 0.3f);

    // Explosion
    node.TriggerExplosion();

    yield return new WaitForSeconds(0.05f);
    node.SetHighlight(false);
}
```

**Résultat** : Effet domino spectaculaire avec flash et son

---

## Cas d'Usage

### Cas 1 : Écran de Chargement

```csharp
public class LoadingScreen : MonoBehaviour
{
    public AIEvolutionSequence evolutionSequence;

    void Start()
    {
        // Désactiver auto-trigger
        evolutionSequence.autoTrigger = false;

        // Déclencher quand le chargement est complet
        StartCoroutine(LoadGame());
    }

    IEnumerator LoadGame()
    {
        // Simuler chargement
        for (int i = 0; i < 100; i++)
        {
            // Load assets...
            yield return new WaitForSeconds(0.05f);
        }

        // Chargement complet → Explosion !
        evolutionSequence.TriggerEvolution();

        yield return new WaitForSeconds(3f);

        // Charger la scène principale
        // SceneManager.LoadScene("MainGame");
    }
}
```

---

### Cas 2 : Visualisation de Décision d'IA

```csharp
public class AIDecisionVisualizer : MonoBehaviour
{
    public BehaviorTreeCircuit circuit;

    // Appeler quand l'IA fait un choix
    public void OnAIDecision(string decision)
    {
        // Trouver le nœud correspondant
        BehaviorNode node = circuit.nodes.Find(n => n.nodeName == decision);

        if (node != null)
        {
            // Explosion uniquement sur ce nœud
            node.TriggerExplosion();
        }
    }

    // Exemple d'utilisation
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            OnAIDecision("ATTACK");

        if (Input.GetKeyDown(KeyCode.Alpha2))
            OnAIDecision("DEFEND");
    }
}
```

---

### Cas 3 : Arbre de Compétences

```csharp
public class SkillTreeEvolution : MonoBehaviour
{
    public BehaviorTreeCircuit skillTree;

    void Start()
    {
        // Noms de compétences
        skillTree.nodeNames = new string[] {
            "BASIC ATTACK",
            "POWER STRIKE",
            "COMBO MASTER",
            "ULTIMATE",
            "DEFENSE UP",
            "SPEED BOOST",
            "HEALTH REGEN"
        };

        skillTree.GenerateCircuit();
        skillTree.useCircularLayout = true;
    }

    public void UnlockSkill(string skillName)
    {
        BehaviorNode skill = skillTree.nodes.Find(n => n.nodeName == skillName);

        if (skill != null)
        {
            // Explosion pour montrer le déblocage
            skill.TriggerExplosion();

            // Changer la couleur du nœud
            skill.nodeColor = Color.green;
        }
    }
}
```

---

## Intégrations

### Avec Timeline

```csharp
using UnityEngine.Playables;

public class TimelineIntegration : MonoBehaviour
{
    public PlayableDirector timeline;
    public AIEvolutionSequence evolution;

    void Start()
    {
        // Déclencher l'évolution à un moment précis de la timeline
        timeline.stopped += OnTimelineStopped;
    }

    void OnTimelineStopped(PlayableDirector director)
    {
        evolution.TriggerEvolution();
    }
}
```

---

### Avec Dialogue System

```csharp
public class DialogueIntegration : MonoBehaviour
{
    public AIEvolutionSequence evolution;

    // Appeler depuis votre système de dialogue
    public void OnDialogueChoice(string choice)
    {
        if (choice == "Evolve AI")
        {
            evolution.TriggerEvolution();
        }
    }
}
```

---

### Avec Analytics

```csharp
public class AnalyticsIntegration : MonoBehaviour
{
    public BehaviorTreeCircuit circuit;

    void Start()
    {
        // Logger les décisions de l'IA
        StartCoroutine(TrackAIBehavior());
    }

    IEnumerator TrackAIBehavior()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            // Récupérer l'état actuel
            if (circuit.nodes.Count > 0)
            {
                string currentState = circuit.nodes[0].nodeName;
                // Analytics.LogEvent("AI_State", currentState);
                Debug.Log($"AI State: {currentState}");
            }
        }
    }
}
```

---

## Variantes Visuelles

### Style Néon Cyberpunk

```csharp
// Couleurs néon
ribbonColors = new Color[] {
    new Color(1f, 0f, 1f, 1f),      // Magenta
    new Color(0f, 1f, 1f, 1f),      // Cyan
    new Color(1f, 0f, 0.5f, 1f),    // Rose néon
    new Color(0.5f, 0f, 1f, 1f)     // Violet néon
};

glowIntensity = 5f;  // Glow très intense
ribbonWidth = 0.15f; // Rubans épais
```

**+ Post-Processing** : Bloom max + Chromatic Aberration

---

### Style Organique

```csharp
// Mouvement plus naturel
enableWave = true;
waveFrequency = 1.5f;  // Lent
waveAmplitude = 0.8f;  // Grande amplitude

// Couleurs naturelles
ribbonColors = new Color[] {
    new Color(0.2f, 0.8f, 0.3f, 1f),  // Vert
    new Color(0.4f, 0.6f, 0.2f, 1f),  // Vert olive
    new Color(0.6f, 0.8f, 0.4f, 1f),  // Vert clair
    new Color(0.3f, 0.5f, 0.6f, 1f)   // Bleu-vert
};

ribbonSpeed = 0.8f;  // Plus lent
```

---

### Style Glitch/Matrix

```csharp
// Dans un nouveau script GlitchEffect.cs
void Update()
{
    if (isGlitching)
    {
        // Randomize positions légèrement
        foreach (var node in circuit.nodes)
        {
            Vector3 glitch = Random.insideUnitSphere * 0.1f;
            node.transform.position += glitch;
        }
    }
}

// Déclencher avant l'évolution
IEnumerator GlitchBeforeEvolution()
{
    isGlitching = true;
    yield return new WaitForSeconds(0.5f);
    isGlitching = false;

    evolution.TriggerEvolution();
}
```

---

### Style Particules

```csharp
// Ajouter des particules aux rubans
public class ParticleRibbon : MonoBehaviour
{
    public ParticleSystem particles;

    void OnRibbonCreated(Vector3 position, Color color)
    {
        var main = particles.main;
        main.startColor = color;

        particles.transform.position = position;
        particles.Play();
    }
}
```

---

## Template de Script Personnalisé

```csharp
using UnityEngine;
using FreeCityAI;

public class MyCustomEvolution : MonoBehaviour
{
    public BehaviorTreeCircuit circuit;
    public AIEvolutionSequence sequence;

    [Header("Custom Settings")]
    public bool useCustomTrigger = true;
    public float customDelay = 3f;

    void Start()
    {
        if (useCustomTrigger)
        {
            sequence.autoTrigger = false;
            Invoke(nameof(CustomEvolution), customDelay);
        }
    }

    void CustomEvolution()
    {
        // Votre logique custom ici
        Debug.Log("Starting custom evolution!");

        // Vous pouvez modifier les paramètres avant l'évolution
        foreach (var node in circuit.nodes)
        {
            RibbonExplosion explosion = node.GetComponent<RibbonExplosion>();
            if (explosion != null)
            {
                // Personnalisation par nœud
                explosion.ribbonCount = Random.Range(3, 8);
                explosion.spreadAngle = Random.Range(30f, 90f);
            }
        }

        // Déclencher
        sequence.TriggerEvolution();
    }
}
```

---

## Tips & Tricks

### 1. Randomiser les Explosions

```csharp
foreach (var node in nodes)
{
    var explosion = node.GetComponent<RibbonExplosion>();
    explosion.ribbonSpeed = Random.Range(1f, 3f);
    explosion.spreadAngle = Random.Range(45f, 90f);
}
```

### 2. Sons Procéduraux

```csharp
AudioSource.pitch = Random.Range(0.8f, 1.2f); // Varier le pitch
```

### 3. Couleurs Gradient

```csharp
Color startColor = Color.blue;
Color endColor = Color.red;
for (int i = 0; i < ribbonColors.Length; i++)
{
    float t = i / (float)ribbonColors.Length;
    ribbonColors[i] = Color.Lerp(startColor, endColor, t);
}
```

---

**Explorez et créez vos propres variantes ! 🎨✨**
