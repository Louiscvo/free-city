# 🚀 Quick Start - Free City AI

Guide rapide pour setup en **5 minutes** !

## Option 1 : Setup Automatique (Recommandé)

### Dans Unity Editor :

1. **Copiez tous les scripts** vers votre projet Unity :
   ```
   FreeCityAI/Scripts/ → Assets/FreeCityAI/Scripts/
   ```

2. **Menu Unity** → `GameObject` → `Free City AI` → `Setup Complete Scene`

3. **Press Play** ▶️

4. **Press SPACE** pour déclencher l'évolution !

**C'est tout !** ✨

---

## Option 2 : Setup Manuel

Si vous préférez contrôler chaque étape :

### Étape 1 : Créer le Circuit (30 sec)

1. `GameObject` → `Create Empty` → Nommez "BehaviorCircuit"
2. Add Component → `BehaviorTreeCircuit`
3. Clic droit sur le component → `Generate Circuit`

### Étape 2 : Créer la Lumière (30 sec)

1. `GameObject` → `Create Empty` → Nommez "CircuitLight"
2. Add Component → `CircuitLight`
3. Dans l'Inspector du circuit, assignez la référence `Circuit Light`

### Étape 3 : Ajouter le Contrôleur (30 sec)

1. Sur "BehaviorCircuit", Add Component → `AIEvolutionSequence`
2. Assignez les références :
   - Behavior Circuit : lui-même
   - Circuit Light : l'objet créé
   - Main Camera : Camera principale

### Étape 4 : Play !

1. Press Play ▶️
2. Press SPACE ⌨️

---

## 🎮 Contrôles

| Touche | Action |
|--------|--------|
| **SPACE** | Déclencher l'évolution |
| **E** | Alternative pour déclencher |
| **R** | Reset après évolution |

---

## ⚙️ Réglages Rapides

### Vitesse de la lumière
Sur `CircuitLight` → `Speed` (défaut: 1)

### Temps avant auto-évolution
Sur `AIEvolutionSequence` → `Normal Loop Duration` (défaut: 5s)

### Nombre de rubans
Sur chaque nœud → `RibbonExplosion` → `Ribbon Count` (défaut: 5)

### Couleurs
Éditez dans les scripts :
- `BehaviorNode.cs` : Couleur des nœuds
- `RibbonExplosion.cs` : Couleurs des rubans

---

## 🎨 Pour un Meilleur Rendu

### 1. Background Sombre
Caméra → `Background` → Noir ou gris foncé

### 2. Post-Processing Bloom
```
Window → Package Manager → Post Processing → Install
Add → Post Process Volume → New Profile → Add Bloom
```

### 3. Caméra Orthographique
Caméra → `Projection` → Orthographic
`Size` → 5-8

---

## 🐛 Problèmes Courants

**La lumière ne bouge pas ?**
→ Vérifiez que les nœuds sont générés (clic droit → Generate Circuit)

**Pas de rubans ?**
→ Vérifiez que chaque nœud a le component `RibbonExplosion`

**Erreurs de namespace ?**
→ Tous les scripts doivent être dans `FreeCityAI/Scripts/`

---

## 📹 Résultat Attendu

1. **Avant évolution** :
   - Circuit de nœuds connectés
   - Lumière cyan qui parcourt en boucle
   - Labels des actions visibles

2. **Après évolution (SPACE)** :
   - Ralentissement de la lumière
   - Explosion de rubans colorés vers la droite
   - Chaque nœud génère 5 rubans fluides
   - Rubans ondulent et se dispersent

---

## 🎯 Next Steps

Une fois que ça marche, explorez :

1. **Personnalisation** :
   - Changez les noms dans `nodeNames[]`
   - Ajustez les couleurs
   - Modifiez l'animation

2. **Effets Visuels** :
   - Ajoutez des particules
   - Sons d'ambiance
   - UI overlay

3. **Intégration** :
   - Utilisez dans votre jeu
   - Créez des variantes
   - Build standalone

---

**Amusez-vous bien ! 🎮✨**

Des questions ? Consultez `README.md` pour la documentation complète.
