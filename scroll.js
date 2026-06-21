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

        const secondaryClickables = document.querySelectorAll('.title-clickable');
        if (secondaryClickables.length > 1) {
            secondaryClickables[1].click();
        }

        // Brief pause between players to allow the list page to load/settle
        await new Promise(r => setTimeout(r, 500));
    }

    console.log('Finished processing all players.');
}

runPlayerProcessor(12_000, 460);
