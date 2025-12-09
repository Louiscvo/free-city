// Smooth scrolling
document.querySelectorAll('a[href^="#"]').forEach(anchor => {
    anchor.addEventListener('click', function (e) {
        e.preventDefault();
        const target = document.querySelector(this.getAttribute('href'));
        if (target) {
            target.scrollIntoView({
                behavior: 'smooth',
                block: 'start'
            });
        }
    });
});

// Fade in on scroll
const observerOptions = {
    threshold: 0.1,
    rootMargin: '0px 0px -50px 0px'
};

const observer = new IntersectionObserver((entries) => {
    entries.forEach(entry => {
        if (entry.isIntersecting) {
            entry.target.classList.add('fade-in');
        }
    });
}, observerOptions);

// Observe all sections
document.querySelectorAll('section').forEach(section => {
    observer.observe(section);
});

// Stats counter animation
function animateCounter(element, target, duration = 2000) {
    const start = 0;
    const increment = target / (duration / 16);
    let current = start;

    const timer = setInterval(() => {
        current += increment;
        if (current >= target) {
            element.textContent = target + (element.dataset.suffix || '');
            clearInterval(timer);
        } else {
            element.textContent = Math.floor(current) + (element.dataset.suffix || '');
        }
    }, 16);
}

// Trigger counter animation when stats section is visible
const statsObserver = new IntersectionObserver((entries) => {
    entries.forEach(entry => {
        if (entry.isIntersecting) {
            const numbers = entry.target.querySelectorAll('.stat-number');
            numbers.forEach(num => {
                const target = parseInt(num.textContent);
                if (!isNaN(target)) {
                    num.dataset.suffix = num.textContent.replace(/[0-9]/g, '');
                    animateCounter(num, target);
                }
            });
            statsObserver.unobserve(entry.target);
        }
    });
}, { threshold: 0.5 });

const statsSection = document.querySelector('.stats');
if (statsSection) {
    statsObserver.observe(statsSection);
}

// Add hover effect to features
document.querySelectorAll('.feature').forEach(feature => {
    feature.addEventListener('mouseenter', function() {
        this.style.transform = 'translateY(-10px) scale(1.02)';
    });

    feature.addEventListener('mouseleave', function() {
        this.style.transform = 'translateY(0) scale(1)';
    });
});

// Parallax effect for hero
window.addEventListener('scroll', () => {
    const scrolled = window.pageYOffset;
    const hero = document.querySelector('.hero');
    if (hero) {
        hero.style.transform = `translateY(${scrolled * 0.5}px)`;
        hero.style.opacity = 1 - (scrolled / 700);
    }
});

// Dynamic button glow
document.querySelectorAll('.btn').forEach(btn => {
    btn.addEventListener('mousemove', (e) => {
        const rect = btn.getBoundingClientRect();
        const x = e.clientX - rect.left;
        const y = e.clientY - rect.top;

        btn.style.setProperty('--mouse-x', `${x}px`);
        btn.style.setProperty('--mouse-y', `${y}px`);
    });
});

// Console easter egg
console.log(`
╔════════════════════════════════════════════════════════════╗
║                                                            ║
║     🎮 FREE CITY AI - Developer Console 🎮                 ║
║                                                            ║
║     Bienvenue dans le système d'évolution d'IA !          ║
║     Inspiré par Free Guy (2021)                           ║
║                                                            ║
║     GitHub: https://github.com/votre-username/FreeCityAI  ║
║                                                            ║
╚════════════════════════════════════════════════════════════╝
`);

// Add loading state to download buttons
document.querySelectorAll('.btn').forEach(btn => {
    if (btn.href && btn.href.includes('.zip')) {
        btn.addEventListener('click', function() {
            const originalText = this.innerHTML;
            this.innerHTML = '<span class="icon">⏳</span> Téléchargement...';
            this.style.pointerEvents = 'none';

            setTimeout(() => {
                this.innerHTML = originalText;
                this.style.pointerEvents = 'auto';
            }, 3000);
        });
    }
});

// Keyboard shortcuts
document.addEventListener('keydown', (e) => {
    // Press 'D' to go to download section
    if (e.key === 'd' || e.key === 'D') {
        if (!e.ctrlKey && !e.metaKey) {
            const downloadSection = document.querySelector('.download');
            if (downloadSection) {
                downloadSection.scrollIntoView({ behavior: 'smooth' });
            }
        }
    }

    // Press 'H' to go back to hero
    if (e.key === 'h' || e.key === 'H') {
        if (!e.ctrlKey && !e.metaKey) {
            window.scrollTo({ top: 0, behavior: 'smooth' });
        }
    }
});

// Show keyboard shortcuts hint
setTimeout(() => {
    console.log('💡 Keyboard shortcuts:');
    console.log('   H - Retour au début');
    console.log('   D - Aller au téléchargement');
}, 1000);
