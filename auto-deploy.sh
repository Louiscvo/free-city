#!/bin/bash

# Script de déploiement automatique avec username louischavouet

set -e

USERNAME="louischavouet"

echo "🚀 FREE CITY AI - Déploiement Automatique"
echo ""
echo "✅ Username GitHub: ${USERNAME}"
echo ""

# Mise à jour des liens
echo "📝 Mise à jour des liens..."
sed -i '' "s/votre-username/${USERNAME}/g" index.html 2>/dev/null || sed -i "s/votre-username/${USERNAME}/g" index.html
sed -i '' "s/votre-username/${USERNAME}/g" README_GITHUB.md 2>/dev/null || sed -i "s/votre-username/${USERNAME}/g" README_GITHUB.md

# Configurer README
[ -f "README.md" ] && mv README.md README_LOCAL.md
cp README_GITHUB.md README.md

echo "✅ Liens mis à jour"
echo ""

# Commit
git add .
git commit -m "🚀 Update links for ${USERNAME}" 2>/dev/null || echo "Rien à commiter"

# Configurer remote
if git remote get-url origin &>/dev/null 2>&1; then
    echo "⚠️  Remote existe déjà"
    git remote remove origin
fi

git remote add origin "https://github.com/${USERNAME}/FreeCityAI.git"
echo "✅ Remote configuré"
echo ""

# Instructions finales
echo "═══════════════════════════════════════════════════════════"
echo ""
echo "📋 PROCHAINES ÉTAPES:"
echo ""
echo "1️⃣  Créer le repository sur GitHub:"
echo "   https://github.com/new"
echo "   - Repository name: FreeCityAI"
echo "   - Public"
echo "   - Ne cochez RIEN"
echo "   - Create repository"
echo ""
echo "2️⃣  Pousser le code:"
echo "   git branch -M main"
echo "   git push -u origin main"
echo ""
echo "3️⃣  Activer GitHub Pages:"
echo "   https://github.com/${USERNAME}/FreeCityAI/settings/pages"
echo "   - Source: main branch, / (root)"
echo "   - Save"
echo ""
echo "🌐 Votre site sera sur:"
echo "   https://${USERNAME}.github.io/FreeCityAI/"
echo ""
echo "═══════════════════════════════════════════════════════════"
echo ""
echo "🌐 Ouverture des pages dans le navigateur..."

# Ouvrir les pages
open "https://github.com/new"
sleep 1
open "index.html"

echo ""
echo "✨ Prêt ! Suivez les 3 étapes ci-dessus."
echo ""
