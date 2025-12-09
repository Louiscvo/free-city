# 📦 Guide de Publication sur GitHub

Ce guide explique comment publier votre projet Free City AI sur GitHub avec GitHub Pages.

## 🚀 Étapes de Publication

### 1. Créer un Nouveau Repository sur GitHub

1. Allez sur [github.com](https://github.com)
2. Cliquez sur **"+"** → **"New repository"**
3. Remplissez :
   - **Repository name** : `FreeCityAI`
   - **Description** : `Système d'évolution d'IA inspiré de Free Guy (2021)`
   - **Public** ou **Private** : Choisissez selon vos préférences
   - **Ne cochez PAS** : "Initialize with README" (on a déjà tout)
4. Cliquez **"Create repository"**

### 2. Lier le Repository Local

Dans le terminal, depuis le dossier FreeCityAI :

```bash
# Remplacez "votre-username" par votre nom d'utilisateur GitHub
git remote add origin https://github.com/votre-username/FreeCityAI.git

# Vérifier
git remote -v
```

### 3. Pousser le Code

```bash
# Première fois
git branch -M main
git push -u origin main
```

Entrez vos identifiants GitHub quand demandé.

### 4. Activer GitHub Pages

1. Sur GitHub, allez dans votre repository
2. Cliquez sur **"Settings"** (en haut à droite)
3. Dans le menu de gauche, cliquez **"Pages"**
4. Sous **"Source"**, sélectionnez :
   - Branch : `main`
   - Folder : `/ (root)`
5. Cliquez **"Save"**

**Attendez 1-2 minutes** pour que GitHub Pages se déploie.

### 5. Vérifier le Site

Votre site sera disponible à :
```
https://votre-username.github.io/FreeCityAI/
```

## 🔧 Configuration Finale

### Mettre à Jour les Liens

Dans `index.html` et `README_GITHUB.md`, remplacez :
```
votre-username
```
par votre vrai nom d'utilisateur GitHub.

**Fichiers à modifier** :
- `index.html` (liens de téléchargement)
- `README_GITHUB.md` (tous les liens)

Puis commitez les changements :

```bash
git add index.html README_GITHUB.md
git commit -m "Update GitHub links"
git push
```

### Renommer README_GITHUB.md

Pour que GitHub affiche le bon README :

```bash
# Backup du README local
mv README.md README_LOCAL.md

# Utiliser README_GITHUB.md comme README principal
mv README_GITHUB.md README.md

# Commit
git add README.md README_LOCAL.md
git commit -m "Use GitHub README as main README"
git push
```

## 📝 Structure Finale sur GitHub

```
https://github.com/votre-username/FreeCityAI/
├── Code (tous les scripts et doc)
└── Website (GitHub Pages)
    └── https://votre-username.github.io/FreeCityAI/
```

## ✅ Checklist de Publication

- [ ] Repository créé sur GitHub
- [ ] Remote origin configuré
- [ ] Code poussé (`git push`)
- [ ] GitHub Pages activé dans Settings
- [ ] Site accessible (https://votre-username.github.io/FreeCityAI/)
- [ ] Liens mis à jour dans index.html
- [ ] Liens mis à jour dans README
- [ ] README_GITHUB.md renommé en README.md

## 🎨 Personnalisation du Site

### Modifier les Couleurs

Dans `style.css`, changez les variables :

```css
:root {
    --primary: #00d9ff;     /* Cyan */
    --secondary: #ff006e;   /* Rose */
    --accent: #ffd60a;      /* Jaune */
}
```

### Ajouter une Image/GIF

1. Créez un dossier `assets/` dans votre projet
2. Ajoutez votre image (screenshot.png, demo.gif, etc.)
3. Dans `index.html`, remplacez :

```html
<img src="assets/demo.gif" alt="Demo">
```

4. Commit et push :

```bash
git add assets/
git commit -m "Add demo assets"
git push
```

## 🔗 Ajouter un Badge

Dans votre README, ajoutez :

```markdown
![GitHub Stars](https://img.shields.io/github/stars/votre-username/FreeCityAI?style=social)
![GitHub Forks](https://img.shields.io/github/forks/votre-username/FreeCityAI?style=social)
![GitHub Issues](https://img.shields.io/github/issues/votre-username/FreeCityAI)
```

## 📢 Partager votre Projet

Une fois publié, partagez :

1. **Sur Twitter** :
   ```
   🎮 Je viens de publier Free City AI !

   Système Unity inspiré du film Free Guy pour visualiser
   l'évolution d'IA avec effet d'explosion spectaculaire ✨

   🔗 https://votre-username.github.io/FreeCityAI/

   #Unity3D #GameDev #IndieGame
   ```

2. **Sur Reddit** :
   - r/Unity3D
   - r/gamedev
   - r/IndieDev

3. **Sur Discord** :
   - Serveurs Unity
   - Communautés gamedev

## 🐛 Résolution de Problèmes

### Site ne s'affiche pas ?

1. Vérifiez dans Settings → Pages que la source est bien `main` branch
2. Attendez 2-3 minutes
3. Videz le cache du navigateur (Ctrl+F5)
4. Vérifiez que `index.html` est bien à la racine du projet

### Liens cassés ?

Vérifiez que vous avez bien remplacé `votre-username` par votre vrai username GitHub dans tous les fichiers.

### Erreur de push ?

```bash
# Si vous avez des problèmes d'authentification
git config credential.helper store
git push
```

Ou utilisez un Personal Access Token (PAT) depuis GitHub Settings → Developer settings → Personal access tokens.

## 🔄 Mises à Jour Futures

Pour mettre à jour le site :

```bash
# Faire vos modifications
git add .
git commit -m "Description des changements"
git push
```

Le site sera automatiquement mis à jour en 1-2 minutes.

## 📞 Support

Si vous avez des problèmes :

1. Vérifiez la [documentation GitHub Pages](https://docs.github.com/pages)
2. Consultez les [GitHub Issues](https://github.com/votre-username/FreeCityAI/issues)
3. Rejoignez les discussions

---

**Félicitations ! Votre projet est maintenant en ligne ! 🎉**

Votre site : `https://votre-username.github.io/FreeCityAI/`
