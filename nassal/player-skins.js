async function syncScrollAndAvatar(durationMs = 1000, anchorOffset = 200) {
    var scroller = document.querySelector('.ScrollbarsCustom-Scroller');
    var anchor = document.querySelector('.player-history-previous-events-title');

    var avatarBtns = document.querySelectorAll('.player-history-avatar-nav-btn');
    var avatarDots = document.querySelectorAll('.player-history-avatar-nav-dot');

    var backBtn = avatarBtns[1];
    var nextBtn = avatarBtns[0];

    if (!scroller || !backBtn || !nextBtn || !avatarDots.length) {
        console.warn('Missing required elements');
        return;
    }

    var steps = avatarDots.length;

    // RESET AVATAR
    for (var i = 0; i < steps; i++) {
        backBtn.click();
        // wait for layout to settle
        await new Promise(r => requestAnimationFrame(r));
    }

    var startScroll = 0;
    if (!anchor) {
        startScroll = scroller.scrollHeight - scroller.clientHeight;
    } else {
        var baseScroll = anchor.offsetTop - scroller.offsetTop;
        startScroll = Math.max(0, baseScroll - anchorOffset);
    }
    scroller.scrollTop = startScroll;

    // ANIMATION SETUP
    var startTime = performance.now();
    var lastStep = 0;

    function getTime() {
        var now = new Date();
        var s = String(now.getSeconds()).padStart(2, '0');
        var ms = String(now.getMilliseconds()).padStart(3, '0');
        return `${s}.${ms}`;
    }

    function ease(t) {
        return t < 0.5 ? 2 * t * t : 1 - Math.pow(-2 * t + 2, 2) / 2;
    }

    return new Promise((resolve) => {
        function step(now) {
            var t = Math.min(1, (now - startTime) / durationMs);
            var e = ease(t);

            scroller.scrollTop = startScroll * (1 - e);

            var currentStep = Math.min(steps - 1, Math.floor(t * steps));

            if (currentStep > lastStep) {
                lastStep = currentStep;
                nextBtn.click();
                console.log(`[${getTime()}] click ${lastStep} (at t=${t.toFixed(2)})`);
            }

            if (t < 1) {
                requestAnimationFrame(step);
            } else {
                resolve();
            }
        }
        requestAnimationFrame(step);
    });
}

async function runPlayerProcessor(durationMs, anchorOffset) {
    let playersCount = document.querySelectorAll('.player-row').length;
    console.log(`Processing ${playersCount} players...`);

    for (let i = 0; i < playersCount; i++) {
        // Re-query players because the page might have changed/refreshed
        const players = document.querySelectorAll('.player-row');
        if (!players[i]) {
            console.warn(`Player at index ${i} no longer exists.`);
            continue;
        }

        console.log(`Processing player ${i + 1}/${playersCount}`);
        players[i].click();

        // Wait a bit for the player content to load/transition
        await new Promise(r => setTimeout(r, 500));

        await syncScrollAndAvatar(durationMs, anchorOffset);

        const back = document.querySelector('.title-right');
        back.click();

        // Brief pause between players to allow the list page to load/settle
        await new Promise(r => setTimeout(r, 500));
    }

    console.log('Finished processing all players.');
}

runPlayerProcessor(12_000, 460);

function runScreensaver() {
    const images = [
        "/assets/Ara_1-CkFafYHh.webp", "/assets/Ara_2-BDRrl0qB.webp",
        "/assets/Cake_1-tGRT4L6q.webp", "/assets/Cake_2-B_Y-qeco.webp",
        "/assets/Dund_1-BhP8X1cS.webp", "/assets/Dund_2-zzoJ56ti.webp",
        "/assets/Dund_3-BPdkgaXU.webp", "/assets/Flash_1-DdmM_bKw.webp",
        "/assets/Flash_2-CksyExgh.webp", "/assets/Glad_1-BRXBdiiP.webp",
        "/assets/Glad_2-CCBhjd--.webp", "/assets/Karm_1-WKmhemyN.webp",
        "/assets/Karm_2-By-zY-BM.webp", "/assets/Karm_3-zi9Bvx5O.webp",
        "/assets/Lasqa_1-C5j9xBqu.webp", "/assets/Lasqa_2-L2nCtAfl.webp",
        "/assets/Lasqa_3-Cx2z_VWs.webp", "/assets/Lasqa_4-Dcl6P0yr.webp",
        "/assets/Mad_1-CoaQsIRS.webp", "/assets/Mad_2-m-VnY4bg.webp",
        "/assets/Mad_3-D_tEE39u.webp", "/assets/Mel_1-ClTujBap.webp",
        "/assets/Mel_2-k83W14nW.webp", "/assets/Mel_3-BDhNVuga.webp",
        "/assets/Mel_4-CQtuwBLT.webp", "/assets/Norm_1-C4iqeMaj.webp",
        "/assets/Norm_2-B-AIAiwE.webp", "/assets/Norm_3-C6ELycvo.webp",
        "/assets/Norm_4-2dBfHAAw.webp", "/assets/Prad_1-Dmh9E29X.webp",
        "/assets/Prad_2-CwjUKP7W.webp", "/assets/Prad_3--0za6PY0.webp",
        "/assets/Prad_4-DmZt086d.webp", "/assets/Zu_1-D6mY4El1.webp",
        "/assets/Zu_2-BH_eLQmy.webp", "/assets/Zu_3-CDgYhke-.webp"
    ];

    const HOLD_TIME = 10000;
    const FADE_OUT = 800;

    let currentIndex = 0;
    let ssTimer = null;
    let ssOverlay = null;

    if (document.querySelector('.screensaver')) return;

    ssOverlay = document.createElement('div');
    ssOverlay.className = 'screensaver';
    ssOverlay.style.cssText = `
        position: fixed; top: 0; left: 0; width: 100%; height: 100%;
        z-index: 9999; background: black; pointer-events: all;
        --screensaver-fade-in-ms: 1200ms;
        --screensaver-fade-out-ms: ${FADE_OUT}ms;
    `;

    const close = () => {
        if (!ssOverlay) return;
        clearTimeout(ssTimer);
        ssOverlay.remove();
        ssOverlay = null;
        document.removeEventListener('keydown', close);
        document.removeEventListener('mousemove', close);
    };

    ssOverlay.onclick = close;
    setTimeout(() => {
        document.addEventListener('keydown', close);
        document.addEventListener('mousemove', close);
    }, 1000);

    function showNext() {
        if (!ssOverlay) return;

        const url = images[currentIndex];
        currentIndex = (currentIndex + 1) % images.length;
        const side = currentIndex % 2 === 0 ? 'left' : 'right';

        ssOverlay.innerHTML = `
            <div class="screensaver__stage">
                <div class="screensaver__frame screensaver__frame--${side}">
                    <div class="screensaver__zoom">
                        <img class="screensaver__image" src="${url}" style="opacity: 0; transition: opacity 1.2s">
                    </div>
                </div>
            </div>
        `;

        const img = ssOverlay.querySelector('.screensaver__image');
        requestAnimationFrame(() => { if (img) img.style.opacity = '1'; });

        ssTimer = setTimeout(() => {
            if (img) img.style.opacity = '0';
            ssTimer = setTimeout(showNext, FADE_OUT);
        }, HOLD_TIME);
    }

    document.body.appendChild(ssOverlay);
    showNext();
}

runScreensaver();
