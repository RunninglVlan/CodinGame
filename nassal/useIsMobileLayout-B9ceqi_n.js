import {ct as e, lt as t, ot as n, p as r, st as i, ut as a} from "./gamepadScope-Bi-UfPAi.js";

var o = e((e => {
    var t = i();

    function n(e) {
        var t = `https://react.dev/errors/` + e;
        if (1 < arguments.length) {
            t += `?args[]=` + encodeURIComponent(arguments[1]);
            for (var n = 2; n < arguments.length; n++) t += `&args[]=` + encodeURIComponent(arguments[n])
        }
        return `Minified React error #` + e + `; visit ` + t + ` for the full message or use the non-minified dev environment for full errors and additional helpful warnings.`
    }

    function r() {
    }

    var a = {
        d: {
            f: r, r: function () {
                throw Error(n(522))
            }, D: r, C: r, L: r, m: r, X: r, S: r, M: r
        }, p: 0, findDOMNode: null
    }, o = Symbol.for(`react.portal`);

    function s(e, t, n) {
        var r = 3 < arguments.length && arguments[3] !== void 0 ? arguments[3] : null;
        return {$$typeof: o, key: r == null ? null : `` + r, children: e, containerInfo: t, implementation: n}
    }

    var c = t.__CLIENT_INTERNALS_DO_NOT_USE_OR_WARN_USERS_THEY_CANNOT_UPGRADE;

    function l(e, t) {
        if (e === `font`) return ``;
        if (typeof t == `string`) return t === `use-credentials` ? t : ``
    }

    e.__DOM_INTERNALS_DO_NOT_USE_OR_WARN_USERS_THEY_CANNOT_UPGRADE = a, e.createPortal = function (e, t) {
        var r = 2 < arguments.length && arguments[2] !== void 0 ? arguments[2] : null;
        if (!t || t.nodeType !== 1 && t.nodeType !== 9 && t.nodeType !== 11) throw Error(n(299));
        return s(e, t, null, r)
    }, e.flushSync = function (e) {
        var t = c.T, n = a.p;
        try {
            if (c.T = null, a.p = 2, e) return e()
        } finally {
            c.T = t, a.p = n, a.d.f()
        }
    }, e.preconnect = function (e, t) {
        typeof e == `string` && (t ? (t = t.crossOrigin, t = typeof t == `string` ? t === `use-credentials` ? t : `` : void 0) : t = null, a.d.C(e, t))
    }, e.prefetchDNS = function (e) {
        typeof e == `string` && a.d.D(e)
    }, e.preinit = function (e, t) {
        if (typeof e == `string` && t && typeof t.as == `string`) {
            var n = t.as, r = l(n, t.crossOrigin), i = typeof t.integrity == `string` ? t.integrity : void 0,
                o = typeof t.fetchPriority == `string` ? t.fetchPriority : void 0;
            n === `style` ? a.d.S(e, typeof t.precedence == `string` ? t.precedence : void 0, {
                crossOrigin: r,
                integrity: i,
                fetchPriority: o
            }) : n === `script` && a.d.X(e, {
                crossOrigin: r,
                integrity: i,
                fetchPriority: o,
                nonce: typeof t.nonce == `string` ? t.nonce : void 0
            })
        }
    }, e.preinitModule = function (e, t) {
        if (typeof e == `string`) if (typeof t == `object` && t) {
            if (t.as == null || t.as === `script`) {
                var n = l(t.as, t.crossOrigin);
                a.d.M(e, {
                    crossOrigin: n,
                    integrity: typeof t.integrity == `string` ? t.integrity : void 0,
                    nonce: typeof t.nonce == `string` ? t.nonce : void 0
                })
            }
        } else t ?? a.d.M(e)
    }, e.preload = function (e, t) {
        if (typeof e == `string` && typeof t == `object` && t && typeof t.as == `string`) {
            var n = t.as, r = l(n, t.crossOrigin);
            a.d.L(e, n, {
                crossOrigin: r,
                integrity: typeof t.integrity == `string` ? t.integrity : void 0,
                nonce: typeof t.nonce == `string` ? t.nonce : void 0,
                type: typeof t.type == `string` ? t.type : void 0,
                fetchPriority: typeof t.fetchPriority == `string` ? t.fetchPriority : void 0,
                referrerPolicy: typeof t.referrerPolicy == `string` ? t.referrerPolicy : void 0,
                imageSrcSet: typeof t.imageSrcSet == `string` ? t.imageSrcSet : void 0,
                imageSizes: typeof t.imageSizes == `string` ? t.imageSizes : void 0,
                media: typeof t.media == `string` ? t.media : void 0
            })
        }
    }, e.preloadModule = function (e, t) {
        if (typeof e == `string`) if (t) {
            var n = l(t.as, t.crossOrigin);
            a.d.m(e, {
                as: typeof t.as == `string` && t.as !== `script` ? t.as : void 0,
                crossOrigin: n,
                integrity: typeof t.integrity == `string` ? t.integrity : void 0
            })
        } else a.d.m(e)
    }, e.requestFormReset = function (e) {
        a.d.r(e)
    }, e.unstable_batchedUpdates = function (e, t) {
        return e(t)
    }, e.useFormState = function (e, t, n) {
        return c.H.useFormState(e, t, n)
    }, e.useFormStatus = function () {
        return c.H.useHostTransitionStatus()
    }, e.version = `19.2.5`
})), s = e(((e, t) => {
    function n() {
        if (!(typeof __REACT_DEVTOOLS_GLOBAL_HOOK__ > `u` || typeof __REACT_DEVTOOLS_GLOBAL_HOOK__.checkDCE != `function`)) try {
            __REACT_DEVTOOLS_GLOBAL_HOOK__.checkDCE(n)
        } catch (e) {
            console.error(e)
        }
    }

    n(), t.exports = o()
})), c = class {
    constructor() {
        this.listeners = new Set, this.subscribe = this.subscribe.bind(this)
    }

    subscribe(e) {
        return this.listeners.add(e), this.onSubscribe(), () => {
            this.listeners.delete(e), this.onUnsubscribe()
        }
    }

    hasListeners() {
        return this.listeners.size > 0
    }

    onSubscribe() {
    }

    onUnsubscribe() {
    }
}, l = new class extends c {
    #e;
    #t;
    #n;

    constructor() {
        super(), this.#n = e => {
            if (typeof window < `u` && window.addEventListener) {
                let t = () => e();
                return window.addEventListener(`visibilitychange`, t, !1), () => {
                    window.removeEventListener(`visibilitychange`, t)
                }
            }
        }
    }

    onSubscribe() {
        this.#t || this.setEventListener(this.#n)
    }

    onUnsubscribe() {
        this.hasListeners() || (this.#t?.(), this.#t = void 0)
    }

    setEventListener(e) {
        this.#n = e, this.#t?.(), this.#t = e(e => {
            typeof e == `boolean` ? this.setFocused(e) : this.onFocus()
        })
    }

    setFocused(e) {
        this.#e !== e && (this.#e = e, this.onFocus())
    }

    onFocus() {
        let e = this.isFocused();
        this.listeners.forEach(t => {
            t(e)
        })
    }

    isFocused() {
        return typeof this.#e == `boolean` ? this.#e : globalThis.document?.visibilityState !== `hidden`
    }
}, u = {
    setTimeout: (e, t) => setTimeout(e, t),
    clearTimeout: e => clearTimeout(e),
    setInterval: (e, t) => setInterval(e, t),
    clearInterval: e => clearInterval(e)
}, d = new class {
    #e = u;

    setTimeoutProvider(e) {
        this.#e = e
    }

    setTimeout(e, t) {
        return this.#e.setTimeout(e, t)
    }

    clearTimeout(e) {
        this.#e.clearTimeout(e)
    }

    setInterval(e, t) {
        return this.#e.setInterval(e, t)
    }

    clearInterval(e) {
        this.#e.clearInterval(e)
    }
};

function f(e) {
    setTimeout(e, 0)
}

var p = typeof window > `u` || `Deno` in globalThis;

function m() {
}

function h(e, t) {
    return typeof e == `function` ? e(t) : e
}

function ee(e) {
    return typeof e == `number` && e >= 0 && e !== 1 / 0
}

function te(e, t) {
    return Math.max(e + (t || 0) - Date.now(), 0)
}

function g(e, t) {
    return typeof e == `function` ? e(t) : e
}

function ne(e, t) {
    return typeof e == `function` ? e(t) : e
}

function re(e, t) {
    let {type: n = `all`, exact: r, fetchStatus: i, predicate: a, queryKey: o, stale: s} = e;
    if (o) {
        if (r) {
            if (t.queryHash !== ae(o, t.options)) return !1
        } else if (!v(t.queryKey, o)) return !1
    }
    if (n !== `all`) {
        let e = t.isActive();
        if (n === `active` && !e || n === `inactive` && e) return !1
    }
    return !(typeof s == `boolean` && t.isStale() !== s || i && i !== t.state.fetchStatus || a && !a(t))
}

function ie(e, t) {
    let {exact: n, status: r, predicate: i, mutationKey: a} = e;
    if (a) {
        if (!t.options.mutationKey) return !1;
        if (n) {
            if (_(t.options.mutationKey) !== _(a)) return !1
        } else if (!v(t.options.mutationKey, a)) return !1
    }
    return !(r && t.state.status !== r || i && !i(t))
}

function ae(e, t) {
    return (t?.queryKeyHashFn || _)(e)
}

function _(e) {
    return JSON.stringify(e, (e, t) => ue(t) ? Object.keys(t).sort().reduce((e, n) => (e[n] = t[n], e), {}) : t)
}

function v(e, t) {
    return e === t ? !0 : typeof e == typeof t && e && t && typeof e == `object` && typeof t == `object` ? Object.keys(t).every(n => v(e[n], t[n])) : !1
}

var oe = Object.prototype.hasOwnProperty;

function se(e, t, n = 0) {
    if (e === t) return e;
    if (n > 500) return t;
    let r = le(e) && le(t);
    if (!r && !(ue(e) && ue(t))) return t;
    let i = (r ? e : Object.keys(e)).length, a = r ? t : Object.keys(t), o = a.length, s = r ? Array(o) : {}, c = 0;
    for (let l = 0; l < o; l++) {
        let o = r ? l : a[l], u = e[o], d = t[o];
        if (u === d) {
            s[o] = u, (r ? l < i : oe.call(e, o)) && c++;
            continue
        }
        if (u === null || d === null || typeof u != `object` || typeof d != `object`) {
            s[o] = d;
            continue
        }
        let f = se(u, d, n + 1);
        s[o] = f, f === u && c++
    }
    return i === o && c === i ? e : s
}

function ce(e, t) {
    if (!t || Object.keys(e).length !== Object.keys(t).length) return !1;
    for (let n in e) if (e[n] !== t[n]) return !1;
    return !0
}

function le(e) {
    return Array.isArray(e) && e.length === Object.keys(e).length
}

function ue(e) {
    if (!de(e)) return !1;
    let t = e.constructor;
    if (t === void 0) return !0;
    let n = t.prototype;
    return !(!de(n) || !n.hasOwnProperty(`isPrototypeOf`) || Object.getPrototypeOf(e) !== Object.prototype)
}

function de(e) {
    return Object.prototype.toString.call(e) === `[object Object]`
}

function fe(e) {
    return new Promise(t => {
        d.setTimeout(t, e)
    })
}

function pe(e, t, n) {
    return typeof n.structuralSharing == `function` ? n.structuralSharing(e, t) : n.structuralSharing === !1 ? t : se(e, t)
}

function me(e, t, n = 0) {
    let r = [...e, t];
    return n && r.length > n ? r.slice(1) : r
}

function he(e, t, n = 0) {
    let r = [t, ...e];
    return n && r.length > n ? r.slice(0, -1) : r
}

var ge = Symbol();

function _e(e, t) {
    return !e.queryFn && t?.initialPromise ? () => t.initialPromise : !e.queryFn || e.queryFn === ge ? () => Promise.reject(Error(`Missing queryFn: '${e.queryHash}'`)) : e.queryFn
}

function ve(e, t) {
    return typeof e == `function` ? e(...t) : !!e
}

function ye(e, t, n) {
    let r = !1, i;
    return Object.defineProperty(e, `signal`, {
        enumerable: !0,
        get: () => (i ??= t(), r ? i : (r = !0, i.aborted ? n() : i.addEventListener(`abort`, n, {once: !0}), i))
    }), e
}

var be = (() => {
    let e = () => p;
    return {
        isServer() {
            return e()
        }, setIsServer(t) {
            e = t
        }
    }
})();

function xe() {
    let e, t, n = new Promise((n, r) => {
        e = n, t = r
    });
    n.status = `pending`, n.catch(() => {
    });

    function r(e) {
        Object.assign(n, e), delete n.resolve, delete n.reject
    }

    return n.resolve = t => {
        r({status: `fulfilled`, value: t}), e(t)
    }, n.reject = e => {
        r({status: `rejected`, reason: e}), t(e)
    }, n
}

var Se = f;

function Ce() {
    let e = [], t = 0, n = e => {
        e()
    }, r = e => {
        e()
    }, i = Se, a = r => {
        t ? e.push(r) : i(() => {
            n(r)
        })
    }, o = () => {
        let t = e;
        e = [], t.length && i(() => {
            r(() => {
                t.forEach(e => {
                    n(e)
                })
            })
        })
    };
    return {
        batch: e => {
            let n;
            t++;
            try {
                n = e()
            } finally {
                t--, t || o()
            }
            return n
        }, batchCalls: e => (...t) => {
            a(() => {
                e(...t)
            })
        }, schedule: a, setNotifyFunction: e => {
            n = e
        }, setBatchNotifyFunction: e => {
            r = e
        }, setScheduler: e => {
            i = e
        }
    }
}

var y = Ce(), b = new class extends c {
    #e = !0;
    #t;
    #n;

    constructor() {
        super(), this.#n = e => {
            if (typeof window < `u` && window.addEventListener) {
                let t = () => e(!0), n = () => e(!1);
                return window.addEventListener(`online`, t, !1), window.addEventListener(`offline`, n, !1), () => {
                    window.removeEventListener(`online`, t), window.removeEventListener(`offline`, n)
                }
            }
        }
    }

    onSubscribe() {
        this.#t || this.setEventListener(this.#n)
    }

    onUnsubscribe() {
        this.hasListeners() || (this.#t?.(), this.#t = void 0)
    }

    setEventListener(e) {
        this.#n = e, this.#t?.(), this.#t = e(this.setOnline.bind(this))
    }

    setOnline(e) {
        this.#e !== e && (this.#e = e, this.listeners.forEach(t => {
            t(e)
        }))
    }

    isOnline() {
        return this.#e
    }
};

function we(e) {
    return Math.min(1e3 * 2 ** e, 3e4)
}

function Te(e) {
    return (e ?? `online`) === `online` ? b.isOnline() : !0
}

var Ee = class extends Error {
    constructor(e) {
        super(`CancelledError`), this.revert = e?.revert, this.silent = e?.silent
    }
};

function De(e) {
    let t = !1, n = 0, r, i = xe(), a = () => i.status !== `pending`, o = t => {
            if (!a()) {
                let n = new Ee(t);
                p(n), e.onCancel?.(n)
            }
        }, s = () => {
            t = !0
        }, c = () => {
            t = !1
        }, u = () => l.isFocused() && (e.networkMode === `always` || b.isOnline()) && e.canRun(),
        d = () => Te(e.networkMode) && e.canRun(), f = e => {
            a() || (r?.(), i.resolve(e))
        }, p = e => {
            a() || (r?.(), i.reject(e))
        }, m = () => new Promise(t => {
            r = e => {
                (a() || u()) && t(e)
            }, e.onPause?.()
        }).then(() => {
            r = void 0, a() || e.onContinue?.()
        }), h = () => {
            if (a()) return;
            let r, i = n === 0 ? e.initialPromise : void 0;
            try {
                r = i ?? e.fn()
            } catch (e) {
                r = Promise.reject(e)
            }
            Promise.resolve(r).then(f).catch(r => {
                if (a()) return;
                let i = e.retry ?? (be.isServer() ? 0 : 3), o = e.retryDelay ?? we,
                    s = typeof o == `function` ? o(n, r) : o,
                    c = i === !0 || typeof i == `number` && n < i || typeof i == `function` && i(n, r);
                if (t || !c) {
                    p(r);
                    return
                }
                n++, e.onFail?.(n, r), fe(s).then(() => u() ? void 0 : m()).then(() => {
                    t ? p(r) : h()
                })
            })
        };
    return {
        promise: i,
        status: () => i.status,
        cancel: o,
        continue: () => (r?.(), i),
        cancelRetry: s,
        continueRetry: c,
        canStart: d,
        start: () => (d() ? h() : m().then(h), i)
    }
}

var Oe = class {
    #e;

    destroy() {
        this.clearGcTimeout()
    }

    scheduleGc() {
        this.clearGcTimeout(), ee(this.gcTime) && (this.#e = d.setTimeout(() => {
            this.optionalRemove()
        }, this.gcTime))
    }

    updateGcTime(e) {
        this.gcTime = Math.max(this.gcTime || 0, e ?? (be.isServer() ? 1 / 0 : 300 * 1e3))
    }

    clearGcTimeout() {
        this.#e !== void 0 && (d.clearTimeout(this.#e), this.#e = void 0)
    }
};

function ke(e) {
    return {
        onFetch: (t, n) => {
            let r = t.options, i = t.fetchOptions?.meta?.fetchMore?.direction, a = t.state.data?.pages || [],
                o = t.state.data?.pageParams || [], s = {pages: [], pageParams: []}, c = 0, l = async () => {
                    let n = !1, l = e => {
                        ye(e, () => t.signal, () => n = !0)
                    }, u = _e(t.options, t.fetchOptions), d = async (e, r, i) => {
                        if (n) return Promise.reject(t.signal.reason);
                        if (r == null && e.pages.length) return Promise.resolve(e);
                        let a = await u((() => {
                            let e = {
                                client: t.client,
                                queryKey: t.queryKey,
                                pageParam: r,
                                direction: i ? `backward` : `forward`,
                                meta: t.options.meta
                            };
                            return l(e), e
                        })()), {maxPages: o} = t.options, s = i ? he : me;
                        return {pages: s(e.pages, a, o), pageParams: s(e.pageParams, r, o)}
                    };
                    if (i && a.length) {
                        let e = i === `backward`, t = e ? je : Ae, n = {pages: a, pageParams: o};
                        s = await d(n, t(r, n), e)
                    } else {
                        let t = e ?? a.length;
                        do {
                            let e = c === 0 ? o[0] ?? r.initialPageParam : Ae(r, s);
                            if (c > 0 && e == null) break;
                            s = await d(s, e), c++
                        } while (c < t)
                    }
                    return s
                };
            t.options.persister ? t.fetchFn = () => t.options.persister?.(l, {
                client: t.client,
                queryKey: t.queryKey,
                meta: t.options.meta,
                signal: t.signal
            }, n) : t.fetchFn = l
        }
    }
}

function Ae(e, {pages: t, pageParams: n}) {
    let r = t.length - 1;
    return t.length > 0 ? e.getNextPageParam(t[r], t, n[r], n) : void 0
}

function je(e, {pages: t, pageParams: n}) {
    return t.length > 0 ? e.getPreviousPageParam?.(t[0], t, n[0], n) : void 0
}

var Me = class extends Oe {
    #e;
    #t;
    #n;
    #r;
    #i;
    #a;
    #o;
    #s;

    constructor(e) {
        super(), this.#s = !1, this.#o = e.defaultOptions, this.setOptions(e.options), this.observers = [], this.#i = e.client, this.#r = this.#i.getQueryCache(), this.queryKey = e.queryKey, this.queryHash = e.queryHash, this.#t = Fe(this.options), this.state = e.state ?? this.#t, this.scheduleGc()
    }

    get meta() {
        return this.options.meta
    }

    get queryType() {
        return this.#e
    }

    get promise() {
        return this.#a?.promise
    }

    setOptions(e) {
        if (this.options = {...this.#o, ...e}, e?._type && (this.#e = e._type), this.updateGcTime(this.options.gcTime), this.state && this.state.data === void 0) {
            let e = Fe(this.options);
            e.data !== void 0 && (this.setState(Pe(e.data, e.dataUpdatedAt)), this.#t = e)
        }
    }

    optionalRemove() {
        !this.observers.length && this.state.fetchStatus === `idle` && this.#r.remove(this)
    }

    setData(e, t) {
        let n = pe(this.state.data, e, this.options);
        return this.#l({data: n, type: `success`, dataUpdatedAt: t?.updatedAt, manual: t?.manual}), n
    }

    setState(e) {
        this.#l({type: `setState`, state: e})
    }

    cancel(e) {
        let t = this.#a?.promise;
        return this.#a?.cancel(e), t ? t.then(m).catch(m) : Promise.resolve()
    }

    destroy() {
        super.destroy(), this.cancel({silent: !0})
    }

    get resetState() {
        return this.#t
    }

    reset() {
        this.destroy(), this.setState(this.resetState)
    }

    isActive() {
        return this.observers.some(e => ne(e.options.enabled, this) !== !1)
    }

    isDisabled() {
        return this.getObserversCount() > 0 ? !this.isActive() : this.options.queryFn === ge || !this.isFetched()
    }

    isFetched() {
        return this.state.dataUpdateCount + this.state.errorUpdateCount > 0
    }

    isStatic() {
        return this.getObserversCount() > 0 ? this.observers.some(e => g(e.options.staleTime, this) === `static`) : !1
    }

    isStale() {
        return this.getObserversCount() > 0 ? this.observers.some(e => e.getCurrentResult().isStale) : this.state.data === void 0 || this.state.isInvalidated
    }

    isStaleByTime(e = 0) {
        return this.state.data === void 0 ? !0 : e === `static` ? !1 : this.state.isInvalidated ? !0 : !te(this.state.dataUpdatedAt, e)
    }

    onFocus() {
        this.observers.find(e => e.shouldFetchOnWindowFocus())?.refetch({cancelRefetch: !1}), this.#a?.continue()
    }

    onOnline() {
        this.observers.find(e => e.shouldFetchOnReconnect())?.refetch({cancelRefetch: !1}), this.#a?.continue()
    }

    addObserver(e) {
        this.observers.includes(e) || (this.observers.push(e), this.clearGcTimeout(), this.#r.notify({
            type: `observerAdded`,
            query: this,
            observer: e
        }))
    }

    removeObserver(e) {
        this.observers.includes(e) && (this.observers = this.observers.filter(t => t !== e), this.observers.length || (this.#a && (this.#s || this.#c() ? this.#a.cancel({revert: !0}) : this.#a.cancelRetry()), this.scheduleGc()), this.#r.notify({
            type: `observerRemoved`,
            query: this,
            observer: e
        }))
    }

    getObserversCount() {
        return this.observers.length
    }

    #c() {
        return this.state.fetchStatus === `paused` && this.state.status === `pending`
    }

    invalidate() {
        this.state.isInvalidated || this.#l({type: `invalidate`})
    }

    async fetch(e, t) {
        if (this.state.fetchStatus !== `idle` && this.#a?.status() !== `rejected`) {
            if (this.state.data !== void 0 && t?.cancelRefetch) this.cancel({silent: !0}); else if (this.#a) return this.#a.continueRetry(), this.#a.promise
        }
        if (e && this.setOptions(e), !this.options.queryFn) {
            let e = this.observers.find(e => e.options.queryFn);
            e && this.setOptions(e.options)
        }
        let n = new AbortController, r = e => {
            Object.defineProperty(e, `signal`, {enumerable: !0, get: () => (this.#s = !0, n.signal)})
        }, i = () => {
            let e = _e(this.options, t), n = (() => {
                let e = {client: this.#i, queryKey: this.queryKey, meta: this.meta};
                return r(e), e
            })();
            return this.#s = !1, this.options.persister ? this.options.persister(e, n, this) : e(n)
        }, a = (() => {
            let e = {
                fetchOptions: t,
                options: this.options,
                queryKey: this.queryKey,
                client: this.#i,
                state: this.state,
                fetchFn: i
            };
            return r(e), e
        })();
        (this.#e === `infinite` ? ke(this.options.pages) : this.options.behavior)?.onFetch(a, this), this.#n = this.state, (this.state.fetchStatus === `idle` || this.state.fetchMeta !== a.fetchOptions?.meta) && this.#l({
            type: `fetch`,
            meta: a.fetchOptions?.meta
        }), this.#a = De({
            initialPromise: t?.initialPromise,
            fn: a.fetchFn,
            onCancel: e => {
                e instanceof Ee && e.revert && this.setState({...this.#n, fetchStatus: `idle`}), n.abort()
            },
            onFail: (e, t) => {
                this.#l({type: `failed`, failureCount: e, error: t})
            },
            onPause: () => {
                this.#l({type: `pause`})
            },
            onContinue: () => {
                this.#l({type: `continue`})
            },
            retry: a.options.retry,
            retryDelay: a.options.retryDelay,
            networkMode: a.options.networkMode,
            canRun: () => !0
        });
        try {
            let e = await this.#a.start();
            if (e === void 0) throw Error(`${this.queryHash} data is undefined`);
            return this.setData(e), this.#r.config.onSuccess?.(e, this), this.#r.config.onSettled?.(e, this.state.error, this), e
        } catch (e) {
            if (e instanceof Ee) {
                if (e.silent) return this.#a.promise;
                if (e.revert) {
                    if (this.state.data === void 0) throw e;
                    return this.state.data
                }
            }
            throw this.#l({
                type: `error`,
                error: e
            }), this.#r.config.onError?.(e, this), this.#r.config.onSettled?.(this.state.data, e, this), e
        } finally {
            this.scheduleGc()
        }
    }

    #l(e) {
        this.state = (t => {
            switch (e.type) {
                case`failed`:
                    return {...t, fetchFailureCount: e.failureCount, fetchFailureReason: e.error};
                case`pause`:
                    return {...t, fetchStatus: `paused`};
                case`continue`:
                    return {...t, fetchStatus: `fetching`};
                case`fetch`:
                    return {...t, ...Ne(t.data, this.options), fetchMeta: e.meta ?? null};
                case`success`:
                    let n = {
                        ...t, ...Pe(e.data, e.dataUpdatedAt),
                        dataUpdateCount: t.dataUpdateCount + 1, ...!e.manual && {
                            fetchStatus: `idle`,
                            fetchFailureCount: 0,
                            fetchFailureReason: null
                        }
                    };
                    return this.#n = e.manual ? n : void 0, n;
                case`error`:
                    let r = e.error;
                    return {
                        ...t,
                        error: r,
                        errorUpdateCount: t.errorUpdateCount + 1,
                        errorUpdatedAt: Date.now(),
                        fetchFailureCount: t.fetchFailureCount + 1,
                        fetchFailureReason: r,
                        fetchStatus: `idle`,
                        status: `error`,
                        isInvalidated: !0
                    };
                case`invalidate`:
                    return {...t, isInvalidated: !0};
                case`setState`:
                    return {...t, ...e.state}
            }
        })(this.state), y.batch(() => {
            this.observers.forEach(e => {
                e.onQueryUpdate()
            }), this.#r.notify({query: this, type: `updated`, action: e})
        })
    }
};

function Ne(e, t) {
    return {
        fetchFailureCount: 0,
        fetchFailureReason: null,
        fetchStatus: Te(t.networkMode) ? `fetching` : `paused`, ...e === void 0 && {error: null, status: `pending`}
    }
}

function Pe(e, t) {
    return {data: e, dataUpdatedAt: t ?? Date.now(), error: null, isInvalidated: !1, status: `success`}
}

function Fe(e) {
    let t = typeof e.initialData == `function` ? e.initialData() : e.initialData, n = t !== void 0,
        r = n ? typeof e.initialDataUpdatedAt == `function` ? e.initialDataUpdatedAt() : e.initialDataUpdatedAt : 0;
    return {
        data: t,
        dataUpdateCount: 0,
        dataUpdatedAt: n ? r ?? Date.now() : 0,
        error: null,
        errorUpdateCount: 0,
        errorUpdatedAt: 0,
        fetchFailureCount: 0,
        fetchFailureReason: null,
        fetchMeta: null,
        isInvalidated: !1,
        status: n ? `success` : `pending`,
        fetchStatus: `idle`
    }
}

var Ie = class extends Oe {
    #e;
    #t;
    #n;
    #r;

    constructor(e) {
        super(), this.#e = e.client, this.mutationId = e.mutationId, this.#n = e.mutationCache, this.#t = [], this.state = e.state || Le(), this.setOptions(e.options), this.scheduleGc()
    }

    setOptions(e) {
        this.options = e, this.updateGcTime(this.options.gcTime)
    }

    get meta() {
        return this.options.meta
    }

    addObserver(e) {
        this.#t.includes(e) || (this.#t.push(e), this.clearGcTimeout(), this.#n.notify({
            type: `observerAdded`,
            mutation: this,
            observer: e
        }))
    }

    removeObserver(e) {
        this.#t = this.#t.filter(t => t !== e), this.scheduleGc(), this.#n.notify({
            type: `observerRemoved`,
            mutation: this,
            observer: e
        })
    }

    optionalRemove() {
        this.#t.length || (this.state.status === `pending` ? this.scheduleGc() : this.#n.remove(this))
    }

    continue() {
        return this.#r?.continue() ?? this.execute(this.state.variables)
    }

    async execute(e) {
        let t = () => {
            this.#i({type: `continue`})
        }, n = {client: this.#e, meta: this.options.meta, mutationKey: this.options.mutationKey};
        this.#r = De({
            fn: () => this.options.mutationFn ? this.options.mutationFn(e, n) : Promise.reject(Error(`No mutationFn found`)),
            onFail: (e, t) => {
                this.#i({type: `failed`, failureCount: e, error: t})
            },
            onPause: () => {
                this.#i({type: `pause`})
            },
            onContinue: t,
            retry: this.options.retry ?? 0,
            retryDelay: this.options.retryDelay,
            networkMode: this.options.networkMode,
            canRun: () => this.#n.canRun(this)
        });
        let r = this.state.status === `pending`, i = !this.#r.canStart();
        try {
            if (r) t(); else {
                this.#i({
                    type: `pending`,
                    variables: e,
                    isPaused: i
                }), this.#n.config.onMutate && await this.#n.config.onMutate(e, this, n);
                let t = await this.options.onMutate?.(e, n);
                t !== this.state.context && this.#i({type: `pending`, context: t, variables: e, isPaused: i})
            }
            let a = await this.#r.start();
            return await this.#n.config.onSuccess?.(a, e, this.state.context, this, n), await this.options.onSuccess?.(a, e, this.state.context, n), await this.#n.config.onSettled?.(a, null, this.state.variables, this.state.context, this, n), await this.options.onSettled?.(a, null, e, this.state.context, n), this.#i({
                type: `success`,
                data: a
            }), a
        } catch (t) {
            try {
                await this.#n.config.onError?.(t, e, this.state.context, this, n)
            } catch (e) {
                Promise.reject(e)
            }
            try {
                await this.options.onError?.(t, e, this.state.context, n)
            } catch (e) {
                Promise.reject(e)
            }
            try {
                await this.#n.config.onSettled?.(void 0, t, this.state.variables, this.state.context, this, n)
            } catch (e) {
                Promise.reject(e)
            }
            try {
                await this.options.onSettled?.(void 0, t, e, this.state.context, n)
            } catch (e) {
                Promise.reject(e)
            }
            throw this.#i({type: `error`, error: t}), t
        } finally {
            this.#n.runNext(this)
        }
    }

    #i(e) {
        this.state = (t => {
            switch (e.type) {
                case`failed`:
                    return {...t, failureCount: e.failureCount, failureReason: e.error};
                case`pause`:
                    return {...t, isPaused: !0};
                case`continue`:
                    return {...t, isPaused: !1};
                case`pending`:
                    return {
                        ...t,
                        context: e.context,
                        data: void 0,
                        failureCount: 0,
                        failureReason: null,
                        error: null,
                        isPaused: e.isPaused,
                        status: `pending`,
                        variables: e.variables,
                        submittedAt: Date.now()
                    };
                case`success`:
                    return {
                        ...t,
                        data: e.data,
                        failureCount: 0,
                        failureReason: null,
                        error: null,
                        status: `success`,
                        isPaused: !1
                    };
                case`error`:
                    return {
                        ...t,
                        data: void 0,
                        error: e.error,
                        failureCount: t.failureCount + 1,
                        failureReason: e.error,
                        isPaused: !1,
                        status: `error`
                    }
            }
        })(this.state), y.batch(() => {
            this.#t.forEach(t => {
                t.onMutationUpdate(e)
            }), this.#n.notify({mutation: this, type: `updated`, action: e})
        })
    }
};

function Le() {
    return {
        context: void 0,
        data: void 0,
        error: null,
        failureCount: 0,
        failureReason: null,
        isPaused: !1,
        status: `idle`,
        variables: void 0,
        submittedAt: 0
    }
}

var Re = class extends c {
    constructor(e = {}) {
        super(), this.config = e, this.#e = new Set, this.#t = new Map, this.#n = 0
    }

    #e;
    #t;
    #n;

    build(e, t, n) {
        let r = new Ie({
            client: e,
            mutationCache: this,
            mutationId: ++this.#n,
            options: e.defaultMutationOptions(t),
            state: n
        });
        return this.add(r), r
    }

    add(e) {
        this.#e.add(e);
        let t = x(e);
        if (typeof t == `string`) {
            let n = this.#t.get(t);
            n ? n.push(e) : this.#t.set(t, [e])
        }
        this.notify({type: `added`, mutation: e})
    }

    remove(e) {
        if (this.#e.delete(e)) {
            let t = x(e);
            if (typeof t == `string`) {
                let n = this.#t.get(t);
                if (n) if (n.length > 1) {
                    let t = n.indexOf(e);
                    t !== -1 && n.splice(t, 1)
                } else n[0] === e && this.#t.delete(t)
            }
        }
        this.notify({type: `removed`, mutation: e})
    }

    canRun(e) {
        let t = x(e);
        if (typeof t == `string`) {
            let n = this.#t.get(t)?.find(e => e.state.status === `pending`);
            return !n || n === e
        } else return !0
    }

    runNext(e) {
        let t = x(e);
        return typeof t == `string` ? (this.#t.get(t)?.find(t => t !== e && t.state.isPaused))?.continue() ?? Promise.resolve() : Promise.resolve()
    }

    clear() {
        y.batch(() => {
            this.#e.forEach(e => {
                this.notify({type: `removed`, mutation: e})
            }), this.#e.clear(), this.#t.clear()
        })
    }

    getAll() {
        return Array.from(this.#e)
    }

    find(e) {
        let t = {exact: !0, ...e};
        return this.getAll().find(e => ie(t, e))
    }

    findAll(e = {}) {
        return this.getAll().filter(t => ie(e, t))
    }

    notify(e) {
        y.batch(() => {
            this.listeners.forEach(t => {
                t(e)
            })
        })
    }

    resumePausedMutations() {
        let e = this.getAll().filter(e => e.state.isPaused);
        return y.batch(() => Promise.all(e.map(e => e.continue().catch(m))))
    }
};

function x(e) {
    return e.options.scope?.id
}

var ze = class extends c {
        constructor(e = {}) {
            super(), this.config = e, this.#e = new Map
        }

        #e;

        build(e, t, n) {
            let r = t.queryKey, i = t.queryHash ?? ae(r, t), a = this.get(i);
            return a || (a = new Me({
                client: e,
                queryKey: r,
                queryHash: i,
                options: e.defaultQueryOptions(t),
                state: n,
                defaultOptions: e.getQueryDefaults(r)
            }), this.add(a)), a
        }

        add(e) {
            this.#e.has(e.queryHash) || (this.#e.set(e.queryHash, e), this.notify({type: `added`, query: e}))
        }

        remove(e) {
            let t = this.#e.get(e.queryHash);
            t && (e.destroy(), t === e && this.#e.delete(e.queryHash), this.notify({type: `removed`, query: e}))
        }

        clear() {
            y.batch(() => {
                this.getAll().forEach(e => {
                    this.remove(e)
                })
            })
        }

        get(e) {
            return this.#e.get(e)
        }

        getAll() {
            return [...this.#e.values()]
        }

        find(e) {
            let t = {exact: !0, ...e};
            return this.getAll().find(e => re(t, e))
        }

        findAll(e = {}) {
            let t = this.getAll();
            return Object.keys(e).length > 0 ? t.filter(t => re(e, t)) : t
        }

        notify(e) {
            y.batch(() => {
                this.listeners.forEach(t => {
                    t(e)
                })
            })
        }

        onFocus() {
            y.batch(() => {
                this.getAll().forEach(e => {
                    e.onFocus()
                })
            })
        }

        onOnline() {
            y.batch(() => {
                this.getAll().forEach(e => {
                    e.onOnline()
                })
            })
        }
    }, Be = class {
        #e;
        #t;
        #n;
        #r;
        #i;
        #a;
        #o;
        #s;

        constructor(e = {}) {
            this.#e = e.queryCache || new ze, this.#t = e.mutationCache || new Re, this.#n = e.defaultOptions || {}, this.#r = new Map, this.#i = new Map, this.#a = 0
        }

        mount() {
            this.#a++, this.#a === 1 && (this.#o = l.subscribe(async e => {
                e && (await this.resumePausedMutations(), this.#e.onFocus())
            }), this.#s = b.subscribe(async e => {
                e && (await this.resumePausedMutations(), this.#e.onOnline())
            }))
        }

        unmount() {
            this.#a--, this.#a === 0 && (this.#o?.(), this.#o = void 0, this.#s?.(), this.#s = void 0)
        }

        isFetching(e) {
            return this.#e.findAll({...e, fetchStatus: `fetching`}).length
        }

        isMutating(e) {
            return this.#t.findAll({...e, status: `pending`}).length
        }

        getQueryData(e) {
            let t = this.defaultQueryOptions({queryKey: e});
            return this.#e.get(t.queryHash)?.state.data
        }

        ensureQueryData(e) {
            let t = this.defaultQueryOptions(e), n = this.#e.build(this, t), r = n.state.data;
            return r === void 0 ? this.fetchQuery(e) : (e.revalidateIfStale && n.isStaleByTime(g(t.staleTime, n)) && this.prefetchQuery(t), Promise.resolve(r))
        }

        getQueriesData(e) {
            return this.#e.findAll(e).map(({queryKey: e, state: t}) => [e, t.data])
        }

        setQueryData(e, t, n) {
            let r = this.defaultQueryOptions({queryKey: e}), i = this.#e.get(r.queryHash)?.state.data, a = h(t, i);
            if (a !== void 0) return this.#e.build(this, r).setData(a, {...n, manual: !0})
        }

        setQueriesData(e, t, n) {
            return y.batch(() => this.#e.findAll(e).map(({queryKey: e}) => [e, this.setQueryData(e, t, n)]))
        }

        getQueryState(e) {
            let t = this.defaultQueryOptions({queryKey: e});
            return this.#e.get(t.queryHash)?.state
        }

        removeQueries(e) {
            let t = this.#e;
            y.batch(() => {
                t.findAll(e).forEach(e => {
                    t.remove(e)
                })
            })
        }

        resetQueries(e, t) {
            let n = this.#e;
            return y.batch(() => (n.findAll(e).forEach(e => {
                e.reset()
            }), this.refetchQueries({type: `active`, ...e}, t)))
        }

        cancelQueries(e, t = {}) {
            let n = {revert: !0, ...t}, r = y.batch(() => this.#e.findAll(e).map(e => e.cancel(n)));
            return Promise.all(r).then(m).catch(m)
        }

        invalidateQueries(e, t = {}) {
            return y.batch(() => (this.#e.findAll(e).forEach(e => {
                e.invalidate()
            }), e?.refetchType === `none` ? Promise.resolve() : this.refetchQueries({
                ...e,
                type: e?.refetchType ?? e?.type ?? `active`
            }, t)))
        }

        refetchQueries(e, t = {}) {
            let n = {...t, cancelRefetch: t.cancelRefetch ?? !0},
                r = y.batch(() => this.#e.findAll(e).filter(e => !e.isDisabled() && !e.isStatic()).map(e => {
                    let t = e.fetch(void 0, n);
                    return n.throwOnError || (t = t.catch(m)), e.state.fetchStatus === `paused` ? Promise.resolve() : t
                }));
            return Promise.all(r).then(m)
        }

        fetchQuery(e) {
            let t = this.defaultQueryOptions(e);
            t.retry === void 0 && (t.retry = !1);
            let n = this.#e.build(this, t);
            return n.isStaleByTime(g(t.staleTime, n)) ? n.fetch(t) : Promise.resolve(n.state.data)
        }

        prefetchQuery(e) {
            return this.fetchQuery(e).then(m).catch(m)
        }

        fetchInfiniteQuery(e) {
            return e._type = `infinite`, this.fetchQuery(e)
        }

        prefetchInfiniteQuery(e) {
            return this.fetchInfiniteQuery(e).then(m).catch(m)
        }

        ensureInfiniteQueryData(e) {
            return e._type = `infinite`, this.ensureQueryData(e)
        }

        resumePausedMutations() {
            return b.isOnline() ? this.#t.resumePausedMutations() : Promise.resolve()
        }

        getQueryCache() {
            return this.#e
        }

        getMutationCache() {
            return this.#t
        }

        getDefaultOptions() {
            return this.#n
        }

        setDefaultOptions(e) {
            this.#n = e
        }

        setQueryDefaults(e, t) {
            this.#r.set(_(e), {queryKey: e, defaultOptions: t})
        }

        getQueryDefaults(e) {
            let t = [...this.#r.values()], n = {};
            return t.forEach(t => {
                v(e, t.queryKey) && Object.assign(n, t.defaultOptions)
            }), n
        }

        setMutationDefaults(e, t) {
            this.#i.set(_(e), {mutationKey: e, defaultOptions: t})
        }

        getMutationDefaults(e) {
            let t = [...this.#i.values()], n = {};
            return t.forEach(t => {
                v(e, t.mutationKey) && Object.assign(n, t.defaultOptions)
            }), n
        }

        defaultQueryOptions(e) {
            if (e._defaulted) return e;
            let t = {...this.#n.queries, ...this.getQueryDefaults(e.queryKey), ...e, _defaulted: !0};
            return t.queryHash ||= ae(t.queryKey, t), t.refetchOnReconnect === void 0 && (t.refetchOnReconnect = t.networkMode !== `always`), t.throwOnError === void 0 && (t.throwOnError = !!t.suspense), !t.networkMode && t.persister && (t.networkMode = `offlineFirst`), t.queryFn === ge && (t.enabled = !1), t
        }

        defaultMutationOptions(e) {
            return e?._defaulted ? e : {
                ...this.#n.mutations, ...e?.mutationKey && this.getMutationDefaults(e.mutationKey), ...e,
                _defaulted: !0
            }
        }

        clear() {
            this.#e.clear(), this.#t.clear()
        }
    }, S = a(i(), 1), C = n(), Ve = S.createContext(void 0), He = e => {
        let t = S.useContext(Ve);
        if (e) return e;
        if (!t) throw Error(`No QueryClient set, use QueryClientProvider to set one`);
        return t
    }, Ue = ({client: e, children: t}) => (S.useEffect(() => (e.mount(), () => {
        e.unmount()
    }), [e]), (0, C.jsx)(Ve.Provider, {value: e, children: t})), We = `user_data`, w = `auth_token`,
    Ge = `viewer_auth_hint`, Ke = `watched_promo`,
    qe = new Be({defaultOptions: {queries: {staleTime: 3e4, gcTime: 5 * 6e4, refetchOnWindowFocus: !1, retry: 1}}}),
    T = function (e) {
        return e.One = `очко`, e.Few = `очка`, e.Many = `очков`, e
    }({}), Je = (e, t = `default`) => {
        let n = Math.abs(Math.trunc(e)), r = n % 10, i = n % 100;
        return t === `genitive` ? r === 1 && i !== 11 ? T.Few : T.Many : r === 1 && i !== 11 ? T.One : r >= 2 && r <= 4 && (i < 10 || i >= 20) ? T.Few : T.Many
    }, Ye = (e, t = `default`) => `${Math.abs(Math.trunc(e))} ${Je(e, t)}`, Xe = e => `получено ${Ye(e)}`,
    Ze = e => `потеряно ${Ye(e)}`, E = e => {
        let t = Math.abs(Math.trunc(e)), n = t % 10, r = t % 100;
        return n === 1 && r !== 11 ? `час` : n >= 2 && n <= 4 && (r < 10 || r >= 20) ? `часа` : `часов`
    }, Qe = e => {
        let t = Math.abs(Math.trunc(e)), n = t % 10, r = t % 100;
        return n === 1 && r !== 11 ? `минута` : n >= 2 && n <= 4 && (r < 10 || r >= 20) ? `минуты` : `минут`
    }, $e = e => {
        let t = Math.abs(Math.trunc(e)), n = t % 10, r = t % 100;
        return n === 1 && r !== 11 ? `день` : n >= 2 && n <= 4 && (r < 10 || r >= 20) ? `дня` : `дней`
    }, et = e => {
        let t = Math.max(0, Math.floor(e));
        if (t < 3600) return `< 1 часа`;
        let n = Math.floor(t / 3600);
        return `${n} ${E(n)}`
    }, tt = e => {
        if (e == null || Number.isNaN(e)) return null;
        let t = Math.max(0, Math.floor(e)), n = Math.floor(t / 3600), r = Math.floor(t % 3600 / 60), i = [];
        return n > 0 && i.push(`${n} ${E(n)}`), (r > 0 || n === 0) && i.push(`${r} ${Qe(r)}`), i.join(` `)
    }, nt = e => {
        let t = Math.max(0, Math.floor(e)), n = Math.floor(t / 3600), r = Math.floor(t % 3600 / 60), i = Math.floor(t % 60),
            a = e => String(e).padStart(2, `0`);
        return `${a(n)}:${a(r)}:${a(i)}`
    }, rt = e => {
        let t = Math.abs(Math.trunc(e)), n = t % 10, r = t % 100;
        return n === 1 && r !== 11 ? `ход` : n >= 2 && n <= 4 && (r < 10 || r >= 20) ? `хода` : `ходов`
    }, it = (e, t) => {
        let n = Math.abs(Math.trunc(e)), r = n % 10, i = n % 100, a = r === 1 && i !== 11;
        return t === `hour` ? a ? `Остался` : `Осталось` : a ? `Осталась` : `Осталось`
    }, at = (e, t = new Date) => {
        let n = new Date(e);
        if (Number.isNaN(n.getTime())) return;
        let r = n.getTime() - t.getTime();
        if (r <= 0) return;
        let i = Math.ceil(r / 6e4), a = Math.floor(i / 60), o = i % 60, s = [];
        return a > 0 && s.push(`${a} ${E(a)}`), s.push(`${o} ${Qe(o)}`), `${a > 0 ? o > 0 ? `Осталось` : it(a, `hour`) : it(o, `minute`)} ${s.join(` `)}`
    }, ot = e => {
        let t = Math.abs(Math.trunc(e)), n = t % 10, r = t % 100;
        return n === 1 && r !== 11 ? `игра` : n >= 2 && n <= 4 && (r < 10 || r >= 20) ? `игры` : `игр`
    };

function st(e, t) {
    return t?.effect?.activationCooldown == null ? t?.effectCatalog?.find(t => t.name === e)?.activationCooldown != null : !0
}

function ct(e) {
    let t = Number(e);
    if (Number.isFinite(t) && t > 0) return t
}

function D(e) {
    if (e != null) {
        if (Array.isArray(e)) {
            for (let t of e) {
                let e = D(t);
                if (e !== void 0) return e
            }
            return
        }
        if (typeof e == `number`) return e > 0 ? e : void 0;
        if (typeof e == `object`) return ct(e.remaining) ?? ct(e.count) ?? ct(e.days)
    }
}

function O(e) {
    if (e != null) {
        if (Array.isArray(e)) {
            for (let t of e) {
                let e = O(t);
                if (e !== void 0) return e
            }
            return
        }
        return D(e) === void 0 ? void 0 : e
    }
}

function lt(e, t) {
    let {effectCooldowns: n, effectInstanceId: r} = t, i = n?.activation, a = [i?.[e], r ? i?.[r] : void 0];
    for (let e of a) {
        let t = O(e);
        if (t !== void 0) return t
    }
}

function ut(e, t) {
    if (typeof e == `object` && e) {
        if (e.type === `calendar-day`) return $e(t);
        if (e.counter === `completed-games`) return ot(t);
        if (e.counter === `turns` || e.type === `counter`) return rt(t)
    }
    return rt(t)
}

function dt(e) {
    let t = O(e);
    if (t === void 0) return;
    let n = D(t);
    if (n !== void 0) return `КД: ${n} ${ut(t, n)}`
}

function ft(e) {
    let t = O(e);
    if (t === void 0) return;
    let n = D(t);
    if (n !== void 0) return `${n} ${ut(t, n)}`
}

function pt(e) {
    if (!e) return {};
    let t = {};
    for (let [n, r] of Object.entries(e)) {
        let e = O(r);
        e !== void 0 && (t[n] = e)
    }
    return t
}

function mt(e) {
    if (e != null) return pt(e.activation)
}

var ht = {dropped: 0, completed: 0, rerolled: 0, totalPlaytimeSec: 0, ggpNetFromGames: 0},
    gt = {positive: 0, negative: 0, neutral: 0}, _t = {regular: 0, platinum: 0, gnus: 0, total: 0};

function vt(e) {
    let t = e.player.rollImpactStats;
    return e.rollImpactStats ?? t ?? gt
}

function yt(e) {
    let t = e.player.rollKindStats;
    return e.rollKindStats ?? t ?? _t
}

function bt(e) {
    if (!e) return null;
    let t = e.type === `movie` ? {name: e.title ?? `Неизвестно`} : {
            id: e.hltbGameId ?? 0,
            name: e.title ?? `Неизвестно`,
            imageUrl: e.imageUrl,
            releaseYear: e.releaseYear,
            hours: e.hours,
            steamStoreLink: e.steamAppId ? `https://store.steampowered.com/app/${e.steamAppId}` : void 0
        }, n = e.fastestFinalSpentSec ?? e.fastestTimerAccumulatedSec,
        r = e.scoreAdjustments?.reduce((e, t) => e + t.amount, 0) ?? 0,
        i = typeof e.ggpBonus == `number` ? e.ggpBonus : 0;
    return {
        wastelandKing: e.fastestPlayerName && n != null ? {
            playerName: e.fastestPlayerName,
            bestTimeMinutes: Math.max(0, Math.floor(n / 60))
        } : null,
        data: t,
        ggpReward: e.ggpReward ?? 0,
        ggpPenalty: e.ggpPenalty ?? 0,
        ggpBonus: r !== 0 || i !== 0 ? {total: r + i} : null
    }
}

function xt(e) {
    return typeof e == `object` && !!e && `player` in e
}

function k(e) {
    let t = e.player, n = e.auctionResultStats ?? ht, r = vt(e), i = yt(e), a = e.currentAuctionResult ?? null,
        o = t.currentGame ?? bt(a);
    return {
        ...t,
        role: t.role ?? `competitor`,
        displayName: t.displayName ?? t.name,
        twitchId: t.twitchId ?? ``,
        currentAuctionResult: a,
        currentGame: o,
        gamesCompleted: t.gamesCompleted ?? n.completed,
        gamesDropped: t.gamesDropped ?? n.dropped,
        gamesRerolled: t.gamesRerolled ?? n.rerolled,
        timeInGame: t.timeInGame ?? n.totalPlaytimeSec,
        rollsPositive: r.positive ?? t.rollsPositive ?? 0,
        rollsNegative: r.negative ?? t.rollsNegative ?? 0,
        rollsNeutral: r.neutral ?? t.rollsNeutral ?? 0,
        rollsTotal: i.total ?? t.rollsTotal ?? 0,
        rollsRegular: i.regular ?? t.rollsRegular ?? 0,
        rollsPlatinum: i.platinum ?? t.rollsPlatinum ?? 0,
        rollsGnus: i.gnus ?? t.rollsGnus ?? 0,
        rollsJudge: i.judge ?? t.rollsJudge ?? 0,
        avgRollsPerTurn: i.avgRollsPerTurn ?? t.avgRollsPerTurn ?? 0,
        ggpNetFromGames: n.ggpNetFromGames ?? t.ggpNetFromGames ?? 0,
        gamesAchievements: t.gamesAchievements ?? e.achievements?.length ?? 0,
        achievements: e.achievements ?? t.achievements ?? [],
        effects: e.inventoryEffects ?? e.storedEffects ?? t.effects ?? [],
        activeEffects: e.attachedEffects ?? e.activeEffects ?? t.activeEffects ?? [],
        effectCooldowns: e.effectCooldowns ?? t.effectCooldowns,
        cooldowns: mt(e.effectCooldowns ?? t.effectCooldowns) ?? t.cooldowns,
        casinoPhase: t.casinoPhase ?? null,
        gnusAvailable: t.gnusAvailable ?? 0,
        stream: e.stream ?? t.stream ?? null,
        socialLinks: e.socialLinks ?? t.socialLinks ?? []
    }
}

function St(e, t) {
    if (!t || e.length === 0) return null;
    if (t.id) {
        let n = e.find(e => e.id === t.id);
        if (n) return n
    }
    if (t.userId) {
        let n = e.find(e => e.userId === t.userId);
        if (n) return n
    }
    return null
}

function A(e) {
    return e?.kind === `viewer`
}

var Ct = new Date(`2026-06-19T19:00:00+03:00`).getTime();

function wt(e = Date.now()) {
    return Ct - e
}

function Tt(e = Date.now()) {
    return wt(e) <= 0
}

var Et = `/data/event-snapshot`, Dt = new Map;

function j(e = Date.now()) {
    return Tt(e)
}

async function M(e) {
    let t = Dt.get(e);
    if (t !== void 0) return t;
    try {
        let t = await fetch(`${Et}/${e}`);
        if (!t.ok) return null;
        let n = await t.json();
        return Dt.set(e, n), n
    } catch {
        return null
    }
}

async function Ot() {
    let e = await M(`players.json`);
    return e?.data?.array ? e.data.array.map(e => xt(e) ? k(e) : e) : null
}

async function kt() {
    return Ot()
}

async function At(e) {
    let t = await Ot();
    return t ? t.find(t => t.id === e) ?? null : null
}

async function jt(e) {
    let t = await Ot();
    return t ? t.find(t => t.userId === e) ?? null : null
}

async function Mt() {
    return (await M(`effects.json`))?.data?.array ?? null
}

async function Nt() {
    let e = await M(`achievements.json`);
    return e?.data?.achievements ? {
        achievements: e.data.achievements,
        totalCount: e.data.totalCount,
        revealedCount: e.data.revealedCount
    } : null
}

async function Pt() {
    return M(`public-player-stats.json`)
}

async function Ft(e) {
    let t = await M(`player-game-histories.json`);
    return t ? t[e] ?? [] : null
}

async function It(e) {
    let t = await M(`player-previous-event-games.json`);
    return t ? t[e] ?? [] : null
}

async function Lt(e) {
    let t = await M(`player-action-logs.json`);
    return t ? t[e] ?? [] : null
}

var Rt = 30;

async function zt(e) {
    if (!e || e.length === 0) return null;
    let t = await M(`games.json`);
    if (!t) return null;
    let n = e.trim().toLowerCase();
    return n ? t.filter(e => e.name.toLowerCase().includes(n)).slice(0, Rt) : null
}

function Bt() {
    if (typeof localStorage > `u`) return !1;
    try {
        return !!(localStorage.getItem(`user_data`) || localStorage.getItem(`auth_token`) || localStorage.getItem(`viewer_auth_hint`))
    } catch {
        return !1
    }
}

function Vt() {
    return Bt()
}

function Ht() {
    if (typeof localStorage > `u`) return null;
    try {
        let e = localStorage.getItem(We);
        if (!e) return null;
        let t = JSON.parse(e);
        return t?.id ? t : null
    } catch {
        return null
    }
}

function Ut() {
    let e = qe.getQueryData([`user`]);
    return e === void 0 ? Ht() : e
}

function Wt() {
    return `https://api-core.nassal.pro/api`.replace(/\/$/, ``)
}

function Gt() {
    return `https://api-game.nassal.pro/api`.replace(/\/$/, ``)
}

function Kt(e) {
    let t = Wt(), n = e.startsWith(`/`) ? e : `/${e}`;
    return n.startsWith(`/api/`) && t.endsWith(`/api`) ? `${t}${n.slice(4)}` : `${t}${n}`
}

function qt(e) {
    if (!e) return ``;
    let t = Gt();
    return t ? e.startsWith(`/api/`) ? t.endsWith(`/api`) ? `${t}${e.slice(4)}` : `${t}${e}` : e.startsWith(`/`) ? `${t}${e}` : e : e
}

var Jt = `https://howlongtobeat.com/games/`;

function Yt(e) {
    if (e == null || e === ``) return ``;
    let t = e.trim();
    return /^https?:\/\//i.test(t) ? t : `${Jt}${t.replace(/^\/+/, ``)}`
}

var Xt = `both`, Zt = `Попробуйте еще раз`, Qt = new Set([502, 503, 504, 520]), N = class extends Error {
    constructor(e, t, n, r) {
        super(n || `API Request Failed: ${e} ${t}`), this.statusCode = e, this.statusMessage = t, this.description = n, this.details = r, this.name = `ApiError`
    }
};

function $t(e) {
    return Qt.has(e)
}

function en(e, t) {
    return $t(e) ? Zt : t
}

function tn(e) {
    return e instanceof N && $t(e.statusCode) ? Zt : null
}

function nn(e) {
    if (!(e instanceof N)) return !1;
    if (e.details?.blockedByResolutionIds?.length) return !0;
    let t = e.description ?? ``;
    return t.includes(`Run required effect actions`) || t.includes(`pending effect resolution`) || t.includes(`blockedByResolutionIds`)
}

async function P(e, t, n = `game`) {
    let r = e.startsWith(`/`) ? e : `/${e}`, i;
    i = n === `game` ? `${Gt()}${r}` : Kt(r);
    let a = localStorage.getItem(w), o = new Headers(t?.headers);
    o.has(`Accept`) || o.set(`Accept`, `application/json`), a && o.set(`Authorization`, `Bearer ${a}`);
    try {
        let e = await fetch(i, {...t, headers: o, credentials: `include`});
        if (e.status === 401) return null;
        if (!e.ok) {
            let t = ``, n = null;
            try {
                t = await e.text(), t && (n = JSON.parse(t))
            } catch {
            }
            let r = n?.statusCode || e.status;
            throw new N(r, n?.statusMessage || e.statusText, en(r, n?.description || t || void 0), n?.details)
        }
        try {
            let t = await e.text();
            return !t || t.trim() === `` ? null : JSON.parse(t)
        } catch {
            return null
        }
    } catch (e) {
        if (e instanceof N) throw e;
        return null
    }
}

async function rn() {
    return Vt() ? P(`/user/me`, {method: `GET`}, `core`) : null
}

async function F() {
    return qe.fetchQuery({queryKey: [`user`], queryFn: rn, staleTime: 1 / 0})
}

async function an(e) {
    let t = await P(`/api/auth/magic-link/exchange`, {
        method: `POST`,
        headers: {"Content-Type": `application/json`},
        body: JSON.stringify({code: e.code, mode: e.mode ?? Xt})
    }, `core`);
    return t?.token && localStorage.setItem(w, t.token), t
}

function on() {
    return null
}

function sn(e) {
    let t = new URL(Kt(`/auth/viewer/${e}/login`)), n = on();
    return n && t.searchParams.set(`redirectTo`, n), t.toString()
}

async function cn(e, t) {
    let n = await F() ? `/mini-game-leaderboard` : `/public/mini-game-leaderboard`, r = new URLSearchParams;
    t !== void 0 && r.set(`limit`, String(t));
    let i = r.toString();
    return P(`${n}/${encodeURIComponent(e)}${i ? `?${i}` : ``}`, {method: `GET`}, `game`)
}

async function ln() {
    return j() ? Pt() : P(`/public/player/stats`, {method: `GET`}, `game`)
}

async function un(e, t) {
    return await F() ? P(`/mini-game-leaderboard/result`, {
        method: `POST`,
        headers: {"Content-Type": `application/json`},
        body: JSON.stringify({miniGameKey: e, score: t})
    }, `game`) : null
}

async function dn() {
    let e = Ut();
    if (!e || A(e)) return null;
    if (j()) return jt(e.id);
    let t = await P(`/player/me`, {method: `GET`}, `game`);
    if (!t?.data?.player) return null;
    let {data: n} = t;
    return k({
        player: n.player,
        currentAuctionResult: n.currentAuctionResult,
        inventoryEffects: n.inventoryEffects ?? n.storedEffects,
        attachedEffects: n.attachedEffects ?? n.activeEffects,
        effectCooldowns: n.effectCooldowns ?? n.player.effectCooldowns,
        auctionResultStats: n.auctionResultStats,
        rollImpactStats: n.rollImpactStats,
        rollKindStats: n.rollKindStats,
        achievements: n.achievements,
        stream: n.stream
    })
}

async function fn() {
    if (j()) return kt();
    let e = await F(), t = await P(e && !A(e) ? `/player/list` : `/public/player/list`, {method: `GET`});
    return !t || !t.data || !t.data.array ? null : t.data.array.map(e => xt(e) ? k(e) : e)
}

async function pn(e) {
    if (j()) return At(e);
    let t = await P(`/player/${encodeURIComponent(e)}`, {method: `GET`}, `game`);
    if (!t?.data?.player) return null;
    let {data: n} = t;
    return k({
        player: n.player,
        currentAuctionResult: n.currentAuctionResult,
        inventoryEffects: n.inventoryEffects ?? n.storedEffects,
        attachedEffects: n.attachedEffects ?? n.activeEffects,
        effectCooldowns: n.effectCooldowns ?? n.player.effectCooldowns,
        auctionResultStats: n.auctionResultStats,
        rollImpactStats: n.rollImpactStats,
        rollKindStats: n.rollKindStats,
        achievements: n.achievements,
        socialLinks: n.socialLinks,
        stream: n.stream
    })
}

async function mn(e) {
    if (j()) return zt(e);
    if (!e || e.length === 0) return null;
    let t = await P(`/game/search?name=${encodeURIComponent(e)}`, {method: `GET`}, `core`);
    return !t || !t.data || !t.data.array ? null : t.data.array
}

async function hn(e) {
    if (j()) return Lt(e);
    let t = await F(),
        n = await P(t && !A(t) ? `/player/${e}/action-logs` : `/public/player/${e}/action-logs`, {method: `GET`});
    return n == null || n.data == null || n.data.logs == null ? null : n.data.logs
}

async function gn(e) {
    if (j()) return Ft(e);
    let t = await F(),
        n = await P(t && !A(t) ? `/player/${e}/auction-result-history` : `/public/player/${e}/auction-results-history`, {method: `GET`});
    return n == null || n.data == null || n.data.array == null ? null : n.data.array
}

async function _n(e) {
    if (j()) return It(e);
    let t = await P(`/public/player/${e}/previous-event-games`, {method: `GET`});
    return t == null || t.data == null || t.data.array == null ? null : t.data.array
}

async function vn() {
    return await P(`/casino/pending`, {method: `GET`}) ?? null
}

async function yn(e) {
    return await P(`/session/turn/auction-result/start`, {
        method: `POST`,
        headers: {"Content-Type": `application/json`},
        body: JSON.stringify(e)
    }), null
}

async function bn(e) {
    return await P(`/session/turn/auction-result/finish`, {
        method: `POST`,
        headers: {"Content-Type": `application/json`},
        body: JSON.stringify(e)
    })
}

async function xn(e) {
    let t = `${Gt()}/session/turn/auction-result/timer`, n = localStorage.getItem(w),
        r = new Headers({Accept: `application/json`, "Content-Type": `application/json`});
    n && r.set(`Authorization`, `Bearer ${n}`);
    let i = await fetch(t, {method: `POST`, headers: r, credentials: `include`, body: JSON.stringify(e)});
    if (i.status === 401) throw new N(401, i.statusText, `Unauthorized`);
    if (!i.ok) {
        let e = ``;
        try {
            e = await i.text()
        } catch {
        }
        throw new N(i.status, i.statusText, en(i.status, e || void 0))
    }
}

async function Sn(e) {
    return await P(`/casino/roll`, {
        method: `POST`,
        headers: {"Content-Type": `application/json`},
        body: JSON.stringify(e)
    }), null
}

function I(e) {
    return typeof e == `object` && !!e
}

function Cn(e) {
    if (!I(e) || typeof e.id != `string`) return null;
    let t = Array.isArray(e.steps) ? e.steps : Array.isArray(e.selectionTrace) ? e.selectionTrace : null;
    return t == null ? null : {...e, steps: t}
}

function wn(e) {
    return I(e) ? e.type === `random-effect` || I(e.wheel) || I(e.step) : !1
}

function Tn(e) {
    if (!I(e) || typeof e.id != `string`) return;
    let t = e.status;
    return {
        id: e.id,
        status: t === `pending` || t === `completed` || t === `cancelled` ? t : `pending`,
        nextStep: typeof e.nextStep == `string` ? e.nextStep : null,
        currentStep: typeof e.currentStep == `string` ? e.currentStep : null
    }
}

function En(e) {
    if (Array.isArray(e)) return e;
    if (!I(e)) return null;
    for (let t of [`array`, `items`, `resolutions`, `list`, `results`]) if (Array.isArray(e[t])) return e[t];
    return null
}

async function Dn(e = {}) {
    let t = await P(`/casino/accept`, {
        method: `POST`,
        headers: {"Content-Type": `application/json`},
        body: JSON.stringify(e)
    });
    if (t == null) return null;
    let n = I(t.data) ? t.data : t, r = I(n.result) ? n.result : n;
    if (typeof r.success != `boolean`) return console.warn(`/casino/accept: unexpected response shape`, t), null;
    let i = n.effectResolution ?? r.effectResolution, a = n.resolution ?? r.resolution, o = Cn(i) ?? Cn(a),
        s = o == null ? Tn(i) ?? Tn(a) : Tn(o),
        c = (I(r.payload) ? r.payload : void 0) ?? (I(n.payload) ? n.payload : void 0) ?? (wn(r) ? r : void 0) ?? (wn(n) ? n : void 0),
        l = Array.isArray(n.affectedPlayerIds) ? n.affectedPlayerIds.filter(e => typeof e == `string`) : Array.isArray(r.affectedPlayerIds) ? r.affectedPlayerIds.filter(e => typeof e == `string`) : [],
        u = typeof n.id == `string` ? n.id : typeof n.rollId == `string` ? n.rollId : typeof r.id == `string` ? r.id : void 0;
    return {
        success: r.success,
        message: typeof r.message == `string` ? r.message : void 0,
        bonus: typeof r.bonus == `number` ? r.bonus : void 0,
        payload: c,
        affectedPlayerIds: l,
        resolution: s,
        fullResolution: o,
        acceptDataId: u
    }
}

async function On() {
    return await P(`/casino/reroll`, {
        method: `POST`,
        headers: {"Content-Type": `application/json`},
        body: JSON.stringify({})
    }), null
}

async function kn() {
    return (await P(`/casino/regular/finish`, {
        method: `POST`,
        headers: {"Content-Type": `application/json`},
        body: JSON.stringify({})
    }))?.data?.casinoPhase ?? null
}

async function An() {
    return await P(`/session/turn/finish`, {method: `POST`}), null
}

async function jn(e) {
    let t = await P(`/effect-instance/activate`, {
        method: `POST`,
        headers: {"Content-Type": `application/json`},
        body: JSON.stringify(e)
    });
    if (!t?.data) return null;
    let {data: n} = t;
    return {success: n.result.success, name: n.name, message: n.result.message, affectedPlayerIds: n.affectedPlayerIds}
}

async function Mn() {
    let e = await P(`/effect-resolution/pending`, {method: `GET`});
    return e == null ? null : (e.data == null ? null : En(e.data)) ?? En(e)
}

async function Nn(e) {
    return (await P(`/effect-resolution/resolve`, {
        method: `POST`,
        headers: {"Content-Type": `application/json`},
        body: JSON.stringify(e)
    }))?.data ?? null
}

async function Pn(e) {
    return await P(`/judge/achievement/grant`, {
        method: `POST`,
        headers: {"Content-Type": `application/json`},
        body: JSON.stringify(e)
    }), null
}

async function Fn(e, t) {
    if (e.role !== `judge`) return;
    let n = t === `gnus` ? `gnus` : t === `platinum` ? `platinum` : `regular`;
    e.status === `casino` && e.casinoPhase === n || await Ln({...In(e), status: `casino`, casinoPhase: n})
}

function In(e) {
    return {
        playerId: e.id,
        name: e.name,
        status: e.status,
        casinoPhase: e.status === `casino` ? e.casinoPhase ?? `regular` : null,
        turn: e.turn,
        ggp: e.ggp,
        gameStreak: e.gameStreak,
        platinumChips: e.platinumChips,
        gnusAvailable: e.gnusAvailable ?? 0,
        regularChipsSpentInTurn: e.regularChipsSpentInTurn,
        regularChipsSpentTotal: e.regularChipsSpentTotal,
        platinumChipsSpentTotal: e.platinumChipsSpentTotal
    }
}

async function Ln(e) {
    let t = await P(`/judge/player/update`, {
        method: `PATCH`,
        headers: {"Content-Type": `application/json`},
        body: JSON.stringify(e)
    }, `game`);
    if (!t?.data?.player) return null;
    let {data: n} = t;
    return k({
        player: n.player,
        currentAuctionResult: n.currentAuctionResult,
        inventoryEffects: n.inventoryEffects ?? n.storedEffects,
        attachedEffects: n.attachedEffects ?? n.activeEffects,
        effectCooldowns: n.effectCooldowns ?? n.player.effectCooldowns,
        auctionResultStats: n.auctionResultStats,
        rollImpactStats: n.rollImpactStats,
        rollKindStats: n.rollKindStats,
        achievements: n.achievements,
        socialLinks: n.socialLinks,
        stream: n.stream
    })
}

async function Rn(e) {
    return (await P(`/judge/auction-result-score-adjustment/add`, {
        method: `POST`,
        headers: {"Content-Type": `application/json`},
        body: JSON.stringify(e)
    }))?.data?.adjustment ?? null
}

async function zn(e) {
    return await P(`/judge/auction-result-score-adjustment/remove`, {
        method: `POST`,
        headers: {"Content-Type": `application/json`},
        body: JSON.stringify(e)
    }) !== null
}

async function Bn() {
    if (j()) return Mt();
    let e = await F(), t = await P(e && !A(e) ? `/effect/list` : `/public/effect/list`, {method: `GET`});
    return !t || !t.data || !t.data.array ? null : t.data.array
}

async function Vn() {
    if (j()) return Nt();
    let e = await F(), t = await P(e && !A(e) ? `/achievements` : `/public/achievements`, {method: `GET`});
    return !t || !t.data || !t.data.achievements ? null : {
        achievements: t.data.achievements,
        totalCount: t.data.totalCount,
        revealedCount: t.data.revealedCount
    }
}

var L = Object.create(null);
L.open = `0`, L.close = `1`, L.ping = `2`, L.pong = `3`, L.message = `4`, L.upgrade = `5`, L.noop = `6`;
var R = Object.create(null);
Object.keys(L).forEach(e => {
    R[L[e]] = e
});
var Hn = {type: `error`, data: `parser error`},
    Un = typeof Blob == `function` || typeof Blob < `u` && Object.prototype.toString.call(Blob) === `[object BlobConstructor]`,
    Wn = typeof ArrayBuffer == `function`,
    Gn = e => typeof ArrayBuffer.isView == `function` ? ArrayBuffer.isView(e) : e && e.buffer instanceof ArrayBuffer,
    Kn = ({
              type: e,
              data: t
          }, n, r) => Un && t instanceof Blob ? n ? r(t) : qn(t, r) : Wn && (t instanceof ArrayBuffer || Gn(t)) ? n ? r(t) : qn(new Blob([t]), r) : r(L[e] + (t || ``)),
    qn = (e, t) => {
        let n = new FileReader;
        return n.onload = function () {
            let e = n.result.split(`,`)[1];
            t(`b` + (e || ``))
        }, n.readAsDataURL(e)
    };

function Jn(e) {
    return e instanceof Uint8Array ? e : e instanceof ArrayBuffer ? new Uint8Array(e) : new Uint8Array(e.buffer, e.byteOffset, e.byteLength)
}

var Yn;

function Xn(e, t) {
    if (Un && e.data instanceof Blob) return e.data.arrayBuffer().then(Jn).then(t);
    if (Wn && (e.data instanceof ArrayBuffer || Gn(e.data))) return t(Jn(e.data));
    Kn(e, !1, e => {
        Yn ||= new TextEncoder, t(Yn.encode(e))
    })
}

var Zn = `ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/`,
    z = typeof Uint8Array > `u` ? [] : new Uint8Array(256);
for (let e = 0; e < 64; e++) z[Zn.charCodeAt(e)] = e;
var Qn = e => {
    let t = e.length * .75, n = e.length, r, i = 0, a, o, s, c;
    e[e.length - 1] === `=` && (t--, e[e.length - 2] === `=` && t--);
    let l = new ArrayBuffer(t), u = new Uint8Array(l);
    for (r = 0; r < n; r += 4) a = z[e.charCodeAt(r)], o = z[e.charCodeAt(r + 1)], s = z[e.charCodeAt(r + 2)], c = z[e.charCodeAt(r + 3)], u[i++] = a << 2 | o >> 4, u[i++] = (o & 15) << 4 | s >> 2, u[i++] = (s & 3) << 6 | c & 63;
    return l
}, $n = typeof ArrayBuffer == `function`, er = (e, t) => {
    if (typeof e != `string`) return {type: `message`, data: nr(e, t)};
    let n = e.charAt(0);
    return n === `b` ? {type: `message`, data: tr(e.substring(1), t)} : R[n] ? e.length > 1 ? {
        type: R[n],
        data: e.substring(1)
    } : {type: R[n]} : Hn
}, tr = (e, t) => $n ? nr(Qn(e), t) : {base64: !0, data: e}, nr = (e, t) => {
    switch (t) {
        case`blob`:
            return e instanceof Blob ? e : new Blob([e]);
        default:
            return e instanceof ArrayBuffer ? e : e.buffer
    }
}, rr = ``, ir = (e, t) => {
    let n = e.length, r = Array(n), i = 0;
    e.forEach((e, a) => {
        Kn(e, !1, e => {
            r[a] = e, ++i === n && t(r.join(rr))
        })
    })
}, ar = (e, t) => {
    let n = e.split(rr), r = [];
    for (let e = 0; e < n.length; e++) {
        let i = er(n[e], t);
        if (r.push(i), i.type === `error`) break
    }
    return r
};

function or() {
    return new TransformStream({
        transform(e, t) {
            Xn(e, n => {
                let r = n.length, i;
                if (r < 126) i = new Uint8Array(1), new DataView(i.buffer).setUint8(0, r); else if (r < 65536) {
                    i = new Uint8Array(3);
                    let e = new DataView(i.buffer);
                    e.setUint8(0, 126), e.setUint16(1, r)
                } else {
                    i = new Uint8Array(9);
                    let e = new DataView(i.buffer);
                    e.setUint8(0, 127), e.setBigUint64(1, BigInt(r))
                }
                e.data && typeof e.data != `string` && (i[0] |= 128), t.enqueue(i), t.enqueue(n)
            })
        }
    })
}

var sr;

function B(e) {
    return e.reduce((e, t) => e + t.length, 0)
}

function V(e, t) {
    if (e[0].length === t) return e.shift();
    let n = new Uint8Array(t), r = 0;
    for (let i = 0; i < t; i++) n[i] = e[0][r++], r === e[0].length && (e.shift(), r = 0);
    return e.length && r < e[0].length && (e[0] = e[0].slice(r)), n
}

function cr(e, t) {
    sr ||= new TextDecoder;
    let n = [], r = 0, i = -1, a = !1;
    return new TransformStream({
        transform(o, s) {
            for (n.push(o); ;) {
                if (r === 0) {
                    if (B(n) < 1) break;
                    let e = V(n, 1);
                    a = (e[0] & 128) == 128, i = e[0] & 127, r = i < 126 ? 3 : i === 126 ? 1 : 2
                } else if (r === 1) {
                    if (B(n) < 2) break;
                    let e = V(n, 2);
                    i = new DataView(e.buffer, e.byteOffset, e.length).getUint16(0), r = 3
                } else if (r === 2) {
                    if (B(n) < 8) break;
                    let e = V(n, 8), t = new DataView(e.buffer, e.byteOffset, e.length), a = t.getUint32(0);
                    if (a > 2 ** 21 - 1) {
                        s.enqueue(Hn);
                        break
                    }
                    i = a * 2 ** 32 + t.getUint32(4), r = 3
                } else {
                    if (B(n) < i) break;
                    let e = V(n, i);
                    s.enqueue(er(a ? e : sr.decode(e), t)), r = 0
                }
                if (i === 0 || i > e) {
                    s.enqueue(Hn);
                    break
                }
            }
        }
    })
}

function H(e) {
    if (e) return lr(e)
}

function lr(e) {
    for (var t in H.prototype) e[t] = H.prototype[t];
    return e
}

H.prototype.on = H.prototype.addEventListener = function (e, t) {
    return this._callbacks = this._callbacks || {}, (this._callbacks[`$` + e] = this._callbacks[`$` + e] || []).push(t), this
}, H.prototype.once = function (e, t) {
    function n() {
        this.off(e, n), t.apply(this, arguments)
    }

    return n.fn = t, this.on(e, n), this
}, H.prototype.off = H.prototype.removeListener = H.prototype.removeAllListeners = H.prototype.removeEventListener = function (e, t) {
    if (this._callbacks = this._callbacks || {}, arguments.length == 0) return this._callbacks = {}, this;
    var n = this._callbacks[`$` + e];
    if (!n) return this;
    if (arguments.length == 1) return delete this._callbacks[`$` + e], this;
    for (var r, i = 0; i < n.length; i++) if (r = n[i], r === t || r.fn === t) {
        n.splice(i, 1);
        break
    }
    return n.length === 0 && delete this._callbacks[`$` + e], this
}, H.prototype.emit = function (e) {
    this._callbacks = this._callbacks || {};
    for (var t = Array(arguments.length - 1), n = this._callbacks[`$` + e], r = 1; r < arguments.length; r++) t[r - 1] = arguments[r];
    if (n) {
        n = n.slice(0);
        for (var r = 0, i = n.length; r < i; ++r) n[r].apply(this, t)
    }
    return this
}, H.prototype.emitReserved = H.prototype.emit, H.prototype.listeners = function (e) {
    return this._callbacks = this._callbacks || {}, this._callbacks[`$` + e] || []
}, H.prototype.hasListeners = function (e) {
    return !!this.listeners(e).length
};
var ur = typeof Promise == `function` && typeof Promise.resolve == `function` ? e => Promise.resolve().then(e) : (e, t) => t(e, 0),
    U = typeof self < `u` ? self : typeof window < `u` ? window : Function(`return this`)(), dr = `arraybuffer`;

function fr(e, ...t) {
    return t.reduce((t, n) => (e.hasOwnProperty(n) && (t[n] = e[n]), t), {})
}

var pr = U.setTimeout, mr = U.clearTimeout;

function W(e, t) {
    t.useNativeTimers ? (e.setTimeoutFn = pr.bind(U), e.clearTimeoutFn = mr.bind(U)) : (e.setTimeoutFn = U.setTimeout.bind(U), e.clearTimeoutFn = U.clearTimeout.bind(U))
}

var hr = 1.33;

function gr(e) {
    return typeof e == `string` ? _r(e) : Math.ceil((e.byteLength || e.size) * hr)
}

function _r(e) {
    let t = 0, n = 0;
    for (let r = 0, i = e.length; r < i; r++) t = e.charCodeAt(r), t < 128 ? n += 1 : t < 2048 ? n += 2 : t < 55296 || t >= 57344 ? n += 3 : (r++, n += 4);
    return n
}

function vr() {
    return Date.now().toString(36).substring(3) + Math.random().toString(36).substring(2, 5)
}

function yr(e) {
    let t = ``;
    for (let n in e) e.hasOwnProperty(n) && (t.length && (t += `&`), t += encodeURIComponent(n) + `=` + encodeURIComponent(e[n]));
    return t
}

function br(e) {
    let t = {}, n = e.split(`&`);
    for (let e = 0, r = n.length; e < r; e++) {
        let r = n[e].split(`=`);
        t[decodeURIComponent(r[0])] = decodeURIComponent(r[1])
    }
    return t
}

var xr = class extends Error {
    constructor(e, t, n) {
        super(e), this.description = t, this.context = n, this.type = `TransportError`
    }
}, Sr = class extends H {
    constructor(e) {
        super(), this.writable = !1, W(this, e), this.opts = e, this.query = e.query, this.socket = e.socket, this.supportsBinary = !e.forceBase64
    }

    onError(e, t, n) {
        return super.emitReserved(`error`, new xr(e, t, n)), this
    }

    open() {
        return this.readyState = `opening`, this.doOpen(), this
    }

    close() {
        return (this.readyState === `opening` || this.readyState === `open`) && (this.doClose(), this.onClose()), this
    }

    send(e) {
        this.readyState === `open` && this.write(e)
    }

    onOpen() {
        this.readyState = `open`, this.writable = !0, super.emitReserved(`open`)
    }

    onData(e) {
        let t = er(e, this.socket.binaryType);
        this.onPacket(t)
    }

    onPacket(e) {
        super.emitReserved(`packet`, e)
    }

    onClose(e) {
        this.readyState = `closed`, super.emitReserved(`close`, e)
    }

    pause(e) {
    }

    createUri(e, t = {}) {
        return e + `://` + this._hostname() + this._port() + this.opts.path + this._query(t)
    }

    _hostname() {
        let e = this.opts.hostname;
        return e.indexOf(`:`) === -1 ? e : `[` + e + `]`
    }

    _port() {
        return this.opts.port && (this.opts.secure && Number(this.opts.port !== 443) || !this.opts.secure && Number(this.opts.port) !== 80) ? `:` + this.opts.port : ``
    }

    _query(e) {
        let t = yr(e);
        return t.length ? `?` + t : ``
    }
}, Cr = class extends Sr {
    constructor() {
        super(...arguments), this._polling = !1
    }

    get name() {
        return `polling`
    }

    doOpen() {
        this._poll()
    }

    pause(e) {
        this.readyState = `pausing`;
        let t = () => {
            this.readyState = `paused`, e()
        };
        if (this._polling || !this.writable) {
            let e = 0;
            this._polling && (e++, this.once(`pollComplete`, function () {
                --e || t()
            })), this.writable || (e++, this.once(`drain`, function () {
                --e || t()
            }))
        } else t()
    }

    _poll() {
        this._polling = !0, this.doPoll(), this.emitReserved(`poll`)
    }

    onData(e) {
        ar(e, this.socket.binaryType).forEach(e => {
            if (this.readyState === `opening` && e.type === `open` && this.onOpen(), e.type === `close`) return this.onClose({description: `transport closed by the server`}), !1;
            this.onPacket(e)
        }), this.readyState !== `closed` && (this._polling = !1, this.emitReserved(`pollComplete`), this.readyState === `open` && this._poll())
    }

    doClose() {
        let e = () => {
            this.write([{type: `close`}])
        };
        this.readyState === `open` ? e() : this.once(`open`, e)
    }

    write(e) {
        this.writable = !1, ir(e, e => {
            this.doWrite(e, () => {
                this.writable = !0, this.emitReserved(`drain`)
            })
        })
    }

    uri() {
        let e = this.opts.secure ? `https` : `http`, t = this.query || {};
        return !1 !== this.opts.timestampRequests && (t[this.opts.timestampParam] = vr()), !this.supportsBinary && !t.sid && (t.b64 = 1), this.createUri(e, t)
    }
}, wr = !1;
try {
    wr = typeof XMLHttpRequest < `u` && `withCredentials` in new XMLHttpRequest
} catch {
}
var Tr = wr;

function Er() {
}

var Dr = class extends Cr {
    constructor(e) {
        if (super(e), typeof location < `u`) {
            let t = location.protocol === `https:`, n = location.port;
            n ||= t ? `443` : `80`, this.xd = typeof location < `u` && e.hostname !== location.hostname || n !== e.port
        }
    }

    doWrite(e, t) {
        let n = this.request({method: `POST`, data: e});
        n.on(`success`, t), n.on(`error`, (e, t) => {
            this.onError(`xhr post error`, e, t)
        })
    }

    doPoll() {
        let e = this.request();
        e.on(`data`, this.onData.bind(this)), e.on(`error`, (e, t) => {
            this.onError(`xhr poll error`, e, t)
        }), this.pollXhr = e
    }
}, G = class e extends H {
    constructor(e, t, n) {
        super(), this.createRequest = e, W(this, n), this._opts = n, this._method = n.method || `GET`, this._uri = t, this._data = n.data === void 0 ? null : n.data, this._create()
    }

    _create() {
        var t;
        let n = fr(this._opts, `agent`, `pfx`, `key`, `passphrase`, `cert`, `ca`, `ciphers`, `rejectUnauthorized`, `autoUnref`);
        n.xdomain = !!this._opts.xd;
        let r = this._xhr = this.createRequest(n);
        try {
            r.open(this._method, this._uri, !0);
            try {
                if (this._opts.extraHeaders) {
                    r.setDisableHeaderCheck && r.setDisableHeaderCheck(!0);
                    for (let e in this._opts.extraHeaders) this._opts.extraHeaders.hasOwnProperty(e) && r.setRequestHeader(e, this._opts.extraHeaders[e])
                }
            } catch {
            }
            if (this._method === `POST`) try {
                r.setRequestHeader(`Content-type`, `text/plain;charset=UTF-8`)
            } catch {
            }
            try {
                r.setRequestHeader(`Accept`, `*/*`)
            } catch {
            }
            (t = this._opts.cookieJar) == null || t.addCookies(r), `withCredentials` in r && (r.withCredentials = this._opts.withCredentials), this._opts.requestTimeout && (r.timeout = this._opts.requestTimeout), r.onreadystatechange = () => {
                var e;
                r.readyState === 3 && ((e = this._opts.cookieJar) == null || e.parseCookies(r.getResponseHeader(`set-cookie`))), r.readyState === 4 && (r.status === 200 || r.status === 1223 ? this._onLoad() : this.setTimeoutFn(() => {
                    this._onError(typeof r.status == `number` ? r.status : 0)
                }, 0))
            }, r.send(this._data)
        } catch (e) {
            this.setTimeoutFn(() => {
                this._onError(e)
            }, 0);
            return
        }
        typeof document < `u` && (this._index = e.requestsCount++, e.requests[this._index] = this)
    }

    _onError(e) {
        this.emitReserved(`error`, e, this._xhr), this._cleanup(!0)
    }

    _cleanup(t) {
        if (!(this._xhr === void 0 || this._xhr === null)) {
            if (this._xhr.onreadystatechange = Er, t) try {
                this._xhr.abort()
            } catch {
            }
            typeof document < `u` && delete e.requests[this._index], this._xhr = null
        }
    }

    _onLoad() {
        let e = this._xhr.responseText;
        e !== null && (this.emitReserved(`data`, e), this.emitReserved(`success`), this._cleanup())
    }

    abort() {
        this._cleanup()
    }
};
if (G.requestsCount = 0, G.requests = {}, typeof document < `u`) {
    if (typeof attachEvent == `function`) attachEvent(`onunload`, Or); else if (typeof addEventListener == `function`) {
        let e = `onpagehide` in U ? `pagehide` : `unload`;
        addEventListener(e, Or, !1)
    }
}

function Or() {
    for (let e in G.requests) G.requests.hasOwnProperty(e) && G.requests[e].abort()
}

var kr = (function () {
    let e = jr({xdomain: !1});
    return e && e.responseType !== null
})(), Ar = class extends Dr {
    constructor(e) {
        super(e);
        let t = e && e.forceBase64;
        this.supportsBinary = kr && !t
    }

    request(e = {}) {
        return Object.assign(e, {xd: this.xd}, this.opts), new G(jr, this.uri(), e)
    }
};

function jr(e) {
    let t = e.xdomain;
    try {
        if (typeof XMLHttpRequest < `u` && (!t || Tr)) return new XMLHttpRequest
    } catch {
    }
    if (!t) try {
        return new U[[`Active`, `Object`].join(`X`)](`Microsoft.XMLHTTP`)
    } catch {
    }
}

var Mr = typeof navigator < `u` && typeof navigator.product == `string` && navigator.product.toLowerCase() === `reactnative`,
    Nr = class extends Sr {
        get name() {
            return `websocket`
        }

        doOpen() {
            let e = this.uri(), t = this.opts.protocols,
                n = Mr ? {} : fr(this.opts, `agent`, `perMessageDeflate`, `pfx`, `key`, `passphrase`, `cert`, `ca`, `ciphers`, `rejectUnauthorized`, `localAddress`, `protocolVersion`, `origin`, `maxPayload`, `family`, `checkServerIdentity`);
            this.opts.extraHeaders && (n.headers = this.opts.extraHeaders);
            try {
                this.ws = this.createSocket(e, t, n)
            } catch (e) {
                return this.emitReserved(`error`, e)
            }
            this.ws.binaryType = this.socket.binaryType, this.addEventListeners()
        }

        addEventListeners() {
            this.ws.onopen = () => {
                this.opts.autoUnref && this.ws._socket.unref(), this.onOpen()
            }, this.ws.onclose = e => this.onClose({
                description: `websocket connection closed`,
                context: e
            }), this.ws.onmessage = e => this.onData(e.data), this.ws.onerror = e => this.onError(`websocket error`, e)
        }

        write(e) {
            this.writable = !1;
            for (let t = 0; t < e.length; t++) {
                let n = e[t], r = t === e.length - 1;
                Kn(n, this.supportsBinary, e => {
                    try {
                        this.doWrite(n, e)
                    } catch {
                    }
                    r && ur(() => {
                        this.writable = !0, this.emitReserved(`drain`)
                    }, this.setTimeoutFn)
                })
            }
        }

        doClose() {
            this.ws !== void 0 && (this.ws.onerror = () => {
            }, this.ws.close(), this.ws = null)
        }

        uri() {
            let e = this.opts.secure ? `wss` : `ws`, t = this.query || {};
            return this.opts.timestampRequests && (t[this.opts.timestampParam] = vr()), this.supportsBinary || (t.b64 = 1), this.createUri(e, t)
        }
    }, Pr = U.WebSocket || U.MozWebSocket, Fr = {
        websocket: class extends Nr {
            createSocket(e, t, n) {
                return Mr ? new Pr(e, t, n) : t ? new Pr(e, t) : new Pr(e)
            }

            doWrite(e, t) {
                this.ws.send(t)
            }
        }, webtransport: class extends Sr {
            get name() {
                return `webtransport`
            }

            doOpen() {
                try {
                    this._transport = new WebTransport(this.createUri(`https`), this.opts.transportOptions[this.name])
                } catch (e) {
                    return this.emitReserved(`error`, e)
                }
                this._transport.closed.then(() => {
                    this.onClose()
                }).catch(e => {
                    this.onError(`webtransport error`, e)
                }), this._transport.ready.then(() => {
                    this._transport.createBidirectionalStream().then(e => {
                        let t = cr(2 ** 53 - 1, this.socket.binaryType), n = e.readable.pipeThrough(t).getReader(),
                            r = or();
                        r.readable.pipeTo(e.writable), this._writer = r.writable.getWriter();
                        let i = () => {
                            n.read().then(({done: e, value: t}) => {
                                e || (this.onPacket(t), i())
                            }).catch(e => {
                            })
                        };
                        i();
                        let a = {type: `open`};
                        this.query.sid && (a.data = `{"sid":"${this.query.sid}"}`), this._writer.write(a).then(() => this.onOpen())
                    })
                })
            }

            write(e) {
                this.writable = !1;
                for (let t = 0; t < e.length; t++) {
                    let n = e[t], r = t === e.length - 1;
                    this._writer.write(n).then(() => {
                        r && ur(() => {
                            this.writable = !0, this.emitReserved(`drain`)
                        }, this.setTimeoutFn)
                    })
                }
            }

            doClose() {
                var e;
                (e = this._transport) == null || e.close()
            }
        }, polling: Ar
    },
    Ir = /^(?:(?![^:@\/?#]+:[^:@\/]*@)(http|https|ws|wss):\/\/)?((?:(([^:@\/?#]*)(?::([^:@\/?#]*))?)?@)?((?:[a-f0-9]{0,4}:){2,7}[a-f0-9]{0,4}|[^:\/?#]*)(?::(\d*))?)(((\/(?:[^?#](?![^?#\/]*\.[^?#\/.]+(?:[?#]|$)))*\/?)?([^?#\/]*))(?:\?([^#]*))?(?:#(.*))?)/,
    Lr = [`source`, `protocol`, `authority`, `userInfo`, `user`, `password`, `host`, `port`, `relative`, `path`, `directory`, `file`, `query`, `anchor`];

function Rr(e) {
    if (e.length > 8e3) throw `URI too long`;
    let t = e, n = e.indexOf(`[`), r = e.indexOf(`]`);
    n != -1 && r != -1 && (e = e.substring(0, n) + e.substring(n, r).replace(/:/g, `;`) + e.substring(r, e.length));
    let i = Ir.exec(e || ``), a = {}, o = 14;
    for (; o--;) a[Lr[o]] = i[o] || ``;
    return n != -1 && r != -1 && (a.source = t, a.host = a.host.substring(1, a.host.length - 1).replace(/;/g, `:`), a.authority = a.authority.replace(`[`, ``).replace(`]`, ``).replace(/;/g, `:`), a.ipv6uri = !0), a.pathNames = zr(a, a.path), a.queryKey = Br(a, a.query), a
}

function zr(e, t) {
    let n = t.replace(/\/{2,9}/g, `/`).split(`/`);
    return (t.slice(0, 1) == `/` || t.length === 0) && n.splice(0, 1), t.slice(-1) == `/` && n.splice(n.length - 1, 1), n
}

function Br(e, t) {
    let n = {};
    return t.replace(/(?:^|&)([^&=]*)=?([^&]*)/g, function (e, t, r) {
        t && (n[t] = r)
    }), n
}

var Vr = typeof addEventListener == `function` && typeof removeEventListener == `function`, K = [];
Vr && addEventListener(`offline`, () => {
    K.forEach(e => e())
}, !1);
var q = class e extends H {
    constructor(e, t) {
        if (super(), this.binaryType = dr, this.writeBuffer = [], this._prevBufferLen = 0, this._pingInterval = -1, this._pingTimeout = -1, this._maxPayload = -1, this._pingTimeoutTime = 1 / 0, e && typeof e == `object` && (t = e, e = null), e) {
            let n = Rr(e);
            t.hostname = n.host, t.secure = n.protocol === `https` || n.protocol === `wss`, t.port = n.port, n.query && (t.query = n.query)
        } else t.host && (t.hostname = Rr(t.host).host);
        W(this, t), this.secure = t.secure == null ? typeof location < `u` && location.protocol === `https:` : t.secure, t.hostname && !t.port && (t.port = this.secure ? `443` : `80`), this.hostname = t.hostname || (typeof location < `u` ? location.hostname : `localhost`), this.port = t.port || (typeof location < `u` && location.port ? location.port : this.secure ? `443` : `80`), this.transports = [], this._transportsByName = {}, t.transports.forEach(e => {
            let t = e.prototype.name;
            this.transports.push(t), this._transportsByName[t] = e
        }), this.opts = Object.assign({
            path: `/engine.io`,
            agent: !1,
            withCredentials: !1,
            upgrade: !0,
            timestampParam: `t`,
            rememberUpgrade: !1,
            addTrailingSlash: !0,
            rejectUnauthorized: !0,
            perMessageDeflate: {threshold: 1024},
            transportOptions: {},
            closeOnBeforeunload: !1
        }, t), this.opts.path = this.opts.path.replace(/\/$/, ``) + (this.opts.addTrailingSlash ? `/` : ``), typeof this.opts.query == `string` && (this.opts.query = br(this.opts.query)), Vr && (this.opts.closeOnBeforeunload && (this._beforeunloadEventListener = () => {
            this.transport && (this.transport.removeAllListeners(), this.transport.close())
        }, addEventListener(`beforeunload`, this._beforeunloadEventListener, !1)), this.hostname !== `localhost` && (this._offlineEventListener = () => {
            this._onClose(`transport close`, {description: `network connection lost`})
        }, K.push(this._offlineEventListener))), this.opts.withCredentials && (this._cookieJar = void 0), this._open()
    }

    createTransport(e) {
        let t = Object.assign({}, this.opts.query);
        t.EIO = 4, t.transport = e, this.id && (t.sid = this.id);
        let n = Object.assign({}, this.opts, {
            query: t,
            socket: this,
            hostname: this.hostname,
            secure: this.secure,
            port: this.port
        }, this.opts.transportOptions[e]);
        return new this._transportsByName[e](n)
    }

    _open() {
        if (this.transports.length === 0) {
            this.setTimeoutFn(() => {
                this.emitReserved(`error`, `No transports available`)
            }, 0);
            return
        }
        let t = this.opts.rememberUpgrade && e.priorWebsocketSuccess && this.transports.indexOf(`websocket`) !== -1 ? `websocket` : this.transports[0];
        this.readyState = `opening`;
        let n = this.createTransport(t);
        n.open(), this.setTransport(n)
    }

    setTransport(e) {
        this.transport && this.transport.removeAllListeners(), this.transport = e, e.on(`drain`, this._onDrain.bind(this)).on(`packet`, this._onPacket.bind(this)).on(`error`, this._onError.bind(this)).on(`close`, e => this._onClose(`transport close`, e))
    }

    onOpen() {
        this.readyState = `open`, e.priorWebsocketSuccess = this.transport.name === `websocket`, this.emitReserved(`open`), this.flush()
    }

    _onPacket(e) {
        if (this.readyState === `opening` || this.readyState === `open` || this.readyState === `closing`) switch (this.emitReserved(`packet`, e), this.emitReserved(`heartbeat`), e.type) {
            case`open`:
                this.onHandshake(JSON.parse(e.data));
                break;
            case`ping`:
                this._sendPacket(`pong`), this.emitReserved(`ping`), this.emitReserved(`pong`), this._resetPingTimeout();
                break;
            case`error`:
                let t = Error(`server error`);
                t.code = e.data, this._onError(t);
                break;
            case`message`:
                this.emitReserved(`data`, e.data), this.emitReserved(`message`, e.data);
                break
        }
    }

    onHandshake(e) {
        this.emitReserved(`handshake`, e), this.id = e.sid, this.transport.query.sid = e.sid, this._pingInterval = e.pingInterval, this._pingTimeout = e.pingTimeout, this._maxPayload = e.maxPayload, this.onOpen(), this.readyState !== `closed` && this._resetPingTimeout()
    }

    _resetPingTimeout() {
        this.clearTimeoutFn(this._pingTimeoutTimer);
        let e = this._pingInterval + this._pingTimeout;
        this._pingTimeoutTime = Date.now() + e, this._pingTimeoutTimer = this.setTimeoutFn(() => {
            this._onClose(`ping timeout`)
        }, e), this.opts.autoUnref && this._pingTimeoutTimer.unref()
    }

    _onDrain() {
        this.writeBuffer.splice(0, this._prevBufferLen), this._prevBufferLen = 0, this.writeBuffer.length === 0 ? this.emitReserved(`drain`) : this.flush()
    }

    flush() {
        if (this.readyState !== `closed` && this.transport.writable && !this.upgrading && this.writeBuffer.length) {
            let e = this._getWritablePackets();
            this.transport.send(e), this._prevBufferLen = e.length, this.emitReserved(`flush`)
        }
    }

    _getWritablePackets() {
        if (!(this._maxPayload && this.transport.name === `polling` && this.writeBuffer.length > 1)) return this.writeBuffer;
        let e = 1;
        for (let t = 0; t < this.writeBuffer.length; t++) {
            let n = this.writeBuffer[t].data;
            if (n && (e += gr(n)), t > 0 && e > this._maxPayload) return this.writeBuffer.slice(0, t);
            e += 2
        }
        return this.writeBuffer
    }

    _hasPingExpired() {
        if (!this._pingTimeoutTime) return !0;
        let e = Date.now() > this._pingTimeoutTime;
        return e && (this._pingTimeoutTime = 0, ur(() => {
            this._onClose(`ping timeout`)
        }, this.setTimeoutFn)), e
    }

    write(e, t, n) {
        return this._sendPacket(`message`, e, t, n), this
    }

    send(e, t, n) {
        return this._sendPacket(`message`, e, t, n), this
    }

    _sendPacket(e, t, n, r) {
        if (typeof t == `function` && (r = t, t = void 0), typeof n == `function` && (r = n, n = null), this.readyState === `closing` || this.readyState === `closed`) return;
        n ||= {}, n.compress = !1 !== n.compress;
        let i = {type: e, data: t, options: n};
        this.emitReserved(`packetCreate`, i), this.writeBuffer.push(i), r && this.once(`flush`, r), this.flush()
    }

    close() {
        let e = () => {
            this._onClose(`forced close`), this.transport.close()
        }, t = () => {
            this.off(`upgrade`, t), this.off(`upgradeError`, t), e()
        }, n = () => {
            this.once(`upgrade`, t), this.once(`upgradeError`, t)
        };
        return (this.readyState === `opening` || this.readyState === `open`) && (this.readyState = `closing`, this.writeBuffer.length ? this.once(`drain`, () => {
            this.upgrading ? n() : e()
        }) : this.upgrading ? n() : e()), this
    }

    _onError(t) {
        if (e.priorWebsocketSuccess = !1, this.opts.tryAllTransports && this.transports.length > 1 && this.readyState === `opening`) return this.transports.shift(), this._open();
        this.emitReserved(`error`, t), this._onClose(`transport error`, t)
    }

    _onClose(e, t) {
        if (this.readyState === `opening` || this.readyState === `open` || this.readyState === `closing`) {
            if (this.clearTimeoutFn(this._pingTimeoutTimer), this.transport.removeAllListeners(`close`), this.transport.close(), this.transport.removeAllListeners(), Vr && (this._beforeunloadEventListener && removeEventListener(`beforeunload`, this._beforeunloadEventListener, !1), this._offlineEventListener)) {
                let e = K.indexOf(this._offlineEventListener);
                e !== -1 && K.splice(e, 1)
            }
            this.readyState = `closed`, this.id = null, this.emitReserved(`close`, e, t), this.writeBuffer = [], this._prevBufferLen = 0
        }
    }
};
q.protocol = 4;
var Hr = class extends q {
    constructor() {
        super(...arguments), this._upgrades = []
    }

    onOpen() {
        if (super.onOpen(), this.readyState === `open` && this.opts.upgrade) for (let e = 0; e < this._upgrades.length; e++) this._probe(this._upgrades[e])
    }

    _probe(e) {
        let t = this.createTransport(e), n = !1;
        q.priorWebsocketSuccess = !1;
        let r = () => {
            n || (t.send([{type: `ping`, data: `probe`}]), t.once(`packet`, e => {
                if (!n) if (e.type === `pong` && e.data === `probe`) {
                    if (this.upgrading = !0, this.emitReserved(`upgrading`, t), !t) return;
                    q.priorWebsocketSuccess = t.name === `websocket`, this.transport.pause(() => {
                        n || this.readyState !== `closed` && (l(), this.setTransport(t), t.send([{type: `upgrade`}]), this.emitReserved(`upgrade`, t), t = null, this.upgrading = !1, this.flush())
                    })
                } else {
                    let e = Error(`probe error`);
                    e.transport = t.name, this.emitReserved(`upgradeError`, e)
                }
            }))
        };

        function i() {
            n || (n = !0, l(), t.close(), t = null)
        }

        let a = e => {
            let n = Error(`probe error: ` + e);
            n.transport = t.name, i(), this.emitReserved(`upgradeError`, n)
        };

        function o() {
            a(`transport closed`)
        }

        function s() {
            a(`socket closed`)
        }

        function c(e) {
            t && e.name !== t.name && i()
        }

        let l = () => {
            t.removeListener(`open`, r), t.removeListener(`error`, a), t.removeListener(`close`, o), this.off(`close`, s), this.off(`upgrading`, c)
        };
        t.once(`open`, r), t.once(`error`, a), t.once(`close`, o), this.once(`close`, s), this.once(`upgrading`, c), this._upgrades.indexOf(`webtransport`) !== -1 && e !== `webtransport` ? this.setTimeoutFn(() => {
            n || t.open()
        }, 200) : t.open()
    }

    onHandshake(e) {
        this._upgrades = this._filterUpgrades(e.upgrades), super.onHandshake(e)
    }

    _filterUpgrades(e) {
        let t = [];
        for (let n = 0; n < e.length; n++) ~this.transports.indexOf(e[n]) && t.push(e[n]);
        return t
    }
}, Ur = class extends Hr {
    constructor(e, t = {}) {
        let n = typeof e == `object` ? e : t;
        (!n.transports || n.transports && typeof n.transports[0] == `string`) && (n.transports = (n.transports || [`polling`, `websocket`, `webtransport`]).map(e => Fr[e]).filter(e => !!e)), super(e, n)
    }
};
Ur.protocol;

function Wr(e, t = ``, n) {
    let r = e;
    n ||= typeof location < `u` && location, e ??= n.protocol + `//` + n.host, typeof e == `string` && (e.charAt(0) === `/` && (e = e.charAt(1) === `/` ? n.protocol + e : n.host + e), /^(https?|wss?):\/\//.test(e) || (e = n === void 0 ? `https://` + e : n.protocol + `//` + e), r = Rr(e)), r.port || (/^(http|ws)$/.test(r.protocol) ? r.port = `80` : /^(http|ws)s$/.test(r.protocol) && (r.port = `443`)), r.path = r.path || `/`;
    let i = r.host.indexOf(`:`) === -1 ? r.host : `[` + r.host + `]`;
    return r.id = r.protocol + `://` + i + `:` + r.port + t, r.href = r.protocol + `://` + i + (n && n.port === r.port ? `` : `:` + r.port), r
}

var Gr = typeof ArrayBuffer == `function`,
    Kr = e => typeof ArrayBuffer.isView == `function` ? ArrayBuffer.isView(e) : e.buffer instanceof ArrayBuffer,
    qr = Object.prototype.toString,
    Jr = typeof Blob == `function` || typeof Blob < `u` && qr.call(Blob) === `[object BlobConstructor]`,
    Yr = typeof File == `function` || typeof File < `u` && qr.call(File) === `[object FileConstructor]`;

function Xr(e) {
    return Gr && (e instanceof ArrayBuffer || Kr(e)) || Jr && e instanceof Blob || Yr && e instanceof File
}

function J(e, t) {
    if (!e || typeof e != `object`) return !1;
    if (Array.isArray(e)) {
        for (let t = 0, n = e.length; t < n; t++) if (J(e[t])) return !0;
        return !1
    }
    if (Xr(e)) return !0;
    if (e.toJSON && typeof e.toJSON == `function` && arguments.length === 1) return J(e.toJSON(), !0);
    for (let t in e) if (Object.prototype.hasOwnProperty.call(e, t) && J(e[t])) return !0;
    return !1
}

function Zr(e) {
    let t = [], n = e.data, r = e;
    return r.data = Qr(n, t), r.attachments = t.length, {packet: r, buffers: t}
}

function Qr(e, t) {
    if (!e) return e;
    if (Xr(e)) {
        let n = {_placeholder: !0, num: t.length};
        return t.push(e), n
    } else if (Array.isArray(e)) {
        let n = Array(e.length);
        for (let r = 0; r < e.length; r++) n[r] = Qr(e[r], t);
        return n
    } else if (typeof e == `object` && !(e instanceof Date)) {
        let n = {};
        for (let r in e) Object.prototype.hasOwnProperty.call(e, r) && (n[r] = Qr(e[r], t));
        return n
    }
    return e
}

function $r(e, t) {
    return e.data = ei(e.data, t), delete e.attachments, e
}

function ei(e, t) {
    if (!e) return e;
    if (e && e._placeholder === !0) {
        if (typeof e.num == `number` && e.num >= 0 && e.num < t.length) return t[e.num];
        throw Error(`illegal attachments`)
    } else if (Array.isArray(e)) for (let n = 0; n < e.length; n++) e[n] = ei(e[n], t); else if (typeof e == `object`) for (let n in e) Object.prototype.hasOwnProperty.call(e, n) && (e[n] = ei(e[n], t));
    return e
}

var ti = t({Decoder: () => ii, Encoder: () => ri, PacketType: () => Y, isPacketValid: () => ui, protocol: () => 5}),
    ni = [`connect`, `connect_error`, `disconnect`, `disconnecting`, `newListener`, `removeListener`], Y;
(function (e) {
    e[e.CONNECT = 0] = `CONNECT`, e[e.DISCONNECT = 1] = `DISCONNECT`, e[e.EVENT = 2] = `EVENT`, e[e.ACK = 3] = `ACK`, e[e.CONNECT_ERROR = 4] = `CONNECT_ERROR`, e[e.BINARY_EVENT = 5] = `BINARY_EVENT`, e[e.BINARY_ACK = 6] = `BINARY_ACK`
})(Y ||= {});
var ri = class {
    constructor(e) {
        this.replacer = e
    }

    encode(e) {
        return (e.type === Y.EVENT || e.type === Y.ACK) && J(e) ? this.encodeAsBinary({
            type: e.type === Y.EVENT ? Y.BINARY_EVENT : Y.BINARY_ACK,
            nsp: e.nsp,
            data: e.data,
            id: e.id
        }) : [this.encodeAsString(e)]
    }

    encodeAsString(e) {
        let t = `` + e.type;
        return (e.type === Y.BINARY_EVENT || e.type === Y.BINARY_ACK) && (t += e.attachments + `-`), e.nsp && e.nsp !== `/` && (t += e.nsp + `,`), e.id != null && (t += e.id), e.data != null && (t += JSON.stringify(e.data, this.replacer)), t
    }

    encodeAsBinary(e) {
        let t = Zr(e), n = this.encodeAsString(t.packet), r = t.buffers;
        return r.unshift(n), r
    }
}, ii = class e extends H {
    constructor(e) {
        super(), this.opts = Object.assign({
            reviver: void 0,
            maxAttachments: 10
        }, typeof e == `function` ? {reviver: e} : e)
    }

    add(e) {
        let t;
        if (typeof e == `string`) {
            if (this.reconstructor) throw Error(`got plaintext data when reconstructing a packet`);
            t = this.decodeString(e);
            let n = t.type === Y.BINARY_EVENT;
            n || t.type === Y.BINARY_ACK ? (t.type = n ? Y.EVENT : Y.ACK, this.reconstructor = new ai(t), t.attachments === 0 && super.emitReserved(`decoded`, t)) : super.emitReserved(`decoded`, t)
        } else if (Xr(e) || e.base64) if (this.reconstructor) t = this.reconstructor.takeBinaryData(e), t && (this.reconstructor = null, super.emitReserved(`decoded`, t)); else throw Error(`got binary data when not reconstructing a packet`); else throw Error(`Unknown type: ` + e)
    }

    decodeString(t) {
        let n = 0, r = {type: Number(t.charAt(0))};
        if (Y[r.type] === void 0) throw Error(`unknown packet type ` + r.type);
        if (r.type === Y.BINARY_EVENT || r.type === Y.BINARY_ACK) {
            let e = n + 1;
            for (; t.charAt(++n) !== `-` && n != t.length;) ;
            let i = t.substring(e, n);
            if (i != Number(i) || t.charAt(n) !== `-`) throw Error(`Illegal attachments`);
            let a = Number(i);
            if (!si(a) || a < 0) throw Error(`Illegal attachments`);
            if (a > this.opts.maxAttachments) throw Error(`too many attachments`);
            r.attachments = a
        }
        if (t.charAt(n + 1) === `/`) {
            let e = n + 1;
            for (; ++n && !(t.charAt(n) === `,` || n === t.length);) ;
            r.nsp = t.substring(e, n)
        } else r.nsp = `/`;
        let i = t.charAt(n + 1);
        if (i !== `` && Number(i) == i) {
            let e = n + 1;
            for (; ++n;) {
                let e = t.charAt(n);
                if (e == null || Number(e) != e) {
                    --n;
                    break
                }
                if (n === t.length) break
            }
            r.id = Number(t.substring(e, n + 1))
        }
        if (t.charAt(++n)) {
            let i = this.tryParse(t.substr(n));
            if (e.isPayloadValid(r.type, i)) r.data = i; else throw Error(`invalid payload`)
        }
        return r
    }

    tryParse(e) {
        try {
            return JSON.parse(e, this.opts.reviver)
        } catch {
            return !1
        }
    }

    static isPayloadValid(e, t) {
        switch (e) {
            case Y.CONNECT:
                return X(t);
            case Y.DISCONNECT:
                return t === void 0;
            case Y.CONNECT_ERROR:
                return typeof t == `string` || X(t);
            case Y.EVENT:
            case Y.BINARY_EVENT:
                return Array.isArray(t) && (typeof t[0] == `number` || typeof t[0] == `string` && ni.indexOf(t[0]) === -1);
            case Y.ACK:
            case Y.BINARY_ACK:
                return Array.isArray(t)
        }
    }

    destroy() {
        this.reconstructor &&= (this.reconstructor.finishedReconstruction(), null)
    }
}, ai = class {
    constructor(e) {
        this.packet = e, this.buffers = [], this.reconPack = e
    }

    takeBinaryData(e) {
        if (this.buffers.push(e), this.buffers.length === this.reconPack.attachments) {
            let e = $r(this.reconPack, this.buffers);
            return this.finishedReconstruction(), e
        }
        return null
    }

    finishedReconstruction() {
        this.reconPack = null, this.buffers = []
    }
};

function oi(e) {
    return typeof e == `string`
}

var si = Number.isInteger || function (e) {
    return typeof e == `number` && isFinite(e) && Math.floor(e) === e
};

function ci(e) {
    return e === void 0 || si(e)
}

function X(e) {
    return Object.prototype.toString.call(e) === `[object Object]`
}

function li(e, t) {
    switch (e) {
        case Y.CONNECT:
            return t === void 0 || X(t);
        case Y.DISCONNECT:
            return t === void 0;
        case Y.EVENT:
            return Array.isArray(t) && (typeof t[0] == `number` || typeof t[0] == `string` && ni.indexOf(t[0]) === -1);
        case Y.ACK:
            return Array.isArray(t);
        case Y.CONNECT_ERROR:
            return typeof t == `string` || X(t);
        default:
            return !1
    }
}

function ui(e) {
    return oi(e.nsp) && ci(e.id) && li(e.type, e.data)
}

function Z(e, t, n) {
    return e.on(t, n), function () {
        e.off(t, n)
    }
}

var di = Object.freeze({
    connect: 1,
    connect_error: 1,
    disconnect: 1,
    disconnecting: 1,
    newListener: 1,
    removeListener: 1
}), fi = class extends H {
    constructor(e, t, n) {
        super(), this.connected = !1, this.recovered = !1, this.receiveBuffer = [], this.sendBuffer = [], this._queue = [], this._queueSeq = 0, this.ids = 0, this.acks = {}, this.flags = {}, this.io = e, this.nsp = t, n && n.auth && (this.auth = n.auth), this._opts = Object.assign({}, n), this.io._autoConnect && this.open()
    }

    get disconnected() {
        return !this.connected
    }

    subEvents() {
        if (this.subs) return;
        let e = this.io;
        this.subs = [Z(e, `open`, this.onopen.bind(this)), Z(e, `packet`, this.onpacket.bind(this)), Z(e, `error`, this.onerror.bind(this)), Z(e, `close`, this.onclose.bind(this))]
    }

    get active() {
        return !!this.subs
    }

    connect() {
        return this.connected ? this : (this.subEvents(), this.io._reconnecting || this.io.open(), this.io._readyState === `open` && this.onopen(), this)
    }

    open() {
        return this.connect()
    }

    send(...e) {
        return e.unshift(`message`), this.emit.apply(this, e), this
    }

    emit(e, ...t) {
        if (di.hasOwnProperty(e)) throw Error(`"` + e.toString() + `" is a reserved event name`);
        if (t.unshift(e), this._opts.retries && !this.flags.fromQueue && !this.flags.volatile) return this._addToQueue(t), this;
        let n = {type: Y.EVENT, data: t};
        if (n.options = {}, n.options.compress = this.flags.compress !== !1, typeof t[t.length - 1] == `function`) {
            let e = this.ids++, r = t.pop();
            this._registerAckCallback(e, r), n.id = e
        }
        let r = this.io.engine?.transport?.writable, i = this.connected && !this.io.engine?._hasPingExpired();
        return this.flags.volatile && !r || (i ? (this.notifyOutgoingListeners(n), this.packet(n)) : this.sendBuffer.push(n)), this.flags = {}, this
    }

    _registerAckCallback(e, t) {
        let n = this.flags.timeout ?? this._opts.ackTimeout;
        if (n === void 0) {
            this.acks[e] = t;
            return
        }
        let r = this.io.setTimeoutFn(() => {
            delete this.acks[e];
            for (let t = 0; t < this.sendBuffer.length; t++) this.sendBuffer[t].id === e && this.sendBuffer.splice(t, 1);
            t.call(this, Error(`operation has timed out`))
        }, n), i = (...e) => {
            this.io.clearTimeoutFn(r), t.apply(this, e)
        };
        i.withError = !0, this.acks[e] = i
    }

    emitWithAck(e, ...t) {
        return new Promise((n, r) => {
            let i = (e, t) => e ? r(e) : n(t);
            i.withError = !0, t.push(i), this.emit(e, ...t)
        })
    }

    _addToQueue(e) {
        let t;
        typeof e[e.length - 1] == `function` && (t = e.pop());
        let n = {
            id: this._queueSeq++,
            tryCount: 0,
            pending: !1,
            args: e,
            flags: Object.assign({fromQueue: !0}, this.flags)
        };
        e.push((e, ...r) => (this._queue[0], e === null ? (this._queue.shift(), t && t(null, ...r)) : n.tryCount > this._opts.retries && (this._queue.shift(), t && t(e)), n.pending = !1, this._drainQueue())), this._queue.push(n), this._drainQueue()
    }

    _drainQueue(e = !1) {
        if (!this.connected || this._queue.length === 0) return;
        let t = this._queue[0];
        t.pending && !e || (t.pending = !0, t.tryCount++, this.flags = t.flags, this.emit.apply(this, t.args))
    }

    packet(e) {
        e.nsp = this.nsp, this.io._packet(e)
    }

    onopen() {
        typeof this.auth == `function` ? this.auth(e => {
            this._sendConnectPacket(e)
        }) : this._sendConnectPacket(this.auth)
    }

    _sendConnectPacket(e) {
        this.packet({
            type: Y.CONNECT,
            data: this._pid ? Object.assign({pid: this._pid, offset: this._lastOffset}, e) : e
        })
    }

    onerror(e) {
        this.connected || this.emitReserved(`connect_error`, e)
    }

    onclose(e, t) {
        this.connected = !1, delete this.id, this.emitReserved(`disconnect`, e, t), this._clearAcks()
    }

    _clearAcks() {
        Object.keys(this.acks).forEach(e => {
            if (!this.sendBuffer.some(t => String(t.id) === e)) {
                let t = this.acks[e];
                delete this.acks[e], t.withError && t.call(this, Error(`socket has been disconnected`))
            }
        })
    }

    onpacket(e) {
        if (e.nsp === this.nsp) switch (e.type) {
            case Y.CONNECT:
                e.data && e.data.sid ? this.onconnect(e.data.sid, e.data.pid) : this.emitReserved(`connect_error`, Error(`It seems you are trying to reach a Socket.IO server in v2.x with a v3.x client, but they are not compatible (more information here: https://socket.io/docs/v3/migrating-from-2-x-to-3-0/)`));
                break;
            case Y.EVENT:
            case Y.BINARY_EVENT:
                this.onevent(e);
                break;
            case Y.ACK:
            case Y.BINARY_ACK:
                this.onack(e);
                break;
            case Y.DISCONNECT:
                this.ondisconnect();
                break;
            case Y.CONNECT_ERROR:
                this.destroy();
                let t = Error(e.data.message);
                t.data = e.data.data, this.emitReserved(`connect_error`, t);
                break
        }
    }

    onevent(e) {
        let t = e.data || [];
        e.id != null && t.push(this.ack(e.id)), this.connected ? this.emitEvent(t) : this.receiveBuffer.push(Object.freeze(t))
    }

    emitEvent(e) {
        if (this._anyListeners && this._anyListeners.length) {
            let t = this._anyListeners.slice();
            for (let n of t) n.apply(this, e)
        }
        super.emit.apply(this, e), this._pid && e.length && typeof e[e.length - 1] == `string` && (this._lastOffset = e[e.length - 1])
    }

    ack(e) {
        let t = this, n = !1;
        return function (...r) {
            n || (n = !0, t.packet({type: Y.ACK, id: e, data: r}))
        }
    }

    onack(e) {
        let t = this.acks[e.id];
        typeof t == `function` && (delete this.acks[e.id], t.withError && e.data.unshift(null), t.apply(this, e.data))
    }

    onconnect(e, t) {
        this.id = e, this.recovered = t && this._pid === t, this._pid = t, this.connected = !0, this.emitBuffered(), this._drainQueue(!0), this.emitReserved(`connect`)
    }

    emitBuffered() {
        this.receiveBuffer.forEach(e => this.emitEvent(e)), this.receiveBuffer = [], this.sendBuffer.forEach(e => {
            this.notifyOutgoingListeners(e), this.packet(e)
        }), this.sendBuffer = []
    }

    ondisconnect() {
        this.destroy(), this.onclose(`io server disconnect`)
    }

    destroy() {
        this.subs &&= (this.subs.forEach(e => e()), void 0), this.io._destroy(this)
    }

    disconnect() {
        return this.connected && this.packet({type: Y.DISCONNECT}), this.destroy(), this.connected && this.onclose(`io client disconnect`), this
    }

    close() {
        return this.disconnect()
    }

    compress(e) {
        return this.flags.compress = e, this
    }

    get volatile() {
        return this.flags.volatile = !0, this
    }

    timeout(e) {
        return this.flags.timeout = e, this
    }

    onAny(e) {
        return this._anyListeners = this._anyListeners || [], this._anyListeners.push(e), this
    }

    prependAny(e) {
        return this._anyListeners = this._anyListeners || [], this._anyListeners.unshift(e), this
    }

    offAny(e) {
        if (!this._anyListeners) return this;
        if (e) {
            let t = this._anyListeners;
            for (let n = 0; n < t.length; n++) if (e === t[n]) return t.splice(n, 1), this
        } else this._anyListeners = [];
        return this
    }

    listenersAny() {
        return this._anyListeners || []
    }

    onAnyOutgoing(e) {
        return this._anyOutgoingListeners = this._anyOutgoingListeners || [], this._anyOutgoingListeners.push(e), this
    }

    prependAnyOutgoing(e) {
        return this._anyOutgoingListeners = this._anyOutgoingListeners || [], this._anyOutgoingListeners.unshift(e), this
    }

    offAnyOutgoing(e) {
        if (!this._anyOutgoingListeners) return this;
        if (e) {
            let t = this._anyOutgoingListeners;
            for (let n = 0; n < t.length; n++) if (e === t[n]) return t.splice(n, 1), this
        } else this._anyOutgoingListeners = [];
        return this
    }

    listenersAnyOutgoing() {
        return this._anyOutgoingListeners || []
    }

    notifyOutgoingListeners(e) {
        if (this._anyOutgoingListeners && this._anyOutgoingListeners.length) {
            let t = this._anyOutgoingListeners.slice();
            for (let n of t) n.apply(this, e.data)
        }
    }
};

function Q(e) {
    e ||= {}, this.ms = e.min || 100, this.max = e.max || 1e4, this.factor = e.factor || 2, this.jitter = e.jitter > 0 && e.jitter <= 1 ? e.jitter : 0, this.attempts = 0
}

Q.prototype.duration = function () {
    var e = this.ms * this.factor ** +this.attempts++;
    if (this.jitter) {
        var t = Math.random(), n = Math.floor(t * this.jitter * e);
        e = Math.floor(t * 10) & 1 ? e + n : e - n
    }
    return Math.min(e, this.max) | 0
}, Q.prototype.reset = function () {
    this.attempts = 0
}, Q.prototype.setMin = function (e) {
    this.ms = e
}, Q.prototype.setMax = function (e) {
    this.max = e
}, Q.prototype.setJitter = function (e) {
    this.jitter = e
};
var pi = class extends H {
    constructor(e, t) {
        super(), this.nsps = {}, this.subs = [], e && typeof e == `object` && (t = e, e = void 0), t ||= {}, t.path = t.path || `/socket.io`, this.opts = t, W(this, t), this.reconnection(t.reconnection !== !1), this.reconnectionAttempts(t.reconnectionAttempts || 1 / 0), this.reconnectionDelay(t.reconnectionDelay || 1e3), this.reconnectionDelayMax(t.reconnectionDelayMax || 5e3), this.randomizationFactor(t.randomizationFactor ?? .5), this.backoff = new Q({
            min: this.reconnectionDelay(),
            max: this.reconnectionDelayMax(),
            jitter: this.randomizationFactor()
        }), this.timeout(t.timeout == null ? 2e4 : t.timeout), this._readyState = `closed`, this.uri = e;
        let n = t.parser || ti;
        this.encoder = new n.Encoder, this.decoder = new n.Decoder, this._autoConnect = t.autoConnect !== !1, this._autoConnect && this.open()
    }

    reconnection(e) {
        return arguments.length ? (this._reconnection = !!e, e || (this.skipReconnect = !0), this) : this._reconnection
    }

    reconnectionAttempts(e) {
        return e === void 0 ? this._reconnectionAttempts : (this._reconnectionAttempts = e, this)
    }

    reconnectionDelay(e) {
        var t;
        return e === void 0 ? this._reconnectionDelay : (this._reconnectionDelay = e, (t = this.backoff) == null || t.setMin(e), this)
    }

    randomizationFactor(e) {
        var t;
        return e === void 0 ? this._randomizationFactor : (this._randomizationFactor = e, (t = this.backoff) == null || t.setJitter(e), this)
    }

    reconnectionDelayMax(e) {
        var t;
        return e === void 0 ? this._reconnectionDelayMax : (this._reconnectionDelayMax = e, (t = this.backoff) == null || t.setMax(e), this)
    }

    timeout(e) {
        return arguments.length ? (this._timeout = e, this) : this._timeout
    }

    maybeReconnectOnOpen() {
        !this._reconnecting && this._reconnection && this.backoff.attempts === 0 && this.reconnect()
    }

    open(e) {
        if (~this._readyState.indexOf(`open`)) return this;
        this.engine = new Ur(this.uri, this.opts);
        let t = this.engine, n = this;
        this._readyState = `opening`, this.skipReconnect = !1;
        let r = Z(t, `open`, function () {
            n.onopen(), e && e()
        }), i = t => {
            this.cleanup(), this._readyState = `closed`, this.emitReserved(`error`, t), e ? e(t) : this.maybeReconnectOnOpen()
        }, a = Z(t, `error`, i);
        if (!1 !== this._timeout) {
            let e = this._timeout, n = this.setTimeoutFn(() => {
                r(), i(Error(`timeout`)), t.close()
            }, e);
            this.opts.autoUnref && n.unref(), this.subs.push(() => {
                this.clearTimeoutFn(n)
            })
        }
        return this.subs.push(r), this.subs.push(a), this
    }

    connect(e) {
        return this.open(e)
    }

    onopen() {
        this.cleanup(), this._readyState = `open`, this.emitReserved(`open`);
        let e = this.engine;
        this.subs.push(Z(e, `ping`, this.onping.bind(this)), Z(e, `data`, this.ondata.bind(this)), Z(e, `error`, this.onerror.bind(this)), Z(e, `close`, this.onclose.bind(this)), Z(this.decoder, `decoded`, this.ondecoded.bind(this)))
    }

    onping() {
        this.emitReserved(`ping`)
    }

    ondata(e) {
        try {
            this.decoder.add(e)
        } catch (e) {
            this.onclose(`parse error`, e)
        }
    }

    ondecoded(e) {
        ur(() => {
            this.emitReserved(`packet`, e)
        }, this.setTimeoutFn)
    }

    onerror(e) {
        this.emitReserved(`error`, e)
    }

    socket(e, t) {
        let n = this.nsps[e];
        return n ? this._autoConnect && !n.active && n.connect() : (n = new fi(this, e, t), this.nsps[e] = n), n
    }

    _destroy(e) {
        let t = Object.keys(this.nsps);
        for (let e of t) if (this.nsps[e].active) return;
        this._close()
    }

    _packet(e) {
        let t = this.encoder.encode(e);
        for (let n = 0; n < t.length; n++) this.engine.write(t[n], e.options)
    }

    cleanup() {
        this.subs.forEach(e => e()), this.subs.length = 0, this.decoder.destroy()
    }

    _close() {
        this.skipReconnect = !0, this._reconnecting = !1, this.onclose(`forced close`)
    }

    disconnect() {
        return this._close()
    }

    onclose(e, t) {
        var n;
        this.cleanup(), (n = this.engine) == null || n.close(), this.backoff.reset(), this._readyState = `closed`, this.emitReserved(`close`, e, t), this._reconnection && !this.skipReconnect && this.reconnect()
    }

    reconnect() {
        if (this._reconnecting || this.skipReconnect) return this;
        let e = this;
        if (this.backoff.attempts >= this._reconnectionAttempts) this.backoff.reset(), this.emitReserved(`reconnect_failed`), this._reconnecting = !1; else {
            let t = this.backoff.duration();
            this._reconnecting = !0;
            let n = this.setTimeoutFn(() => {
                e.skipReconnect || (this.emitReserved(`reconnect_attempt`, e.backoff.attempts), !e.skipReconnect && e.open(t => {
                    t ? (e._reconnecting = !1, e.reconnect(), this.emitReserved(`reconnect_error`, t)) : e.onreconnect()
                }))
            }, t);
            this.opts.autoUnref && n.unref(), this.subs.push(() => {
                this.clearTimeoutFn(n)
            })
        }
    }

    onreconnect() {
        let e = this.backoff.attempts;
        this._reconnecting = !1, this.backoff.reset(), this.emitReserved(`reconnect`, e)
    }
}, $ = {};

function mi(e, t) {
    typeof e == `object` && (t = e, e = void 0), t ||= {};
    let n = Wr(e, t.path || `/socket.io`), r = n.source, i = n.id, a = n.path, o = $[i] && a in $[i].nsps,
        s = t.forceNew || t[`force new connection`] || !1 === t.multiplex || o, c;
    return s ? c = new pi(r, t) : ($[i] || ($[i] = new pi(r, t)), c = $[i]), n.query && !t.query && (t.query = n.queryKey), c.socket(n.path, t)
}

Object.assign(mi, {Manager: pi, Socket: fi, io: mi, connect: mi});
var hi = `https://api-game.nassal.pro`, gi = new class {
    socket = null;
    isConnecting = !1;
    onPongCallbacks = [];
    onVersionUpdatedCallbacks = [];
    onPlayerUpdatedCallbacks = [];
    onAchievementReceivedCallbacks = [];
    onDataRefreshCallbacks = [];

    connect() {
        if (!this.socket?.connected && !this.isConnecting) {
            this.socket &&= (this.socket.connected && this.socket.disconnect(), this.socket.removeAllListeners(), null), this.isConnecting = !0;
            try {
                let e = localStorage.getItem(w);
                this.socket = mi(hi, {
                    transports: [`websocket`, `polling`],
                    withCredentials: !0,
                    auth: e ? {token: e} : void 0,
                    reconnection: !0,
                    reconnectionAttempts: 5,
                    reconnectionDelay: 1e3,
                    reconnectionDelayMax: 5e3
                }), this.socket.on(`connect`, () => {
                    this.isConnecting = !1, this.ping()
                }), this.socket.on(`connect_error`, e => {
                    console.error(`Socket.IO connection error:`, e), this.isConnecting = !1
                }), this.socket.on(`disconnect`, e => {
                }), this.setupEventListeners()
            } catch (e) {
                console.error(`Failed to create Socket.IO connection:`, e), this.isConnecting = !1
            }
        }
    }

    setupEventListeners() {
        this.socket && (this.socket.removeAllListeners(`pong`), this.socket.removeAllListeners(`player-updated`), this.socket.removeAllListeners(`version-updated`), this.socket.removeAllListeners(`achievement-received`), this.socket.removeAllListeners(`message`), this.socket.on(`pong`, e => {
            this.onPongCallbacks.forEach(t => {
                try {
                    t(e)
                } catch (e) {
                    console.error(`Error in pong callback:`, e)
                }
            })
        }), this.socket.on(`player-updated`, e => {
            let t = typeof e == `object` && e && `data` in e && e.data ? e.data : e;
            this.onPlayerUpdatedCallbacks.forEach(e => {
                try {
                    e(t)
                } catch (e) {
                    console.error(`Error in player-updated callback:`, e)
                }
            })
        }), this.socket.on(`version-updated`, e => {
            this.onVersionUpdatedCallbacks.forEach(t => {
                try {
                    t(e)
                } catch (e) {
                    console.error(`Error in version-updated callback:`, e)
                }
            }), this.onDataRefreshCallbacks.forEach(e => {
                try {
                    e()
                } catch (e) {
                    console.error(`Error in data_refresh callback:`, e)
                }
            })
        }), this.socket.on(`achievement-received`, e => {
            let t = typeof e == `object` && e && `data` in e && e.data ? e.data : e;
            this.onAchievementReceivedCallbacks.forEach(e => {
                try {
                    e(t)
                } catch (e) {
                    console.error(`Error in achievement-received callback:`, e)
                }
            })
        }), this.socket.on(`message`, e => {
            let t = e;
            (t?.type === `pong` || t?.event === `pong`) && this.onPongCallbacks.forEach(e => {
                try {
                    e(t)
                } catch (e) {
                    console.error(`Error in pong (message) callback:`, e)
                }
            })
        }))
    }

    ping() {
        this.socket?.connected ? this.socket.emit(`ping`, {
            type: `ping`,
            event: `ping`
        }) : console.warn(`Socket.IO is not connected. Cannot send ping.`)
    }

    onPong(e) {
        return this.onPongCallbacks.push(e), () => {
            let t = this.onPongCallbacks.indexOf(e);
            t > -1 && this.onPongCallbacks.splice(t, 1)
        }
    }

    onVersionUpdated(e) {
        return this.onVersionUpdatedCallbacks.push(e), () => {
            let t = this.onVersionUpdatedCallbacks.indexOf(e);
            t > -1 && this.onVersionUpdatedCallbacks.splice(t, 1)
        }
    }

    onPlayerUpdated(e) {
        return this.onPlayerUpdatedCallbacks.push(e), () => {
            let t = this.onPlayerUpdatedCallbacks.indexOf(e);
            t > -1 && this.onPlayerUpdatedCallbacks.splice(t, 1)
        }
    }

    onAchievementReceived(e) {
        return this.onAchievementReceivedCallbacks.push(e), () => {
            let t = this.onAchievementReceivedCallbacks.indexOf(e);
            t > -1 && this.onAchievementReceivedCallbacks.splice(t, 1)
        }
    }

    onDataRefresh(e) {
        return this.onDataRefreshCallbacks.push(e), () => {
            let t = this.onDataRefreshCallbacks.indexOf(e);
            t > -1 && this.onDataRefreshCallbacks.splice(t, 1)
        }
    }

    disconnect() {
        this.isConnecting = !1, this.socket &&= (this.socket.removeAllListeners(), this.socket.connected && this.socket.disconnect(), null)
    }

    isConnected() {
        return this.socket?.connected ?? !1
    }

    getSocket() {
        return this.socket
    }
}, _i = (0, S.createContext)(void 0);

function vi() {
    let e = (0, S.useContext)(_i);
    if (e === void 0) throw Error(`useUser must be used within a UserProvider`);
    return e
}

var yi = {
    A: {label: `A`, shape: `circle`, color: `#1e8c3b`},
    B: {label: `B`, shape: `circle`, color: `#c8312b`},
    X: {label: `X`, shape: `circle`, color: `#2273c4`},
    Y: {label: `Y`, shape: `circle`, color: `#d4a017`}
}, bi = {
    A: {label: `✕`, shape: `cross`, color: `#3ea1d6`},
    B: {label: `◯`, shape: `circle`, color: `#d6403e`},
    X: {label: `□`, shape: `square`, color: `#e183b8`},
    Y: {label: `△`, shape: `triangle`, color: `#3ec07a`}
};

function xi(e, t) {
    let n = t === `playstation` ? bi[e] : yi[e];
    return (0, C.jsx)(`span`, {
        className: `gamepad-hint__face`,
        style: {backgroundColor: n.color},
        "aria-label": `Gamepad ${e}`,
        children: (0, C.jsx)(`span`, {className: `gamepad-hint__face-label`, children: n.label})
    })
}

function Si(e) {
    let t = e === `Up` ? `▲` : e === `Down` ? `▼` : e === `Left` ? `◀` : `▶`;
    return (0, C.jsx)(`span`, {className: `gamepad-hint__dpad`, "aria-label": `D-Pad ${e}`, children: t})
}

function Ci(e) {
    return (0, C.jsx)(`span`, {className: `gamepad-hint__shoulder`, "aria-label": `Gamepad ${e}`, children: e})
}

function wi(e) {
    return (0, C.jsx)(`span`, {className: `gamepad-hint__menu`, "aria-label": `Gamepad ${e}`, children: e})
}

function Ti(e, t) {
    switch (e) {
        case`A`:
        case`B`:
        case`X`:
        case`Y`:
            return xi(e, t);
        case`DPadUp`:
            return Si(`Up`);
        case`DPadDown`:
            return Si(`Down`);
        case`DPadLeft`:
            return Si(`Left`);
        case`DPadRight`:
            return Si(`Right`);
        case`LB`:
            return Ci(t === `playstation` ? `L1` : `LB`);
        case`RB`:
            return Ci(t === `playstation` ? `R1` : `RB`);
        case`LT`:
            return Ci(t === `playstation` ? `L2` : `LT`);
        case`RT`:
            return Ci(t === `playstation` ? `R2` : `RT`);
        case`Start`:
            return wi(t === `playstation` ? `Options` : `Start`);
        case`Back`:
            return wi(t === `playstation` ? `Share` : `Back`);
        case`LStick`:
            return wi(`L3`);
        case`RStick`:
            return wi(`R3`);
        default:
            return null
    }
}

function Ei({button: e, className: t, style: n, alwaysShow: i = !1}) {
    let {connected: a, vendor: o} = r();
    return !i && !a ? null : (0, C.jsx)(`span`, {
        className: `gamepad-hint ${t ?? ``}`.trim(),
        style: n,
        "data-vendor": o,
        "data-button": e,
        children: Ti(e, o)
    })
}

var Di = `darktheme`;

function Oi(e) {
    return e === `lighttheme` ? `lighttheme` : e === `darktheme` ? `darktheme` : Di
}

var ki = `(max-width: 768px)`, Ai = `(pointer: coarse)`;

function ji() {
    return typeof window > `u` ? !1 : window.matchMedia(ki).matches
}

function Mi() {
    return typeof window > `u` ? !1 : Math.min(window.screen.width, window.screen.height) <= 600 && window.matchMedia(`(pointer: coarse)`).matches
}

function Ni() {
    return !(typeof window > `u` || ji() || Mi())
}

function Pi(e) {
    if (typeof window > `u`) return () => {
    };
    let t = window.matchMedia(ki), n = () => e();
    return t.addEventListener(`change`, n), () => t.removeEventListener(`change`, n)
}

function Fi(e) {
    if (typeof window > `u`) return () => {
    };
    let t = () => e(), n = window.matchMedia(Ai);
    n.addEventListener(`change`, t), window.addEventListener(`orientationchange`, t);
    let r = Pi(e);
    return () => {
        n.removeEventListener(`change`, t), window.removeEventListener(`orientationchange`, t), r()
    }
}

function Ii() {
    return (0, S.useSyncExternalStore)(Pi, ji, () => !1)
}

export {
    ft as $,
    _n as A,
    d as At,
    On as B,
    Mn as C,
    m as Ct,
    pn as D,
    ce as Dt,
    hn as E,
    g as Et,
    Ln as F,
    mn as G,
    qt as H,
    In as I,
    An as J,
    yn as K,
    xn as L,
    rn as M,
    c as Mt,
    Pn as N,
    s as Nt,
    gn as O,
    ve as Ot,
    nn as P,
    St as Q,
    Rn as R,
    cn as S,
    ee as St,
    dn as T,
    ne as Tt,
    Yt as U,
    Nn as V,
    Sn as W,
    Tt as X,
    wt as Y,
    A as Z,
    kn as _,
    He as _t,
    Oi as a,
    nt as at,
    Bn as b,
    xe as bt,
    _i as c,
    Qe as ct,
    Dn as d,
    Ze as dt,
    dt as et,
    jn as f,
    qe as ft,
    bn as g,
    Ue as gt,
    an as h,
    Ke as ht,
    Di as i,
    tt as it,
    ln as j,
    l as jt,
    fn as k,
    te as kt,
    gi as l,
    Ye as lt,
    Fn as m,
    Ge as mt,
    Ni as n,
    st as nt,
    Ei as o,
    et as ot,
    sn as p,
    We as pt,
    un as q,
    Fi as r,
    at as rt,
    vi as s,
    E as st,
    Ii as t,
    lt as tt,
    N as u,
    Xe as ut,
    Vn as v,
    Ne as vt,
    vn as w,
    pe as wt,
    tn as x,
    be as xt,
    F as y,
    y as yt,
    zn as z
};