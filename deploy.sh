#!/bin/bash

# Script de déploiement automatique pour Free City AI sur GitHub
# Usage: ./deploy.sh VOTRE-USERNAME

set -e

# Colors
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
CYAN='\033[0;36m'
NC='\033[0m' # No Color

echo -e "${CYAN}"
cat << "EOF"
╔════════════════════════════════════════════════════════════╗
║                                                            ║
║         🚀 FREE CITY AI - DÉPLOIEMENT GITHUB 🚀            ║
║                                                            ║
╚════════════════════════════════════════════════════════════╝
EOF
echo -e "${NC}"

# Vérifier l'argument username
if [ -z "$1" ]; then
    echo -e "${RED}❌ Erreur: Username GitHub requis${NC}"
    echo -e "${YELLOW}Usage: ./deploy.sh VOTRE-USERNAME${NC}"
    exit 1
fi

USERNAME=$1
echo -e "${GREEN}✓ Username GitHub: ${USERNAME}${NC}\n"

# 1. Mettre à jour les liens dans les fichiers
echo -e "${CYAN}📝 Étape 1/5: Mise à jour des liens...${NC}"

# Backup
cp index.html index.html.backup
cp README_GITHUB.md README_GITHUB.md.backup

# Remplacer votre-username
sed -i.bak "s/votre-username/${USERNAME}/g" index.html
sed -i.bak "s/votre-username/${USERNAME}/g" README_GITHUB.md

# Nettoyer les backups sed
rm -f *.bak

echo -e "${GREEN}✓ Liens mis à jour${NC}\n"

# 2. Utiliser README_GITHUB.md comme README principal
echo -e "${CYAN}📝 Étape 2/5: Configuration du README...${NC}"

if [ -f "README.md" ]; then
    mv README.md README_LOCAL.md
fi
cp README_GITHUB.md README.md

echo -e "${GREEN}✓ README configuré${NC}\n"

# 3. Commit des changements
echo -e "${CYAN}💾 Étape 3/5: Commit des changements...${NC}"

git add .
git commit -m "🚀 Update links and prepare for deployment" || echo "Nothing to commit"

echo -e "${GREEN}✓ Changements commités${NC}\n"

# 4. Configurer remote et pousser
echo -e "${CYAN}🔗 Étape 4/5: Configuration du remote GitHub...${NC}"

# Vérifier si remote existe déjà
if git remote get-url origin &>/dev/null; then
    echo -e "${YELLOW}⚠️  Remote 'origin' existe déjà${NC}"
    echo -e "${YELLOW}URL actuelle: $(git remote get-url origin)${NC}"
    read -p "Voulez-vous le remplacer? (y/n) " -n 1 -r
    echo
    if [[ $REPLY =~ ^[Yy]$ ]]; then
        git remote remove origin
        git remote add origin "https://github.com/${USERNAME}/FreeCityAI.git"
        echo -e "${GREEN}✓ Remote mis à jour${NC}"
    else
        echo -e "${YELLOW}⚠️  Remote non modifié${NC}"
    fi
else
    git remote add origin "https://github.com/${USERNAME}/FreeCityAI.git"
    echo -e "${GREEN}✓ Remote ajouté${NC}"
fi

echo ""

# 5. Push vers GitHub
echo -e "${CYAN}🚀 Étape 5/5: Push vers GitHub...${NC}"
echo -e "${YELLOW}⚠️  Assurez-vous d'avoir créé le repository sur GitHub !${NC}"
echo -e "${YELLOW}   https://github.com/new${NC}\n"

read -p "Repository créé sur GitHub? (y/n) " -n 1 -r
echo

if [[ $REPLY =~ ^[Yy]$ ]]; then
    echo -e "${CYAN}🔐 Authentification GitHub requise...${NC}"

    git branch -M main
    git push -u origin main

    if [ $? -eq 0 ]; then
        echo -e "\n${GREEN}✓ Code poussé avec succès !${NC}\n"

        # Instructions pour GitHub Pages
        echo -e "${CYAN}╔════════════════════════════════════════════════════════════╗${NC}"
        echo -e "${CYAN}║                                                            ║${NC}"
        echo -e "${CYAN}║  📋 PROCHAINE ÉTAPE: Activer GitHub Pages                  ║${NC}"
        echo -e "${CYAN}║                                                            ║${NC}"
        echo -e "${CYAN}╚════════════════════════════════════════════════════════════╝${NC}\n"

        echo -e "1. Allez sur: ${GREEN}https://github.com/${USERNAME}/FreeCityAI/settings/pages${NC}"
        echo -e "2. Source: ${YELLOW}main${NC} branch, ${YELLOW}/ (root)${NC}"
        echo -e "3. Cliquez ${GREEN}Save${NC}"
        echo -e "4. Attendez 1-2 minutes\n"

        echo -e "🌐 Votre site sera disponible sur:"
        echo -e "${GREEN}https://${USERNAME}.github.io/FreeCityAI/${NC}\n"

        # Ouvrir dans le navigateur
        echo -e "${CYAN}Voulez-vous ouvrir le repository dans votre navigateur? (y/n)${NC}"
        read -p "" -n 1 -r
        echo

        if [[ $REPLY =~ ^[Yy]$ ]]; then
            open "https://github.com/${USERNAME}/FreeCityAI"
        fi

    else
        echo -e "\n${RED}❌ Erreur lors du push${NC}"
        echo -e "${YELLOW}Vérifiez vos identifiants GitHub${NC}"
        exit 1
    fi
else
    echo -e "\n${YELLOW}⚠️  Créez d'abord le repository sur GitHub:${NC}"
    echo -e "${GREEN}https://github.com/new${NC}"
    echo -e "\nPuis relancez ce script.\n"
    exit 0
fi

# Résumé final
echo -e "${GREEN}╔════════════════════════════════════════════════════════════╗${NC}"
echo -e "${GREEN}║                                                            ║${NC}"
echo -e "${GREEN}║              ✨ DÉPLOIEMENT TERMINÉ ! ✨                    ║${NC}"
echo -e "${GREEN}║                                                            ║${NC}"
echo -e "${GREEN}╚════════════════════════════════════════════════════════════╝${NC}\n"

echo -e "📦 Repository: ${GREEN}https://github.com/${USERNAME}/FreeCityAI${NC}"
echo -e "🌐 Website:    ${GREEN}https://${USERNAME}.github.io/FreeCityAI/${NC}"
echo -e "📚 Docs:       ${GREEN}https://${USERNAME}.github.io/FreeCityAI/START_HERE.md${NC}\n"

echo -e "${CYAN}N'oubliez pas d'activer GitHub Pages dans Settings !${NC}\n"
