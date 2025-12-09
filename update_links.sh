#!/bin/sh
USERNAME="louischavouet"
sed "s/votre-username/${USERNAME}/g" index.html > index.html.tmp && mv index.html.tmp index.html
sed "s/votre-username/${USERNAME}/g" README_GITHUB.md > README_GITHUB.md.tmp && mv README_GITHUB.md.tmp README_GITHUB.md
[ -f README.md ] && mv README.md README_LOCAL.md
cp README_GITHUB.md README.md
echo "✅ Liens mis à jour pour ${USERNAME}"
