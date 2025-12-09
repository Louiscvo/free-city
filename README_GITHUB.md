# 🎮 Free City AI

[![Unity Version](https://img.shields.io/badge/Unity-2020.3%2B-blue)](https://unity.com/)
[![License](https://img.shields.io/badge/License-Free%20to%20Use-green)](LICENSE)
[![Version](https://img.shields.io/badge/Version-1.0.0-orange)](VERSION.md)

> Système d'évolution d'IA inspiré du film **Free Guy** (2021)

Reproduction fidèle de l'effet visuel iconique montrant l'évolution d'une IA au-delà de sa programmation initiale.

![Free City AI Demo](https://via.placeholder.com/800x400/0a0e27/00d9ff?text=Free+City+AI+Demo)

## ✨ Fonctionnalités

- 🔄 **Arbre comportemental** - Circuit de nœuds connectés
- 💡 **Lumière cyan animée** - Parcourt le circuit en boucle
- 💥 **Explosion de rubans** - Rubans colorés lors de l'évolution
- ⚙️ **Setup automatique** - Menu Unity intégré
- 🎨 **30+ paramètres** - Hautement personnalisable
- 📚 **Documentation complète** - 10 fichiers de doc

## 🚀 Installation Rapide

### Prérequis

- Unity 2020.3 ou supérieur
- Aucune dépendance externe

### Setup (3 minutes)

```bash
# 1. Cloner le repo
git clone https://github.com/votre-username/FreeCityAI.git

# 2. Dans Unity
# Copier Scripts/ vers Assets/FreeCityAI/Scripts/

# 3. Menu Unity
GameObject → Free City AI → Setup Complete Scene

# 4. Play & Enjoy!
# Press Play → Press SPACE
```

## 📖 Documentation

| Fichier | Description |
|---------|-------------|
| [START_HERE.md](START_HERE.md) | 👉 **Commencez ici** |
| [QUICKSTART.md](QUICKSTART.md) | Installation rapide |
| [README.md](README.md) | Documentation complète |
| [EXAMPLES.md](EXAMPLES.md) | 15+ exemples pratiques |
| [STRUCTURE.txt](STRUCTURE.txt) | Architecture technique |

📚 [Documentation complète →](https://votre-username.github.io/FreeCityAI/)

## 🎬 Résultat Visuel

### Avant Évolution
- Circuit de nœuds en boucle
- Lumière cyan parcourant
- Routine répétitive

### Après Évolution (SPACE)
- 💥 **Explosion spectaculaire**
- Rubans colorés jaillissant
- Animation fluide
- Effet "WOW" garanti

## 🔧 Technologies

- **Unity Engine** 2020.3+
- **C#** .NET Standard 2.0
- **LineRenderer** pour les rubans
- **Coroutines** pour animations
- **Custom Inspectors** pour l'édition

## 📊 Statistiques

```
📦 Code:              6 scripts C#, ~980 lignes
📚 Documentation:     10 fichiers, ~3500 lignes
✨ Fonctionnalités:   30+ paramètres configurables
⚡ Setup:             3 minutes
```

## 🎯 Cas d'Usage

- ⏳ Écran de chargement
- 🚪 Transition de niveau
- 🎨 Menu principal animé
- ⭐ Arbre de compétences
- 🤖 Visualisation d'IA
- 🎬 Cutscenes

## 💡 Exemples d'Utilisation

### Exemple 1 : Setup Basique

```csharp
// Dans votre scène Unity
// 1. Menu GameObject → Free City AI → Setup Complete Scene
// 2. Play !
```

### Exemple 2 : Personnalisation

```csharp
// Changer les couleurs
ribbonColors = new Color[] {
    Color.red,
    Color.blue,
    Color.green
};

// Modifier le timing
normalLoopDuration = 10f; // 10 secondes
```

### Exemple 3 : Déclencher Manuellement

```csharp
public AIEvolutionSequence evolution;

void OnPlayerWins() {
    evolution.TriggerEvolution();
}
```

Voir [EXAMPLES.md](EXAMPLES.md) pour plus d'exemples !

## 🏗️ Architecture

```
FreeCityAI/
├── Scripts/
│   ├── BehaviorNode.cs           # Nœud individuel
│   ├── BehaviorTreeCircuit.cs    # Circuit complet
│   ├── CircuitLight.cs           # Lumière animée
│   ├── RibbonExplosion.cs        # Système d'explosion
│   ├── AIEvolutionSequence.cs    # Contrôleur principal
│   └── FreeCityAISetup.cs        # Setup automatique
└── Documentation/
    ├── START_HERE.md
    ├── QUICKSTART.md
    ├── README.md
    ├── EXAMPLES.md
    └── ...
```

## 🤝 Contribution

Les contributions sont les bienvenues !

1. Fork le projet
2. Créez une branche (`git checkout -b feature/AmazingFeature`)
3. Commit vos changements (`git commit -m 'Add AmazingFeature'`)
4. Push vers la branche (`git push origin feature/AmazingFeature`)
5. Ouvrez une Pull Request

## 📝 License

**Libre d'utilisation** pour :
- ✅ Projets personnels
- ✅ Projets commerciaux
- ✅ Portfolio
- ✅ Apprentissage

**Mention appréciée** : "Inspired by Free Guy (2021)"

## 🌟 Remerciements

- 🎬 Film : **Free Guy** (2021)
- 🎮 Inspiré par la scène de l'interface d'évolution d'IA
- ❤️ Créé pour la communauté Unity

## 🔗 Liens

- 🌐 [Site Web](https://votre-username.github.io/FreeCityAI/)
- 📚 [Documentation](https://votre-username.github.io/FreeCityAI/)
- 🐛 [Issues](https://github.com/votre-username/FreeCityAI/issues)
- 💬 [Discussions](https://github.com/votre-username/FreeCityAI/discussions)

## 📞 Support

Besoin d'aide ?
1. Consultez [START_HERE.md](START_HERE.md)
2. Lisez [QUICKSTART.md](QUICKSTART.md)
3. Voir les [Issues](https://github.com/votre-username/FreeCityAI/issues)
4. Rejoignez les [Discussions](https://github.com/votre-username/FreeCityAI/discussions)

## 📈 Roadmap

### v1.1.0 (Planned)
- [ ] VFX Graph support
- [ ] Système audio intégré
- [ ] Save/Load presets
- [ ] Interactive nodes

### v2.0.0 (Future)
- [ ] Éditeur visuel
- [ ] Multi-circuit support
- [ ] Timeline integration

Voir [VERSION.md](VERSION.md) pour détails complets.

---

<div align="center">

**🎮 Free City AI v1.0.0**

Créé avec ❤️ pour la communauté Unity

[Documentation](https://votre-username.github.io/FreeCityAI/) • [Télécharger](https://github.com/votre-username/FreeCityAI/releases) • [Exemples](EXAMPLES.md)

</div>
