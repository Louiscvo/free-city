#!/bin/bash

# Script de déploiement ULTRA-RAPIDE
# Ce script ouvre automatiquement toutes les pages nécessaires

set -e

echo "🚀 FREE CITY AI - Déploiement Ultra-Rapide"
echo ""

# Demander le username
read -p "📝 Votre username GitHub: " USERNAME

if [ -z "$USERNAME" ]; then
    echo "❌ Username requis!"
    exit 1
fi

echo ""
echo "✅ Username: $USERNAME"
echo ""
echo "🔧 Mise à jour des fichiers..."

# Mettre à jour les liens
sed -i.bak "s/votre-username/${USERNAME}/g" index.html
sed -i.bak "s/votre-username/${USERNAME}/g" README_GITHUB.md
rm -f *.bak

# Configurer README
[ -f "README.md" ] && mv README.md README_LOCAL.md
cp README_GITHUB.md README.md

# Commit
git add .
git commit -m "🚀 Prepare for deployment - Update links for ${USERNAME}" || true

# Configurer remote
if git remote get-url origin &>/dev/null; then
    git remote remove origin
fi
git remote add origin "https://github.com/${USERNAME}/FreeCityAI.git"

echo ""
echo "✅ Fichiers préparés!"
echo ""
echo "📋 PROCHAINES ÉTAPES:"
echo ""
echo "1️⃣  Créer le repository sur GitHub"
echo "2️⃣  Pousser le code"
echo "3️⃣  Activer GitHub Pages"
echo ""
echo "🌐 J'ouvre les pages nécessaires dans votre navigateur..."
sleep 2

# Ouvrir les pages dans le navigateur
open "https://github.com/new"
sleep 1
open "index.html"

echo ""
echo "✨ Pages ouvertes dans le navigateur !"
echo ""
echo "📝 Dans la page GitHub 'Create repository':"
echo "   - Repository name: FreeCityAI"
echo "   - Public"
echo "   - Ne cochez RIEN"
echo "   - Create repository"
echo ""
echo "⌨️  Ensuite, exécutez ces commandes:"
echo ""
echo "   git branch -M main"
echo "   git push -u origin main"
echo ""
echo "🎯 Puis activez GitHub Pages:"
echo "   Settings → Pages → Source: main, / (root) → Save"
echo ""
echo "🌐 Votre site sera sur:"
echo "   https://${USERNAME}.github.io/FreeCityAI/"
echo ""
