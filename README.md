# Free City AI - Système d'Évolution d'IA

Reproduction de l'effet visuel d'interface du film **Free Guy** montrant l'évolution d'une IA de personnage au-delà de sa programmation initiale.

## 🎬 Effet Visuel

L'interface montre :
1. **Arbre comportemental** - Nœuds connectés en boucle (SLEEP → WAKE UP → BANK TELLER → etc.)
2. **Lumière cyan** - Parcourt lentement le circuit, symbolisant l'exécution normale de l'IA
3. **Explosion de rubans** - Quand l'IA évolue, des rubans colorés (rose, jaune, bleu, orange, violet) explosent depuis chaque nœud vers la droite, représentant toutes les nouvelles possibilités

## 📁 Structure du Projet

```
FreeCityAI/
├── Scripts/
│   ├── BehaviorNode.cs              # Nœud individuel de l'arbre
│   ├── BehaviorTreeCircuit.cs       # Circuit complet et gestion des nœuds
│   ├── CircuitLight.cs              # Lumière cyan qui parcourt le circuit
│   ├── RibbonExplosion.cs           # Système d'explosion de rubans
│   └── AIEvolutionSequence.cs       # Contrôleur principal de la séquence
├── Prefabs/                         # (vide - à créer dans Unity)
├── Materials/                       # (vide - à créer dans Unity)
└── README.md                        # Ce fichier
```

## 🚀 Setup dans Unity

### Étape 1 : Importer les scripts

1. Ouvrez votre projet Unity
2. Créez un dossier `Assets/FreeCityAI/`
3. Copiez tous les scripts de `FreeCityAI/Scripts/` vers `Assets/FreeCityAI/Scripts/`

### Étape 2 : Créer la scène

1. **Créez un GameObject vide** : `Create → Empty GameObject`
   - Nommez-le `BehaviorCircuit`
   - Position: (0, 0, 0)

2. **Ajoutez le script BehaviorTreeCircuit** au GameObject
   - Dans l'Inspector, ajustez les paramètres :
     - `Node Names`: Liste des actions (pré-remplie)
     - `Circuit Radius`: 3
     - `Node Spacing`: 1.5
     - `Use Circular Layout`: false (pour layout horizontal)

3. **Créez la lumière du circuit** : `Create → Empty GameObject`
   - Nommez-le `CircuitLight`
   - Ajoutez le script `CircuitLight`
   - Paramètres :
     - `Light Color`: Cyan (0, 255, 255)
     - `Speed`: 1
     - `Pause At Nodes`: true

4. **Ajoutez le contrôleur de séquence** au GameObject `BehaviorCircuit`
   - Ajoutez le script `AIEvolutionSequence`
   - Assignez les références :
     - `Behavior Circuit`: Le même GameObject
     - `Circuit Light`: L'objet CircuitLight créé
     - `Main Camera`: La caméra principale
   - Paramètres :
     - `Normal Loop Duration`: 5 (secondes avant auto-évolution)
     - `Auto Trigger`: true

### Étape 3 : Générer le circuit

1. Sélectionnez le GameObject `BehaviorCircuit`
2. Dans l'Inspector, faites un clic droit sur le script `BehaviorTreeCircuit`
3. Cliquez sur **"Generate Circuit"**
4. Les nœuds et leurs connexions seront créés automatiquement

### Étape 4 : Configurer la caméra

1. Positionnez la caméra pour voir tout le circuit :
   - Position: (0, 0, -10)
   - Projection: Orthographic
   - Size: 5-8 selon votre layout

### Étape 5 : Lancer la scène

1. Appuyez sur **Play**
2. La lumière cyan devrait parcourir le circuit
3. Après 5 secondes (ou appuyez sur **SPACE**), l'explosion se déclenche !

## 🎮 Contrôles

- **SPACE** ou **E** : Déclencher l'évolution manuellement
- **R** : Reset la séquence (après l'évolution)

## ⚙️ Paramètres Personnalisables

### BehaviorTreeCircuit
- `Node Names`: Changez les noms des actions
- `Circuit Radius`: Rayon du circuit (si circulaire)
- `Node Spacing`: Espacement entre nœuds (si horizontal)
- `Use Circular Layout`: Circulaire vs horizontal

### CircuitLight
- `Light Color`: Couleur de la lumière (défaut: cyan)
- `Speed`: Vitesse de parcours
- `Pause At Nodes`: Pause aux nœuds
- `Enable Trail`: Activer la trainée

### RibbonExplosion
- `Ribbon Count`: Nombre de rubans par nœud (défaut: 5)
- `Ribbon Length`: Longueur des rubans
- `Ribbon Speed`: Vitesse d'expansion
- `Spread Angle`: Angle de dispersion (60° par défaut)
- `Enable Wave`: Ondulation des rubans
- `Wave Frequency`: Fréquence de l'ondulation

### AIEvolutionSequence
- `Normal Loop Duration`: Temps avant auto-évolution
- `Auto Trigger`: Évolution automatique ou manuelle
- `Animate Camera`: Animation de caméra pendant l'évolution
- `Enable Screen Shake`: Shake de caméra à l'explosion

## 🎨 Customisation Visuelle

### Changer les couleurs des nœuds

Dans `BehaviorNode.cs`, modifiez :
```csharp
public Color nodeColor = new Color(0.2f, 0.6f, 0.8f, 1f);
```

### Changer les couleurs des rubans

Dans `RibbonExplosion.cs`, modifiez le tableau `ribbonColors` :
```csharp
public Color[] ribbonColors = new Color[]
{
    new Color(1f, 0.4f, 0.7f, 1f),    // Rose
    new Color(1f, 0.9f, 0.2f, 1f),    // Jaune
    // ... ajoutez vos couleurs
};
```

### Améliorer le rendu

Pour un meilleur effet visuel :

1. **Bloom Post-Processing** :
   - Installez le package `Post Processing` (Window → Package Manager)
   - Ajoutez un `Post Process Volume` à la scène
   - Activez `Bloom` avec forte intensité

2. **Matériaux émissifs** :
   - Créez des matériaux avec Shader `Standard`
   - Activez `Emission`
   - Assignez aux nœuds

3. **Background sombre** :
   - Réglez la couleur de fond de la caméra en noir/gris foncé

## 🐛 Debug

### La lumière ne bouge pas
- Vérifiez que `CircuitLight` est bien initialisé avec les nœuds
- Vérifiez que `isMoving` est true
- Appelez manuellement `Initialize()` avec la liste de nœuds

### Pas de rubans lors de l'explosion
- Vérifiez que chaque nœud a le composant `RibbonExplosion`
- Regardez dans la console Unity pour les erreurs
- Testez manuellement avec le menu contextuel "Test Explosion"

### Les nœuds ne sont pas connectés
- Utilisez le menu contextuel "Generate Circuit" sur `BehaviorTreeCircuit`
- Vérifiez que `nextNode` est assigné dans l'Inspector

## 📝 Menu Contextuel (Debug)

Clic droit sur les scripts dans l'Inspector :

- `BehaviorTreeCircuit` :
  - **Generate Circuit** : Régénère tous les nœuds
  - **Test Evolution** : Déclenche l'explosion

- `RibbonExplosion` :
  - **Test Explosion** : Test l'explosion d'un seul nœud

- `AIEvolutionSequence` :
  - **Test Evolution Sequence** : Lance la séquence complète
  - **Reset Sequence** : Reset tout

## 🎯 Améliorations Possibles

1. **Sons** :
   - Ajouter un son de "boucle" pour le circuit normal
   - Son d'explosion quand l'évolution se déclenche
   - Sons subtils au passage de chaque nœud

2. **VFX Graph** :
   - Remplacer les LineRenderer par Visual Effect Graph (pour plus de performance et d'options)
   - Particules additionnelles lors de l'explosion

3. **UI** :
   - Panel d'information sur l'état de l'IA
   - Texte 3D sur les nœuds
   - Timeline pour contrôler la séquence

4. **Interactivité** :
   - Cliquer sur les nœuds pour voir les détails
   - Mode "debug" pour explorer l'arbre
   - Édition en temps réel des paramètres

## 📚 Références

- Film : **Free Guy** (2021)
- Scène : Interface de développement montrant l'évolution de l'IA de Guy

## 🤝 Support

Si vous avez des questions ou des problèmes, vérifiez :
1. Que tous les scripts sont dans le bon namespace `FreeCityAI`
2. Que Unity est en version 2020.3+ (pour meilleure compatibilité)
3. La console Unity pour les messages d'erreur

---

**Créé pour reproduire l'effet visuel iconique de Free Guy** 🎮✨
