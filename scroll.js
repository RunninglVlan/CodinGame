async function syncScrollAndAvatar(durationMs = 1000, offsetAboveAnchor = 200) {
    console.log(`[${getTime()}] start`);
    var scroller = document.querySelector('.ScrollbarsCustom-Scroller');
    var anchor = document.querySelector('.player-history-previous-events-title');

    var avatarBtns = document.querySelectorAll('.player-history-avatar-nav-btn');
    var avatarDots = document.querySelectorAll('.player-history-avatar-nav-dot');

    var backBtn = avatarBtns[1];
    var nextBtn = avatarBtns[0];

    if (!scroller || !anchor || !backBtn || !nextBtn || !avatarDots.length) {
        console.warn('Missing required elements');
        return;
    }

    var steps = avatarDots.length;

    // RESET AVATAR
    for (var i = 0; i < steps; i++) {
        backBtn.click();
        await new Promise(r => requestAnimationFrame(r));
    }

    // LET LAYOUT SETTLE (ONCE)
    await new Promise(r => requestAnimationFrame(r));

    var baseScroll = anchor.offsetTop - scroller.offsetTop;
    var startScroll = Math.max(0, baseScroll - offsetAboveAnchor);

    scroller.scrollTop = startScroll;

    // ANIMATION SETUP
    var startTime = performance.now();
    // Start at 0 to skip the first click
    var lastStep = 0;

    function ease(t) {
        return t < 0.5
            ? 2 * t * t
            : 1 - Math.pow(-2 * t + 2, 2) / 2;
    }

    function getTime() {
        var now = new Date();
        var s = String(now.getSeconds()).padStart(2, '0');
        var ms = String(now.getMilliseconds()).padStart(3, '0');
        return `${s}.${ms}`;
    }

    function step(now) {
        var t = Math.min(1, (now - startTime) / durationMs);
        var e = ease(t);

        // scroll from anchored position → top
        scroller.scrollTop = startScroll * (1 - e);

        // Map linear t (0 to 1) to step index (0 to steps - 1)
        // Using Math.floor(t * steps) ensures each avatar gets 1/steps of the duration
        var currentStep = Math.min(steps - 1, Math.floor(t * steps));

        if (currentStep > lastStep) {
            lastStep = currentStep;
            nextBtn.click();
            console.log(`[${getTime()}] click ${lastStep} (at t=${t.toFixed(2)})`);
        }

        if (t < 1) {
            requestAnimationFrame(step);
        }
    }

    requestAnimationFrame(step);
}
