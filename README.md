# Free City AI

Effet visuel Unity reproduisant l'interface d'évolution d'IA du film Free Guy (2021).

## Fonctionnalités

- Arbre comportemental avec circuit de nœuds connectés
- Lumière animée parcourant le circuit
- Explosion de rubans colorés lors de l'évolution
- Setup automatique via menu Unity
- Paramètres personnalisables

## Installation

**Prérequis** : Unity 2020.3+

1. Cloner le repo
2. Copier `Scripts/` dans `Assets/free-city/Scripts/`
3. Menu `GameObject > Free City AI > Setup Complete Scene`
4. Appuyer sur Play puis Espace pour déclencher l'effet

## Structure

```
Scripts/
├── BehaviorNode.cs           # Nœud individuel
├── BehaviorTreeCircuit.cs    # Circuit complet
├── CircuitLight.cs           # Lumière animée
├── RibbonExplosion.cs        # Système d'explosion
├── AIEvolutionSequence.cs    # Contrôleur principal
└── free-citySetup.cs         # Setup automatique
```

## Utilisation

```csharp
public AIEvolutionSequence evolution;

void OnTrigger() {
    evolution.TriggerEvolution();
}
```

## Licence

Libre d'utilisation pour projets personnels et commerciaux.
