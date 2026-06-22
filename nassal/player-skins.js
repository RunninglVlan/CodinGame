async function run(displayTime) {
    hideVideoButtons();

    // hideResults
    doOnRender(() => {
        const results = document.querySelector('.laptop-result-button');
        if (results) {
            results.style.display = 'none';
            return true;
        }
        return false;
    }, 5000)

    await doOnRenderAsync(async () => {
        var statistics = document.querySelector('.members-list-btn');
        if (statistics) {
            statistics.click();

            await waitAfterClick(2000);
            await showPlayerSkins(displayTime, 460);
            showScreensaverSkins(displayTime);
            return true;
        }
        return false;
    }, 5000)
}

function hideVideoButtons() {
    var videoButtons = document.querySelector('#detach-button-host').shadowRoot.querySelector('.underwave');
    videoButtons.style.display = 'none';
}

async function doOnRenderAsync(f, waitMs = 2000) {
    if (await f()) return;

    return new Promise((resolve) => {
        const observer = new MutationObserver(async () => {
            if (await f()) {
                observer.disconnect();
                resolve();
            }
        });
        observer.observe(document.body, {childList: true, subtree: true});
        // Safety timeout to stop observer if it never appears
        setTimeout(() => {
            observer.disconnect();
            resolve();
        }, waitMs);
    });
}

function doOnRender(f, waitMs = 2000) {
    if (!f()) {
        const observer = new MutationObserver(() => {
            if (f()) observer.disconnect();
        });
        observer.observe(document.body, {childList: true, subtree: true});
        // Safety timeout to stop observer if it never appears
        setTimeout(() => observer.disconnect(), waitMs);
    }
}

async function waitAfterClick(waitMs = 500) {
    await new Promise(r => setTimeout(r, waitMs));
}

async function showPlayerSkins(displayTime, anchorOffset) {
    const avatars = [
        '/assets/FLASH_v2-CH9tn0y7.webp',
        '/assets/DUND_v2-BjJZSVOU.webp',
        '/assets/MEL_v2-Bk6vvp3N.webp',
        '/assets/KARM_v2-Ds90Gkdq.webp',
        '/assets/LASQA_v2-DnoodyAh.webp',
        '/assets/PRAD_v2-B46QNEEZ.webp',
        '/assets/MAD_v2-CW0lC5s0.webp',
        '/assets/CAKE_v2-CCS8umQY.webp',
        '/assets/Zu_v2-B8RkTGuj.webp',
        '/assets/GLAD_v2-CXuF-H8c.webp',
        '/assets/ARA_v2-TwhOKf9-.webp',
        '/assets/NORM_v3-I9eMrLXJ.webp'
    ];
    let playersCount = avatars.length;
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

        // resetAvatar
        await doOnRenderAsync(() => {
            const avatarImg = document.querySelector('.player-history-avatar');
            if (avatarImg) {
                avatarImg.src = avatars[i];
                avatarImg.style.visibility = 'hidden';
                return true;
            }
            return false;
        })

        await waitAfterClick();

        await syncScrollAndAvatar(displayTime, anchorOffset);

        const back = document.querySelector('.title-right');
        back.click();
        await waitAfterClick();
    }

    console.log('Finished processing all players.');
}

async function syncScrollAndAvatar(displayTime = 2000, anchorOffset = 200) {
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
    var durationMs = displayTime * steps;
    // RESET AVATAR
    for (var i = 0; i < steps; i++) {
        backBtn.click();
        // wait for layout to settle
        await new Promise(r => requestAnimationFrame(r));
    }
    var avatarImg = document.querySelector('.player-history-avatar');
    avatarImg.style.visibility = 'visible';

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

function showScreensaverSkins(displayTime = 2000) {
    const images = [
        "/assets/Ara_1-CkFafYHh.webp",
        "/assets/Ara_2-BDRrl0qB.webp",
        "/assets/Cake_1-tGRT4L6q.webp",
        "/assets/Cake_2-B_Y-qeco.webp",
        "/assets/Dund_1-BhP8X1cS.webp",
        "/assets/Dund_2-zzoJ56ti.webp",
        "/assets/Dund_3-BPdkgaXU.webp",
        "/assets/Flash_1-DdmM_bKw.webp",
        "/assets/Flash_2-CksyExgh.webp",
        "/assets/Glad_1-BRXBdiiP.webp",
        "/assets/Glad_2-CCBhjd--.webp",
        "/assets/Karm_1-WKmhemyN.webp",
        "/assets/Karm_2-By-zY-BM.webp",
        "/assets/Karm_3-zi9Bvx5O.webp",
        "/assets/Lasqa_1-C5j9xBqu.webp",
        "/assets/Lasqa_2-L2nCtAfl.webp",
        "/assets/Lasqa_3-Cx2z_VWs.webp",
        "/assets/Lasqa_4-Dcl6P0yr.webp",
        "/assets/Mad_1-CoaQsIRS.webp",
        "/assets/Mad_2-m-VnY4bg.webp",
        "/assets/Mad_3-D_tEE39u.webp",
        "/assets/Mel_1-ClTujBap.webp",
        "/assets/Mel_2-k83W14nW.webp",
        "/assets/Mel_3-BDhNVuga.webp",
        "/assets/Mel_4-CQtuwBLT.webp",
        "/assets/Norm_1-C4iqeMaj.webp",
        "/assets/Norm_2-B-AIAiwE.webp",
        "/assets/Norm_3-C6ELycvo.webp",
        "/assets/Norm_4-2dBfHAAw.webp",
        "/assets/Prad_1-Dmh9E29X.webp",
        "/assets/Prad_2-CwjUKP7W.webp",
        "/assets/Prad_3--0za6PY0.webp",
        "/assets/Prad_4-DmZt086d.webp",
        "/assets/Zu_1-D6mY4El1.webp",
        "/assets/Zu_2-BH_eLQmy.webp",
        "/assets/Zu_3-CDgYhke-.webp"
    ];

    // displayTime is the "visible" time (HOLD_TIME).
    // Fades are kept consistent with the site (1.2s and 0.8s) unless displayTime is very short.
    const FADE_IN = 1200;
    const FADE_OUT = 800;
    const HOLD_TIME = displayTime;

    let ssCurrentIndex = 0;
    let ssTimer = null;
    let ssOverlay = null;

    if (document.querySelector('.screensaver')) return;

    ssOverlay = document.createElement('div');
    ssOverlay.className = 'screensaver';
    ssOverlay.style.cssText = `
        position: fixed; top: 0; left: 0; width: 100%; height: 100%;
        z-index: 9999; pointer-events: all;
        --screensaver-fade-in-ms: ${FADE_IN}ms;
        --screensaver-fade-out-ms: ${FADE_OUT}ms;
    `;

    // Initialize the static structure once
    ssOverlay.innerHTML = `
        <div class="screensaver__stage"></div>
        <div class="screensaver__brand">
            <div class="site-brand-container">
                <img alt="Nassal" class="site-brand-image" src="/assets/nassal-Cc58TuAC.webp">
            </div>
        </div>
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

        const url = images[ssCurrentIndex];
        ssCurrentIndex = (ssCurrentIndex + 1) % images.length;
        const side = ssCurrentIndex % 2 === 0 ? 'left' : 'right';

        const stage = ssOverlay.querySelector('.screensaver__stage');
        stage.innerHTML = `
            <div class="screensaver__frame screensaver__frame--${side}">
                <div class="screensaver__zoom">
                    <img class="screensaver__image" src="${url}" style="opacity: 0; transition: opacity ${FADE_IN}ms">
                </div>
            </div>
        `;

        const img = stage.querySelector('.screensaver__image');
        requestAnimationFrame(() => { if (img) img.style.opacity = 1; });

        ssTimer = setTimeout(() => {
            if (img) img.style.opacity = 0;
            ssTimer = setTimeout(showNext, FADE_OUT);
        }, HOLD_TIME);
    }

    document.body.appendChild(ssOverlay);
    showNext();
}

var start = document.querySelector('.intro-start-hitbox');
start.onclick = () => run(2000);
