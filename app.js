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

        // Create ribbons from each node
        this.nodes.forEach((node, nodeIndex) => {
            for (let i = 0; i < this.ribbonCount; i++) {
                setTimeout(() => {
                    this.createRibbon(node, i);
                }, nodeIndex * 100 + i * 50);
            }
        });
    }

    createRibbon(node, index) {
        const colors = ['#ff006e', '#ffd60a', '#00d9ff', '#ff5733', '#a855f7'];
        // Explosion radiale complète - 360 degrés
        const angle = Math.random() * Math.PI * 2; // Direction aléatoire complète

        this.ribbons.push({
            startX: node.x,
            startY: node.y,
            angle: angle,
            speed: 2 + Math.random() * 2,
            color: colors[index % colors.length],
            points: [{x: node.x, y: node.y}],
            maxPoints: 50,
            width: 3,
            life: 1,
            wave: Math.random() * Math.PI * 2
        });
    }

    reset() {
        this.isEvolved = false;
        this.ribbons = [];
        this.lightPosition = 0;
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
        this.ribbons = this.ribbons.filter(ribbon => ribbon.life > 0);

        this.ribbons.forEach(ribbon => {
            const lastPoint = ribbon.points[ribbon.points.length - 1];
            const waveOffset = Math.sin(this.time * 0.05 + ribbon.wave) * 20;
            const perpX = -Math.sin(ribbon.angle);
            const perpY = Math.cos(ribbon.angle);

            const newX = lastPoint.x + Math.cos(ribbon.angle) * ribbon.speed + perpX * waveOffset;
            const newY = lastPoint.y + Math.sin(ribbon.angle) * ribbon.speed + perpY * waveOffset;

            ribbon.points.push({x: newX, y: newY});

            if (ribbon.points.length > ribbon.maxPoints) {
                ribbon.points.shift();
            }

            ribbon.life -= 0.005;

            // Draw ribbon
            if (ribbon.points.length > 1) {
                this.ctx.strokeStyle = ribbon.color;
                this.ctx.lineWidth = ribbon.width;
                this.ctx.globalAlpha = ribbon.life;
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

        this.drawNodes();
        this.drawLight();
        this.drawRibbons();

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
