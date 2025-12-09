// Free City AI - Interactive Web App
class FreeCityApp {
    constructor() {
        this.canvas = document.getElementById('canvas');
        this.ctx = this.canvas.getContext('2d');
        this.resizeCanvas();

        this.nodes = [];
        this.nodeCount = 8;
        this.lightPosition = 0;
        this.lightSpeed = 1;
        this.ribbonCount = 5;
        this.lightColor = '#00ffff';
        this.isEvolved = false;
        this.ribbons = [];
        this.time = 0;
        this.fps = 0;
        this.lastTime = performance.now();
        this.frameCount = 0;
        this.zoom = 1;
        this.offsetX = 0;
        this.offsetY = 0;

        this.setupEventListeners();
        this.createNodes();
        this.animate();
    }

    resizeCanvas() {
        this.canvas.width = window.innerWidth;
        this.canvas.height = window.innerHeight;
        this.centerX = this.canvas.width / 2;
        this.centerY = this.canvas.height / 2;
    }

    setupEventListeners() {
        window.addEventListener('resize', () => this.resizeCanvas());

        document.getElementById('evolve-btn').addEventListener('click', () => this.evolve());
        document.getElementById('reset-btn').addEventListener('click', () => this.reset());

        document.getElementById('node-slider').addEventListener('input', (e) => {
            this.nodeCount = parseInt(e.target.value);
            document.getElementById('node-count').textContent = this.nodeCount;
            if (!this.isEvolved) this.createNodes();
        });

        document.getElementById('speed-slider').addEventListener('input', (e) => {
            this.lightSpeed = parseFloat(e.target.value);
            document.getElementById('speed-value').textContent = this.lightSpeed.toFixed(1);
        });

        document.getElementById('ribbon-slider').addEventListener('input', (e) => {
            this.ribbonCount = parseInt(e.target.value);
            document.getElementById('ribbon-count').textContent = this.ribbonCount;
        });

        document.getElementById('light-color').addEventListener('input', (e) => {
            this.lightColor = e.target.value;
        });

        // Keyboard shortcuts
        document.addEventListener('keydown', (e) => {
            if (e.key === ' ' || e.key === 'e' || e.key === 'E') {
                e.preventDefault();
                this.evolve();
            }
            if (e.key === 'r' || e.key === 'R') {
                this.reset();
            }
        });
    }

    createNodes() {
        this.nodes = [];
        const radius = Math.min(this.canvas.width, this.canvas.height) * 0.3;

        for (let i = 0; i < this.nodeCount; i++) {
            const angle = (i / this.nodeCount) * Math.PI * 2 - Math.PI / 2;
            const x = this.centerX + Math.cos(angle) * radius;
            const y = this.centerY + Math.sin(angle) * radius;

            this.nodes.push({
                x, y,
                angle,
                radius: 20,
                color: '#00d9ff',
                label: this.getNodeLabel(i)
            });
        }

        document.getElementById('nodes-stat').textContent = this.nodeCount;
    }

    getNodeLabel(index) {
        const labels = ['SLEEP', 'WAKE UP', 'WORK', 'LUNCH', 'MEET', 'COFFEE', 'HOME', 'RELAX',
                       'HOBBY', 'DINNER', 'WALK', 'READ', 'PLAY', 'STUDY', 'TRAIN', 'REST'];
        return labels[index % labels.length];
    }

    evolve() {
        if (this.isEvolved) return;
        this.isEvolved = true;
        document.getElementById('state').textContent = 'ÉVOLUTION !';

        // Create ribbons only from the rightmost node
        const rightmostNode = this.nodes.reduce((prev, current) => {
            return (current.x > prev.x) ? current : prev;
        });

        for (let i = 0; i < this.ribbonCount; i++) {
            setTimeout(() => {
                this.createRibbon(rightmostNode, i);
            }, i * 50);
        }
    }

    createRibbon(node, index, depth = 0, yOffset = 0) {
        const colors = ['#ff006e', '#ffd60a', '#00d9ff', '#ff5733', '#a855f7'];

        // Arborescence: alterne entre ligne droite et courbe
        const verticalOffset = yOffset || (index - this.ribbonCount / 2) * 40;

        this.ribbons.push({
            startX: node.x,
            startY: node.y,
            currentY: node.y,
            targetY: node.y + verticalOffset,
            angle: 0,
            speed: 3,
            color: colors[depth % colors.length],
            points: [{x: node.x, y: node.y}],
            width: 3 - (depth * 0.5), // Plus fin à chaque subdivision
            life: 999, // Ne disparaît jamais
            phase: 'curve',
            distance: 0,
            depth: depth,
            branchTimer: 100 + Math.random() * 100, // Quand se subdiviser
            hasBranched: false
        });
    }

    reset() {
        this.isEvolved = false;
        this.ribbons = [];
        this.lightPosition = 0;
        this.zoom = 1;
        this.offsetX = 0;
        this.offsetY = 0;
        this.createNodes();
        document.getElementById('state').textContent = 'Boucle normale';
    }

    drawNodes() {
        this.nodes.forEach((node, i) => {
            // Connection line to next node
            const nextNode = this.nodes[(i + 1) % this.nodes.length];
            this.ctx.strokeStyle = 'rgba(0, 217, 255, 0.3)';
            this.ctx.lineWidth = 2;
            this.ctx.beginPath();
            this.ctx.moveTo(node.x, node.y);
            this.ctx.lineTo(nextNode.x, nextNode.y);
            this.ctx.stroke();

            // Node circle
            this.ctx.fillStyle = node.color;
            this.ctx.beginPath();
            this.ctx.arc(node.x, node.y, node.radius, 0, Math.PI * 2);
            this.ctx.fill();

            // Node label
            this.ctx.fillStyle = '#0a0e27';
            this.ctx.font = 'bold 10px Arial';
            this.ctx.textAlign = 'center';
            this.ctx.textBaseline = 'middle';
            this.ctx.fillText(node.label, node.x, node.y);
        });
    }

    drawLight() {
        if (this.isEvolved) return;

        const currentIndex = Math.floor(this.lightPosition);
        const nextIndex = (currentIndex + 1) % this.nodes.length;
        const progress = this.lightPosition - currentIndex;

        const current = this.nodes[currentIndex];
        const next = this.nodes[nextIndex];

        const x = current.x + (next.x - current.x) * progress;
        const y = current.y + (next.y - current.y) * progress;

        // Glow
        const gradient = this.ctx.createRadialGradient(x, y, 0, x, y, 30);
        gradient.addColorStop(0, this.lightColor);
        gradient.addColorStop(1, 'rgba(0, 217, 255, 0)');
        this.ctx.fillStyle = gradient;
        this.ctx.beginPath();
        this.ctx.arc(x, y, 30, 0, Math.PI * 2);
        this.ctx.fill();

        // Core
        this.ctx.fillStyle = this.lightColor;
        this.ctx.beginPath();
        this.ctx.arc(x, y, 8, 0, Math.PI * 2);
        this.ctx.fill();

        document.getElementById('position').textContent = `${currentIndex + 1}/${this.nodeCount}`;
    }

    drawRibbons() {
        this.ribbons.forEach(ribbon => {
            const lastPoint = ribbon.points[ribbon.points.length - 1];
            ribbon.distance += ribbon.speed;
            ribbon.branchTimer--;

            let newX, newY;

            if (ribbon.phase === 'curve') {
                // Phase courbe: aller vers targetY avec une courbe
                const progress = Math.min(ribbon.distance / 100, 1);
                const curveY = ribbon.startY + (ribbon.targetY - ribbon.startY) * this.easeInOutCubic(progress);

                newX = lastPoint.x + ribbon.speed;
                newY = curveY;
                ribbon.currentY = curveY;

                if (progress >= 1) {
                    ribbon.phase = 'straight';
                    ribbon.distance = 0;
                }
            } else {
                // Phase droite: ligne droite horizontale
                newX = lastPoint.x + ribbon.speed;
                newY = ribbon.targetY;
                ribbon.currentY = ribbon.targetY;

                // Créer des branches (subdivisions)
                if (ribbon.branchTimer <= 0 && !ribbon.hasBranched && ribbon.depth < 3) {
                    ribbon.hasBranched = true;

                    // Créer 2-3 nouvelles branches
                    const numBranches = 2 + Math.floor(Math.random() * 2);
                    for (let i = 0; i < numBranches; i++) {
                        const branchOffset = (i - numBranches / 2) * 60;
                        const branchNode = {
                            x: lastPoint.x,
                            y: ribbon.currentY
                        };
                        this.createRibbon(branchNode, i, ribbon.depth + 1, branchOffset);
                    }
                }
            }

            ribbon.points.push({x: newX, y: newY});

            // Draw ribbon (tous les points, pas de limite)
            if (ribbon.points.length > 1) {
                this.ctx.strokeStyle = ribbon.color;
                this.ctx.lineWidth = Math.max(ribbon.width, 1);
                this.ctx.globalAlpha = 0.8;
                this.ctx.lineCap = 'round';
                this.ctx.lineJoin = 'round';

                this.ctx.beginPath();
                this.ctx.moveTo(ribbon.points[0].x, ribbon.points[0].y);

                for (let i = 1; i < ribbon.points.length; i++) {
                    this.ctx.lineTo(ribbon.points[i].x, ribbon.points[i].y);
                }

                this.ctx.stroke();
                this.ctx.globalAlpha = 1;
            }
        });

        document.getElementById('ribbons').textContent = this.ribbons.length;
    }

    easeInOutCubic(t) {
        return t < 0.5 ? 4 * t * t * t : 1 - Math.pow(-2 * t + 2, 3) / 2;
    }

    updateFPS() {
        this.frameCount++;
        const now = performance.now();
        const delta = now - this.lastTime;

        if (delta >= 1000) {
            this.fps = Math.round((this.frameCount * 1000) / delta);
            document.getElementById('fps').textContent = this.fps;
            this.frameCount = 0;
            this.lastTime = now;
        }
    }

    animate() {
        this.ctx.clearRect(0, 0, this.canvas.width, this.canvas.height);

        // Dezoom progressif quand l'arborescence grandit
        if (this.isEvolved && this.ribbons.length > 0) {
            const maxX = Math.max(...this.ribbons.flatMap(r => r.points.map(p => p.x)));
            const maxY = Math.max(...this.ribbons.flatMap(r => r.points.map(p => p.y)));
            const minY = Math.min(...this.ribbons.flatMap(r => r.points.map(p => p.y)));

            const targetZoom = Math.min(
                this.canvas.width / (maxX - this.centerX + 200),
                this.canvas.height / (maxY - minY + 200),
                1
            );
            this.zoom += (targetZoom - this.zoom) * 0.02;

            this.offsetX += ((this.canvas.width / 2 - maxX * this.zoom) - this.offsetX) * 0.02;
            this.offsetY += ((this.canvas.height / 2 - (maxY + minY) / 2 * this.zoom) - this.offsetY) * 0.02;
        }

        // Appliquer la transformation
        this.ctx.save();
        this.ctx.translate(this.offsetX, this.offsetY);
        this.ctx.scale(this.zoom, this.zoom);

        this.drawNodes();
        this.drawLight();
        this.drawRibbons();

        this.ctx.restore();

        // Update light position
        if (!this.isEvolved) {
            this.lightPosition += this.lightSpeed * 0.01;
            if (this.lightPosition >= this.nodeCount) {
                this.lightPosition = 0;
            }
        }

        this.time++;
        this.updateFPS();

        requestAnimationFrame(() => this.animate());
    }
}

// Initialize app
const app = new FreeCityApp();
