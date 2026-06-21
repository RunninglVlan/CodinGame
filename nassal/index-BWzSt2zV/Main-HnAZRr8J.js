import {
    A as e,
    B as t,
    C as n,
    D as r,
    E as i,
    F as a,
    J as o,
    L as s,
    M as c,
    N as l,
    O as u,
    P as d,
    Q as f,
    R as p,
    S as m,
    T as h,
    U as g,
    W as _,
    X as v,
    Y as y,
    Z as b,
    _ as x,
    a as S,
    c as C,
    ct as w,
    d as T,
    i as E,
    j as D,
    k as O,
    l as k,
    m as A,
    n as j,
    ot as M,
    p as N,
    q as ee,
    r as te,
    s as ne,
    st as P,
    t as re,
    u as F,
    ut as ie,
    w as I,
    x as ae,
    y as L,
    z as oe
} from "./gamepadScope-Bi-UfPAi.js";
import {
    $ as se,
    A as R,
    At as z,
    B as ce,
    C as le,
    Ct as ue,
    D as de,
    Dt as fe,
    E as pe,
    Et as B,
    F as V,
    G as me,
    H as he,
    I as ge,
    J as _e,
    K as ve,
    L as ye,
    Mt as be,
    N as xe,
    Nt as Se,
    O as Ce,
    Ot as we,
    P as Te,
    Q as Ee,
    R as De,
    S as Oe,
    St as ke,
    T as Ae,
    Tt as je,
    U as H,
    V as Me,
    W as Ne,
    X as U,
    Y as W,
    Z as Pe,
    _ as G,
    _t as Fe,
    a as Ie,
    at as Le,
    b as Re,
    bt as ze,
    ct as Be,
    d as Ve,
    dt as He,
    et as Ue,
    f as We,
    ft as Ge,
    g as Ke,
    i as qe,
    it as Je,
    j as Ye,
    jt as Xe,
    k as Ze,
    kt as Qe,
    l as $e,
    lt as et,
    m as tt,
    mt as K,
    n as nt,
    nt as rt,
    o as q,
    ot as it,
    p as at,
    pt as ot,
    q as st,
    r as ct,
    rt as lt,
    s as ut,
    st as dt,
    t as ft,
    tt as pt,
    u as mt,
    ut as ht,
    v as gt,
    vt as _t,
    w as vt,
    wt as yt,
    x as bt,
    xt,
    y as St,
    yt as Ct,
    z as wt
} from "./useIsMobileLayout-B9ceqi_n.js";
import {
    $ as Tt,
    A as Et,
    B as Dt,
    D as J,
    F as Ot,
    G as kt,
    H as At,
    I as jt,
    J as Mt,
    K as Nt,
    L as Pt,
    M as Ft,
    N as It,
    O as Lt,
    P as Rt,
    Q as zt,
    R as Bt,
    U as Vt,
    V as Ht,
    W as Ut,
    X as Wt,
    Y as Gt,
    Z as Kt,
    _ as qt,
    at as Jt,
    c as Yt,
    ct as Xt,
    d as Zt,
    dt as Qt,
    et as $t,
    f as en,
    ft as tn,
    g as nn,
    h as rn,
    it as an,
    j as on,
    k as sn,
    l as cn,
    lt as ln,
    m as un,
    n as dn,
    nt as fn,
    o as pn,
    ot as mn,
    q as hn,
    rt as gn,
    s as _n,
    st as vn,
    tt as yn,
    u as bn,
    ut as xn,
    x as Sn,
    y as Cn,
    z as wn
} from "./rhythmUploadBridge-ff3UCNfN.js";

var Tn = class extends be {
    constructor(e, t) {
        super(), this.options = t, this.#e = e, this.#s = null, this.#o = ze(), this.bindMethods(), this.setOptions(t)
    }

    #e;
    #t = void 0;
    #n = void 0;
    #r = void 0;
    #i;
    #a;
    #o;
    #s;
    #c;
    #l;
    #u;
    #d;
    #f;
    #p;
    #m = new Set;

    bindMethods() {
        this.refetch = this.refetch.bind(this)
    }

    onSubscribe() {
        this.listeners.size === 1 && (this.#t.addObserver(this), Dn(this.#t, this.options) ? this.#h() : this.updateResult(), this.#y())
    }

    onUnsubscribe() {
        this.hasListeners() || this.destroy()
    }

    shouldFetchOnReconnect() {
        return On(this.#t, this.options, this.options.refetchOnReconnect)
    }

    shouldFetchOnWindowFocus() {
        return On(this.#t, this.options, this.options.refetchOnWindowFocus)
    }

    destroy() {
        this.listeners = new Set, this.#b(), this.#x(), this.#t.removeObserver(this)
    }

    setOptions(e) {
        let t = this.options, n = this.#t;
        if (this.options = this.#e.defaultQueryOptions(e), this.options.enabled !== void 0 && typeof this.options.enabled != `boolean` && typeof this.options.enabled != `function` && typeof je(this.options.enabled, this.#t) != `boolean`) throw Error(`Expected enabled to be a boolean or a callback that returns a boolean`);
        this.#S(), this.#t.setOptions(this.options), t._defaulted && !fe(this.options, t) && this.#e.getQueryCache().notify({
            type: `observerOptionsUpdated`,
            query: this.#t,
            observer: this
        });
        let r = this.hasListeners();
        r && kn(this.#t, n, this.options, t) && this.#h(), this.updateResult(), r && (this.#t !== n || je(this.options.enabled, this.#t) !== je(t.enabled, this.#t) || B(this.options.staleTime, this.#t) !== B(t.staleTime, this.#t)) && this.#g();
        let i = this.#_();
        r && (this.#t !== n || je(this.options.enabled, this.#t) !== je(t.enabled, this.#t) || i !== this.#p) && this.#v(i)
    }

    getOptimisticResult(e) {
        let t = this.#e.getQueryCache().build(this.#e, e), n = this.createResult(t, e);
        return jn(this, n) && (this.#r = n, this.#a = this.options, this.#i = this.#t.state), n
    }

    getCurrentResult() {
        return this.#r
    }

    trackResult(e, t) {
        return new Proxy(e, {get: (e, n) => (this.trackProp(n), t?.(n), n === `promise` && (this.trackProp(`data`), !this.options.experimental_prefetchInRender && this.#o.status === `pending` && this.#o.reject(Error(`experimental_prefetchInRender feature flag is not enabled`))), Reflect.get(e, n))})
    }

    trackProp(e) {
        this.#m.add(e)
    }

    getCurrentQuery() {
        return this.#t
    }

    refetch({...e} = {}) {
        return this.fetch({...e})
    }

    fetchOptimistic(e) {
        let t = this.#e.defaultQueryOptions(e), n = this.#e.getQueryCache().build(this.#e, t);
        return n.fetch().then(() => this.createResult(n, t))
    }

    fetch(e) {
        return this.#h({...e, cancelRefetch: e.cancelRefetch ?? !0}).then(() => (this.updateResult(), this.#r))
    }

    #h(e) {
        this.#S();
        let t = this.#t.fetch(this.options, e);
        return e?.throwOnError || (t = t.catch(ue)), t
    }

    #g() {
        this.#b();
        let e = B(this.options.staleTime, this.#t);
        if (xt.isServer() || this.#r.isStale || !ke(e)) return;
        let t = Qe(this.#r.dataUpdatedAt, e) + 1;
        this.#d = z.setTimeout(() => {
            this.#r.isStale || this.updateResult()
        }, t)
    }

    #_() {
        return (typeof this.options.refetchInterval == `function` ? this.options.refetchInterval(this.#t) : this.options.refetchInterval) ?? !1
    }

    #v(e) {
        this.#x(), this.#p = e, !(xt.isServer() || je(this.options.enabled, this.#t) === !1 || !ke(this.#p) || this.#p === 0) && (this.#f = z.setInterval(() => {
            (this.options.refetchIntervalInBackground || Xe.isFocused()) && this.#h()
        }, this.#p))
    }

    #y() {
        this.#g(), this.#v(this.#_())
    }

    #b() {
        this.#d !== void 0 && (z.clearTimeout(this.#d), this.#d = void 0)
    }

    #x() {
        this.#f !== void 0 && (z.clearInterval(this.#f), this.#f = void 0)
    }

    createResult(e, t) {
        let n = this.#t, r = this.options, i = this.#r, a = this.#i, o = this.#a,
            s = e === n ? this.#n : e.state, {state: c} = e, l = {...c}, u = !1, d;
        if (t._optimisticResults) {
            let i = this.hasListeners(), a = !i && Dn(e, t), o = i && kn(e, n, t, r);
            (a || o) && (l = {...l, ..._t(c.data, e.options)}), t._optimisticResults === `isRestoring` && (l.fetchStatus = `idle`)
        }
        let {error: f, errorUpdatedAt: p, status: m} = l;
        d = l.data;
        let h = !1;
        if (t.placeholderData !== void 0 && d === void 0 && m === `pending`) {
            let e;
            i?.isPlaceholderData && t.placeholderData === o?.placeholderData ? (e = i.data, h = !0) : e = typeof t.placeholderData == `function` ? t.placeholderData(this.#u?.state.data, this.#u) : t.placeholderData, e !== void 0 && (m = `success`, d = yt(i?.data, e, t), u = !0)
        }
        if (t.select && d !== void 0 && !h) if (i && d === a?.data && t.select === this.#c) d = this.#l; else try {
            this.#c = t.select, d = t.select(d), d = yt(i?.data, d, t), this.#l = d, this.#s = null
        } catch (e) {
            this.#s = e
        }
        this.#s && (f = this.#s, d = this.#l, p = Date.now(), m = `error`);
        let g = l.fetchStatus === `fetching`, _ = m === `pending`, v = m === `error`, y = _ && g, b = d !== void 0,
            x = {
                status: m,
                fetchStatus: l.fetchStatus,
                isPending: _,
                isSuccess: m === `success`,
                isError: v,
                isInitialLoading: y,
                isLoading: y,
                data: d,
                dataUpdatedAt: l.dataUpdatedAt,
                error: f,
                errorUpdatedAt: p,
                failureCount: l.fetchFailureCount,
                failureReason: l.fetchFailureReason,
                errorUpdateCount: l.errorUpdateCount,
                isFetched: e.isFetched(),
                isFetchedAfterMount: l.dataUpdateCount > s.dataUpdateCount || l.errorUpdateCount > s.errorUpdateCount,
                isFetching: g,
                isRefetching: g && !_,
                isLoadingError: v && !b,
                isPaused: l.fetchStatus === `paused`,
                isPlaceholderData: u,
                isRefetchError: v && b,
                isStale: An(e, t),
                refetch: this.refetch,
                promise: this.#o,
                isEnabled: je(t.enabled, e) !== !1
            };
        if (this.options.experimental_prefetchInRender) {
            let t = x.data !== void 0, r = x.status === `error` && !t, i = e => {
                r ? e.reject(x.error) : t && e.resolve(x.data)
            }, a = () => {
                i(this.#o = x.promise = ze())
            }, o = this.#o;
            switch (o.status) {
                case`pending`:
                    e.queryHash === n.queryHash && i(o);
                    break;
                case`fulfilled`:
                    (r || x.data !== o.value) && a();
                    break;
                case`rejected`:
                    (!r || x.error !== o.reason) && a();
                    break
            }
        }
        return x
    }

    updateResult() {
        let e = this.#r, t = this.createResult(this.#t, this.options);
        this.#i = this.#t.state, this.#a = this.options, this.#i.data !== void 0 && (this.#u = this.#t), !fe(t, e) && (this.#r = t, this.#C({
            listeners: (() => {
                if (!e) return !0;
                let {notifyOnChangeProps: t} = this.options, n = typeof t == `function` ? t() : t;
                if (n === `all` || !n && !this.#m.size) return !0;
                let r = new Set(n ?? this.#m);
                return this.options.throwOnError && r.add(`error`), Object.keys(this.#r).some(t => {
                    let n = t;
                    return this.#r[n] !== e[n] && r.has(n)
                })
            })()
        }))
    }

    #S() {
        let e = this.#e.getQueryCache().build(this.#e, this.options);
        if (e === this.#t) return;
        let t = this.#t;
        this.#t = e, this.#n = e.state, this.hasListeners() && (t?.removeObserver(this), e.addObserver(this))
    }

    onQueryUpdate() {
        this.updateResult(), this.hasListeners() && this.#y()
    }

    #C(e) {
        Ct.batch(() => {
            e.listeners && this.listeners.forEach(e => {
                e(this.#r)
            }), this.#e.getQueryCache().notify({query: this.#t, type: `observerResultsUpdated`})
        })
    }
};

function En(e, t) {
    return je(t.enabled, e) !== !1 && e.state.data === void 0 && !(e.state.status === `error` && je(t.retryOnMount, e) === !1)
}

function Dn(e, t) {
    return En(e, t) || e.state.data !== void 0 && On(e, t, t.refetchOnMount)
}

function On(e, t, n) {
    if (je(t.enabled, e) !== !1 && B(t.staleTime, e) !== `static`) {
        let r = typeof n == `function` ? n(e) : n;
        return r === `always` || r !== !1 && An(e, t)
    }
    return !1
}

function kn(e, t, n, r) {
    return (e !== t || je(r.enabled, e) === !1) && (!n.suspense || e.state.status !== `error`) && An(e, n)
}

function An(e, t) {
    return je(t.enabled, e) !== !1 && e.isStaleByTime(B(t.staleTime, e))
}

function jn(e, t) {
    return !fe(e.getCurrentResult(), t)
}

var Y = ie(P(), 1), Mn = Y.createContext(!1), Nn = () => Y.useContext(Mn);
Mn.Provider;
var X = M();

function Pn() {
    let e = !1;
    return {
        clearReset: () => {
            e = !1
        }, reset: () => {
            e = !0
        }, isReset: () => e
    }
}

var Fn = Y.createContext(Pn()), In = () => Y.useContext(Fn), Ln = (e, t, n) => {
        let r = n?.state.error && typeof e.throwOnError == `function` ? we(e.throwOnError, [n.state.error, n]) : e.throwOnError;
        (e.suspense || e.experimental_prefetchInRender || r) && (t.isReset() || (e.retryOnMount = !1))
    }, Rn = e => {
        Y.useEffect(() => {
            e.clearReset()
        }, [e])
    }, zn = ({
                 result: e,
                 errorResetBoundary: t,
                 throwOnError: n,
                 query: r,
                 suspense: i
             }) => e.isError && !t.isReset() && !e.isFetching && r && (i && e.data === void 0 || we(n, [e.error, r])),
    Bn = e => {
        if (e.suspense) {
            let t = 1e3, n = e => e === `static` ? e : Math.max(e ?? t, t), r = e.staleTime;
            e.staleTime = typeof r == `function` ? (...e) => n(r(...e)) : n(r), typeof e.gcTime == `number` && (e.gcTime = Math.max(e.gcTime, t))
        }
    }, Vn = (e, t) => e.isLoading && e.isFetching && !t, Hn = (e, t) => e?.suspense && t.isPending,
    Un = (e, t, n) => t.fetchOptimistic(e).catch(() => {
        n.clearReset()
    });

function Wn(e, t, n) {
    let r = Nn(), i = In(), a = Fe(n), o = a.defaultQueryOptions(e);
    a.getDefaultOptions().queries?._experimental_beforeQuery?.(o);
    let s = a.getQueryCache().get(o.queryHash), c = e.subscribed !== !1;
    o._optimisticResults = r ? `isRestoring` : c ? `optimistic` : void 0, Bn(o), Ln(o, i, s), Rn(i);
    let l = !a.getQueryCache().get(o.queryHash), [u] = Y.useState(() => new t(a, o)), d = u.getOptimisticResult(o),
        f = !r && c;
    if (Y.useSyncExternalStore(Y.useCallback(e => {
        let t = f ? u.subscribe(Ct.batchCalls(e)) : ue;
        return u.updateResult(), t
    }, [u, f]), () => u.getCurrentResult(), () => u.getCurrentResult()), Y.useEffect(() => {
        u.setOptions(o)
    }, [o, u]), Hn(o, d)) throw Un(o, u, i);
    if (zn({
        result: d,
        errorResetBoundary: i,
        throwOnError: o.throwOnError,
        query: s,
        suspense: o.suspense
    })) throw d.error;
    return a.getDefaultOptions().queries?._experimental_afterQuery?.(o, d), o.experimental_prefetchInRender && !xt.isServer() && Vn(d, r) && (l ? Un(o, u, i) : s?.promise)?.catch(ue).finally(() => {
        u.updateResult()
    }), o.notifyOnChangeProps ? d : u.trackResult(d)
}

function Gn(e, t) {
    return Wn(e, Tn, t)
}

var Kn = {static: `effects/static/error.webp`, animated: `effects/animated/error.webp`};

function qn(e, t) {
    return `effects/${t}/${e}.webp`
}

function Jn(e, t = `static`) {
    let n = qn(e, t);
    return J(n) ? n : Kn[t]
}

function Yn(e, t = `static`) {
    return J(Jn(e, t))
}

var Xn = ie(Se(), 1);

function Zn(e) {
    return typeof e == `object` && !!e && e.type === `effect-confirm`
}

var Qn = /^([+-]?)(\d+)$/;

function $n(e) {
    let t = e.trim();
    return t.length === 0 ? !0 : Qn.test(t) ? !1 : !!(/\b[a-z][a-z0-9]*_[a-z0-9_]+\b/.test(t) || /^[a-z][a-z0-9]*\s+(recorded|resolved|confirmed|accepted|applied|granted|completed|attached|resolution created)$/i.test(t))
}

function er(e, t) {
    return e.find(e => e.name === t)?.displayName ?? t
}

function tr(e) {
    let t = Qn.exec(e.trim());
    if (t == null) return null;
    let n = t[1], r = Number.parseInt(t[2], 10);
    return Number.isNaN(r) || r === 0 ? null : n === `-` ? He(-r) : ht(r)
}

function nr(e) {
    return typeof e == `string` ? tr(e) ?? ($n(e) ? null : e.trim()) : null
}

function rr(e, t) {
    let n = nr(t);
    return n ? [e, n] : e
}

function ir(e) {
    return Jn(e, `animated`)
}

function ar(e) {
    return Jn(e, `static`)
}

function or(e, t, n) {
    return {message: `«${er(e, t)}» заблокировал «${er(e, n)}»`, imagePath: ir(t)}
}

function sr(e) {
    return gr(e) != null
}

function cr(e, t, n) {
    let r = _r(n.payload);
    if (r.length > 0) return lr(e, r);
    let i = gr(n.payload);
    if (i) return or(e, i.hookEffectName, i.blockedEffectName);
    let a = t?.name;
    return {
        message: rr(`Получен эффект «${t?.displayName ?? (typeof a == `string` ? er(e, a) : `эффект`)}»`, n.message),
        imagePath: ir(a ?? `unknown`)
    }
}

function lr(e, t) {
    let n = t.map(t => er(e, t)).map(e => `«${e}»`).join(`, `);
    return {message: t.length === 1 ? `Эффект ${n} удалён` : `Эффекты ${n} удалены`, imagePath: ir(t[0])}
}

function ur(e, t) {
    return {message: rr(`Эффект «${e.displayName}» активирован`, t?.message), imagePath: ar(t?.name ?? e.name)}
}

function dr(e) {
    return {message: `Эффект «${e.displayName}» разрешён`, imagePath: ar(e.name)}
}

function fr(e, t) {
    let n = t.trim();
    return e.find(e => e.displayName === n)?.name ?? null
}

function pr(e, t) {
    let n = /потерял\s+эффект\s+«([^»]+)»/i.exec(t);
    if (n) return fr(e, n[1]) ?? null;
    let r = [...t.matchAll(/«([^»]+)»/g)];
    if (r.length > 0) {
        let t = r[r.length - 1][1];
        return fr(e, t)
    }
    return null
}

function mr(e, t) {
    if (!Zn(t)) return null;
    let n = t.removedEffectName ?? t.lostEffectName;
    return typeof n == `string` && n.length > 0 ? n : typeof t.message == `string` ? pr(e, t.message) : null
}

function hr(e, t, n) {
    let r = er(e, t.effectName), i = t.pendingContext, a = Zn(i), o = a ? i.message : void 0,
        s = mr(e, t.pendingContext), c = s == null ? null : er(e, s), l = [], u = null;
    a ? (typeof o == `string` && o.trim().length > 0 && !$n(o) ? u = o.trim() : c && (u = `Потерян эффект «${c}»`), l.push(`Эффект «${r}»`), u && l.push(u)) : (n ? l.push(`${n} применил эффект «${r}»`) : l.push(`Применён эффект «${r}»`), typeof o == `string` && o.trim().length > 0 && !$n(o) ? l.push(o.trim()) : s ? l.push(`Потерян эффект «${er(e, s)}»`) : l.push(`Требуется подтверждение`));
    let d = s ?? t.effectName;
    return {
        title: n ?? `Нужно действие`,
        sourcePlayerName: n,
        isEffectConfirm: a,
        sourceEffectDisplayName: r,
        headlineMessage: u,
        lostEffectDisplayName: c,
        sourceEffectImagePath: ir(t.effectName),
        message: l.length === 1 ? l[0] : l,
        imagePath: ir(d)
    }
}

function gr(e) {
    let t = e?.hookEffectName, n = e?.blockedEffectName;
    return typeof t == `string` && t.length > 0 && typeof n == `string` && n.length > 0 ? {
        hookEffectName: t,
        blockedEffectName: n
    } : null
}

function _r(e) {
    let t = e?.removedEffectNames?.filter(e => typeof e == `string` && e.length > 0);
    if (t != null && t.length > 0) return t;
    let n = e?.removedEffectName;
    return typeof n == `string` && n.length > 0 ? [n] : []
}

function vr(e) {
    return e.casinoPhase == null || e.casinoPhase === `regular` || e.casinoPhase === `done`
}

function yr(e) {
    return (e.gnusAvailable ?? 0) > 0 && e.casinoPhase === `gnus`
}

function br(e) {
    if (e.role === `judge`) switch (e.casinoPhase) {
        case`gnus`:
            return yr(e) ? `gnus` : `judge`;
        case`platinum`:
            return `platinum`;
        case`regular`:
        case`done`:
        case null:
            return `judge`;
        default:
            return `judge`
    }
    switch (e.casinoPhase) {
        case`gnus`:
            return yr(e) ? `gnus` : null;
        case`platinum`:
            return `platinum`;
        case`regular`:
        case`done`:
        case null:
            return vr(e) ? `regular` : null;
        default:
            return null
    }
}

function xr(e) {
    let t = br(e);
    if (t != null) return t;
    if (e.role === `judge`) return `judge`;
    if (vr(e)) return `regular`;
    switch (e.casinoPhase) {
        case`platinum`:
            return `platinum`;
        case`gnus`:
            return yr(e) ? `gnus` : `regular`;
        default:
            break
    }
    return (e.platinumChips ?? 0) > 0 ? `platinum` : `regular`
}

function Sr(e, t) {
    let n = br(t);
    if (n != null && n !== e && (t.casinoPhase === `gnus` || t.casinoPhase === `platinum` || t.role === `judge` && t.casinoPhase != null && t.casinoPhase !== `regular` && t.casinoPhase !== `done`)) return !1;
    switch (e) {
        case`platinum`:
            return t.casinoPhase === `platinum` || (t.platinumChips ?? 0) > 0;
        case`gnus`:
            return yr(t);
        case`judge`:
            return t.role === `judge` && t.casinoPhase !== `gnus` && t.casinoPhase !== `platinum`;
        case`regular`:
            return vr(t);
        default:
            return !1
    }
}

function Cr(e) {
    let t = br(e);
    if (t != null && Sr(t, e)) return t;
    if (e.role === `judge`) return `judge`;
    if (vr(e)) return `regular`;
    let n = xr(e);
    return Sr(n, e) ? n : `regular`
}

function wr(e) {
    if (e == null || e.casinoPhase === `done` || e.role !== `judge` && e.status !== `casino`) return !1;
    let t = br(e);
    return t == null ? !1 : Sr(t, e)
}

function Tr(e, t) {
    let n = br(e);
    return n == null ? Sr(t, e) ? t : Cr(e) : t === n && Sr(t, e) ? t : n
}

var Er = {
    user: [`user`],
    player: [`player`, `me`],
    playerList: [`player`, `list`],
    playerById: e => [`player`, `byId`, e],
    effectList: [`effect`, `list`],
    achievements: [`achievements`],
    playerActionLog: e => [`player`, e, `actionLog`],
    playerGameHistory: e => [`player`, e, `gameHistory`],
    playerPreviousEventGames: e => [`player`, e, `previousEventGames`],
    pendingSlots: [`casino`, `pending`],
    pendingEffectResolutions: [`effectResolution`, `pending`],
    currentAuctionReward: [`auction`, `currentReward`],
    publicMiniGameLeaderboard: (e, t) => [`miniGame`, `publicLeaderboard`, e, t ?? `all`],
    publicPlayerStats: [`player`, `publicStats`]
};
Er.effectList;

function Dr(e) {
    let t = e.getQueryData([`user`]);
    if (t !== void 0) return !t || Pe(t);
    if (typeof localStorage > `u`) return !0;
    try {
        let e = localStorage.getItem(ot);
        return e ? Pe(JSON.parse(e)) : !0
    } catch {
        return !0
    }
}

function Or(e, t = Ge) {
    let n = t.getQueryData(Er.player)?.id ?? null, r = e == null || n != null && e === n;
    t.invalidateQueries({queryKey: Er.playerList}), r && !Dr(t) && (t.invalidateQueries({queryKey: Er.player}), t.invalidateQueries({queryKey: Er.pendingSlots}), t.invalidateQueries({queryKey: Er.pendingEffectResolutions}));
    let i = e ?? n;
    i && (t.invalidateQueries({queryKey: Er.playerById(i)}), t.invalidateQueries({queryKey: Er.playerActionLog(i)}), t.invalidateQueries({queryKey: Er.playerGameHistory(i)}))
}

function kr(e = Ge) {
    e.invalidateQueries({queryKey: Er.achievements})
}

var Ar = /[a-zA-Zа-яА-ЯЁё0-9]/, jr = new Set([`Shift`, `Control`, `Alt`, `Meta`, `CapsLock`]),
    Mr = new Set([`Backspace`, `Delete`, `Enter`, `Tab`, `Escape`, `ArrowUp`, `ArrowDown`, `ArrowLeft`, `ArrowRight`, `Home`, `End`, `PageUp`, `PageDown`, `Insert`, ` `]);

function Nr(e) {
    let {key: t, repeat: n} = e;
    return jr.has(t) || Mr.has(t) || t.length === 1 ? !n : !1
}

function Pr(e) {
    return Ar.test(e)
}

function Fr(e, t) {
    Nr(e) && t(Pr(e.key))
}

function Ir(e) {
    let t = e.value;
    if (typeof t != `object` || !t) return {};
    let n = {};
    for (let r of e.input) {
        let e = t[r.name];
        if (e != null) {
            if (r.type === `boolean` && typeof e == `boolean`) {
                n[r.name] = e;
                continue
            }
            if (r.type === `number`) {
                if (typeof e == `number` && !Number.isNaN(e)) n[r.name] = e; else if (typeof e == `string` && e.trim() !== ``) {
                    let t = Number(e);
                    Number.isNaN(t) || (n[r.name] = t)
                }
                continue
            }
            if (r.type === `player-ids`) {
                Array.isArray(e) && (n[r.name] = e.filter(e => typeof e == `string` && e.length > 0));
                continue
            }
            typeof e == `string` && (n[r.name] = e)
        }
    }
    return n
}

function Lr(e, t) {
    return e.input.every(e => {
        let n = t[e.name];
        if (e.type === `player-ids`) {
            if (!Array.isArray(n)) return !e.required && (e.minItems ?? 0) === 0;
            let t = e.minItems ?? +!!e.required, r = e.maxItems;
            return !(n.length < t || r != null && n.length > r)
        }
        return e.allowedValues != null && e.allowedValues.length > 0 ? n !== void 0 && !Array.isArray(n) && e.allowedValues.includes(n) : e.required ? e.type === `boolean` ? typeof n == `boolean` : e.type === `number` ? typeof n == `number` && !Number.isNaN(n) : typeof n == `string` && n.trim().length > 0 : !0
    })
}

function Rr(e) {
    if (typeof e != `object` || !e) return !1;
    let t = e.type;
    return t === `random-effect` || t === `target-random` || t === `manual-target` || t === `manual-input` || t === `confirm`
}

function zr(e) {
    return Array.isArray(e) ? e.filter(Rr) : []
}

function Br(e) {
    switch (e.type) {
        case`random-effect`:
            return e.selectedName == null;
        case`manual-target`:
            return e.candidates.length > 0 && (e.selected == null || Jr(e.selected) == null);
        case`target-random`:
            return e.candidates.length > 0;
        case`manual-input`:
            return !Lr(e, Ir(e));
        case`confirm`:
            return !e.confirmed;
        default:
            return !1
    }
}

function Vr(e) {
    return zr(e.steps).some(Br)
}

function Hr(e) {
    return e.find(e => (e.type === `target-random` || e.type === `manual-target`) && Br(e)) || e.find(e => e.type === `random-effect` && Br(e)) || e.find(e => e.type === `manual-input` && Br(e)) || e.find(e => e.type === `confirm` && Br(e)) || null
}

function Ur(e, t) {
    let n = zr(e);
    if (n.length === 0) return null;
    let r = Hr(n);
    if (r) {
        if (t) {
            let e = n.find(e => e.id === t || e.type === t);
            if (e && Br(e)) return e
        }
        return r
    }
    if (t) {
        let e = n.find(e => e.id === t);
        if (e) return e;
        let r = n.find(e => e.type === t);
        if (r) return r
    }
    return n[n.length - 1]
}

function Wr(e) {
    return e?.type === `target-random` || e?.type === `manual-target`
}

function Gr(e, t) {
    return t || (e != null && (e.type === `target-random` || e.type === `manual-target`) && e.selected != null ? Jr(e.selected) : null)
}

function Kr(e) {
    return zr(e).some(e => e.type === `target-random` || e.type === `manual-target`)
}

function qr(e, t, n) {
    let r = Gr(t, n);
    if (r) return r;
    for (let t of zr(e)) if (t.type === `target-random` || t.type === `manual-target`) {
        let e = Gr(t, null);
        if (e) return e
    }
    return null
}

function Jr(e) {
    let t = e.playerId ?? e.id;
    return typeof t == `string` && t.length > 0 ? t : null
}

var Yr = `body_armor`, Xr = `small_theft_effect`;

function Zr(e) {
    return typeof e == `object` && !!e
}

function Qr(e) {
    return e == null ? `` : Array.isArray(e) ? e.map(e => e.trim()).filter(Boolean).join(`
`) : e.trim()
}

function $r(e) {
    if (!Zr(e)) return;
    let t = typeof e.displayName == `string` ? e.displayName.trim() : ``,
        n = Qr(typeof e.description == `string` || Array.isArray(e.description) ? e.description : void 0);
    if (!(t === `` && n === ``)) return {displayName: t || `Бронежилет`, description: n}
}

function ei(e) {
    return (e?.type === `targeted` || e?.type === `targeted-multi`) && e.selectionMode === `manual`
}

function ti(e) {
    return e?.targetRules?.type === `global`
}

function ni(e) {
    return ei(e?.targetRules)
}

function ri(e) {
    let t = e?.targetRules;
    return t?.type === `targeted-multi` && t.selectionMode === `manual`
}

function ii(e, t) {
    let n = e.activeEffects?.find(e => e.name === `body_armor`) ?? e.effects?.find(e => e.name === `body_armor`);
    return n == null ? null : {
        displayName: n.displayName?.trim() || t?.displayName?.trim() || `Бронежилет`,
        description: Qr(n.description) || Qr(t?.description) || ``
    }
}

function ai(e, t, n) {
    if (t.length === 0) return e;
    let r = new Map(t.map(e => [e.id, e])), i = n?.find(e => e.name === `body_armor`) ?? null;
    return e.map(e => {
        if (e.bodyArmor != null) return e;
        let t = r.get(e.playerId);
        if (t == null) return e;
        let n = ii(t, i);
        return n == null ? e : {...e, bodyArmor: n}
    })
}

function oi(e) {
    return e == null || e.length === 0 ? null : e.map(e => ({
        playerId: e.playerId,
        name: e.name,
        status: e.status === `idle` || e.status === `content` || e.status === `casino` ? e.status : `idle`,
        canTarget: e.canTarget !== !1,
        reason: e.reason, ...typeof e.gnusReceivedToday == `number` ? {gnusReceivedToday: e.gnusReceivedToday} : {}, ...e.bodyArmor == null ? {} : {bodyArmor: e.bodyArmor}
    }))
}

function si(e) {
    return e == null ? null : oi(e.gnusWheel == null ? e.targets : e.gnusWheel.targets ?? e.targets)
}

function ci(e, t) {
    return ni(e) ? oi(t) ?? null : null
}

function li(e, t) {
    let n = ci(e, t);
    return n != null && n.some(e => e.canTarget)
}

function ui(e) {
    let t = e?.gnusWheel?.selectedTarget ?? e?.selectedTarget;
    return Zr(t) ? t : null
}

function di(e, t) {
    let n = si(e);
    if (n == null || n.length === 0 || li(t, n)) return null;
    let r = ui(e);
    return r == null || Jr(r) == null ? null : {
        id: `gnus-wheel-target-random`,
        type: `target-random`,
        candidates: n.map(e => ({
            playerId: e.playerId,
            id: e.playerId,
            name: e.name,
            status: e.status,
            canTarget: e.canTarget, ...e.reason == null ? {} : {reason: e.reason}, ...typeof e.gnusReceivedToday == `number` ? {gnusReceivedToday: e.gnusReceivedToday} : {}
        })),
        selected: r
    }
}

function fi(e) {
    return e.map((e, t) => {
        let n = $r(e.bodyArmor);
        return {
            playerId: String(e.playerId ?? e.id ?? t),
            name: String(e.name ?? `Игрок`),
            status: e.status === `idle` || e.status === `content` || e.status === `casino` ? e.status : `idle`,
            canTarget: e.canTarget !== !1,
            reason: typeof e.reason == `string` ? e.reason : void 0, ...typeof e.gnusReceivedToday == `number` ? {gnusReceivedToday: e.gnusReceivedToday} : {}, ...n == null ? {} : {bodyArmor: n}
        }
    })
}

var pi = {
    honeymad: `MAD.webp`,
    maddyson: `MAD.webp`,
    c_a_k_e: `CAKE.webp`,
    melharucos: `MEL.webp`,
    dunduk: `DUND.webp`,
    gladvalakas: `GLAD.webp`,
    nenormova: `NORM.webp`,
    f1ashko: `FLASH.webp`,
    viktorzu: `Zu.webp`,
    karmikkoala: `KARM.webp`,
    lasqa: `LASQA.webp`,
    arrowwoods: `ARA.webp`,
    praden: `PRAD.webp`,
    mistafaker: `FAK.webp`
}, mi = `FAK.webp`, hi = {
    honeymad: `mad.webp`,
    maddyson: `mad.webp`,
    c_a_k_e: `cake.webp`,
    melharucos: `mel.webp`,
    dunduk: `dund.webp`,
    gladvalakas: `glad.webp`,
    nenormova: `norm.webp`,
    f1ashko: `flash.webp`,
    viktorzu: `zu.webp`,
    karmikkoala: `karm.webp`,
    lasqa: `lsq.webp`,
    arrowwoods: `ara.webp`,
    praden: `prad.webp`,
    mistafaker: `fak.webp`
}, gi = `mad.webp`;

function _i(e) {
    return J(`avatars/${pi[e.toLowerCase()] ?? mi}`)
}

function vi(e) {
    let t = pi[e.toLowerCase()] ?? mi;
    return [...Lt(t.replace(/\.webp$/i, ``)).map(e => J(e)), J(`avatars/${t}`)]
}

function yi(e) {
    return J(`miniavatars/${hi[e.toLowerCase()] ?? gi}`)
}

var bi = {
    "target-already-has-effect": `На игрока уже действует этот эффект`,
    "gnus-daily-limit-reached": `Достигнут лимит гнусностей за сутки`
};

function xi(e) {
    if (!(e == null || e === ``)) return bi[e] ?? e
}

function Si(e) {
    return e.map(e => `${e.playerId}:${e.canTarget}`).join(`|`)
}

var Ci = `Количество гнусностей, полученных за текущие сутки`, wi = (0, Y.memo)(function ({
                                                                                              className: e,
                                                                                              ariaLabel: t,
                                                                                              tooltip: n,
                                                                                              tooltipClassName: r = `effect-target-picker-card-tooltip`,
                                                                                              children: i
                                                                                          }) {
    let a = (0, Y.useRef)(null), o = (0, Y.useRef)(null), [s, c] = (0, Y.useState)(!1), [l, u] = (0, Y.useState)(null);
    return (0, Y.useLayoutEffect)(() => {
        if (!s || !a.current || !o.current) return;
        let e = a.current.getBoundingClientRect(), t = o.current.getBoundingClientRect(), n = window.innerWidth,
            r = window.innerHeight, i = e.left + e.width / 2 - t.width / 2;
        i = Math.max(8, Math.min(i, n - t.width - 8));
        let c = e.top - t.height - 6;
        c < 8 && (c = e.bottom + 6), c = Math.max(8, Math.min(c, r - t.height - 8)), u({top: c, left: i})
    }, [s, n]), (0, X.jsxs)(X.Fragment, {
        children: [(0, X.jsx)(`span`, {
            ref: a,
            className: e,
            "aria-label": t,
            onMouseEnter: () => c(!0),
            onMouseLeave: () => {
                c(!1), u(null)
            },
            children: i
        }), s ? (0, Xn.createPortal)((0, X.jsx)(`div`, {
            ref: o,
            className: `${r}${l ? `` : ` effect-target-picker-card-tooltip--measuring`}`,
            style: l ? {top: l.top, left: l.left} : {top: -9999, left: -9999},
            role: `tooltip`,
            children: n
        }), document.body) : null]
    })
}), Ti = (0, Y.memo)(function ({gnusReceivedToday: e}) {
    return (0, X.jsxs)(wi, {
        className: `effect-target-picker-card-gnus-count`,
        ariaLabel: `${Ci}: ${e} из 4`,
        tooltip: Ci,
        children: [`(`, e, `/4)`]
    })
}), Ei = (0, Y.memo)(function ({bodyArmor: e}) {
    return (0, X.jsx)(wi, {
        className: `effect-target-picker-card-body-armor`,
        ariaLabel: `${e.displayName}. ${e.description}`,
        tooltip: (0, X.jsxs)(X.Fragment, {
            children: [(0, X.jsx)(`span`, {
                className: `effect-target-picker-card-tooltip-title`,
                children: e.displayName
            }), e.description]
        }),
        tooltipClassName: `effect-target-picker-card-tooltip effect-target-picker-card-tooltip--body-armor`,
        children: (0, X.jsx)(`img`, {
            className: `effect-target-picker-card-body-armor-icon`,
            src: Yn(Yr, `static`),
            alt: ``,
            draggable: !1
        })
    })
}), Di = (0, Y.memo)(function ({gnusReceivedToday: e, showGnusReceivedCount: t}) {
    return t ? (0, X.jsx)(`div`, {
        className: `effect-target-picker-card-badges`,
        children: (0, X.jsx)(Ti, {gnusReceivedToday: e ?? 0})
    }) : null
}), Oi = (0, Y.memo)(function ({
                                   playerId: e,
                                   name: t,
                                   canTarget: n,
                                   reason: r,
                                   gnusReceivedToday: i,
                                   bodyArmor: a,
                                   showBodyArmor: o,
                                   showGnusReceivedCount: s,
                                   isSelected: c,
                                   isDisabled: l,
                                   onSelect: u
                               }) {
    let d = (0, Y.useCallback)(() => {
        n && u(e)
    }, [n, u, e]), f = xi(r);
    return (0, X.jsxs)(`button`, {
        type: `button`,
        className: `effect-target-picker-card ${c ? `effect-target-picker-card--selected` : ``} ${l ? `effect-target-picker-card--disabled` : ``}`,
        disabled: l,
        title: !n && f ? f : t,
        onClick: d,
        children: [(0, X.jsx)(`img`, {
            className: `effect-target-picker-avatar`,
            src: yi(t),
            alt: t,
            draggable: !1
        }), o && a ? (0, X.jsx)(Ei, {bodyArmor: a}) : null, (0, X.jsx)(Di, {
            gnusReceivedToday: i,
            showGnusReceivedCount: s
        })]
    })
}), ki = ({
              targets: e,
              onClose: t,
              onConfirm: r,
              onConfirmMulti: i,
              submitting: a,
              effectDisplayName: o,
              effectName: s,
              variant: c,
              multiSelect: l,
              minSelections: u,
              maxSelections: d,
              selectionPlaceholder: p
          }) => {
    let {play: m} = oe(), {
            onBackHover: h,
            withBackClick: g
        } = n(), [_, v] = (0, Y.useState)(() => l ? null : e.find(e => e.canTarget)?.playerId ?? null), [y, b] = (0, Y.useState)(() => new Set),
        x = (0, Y.useCallback)(e => {
            if (!(!l && e === _)) {
                if (m(f.Menu_Button_Back_Click), l) {
                    (0, Xn.flushSync)(() => {
                        b(t => {
                            let n = new Set(t);
                            return n.has(e) ? n.delete(e) : (d == null || n.size < d) && n.add(e), n
                        })
                    });
                    return
                }
                (0, Xn.flushSync)(() => {
                    v(e)
                })
            }
        }, [d, l, m, _]), S = (0, Y.useMemo)(() => !(!l || y.size < u || d != null && y.size > d), [d, u, l, y]),
        C = l ? a || !S : a || _ == null, w = (0, Y.useCallback)(e => {
            C || m(f.Menu_Button_Back_Hover, {panElement: e.currentTarget})
        }, [C, m]), T = () => {
            C || (m(f.Menu_Button_Back_Click), l ? i?.(Array.from(y)) : _ && r(_))
        };
    E(!0), k(`A`, T, !C), k(`B`, () => {
        a || (m(f.Menu_Button_Back_Click), t())
    }, !a);
    let D = l && d != null && y.size >= d,
        O = (0, Y.useMemo)(() => e.some(e => typeof e.gnusReceivedToday == `number`), [e]), A = s === Xr;
    return (0, X.jsx)(`div`, {
        className: `effect-target-picker-overlay effect-target-picker-overlay--${c}`,
        role: `dialog`,
        "aria-modal": `true`,
        "aria-labelledby": `effect-target-picker-title`,
        children: (0, X.jsxs)(`div`, {
            className: `effect-target-picker-panel effect-target-picker-panel--${c}`,
            children: [(0, X.jsxs)(`div`, {
                className: `effect-target-picker-header`,
                children: [(0, X.jsx)(`h2`, {
                    id: `effect-target-picker-title`,
                    className: `effect-target-picker-title`,
                    children: l ? `Выберите игроков` : `Выберите игрока`
                }), (0, X.jsxs)(`button`, {
                    type: `button`,
                    className: `effect-target-picker-back`,
                    onClick: g(t),
                    onMouseEnter: h,
                    disabled: a,
                    "aria-label": `Назад`,
                    children: [(0, X.jsx)(`span`, {
                        className: `effect-target-picker-back-arrow`,
                        "aria-hidden": !0
                    }), (0, X.jsx)(`span`, {children: `Назад`}), (0, X.jsx)(q, {
                        button: `B`,
                        className: `effect-target-picker-back-gp-hint`
                    })]
                })]
            }), o ? (0, X.jsxs)(`p`, {
                className: `effect-target-picker-subtitle`,
                children: [`Цель для эффекта «`, o, `»`]
            }) : null, l ? (0, X.jsxs)(`p`, {
                className: `effect-target-picker-count`,
                children: [p ? (0, X.jsx)(`span`, {
                    className: `effect-target-picker-count-placeholder`,
                    children: p
                }) : null, `Выбрано: `, y.size, d == null ? `` : ` / ${d}`, u > 0 ? ` (минимум ${u})` : ``]
            }) : null, (0, X.jsx)(`div`, {
                className: `effect-target-picker-grid`, children: e.map(e => {
                    let t = l ? y.has(e.playerId) : e.playerId === _, n = !t && D, r = !e.canTarget, i = !l && t && !r;
                    return (0, X.jsx)(Oi, {
                        playerId: e.playerId,
                        name: e.name,
                        canTarget: !r && !n && !i,
                        reason: e.reason,
                        gnusReceivedToday: e.gnusReceivedToday,
                        bodyArmor: e.bodyArmor,
                        showBodyArmor: A,
                        showGnusReceivedCount: O,
                        isSelected: t && !r,
                        isDisabled: r || a || n || i,
                        onSelect: x
                    }, e.playerId)
                })
            }), (0, X.jsx)(`div`, {
                className: `effect-target-picker-actions`,
                children: (0, X.jsxs)(`button`, {
                    type: `button`,
                    className: `effect-target-picker-confirm ${C ? `effect-target-picker-confirm--disabled` : ``}`,
                    disabled: C,
                    onClick: T,
                    onMouseEnter: w,
                    children: [!a && !C && (0, X.jsx)(q, {
                        button: `A`,
                        className: `effect-target-picker-confirm-gp-hint`
                    }), a ? `Отправка` : `Подтвердить`]
                })
            })]
        })
    })
}, Ai = ({
             isOpen: e,
             onClose: t,
             targets: n,
             onConfirm: r,
             onConfirmMulti: i,
             submitting: a = !1,
             effectDisplayName: o,
             effectName: s,
             variant: c = `inscribed`,
             multiSelect: l = !1,
             minSelections: u = 0,
             maxSelections: d,
             selectionPlaceholder: f
         }) => e ? (0, X.jsx)(ki, {
    targets: n,
    onClose: t,
    onConfirm: r,
    onConfirmMulti: i,
    submitting: a,
    effectDisplayName: o,
    effectName: s,
    variant: c,
    multiSelect: l,
    minSelections: u,
    maxSelections: d ?? null,
    selectionPlaceholder: f
}, Si(n)) : null, ji = J(`miniavatars/random.webp`), Mi = J(`miniavatars/random-hover.webp`), Ni = 650;

function Pi(e) {
    let t = [...e];
    for (let e = t.length - 1; e > 0; --e) {
        let n = Math.floor(Math.random() * (e + 1));
        [t[e], t[n]] = [t[n], t[e]]
    }
    return t
}

function Fi(e, t, n, r) {
    let i = e.find(e => e.playerId === n)?.name ?? r ?? `Игрок`, a = yi(i),
        o = Pi(e.filter(e => e.canTarget && e.playerId !== n).map(e => ({avatarUrl: yi(e.name), name: e.name}))), s = 0,
        c = {};
    for (let n of e) {
        if (n.playerId === t) {
            c[n.playerId] = {avatarUrl: a, name: i};
            continue
        }
        if (n.canTarget && o.length > 0) {
            let e = o[s % o.length];
            s += 1, c[n.playerId] = e;
            continue
        }
        c[n.playerId] = {avatarUrl: yi(n.name), name: n.name}
    }
    return c
}

var Ii = (0, Y.memo)(function ({
                                   target: e,
                                   isHovered: t,
                                   isFlipping: n,
                                   isWinner: r,
                                   revealAssignment: i,
                                   disabled: a,
                                   onHover: o,
                                   onLeave: s,
                                   onPick: c
                               }) {
    let l = (0, Y.useCallback)(() => {
        a || c(e.playerId)
    }, [a, c, e.playerId]), u = t && !n ? Mi : ji;
    return (0, X.jsx)(`button`, {
        type: `button`,
        className: [`effect-random-target-card`, n ? `effect-random-target-card--flipping` : ``, r ? `effect-random-target-card--winner` : ``, a ? `effect-random-target-card--disabled` : ``].filter(Boolean).join(` `),
        disabled: a,
        onClick: l,
        onMouseEnter: () => o(e.playerId),
        onMouseLeave: s,
        children: (0, X.jsxs)(`div`, {
            className: `effect-random-target-card__flipper`,
            children: [(0, X.jsx)(`div`, {
                className: `effect-random-target-card__face effect-random-target-card__face--front`,
                children: (0, X.jsx)(`img`, {
                    className: `effect-random-target-card__mask`,
                    src: u,
                    alt: ``,
                    draggable: !1
                })
            }), (0, X.jsx)(`div`, {
                className: `effect-random-target-card__face effect-random-target-card__face--back`,
                children: i ? (0, X.jsx)(`img`, {
                    className: `effect-random-target-card__avatar`,
                    src: i.avatarUrl,
                    alt: i.name,
                    draggable: !1
                }) : null
            })]
        })
    })
}), Li = ({
              isOpen: e,
              onClose: t,
              targets: r,
              predeterminedPlayerId: i,
              predeterminedPlayerName: a,
              onConfirm: o,
              submitting: s = !1,
              effectDisplayName: c,
              showBackButton: l = !0
          }) => {
    let {play: u} = oe(), {
            onBackHover: d,
            onBackClick: p
        } = n(), [m, h] = (0, Y.useState)(null), [g, _] = (0, Y.useState)(null), [v, y] = (0, Y.useState)(!1), [b, x] = (0, Y.useState)(!1), [S, C] = (0, Y.useState)({}),
        w = (0, Y.useRef)(null), T = (0, Y.useCallback)(() => {
            w.current != null && (window.clearTimeout(w.current), w.current = null), h(null), _(null), y(!1), x(!1), C({})
        }, []);
    (0, Y.useEffect)(() => () => {
        w.current != null && window.clearTimeout(w.current)
    }, []);
    let D = (0, Y.useCallback)(e => {
        if (s || v || g != null) return;
        u(f.Menu_Button_Back_Click);
        let t = Fi(r, e, i, a);
        _(e), C(t), h(e), y(!0), w.current = window.setTimeout(() => {
            x(!0), w.current = null
        }, Ni)
    }, [g, v, u, i, a, s, r]), O = (0, Y.useCallback)(() => {
        !b || s || (u(f.Menu_Button_Back_Click), o())
    }, [b, o, u, s]), A = (0, Y.useCallback)(() => {
        p(), T(), t()
    }, [p, t, T]);
    E(e), k(`A`, O, e && b && !s), k(`B`, A, e && l && !s);
    let j = (0, Y.useMemo)(() => r.map(e => `${e.playerId}:${e.canTarget}:${e.name}`).join(`|`), [r]);
    return e ? (0, X.jsx)(`div`, {
        className: `effect-random-target-picker-overlay`,
        role: `dialog`,
        "aria-modal": `true`,
        "aria-labelledby": `effect-random-target-picker-title`,
        children: (0, X.jsxs)(`div`, {
            className: `effect-random-target-picker-panel`,
            children: [(0, X.jsxs)(`div`, {
                className: `effect-random-target-picker-header`,
                children: [(0, X.jsx)(`h2`, {
                    id: `effect-random-target-picker-title`,
                    className: `effect-random-target-picker-title`,
                    children: `Выберите игрока`
                }), l ? (0, X.jsxs)(`button`, {
                    type: `button`,
                    className: `effect-random-target-picker-back`,
                    onClick: A,
                    onMouseEnter: d,
                    disabled: s,
                    "aria-label": `Назад`,
                    children: [(0, X.jsx)(`span`, {
                        className: `effect-random-target-picker-back-arrow`,
                        "aria-hidden": !0
                    }), (0, X.jsx)(`span`, {children: `Назад`}), (0, X.jsx)(q, {
                        button: `B`,
                        className: `effect-random-target-picker-back-gp-hint`
                    })]
                }) : null]
            }), c ? (0, X.jsxs)(`p`, {
                className: `effect-random-target-picker-subtitle`,
                children: [`Случайная цель для эффекта «`, c, `»`]
            }) : null, (0, X.jsx)(`div`, {
                className: `effect-random-target-picker-grid`,
                children: r.map(e => (0, X.jsx)(Ii, {
                    target: e,
                    isHovered: m === e.playerId,
                    isFlipping: v,
                    isWinner: b && g != null ? e.playerId === g : !1,
                    revealAssignment: S[e.playerId] ?? null,
                    disabled: s || v || g != null,
                    onHover: e => {
                        s || v || g != null || h(e)
                    },
                    onLeave: () => {
                        v || g != null || h(null)
                    },
                    onPick: D
                }, e.playerId))
            }), (0, X.jsx)(`div`, {
                className: `effect-random-target-picker-actions`,
                children: (0, X.jsxs)(`button`, {
                    type: `button`,
                    className: `effect-random-target-picker-confirm ${!b || s ? `effect-random-target-picker-confirm--disabled` : ``}`,
                    disabled: !b || s,
                    onClick: O,
                    children: [!s && b && (0, X.jsx)(q, {
                        button: `A`,
                        className: `effect-random-target-picker-confirm-gp-hint`
                    }), s ? `Отправка` : `Подтвердить`]
                })
            })]
        })
    }, j) : null
};

function Ri() {
    let [e, t] = (0, Y.useState)(qt);
    return (0, Y.useEffect)(() => {
        let e = () => t(qt());
        return window.addEventListener(nn, e), () => {
            window.removeEventListener(nn, e)
        }
    }, []), e
}

var zi = {Ozon_idle: `casino/Ozon _idle.webm`};

function Bi(e) {
    return J(zi[e] ?? `casino/${e}.webm`)
}

var Vi = 5e3, Hi = 1e4, Ui = .2, Wi = 2;

function Gi() {
    return Vi + Math.floor(Math.random() * (Hi - Vi + 1))
}

function Ki(e) {
    let {randomAnimations: t} = e;
    return t.length === 0 ? e.idleAnimation : t[Math.floor(Math.random() * t.length)] ?? e.idleAnimation
}

function qi(e) {
    return [...new Set([e.idleAnimation.animation, e.rollAnimation.animation, ...e.randomAnimations.map(e => e.animation)])]
}

function Ji(e) {
    return e.readyState >= HTMLMediaElement.HAVE_CURRENT_DATA ? Promise.resolve() : new Promise(t => {
        let n = () => {
            e.removeEventListener(`loadeddata`, n), e.removeEventListener(`canplay`, n), t()
        };
        e.addEventListener(`loadeddata`, n), e.addEventListener(`canplay`, n)
    })
}

function Yi(e, t) {
    return Math.abs(e.currentTime - t) < .01 ? Promise.resolve() : new Promise(n => {
        let r = () => {
            e.removeEventListener(`seeked`, r), n()
        };
        e.addEventListener(`seeked`, r), e.currentTime = t
    })
}

function Xi(e, t) {
    return e.currentSrc === t || e.src === t
}

function Zi({dealer: e, isRolling: t, onRollDelayElapsed: n}) {
    let {play: r} = oe(), i = Ri(), a = (0, Y.useRef)(i), o = (0, Y.useRef)(Array.from({length: Wi}, () => null)),
        s = (0, Y.useRef)(0), c = (0, Y.useRef)(new Map), l = (0, Y.useRef)(null), u = (0, Y.useRef)(null),
        d = (0, Y.useRef)(t), f = (0, Y.useRef)(t), p = (0, Y.useRef)(n), m = (0, Y.useRef)(() => void 0);
    (0, Y.useEffect)(() => {
        d.current = t
    }, [t]), (0, Y.useEffect)(() => {
        p.current = n
    }, [n]), (0, Y.useEffect)(() => {
        a.current = i
    }, [i]);
    let h = (0, Y.useCallback)(() => {
        l.current != null && (window.clearTimeout(l.current), l.current = null)
    }, []), g = (0, Y.useCallback)(() => {
        u.current != null && (window.clearTimeout(u.current), u.current = null)
    }, []), _ = (0, Y.useCallback)(e => {
        s.current = e, o.current.forEach((t, n) => {
            if (t == null) return;
            let r = n === e;
            t.style.opacity = r ? `1` : `0`, t.style.zIndex = r ? `1` : `0`
        })
    }, []), v = (0, Y.useCallback)(async (e, t) => {
        let n = o.current[e];
        if (n == null) return;
        let r = Bi(t);
        if (c.current.get(e) === r && Xi(n, r)) {
            await Ji(n);
            return
        }
        n.src = r, n.loop = !1, n.load(), await Ji(n), c.current.set(e, r)
    }, []), y = (0, Y.useCallback)(() => 1 - s.current, []), b = (0, Y.useCallback)(async () => {
        let t = s.current, n = o.current[t];
        n != null && (await v(t, e.idleAnimation.animation), await Yi(n, 0), n.pause())
    }, [e.idleAnimation.animation, v]), x = (0, Y.useCallback)(async () => {
        let t = s.current, n = y(), r = o.current[n];
        r != null && (await v(n, e.idleAnimation.animation), await Yi(r, 0), r.pause(), _(n), o.current[t]?.pause())
    }, [e.idleAnimation.animation, y, v, _]), S = (0, Y.useCallback)(async ({animation: e, sound: t}) => {
        let n = s.current, i = y(), c = o.current[i];
        if (c != null) return a.current || r(t), await v(i, e), await Yi(c, 0), new Promise(e => {
            let t = () => {
                c.removeEventListener(`ended`, t), e()
            };
            c.addEventListener(`ended`, t), _(i), c.play().catch(() => {
                c.removeEventListener(`ended`, t), e()
            }), o.current[n]?.pause()
        })
    }, [y, v, r, _]), C = (0, Y.useCallback)(async () => {
        let t = qi(e), n = y();
        for (let r of t) r !== e.idleAnimation.animation && await v(n, r);
        await v(n, e.idleAnimation.animation)
    }, [e, y, v]), w = (0, Y.useCallback)(() => {
        h(), l.current = window.setTimeout(() => {
            d.current || a.current || (async () => {
                a.current || (await S(Math.random() < Ui ? Ki(e) : e.idleAnimation), !d.current && (await x(), m.current()))
            })()
        }, Gi())
    }, [h, e, S, x]);
    (0, Y.useEffect)(() => {
        m.current = w
    }, [w]), (0, Y.useEffect)(() => {
        if (i) {
            h();
            return
        }
        d.current || m.current()
    }, [i, h]);
    let T = (0, Y.useCallback)(e => t => {
        o.current[e] = t, t != null && (t.style.opacity = e === s.current ? `1` : `0`, t.style.zIndex = e === s.current ? `1` : `0`)
    }, []);
    return (0, Y.useEffect)(() => (c.current.clear(), (async () => {
        await v(0, e.idleAnimation.animation);
        let t = o.current[0];
        t != null && (await Yi(t, 0), t.pause()), _(0), await C(), !d.current && !a.current && (await b(), w())
    })(), () => {
        h(), g()
    }), [h, g, e, v, C, w, _, b]), (0, Y.useEffect)(() => {
        let n = f.current;
        if (f.current = t, !t) {
            g(), n && (async () => {
                await x(), a.current || w()
            })();
            return
        }
        h(), g(), S(e.rollAnimation), u.current = window.setTimeout(() => {
            p.current()
        }, e.rollDelay)
    }, [h, g, e.rollAnimation, e.rollDelay, t, S, x, w]), (0, X.jsxs)(`div`, {
        className: `wheel-dealer-animation`,
        "aria-hidden": !0,
        children: [Array.from({length: Wi}, (e, t) => (0, X.jsx)(`video`, {
            ref: T(t),
            className: `wheel-dealer-animation-video`,
            muted: !0,
            playsInline: !0,
            preload: `auto`
        }, t)), (0, X.jsx)(`div`, {className: `wheel-dealer-animation-shadow`})]
    })
}

var Qi = {
    blue: {base: `#7AE2FF`, accent: `#4EC8E8`},
    lightBlue: {base: `#9FDFCE`, accent: `#9FDFCE`},
    pink: {base: `#FFB8D4`, accent: `#FF8FB8`},
    yellow: {base: `#FFF06B`, accent: `#C4A018`},
    green: {base: `#CCF56C`, accent: `#CCF56C`},
    orange: {base: `#FFAB82`, accent: `#E87850`}
}, $i = [{color: `lightBlue`, pattern: `stripes`}, {color: `pink`, pattern: `stripes`}, {
    color: `yellow`,
    pattern: `dots`
}, {color: `lightBlue`, pattern: `stripes`}, {color: `blue`, pattern: `stripes`}, {
    color: `orange`,
    pattern: `dots`
}, {color: `green`, pattern: `dots`}, {color: `pink`, pattern: `stripes`}, {
    color: `yellow`,
    pattern: `dots`
}, {color: `lightBlue`, pattern: `stripes`}, {color: `blue`, pattern: `stripes`}, {
    color: `orange`,
    pattern: `dots`
}, {color: `green`, pattern: `dots`}, {color: `pink`, pattern: `stripes`}, {
    color: `yellow`,
    pattern: `dots`
}, {color: `blue`, pattern: `stripes`}];

function ea(e, t) {
    return t <= 0 ? -45 : (e + .5) * (360 / t) - 45
}

function ta(e, t, n, r) {
    let {base: i, accent: a} = Qi[e], o = ea(n, r);
    return t === `stripes` ? {
        backgroundColor: i,
        backgroundImage: `repeating-linear-gradient(${o}deg, ${i}, ${i} 5px, ${a} 5px, ${a} 10px)`
    } : {
        backgroundColor: i,
        backgroundImage: `radial-gradient(circle, ${a} 1.5px, transparent 1.5px)`,
        backgroundSize: `10px 10px`,
        transform: `rotate(${o}deg)`
    }
}

function na(e, t) {
    let n = e % $i.length, r = $i[n];
    if (t > 1 && e === t - 1) {
        let e = $i[0].color;
        if (r.color === e) for (let t = 1; t < $i.length; t += 1) {
            let i = $i[(n + t) % $i.length];
            if (i.color !== e) {
                r = i;
                break
            }
        }
    }
    return r
}

function ra(e, t) {
    let n = na(e, t);
    return ta(n.color, n.pattern, e, t)
}

function ia(e, t) {
    if (t <= 0) return `none`;
    let n = 360 / t * e, r = (e + 1) * (360 / t), i = r - n, a = e => {
        let t = e * Math.PI / 180;
        return `${50 + 50 * Math.sin(t)}% ${50 - 50 * Math.cos(t)}%`
    }, o = Math.max(4, Math.ceil(i / 2)), s = [`50% 50%`, a(n)];
    for (let e = 1; e < o; e += 1) {
        let t = n + i * e / o;
        s.push(a(t))
    }
    return s.push(a(r)), `polygon(${s.join(`, `)})`
}

var aa = Array.isArray, oa = function (e) {
    var t = ``;
    if (typeof e == `string`) t += e; else if (typeof e == `object`) {
        var n = void 0;
        if (aa(e)) for (var r = 0, i = e.length; r < i;) n = oa(e[r++]), n && (t && (t += ` `), t += n); else for (n in e) n && e[n] && (t && (t += ` `), t += n)
    }
    return t
};

function sa() {
    for (var e = arguments.length, t = 0, n, r, i = ``; t < e;) n = arguments[t++], n && (r = oa(n), r && (i && (i += ` `), i += r));
    return i
}

function ca(e, t, n, r) {
    for (; t >= n && !e(`(min-resolution: ` + t / r + `dppx)`).matches;) t--;
    return t
}

function la(e) {
    if (e === void 0 && (e = window), !e) return 1;
    if (e.devicePixelRatio !== void 0) return e.devicePixelRatio;
    var t = e.document.frames;
    return t === void 0 ? e.matchMedia === void 0 ? 1 : function (e) {
        for (var t = e.matchMedia, n = 10, r = .1, i = 1, a = n, o = 0; o < 4; o++) n = (a = 10 * ca(t, n, r, i)) + 9, r = a, i *= 10;
        return a / i
    }(e) : t.devicePixelRatio === void 0 ? t.screen.deviceXDPI / t.screen.systemXDPI : t.devicePixelRatio
}

var ua = w(((e, t) => {
    t.exports = `SECRET_DO_NOT_PASS_THIS_OR_YOU_WILL_BE_FIRED`
})), da = w(((e, t) => {
    var n = ua();

    function r() {
    }

    function i() {
    }

    i.resetWarningCache = r, t.exports = function () {
        function e(e, t, r, i, a, o) {
            if (o !== n) {
                var s = Error("Calling PropTypes validators directly is not supported by the `prop-types` package. Use PropTypes.checkPropTypes() to call them. Read more at http://fb.me/use-check-prop-types");
                throw s.name = `Invariant Violation`, s
            }
        }

        e.isRequired = e;

        function t() {
            return e
        }

        var a = {
            array: e,
            bigint: e,
            bool: e,
            func: e,
            number: e,
            object: e,
            string: e,
            symbol: e,
            any: e,
            arrayOf: t,
            element: e,
            elementType: e,
            instanceOf: t,
            node: e,
            objectOf: t,
            oneOf: t,
            oneOfType: t,
            shape: t,
            exact: t,
            checkPropTypes: i,
            resetWarningCache: r
        };
        return a.PropTypes = a, a
    }
})), fa = w(((e, t) => {
    t.exports = da()()
})), pa = w(((e, t) => {
    function n(e) {
        var t, r, i = ``;
        if (typeof e == `string` || typeof e == `number`) i += e; else if (typeof e == `object`) if (Array.isArray(e)) {
            var a = e.length;
            for (t = 0; t < a; t++) e[t] && (r = n(e[t])) && (i && (i += ` `), i += r)
        } else for (r in e) e[r] && (i && (i += ` `), i += r);
        return i
    }

    function r() {
        for (var e, t, r = 0, i = ``, a = arguments.length; r < a; r++) (e = arguments[r]) && (t = n(e)) && (i && (i += ` `), i += t);
        return i
    }

    t.exports = r, t.exports.clsx = r
})), ma = w((e => {
    Object.defineProperty(e, `__esModule`, {value: !0}), e.dontSetMe = a, e.findInArray = t, e.int = i, e.isFunction = n, e.isNum = r;

    function t(e, t) {
        for (let n = 0, r = e.length; n < r; n++) if (t.apply(t, [e[n], n, e])) return e[n]
    }

    function n(e) {
        return typeof e == `function` || Object.prototype.toString.call(e) === `[object Function]`
    }

    function r(e) {
        return typeof e == `number` && !isNaN(e)
    }

    function i(e) {
        return parseInt(e, 10)
    }

    function a(e, t, n) {
        if (e[t]) return Error(`Invalid prop ${t} passed to ${n} - do not set this, set it on the child.`)
    }
})), ha = w((e => {
    Object.defineProperty(e, `__esModule`, {value: !0}), e.browserPrefixToKey = r, e.browserPrefixToStyle = i, e.default = void 0, e.getPrefix = n;
    var t = [`Moz`, `Webkit`, `O`, `ms`];

    function n() {
        let e = arguments.length > 0 && arguments[0] !== void 0 ? arguments[0] : `transform`;
        if (typeof window > `u`) return ``;
        let n = window.document?.documentElement?.style;
        if (!n || e in n) return ``;
        for (let i = 0; i < t.length; i++) if (r(e, t[i]) in n) return t[i];
        return ``
    }

    function r(e, t) {
        return t ? `${t}${a(e)}` : e
    }

    function i(e, t) {
        return t ? `-${t.toLowerCase()}-${e}` : e
    }

    function a(e) {
        let t = ``, n = !0;
        for (let r = 0; r < e.length; r++) n ? (t += e[r].toUpperCase(), n = !1) : e[r] === `-` ? n = !0 : t += e[r];
        return t
    }

    e.default = n()
})), ga = w((e => {
    Object.defineProperty(e, `__esModule`, {value: !0}), e.addClassName = S, e.addEvent = s, e.addUserSelectStyles = y, e.createCSSTransform = m, e.createSVGTransform = h, e.getTouch = _, e.getTouchIdentifier = v, e.getTranslation = g, e.innerHeight = d, e.innerWidth = f, e.matchesSelector = a, e.matchesSelectorAndParentsTo = o, e.offsetXYFromParent = p, e.outerHeight = l, e.outerWidth = u, e.removeClassName = C, e.removeEvent = c, e.scheduleRemoveUserSelectStyles = b;
    var t = ma(), n = r(ha());

    function r(e, t) {
        if (typeof WeakMap == `function`) var n = new WeakMap, i = new WeakMap;
        return (r = function (e, t) {
            if (!t && e && e.__esModule) return e;
            var r, a, o = {__proto__: null, default: e};
            if (e === null || typeof e != `object` && typeof e != `function`) return o;
            if (r = t ? i : n) {
                if (r.has(e)) return r.get(e);
                r.set(e, o)
            }
            for (let t in e) t !== `default` && {}.hasOwnProperty.call(e, t) && ((a = (r = Object.defineProperty) && Object.getOwnPropertyDescriptor(e, t)) && (a.get || a.set) ? r(o, t, a) : o[t] = e[t]);
            return o
        })(e, t)
    }

    var i = ``;

    function a(e, n) {
        return i ||= (0, t.findInArray)([`matches`, `webkitMatchesSelector`, `mozMatchesSelector`, `msMatchesSelector`, `oMatchesSelector`], function (n) {
            return (0, t.isFunction)(e[n])
        }), (0, t.isFunction)(e[i]) ? e[i](n) : !1
    }

    function o(e, t, n) {
        let r = e;
        do {
            if (a(r, t)) return !0;
            if (r === n) return !1;
            r = r.parentNode
        } while (r);
        return !1
    }

    function s(e, t, n, r) {
        if (!e) return;
        let i = {capture: !0, ...r};
        e.addEventListener ? e.addEventListener(t, n, i) : e.attachEvent ? e.attachEvent(`on` + t, n) : e[`on` + t] = n
    }

    function c(e, t, n, r) {
        if (!e) return;
        let i = {capture: !0, ...r};
        e.removeEventListener ? e.removeEventListener(t, n, i) : e.detachEvent ? e.detachEvent(`on` + t, n) : e[`on` + t] = null
    }

    function l(e) {
        let n = e.clientHeight, r = e.ownerDocument.defaultView.getComputedStyle(e);
        return n += (0, t.int)(r.borderTopWidth), n += (0, t.int)(r.borderBottomWidth), n
    }

    function u(e) {
        let n = e.clientWidth, r = e.ownerDocument.defaultView.getComputedStyle(e);
        return n += (0, t.int)(r.borderLeftWidth), n += (0, t.int)(r.borderRightWidth), n
    }

    function d(e) {
        let n = e.clientHeight, r = e.ownerDocument.defaultView.getComputedStyle(e);
        return n -= (0, t.int)(r.paddingTop), n -= (0, t.int)(r.paddingBottom), n
    }

    function f(e) {
        let n = e.clientWidth, r = e.ownerDocument.defaultView.getComputedStyle(e);
        return n -= (0, t.int)(r.paddingLeft), n -= (0, t.int)(r.paddingRight), n
    }

    function p(e, t, n) {
        let r = t === t.ownerDocument.body ? {left: 0, top: 0} : t.getBoundingClientRect();
        return {x: (e.clientX + t.scrollLeft - r.left) / n, y: (e.clientY + t.scrollTop - r.top) / n}
    }

    function m(e, t) {
        let r = g(e, t, `px`);
        return {[(0, n.browserPrefixToKey)(`transform`, n.default)]: r}
    }

    function h(e, t) {
        return g(e, t, ``)
    }

    function g(e, t, n) {
        let {x: r, y: i} = e, a = `translate(${r}${n},${i}${n})`;
        return t && (a = `translate(${`${typeof t.x == `string` ? t.x : t.x + n}`}, ${`${typeof t.y == `string` ? t.y : t.y + n}`})` + a), a
    }

    function _(e, n) {
        return e.targetTouches && (0, t.findInArray)(e.targetTouches, e => n === e.identifier) || e.changedTouches && (0, t.findInArray)(e.changedTouches, e => n === e.identifier)
    }

    function v(e) {
        if (e.targetTouches && e.targetTouches[0]) return e.targetTouches[0].identifier;
        if (e.changedTouches && e.changedTouches[0]) return e.changedTouches[0].identifier
    }

    function y(e) {
        if (!e) return;
        let t = e.getElementById(`react-draggable-style-el`);
        t || (t = e.createElement(`style`), t.type = `text/css`, t.id = `react-draggable-style-el`, t.innerHTML = `.react-draggable-transparent-selection *::-moz-selection {all: inherit;}
`, t.innerHTML += `.react-draggable-transparent-selection *::selection {all: inherit;}
`, e.getElementsByTagName(`head`)[0].appendChild(t)), e.body && S(e.body, `react-draggable-transparent-selection`)
    }

    function b(e) {
        window.requestAnimationFrame ? window.requestAnimationFrame(() => {
            x(e)
        }) : x(e)
    }

    function x(e) {
        if (e) try {
            if (e.body && C(e.body, `react-draggable-transparent-selection`), e.selection) e.selection.empty(); else {
                let t = (e.defaultView || window).getSelection();
                t && t.type !== `Caret` && t.removeAllRanges()
            }
        } catch {
        }
    }

    function S(e, t) {
        e.classList ? e.classList.add(t) : e.className.match(RegExp(`(?:^|\\s)${t}(?!\\S)`)) || (e.className += ` ${t}`)
    }

    function C(e, t) {
        e.classList ? e.classList.remove(t) : e.className = e.className.replace(RegExp(`(?:^|\\s)${t}(?!\\S)`, `g`), ``)
    }
})), _a = w((e => {
    Object.defineProperty(e, `__esModule`, {value: !0}), e.canDragX = a, e.canDragY = o, e.createCoreData = c, e.createDraggableData = l, e.getBoundPosition = r, e.getControlPosition = s, e.snapToGrid = i;
    var t = ma(), n = ga();

    function r(e, r, i) {
        if (!e.props.bounds) return [r, i];
        let {bounds: a} = e.props;
        a = typeof a == `string` ? a : u(a);
        let o = d(e);
        if (typeof a == `string`) {
            let {ownerDocument: e} = o, r = e.defaultView, i;
            if (i = a === `parent` ? o.parentNode : o.getRootNode().querySelector(a), !(i instanceof r.HTMLElement)) throw Error(`Bounds selector "` + a + `" could not find an element.`);
            let s = i, c = r.getComputedStyle(o), l = r.getComputedStyle(s);
            a = {
                left: -o.offsetLeft + (0, t.int)(l.paddingLeft) + (0, t.int)(c.marginLeft),
                top: -o.offsetTop + (0, t.int)(l.paddingTop) + (0, t.int)(c.marginTop),
                right: (0, n.innerWidth)(s) - (0, n.outerWidth)(o) - o.offsetLeft + (0, t.int)(l.paddingRight) - (0, t.int)(c.marginRight),
                bottom: (0, n.innerHeight)(s) - (0, n.outerHeight)(o) - o.offsetTop + (0, t.int)(l.paddingBottom) - (0, t.int)(c.marginBottom)
            }
        }
        return (0, t.isNum)(a.right) && (r = Math.min(r, a.right)), (0, t.isNum)(a.bottom) && (i = Math.min(i, a.bottom)), (0, t.isNum)(a.left) && (r = Math.max(r, a.left)), (0, t.isNum)(a.top) && (i = Math.max(i, a.top)), [r, i]
    }

    function i(e, t, n) {
        return [Math.round(t / e[0]) * e[0], Math.round(n / e[1]) * e[1]]
    }

    function a(e) {
        return e.props.axis === `both` || e.props.axis === `x`
    }

    function o(e) {
        return e.props.axis === `both` || e.props.axis === `y`
    }

    function s(e, t, r) {
        let i = typeof t == `number` ? (0, n.getTouch)(e, t) : null;
        if (typeof t == `number` && !i) return null;
        let a = d(r), o = r.props.offsetParent || a.offsetParent || a.ownerDocument.body;
        return (0, n.offsetXYFromParent)(i || e, o, r.props.scale)
    }

    function c(e, n, r) {
        let i = !(0, t.isNum)(e.lastX), a = d(e);
        return i ? {node: a, deltaX: 0, deltaY: 0, lastX: n, lastY: r, x: n, y: r} : {
            node: a,
            deltaX: n - e.lastX,
            deltaY: r - e.lastY,
            lastX: e.lastX,
            lastY: e.lastY,
            x: n,
            y: r
        }
    }

    function l(e, t) {
        let n = e.props.scale;
        return {
            node: t.node,
            x: e.state.x + t.deltaX / n,
            y: e.state.y + t.deltaY / n,
            deltaX: t.deltaX / n,
            deltaY: t.deltaY / n,
            lastX: e.state.x,
            lastY: e.state.y
        }
    }

    function u(e) {
        return {left: e.left, top: e.top, right: e.right, bottom: e.bottom}
    }

    function d(e) {
        let t = e.findDOMNode();
        if (!t) throw Error(`<DraggableCore>: Unmounted during event!`);
        return t
    }
})), va = w((e => {
    Object.defineProperty(e, `__esModule`, {value: !0}), e.default = t;

    function t() {
    }
})), ya = w((e => {
    Object.defineProperty(e, `__esModule`, {value: !0}), e.default = void 0;
    var t = l(P()), n = c(fa()), r = c(Se()), i = ga(), a = _a(), o = ma(), s = c(va());

    function c(e) {
        return e && e.__esModule ? e : {default: e}
    }

    function l(e, t) {
        if (typeof WeakMap == `function`) var n = new WeakMap, r = new WeakMap;
        return (l = function (e, t) {
            if (!t && e && e.__esModule) return e;
            var i, a, o = {__proto__: null, default: e};
            if (e === null || typeof e != `object` && typeof e != `function`) return o;
            if (i = t ? r : n) {
                if (i.has(e)) return i.get(e);
                i.set(e, o)
            }
            for (let t in e) t !== `default` && {}.hasOwnProperty.call(e, t) && ((a = (i = Object.defineProperty) && Object.getOwnPropertyDescriptor(e, t)) && (a.get || a.set) ? i(o, t, a) : o[t] = e[t]);
            return o
        })(e, t)
    }

    function u(e, t, n) {
        return (t = d(t)) in e ? Object.defineProperty(e, t, {
            value: n,
            enumerable: !0,
            configurable: !0,
            writable: !0
        }) : e[t] = n, e
    }

    function d(e) {
        var t = f(e, `string`);
        return typeof t == `symbol` ? t : t + ``
    }

    function f(e, t) {
        if (typeof e != `object` || !e) return e;
        var n = e[Symbol.toPrimitive];
        if (n !== void 0) {
            var r = n.call(e, t || `default`);
            if (typeof r != `object`) return r;
            throw TypeError(`@@toPrimitive must return a primitive value.`)
        }
        return (t === `string` ? String : Number)(e)
    }

    var p = {
        touch: {start: `touchstart`, move: `touchmove`, stop: `touchend`},
        mouse: {start: `mousedown`, move: `mousemove`, stop: `mouseup`}
    }, m = p.mouse, h = class extends t.Component {
        constructor() {
            super(...arguments), u(this, `dragging`, !1), u(this, `lastX`, NaN), u(this, `lastY`, NaN), u(this, `touchIdentifier`, null), u(this, `mounted`, !1), u(this, `handleDragStart`, e => {
                if (this.props.onMouseDown(e), !this.props.allowAnyClick && typeof e.button == `number` && e.button !== 0) return !1;
                let t = this.findDOMNode();
                if (!t || !t.ownerDocument || !t.ownerDocument.body) throw Error(`<DraggableCore> not mounted on DragStart!`);
                let {ownerDocument: n} = t;
                if (this.props.disabled || !(e.target instanceof n.defaultView.Node) || this.props.handle && !(0, i.matchesSelectorAndParentsTo)(e.target, this.props.handle, t) || this.props.cancel && (0, i.matchesSelectorAndParentsTo)(e.target, this.props.cancel, t)) return;
                e.type === `touchstart` && !this.props.allowMobileScroll && e.preventDefault();
                let r = (0, i.getTouchIdentifier)(e);
                this.touchIdentifier = r;
                let o = (0, a.getControlPosition)(e, r, this);
                if (o == null) return;
                let {x: c, y: l} = o, u = (0, a.createCoreData)(this, c, l);
                (0, s.default)(`DraggableCore: handleDragStart: %j`, u), (0, s.default)(`calling`, this.props.onStart), !(this.props.onStart(e, u) === !1 || this.mounted === !1) && (this.props.enableUserSelectHack && (0, i.addUserSelectStyles)(n), this.dragging = !0, this.lastX = c, this.lastY = l, (0, i.addEvent)(n, m.move, this.handleDrag), (0, i.addEvent)(n, m.stop, this.handleDragStop))
            }), u(this, `handleDrag`, e => {
                let t = (0, a.getControlPosition)(e, this.touchIdentifier, this);
                if (t == null) return;
                let {x: n, y: r} = t;
                if (Array.isArray(this.props.grid)) {
                    let e = n - this.lastX, t = r - this.lastY;
                    if ([e, t] = (0, a.snapToGrid)(this.props.grid, e, t), !e && !t) return;
                    n = this.lastX + e, r = this.lastY + t
                }
                let i = (0, a.createCoreData)(this, n, r);
                if ((0, s.default)(`DraggableCore: handleDrag: %j`, i), this.props.onDrag(e, i) === !1 || this.mounted === !1) {
                    try {
                        this.handleDragStop(new MouseEvent(`mouseup`))
                    } catch {
                        let e = document.createEvent(`MouseEvents`);
                        e.initMouseEvent(`mouseup`, !0, !0, window, 0, 0, 0, 0, 0, !1, !1, !1, !1, 0, null), this.handleDragStop(e)
                    }
                    return
                }
                this.lastX = n, this.lastY = r
            }), u(this, `handleDragStop`, e => {
                if (!this.dragging) return;
                let t = (0, a.getControlPosition)(e, this.touchIdentifier, this);
                if (t == null) return;
                let {x: n, y: r} = t;
                if (Array.isArray(this.props.grid)) {
                    let e = n - this.lastX || 0, t = r - this.lastY || 0;
                    [e, t] = (0, a.snapToGrid)(this.props.grid, e, t), n = this.lastX + e, r = this.lastY + t
                }
                let o = (0, a.createCoreData)(this, n, r);
                if (this.props.onStop(e, o) === !1 || this.mounted === !1) return !1;
                let c = this.findDOMNode();
                c && this.props.enableUserSelectHack && (0, i.scheduleRemoveUserSelectStyles)(c.ownerDocument), (0, s.default)(`DraggableCore: handleDragStop: %j`, o), this.dragging = !1, this.lastX = NaN, this.lastY = NaN, c && ((0, s.default)(`DraggableCore: Removing handlers`), (0, i.removeEvent)(c.ownerDocument, m.move, this.handleDrag), (0, i.removeEvent)(c.ownerDocument, m.stop, this.handleDragStop))
            }), u(this, `onMouseDown`, e => (m = p.mouse, this.handleDragStart(e))), u(this, `onMouseUp`, e => (m = p.mouse, this.handleDragStop(e))), u(this, `onTouchStart`, e => (m = p.touch, this.handleDragStart(e))), u(this, `onTouchEnd`, e => (m = p.touch, this.handleDragStop(e)))
        }

        componentDidMount() {
            this.mounted = !0;
            let e = this.findDOMNode();
            e && (0, i.addEvent)(e, p.touch.start, this.onTouchStart, {passive: !1})
        }

        componentWillUnmount() {
            this.mounted = !1;
            let e = this.findDOMNode();
            if (e) {
                let {ownerDocument: t} = e;
                (0, i.removeEvent)(t, p.mouse.move, this.handleDrag), (0, i.removeEvent)(t, p.touch.move, this.handleDrag), (0, i.removeEvent)(t, p.mouse.stop, this.handleDragStop), (0, i.removeEvent)(t, p.touch.stop, this.handleDragStop), (0, i.removeEvent)(e, p.touch.start, this.onTouchStart, {passive: !1}), this.props.enableUserSelectHack && (0, i.scheduleRemoveUserSelectStyles)(t)
            }
        }

        findDOMNode() {
            return this.props?.nodeRef ? this.props?.nodeRef?.current : r.default.findDOMNode(this)
        }

        render() {
            return t.cloneElement(t.Children.only(this.props.children), {
                onMouseDown: this.onMouseDown,
                onMouseUp: this.onMouseUp,
                onTouchEnd: this.onTouchEnd
            })
        }
    };
    e.default = h, u(h, `displayName`, `DraggableCore`), u(h, `propTypes`, {
        allowAnyClick: n.default.bool,
        allowMobileScroll: n.default.bool,
        children: n.default.node.isRequired,
        disabled: n.default.bool,
        enableUserSelectHack: n.default.bool,
        offsetParent: function (e, t) {
            if (e[t] && e[t].nodeType !== 1) throw Error(`Draggable's offsetParent must be a DOM Node.`)
        },
        grid: n.default.arrayOf(n.default.number),
        handle: n.default.string,
        cancel: n.default.string,
        nodeRef: n.default.object,
        onStart: n.default.func,
        onDrag: n.default.func,
        onStop: n.default.func,
        onMouseDown: n.default.func,
        scale: n.default.number,
        className: o.dontSetMe,
        style: o.dontSetMe,
        transform: o.dontSetMe
    }), u(h, `defaultProps`, {
        allowAnyClick: !1,
        allowMobileScroll: !1,
        disabled: !1,
        enableUserSelectHack: !0,
        onStart: function () {
        },
        onDrag: function () {
        },
        onStop: function () {
        },
        onMouseDown: function () {
        },
        scale: 1
    })
})), ba = w((e => {
    Object.defineProperty(e, `__esModule`, {value: !0}), Object.defineProperty(e, `DraggableCore`, {
        enumerable: !0,
        get: function () {
            return c.default
        }
    }), e.default = void 0;
    var t = d(P()), n = u(fa()), r = u(Se()), i = pa(), a = ga(), o = _a(), s = ma(), c = u(ya()), l = u(va());

    function u(e) {
        return e && e.__esModule ? e : {default: e}
    }

    function d(e, t) {
        if (typeof WeakMap == `function`) var n = new WeakMap, r = new WeakMap;
        return (d = function (e, t) {
            if (!t && e && e.__esModule) return e;
            var i, a, o = {__proto__: null, default: e};
            if (e === null || typeof e != `object` && typeof e != `function`) return o;
            if (i = t ? r : n) {
                if (i.has(e)) return i.get(e);
                i.set(e, o)
            }
            for (let t in e) t !== `default` && {}.hasOwnProperty.call(e, t) && ((a = (i = Object.defineProperty) && Object.getOwnPropertyDescriptor(e, t)) && (a.get || a.set) ? i(o, t, a) : o[t] = e[t]);
            return o
        })(e, t)
    }

    function f() {
        return f = Object.assign ? Object.assign.bind() : function (e) {
            for (var t = 1; t < arguments.length; t++) {
                var n = arguments[t];
                for (var r in n) ({}).hasOwnProperty.call(n, r) && (e[r] = n[r])
            }
            return e
        }, f.apply(null, arguments)
    }

    function p(e, t, n) {
        return (t = m(t)) in e ? Object.defineProperty(e, t, {
            value: n,
            enumerable: !0,
            configurable: !0,
            writable: !0
        }) : e[t] = n, e
    }

    function m(e) {
        var t = h(e, `string`);
        return typeof t == `symbol` ? t : t + ``
    }

    function h(e, t) {
        if (typeof e != `object` || !e) return e;
        var n = e[Symbol.toPrimitive];
        if (n !== void 0) {
            var r = n.call(e, t || `default`);
            if (typeof r != `object`) return r;
            throw TypeError(`@@toPrimitive must return a primitive value.`)
        }
        return (t === `string` ? String : Number)(e)
    }

    var g = class extends t.Component {
        static getDerivedStateFromProps(e, t) {
            let {position: n} = e, {prevPropsPosition: r} = t;
            return n && (!r || n.x !== r.x || n.y !== r.y) ? ((0, l.default)(`Draggable: getDerivedStateFromProps %j`, {
                position: n,
                prevPropsPosition: r
            }), {x: n.x, y: n.y, prevPropsPosition: {...n}}) : null
        }

        constructor(e) {
            super(e), p(this, `onDragStart`, (e, t) => {
                if ((0, l.default)(`Draggable: onDragStart: %j`, t), this.props.onStart(e, (0, o.createDraggableData)(this, t)) === !1) return !1;
                this.setState({dragging: !0, dragged: !0})
            }), p(this, `onDrag`, (e, t) => {
                if (!this.state.dragging) return !1;
                (0, l.default)(`Draggable: onDrag: %j`, t);
                let n = (0, o.createDraggableData)(this, t), r = {x: n.x, y: n.y, slackX: 0, slackY: 0};
                if (this.props.bounds) {
                    let {x: e, y: t} = r;
                    r.x += this.state.slackX, r.y += this.state.slackY;
                    let [i, a] = (0, o.getBoundPosition)(this, r.x, r.y);
                    r.x = i, r.y = a, r.slackX = this.state.slackX + (e - r.x), r.slackY = this.state.slackY + (t - r.y), n.x = r.x, n.y = r.y, n.deltaX = r.x - this.state.x, n.deltaY = r.y - this.state.y
                }
                if (this.props.onDrag(e, n) === !1) return !1;
                this.setState(r)
            }), p(this, `onDragStop`, (e, t) => {
                if (!this.state.dragging || this.props.onStop(e, (0, o.createDraggableData)(this, t)) === !1) return !1;
                (0, l.default)(`Draggable: onDragStop: %j`, t);
                let n = {dragging: !1, slackX: 0, slackY: 0};
                if (this.props.position) {
                    let {x: e, y: t} = this.props.position;
                    n.x = e, n.y = t
                }
                this.setState(n)
            }), this.state = {
                dragging: !1,
                dragged: !1,
                x: e.position ? e.position.x : e.defaultPosition.x,
                y: e.position ? e.position.y : e.defaultPosition.y,
                prevPropsPosition: {...e.position},
                slackX: 0,
                slackY: 0,
                isElementSVG: !1
            }, e.position && !(e.onDrag || e.onStop) && console.warn("A `position` was applied to this <Draggable>, without drag handlers. This will make this component effectively undraggable. Please attach `onDrag` or `onStop` handlers so you can adjust the `position` of this element.")
        }

        componentDidMount() {
            window.SVGElement !== void 0 && this.findDOMNode() instanceof window.SVGElement && this.setState({isElementSVG: !0})
        }

        componentWillUnmount() {
            this.state.dragging && this.setState({dragging: !1})
        }

        findDOMNode() {
            return this.props?.nodeRef?.current ?? r.default.findDOMNode(this)
        }

        render() {
            let {
                axis: e,
                bounds: n,
                children: r,
                defaultPosition: s,
                defaultClassName: l,
                defaultClassNameDragging: u,
                defaultClassNameDragged: d,
                position: p,
                positionOffset: m,
                scale: h,
                ...g
            } = this.props, _ = {}, v = null, y = !p || this.state.dragging, b = p || s, x = {
                x: (0, o.canDragX)(this) && y ? this.state.x : b.x,
                y: (0, o.canDragY)(this) && y ? this.state.y : b.y
            };
            this.state.isElementSVG ? v = (0, a.createSVGTransform)(x, m) : _ = (0, a.createCSSTransform)(x, m);
            let S = (0, i.clsx)(r.props.className || ``, l, {[u]: this.state.dragging, [d]: this.state.dragged});
            return t.createElement(c.default, f({}, g, {
                onStart: this.onDragStart,
                onDrag: this.onDrag,
                onStop: this.onDragStop
            }), t.cloneElement(t.Children.only(r), {className: S, style: {...r.props.style, ..._}, transform: v}))
        }
    };
    e.default = g, p(g, `displayName`, `Draggable`), p(g, `propTypes`, {
        ...c.default.propTypes,
        axis: n.default.oneOf([`both`, `x`, `y`, `none`]),
        bounds: n.default.oneOfType([n.default.shape({
            left: n.default.number,
            right: n.default.number,
            top: n.default.number,
            bottom: n.default.number
        }), n.default.string, n.default.oneOf([!1])]),
        defaultClassName: n.default.string,
        defaultClassNameDragging: n.default.string,
        defaultClassNameDragged: n.default.string,
        defaultPosition: n.default.shape({x: n.default.number, y: n.default.number}),
        positionOffset: n.default.shape({
            x: n.default.oneOfType([n.default.number, n.default.string]),
            y: n.default.oneOfType([n.default.number, n.default.string])
        }),
        position: n.default.shape({x: n.default.number, y: n.default.number}),
        className: s.dontSetMe,
        style: s.dontSetMe,
        transform: s.dontSetMe
    }), p(g, `defaultProps`, {
        ...c.default.defaultProps,
        axis: `both`,
        bounds: !1,
        defaultClassName: `react-draggable`,
        defaultClassNameDragging: `react-draggable-dragging`,
        defaultClassNameDragged: `react-draggable-dragged`,
        defaultPosition: {x: 0, y: 0},
        scale: 1
    })
})), xa = w(((e, t) => {
    var {default: n, DraggableCore: r} = ba();
    t.exports = n, t.exports.default = n, t.exports.DraggableCore = r
}))(), Sa = function (e, t) {
    return Sa = Object.setPrototypeOf || {__proto__: []} instanceof Array && function (e, t) {
        e.__proto__ = t
    } || function (e, t) {
        for (var n in t) Object.prototype.hasOwnProperty.call(t, n) && (e[n] = t[n])
    }, Sa(e, t)
};

function Ca(e, t) {
    if (typeof t != `function` && t !== null) throw TypeError(`Class extends value ` + String(t) + ` is not a constructor or null`);
    Sa(e, t);

    function n() {
        this.constructor = e
    }

    e.prototype = t === null ? Object.create(t) : (n.prototype = t.prototype, new n)
}

var Z = function () {
    return Z = Object.assign || function (e) {
        for (var t, n = 1, r = arguments.length; n < r; n++) for (var i in t = arguments[n], t) Object.prototype.hasOwnProperty.call(t, i) && (e[i] = t[i]);
        return e
    }, Z.apply(this, arguments)
};

function wa(e, t) {
    var n = {};
    for (var r in e) Object.prototype.hasOwnProperty.call(e, r) && t.indexOf(r) < 0 && (n[r] = e[r]);
    if (e != null && typeof Object.getOwnPropertySymbols == `function`) for (var i = 0, r = Object.getOwnPropertySymbols(e); i < r.length; i++) t.indexOf(r[i]) < 0 && Object.prototype.propertyIsEnumerable.call(e, r[i]) && (n[r[i]] = e[r[i]]);
    return n
}

function Ta(e, t, n) {
    if (n || arguments.length === 2) for (var r = 0, i = t.length, a; r < i; r++) (a || !(r in t)) && (a ||= Array.prototype.slice.call(t, 0, r), a[r] = t[r]);
    return e.concat(a || Array.prototype.slice.call(t))
}

function Ea(e) {
    "@babel/helpers - typeof";
    return Ea = typeof Symbol == `function` && typeof Symbol.iterator == `symbol` ? function (e) {
        return typeof e
    } : function (e) {
        return e && typeof Symbol == `function` && e.constructor === Symbol && e !== Symbol.prototype ? `symbol` : typeof e
    }, Ea(e)
}

var Da = (typeof document > `u` ? `undefined` : Ea(document)) === `object` ? document : null,
    Oa = typeof window < `u` && typeof navigator < `u` && typeof document < `u`, ka = function (e) {
        return e === void 0
    }, Aa = function (e) {
        return typeof e == `function`
    }, ja = function (e) {
        return typeof e == `number`
    }, Ma = function (e, t) {
        if (Aa(e.renderer)) {
            e.elementRef = t;
            var n = e.renderer;
            return delete e.renderer, n(e)
        }
        return delete e.elementRef, Y.createElement(`div`, Z({}, e, {ref: t}))
    }, Na = function (e, t, n, r) {
        var i = getComputedStyle(e);
        return i.boxSizing === `border-box` ? Math.max(0, (Number.parseFloat(i[t]) || 0) - (Number.parseFloat(i[n]) || 0) - (Number.parseFloat(i[r]) || 0)) : Number.parseFloat(i[t]) || 0
    }, Pa = function (e) {
        return Na(e, `height`, `paddingTop`, `paddingBottom`)
    }, Fa = function (e) {
        return Na(e, `width`, `paddingLeft`, `paddingRight`)
    }, Ia = function () {
        for (var e = ``, t = 0; t < 32; t++) switch (t) {
            case 8:
            case 20:
                e += `-${Math.trunc(Math.random() * 16).toString(16)}`;
                break;
            case 12:
                e += `-4`;
                break;
            case 16:
                e += `-${(Math.random() * 16 | 8).toString(16)}`;
                break;
            default:
                e += Math.trunc(Math.random() * 16).toString(16)
        }
        return e
    }, La = function (e, t, n, r, i) {
        if (t >= e) return 0;
        var a = t / e * n;
        return ja(i) && (a = Math.min(i, a)), ja(r) && (a = Math.max(r, a)), a
    }, Ra = function (e, t, n, r, i) {
        return !i || !r || t >= e ? 0 : (n - r) * i / (e - t)
    }, za = function (e, t, n, r, i) {
        return !i || !r || t >= e ? 0 : i * (e - t) / (n - r)
    }, Ba = function e(t) {
        if (t === void 0 && (t = !1), !Da) return e._cache = 0, e._cache;
        if (!t && !ka(e._cache)) return e._cache;
        var n = Da.createElement(`div`);
        if (n.setAttribute(`style`, `position:absolute;width:100px;height:100px;top:-999px;left:-999px;overflow:scroll;`), Da.body.append(n), n.clientWidth === 0) {
            n.remove();
            return
        }
        return e._cache = 100 - n.clientWidth, n.remove(), e._cache
    }, Va = function e(t) {
        if (t === void 0 && (t = !1), !t && !ka(e._cache)) return e._cache;
        if (!Da) return e._cache = !1, e._cache;
        var n = Da.createElement(`div`), r = Da.createElement(`div`);
        return n.append(r), n.setAttribute(`style`, `position:absolute;width:100px;height:100px;top:-999px;left:-999px;overflow:scroll;direction:rtl`), r.setAttribute(`style`, `width:1000px;height:1000px`), Da.body.append(n), n.scrollLeft = -50, e._cache = n.scrollLeft === -50, n.remove(), e._cache
    }, Ha = function () {
        function e(e) {
            e === void 0 && (e = 10), this.setMaxHandlers(e), this._handlers = Object.create(null)
        }

        return e._callEventHandlers = function (e, t, n) {
            if (t.length) {
                if (t.length === 1) {
                    Reflect.apply(t[0], e, n);
                    return
                }
                t = Ta([], t, !0);
                var r;
                for (r = 0; r < t.length; r++) Reflect.apply(t[r], e, n)
            }
        }, e.prototype.setMaxHandlers = function (e) {
            if (!ja(e) || e <= 0) throw TypeError(`Expected maxHandlers to be a positive number, got '${e}' of type ${Ea(e)}`);
            return this._maxHandlers = e, this
        }, e.prototype.getMaxHandlers = function () {
            return this._maxHandlers
        }, e.prototype.emit = function (t) {
            var n = [...arguments].slice(1);
            return Ea(this._handlers[t]) !== `object` || !Array.isArray(this._handlers[t]) ? !1 : (e._callEventHandlers(this, this._handlers[t], n), !0)
        }, e.prototype.on = function (t, n) {
            return e._addHandler(this, t, n), this
        }, e.prototype.prependOn = function (t, n) {
            return e._addHandler(this, t, n, !0), this
        }, e.prototype.once = function (t, n) {
            if (!Aa(n)) throw TypeError(`Expected event handler to be a function, got ${Ea(n)}`);
            return e._addHandler(this, t, this._wrapOnceHandler(t, n)), this
        }, e.prototype.prependOnce = function (t, n) {
            if (!Aa(n)) throw TypeError(`Expected event handler to be a function, got ${Ea(n)}`);
            return e._addHandler(this, t, this._wrapOnceHandler(t, n), !0), this
        }, e.prototype.off = function (t, n) {
            return e._removeHandler(this, t, n), this
        }, e.prototype.removeAllHandlers = function () {
            var t = this._handlers;
            this._handlers = Object.create(null);
            var n = t.removeHandler;
            delete t.removeHandler;
            var r, i;
            for (i in t) for (r = t[i].length - 1; r >= 0; r--) e._callEventHandlers(this, n, [i, t[i][r].handler || t[i][r]]);
            return this
        }, e.prototype._wrapOnceHandler = function (t, n) {
            var r = {fired: !1, handler: n, wrappedHandler: void 0, emitter: this, event: t}, i = e._onceWrapper.bind(r);
            return r.wrappedHandler = i, i.handler = n, i.event = t, i
        }, e._addHandler = function (e, t, n, r) {
            if (r === void 0 && (r = !1), !Aa(n)) throw TypeError(`Expected event handler to be a function, got ${Ea(n)}`);
            return e._handlers[t] = e._handlers[t] || [], e.emit(`addHandler`, t, n), r ? e._handlers[t].unshift(n) : e._handlers[t].push(n), e
        }, e._onceWrapper = function () {
            var e = [...arguments];
            this.fired || (this.fired = !0, this.emitter.off(this.event, this.wrappedHandler), Reflect.apply(this.handler, this.emitter, e))
        }, e._removeHandler = function (e, t, n) {
            if (!Aa(n)) throw TypeError(`Expected event handler to be a function, got ${Ea(n)}`);
            if (ka(e._handlers[t]) || !e._handlers[t].length) return e;
            var r = -1;
            if (e._handlers[t].length === 1) (e._handlers[t][0] === n || e._handlers[t][0].handler === n) && (r = 0, n = e._handlers[t][0].handler || e._handlers[t][0]); else for (r = e._handlers[t].length - 1; r >= 0; r--) if (e._handlers[t][r] === n || e._handlers[t][r].handler === n) {
                n = e._handlers[t][r].handler || e._handlers[t][r];
                break
            }
            return r === -1 ? e : (r === 0 ? e._handlers[t].shift() : e._handlers[t].splice(r, 1), e.emit(`removeHandler`, t, n), e)
        }, e
    }(), Ua = new (function () {
        function e() {
            var e = this;
            this.targets = [], this.animationFrameID = 0, this._isActive = !1, this.start = function () {
                return !e._isActive && e.targets.length && (e._isActive = !0, e.animationFrameID && cancelAnimationFrame(e.animationFrameID), e.animationFrameID = requestAnimationFrame(e.rafCallback)), e
            }, this.stop = function () {
                return e._isActive && (e._isActive = !1, e.animationFrameID && cancelAnimationFrame(e.animationFrameID), e.animationFrameID = 0), e
            }, this.addTarget = function (t, n) {
                return n === void 0 && (n = !1), e.targets.includes(t) || (e.targets.push(t), e.targets.length === 1 && !n && e.start()), e
            }, this.removeTarget = function (t) {
                var n = e.targets.indexOf(t);
                return n !== -1 && (e.targets.splice(n, 1), e.targets.length === 0 && e.stop()), e
            }, this.rafCallback = function () {
                if (!e._isActive) return 0;
                for (var t = 0; t < e.targets.length; t++) e.targets[t]._unmounted || e.targets[t].update();
                return e.animationFrameID = requestAnimationFrame(e.rafCallback), e.animationFrameID
            }
        }

        return Object.defineProperty(e.prototype, `isActive`, {
            get: function () {
                return this._isActive
            }, enumerable: !1, configurable: !0
        }), e
    }()), Wa;
(function (e) {
    e.X = `x`, e.Y = `y`
})(Wa ||= {});
var Ga;
(function (e) {
    e.JUMP = `jump`, e.STEP = `step`
})(Ga ||= {});
var Ka = function (e) {
    Ca(t, e);

    function t() {
        var n = e !== null && e.apply(this, arguments) || this;
        return n.element = null, n.initialOffsetX = 0, n.initialOffsetY = 0, n.elementRefHack = Y.createRef(), n.lastDragData = {
            x: 0,
            y: 0,
            deltaX: 0,
            deltaY: 0,
            lastX: 0,
            lastY: 0
        }, n.handleOnDragStart = function (e, r) {
            if (!n.element) {
                n.handleOnDragStop(e, r);
                return
            }
            Oa && (n.prevUserSelect = document.body.style.userSelect, document.body.style.userSelect = `none`, n.prevOnSelectStart = document.onselectstart, document.addEventListener(`selectstart`, t.selectStartReplacer)), n.props.onDragStart && n.props.onDragStart(n.lastDragData = {
                x: r.x - n.initialOffsetX,
                y: r.y - n.initialOffsetY,
                lastX: r.lastX - n.initialOffsetX,
                lastY: r.lastY - n.initialOffsetY,
                deltaX: r.deltaX,
                deltaY: r.deltaY
            }), n.element.classList.add(`dragging`)
        }, n.handleOnDrag = function (e, t) {
            if (!n.element) {
                n.handleOnDragStop(e, t);
                return
            }
            n.props.onDrag && n.props.onDrag(n.lastDragData = {
                x: t.x - n.initialOffsetX,
                y: t.y - n.initialOffsetY,
                lastX: t.lastX - n.initialOffsetX,
                lastY: t.lastY - n.initialOffsetY,
                deltaX: t.deltaX,
                deltaY: t.deltaY
            })
        }, n.handleOnDragStop = function (e, t) {
            var r = t ? {
                x: t.x - n.initialOffsetX,
                y: t.y - n.initialOffsetY,
                lastX: t.lastX - n.initialOffsetX,
                lastY: t.lastY - n.initialOffsetY,
                deltaX: t.deltaX,
                deltaY: t.deltaY
            } : n.lastDragData;
            n.props.onDragEnd && n.props.onDragEnd(r), n.element && n.element.classList.remove(`dragging`), Oa && (document.body.style.userSelect = n.prevUserSelect, n.prevOnSelectStart && document.addEventListener(`selectstart`, n.prevOnSelectStart), n.prevOnSelectStart = null), n.initialOffsetX = 0, n.initialOffsetY = 0, n.lastDragData = {
                x: 0,
                y: 0,
                deltaX: 0,
                deltaY: 0,
                lastX: 0,
                lastY: 0
            }
        }, n.handleOnMouseDown = function (e) {
            if (n.element) if (e.preventDefault(), e.stopPropagation(), !ka(e.offsetX)) n.initialOffsetX = e.offsetX, n.initialOffsetY = e.offsetY; else {
                var t = n.element.getBoundingClientRect();
                n.initialOffsetX = (e.clientX || e.touches[0].clientX) - t.left, n.initialOffsetY = (e.clientY || e.touches[0].clientY) - t.top
            }
        }, n.elementRef = function (e) {
            Aa(n.props.elementRef) && n.props.elementRef(e), n.element = e, n.elementRefHack.current = e
        }, n
    }

    return t.prototype.componentDidMount = function () {
        this.element || this.setState(function () {
            throw Error("<ScrollbarThumb> Element was not created. Possibly you haven't provided HTMLDivElement to renderer's `elementRef` function.")
        })
    }, t.prototype.componentWillUnmount = function () {
        this.handleOnDragStop(), this.elementRef(null)
    }, t.prototype.render = function () {
        var e = this.props;
        e.elementRef;
        var t = e.axis;
        e.onDrag, e.onDragEnd, e.onDragStart;
        var n = wa(e, [`elementRef`, `axis`, `onDrag`, `onDragEnd`, `onDragStart`]);
        return n.className = sa(`ScrollbarsCustom-Thumb`, t === Wa.X ? `ScrollbarsCustom-ThumbX` : `ScrollbarsCustom-ThumbY`, n.className), n.renderer && (n.axis = t), Y.createElement(xa.DraggableCore, {
            allowAnyClick: !1,
            enableUserSelectHack: !1,
            onMouseDown: this.handleOnMouseDown,
            onDrag: this.handleOnDrag,
            onStart: this.handleOnDragStart,
            onStop: this.handleOnDragStop,
            nodeRef: this.elementRefHack
        }, Ma(n, this.elementRef))
    }, t.selectStartReplacer = function () {
        return !1
    }, t
}(Y.Component), qa = function (e) {
    Ca(t, e);

    function t() {
        var t = e !== null && e.apply(this, arguments) || this;
        return t.element = null, t.elementRef = function (e) {
            Aa(t.props.elementRef) && t.props.elementRef(e), t.element = e
        }, t.handleClick = function (e) {
            if (!(!e || !t.element || e.button !== 0)) {
                if (Aa(t.props.onClick) && e.target === t.element) if (!ka(e.offsetX)) t.props.onClick(e, {
                    axis: t.props.axis,
                    offset: t.props.axis === Wa.X ? e.offsetX : e.offsetY
                }); else {
                    var n = t.element.getBoundingClientRect();
                    t.props.onClick(e, {
                        axis: t.props.axis,
                        offset: t.props.axis === Wa.X ? (e.clientX || e.touches[0].clientX) - n.left : (e.clientY || e.touches[0].clientY) - n.top
                    })
                }
                return !0
            }
        }, t
    }

    return t.prototype.componentDidMount = function () {
        if (!this.element) {
            this.setState(function () {
                throw Error("Element was not created. Possibly you haven't provided HTMLDivElement to renderer's `elementRef` function.")
            });
            return
        }
        this.element.addEventListener(`click`, this.handleClick)
    }, t.prototype.componentWillUnmount = function () {
        this.element && (this.element.removeEventListener(`click`, this.handleClick), this.element = null, this.elementRef(null))
    }, t.prototype.render = function () {
        var e = this.props;
        e.elementRef;
        var t = e.axis;
        e.onClick;
        var n = wa(e, [`elementRef`, `axis`, `onClick`]);
        return n.className = sa(`ScrollbarsCustom-Track`, t === Wa.X ? `ScrollbarsCustom-TrackX` : `ScrollbarsCustom-TrackY`, n.className), n.renderer && (n.axis = t), Ma(n, this.elementRef)
    }, t
}(Y.Component), Ja = {
    holder: {position: `relative`, width: `100%`, height: `100%`},
    wrapper: {position: `absolute`, top: 0, left: 0, bottom: 0, right: 0},
    content: {boxSizing: `border-box`},
    track: {
        common: {
            position: `absolute`,
            overflow: `hidden`,
            borderRadius: 4,
            background: `rgba(0,0,0,.1)`,
            userSelect: `none`
        },
        x: {height: 10, width: `calc(100% - 20px)`, bottom: 0, left: 10},
        y: {width: 10, height: `calc(100% - 20px)`, top: 10}
    },
    thumb: {
        common: {cursor: `pointer`, borderRadius: 4, background: `rgba(0,0,0,.4)`},
        x: {height: `100%`, width: 0},
        y: {width: `100%`, height: 0}
    }
}, Ya = Oa ? la() : 1;
Oa && window.addEventListener(`resize`, function () {
    Ya = la()
}, {passive: !0});
var Xa = Y.createContext({parentScrollbar: null}), Za = function (e) {
        Ca(t, e);

        function t(t) {
            var n = e.call(this, t) || this;
            return n.getScrollState = function (e) {
                if (e === void 0 && (e = !1), n.scrollValues && !e) return Z({}, n.scrollValues);
                var t = {
                    clientHeight: 0,
                    clientWidth: 0,
                    contentScrollHeight: 0,
                    contentScrollWidth: 0,
                    scrollHeight: 0,
                    scrollWidth: 0,
                    scrollTop: 0,
                    scrollLeft: 0,
                    scrollYBlocked: !1,
                    scrollXBlocked: !1,
                    scrollYPossible: !1,
                    scrollXPossible: !1,
                    trackYVisible: !1,
                    trackXVisible: !1,
                    zoomLevel: Ya * 1,
                    isRTL: void 0
                }, r = n.props;
                return t.isRTL = n.state.isRTL, t.scrollYBlocked = r.noScroll || r.noScrollY, t.scrollXBlocked = r.noScroll || r.noScrollX, n.scrollerElement && (t.clientHeight = n.scrollerElement.clientHeight, t.clientWidth = n.scrollerElement.clientWidth, t.scrollHeight = n.scrollerElement.scrollHeight, t.scrollWidth = n.scrollerElement.scrollWidth, t.scrollTop = n.scrollerElement.scrollTop, t.scrollLeft = n.scrollerElement.scrollLeft, t.scrollYPossible = !t.scrollYBlocked && t.scrollHeight > t.clientHeight, t.scrollXPossible = !t.scrollXBlocked && t.scrollWidth > t.clientWidth, t.trackYVisible = t.scrollYPossible || r.permanentTracks || r.permanentTrackY, t.trackXVisible = t.scrollXPossible || r.permanentTracks || r.permanentTrackX), n.contentElement && (t.contentScrollHeight = n.contentElement.scrollHeight, t.contentScrollWidth = n.contentElement.scrollWidth), t
            }, n.scrollToTop = function () {
                return n.scrollerElement && (n.scrollerElement.scrollTop = 0), n
            }, n.scrollToLeft = function () {
                return n.scrollerElement && (n.scrollerElement.scrollLeft = 0), n
            }, n.scrollToBottom = function () {
                return n.scrollerElement && (n.scrollerElement.scrollTop = n.scrollerElement.scrollHeight - n.scrollerElement.clientHeight), n
            }, n.scrollToRight = function () {
                return n.scrollerElement && (n.scrollerElement.scrollLeft = n.scrollerElement.scrollWidth - n.scrollerElement.clientWidth), n
            }, n.scrollTo = function (e, t) {
                return n.scrollerElement && (ja(e) && (n.scrollerElement.scrollLeft = e), ja(t) && (n.scrollerElement.scrollTop = t)), n
            }, n.centerAt = function (e, t) {
                return n.scrollerElement && (ja(e) && (n.scrollerElement.scrollLeft = e - n.scrollerElement.clientWidth / 2), ja(t) && (n.scrollerElement.scrollTop = t - n.scrollerElement.clientHeight / 2)), n
            }, n.update = function (e) {
                if (e === void 0 && (e = !1), n.scrollerElement) {
                    if (ka(n.state.isRTL)) return n.setState({isRTL: getComputedStyle(n.scrollerElement).direction === `rtl`}), n.getScrollState();
                    var t = n.getScrollState(!0), r = Z({}, n.scrollValues), i = n.props, a = 0;
                    if (e) a = 32767; else if (r.clientHeight !== t.clientHeight && (a |= 1), r.clientWidth !== t.clientWidth && (a |= 2), r.scrollHeight !== t.scrollHeight && (a |= 4), r.scrollWidth !== t.scrollWidth && (a |= 8), r.scrollTop !== t.scrollTop && (a |= 16), r.scrollLeft !== t.scrollLeft && (a |= 32), r.scrollYBlocked !== t.scrollYBlocked && (a |= 64), r.scrollXBlocked !== t.scrollXBlocked && (a |= 128), r.scrollYPossible !== t.scrollYPossible && (a |= 256), r.scrollXPossible !== t.scrollXPossible && (a |= 512), r.trackYVisible !== t.trackYVisible && (a |= 1024), r.trackXVisible !== t.trackXVisible && (a |= 2048), r.isRTL !== t.isRTL && (a |= 4096), r.contentScrollHeight !== t.contentScrollHeight && (a |= 8192), r.contentScrollWidth !== t.contentScrollWidth && (a |= 16384), r.zoomLevel !== t.zoomLevel && (a |= 32768), a === 0) return r;
                    if (!(!i.native && n.holderElement && (a & 8192 && (i.translateContentSizesToHolder || i.translateContentSizeYToHolder) && (n.holderElement.style.height = `${t.contentScrollHeight}px`), a & 16384 && (i.translateContentSizesToHolder || i.translateContentSizeXToHolder) && (n.holderElement.style.width = `${t.contentScrollWidth}px`), (i.translateContentSizesToHolder || i.translateContentSizeYToHolder || i.translateContentSizeXToHolder) && (!t.clientHeight && t.contentScrollHeight || !t.clientWidth && t.contentScrollWidth)))) {
                        if (a & 1024 || a & 2048) {
                            r.scrollYBlocked = t.scrollYBlocked, r.scrollXBlocked = t.scrollXBlocked, r.scrollYPossible = t.scrollYPossible, r.scrollXPossible = t.scrollXPossible, n.trackYElement && a & 1024 && (n.trackYElement.style.display = t.trackYVisible ? `` : `none`), n.trackXElement && a & 2048 && (n.trackXElement.style.display = t.trackXVisible ? `` : `none`), n.scrollValues = r, n.setState({
                                trackYVisible: n.scrollValues.trackYVisible = t.trackYVisible,
                                trackXVisible: n.scrollValues.trackXVisible = t.trackXVisible
                            });
                            return
                        }
                        return (i.native ? n.updaterNative : n.updaterCustom)(a, t), n.scrollValues = t, !i.native && a & 32768 && (Ba(!0), n.forceUpdate()), n.eventEmitter.emit(`update`, Z({}, t), r), (a & 16 || a & 32) && n.eventEmitter.emit(`scroll`, Z({}, t), r), n.scrollValues
                    }
                }
            }, n.updaterNative = function () {
                return !0
            }, n.updaterCustom = function (e, t) {
                var r = n.props;
                if (n.trackYElement && n.thumbYElement && (e & 1 || e & 4 || e & 16 || e & 64 || e & 256)) if (t.scrollYPossible) {
                    var i = Pa(n.trackYElement),
                        a = La(t.scrollHeight, t.clientHeight, i, r.minimalThumbYSize || r.minimalThumbSize, r.maximalThumbYSize || r.maximalThumbSize),
                        o = Ra(t.scrollHeight, t.clientHeight, i, a, t.scrollTop);
                    n.thumbYElement.style.transform = `translateY(${o}px)`, n.thumbYElement.style.height = `${a}px`, n.thumbYElement.style.display = ``
                } else n.thumbYElement.style.transform = ``, n.thumbYElement.style.height = `0px`, n.thumbYElement.style.display = `none`;
                if (n.trackXElement && n.thumbXElement && (e & 2 || e & 8 || e & 32 || e & 128 || e & 512 || e & 4096)) if (t.scrollXPossible) {
                    var i = Fa(n.trackXElement),
                        a = La(t.scrollWidth, t.clientWidth, i, r.minimalThumbXSize || r.minimalThumbSize, r.maximalThumbXSize || r.maximalThumbSize),
                        o = Ra(t.scrollWidth, t.clientWidth, i, a, t.scrollLeft);
                    n.state.isRTL && Va() && (o += i - a), n.thumbXElement.style.transform = `translateX(${o}px)`, n.thumbXElement.style.width = `${a}px`, n.thumbXElement.style.display = ``
                } else n.thumbXElement.style.transform = ``, n.thumbXElement.style.width = `0px`, n.thumbXElement.style.display = `none`;
                return !0
            }, n.elementRefHolder = function (e) {
                n.holderElement = e, Aa(n.props.elementRef) && n.props.elementRef(e)
            }, n.elementRefWrapper = function (e) {
                n.wrapperElement = e, Aa(n.props.wrapperProps.elementRef) && n.props.wrapperProps.elementRef(e)
            }, n.elementRefScroller = function (e) {
                n.scrollerElement = e, Aa(n.props.scrollerProps.elementRef) && n.props.scrollerProps.elementRef(e)
            }, n.elementRefContent = function (e) {
                n.contentElement = e, Aa(n.props.contentProps.elementRef) && n.props.contentProps.elementRef(e)
            }, n.elementRefTrackX = function (e) {
                n.trackXElement = e, Aa(n.props.trackXProps.elementRef) && n.props.trackXProps.elementRef(e)
            }, n.elementRefTrackY = function (e) {
                n.trackYElement = e, Aa(n.props.trackYProps.elementRef) && n.props.trackYProps.elementRef(e)
            }, n.elementRefThumbX = function (e) {
                n.thumbXElement = e, Aa(n.props.thumbXProps.elementRef) && n.props.thumbXProps.elementRef(e)
            }, n.elementRefThumbY = function (e) {
                n.thumbYElement = e, Aa(n.props.thumbYProps.elementRef) && n.props.thumbYProps.elementRef(e)
            }, n.handleTrackXClick = function (e, t) {
                if (n.props.trackXProps.onClick && n.props.trackXProps.onClick(e, t), !(!n.scrollerElement || !n.trackXElement || !n.thumbXElement || !n.scrollValues || !n.scrollValues.scrollXPossible)) {
                    n._scrollDetection();
                    var r = n.thumbXElement.clientWidth, i = Fa(n.trackXElement),
                        a = (n.scrollValues.isRTL && Va() ? t.offset + r / 2 - i : t.offset - r / 2) - (Number.parseFloat(getComputedStyle(n.trackXElement).paddingLeft) || 0),
                        o = za(n.scrollValues.scrollWidth, n.scrollValues.clientWidth, i, r, a);
                    n.props.trackClickBehavior === Ga.STEP && (o = (n.scrollValues.isRTL ? n.scrollValues.scrollLeft > o : n.scrollValues.scrollLeft < o) ? n.scrollValues.scrollLeft + n.scrollValues.clientWidth : n.scrollValues.scrollLeft - n.scrollValues.clientWidth), n.scrollerElement.scrollLeft = o
                }
            }, n.handleTrackYClick = function (e, t) {
                if (n.props.trackYProps.onClick && n.props.trackYProps.onClick(e, t), !(!n.scrollerElement || !n.trackYElement || !n.thumbYElement || !n.scrollValues || !n.scrollValues.scrollYPossible)) {
                    n._scrollDetection();
                    var r = n.thumbYElement.clientHeight,
                        i = za(n.scrollValues.scrollHeight, n.scrollValues.clientHeight, Pa(n.trackYElement), r, t.offset - r / 2) - (Number.parseFloat(getComputedStyle(n.trackYElement).paddingTop) || 0);
                    n.props.trackClickBehavior === Ga.JUMP ? n.scrollerElement.scrollTop = i : n.scrollerElement.scrollTop = n.scrollValues.scrollTop < i ? n.scrollValues.scrollTop + n.scrollValues.clientHeight : n.scrollValues.scrollTop - n.scrollValues.clientHeight
                }
            }, n.handleTrackYMouseWheel = function (e) {
                var t = n.props;
                t.trackYProps && t.trackYProps.onWheel && t.trackYProps.onWheel(e), !(t.disableTracksMousewheelScrolling || t.disableTrackYMousewheelScrolling) && (n._scrollDetection(), !(!n.scrollerElement || n.scrollValues.scrollYBlocked) && (n.scrollTop += e.deltaY))
            }, n.handleTrackXMouseWheel = function (e) {
                var t = n.props;
                t.trackXProps && t.trackXProps.onWheel && t.trackXProps.onWheel(e), !(t.disableTracksMousewheelScrolling || t.disableTrackXMousewheelScrolling) && (n._scrollDetection(), !(!n.scrollerElement || n.scrollValues.scrollXBlocked) && (n.scrollLeft += e.deltaX))
            }, n.handleThumbXDrag = function (e) {
                if (!(!n.trackXElement || !n.thumbXElement || !n.scrollerElement || !n.scrollValues || !n.scrollValues.scrollXPossible)) {
                    n._scrollDetection();
                    var t = n.trackXElement.getBoundingClientRect(), r = getComputedStyle(n.trackXElement),
                        i = Number.parseFloat(r.paddingLeft) || 0, a = Number.parseFloat(r.paddingRight) || 0,
                        o = t.width - i - a, s = n.thumbXElement.clientWidth,
                        c = n.scrollValues.isRTL && Va() ? e.x + s - o + i : e.lastX - i;
                    n.scrollerElement.scrollLeft = za(n.scrollValues.scrollWidth, n.scrollValues.clientWidth, o, s, c), n.props.thumbXProps?.onDrag && n.props.thumbXProps.onDrag(e)
                }
            }, n.handleThumbXDragEnd = function (e) {
                n.handleThumbXDrag(e), n.props.thumbXProps?.onDragEnd && n.props.thumbXProps.onDragEnd(e)
            }, n.handleThumbYDrag = function (e) {
                if (!(!n.scrollerElement || !n.trackYElement || !n.thumbYElement || !n.scrollValues || !n.scrollValues.scrollYPossible)) {
                    n._scrollDetection();
                    var t = n.trackYElement.getBoundingClientRect(), r = getComputedStyle(n.trackYElement),
                        i = Number.parseFloat(r.paddingTop) || 0, a = Number.parseFloat(r.paddingBottom) || 0,
                        o = t.height - i - a, s = n.thumbYElement.clientHeight, c = e.y - i;
                    n.scrollerElement.scrollTop = za(n.scrollValues.scrollHeight, n.scrollValues.clientHeight, o, s, c), n.props.thumbYProps?.onDrag && n.props.thumbYProps.onDrag(e)
                }
            }, n.handleThumbYDragEnd = function (e) {
                n.handleThumbYDrag(e), n.props.thumbYProps?.onDragEnd && n.props.thumbYProps.onDragEnd(e)
            }, n.handleScrollerScroll = function () {
                n._scrollDetection()
            }, n._scrollDetection = function () {
                n._scrollDetectionTO ? Oa && window.clearTimeout(n._scrollDetectionTO) : n.eventEmitter.emit(`scrollStart`, n.getScrollState()), n._scrollDetectionTO = Oa ? window.setTimeout(n._scrollDetectionCallback, n.props.scrollDetectionThreshold || 0) : null
            }, n._scrollDetectionCallback = function () {
                n._scrollDetectionTO = null, n.eventEmitter.emit(`scrollStop`, n.getScrollState())
            }, n.state = {
                trackXVisible: !1,
                trackYVisible: !1,
                isRTL: t.rtl
            }, n.scrollValues = n.getScrollState(!0), n.eventEmitter = new Ha(15), t.onUpdate && n.eventEmitter.on(`update`, t.onUpdate), t.onScroll && n.eventEmitter.on(`scroll`, t.onScroll), t.onScrollStart && n.eventEmitter.on(`scrollStart`, t.onScrollStart), t.onScrollStop && n.eventEmitter.on(`scrollStop`, t.onScrollStop), n.id = Ia(), n
        }

        return Object.defineProperty(t.prototype, `scrollTop`, {
            get: function () {
                return this.scrollerElement ? this.scrollerElement.scrollTop : 0
            }, set: function (e) {
                this.scrollerElement && (this.scrollerElement.scrollTop = e, this.update())
            }, enumerable: !1, configurable: !0
        }), Object.defineProperty(t.prototype, `scrollLeft`, {
            get: function () {
                return this.scrollerElement ? this.scrollerElement.scrollLeft : 0
            }, set: function (e) {
                this.scrollerElement && (this.scrollerElement.scrollLeft = e)
            }, enumerable: !1, configurable: !0
        }), Object.defineProperty(t.prototype, `scrollHeight`, {
            get: function () {
                return this.scrollerElement ? this.scrollerElement.scrollHeight : 0
            }, enumerable: !1, configurable: !0
        }), Object.defineProperty(t.prototype, `scrollWidth`, {
            get: function () {
                return this.scrollerElement ? this.scrollerElement.scrollWidth : 0
            }, enumerable: !1, configurable: !0
        }), Object.defineProperty(t.prototype, `clientHeight`, {
            get: function () {
                return this.scrollerElement ? this.scrollerElement.clientHeight : 0
            }, enumerable: !1, configurable: !0
        }), Object.defineProperty(t.prototype, `clientWidth`, {
            get: function () {
                return this.scrollerElement ? this.scrollerElement.clientWidth : 0
            }, enumerable: !1, configurable: !0
        }), t.calculateStyles = function (e, t, n, r) {
            var i, a, o, s, c = !e.noDefaultStyles;
            return {
                holder: Z(Z(Z({}, c && Ja.holder), {position: `relative`}), e.style),
                wrapper: Z(Z(Z({}, c && Z(Z(Z({}, Ja.wrapper), !e.disableTracksWidthCompensation && !e.disableTrackYWidthCompensation && (i = {}, i[t.isRTL ? `left` : `right`] = t.trackYVisible ? 10 : 0, i)), !e.disableTracksWidthCompensation && !e.disableTrackXWidthCompensation && {bottom: t.trackXVisible ? 10 : 0})), e.wrapperProps.style), {
                    position: `absolute`,
                    overflow: `hidden`
                }),
                content: Z(Z(Z(Z(Z({}, c && Ja.content), e.translateContentSizesToHolder || e.translateContentSizeYToHolder || e.translateContentSizeXToHolder ? {display: `table-cell`} : {padding: .05}), c && !(e.translateContentSizesToHolder || e.translateContentSizeYToHolder) && {minHeight: `100%`}), c && !(e.translateContentSizesToHolder || e.translateContentSizeXToHolder) && {minWidth: `100%`}), e.contentProps.style),
                scroller: Z(Z(Z(Z((a = {
                    position: `absolute`,
                    top: 0,
                    left: 0,
                    bottom: 0,
                    right: 0,
                    paddingBottom: !r && n.scrollXPossible ? e.fallbackScrollbarWidth : void 0
                }, a[t.isRTL ? `paddingLeft` : `paddingRight`] = !r && n.scrollYPossible ? e.fallbackScrollbarWidth : void 0, a), e.scrollerProps.style), !ka(e.rtl) && {direction: e.rtl ? `rtl` : `ltr`}), e.momentum && {WebkitOverflowScrolling: `touch`}), (o = {
                    overflowY: n.scrollYPossible ? `scroll` : `hidden`,
                    overflowX: n.scrollXPossible ? `scroll` : `hidden`,
                    marginBottom: n.scrollXPossible ? -(r || e.fallbackScrollbarWidth) - Number(n.zoomLevel !== 1) : void 0
                }, o[t.isRTL ? `marginLeft` : `marginRight`] = n.scrollYPossible ? -(r || e.fallbackScrollbarWidth) - Number(n.zoomLevel !== 1) : void 0, o)),
                trackX: Z(Z(Z(Z({}, c && Ja.track.common), c && Ja.track.x), e.trackXProps.style), !t.trackXVisible && {display: `none`}),
                trackY: Z(Z(Z(Z(Z({}, c && Ja.track.common), c && Ja.track.y), c && (s = {}, s[t.isRTL ? `left` : `right`] = 0, s)), e.trackYProps.style), !t.trackYVisible && {display: `none`}),
                thumbX: Z(Z(Z({}, c && Ja.thumb.common), c && Ja.thumb.x), e.thumbXProps.style),
                thumbY: Z(Z(Z({}, c && Ja.thumb.common), c && Ja.thumb.y), e.thumbYProps.style)
            }
        }, t.prototype.componentDidMount = function () {
            if (!this.scrollerElement) {
                this.setState(function () {
                    throw Error("scroller element was not created. Possibly you haven't provided HTMLDivElement to renderer's `elementRef` function.")
                });
                return
            }
            if (!this.contentElement) {
                this.setState(function () {
                    throw Error("content element was not created. Possibly you haven't provided HTMLDivElement to renderer's `elementRef` function.")
                });
                return
            }
            var e = this.props;
            if (!e.native && !e.mobileNative) {
                if (!this.holderElement) {
                    this.setState(function () {
                        throw Error("holder element was not created. Possibly you haven't provided HTMLDivElement to renderer's `elementRef` function.")
                    });
                    return
                }
                if (!this.wrapperElement) {
                    this.setState(function () {
                        throw Error("wrapper element was not created. Possibly you haven't provided HTMLDivElement to renderer's `elementRef` function.")
                    });
                    return
                }
            }
            Ua.addTarget(this), ka(e.scrollTop) || (this.scrollerElement.scrollTop = e.scrollTop), ka(e.scrollLeft) || (this.scrollerElement.scrollLeft = e.scrollLeft), this.update(!0)
        }, t.prototype.componentWillUnmount = function () {
            Ua.removeTarget(this)
        }, t.prototype.componentDidUpdate = function (e, t) {
            if (this.scrollerElement) {
                var n = this.props;
                n.rtl !== e.rtl && n.rtl !== this.state.isRTL && this.setState({isRTL: n.rtl}), this.state.isRTL !== t.isRTL && this.update(), !ka(n.scrollTop) && n.scrollTop !== this.scrollerElement.scrollTop && (this.scrollerElement.scrollTop = n.scrollTop), !ka(n.scrollLeft) && n.scrollLeft !== this.scrollerElement.scrollLeft && (this.scrollerElement.scrollLeft = n.scrollLeft), e.onUpdate !== n.onUpdate && (e.onUpdate && this.eventEmitter.off(`update`, e.onUpdate), n.onUpdate && this.eventEmitter.on(`update`, n.onUpdate)), e.onScroll !== n.onScroll && (e.onScroll && this.eventEmitter.off(`scroll`, e.onScroll), n.onScroll && this.eventEmitter.on(`scroll`, n.onScroll)), e.onScrollStart !== n.onScrollStart && (e.onScrollStart && this.eventEmitter.off(`scrollStart`, e.onScrollStart), n.onScrollStart && this.eventEmitter.on(`scrollStart`, n.onScrollStart)), e.onScrollStop !== n.onScrollStop && (e.onScrollStop && this.eventEmitter.off(`scrollStop`, e.onScrollStop), n.onScrollStop && this.eventEmitter.on(`scrollStop`, n.onScrollStop))
            }
        }, t.prototype.render = function () {
            var e = this.props, n = e.createContext, r = e.rtl, i = e.native, a = e.mobileNative, o = e.momentum;
            e.noDefaultStyles;
            var s = e.disableTracksMousewheelScrolling, c = e.disableTrackXMousewheelScrolling,
                l = e.disableTrackYMousewheelScrolling;
            e.disableTracksWidthCompensation, e.disableTrackXWidthCompensation, e.disableTrackYWidthCompensation;
            var u = e.noScrollX, d = e.noScrollY, f = e.noScroll, p = e.permanentTrackX, m = e.permanentTrackY,
                h = e.permanentTracks, g = e.removeTracksWhenNotUsed, _ = e.removeTrackYWhenNotUsed,
                v = e.removeTrackXWhenNotUsed;
            e.minimalThumbSize, e.maximalThumbSize, e.minimalThumbXSize, e.maximalThumbXSize, e.minimalThumbYSize, e.maximalThumbYSize, e.fallbackScrollbarWidth, e.scrollTop, e.scrollLeft, e.trackClickBehavior, e.scrollDetectionThreshold;
            var y = e.wrapperProps, b = e.scrollerProps, x = e.contentProps, S = e.trackXProps, C = e.trackYProps,
                w = e.thumbXProps, T = e.thumbYProps, E = e.scrollbarWidth;
            e.elementRef, e.onUpdate, e.onScroll, e.onScrollStart, e.onScrollStop, e.translateContentSizesToHolder, e.translateContentSizeYToHolder, e.translateContentSizeXToHolder;
            var D = e.children,
                O = wa(e, `createContext.rtl.native.mobileNative.momentum.noDefaultStyles.disableTracksMousewheelScrolling.disableTrackXMousewheelScrolling.disableTrackYMousewheelScrolling.disableTracksWidthCompensation.disableTrackXWidthCompensation.disableTrackYWidthCompensation.noScrollX.noScrollY.noScroll.permanentTrackX.permanentTrackY.permanentTracks.removeTracksWhenNotUsed.removeTrackYWhenNotUsed.removeTrackXWhenNotUsed.minimalThumbSize.maximalThumbSize.minimalThumbXSize.maximalThumbXSize.minimalThumbYSize.maximalThumbYSize.fallbackScrollbarWidth.scrollTop.scrollLeft.trackClickBehavior.scrollDetectionThreshold.wrapperProps.scrollerProps.contentProps.trackXProps.trackYProps.thumbXProps.thumbYProps.scrollbarWidth.elementRef.onUpdate.onScroll.onScrollStart.onScrollStop.translateContentSizesToHolder.translateContentSizeYToHolder.translateContentSizeXToHolder.children`.split(`.`)),
                k = ka(E) ? Ba() || 0 : E;
            if (i || !k && a) {
                this.elementRefHolder(null), this.elementRefWrapper(null), this.elementRefTrackX(null), this.elementRefTrackY(null), this.elementRefThumbX(null), this.elementRefThumbY(null);
                var A = Z(Z({}, x), {
                    key: `ScrollbarsCustom-Content`,
                    className: sa(`ScrollbarsCustom-Content`, x.className),
                    children: D
                });
                return Ma(Z(Z({}, O), {
                    className: sa(`ScrollbarsCustom native`, this.state.trackYVisible && `trackYVisible`, this.state.trackXVisible && `trackXVisible`, this.state.isRTL && `rtl`, O.className),
                    style: Z(Z(Z(Z({}, O.style), !ka(r) && {direction: r ? `rtl` : `ltr`}), o && {WebkitOverflowScrolling: `touch`}), {
                        overflowX: f || u ? `hidden` : h || p ? `scroll` : `auto`,
                        overflowY: f || d ? `hidden` : h || m ? `scroll` : `auto`
                    }),
                    onScroll: this.handleScrollerScroll,
                    children: Ma(A, this.elementRefContent),
                    renderer: b.renderer,
                    elementRef: b.elementRef
                }), this.elementRefScroller)
            }
            var j = t.calculateStyles(this.props, this.state, this.scrollValues, k), M = [], N = Z(Z({}, x), {
                key: `ScrollbarsCustom-Content`,
                className: sa(`ScrollbarsCustom-Content`, x.className),
                style: j.content,
                children: n ? Y.createElement(Xa.Provider, {value: {parentScrollbar: this}}, D) : D
            }), ee = Z(Z({}, b), {
                key: `ScrollbarsCustom-Scroller`,
                className: sa(`ScrollbarsCustom-Scroller`, b.className),
                style: j.scroller,
                children: Ma(N, this.elementRefContent),
                onScroll: this.handleScrollerScroll
            }), te = Z(Z({}, y), {
                key: `ScrollbarsCustom-Wrapper`,
                className: sa(`ScrollbarsCustom-Wrapper`, y.className),
                style: j.wrapper,
                children: Ma(ee, this.elementRefScroller)
            });
            if (M.push(Ma(te, this.elementRefWrapper)), this.state.trackYVisible || !g && !_) {
                var ne = Z(Z({}, T), {
                    key: `ScrollbarsCustom-ThumbY`,
                    style: j.thumbY,
                    elementRef: this.elementRefThumbY,
                    onDrag: this.handleThumbYDrag,
                    onDragEnd: this.handleThumbYDragEnd,
                    axis: Wa.Y
                }), P = Z(Z(Z(Z({}, C), {
                    key: `ScrollbarsCustom-TrackY`,
                    style: j.trackY,
                    elementRef: this.elementRefTrackY,
                    onClick: this.handleTrackYClick
                }), (s || l) && {onWheel: this.handleTrackYMouseWheel}), {axis: Wa.Y});
                P.children = Y.createElement(Ka, Z({}, ne)), M.push(Y.createElement(qa, Z({}, P)))
            } else this.elementRefTrackY(null), this.elementRefThumbY(null);
            if (this.state.trackXVisible || !g && !v) {
                var re = Z(Z({}, w), {
                    key: `ScrollbarsCustom-ThumbX`,
                    style: j.thumbX,
                    elementRef: this.elementRefThumbX,
                    onDrag: this.handleThumbXDrag,
                    onDragEnd: this.handleThumbXDragEnd,
                    axis: Wa.X
                }), F = Z(Z(Z(Z({}, S), {
                    key: `ScrollbarsCustom-TrackX`,
                    style: j.trackX,
                    elementRef: this.elementRefTrackX,
                    onClick: this.handleTrackXClick
                }), (s || c) && {onWheel: this.handleTrackXMouseWheel}), {axis: Wa.X});
                F.children = Y.createElement(Ka, Z({}, re)), M.push(Y.createElement(qa, Z({}, F)))
            } else this.elementRefTrackX(null), this.elementRefThumbX(null);
            return Ma(Z(Z({}, O), {
                className: sa(`ScrollbarsCustom`, this.state.trackYVisible && `trackYVisible`, this.state.trackXVisible && `trackXVisible`, this.state.isRTL && `rtl`, O.className),
                style: j.holder,
                children: M
            }), this.elementRefHolder)
        }, t.contextType = Xa, t.defaultProps = {
            momentum: !0,
            minimalThumbSize: 30,
            fallbackScrollbarWidth: 20,
            trackClickBehavior: Ga.JUMP,
            scrollDetectionThreshold: 100,
            wrapperProps: {},
            scrollerProps: {},
            contentProps: {},
            trackXProps: {},
            trackYProps: {},
            thumbXProps: {},
            thumbYProps: {}
        }, t
    }(Y.Component), Qa = J(`ui/scrollbar.svg`), $a = J(`ui/scrollbar-hover.svg`), eo = J(`ui/scrollbar-vertical-hover.svg`),
    to = 10, no = 29;

function ro(e, t) {
    let n = () => {
        t === `horizontal` ? (e.style.setProperty(`width`, `${no}px`, `important`), e.style.setProperty(`height`, `${to}px`, `important`)) : (e.style.setProperty(`height`, `${no}px`, `important`), e.style.setProperty(`width`, `${to}px`, `important`))
    };
    n();
    let r = new MutationObserver(n);
    return r.observe(e, {attributes: !0, attributeFilter: [`style`]}), () => r.disconnect()
}

var io = e => e instanceof HTMLElement ? !!e.closest(`.ScrollbarsCustom-ThumbX, .ScrollbarsCustom-ThumbY, .svg-scrollbar-thumb`) : !1;

function ao({
                direction: e = `vertical`,
                scrollbarWidth: t = 20,
                className: n,
                scrollerRef: r,
                onTrackPress: i,
                gamepadScroll: a = !1,
                ...o
            }) {
    let s = (0, Y.useRef)(new Map), [c, l] = (0, Y.useState)(!1), u = (0, Y.useRef)(null), d = (0, Y.useCallback)(e => {
        u.current = e, r?.(e)
    }, [r]);
    C(() => a ? u.current : null, {orientation: e});
    let f = (0, Y.useCallback)(() => {
        l(!0)
    }, []), p = (0, Y.useCallback)(() => {
        l(!1)
    }, []);
    (0, Y.useEffect)(() => {
        let e = s.current;
        return () => {
            e.forEach(e => e()), e.clear()
        }
    }, []), (0, Y.useEffect)(() => {
        if (!c) return;
        let e = () => l(!1);
        return document.addEventListener(`mouseup`, e), document.addEventListener(`pointerup`, e), () => {
            document.removeEventListener(`mouseup`, e), document.removeEventListener(`pointerup`, e)
        }
    }, [c]);
    let m = (0, Y.useCallback)(e => function ({elementRef: t, style: n, onMouseDown: r, ...i}) {
            let a = n => {
                    n && (s.current.get(n) || s.current.set(n, ro(n, e))), t?.(n)
                }, o = e === `horizontal`,
                c = o ? {...n, width: no, height: to, minWidth: no, maxWidth: no, minHeight: to, maxHeight: to} : {
                    ...n,
                    height: no,
                    width: to,
                    minHeight: no,
                    maxHeight: no,
                    minWidth: to,
                    maxWidth: to
                }, l = e => {
                    f(), r?.(e)
                };
            return (0, X.jsxs)(`div`, {
                ...i,
                ref: a,
                className: [`svg-scrollbar-thumb`, o ? `svg-scrollbar-thumb--horizontal` : `svg-scrollbar-thumb--vertical`].join(` `),
                style: c,
                onMouseDown: l,
                children: [(0, X.jsx)(`img`, {
                    className: `svg-scrollbar-thumb__default`,
                    src: Qa,
                    width: to,
                    height: no,
                    draggable: !1,
                    alt: ``
                }), (0, X.jsx)(`img`, {
                    className: `svg-scrollbar-thumb__hover`,
                    src: o ? $a : eo,
                    width: o ? 30 : to,
                    height: o ? 11 : no,
                    draggable: !1,
                    alt: ``
                })]
            })
        }, [f]),
        h = [`svg-scrollbar`, e === `horizontal` ? `svg-scrollbar--horizontal` : void 0, c ? `svg-scrollbar--active` : void 0, n].filter(Boolean).join(` `),
        g = (0, Y.useCallback)(() => ({
            onMouseDown: e => {
                f(), !(!i || io(e.target)) && i()
            }
        }), [f, i]), _ = {onDragStart: () => f(), onDragEnd: () => p()},
        v = {minimalThumbSize: no, maximalThumbSize: no}, y = {elementRef: d};
    return e === `horizontal` ? (0, X.jsx)(`div`, {
        className: h,
        children: (0, X.jsx)(Za, {
            ...o, ...v,
            noScrollY: !0,
            scrollbarWidth: t,
            trackXProps: g(),
            scrollerProps: y,
            thumbXProps: {..._, renderer: m(`horizontal`)}
        })
    }) : (0, X.jsx)(`div`, {
        className: h,
        children: (0, X.jsx)(Za, {
            ...o, ...v,
            noScrollX: !0,
            scrollbarWidth: t,
            trackYProps: g(),
            scrollerProps: y,
            thumbYProps: {..._, renderer: m(`vertical`)}
        })
    })
}

function oo({
                effect: e,
                renderDescription: t,
                waitingForAccept: n,
                onAcceptButtonClick: r,
                onRerollButtonClick: i,
                isResultDismissing: a
            }) {
    let o = e.rerollAllowed === !0 && i != null,
        s = Array.isArray(e.description) ? e.description : e.description == null ? [] : [e.description], {play: c} = oe();
    return (0, X.jsx)(`div`, {
        className: `wheel-spin-result`, "aria-live": `polite`, children: (0, X.jsxs)(`div`, {
            className: `wheel-spin-result-stack`,
            children: [(0, X.jsx)(`img`, {
                src: Yn(e.name, `animated`),
                className: `wheel-spin-result-flyer-image`
            }), (0, X.jsx)(`div`, {
                className: `wheel-spin-result-info`,
                children: (0, X.jsxs)(`div`, {
                    className: `wheel-spin-result-info-inner`,
                    children: [(0, X.jsx)(`div`, {
                        className: `wheel-spin-result-title`,
                        children: e.displayName
                    }), (0, X.jsx)(`div`, {
                        className: `wheel-spin-result-description-scroll`,
                        children: (0, X.jsx)(ao, {
                            className: `wheel-spin-result-description-scrollbar`,
                            children: (0, X.jsx)(`div`, {
                                className: `wheel-spin-result-description-inner`,
                                children: (0, X.jsx)(`div`, {
                                    className: `wheel-spin-result-description`,
                                    children: s.map((e, n) => (0, X.jsx)(`div`, {
                                        className: `wheel-spin-result-description-item`,
                                        children: t(e)
                                    }, `desc-${n}`))
                                })
                            })
                        })
                    })]
                })
            }), (0, X.jsxs)(`div`, {
                className: `wheel-spin-result-actions`,
                children: [(0, X.jsx)(`button`, {
                    type: `button`,
                    className: `wheel-spin-result-accept`,
                    disabled: n || a,
                    onClick: () => {
                        c(f.Casino_Button_Click), r()
                    },
                    onMouseEnter: e => {
                        e.currentTarget.disabled || c(f.Casino_Button_Hover)
                    },
                    children: (0, X.jsx)(q, {button: `X`, className: `wheel-spin-result-accept-gp-hint`})
                }), o && (0, X.jsx)(`button`, {
                    type: `button`,
                    className: `wheel-spin-result-reroll`,
                    disabled: n || a,
                    onClick: () => {
                        c(f.Casino_Button_Click), i()
                    },
                    onMouseEnter: () => c(f.Casino_Button_Hover)
                })]
            })]
        })
    })
}

var so = {
    idleAnimation: {animation: `SHEV_2`, sound: `Casino_Dealer_Shev_2`},
    randomAnimations: [{animation: `SHEV_1`, sound: `Casino_Dealer_Shev_1`}, {
        animation: `SHEV_3`,
        sound: `Casino_Dealer_Shev_3`
    }],
    rollAnimation: {animation: `SHEV_0`, sound: `Casino_Dealer_Shev_0`},
    rollDelay: 1100
}, co = {
    idleAnimation: {animation: `Rin_1`, sound: `Casino_Dealer_Rin_1`},
    randomAnimations: [{animation: `Rin_0`, sound: `Casino_Dealer_Rin_0`}, {
        animation: `Rin_2`,
        sound: `Casino_Dealer_Rin_2`
    }, {animation: `Rin_4`, sound: `Casino_Dealer_Rin_4`}, {
        animation: `Rin_5`,
        sound: `Casino_Dealer_Rin_5`
    }, {animation: `Rin_6`, sound: `Casino_Dealer_Rin_6`}],
    rollAnimation: {animation: `Rin_5`, sound: `Casino_Dealer_Rin_5`},
    rollDelay: 2700
}, lo = {
    idleAnimation: {animation: `Faker_idle`, sound: `Casino_Dealer_Faker_Idle`},
    randomAnimations: [{animation: `Faker_1`, sound: `Casino_Dealer_Faker_1`}, {
        animation: `Faker_2`,
        sound: `Casino_Dealer_Faker_2`
    }, {animation: `Faker_3`, sound: `Casino_Dealer_Faker_3`}],
    rollAnimation: {animation: `Faker_roll`, sound: `Casino_Dealer_Faker_Roll`},
    rollDelay: 1900
}, uo = {
    idleAnimation: {animation: `Ozon_idle`, sound: `Casino_Dealer_Ozon_Idle`},
    randomAnimations: [{animation: `Ozon_1`, sound: `Casino_Dealer_Ozon_1`}, {
        animation: `Ozon_2`,
        sound: `Casino_Dealer_Ozon_2`
    }, {animation: `Ozon_3`, sound: `Casino_Dealer_Ozon_3`}],
    rollAnimation: {animation: `Ozon_roll`, sound: `Casino_Dealer_Ozon_Roll`},
    rollDelay: 1200
};

function fo(e) {
    return e.name === `selectedGenre` && e.type === `string` && Array.isArray(e.allowedValues)
}

function po(e) {
    return e.find(fo) ?? null
}

function mo(e) {
    return e.type === `player-ids`
}

function ho(e) {
    return e?.params ? e.params.find(mo) ?? null : null
}

var go = 6e4, _o = `Нужно действие`, vo = `Сначала выполните обязательное действие по эффекту`;

function yo(e, t) {
    return e?.filter(e => e.ownerPlayerId === t && e.status === `pending` && (Vr(e) || typeof e.nextStep == `string` && e.nextStep.length > 0)) ?? []
}

async function bo(e) {
    return yo(await le(), e)
}

function xo(e, t) {
    return yo(e, t).length > 0
}

function So(e) {
    return typeof e == `object` && !!e
}

function Co(e, t) {
    return So(e) && typeof e.name == `string` && e.name.length > 0 ? t.find(t => t.name === e.name) ?? e : typeof e == `string` && e.length > 0 ? t.find(t => t.name === e) ?? {
        name: e,
        displayName: e
    } : null
}

function wo(e) {
    return typeof e == `string` && e.length > 0 ? e : So(e) && typeof e.name == `string` && e.name.length > 0 ? e.name : null
}

function To(e, t, n) {
    let r = wo(e);
    if (r != null) {
        let e = t.find(e => e.name === r);
        if (e != null) return e
    }
    if (So(e)) {
        let t = wo(e);
        if (t != null) {
            let r = n.find(e => e.name === t);
            if (r != null) return r;
            if (typeof e.displayName == `string`) return e
        }
    }
    return Co(e, n)
}

function Eo(e, t) {
    return e.map(e => Co(e, t)).filter(e => e != null)
}

function Do(e, t) {
    if (!So(e)) return null;
    let n = e.candidates ?? e.candidadets ?? e.candidateNames, r = e.selected ?? e.selectedName;
    if (!Array.isArray(n)) return null;
    let i = Eo(n, t);
    if (i.length === 0) return null;
    let a = To(r, i, t);
    return a == null ? null : {candidates: i, selected: a}
}

function Oo(e, t) {
    return e?.gnusWheel == null ? null : Do(e.gnusWheel, t)
}

function ko(e, t, n = 0) {
    if (n > 8) return null;
    let r = Do(e, t);
    if (r != null) return r;
    if (!So(e)) return null;
    for (let r of Object.values(e)) {
        let e = ko(r, t, n + 1);
        if (e != null) return e
    }
    return null
}

function Ao(e) {
    return So(e) ? e.type === `random-effect` ? {
        id: typeof e.id == `string` ? e.id : void 0,
        type: e.type,
        candidateNames: Array.isArray(e.candidateNames) ? e.candidateNames.filter(e => typeof e == `string` && e.length > 0) : void 0,
        selectedName: typeof e.selectedName == `string` ? e.selectedName : null
    } : So(e.step) && e.step.type === `random-effect` ? Ao(e.step) : null : null
}

function jo(e, t = 0) {
    if (t > 8) return null;
    let n = Ao(e);
    if (n != null) return n;
    if (Array.isArray(e)) {
        for (let n of e) {
            let e = jo(n, t + 1);
            if (e != null) return e
        }
        return null
    }
    if (!So(e)) return null;
    let r = Mo(e);
    if (r != null) return r;
    for (let n of Object.values(e)) {
        let e = jo(n, t + 1);
        if (e != null) return e
    }
    return null
}

function Mo(e) {
    let t = e.steps;
    if (!Array.isArray(t)) return null;
    for (let e of t) {
        let t = Ao(e);
        if (t?.type === `random-effect`) return t
    }
    return null
}

function No(e) {
    let t = e.steps;
    if (!Array.isArray(t)) return null;
    for (let e of t) if (!(!So(e) || e.type !== `target-random`) && !(typeof e.id != `string` || e.id.length === 0)) return {
        id: e.id,
        type: `target-random`,
        candidates: Array.isArray(e.candidates) ? e.candidates.filter(So) : [],
        selected: So(e.selected) ? e.selected : null
    };
    return null
}

function Po(e, t) {
    if (!So(e)) return null;
    let n = e.candidates ?? e.candidadets;
    if (!Array.isArray(n)) return null;
    let r = Eo(n, t);
    return r.length > 0 ? r : null
}

function Fo(e, t, n) {
    let r = n != null && n.length > 0 ? n : e.candidateNames != null && e.candidateNames.length > 0 ? Eo(e.candidateNames, t) : [];
    if (r.length === 0) return null;
    let i = To(e.selectedName ?? e.candidateNames?.[0] ?? null, r, t);
    return i == null ? null : {candidates: r, selected: i}
}

function Io(e, t) {
    if (e.acceptDataId != null && e.acceptDataId.length > 0) return e.acceptDataId;
    if (e.fullResolution?.id) return e.fullResolution.id;
    if (e.resolution?.id) return e.resolution.id;
    let n = t.resolution ?? t.effectResolution;
    if (So(n) && typeof n.id == `string`) return n.id;
    for (let e of [`resolutionId`, `effectResolutionId`, `sourceRollId`, `rollId`, `id`]) {
        let n = t[e];
        if (typeof n == `string` && n.length > 0) return n
    }
    return null
}

function Lo(e, t, n) {
    if (n?.id) return n.id;
    let r = Mo(e);
    if (r?.id) return r.id;
    let i = e.step;
    return So(i) && typeof i.id == `string` && i.id.length > 0 ? i.id : typeof e.stepId == `string` && e.stepId.length > 0 ? e.stepId : typeof t.fullResolution?.nextStep == `string` && t.fullResolution.nextStep.length > 0 ? t.fullResolution.nextStep : typeof t.fullResolution?.currentStep == `string` && t.fullResolution.currentStep.length > 0 ? t.fullResolution.currentStep : typeof t.resolution?.nextStep == `string` && t.resolution.nextStep.length > 0 ? t.resolution.nextStep : typeof t.resolution?.currentStep == `string` && t.resolution.currentStep.length > 0 ? t.resolution.currentStep : null
}

function Ro(e, t, n = {}) {
    return {
        resolutionId: n.resolutionId,
        stepId: e,
        candidates: t.candidates,
        selected: t.selected,
        targetRandomStep: n.targetRandomStep,
        inlineCasinoResolution: n.inlineCasinoResolution
    }
}

function zo(e) {
    let t = zr(e.steps).find(e => e.type === `random-effect`);
    return t?.type === `random-effect` ? {
        id: t.id,
        type: t.type,
        candidateNames: t.candidateNames,
        selectedName: t.selectedName
    } : jo(e)
}

function Bo(e, t, n, r) {
    let i = zo(e);
    if (i?.type !== `random-effect`) return null;
    let a = i.id ?? e.nextStep ?? e.currentStep ?? null;
    if (a == null) return null;
    if (n != null && n.length > 0 && r != null) return Ro(a, {
        candidates: n,
        selected: To(r.name, n, t) ?? r
    }, {resolutionId: e.id});
    let o = ko(e.pendingContext, t);
    if (o != null) return Ro(a, o, {resolutionId: e.id});
    let s = ko(e.result, t);
    if (s != null) return Ro(a, s, {resolutionId: e.id});
    let c = Fo(i, t, Po(So(e.result) && So(e.result.wheel) ? e.result.wheel : So(e.pendingContext) && So(e.pendingContext.wheel) ? e.pendingContext.wheel : null, t));
    return c == null ? null : Ro(a, c, {resolutionId: e.id})
}

function Vo(e) {
    return zo(e)?.type === `random-effect`
}

function Ho(e, t) {
    return e.ownerPlayerId === t || e.sourcePlayerId === t
}

function Uo(e, t) {
    let n = yo(e, t);
    return n.find(e => e.kind === `gnus-wheel` || e.effectName === `untargeted_gnus` || Vo(e)) ?? n[0] ?? e?.find(e => e.status === `pending` && Ho(e, t) && (e.kind === `gnus-wheel` || e.effectName === `untargeted_gnus` || Vo(e))) ?? null
}

function Wo(e) {
    let t = e?.data;
    if (t != null) return {candidates: t.candidates ?? null, selected: t.selected ?? null};
    if (So(e)) {
        let t = ko(e, []);
        if (t != null) return {candidates: t.candidates, selected: t.selected}
    }
    return {candidates: null, selected: null}
}

function Go(e, t) {
    if (e?.payload == null || !So(e.payload)) return null;
    let n = e.payload, r = ko(n.wheel, t) ?? ko(n, t);
    if (r == null) return null;
    let i = Mo(n);
    if (i?.type !== `random-effect`) return null;
    let a = Lo(n, e, i);
    if (a == null) return null;
    let o = Io(e, n), s = No(n);
    return Ro(a, r, {resolutionId: o ?? void 0, targetRandomStep: s, inlineCasinoResolution: o == null})
}

function Ko(e, t = []) {
    let n = Go(e, t);
    if (n != null) return n;
    if (e == null) return null;
    if (e.fullResolution != null) {
        let n = Bo(e.fullResolution, t, null, null);
        if (n != null) return n
    }
    let r = So(e.payload) ? e.payload : {}, i = [r, e], a = null, o = null;
    for (let e of i) a ??= jo(e), o ??= ko(e, t);
    if (o == null && a != null) {
        let e = So(r.wheel) ? Po(r.wheel, t) : null;
        o = Fo(a, t, e)
    }
    if (o == null) return null;
    let s = Io(e, r);
    if (s == null) return null;
    let c = Lo(r, e, a);
    return c == null ? null : Ro(c, o, {resolutionId: s, targetRandomStep: No(r)})
}

function qo(e) {
    return e.acceptedWheelKind === `gnus` || e.acceptedEffectName === `untargeted_gnus`
}

async function Jo(e, t, n, r = {}) {
    let i = Ko(e, t);
    if (i != null) return i;
    if (!qo(r) || n == null) return null;
    let a = await vt(), o = Uo(await le(), n), {candidates: s, selected: c} = Wo(a),
        l = (s?.length ?? 0) > 0 && c != null;
    if (o != null) {
        let e = Bo(o, t, s, c);
        if (e != null) return e;
        if (l) {
            let e = zo(o)?.id ?? o.nextStep ?? o.currentStep ?? Ur(o.steps, o.nextStep ?? o.currentStep ?? null)?.id ?? null;
            if (e != null) return Ro(e, {candidates: s, selected: c}, {resolutionId: o.id})
        }
    }
    return null
}

var Yo = {
    action_shooter_slasher_fighting: `Экшен, шутер, слэшер, файтинг`,
    strategy_tycoon_tower_defence: `Стратегия, тайкун, tower defence`,
    rpg: `RPG`,
    horror: `Хоррор`,
    platformer_puzzle_quest: `Платформер, головоломка, квест`,
    racing: `Гонки`,
    adventure: `Приключения`
};

function Xo(e) {
    return Yo[e] ?? e.replace(/_/g, ` `)
}

var Zo = 650;

function Qo(e) {
    let t = [...e];
    for (let e = t.length - 1; e > 0; --e) {
        let n = Math.floor(Math.random() * (e + 1));
        [t[e], t[n]] = [t[n], t[e]]
    }
    return t
}

function $o(e) {
    return e.length === 0 ? null : e[Math.floor(Math.random() * e.length)] ?? null
}

function es(e) {
    return Qo(e).map((e, t) => ({slotId: `genre-slot-${t}`, genreValue: e}))
}

function ts(e, t, n) {
    let r = Qo(e.map(e => e.genreValue).filter(e => e !== n)), i = 0, a = {};
    for (let o of e) {
        if (o.slotId === t) {
            a[o.slotId] = n;
            continue
        }
        if (r.length > 0) {
            let e = r[i % r.length];
            i += 1, a[o.slotId] = e;
            continue
        }
        a[o.slotId] = o.genreValue
    }
    return a
}

var ns = (0, Y.memo)(function ({
                                   slot: e,
                                   isFlipping: t,
                                   isWinner: n,
                                   revealedLabel: r,
                                   disabled: i,
                                   onHover: a,
                                   onPick: o
                               }) {
    let {slotId: s} = e, c = (0, Y.useCallback)(() => {
        i || o(s)
    }, [i, o, s]);
    return (0, X.jsx)(`button`, {
        type: `button`,
        className: [`random-genre-picker-item`, t ? `random-genre-picker-item--flipping` : ``, n ? `random-genre-picker-item--revealed` : ``, i ? `random-genre-picker-item--disabled` : ``].filter(Boolean).join(` `),
        disabled: i,
        onClick: c,
        onMouseEnter: () => {
            i || a()
        },
        children: (0, X.jsxs)(`div`, {
            className: `random-genre-picker-item__flipper`,
            children: [(0, X.jsx)(`div`, {
                className: `random-genre-picker-item__face random-genre-picker-item__face--front`,
                children: `?`
            }), (0, X.jsx)(`div`, {
                className: `random-genre-picker-item__face random-genre-picker-item__face--back`,
                children: r
            })]
        })
    })
}), rs = ({genreOptions: e, placeholder: t, onClose: r, onRevealComplete: i, onConfirm: a, submitting: o}) => {
    let {play: s} = oe(), {
            onBackHover: c,
            onBackClick: l
        } = n(), [u, d] = (0, Y.useState)(null), [p, m] = (0, Y.useState)(!1), [h, g] = (0, Y.useState)(!1), [_, v] = (0, Y.useState)({}),
        y = (0, Y.useRef)(null), [b] = (0, Y.useState)(() => es(e)), [x] = (0, Y.useState)(() => $o(e)),
        S = (0, Y.useCallback)(() => {
            y.current != null && (window.clearTimeout(y.current), y.current = null), d(null), m(!1), g(!1), v({})
        }, []);
    (0, Y.useEffect)(() => () => {
        y.current != null && window.clearTimeout(y.current)
    }, []);
    let C = (0, Y.useCallback)(e => {
        if (o || p || u != null || x == null) return;
        s(f.Casino_Button_Click);
        let t = ts(b, e, x);
        d(e), v(t), m(!0), y.current = window.setTimeout(() => {
            g(!0), i(x), y.current = null
        }, Zo)
    }, [u, p, i, s, x, o, b]), w = (0, Y.useCallback)(() => {
        !h || o || (s(f.Casino_Button_Click), a())
    }, [h, a, s, o]), T = (0, Y.useCallback)(() => {
        l(), S(), r()
    }, [l, r, S]);
    k(`A`, w, h && !o), k(`B`, T, !h && !o);
    let E = e => {
        if (!p && !h) return null;
        let t = _[e.slotId];
        return t == null ? null : Xo(t)
    };
    return (0, X.jsx)(`div`, {
        className: `success-question-overlay`,
        role: `dialog`,
        "aria-modal": `true`,
        "aria-labelledby": `random-genre-picker-title`,
        children: (0, X.jsxs)(`div`, {
            className: `success-question-panel success-question-panel--wide`,
            children: [(0, X.jsxs)(`div`, {
                className: `success-question-header`,
                children: [(0, X.jsx)(`h2`, {
                    id: `random-genre-picker-title`,
                    className: `success-question-title`,
                    children: t
                }), h ? null : (0, X.jsxs)(`button`, {
                    type: `button`,
                    className: `success-question-back`,
                    onClick: T,
                    onMouseEnter: c,
                    disabled: o,
                    "aria-label": `Назад`,
                    children: [(0, X.jsx)(`span`, {
                        className: `success-question-back-arrow`,
                        "aria-hidden": !0
                    }), (0, X.jsx)(`span`, {children: `Назад`}), (0, X.jsx)(q, {
                        button: `B`,
                        className: `success-question-back-gp-hint`
                    })]
                })]
            }), (0, X.jsx)(`div`, {
                className: `success-question-genre-list`,
                children: b.map(e => (0, X.jsx)(ns, {
                    slot: e,
                    isFlipping: p,
                    isWinner: h && u != null ? u === e.slotId : !1,
                    revealedLabel: E(e),
                    disabled: o || p || u != null,
                    onHover: () => s(f.Casino_Button_Hover),
                    onPick: C
                }, e.slotId))
            }), (0, X.jsx)(`div`, {
                className: `success-question-actions`,
                children: (0, X.jsxs)(`button`, {
                    type: `button`,
                    className: `success-question-confirm ${!h || o ? `success-question-confirm--disabled` : ``}`,
                    disabled: !h || o,
                    onClick: w,
                    onMouseEnter: () => {
                        h && !o && s(f.Casino_Button_Hover)
                    },
                    children: [!o && h && (0, X.jsx)(q, {
                        button: `A`,
                        className: `success-question-confirm-gp-hint`
                    }), o ? `Загрузка` : `Подтвердить`]
                })
            })]
        })
    })
}, is = ({
             isOpen: e,
             onClose: t,
             genreOptions: n,
             placeholder: r = `Выберите жанр`,
             onRevealComplete: i,
             onConfirm: a,
             submitting: o = !1
         }) => {
    let s = (0, Y.useMemo)(() => n.join(`|`), [n]);
    return E(e), e ? (0, X.jsx)(rs, {
        genreOptions: n,
        placeholder: r,
        onClose: t,
        onRevealComplete: i,
        onConfirm: a,
        submitting: o
    }, s) : null
};

function as(e) {
    return Gn({
        queryKey: Er.playerList,
        queryFn: Ze,
        enabled: e?.enabled ?? !0,
        refetchInterval: e?.refetchInterval ?? !1
    })
}

var os = `nassal-auth`, ss = `nassal-pause`, cs = `nassal-layout`, ls = `nassal-mini-game-leaderboard-get`,
    us = `nassal-mini-game-leaderboard-get-result`, ds = `nassal-mini-game-leaderboard-post`,
    fs = `nassal-mini-game-leaderboard-post-result`, ps = `nassal-sound-duration-result`,
    ms = `nassal-mini-game-wordle`, hs = `nassal-mini-game-flappy`, gs = `nassal-mini-game-guess-the-game`,
    _s = `INSIDER`;

function vs(e) {
    return typeof e == `object` && !!e
}

function ys(e) {
    if (e.type !== `nassal-mini-game-leaderboard-get` || typeof e.requestId != `string` || !e.requestId || typeof e.miniGameKey != `string` || !e.miniGameKey) return null;
    let t = e.limit === void 0 || e.limit === null || e.limit === `` ? void 0 : Number(e.limit);
    return t !== void 0 && !Number.isFinite(t) ? null : {
        type: ls,
        requestId: e.requestId,
        miniGameKey: e.miniGameKey,
        limit: t
    }
}

function bs(e) {
    if (e.type !== `nassal-mini-game-leaderboard-post` || typeof e.requestId != `string` || !e.requestId || typeof e.miniGameKey != `string` || !e.miniGameKey) return null;
    let t = Number(e.score);
    return Number.isFinite(t) ? {type: ds, requestId: e.requestId, miniGameKey: e.miniGameKey, score: t} : null
}

function xs(e) {
    return vs(e) ? ys(e) ?? bs(e) : null
}

function Ss(e) {
    return !vs(e) || e.type !== `nassal-insider` ? !1 : (typeof e.text == `string` ? e.text.trim() : ``).toUpperCase() === _s
}

function Cs(e) {
    return e instanceof Error ? e.message : `Request failed`
}

async function ws(e) {
    if (e.type === `nassal-mini-game-leaderboard-get`) {
        let t = await Oe(e.miniGameKey, e.limit);
        return {type: us, requestId: e.requestId, ok: !0, data: t}
    }
    if (!await St()) return {type: fs, requestId: e.requestId, ok: !1, error: `Требуется авторизация`};
    let t = rn(), n = await st(e.miniGameKey, e.score);
    return n ? (t && Promise.resolve(t.onScore(e.score)).catch(e => {
        console.error(`miniGameEffectAcceptBridge: accept after score failed`, e)
    }), {type: fs, requestId: e.requestId, ok: !0, data: n}) : {
        type: fs,
        requestId: e.requestId,
        ok: !1,
        error: `Не удалось отправить результат`
    }
}

function Ts(e, t) {
    return {type: e.type === `nassal-mini-game-leaderboard-get` ? us : fs, requestId: e.requestId, ok: !1, error: Cs(t)}
}

function Es(e) {
    return {type: os, user: e}
}

function Ds() {
    return {type: ms}
}

function Os() {
    return {type: hs}
}

function ks() {
    return {type: gs}
}

var As = `nassal-webgl-iframe-post`;

function js(e) {
    window.dispatchEvent(new CustomEvent(As, {detail: {payload: e}}))
}

function Ms() {
    js(Ds())
}

function Ns() {
    js(Os())
}

function Ps() {
    js(ks())
}

var Fs = {[cn.southernDialect]: Ms, [cn.helicopter]: Ns, [cn.gamemaster]: Ps};

function Is(e) {
    Fs[e]()
}

function Ls(e, t) {
    return un(e) ? (Zt(t, () => Is(e)), !0) : !1
}

function Rs(e) {
    return e != null && Object.values(bn).includes(e)
}

function zs(e, t) {
    return Rs(e) ? (Zt(t, () => pn({forWheelEffectAccept: !0})), !0) : !1
}

var Bs = {
        regular: `ui/dealer-regular.svg`,
        platinum: `ui/dealer-premium.svg`,
        judge: `ui/dealer-judge.svg`,
        gnus: `ui/dealer-gnus.svg`
    }, Vs = 500, Hs = 2e3, Us = 4, Ws = 8, Gs = 5e3, Ks = 1e4, qs = .99, Js = 1162, Ys = Js / 800,
    Xs = (Js - 2 * Math.round(6 * Ys)) / 2, Zs = Math.round(10 * Ys), Qs = Math.round(30 * Ys), $s = .9, ec = 16,
    tc = new Set([`leprechaun_boost`, `leprechaun_debuff`]);

function nc(e, t, n) {
    return e === `regular` ? [...t ?? [], ...n ?? []].some(e => tc.has(e.name)) : !1
}

var rc = `Смена крупье`,
    ic = `Казино не несет ответственности за потерю доступных обычных колес, вызванную сменой крупье.`,
    ac = `Завершение фазы`,
    oc = `У вас не осталось Premium и гнусных колес. После подтверждения фаза казино в этом ходу будет завершена.`;

function sc(e) {
    if (e <= 0) return {r: 0, chordPx: 0};
    let t = Math.max(1, Xs - Zs), n = t * .65, r = 0, i = t * .5, a = t - 2;
    for (let o = i; o <= a; o += 1.5) {
        let i = 2 * o * Math.sin(Math.PI / e), a = 2 * t * t - o * o;
        if (a <= 0) continue;
        let s = -o + Math.sqrt(a);
        if (s <= 0) continue;
        let c = Math.min(i, s) * .99;
        c > r && (r = c, n = o)
    }
    return {r: n, chordPx: Math.max(0, r)}
}

function cc(e, t, n, r) {
    if (t <= 0) return {chordPx: 0, offsetX: 0, offsetY: 0, rotateDeg: 0};
    let i = n, a = (e + .5) * (360 / t) * Math.PI / 180, o = i * Math.sin(a), s = -i * Math.cos(a);
    return {chordPx: r, offsetX: o, offsetY: s, rotateDeg: Math.atan2(o, -s) * 180 / Math.PI}
}

function lc(e, t) {
    if (t <= 0) return {offsetX: 0, offsetY: 0};
    let n = Xs * $s, r = 360 / t * e * Math.PI / 180;
    return {offsetX: n * Math.sin(r), offsetY: -n * Math.cos(r)}
}

var uc = e => {
    let t = /(https?:\/\/[^\s]+)/gi;
    return e.split(t).map((e, n) => e == null ? null : t.test(e) ? (t.lastIndex = 0, (0, X.jsx)(`a`, {
        href: e,
        target: `_blank`,
        rel: `noopener noreferrer`,
        className: `description-link`,
        children: e
    }, n)) : e)
};

function dc(e, t) {
    let n;
    switch (t) {
        case`platinum`:
            n = e.filter(e => e.impact === `positive`);
            break;
        case`judge`:
            n = e.filter(e => e.pools?.includes(`judge`));
            break;
        case`gnus`:
            n = e.filter(e => e.pools?.includes(`gnus`));
            break;
        default:
            n = e;
            break
    }
    return n.length > 0 ? n.slice(0, ec) : e.slice(0, ec)
}

function fc(e, t) {
    let n = e?.length ? [...e] : t ? [t] : [];
    return t != null && !n.some(e => e.name === t.name) && n.push(t), n
}

function pc(e, t) {
    return (e + .5) * 360 / t
}

function mc(e, t) {
    if (t <= 0) return 0;
    let n = 360 / t, r = (-((e % 360 + 360) % 360) % 360 + 360) % 360;
    return Math.floor(r / n) % t
}

function hc(e, t, n) {
    let r = 360 / Math.max(1, n) * qs / 2, i = (Math.random() * 2 - 1) * r, a = pc(t, n) + i,
        o = Us + Math.floor(Math.random() * (Ws - Us + 1)), s = (e % 360 + 360) % 360, c = ((-a - s) % 360 + 360) % 360;
    return {end: e - o * 360 + c, durationMs: Gs + Math.floor(Math.random() * (Ks - Gs + 1))}
}

var gc = ({
              isOpen: e,
              onClose: t,
              player: r,
              effects: i,
              refreshPlayer: a,
              slotsTerminalMistakesCount: o = null,
              setSlotsTerminalMistakesCount: s = () => {
              },
              onResolutionFlowActiveChange: c,
              onPendingResolutionNeedsUi: l,
              onCasinoInteractionBlockedChange: u
          }) => {
    let {play: d, playKeyboard: p, playCasinoWheelItem: m} = oe(), {
            onBackHover: h,
            withBackClick: g
        } = n(), {showNotification: _} = Cn(), {data: v} = as({enabled: e}), y = r?.platinumChips ?? 0,
        b = r?.gnusAvailable ?? 0, x = r?.role, S = (0, Y.useMemo)(() => ({
            casinoPhase: r?.casinoPhase ?? null,
            platinumChips: y,
            gnusAvailable: b,
            role: x,
            status: r?.status ?? `idle`
        }), [r?.casinoPhase, y, b, x, r?.status]), [C, w] = (0, Y.useState)(() => xr(S)), [T, D] = (0, Y.useState)([]), [O, j] = (0, Y.useState)(0), [M, N] = (0, Y.useState)(0),
        ee = (0, Y.useRef)(0), P = (0, Y.useRef)(!1), re = (0, Y.useRef)(null), F = (0, Y.useRef)(!1),
        ie = (0, Y.useRef)(`catalog`), I = (0, Y.useRef)(C), ae = (0, Y.useRef)(null),
        L = (0, Y.useRef)(null), [se, R] = (0, Y.useState)(null), z = (0, Y.useCallback)(e => {
            ie.current = `catalog`, D(dc(i, e))
        }, [i]), le = (0, Y.useCallback)(e => {
            ie.current = `roll`, D(e)
        }, []), [ue, de] = (0, Y.useState)(!1);
    (0, Y.useEffect)(() => {
        if (!ue) {
            ne();
            return
        }
        let e = () => {
            A(900, {strongMagnitude: .55, weakMagnitude: .35})
        };
        e();
        let t = window.setInterval(e, 800);
        return () => {
            window.clearInterval(t), ne()
        }
    }, [ue]);
    let [fe, pe] = (0, Y.useState)(!1), [B, me] = (0, Y.useState)(null), [he, _e] = (0, Y.useState)(!1), [ve, ye] = (0, Y.useState)(null),
        be = (0, Y.useRef)(null), [xe, Se] = (0, Y.useState)(null), [Ce, we] = (0, Y.useState)(!1), [Te, De] = (0, Y.useState)(!1), [Oe, ke] = (0, Y.useState)(!1), [Ae, je] = (0, Y.useState)(null), [H, U] = (0, Y.useState)(null), [W, Pe] = (0, Y.useState)(!1), [Fe, Ie] = (0, Y.useState)(!1),
        Le = (0, Y.useRef)(null), [Re, ze] = (0, Y.useState)(!1), [Be, He] = (0, Y.useState)(!1), [Ue, We] = (0, Y.useState)(!1), [Ge, Ke] = (0, Y.useState)(null), [qe, Je] = (0, Y.useState)(null), [Ye, Xe] = (0, Y.useState)(null),
        Ze = (0, Y.useRef)(null),
        Qe = (0, Y.useRef)(null), [$e, et] = (0, Y.useState)(null), [K, nt] = (0, Y.useState)(null),
        rt = (0, Y.useRef)(null),
        it = (0, Y.useRef)(null), [at, ot] = (0, Y.useState)(null), [st, ct] = (0, Y.useState)(void 0),
        lt = (0, Y.useRef)(null), ut = (0, Y.useCallback)(() => {
            Ie(!0), Le.current != null && clearTimeout(Le.current), Le.current = setTimeout(() => {
                Le.current = null, Ie(!1)
            }, Hs)
        }, []);
    (0, Y.useEffect)(() => {
        ee.current = O
    }, [O]), (0, Y.useEffect)(() => {
        if (!e) {
            let e = window.setTimeout(() => {
                j(0), ee.current = 0, N(0), de(!1), R(null), pe(!1), me(null), D([]), Se(null), we(!1), De(!1), ke(!1), je(null), U(null), Pe(!1), Le.current != null && (clearTimeout(Le.current), Le.current = null), Ie(!1), He(!1), nt(null), rt.current = null, ot(null), ct(void 0), Ze.current = null, Qe.current = null, et(null), Ke(null), We(!1), Je(null), w(xr(S)), ie.current = `catalog`, I.current = xr(S), P.current = !1, re.current = null, F.current = !1
            }, 0);
            return () => clearTimeout(e)
        }
        let t = window.setTimeout(() => {
            w(e => Sr(e, S) ? e : xr(S))
        }, 0);
        return () => clearTimeout(t)
    }, [e, S]), (0, Y.useEffect)(() => {
        if (!e || ue || K != null || at != null || ie.current !== `catalog`) return;
        let t = window.setTimeout(() => {
            D(dc(i, C))
        }, 0);
        return () => clearTimeout(t)
    }, [e, ue, K, at, i, C]), (0, Y.useEffect)(() => {
        !e || ue || K != null || at != null || I.current !== C && (d(f.Casino_Wheel_Switch), I.current = C, z(C))
    }, [K, z, e, ue, d, at, C]), (0, Y.useEffect)(() => {
        e || en()
    }, [e]), (0, Y.useEffect)(() => () => {
        ae.current != null && cancelAnimationFrame(ae.current)
    }, []), (0, Y.useEffect)(() => {
        if (!e || ue || T.length > 0 || B?.data?.candidates != null) return;
        let t = window.setTimeout(() => {
            z(C)
        }, 0);
        return () => clearTimeout(t)
    }, [z, B?.data?.candidates, e, ue, C, T.length]), (0, Y.useEffect)(() => {
        if (!e) return;
        let t = window.setTimeout(() => {
            Sr(C, S) ? vr(S) && C === `gnus` && !yr(S) && w(`regular`) : w(Cr(S))
        }, 0);
        return () => clearTimeout(t)
    }, [e, S, C]), (0, Y.useEffect)(() => {
        if (!e || r == null) return;
        let t = !1;
        return (async () => {
            try {
                let e = await vt();
                if (t) return;
                if (e?.data != null) {
                    me(e), pe(!0);
                    let t = e.data.kind;
                    Sr(t, S) && (I.current = t, w(t));
                    let n = fc(e.data.candidates, e.data.selected);
                    n.length > 0 && le(n)
                }
            } catch (e) {
                console.error(`WheelOverlay: getPendingSlots failed`, e)
            }
        })(), () => {
            t = !0
        }
    }, [le, e, r, S]);
    let dt = () => {
            let e = (qe ?? B?.data?.selected)?.params;
            return Array.isArray(e) ? e : []
        }, ft = dt().some(e => e.name === `value` && e.type === `number`),
        pt = dt().some(e => e.name === `successfully` && e.type === `boolean`), ht = po(dt()), gt = ht != null,
        _t = ht?.allowedValues ?? [], yt = (0, Y.useCallback)(() => {
            we(!1), Se(null), U(null)
        }, []), bt = (0, Y.useCallback)(() => {
            ke(!1), je(null)
        }, []), xt = (0, Y.useCallback)(() => {
            De(!1), U(null), pt && Se(null)
        }, [pt]), St = (0, Y.useRef)(!1);
    (0, Y.useEffect)(() => {
        St.current && !Ce && (Se(null), U(null)), St.current = Ce
    }, [Ce]);
    let Ct = (0, Y.useRef)(!1);
    (0, Y.useEffect)(() => {
        Ct.current && !Te && (U(null), pt && Se(null)), Ct.current = Te
    }, [Te, pt]);
    let wt = (0, Y.useRef)(!1);
    (0, Y.useEffect)(() => {
        wt.current && !Oe && je(null), wt.current = Oe
    }, [Oe]);
    let Tt = B?.data?.selected, Et = qe ?? Tt ?? null, Dt = (0, Y.useMemo)(() => ho(Et), [Et]),
        Ot = (0, Y.useMemo)(() => ci(Tt, si(B?.data)), [Tt, B?.data]), kt = (0, Y.useMemo)(() => {
            if (!ni(Tt) || !Array.isArray(v) || v.length === 0) return null;
            let e = Tt?.targetRules, t = e?.type === `targeted` || e?.type === `targeted-multi` ? e.targetRole : void 0;
            return v.filter(e => t === `judge` ? e.role === `judge` : t === `competitor` ? e.role === `competitor` : !0).map(e => ({
                playerId: e.id,
                name: e.name,
                status: e.status === `idle` || e.status === `content` || e.status === `casino` ? e.status : `idle`,
                canTarget: !0
            }))
        }, [Tt, v]), At = Ot ?? kt, jt = (0, Y.useMemo)(() => {
            let e = Ge ?? At;
            return e == null ? null : v?.length ? ai(e, v, i) : e
        }, [Ge, At, v, i]), Mt = (0, Y.useMemo)(() => li(Tt, At), [Tt, At]),
        Nt = (0, Y.useMemo)(() => di(B?.data, Tt), [B?.data, Tt]),
        Pt = (0, Y.useMemo)(() => Oo(B?.data, i), [B?.data, i]);
    (0, Y.useEffect)(() => {
        let e = B?.data;
        e != null && (e.gnusWheel != null || e.targets != null && e.targets.length > 0) && (Qe.current = e)
    }, [B?.data]);
    let Ft = Pt != null && (Mt || Nt != null), It = (0, Y.useCallback)(async (e, t, n, o, s) => {
            if (W) return;
            let u = Ze.current, d = u?.parentEffect ?? B?.data?.selected, p = u?.wheelKind ?? B?.data?.kind,
                m = u?.rollId ?? B?.data?.id;
            Pe(!0), ut();
            let h = u?.value ?? e, g = u?.successfully ?? t, v = u?.selectedGenre ?? o,
                y = u?.pendingRandomTargetConfirmAfterWheel === !0 ? void 0 : u?.targetPlayerId ?? n, b = u?.extraData ?? s,
                x = u?.pendingTargetPickAfterWheel === !0 || u?.pendingRandomTargetConfirmAfterWheel === !0,
                S = b != null && Object.keys(b).length > 0,
                C = ft || pt || gt && v != null || h !== void 0 || g !== void 0 || S;
            try {
                let e = await Ve({
                    ...y ? {targetPlayerId: y} : {}, ...C ? {
                        data: {
                            value: ft ? h : void 0,
                            successfully: g,
                            selectedGenre: gt ? v : void 0, ...b ?? {}
                        }
                    } : {}
                });
                Ze.current = null, Je(null), x && (Qe.current = null);
                let t = x ? null : await Jo(e, i, r?.id, {
                    acceptedEffectName: d?.name,
                    acceptedWheelKind: p,
                    acceptedRollId: m
                });
                t != null && c?.(!0);
                let n = p === `gnus`, o = a == null ? null : await a();
                Or(o?.id ?? r?.id);
                let s = {
                    casinoPhase: o?.casinoPhase ?? r?.casinoPhase ?? null,
                    platinumChips: o?.platinumChips ?? r?.platinumChips ?? 0,
                    gnusAvailable: o?.gnusAvailable ?? r?.gnusAvailable ?? 0,
                    role: o?.role ?? r?.role,
                    status: o?.status ?? r?.status ?? `idle`
                };
                if (n && t == null) if (s.role === `judge`) {
                    let e = o ?? r;
                    if (e?.role === `judge`) try {
                        await V({...ge(e), status: `casino`, casinoPhase: `regular`}), a != null && await a()
                    } catch (e) {
                        console.error(`WheelOverlay: judge phase reset after gnus failed`, e)
                    }
                    w(`judge`)
                } else w(vr(s) ? `regular` : Cr(s));
                if (e?.success && (d || sr(e.payload))) {
                    let {message: t, imagePath: n} = cr(i, d, e);
                    _(t, J(n), void 0, void 0, f.UI_NOT_Effect)
                }
                if (t != null) {
                    We(!1), Xe(null), we(!1), De(!1), ke(!1), je(null), Se(null), U(null), Pe(!1), be.current != null && (clearTimeout(be.current), be.current = null), ye(null), _e(!1);
                    let e = {
                        candidates: t.candidates,
                        selected: t.selected,
                        stepId: t.stepId,
                        resolutionId: t.resolutionId,
                        inlineCasinoResolution: t.inlineCasinoResolution,
                        effectDisplayName: d?.displayName,
                        targetRandomStep: t.targetRandomStep ?? null
                    };
                    lt.current == null ? ot(e) : lt.current(e);
                    return
                }
                We(!1), Xe(null), we(!1), De(!1), ke(!1), je(null), Se(null), U(null), Pe(!1);
                let u = () => {
                    me(null), pe(!1), ye(null)
                }, S = d;
                S == null ? u() : (be.current != null && clearTimeout(be.current), ye(S), _e(!1), be.current = setTimeout(() => {
                    be.current = null, u()
                }, Vs)), (p === `gnus` || d?.name === `untargeted_gnus`) && l?.()
            } catch (e) {
                console.error(`WheelOverlay: accept failed`, e), e instanceof mt && _(e.description ?? e.message, J(`faces/angry.webp`), `Ошибка`), Pe(!1)
            }
        }, [W, ut, B?.data?.selected, B?.data?.kind, B?.data?.id, ft, pt, gt, i, r, a, c, _, l]),
        Lt = (0, Y.useCallback)(async (e, t, n) => {
            let r = B?.data;
            if (r != null && Nt != null && !Ft) {
                Xe({
                    value: e,
                    successfully: t,
                    selectedGenre: n
                }), Je(r.selected), it.current = Nt, Ze.current = {
                    rollId: r.id,
                    wheelKind: r.kind,
                    parentEffect: r.selected,
                    value: e,
                    successfully: t,
                    selectedGenre: n,
                    pendingRandomTargetConfirmAfterWheel: !0
                }, ct(r.selected.displayName), nt({
                    kind: `target-random`,
                    step: Nt,
                    inlineCasinoResolution: !0
                }), c?.(!0);
                return
            }
            if (Ft && Pt != null && r != null) {
                let i = Mt && At != null, a = !i && Nt != null;
                if (!i && !a) {
                    await It(e, t, void 0, n);
                    return
                }
                Xe({
                    value: e,
                    successfully: t,
                    selectedGenre: n
                }), Je(r.selected), a && (it.current = Nt), Ze.current = {
                    rollId: r.id,
                    wheelKind: r.kind,
                    parentEffect: r.selected,
                    value: e,
                    successfully: t,
                    selectedGenre: n,
                    pendingTargetPickAfterWheel: i,
                    pendingRandomTargetConfirmAfterWheel: a,
                    targetCandidatesSnapshot: i ? At ?? void 0 : void 0
                }, et({
                    candidates: Pt.candidates,
                    selected: Pt.selected,
                    rollId: r.id,
                    pendingTargetPickAfterWheel: i,
                    targetCandidatesSnapshot: i && At != null ? At : []
                });
                return
            }
            if (Mt) {
                Xe({value: e, successfully: t, selectedGenre: n}), r != null && Je(r.selected), Ke(At), We(!0);
                return
            }
            await It(e, t, void 0, n)
        }, [Ft, Mt, Nt, Pt, B?.data, At, It, c]);
    (0, Y.useEffect)(() => {
        o != null && (async () => {
            await Lt(o, void 0), s(null)
        })()
    }, [o, s, Lt]);
    let Rt = (0, Y.useCallback)(async () => {
            Ae == null || W || await Lt(void 0, void 0, Ae)
        }, [Ae, W, Lt]), zt = async () => {
            if (!(B == null || xe == null || W)) {
                if (xe === !1 && ft) {
                    await Lt(void 0, !1);
                    return
                }
                if (xe === !0 && ft) {
                    let e = Number(H);
                    if (H == null || H === `` || Number.isNaN(e) || e < 0) return;
                    await Lt(e, !0);
                    return
                }
                await Lt(void 0, pt ? xe : void 0)
            }
        }, Bt = B?.data != null, Vt = r?.casinoPhase === `regular`, Ht = (r?.platinumChips ?? 0) > 0,
        Ut = (r?.gnusAvailable ?? 0) > 0, Wt = !Ht && !Ut, Gt = Vt && !ue && !Bt && !Re && r != null,
        Kt = (0, Y.useCallback)(async () => {
            if (Gt) {
                ze(!0), He(!1);
                try {
                    let e = await G(), n = a == null ? null : await a();
                    Or(n?.id ?? r?.id);
                    let i = n ?? r;
                    if (i != null) {
                        let n = e ?? i.casinoPhase ?? null;
                        if (n === `done`) {
                            t();
                            return
                        }
                        w(xr({
                            casinoPhase: n,
                            platinumChips: i.platinumChips ?? 0,
                            gnusAvailable: i.gnusAvailable ?? 0,
                            role: i.role,
                            status: i.status
                        }))
                    }
                } catch (e) {
                    console.error(`WheelOverlay: finishRegularCasinoPhase failed`, e), _(e instanceof mt ? e.description ?? e.message : `Не удалось завершить фазу`, J(`faces/angry.webp`), `Ошибка`)
                } finally {
                    ze(!1)
                }
            }
        }, [Gt, a, r, _, t]), qt = (0, Y.useCallback)(() => {
            if (Gt) {
                if (d(f.Menu_Button_Back_Click), Ht || Wt) {
                    He(!0);
                    return
                }
                Kt()
            }
        }, [Gt, Ht, Wt, d, Kt]);
    (0, Y.useEffect)(() => {
        c?.(K != null || at != null)
    }, [K, at, c]);
    let Jt = (0, Y.useMemo)(() => r == null ? null : !Array.isArray(v) || v.length === 0 ? r : Ee(v, r) ?? r, [r, v]),
        Yt = nc(C, Jt?.effects, Jt?.activeEffects),
        Xt = C === `platinum` ? co : C === `judge` ? lo : C === `gnus` ? uo : Yt ? lo : so,
        Zt = C === `judge` || r?.role === `judge`, Qt = (0, Y.useCallback)(e => {
            let {spinSegments: t, winnerName: n} = e, r = t.findIndex(e => e.name === n), i = t.length,
                a = ee.current, {end: o, durationMs: s} = hc(a, r, i);
            ae.current != null && (cancelAnimationFrame(ae.current), ae.current = null), L.current = null, R(null), d(f.Casino_Wheel_Start);
            let c = e => 1 - (1 - Math.min(1, Math.max(0, e))) ** 2.8, l = performance.now();
            N(0), j(a), ee.current = a;
            let u = e => {
                let n = e - l, r = Math.min(1, n / s), d = c(r), f = a + (o - a) * d;
                ee.current = f, j(f);
                let p = mc(f, i);
                if (p !== L.current) {
                    L.current !== null && m(r);
                    let e = t[p];
                    R(e?.displayName ?? e?.name ?? null), L.current = p
                }
                r < 1 ? ae.current = requestAnimationFrame(u) : (ee.current = o, j(o), ae.current = null)
            };
            ae.current = requestAnimationFrame(u), window.setTimeout(() => {
                ae.current != null && (cancelAnimationFrame(ae.current), ae.current = null), de(!1), R(null), window.setTimeout(() => {
                    d(f.Casino_Wheel_End), pe(!0)
                }, Vs)
            }, s + 80)
        }, [d, m]), $t = (0, Y.useCallback)(() => {
            if (F.current || !P.current || re.current == null) return;
            F.current = !0;
            let e = re.current;
            (0, Xn.flushSync)(() => {
                me(e.pendingSlots), le(e.spinSegments)
            }), Qt(e)
        }, [le, Qt]), tn = (0, Y.useCallback)(() => {
            P.current = !0, $t()
        }, [$t]), nn = (0, Y.useCallback)(() => {
            nt(null), rt.current = null, it.current = null, ct(void 0), me(null), pe(!1), ye(null), _e(!1)
        }, []), rn = (0, Y.useCallback)(e => {
            let t = fc(e.candidates, e.selected);
            if (t.length === 0) return;
            be.current != null && (clearTimeout(be.current), be.current = null), ye(null), _e(!1), me(null), pe(!1);
            let n = {
                data: {
                    id: e.stepId,
                    candidates: e.candidates,
                    selected: e.selected,
                    kind: B?.data?.kind ?? C,
                    targets: null
                }
            };
            rt.current = {
                stepId: e.stepId,
                candidates: e.candidates,
                selected: e.selected,
                resolutionId: e.resolutionId,
                inlineCasinoResolution: e.inlineCasinoResolution,
                targetRandomStep: e.targetRandomStep ?? null
            }, it.current = e.targetRandomStep ?? null, nt({
                kind: `random-effect`,
                stepId: e.stepId,
                resolutionId: e.resolutionId,
                inlineCasinoResolution: e.inlineCasinoResolution
            }), e.effectDisplayName && ct(e.effectDisplayName), de(!0), pe(!1), P.current = !0, F.current = !1, re.current = {
                pendingSlots: n,
                spinSegments: t,
                winnerName: e.selected.name
            }, $t()
        }, [B?.data?.kind, C, $t]);
    lt.current = rn, (0, Y.useEffect)(() => {
        at != null && (rn(at), ot(null))
    }, [at, rn]), (0, Y.useEffect)(() => {
        if ($e == null) return;
        let {candidates: e, selected: t, rollId: n} = $e;
        rn({
            candidates: e,
            selected: t,
            stepId: n,
            inlineCasinoResolution: !0,
            effectDisplayName: Ze.current?.parentEffect.displayName,
            targetRandomStep: it.current
        }), et(null)
    }, [$e, rn]);
    let an = (0, Y.useCallback)(async () => {
        if (K?.kind !== `random-effect` || W || B == null) return;
        let e = K;
        Pe(!0), ut();
        let t = t => {
            me(null), pe(!1), ye(null), _e(!1);
            let n = t?.resolution;
            if (!e.inlineCasinoResolution && n?.status === `pending`) {
                let t = Ur(n.steps, n.nextStep ?? n.currentStep ?? null);
                if (t?.type === `target-random`) {
                    let n = {id: t.id, type: `target-random`, candidates: t.candidates, selected: t.selected};
                    nt({
                        kind: `target-random`,
                        resolutionId: e.resolutionId,
                        inlineCasinoResolution: e.inlineCasinoResolution,
                        step: n
                    });
                    return
                }
            }
            let r = it.current ?? rt.current?.targetRandomStep ?? null;
            if (r != null) {
                nt({
                    kind: `target-random`,
                    resolutionId: e.resolutionId,
                    inlineCasinoResolution: e.inlineCasinoResolution,
                    step: r
                });
                return
            }
            let i = Ze.current;
            if (e.inlineCasinoResolution && i?.pendingTargetPickAfterWheel && i.targetCandidatesSnapshot != null) {
                nn(), Ke(ci(i.parentEffect, si(Qe.current)) ?? i.targetCandidatesSnapshot), We(!0);
                return
            }
            if (e.inlineCasinoResolution && i != null) {
                i.pendingTargetPickAfterWheel === !0 || i.pendingRandomTargetConfirmAfterWheel === !0 || it.current != null || (Ze.current = null, It(i.value, i.successfully, i.targetPlayerId, i.selectedGenre, i.extraData));
                return
            }
            nn()
        };
        try {
            let n = null;
            if (!e.inlineCasinoResolution) {
                if (e.resolutionId == null) throw Error(`WheelOverlay: missing resolutionId for resolve`);
                n = await Me({
                    resolutionId: e.resolutionId,
                    step: e.stepId
                }), Or((a == null ? null : await a())?.id ?? r?.id)
            }
            let i = B.data?.selected;
            i == null ? t(n) : (be.current != null && clearTimeout(be.current), ye(i), _e(!1), be.current = setTimeout(() => {
                be.current = null, t(n)
            }, Vs))
        } catch (e) {
            console.error(`WheelOverlay: random-effect resolve failed`, e);
            let t = it.current;
            if (t != null && K?.kind === `random-effect`) {
                nt({
                    kind: `target-random`,
                    resolutionId: K.resolutionId,
                    inlineCasinoResolution: K.inlineCasinoResolution,
                    step: t
                });
                return
            }
            _(e instanceof mt ? e.description ?? e.message : `Не удалось выполнить действие`, J(`faces/angry.webp`), `Ошибка`)
        } finally {
            Pe(!1)
        }
    }, [K, W, ut, B, a, r?.id, nn, _, It]), on = (0, Y.useCallback)(async () => {
        if (K?.kind !== `target-random` || W) return;
        let e = K, t = Ze.current;
        if (t?.pendingRandomTargetConfirmAfterWheel) {
            Pe(!0);
            try {
                await It(t.value, t.successfully, void 0, t.selectedGenre, t.extraData), nn(), c?.(!1)
            } finally {
                Pe(!1)
            }
            return
        }
        if (t?.pendingTargetPickAfterWheel) {
            let n = (e.step.selected == null ? null : Jr(e.step.selected)) ?? t.targetPlayerId;
            if (n == null) return;
            Pe(!0);
            try {
                await It(t.value, t.successfully, n, t.selectedGenre, t.extraData), nn(), c?.(!1)
            } finally {
                Pe(!1)
            }
            return
        }
        nn(), Or((a == null ? null : await a())?.id ?? r?.id)
    }, [K, W, a, r?.id, nn, It, c]), sn = (0, Y.useCallback)(async () => {
        if (B == null || W) return;
        if (K?.kind === `random-effect`) {
            await an();
            return
        }
        if (gt && Ae == null) {
            ke(!0);
            return
        }
        if (pt || ft) {
            let e = e => Lt(e, !0);
            if (zs(Et?.name, e) || Ls(Et?.name, e)) return
        }
        if (pt && ft && (H == null || xe == null)) {
            we(!0);
            return
        }
        if (pt && !ft && xe == null) {
            we(!0);
            return
        }
        if (ft && H == null) {
            De(!0);
            return
        }
        if (Te && ft && pt && xe === !0) {
            let e = Number(H);
            if (!H || Number.isNaN(e) || e < 0) return;
            await Lt(e, !0);
            return
        }
        if (pt && ft && (xe == null || H == null) || pt && !ft && xe == null) return;
        let e = Number(H);
        ft && H != null && (!H || Number.isNaN(e) || e < 0) || ft && H == null || await Lt(ft ? e : void 0, pt ? xe ?? void 0 : void 0, gt ? Ae ?? void 0 : void 0)
    }, [B, W, K, an, Et?.name, pt, ft, gt, Ae, H, Te, xe, Lt]), cn = (0, Y.useCallback)(async () => {
        if (!(B?.data?.selected == null || B.data.selected.rerollAllowed !== !0 || W || ue || K != null)) {
            Pe(!0), d(f.Casino_Button_Click);
            try {
                await ce();
                let e = await vt();
                if (Or(r?.id), e?.data?.selected == null) return;
                let t = e.data.selected, n = fc(e.data.candidates, t);
                if (n.length === 0) return;
                be.current != null && (clearTimeout(be.current), be.current = null), ye(null), _e(!1), we(!1), De(!1), ke(!1), je(null), Se(null), U(null), de(!0), pe(!1), P.current = !0, F.current = !1, re.current = {
                    pendingSlots: e,
                    spinSegments: n,
                    winnerName: t.name
                }, $t()
            } catch (e) {
                console.error(`WheelOverlay: reroll failed`, e), e instanceof mt && _(e.description ?? e.message, J(`faces/angry.webp`), `Ошибка`)
            } finally {
                Pe(!1)
            }
        }
    }, [B?.data?.selected, W, ue, K, d, r?.id, $t, _]), ln = (0, Y.useCallback)(async e => {
        if (r == null || ue || T.length === 0 || Fe || K != null || B != null && B.data != null) return;
        let t = e ?? C, n = r;
        d(f.Casino_Button_Click), r.role === `judge` && (await tt(r, t), n = await a?.() ?? r);
        let i = Tr({
            casinoPhase: n.casinoPhase ?? null,
            platinumChips: n.platinumChips ?? 0,
            gnusAvailable: n.gnusAvailable ?? 0,
            role: n.role,
            status: n.status ?? `idle`
        }, t);
        de(!0), pe(!1), P.current = !1, re.current = null, F.current = !1, i !== C && (d(f.Casino_Wheel_Switch), I.current = i, w(i), z(i));
        let o = null;
        try {
            r.role === `judge` && i !== t && (await tt(n, i), n = await a?.() ?? n), await Ne({kind: i}), o = await vt()
        } catch (e) {
            if (console.error(`WheelOverlay: roll failed`, e), e instanceof mt) {
                let t = e.description ?? e.message;
                i === `gnus` && vr(S) && w(`regular`), _(t, J(`faces/angry.webp`), `Ошибка`)
            }
            de(!1), R(null);
            return
        }
        if (o?.data?.selected == null) {
            de(!1), R(null);
            return
        }
        let s = o.data.selected, c = fc(o.data.candidates, s);
        if (c.length === 0) {
            de(!1), R(null);
            return
        }
        re.current = {pendingSlots: o, spinSegments: c, winnerName: s.name}, $t()
    }, [r, S, ue, Fe, T, K, B, C, d, a, _, z, $t]), un = () => {
        ue || (d(f.Menu_Button_Back_Click), t())
    }, dn = (0, Y.useMemo)(() => K?.kind === `target-random` ? fi(K.step.candidates) : [], [K]), fn = () => {
        let e = [], t = r?.platinumChips ?? 0;
        t == 0 && e.push((0, X.jsx)(`div`, {className: `token-zero`, children: `0`}, `zero`));
        for (let n = 0; n < t; n++) e.push((0, X.jsx)(`div`, {
            className: `token-icon`,
            style: {left: `${n * 14}px`, zIndex: 100 - n}
        }, n));
        return e
    }, pn = () => {
        let e = [], t = r?.gnusAvailable ?? 0;
        t === 0 && e.push((0, X.jsx)(`div`, {className: `token-zero-gnus`, "aria-hidden": !0}, `zero`));
        for (let n = 0; n < t; n++) e.push((0, X.jsx)(`div`, {
            className: `token-icon token-icon--gnus`,
            style: {left: `${n * 14}px`, zIndex: 100 - n}
        }, n));
        return e
    }, mn = T.length, hn = mn > 0 ? sc(mn) : {r: 0, chordPx: 0}, gn = B?.data?.selected, _n = fe && !!gn && gn != null;
    E(Be), E(Ce), E(Te), E(Oe);
    let vn = te();
    k(`A`, () => {
        ln()
    }, e && !ue && !Bt && r != null && mn > 0 && wr(S) && !Fe && K == null && !_n && !vn), k(`B`, () => {
        un()
    }, e && !ue && K == null && !(B?.data != null && B.data.selected != null) && !vn), k(`X`, () => {
        sn()
    }, e && _n && !W && B?.data?.selected != null && !vn), k(`Y`, () => {
        qt()
    }, e && !Zt && Vt && Gt && !_n && !vn), k(`A`, () => {
        Re || Kt()
    }, Be && !Re), k(`B`, () => {
        Re || He(!1)
    }, Be && !Re);
    let yn = Number(H), bn = H != null && H !== `` && !Number.isNaN(yn) && yn > 0;
    k(`A`, () => {
        d(f.Casino_Button_Click), zt()
    }, Ce && !W && xe !== null && !(ft && xe === !0 && !bn)), k(`B`, () => {
        W || (we(!1), Se(null), U(null))
    }, Ce && !W), k(`A`, () => {
        d(f.Casino_Button_Click), sn()
    }, Te && !W && bn), k(`B`, () => {
        W || (De(!1), U(null))
    }, Te && !W);
    let xn = gn ?? ve, Sn = ve != null, wn = _n && he;
    (0, Y.useEffect)(() => {
        _n && (_e(!0), ye(null), be.current != null && (clearTimeout(be.current), be.current = null))
    }, [_n]), (0, Y.useEffect)(() => () => {
        be.current != null && clearTimeout(be.current)
    }, []);
    let Tn = ue, En = !wr(S) && !ue && K == null && at == null && !(B?.data != null && B.data.selected != null);
    return (0, Y.useEffect)(() => {
        if (!e) {
            u?.(!1);
            return
        }
        u?.(En)
    }, [e, En, u]), e ? (0, X.jsxs)(`div`, {
        className: `wheel`,
        role: `dialog`,
        "aria-modal": `true`,
        "aria-labelledby": `wheel-heading`,
        children: [(0, X.jsxs)(`div`, {
            className: `wheel-left ${Zt ? `wheel-left--judge` : ``}`,
            children: [(0, X.jsx)(`div`, {
                className: `dealer-text`,
                children: `Ваш крупье`
            }), (0, X.jsxs)(`div`, {
                className: `dealer-left-top`,
                children: [(0, X.jsx)(`img`, {
                    className: `dealer-image`,
                    src: J(Bs[C]),
                    alt: C
                }), !Zt && Vt && (0, X.jsxs)(`div`, {
                    className: `wheel-next-stage-container`,
                    children: [(0, X.jsxs)(`button`, {
                        type: `button`,
                        className: `wheel-next-stage`,
                        onClick: () => qt(),
                        disabled: !Gt,
                        title: Bt ? `Сначала примите выпавший эффект` : void 0,
                        onMouseEnter: () => Gt && d(f.Menu_Button_Participants_Hover),
                        children: [Wt ? `завершить` : `сменить`, Gt && (0, X.jsx)(q, {
                            button: `Y`,
                            className: `wheel-next-stage-gp-hint`
                        })]
                    }), (0, X.jsxs)(`div`, {
                        className: `wheel-next-stage-text`,
                        children: [`прокручено: `, r?.regularChipsSpentInTurn ?? 0]
                    })]
                })]
            }), !Zt && (0, X.jsxs)(`div`, {
                className: `wheel-left-tokens`,
                children: [(0, X.jsxs)(`div`, {
                    className: `premium`,
                    children: [(0, X.jsx)(`div`, {className: `premium-text`}), (0, X.jsx)(`span`, {
                        className: `tokens`,
                        children: fn()
                    })]
                }), (0, X.jsxs)(`div`, {
                    className: `gnus`,
                    children: [(0, X.jsx)(`div`, {className: `gnus-text`}), (0, X.jsx)(`span`, {
                        className: `tokens`,
                        children: pn()
                    })]
                })]
            })]
        }), (0, X.jsx)(`div`, {
            className: `wheel-panel`, children: (0, X.jsxs)(`div`, {
                className: `wheel-stage-shell`,
                children: [(0, X.jsx)(Zi, {
                    dealer: Xt,
                    isRolling: ue,
                    onRollDelayElapsed: tn
                }, Xt.rollAnimation.animation), (0, X.jsx)(`div`, {className: `wheel-stage-bg`}), Bt && fe && xn != null && (0, X.jsx)(oo, {
                    effect: xn,
                    renderDescription: uc,
                    waitingForAccept: W,
                    onAcceptButtonClick: () => void sn(),
                    onRerollButtonClick: xn.rerollAllowed === !0 ? () => void cn() : void 0,
                    isResultDismissing: Sn
                }, xn.name), (0, X.jsxs)(`div`, {
                    className: `wheel-stage`,
                    style: {opacity: +!wn, pointerEvents: wn ? `none` : `auto`},
                    children: [(0, X.jsx)(`div`, {
                        className: `wheel-disk-wrap`, children: (0, X.jsxs)(`div`, {
                            className: `wheel-disk`,
                            style: {
                                transform: `rotate(${O}deg)`,
                                transition: M > 0 ? `transform ${M}ms cubic-bezier(0.12, 0.72, 0.12, 1)` : `none`
                            },
                            children: [mn > 0 && Array.from({length: mn}, (e, t) => (0, X.jsx)(`div`, {
                                className: `wheel-segment`,
                                "aria-hidden": !0,
                                style: {clipPath: ia(t, mn)},
                                children: (0, X.jsx)(`div`, {className: `wheel-segment-pattern`, style: ra(t, mn)})
                            }, `wheel-segment-${t}`)), Array.from({length: mn}, (e, t) => {
                                let {offsetX: n, offsetY: r} = lc(t, mn);
                                return (0, X.jsx)(`img`, {
                                    className: `wheel-boundary-dot`,
                                    src: J(`casino/wheel-pin.webp`),
                                    alt: ``,
                                    "aria-hidden": !0,
                                    style: {
                                        left: `calc(50% + ${n}px)`,
                                        top: `calc(50% + ${r}px)`,
                                        width: `${Qs}px`,
                                        height: `${Qs}px`
                                    }
                                }, `wheel-boundary-dot-${t}`)
                            }), T.map((e, t) => {
                                let {chordPx: n, offsetX: r, offsetY: i, rotateDeg: a} = cc(t, mn, hn.r, hn.chordPx);
                                return (0, X.jsx)(`div`, {
                                    className: `wheel-label`,
                                    style: {
                                        left: `calc(50% + ${r}px)`,
                                        top: `calc(50% + ${i}px)`,
                                        width: `${Math.max(1, n)}px`,
                                        transform: `translate(-50%, -50%)`
                                    },
                                    children: (0, X.jsx)(`img`, {
                                        className: `wheel-label-img`,
                                        src: Yn(e.name, `static`),
                                        alt: e.displayName ?? e.name,
                                        style: {transform: `rotate(${a}deg)`}
                                    })
                                }, `${e.name}-${t}`)
                            })]
                        })
                    }), (0, X.jsxs)(`div`, {
                        className: `wheel-actions`,
                        children: [(0, X.jsx)(`div`, {className: `wheel-spin-wait`}), se != null && (0, X.jsx)(`div`, {
                            className: `wheel-spin-pointer-label`,
                            "aria-live": `polite`,
                            "aria-atomic": `true`,
                            children: se
                        }), Fe && !ue && !Bt && (0, X.jsxs)(X.Fragment, {
                            children: [(0, X.jsx)(`div`, {
                                className: `wheel-spin-waiting-block`,
                                "aria-hidden": !0
                            }), (0, X.jsx)(`div`, {
                                className: `wheel-spin-pointer-label wheel-spin-waiting-label`,
                                "aria-live": `polite`,
                                children: `Ожидание...`
                            })]
                        }), !ue && !Bt && !Fe && (0, X.jsx)(`button`, {
                            type: `button`,
                            className: `wheel-spin`,
                            style: {opacity: +!wn},
                            onClick: () => void ln(),
                            onMouseEnter: e => {
                                e.currentTarget.disabled || d(f.Casino_Button_Hover)
                            },
                            disabled: r == null || mn === 0 || Bt || !wr(S),
                            children: (0, X.jsx)(q, {button: `A`, className: `wheel-spin-gamepad-hint`})
                        })]
                    })]
                }), (0, X.jsx)(`div`, {className: `wheel-stage-outer-bg`}), (0, X.jsx)(`div`, {className: `wheel-stage-arrow`})]
            })
        }), _n && gn != null && (0, X.jsxs)(X.Fragment, {
            children: [Ce && (() => {
                let e = Number(H), t = H != null && H !== `` && !Number.isNaN(e) && e >= 0, n = !ft || xe !== !0,
                    r = W || xe === null || ft && xe === !0 && !t;
                return (0, X.jsx)(`div`, {
                    className: `success-question-overlay`,
                    role: `dialog`,
                    "aria-modal": `true`,
                    children: (0, X.jsxs)(`div`, {
                        className: `success-question-panel`,
                        children: [(0, X.jsxs)(`div`, {
                            className: `success-question-header`,
                            children: [(0, X.jsx)(`h2`, {
                                className: `success-question-title`,
                                children: dt().find(e => e.name === `successfully`)?.placeholder
                            }), (0, X.jsxs)(`button`, {
                                type: `button`,
                                className: `success-question-back`,
                                onClick: g(yt),
                                onMouseEnter: h,
                                disabled: W,
                                "aria-label": `Назад`,
                                children: [(0, X.jsx)(`span`, {
                                    className: `success-question-back-arrow`,
                                    "aria-hidden": !0
                                }), (0, X.jsx)(`span`, {children: `Назад`}), (0, X.jsx)(q, {
                                    button: `B`,
                                    className: `success-question-back-gp-hint`
                                })]
                            })]
                        }), (0, X.jsxs)(`div`, {
                            className: `success-question-choice`,
                            children: [(0, X.jsx)(`button`, {
                                type: `button`,
                                className: `success-question-card ${xe === !0 ? `success-question-card--selected` : ``}`,
                                onClick: () => Se(!0),
                                onMouseEnter: () => d(f.Casino_Button_Hover),
                                children: `Да`
                            }), (0, X.jsx)(`button`, {
                                type: `button`,
                                className: `success-question-card ${xe === !1 ? `success-question-card--selected` : ``}`,
                                onClick: () => {
                                    Se(!1), ft && U(null)
                                },
                                onMouseEnter: () => d(f.Casino_Button_Hover),
                                children: `Нет`
                            })]
                        }), ft && (0, X.jsx)(`div`, {
                            className: `success-question-input-row`,
                            children: (0, X.jsx)(`input`, {
                                className: `success-question-input`,
                                type: `number`,
                                min: 0,
                                value: n ? `` : H ?? ``,
                                disabled: n,
                                onChange: e => U(e.target.value),
                                onKeyDown: e => Fr(e, p),
                                placeholder: dt().find(e => e.name === `value`)?.placeholder ?? `Количество`
                            })
                        }), (0, X.jsx)(`div`, {
                            className: `success-question-actions`,
                            children: (0, X.jsxs)(`button`, {
                                type: `button`,
                                className: `success-question-confirm ${r ? `success-question-confirm--disabled` : ``}`,
                                disabled: r,
                                onClick: () => {
                                    d(f.Casino_Button_Click), zt()
                                },
                                onMouseEnter: () => {
                                    r || d(f.Casino_Button_Hover)
                                },
                                children: [!W && (0, X.jsx)(q, {
                                    button: `A`,
                                    className: `success-question-confirm-gp-hint`
                                }), W ? `Загрузка` : `Подтвердить`]
                            })
                        })]
                    })
                })
            })(), (0, X.jsx)(is, {
                isOpen: Oe,
                genreOptions: _t,
                placeholder: ht?.placeholder ?? `Выберите жанр`,
                submitting: W,
                onClose: bt,
                onRevealComplete: je,
                onConfirm: () => {
                    Rt()
                }
            }), Te && (() => {
                let e = dt().find(e => e.name === `value`), t = Number(H),
                    n = H != null && H !== `` && !Number.isNaN(t) && t >= 0, r = W || !n;
                return (0, X.jsx)(`div`, {
                    className: `success-question-overlay`,
                    role: `dialog`,
                    "aria-modal": `true`,
                    children: (0, X.jsxs)(`div`, {
                        className: `success-question-panel`,
                        children: [(0, X.jsxs)(`div`, {
                            className: `success-question-header`,
                            children: [(0, X.jsx)(`h2`, {
                                className: `success-question-title`,
                                children: e?.placeholder ?? `Введите количество`
                            }), (0, X.jsxs)(`button`, {
                                type: `button`,
                                className: `success-question-back`,
                                onClick: g(xt),
                                onMouseEnter: h,
                                disabled: W,
                                "aria-label": `Назад`,
                                children: [(0, X.jsx)(`span`, {
                                    className: `success-question-back-arrow`,
                                    "aria-hidden": !0
                                }), (0, X.jsx)(`span`, {children: `Назад`}), (0, X.jsx)(q, {
                                    button: `B`,
                                    className: `success-question-back-gp-hint`
                                })]
                            })]
                        }), (0, X.jsx)(`div`, {
                            className: `success-question-input-row`,
                            children: (0, X.jsx)(`input`, {
                                className: `success-question-input`,
                                type: `number`,
                                min: 0,
                                value: H ?? ``,
                                autoFocus: !0,
                                onChange: e => U(e.target.value),
                                onKeyDown: e => {
                                    Fr(e, p), e.key === `Enter` && n && (d(f.Casino_Button_Click), sn())
                                },
                                placeholder: e?.placeholder ?? `Количество`
                            })
                        }), (0, X.jsx)(`div`, {
                            className: `success-question-actions`,
                            children: (0, X.jsxs)(`button`, {
                                type: `button`,
                                className: `success-question-confirm ${r ? `success-question-confirm--disabled` : ``}`,
                                disabled: r,
                                onClick: () => {
                                    d(f.Casino_Button_Click), sn()
                                },
                                onMouseEnter: () => {
                                    r || d(f.Casino_Button_Hover)
                                },
                                children: [!W && (0, X.jsx)(q, {
                                    button: `A`,
                                    className: `success-question-confirm-gp-hint`
                                }), W ? `Загрузка` : `Подтвердить`]
                            })
                        })]
                    })
                })
            })()]
        }), En && (0, X.jsxs)(`div`, {
            className: `casino-closed-overlay`,
            "aria-hidden": !1,
            children: [(0, X.jsx)(`div`, {className: `casino-closed-overlay-dim`}), (0, X.jsx)(`img`, {
                className: `casino-closed-overlay-image`,
                src: J(`casino/closed.webp`),
                alt: ``
            })]
        }), (0, X.jsxs)(`div`, {
            className: `wheel-leave-btn`,
            style: {visibility: Tn ? `hidden` : `visible`},
            onMouseEnter: () => d(f.Menu_Button_Participants_Hover),
            onClick: un,
            children: [(0, X.jsxs)(`div`, {
                className: `wheel-right-top-text`,
                children: [`УЙТИ`, (0, X.jsx)(q, {button: `B`, className: `wheel-leave-gp-hint`})]
            }), (0, X.jsx)(`div`, {className: `wheel-right-top-close`})]
        }), (0, X.jsx)(`div`, {
            className: `wheel-right ${Zt ? `wheel-right--judge` : ``}`,
            children: Zt ? (0, X.jsx)(`div`, {
                className: `wheel-right-center wheel-right-center--judge`,
                children: (0, X.jsxs)(`div`, {
                    className: `wheel-right-center-text`,
                    children: [(0, X.jsx)(`div`, {
                        className: `wheel-right-center-text-title`,
                        children: `Всего покручено`
                    }), (0, X.jsx)(`div`, {className: `wheel-right-center-text-number`, children: r?.rollsTotal ?? 0})]
                })
            }) : (0, X.jsxs)(X.Fragment, {
                children: [(0, X.jsxs)(`div`, {
                    className: `wheel-right-center`,
                    children: [(0, X.jsxs)(`div`, {
                        className: `wheel-right-center-text`,
                        children: [(0, X.jsx)(`div`, {
                            className: `wheel-right-center-text-title`,
                            children: `Всего прокручено`
                        }), (0, X.jsx)(`div`, {
                            className: `wheel-right-center-text-number`,
                            children: r?.rollsTotal ?? 0
                        })]
                    }), (0, X.jsxs)(`div`, {
                        className: `wheel-right-center-text`,
                        children: [(0, X.jsx)(`div`, {
                            className: `wheel-right-center-text-title`,
                            children: `Премиум`
                        }), (0, X.jsx)(`div`, {
                            className: `wheel-right-center-text-number`,
                            children: r?.rollsPlatinum ?? 0
                        })]
                    }), (0, X.jsxs)(`div`, {
                        className: `wheel-right-center-text`,
                        children: [(0, X.jsx)(`div`, {
                            className: `wheel-right-center-text-title`,
                            children: `Обычный`
                        }), (0, X.jsx)(`div`, {
                            className: `wheel-right-center-text-number`,
                            children: r?.rollsRegular ?? 0
                        })]
                    }), (0, X.jsxs)(`div`, {
                        className: `wheel-right-center-text`,
                        children: [(0, X.jsx)(`div`, {
                            className: `wheel-right-center-text-title`,
                            children: `Гнусный`
                        }), (0, X.jsx)(`div`, {
                            className: `wheel-right-center-text-number`,
                            children: r?.rollsGnus ?? 0
                        })]
                    }), (0, X.jsxs)(`div`, {
                        className: `wheel-right-center-text`,
                        children: [(0, X.jsx)(`div`, {
                            className: `wheel-right-center-text-title`,
                            children: `Положительные роллы`
                        }), (0, X.jsx)(`div`, {
                            className: `wheel-right-center-text-number`,
                            children: r?.rollsPositive ?? 0
                        })]
                    }), (0, X.jsxs)(`div`, {
                        className: `wheel-right-center-text`,
                        children: [(0, X.jsx)(`div`, {
                            className: `wheel-right-center-text-title`,
                            children: `Отрицательные роллы`
                        }), (0, X.jsx)(`div`, {
                            className: `wheel-right-center-text-number`,
                            children: r?.rollsNegative ?? 0
                        })]
                    }), (0, X.jsxs)(`div`, {
                        className: `wheel-right-center-text`,
                        children: [(0, X.jsx)(`div`, {
                            className: `wheel-right-center-text-title`,
                            children: `Нейтральные роллы`
                        }), (0, X.jsx)(`div`, {
                            className: `wheel-right-center-text-number`,
                            children: r?.rollsNeutral ?? 0
                        })]
                    })]
                }), (0, X.jsx)(`div`, {
                    className: `wheel-right-bottom`,
                    children: (0, X.jsxs)(`div`, {
                        className: `wheel-right-bottom-balance`,
                        children: [`Очки: `, r?.ggp]
                    })
                })]
            })
        }), Be && (0, X.jsx)(`div`, {
            className: `platinum-switch-confirm-overlay`, role: `presentation`, onClick: () => {
                Re || He(!1)
            }, children: (0, X.jsxs)(`div`, {
                className: `platinum-switch-confirm-panel`,
                role: `dialog`,
                "aria-modal": `true`,
                "aria-labelledby": `wheel-platinum-switch-title`,
                onClick: e => e.stopPropagation(),
                children: [(0, X.jsxs)(`div`, {
                    className: `platinum-switch-confirm-header`,
                    children: [(0, X.jsx)(`h3`, {
                        id: `wheel-platinum-switch-title`,
                        className: `platinum-switch-confirm-title`,
                        children: Wt ? ac : rc
                    }), (0, X.jsxs)(`button`, {
                        type: `button`,
                        className: `platinum-switch-confirm-back`,
                        onClick: g(() => He(!1)),
                        onMouseEnter: h,
                        disabled: Re,
                        "aria-label": `Назад`,
                        children: [(0, X.jsx)(`span`, {
                            className: `platinum-switch-confirm-back-arrow`,
                            "aria-hidden": !0
                        }), (0, X.jsx)(`span`, {children: `Назад`}), (0, X.jsx)(q, {
                            button: `B`,
                            className: `platinum-switch-back-gp-hint`
                        })]
                    })]
                }), Wt ? (0, X.jsx)(`p`, {
                    className: `platinum-switch-confirm-text`,
                    children: oc
                }) : (0, X.jsxs)(X.Fragment, {
                    children: [(0, X.jsxs)(`div`, {
                        className: `platinum-switch-confirm-body`,
                        children: [(0, X.jsxs)(`p`, {children: [`Переход на премиум-колесо `, (0, X.jsx)(`strong`, {children: `НЕОБРАТИМ`}), `.`]}), (0, X.jsxs)(`p`, {children: [`После подтверждения обычное колесо в этом ходу будет`, ` `, (0, X.jsx)(`strong`, {children: `НЕДОСТУПНО`}), `.`]}), (0, X.jsx)(`p`, {children: `Это необходимо, чтобы негативные эффекты обычного колеса не могли испортить позитивные эффекты, полученные на премиум-колесе.`})]
                    }), (0, X.jsx)(`div`, {
                        className: `platinum-switch-confirm-divider`,
                        "aria-hidden": !0
                    }), (0, X.jsxs)(`div`, {
                        className: `platinum-switch-confirm-warning`,
                        children: [(0, X.jsx)(`p`, {
                            className: `platinum-switch-confirm-warning-text`,
                            children: ic
                        }), (0, X.jsx)(`img`, {
                            src: J(`casino/alert.png`),
                            alt: ``,
                            className: `platinum-switch-confirm-alert`,
                            "aria-hidden": !0
                        })]
                    })]
                }), (0, X.jsx)(`div`, {
                    className: `platinum-switch-confirm-actions`,
                    children: (0, X.jsxs)(`button`, {
                        type: `button`,
                        className: `platinum-switch-confirm-btn ${Re ? `platinum-switch-confirm-btn--disabled` : ``}`,
                        disabled: Re,
                        onClick: () => {
                            Kt()
                        },
                        onMouseEnter: () => {
                            Re || d(f.Casino_Button_Hover)
                        },
                        children: [!Re && (0, X.jsx)(q, {
                            button: `A`,
                            className: `platinum-switch-confirm-gp-hint`
                        }), Re ? Wt ? `Завершение…` : `Смена…` : Wt ? `Завершить` : `Сменить`]
                    })
                })]
            })
        }), Ue && jt != null && (0, X.jsx)(Ai, {
            isOpen: Ue,
            targets: jt,
            effectDisplayName: (qe ?? Tt)?.displayName,
            effectName: (qe ?? Tt)?.name,
            submitting: W,
            variant: `centered`,
            multiSelect: ri(Et) || Dt != null,
            minSelections: Dt?.minItems ?? 0,
            maxSelections: Dt?.maxItems,
            selectionPlaceholder: Dt?.placeholder,
            onClose: () => {
                W || (We(!1), Ke(null), Xe(null), Ze.current = null, Je(null))
            },
            onConfirm: e => {
                It(Ye?.value, Ye?.successfully, e, Ye?.selectedGenre), We(!1), Ke(null), Xe(null), Je(null)
            },
            onConfirmMulti: e => {
                Dt && (It(Ye?.value, Ye?.successfully, void 0, Ye?.selectedGenre, {[Dt.name]: e}), We(!1), Ke(null), Xe(null), Je(null))
            }
        }), K?.kind === `target-random` && (0, X.jsx)(Li, {
            isOpen: !0,
            targets: dn,
            predeterminedPlayerId: K.step.selected == null ? null : Jr(K.step.selected),
            predeterminedPlayerName: typeof K.step.selected?.name == `string` ? K.step.selected.name : null,
            effectDisplayName: st,
            showBackButton: !1,
            onClose: () => {
            },
            onConfirm: on
        })]
    }) : null
};

function _c() {
    let [e, t] = (0, Y.useState)(() => Ie(localStorage.getItem(`settings_colorScheme`)));
    return (0, Y.useEffect)(() => {
        let e = () => {
            t(Ie(localStorage.getItem(`settings_colorScheme`)))
        };
        window.addEventListener(`storage`, e);
        let n = () => {
            t(Ie(localStorage.getItem(`settings_colorScheme`)))
        };
        window.addEventListener(`colorSchemeChanged`, n);
        let r = () => {
            t(document.body.classList.contains(`lighttheme`) ? `lighttheme` : qe)
        };
        r();
        let i = new MutationObserver(r);
        return i.observe(document.body, {attributes: !0, attributeFilter: [`class`]}), () => {
            window.removeEventListener(`storage`, e), window.removeEventListener(`colorSchemeChanged`, n), i.disconnect()
        }
    }, []), e
}

var vc = ({width: e = `100%`, height: t = `1rem`, className: n = ``, variant: r = `rect`}) => {
    let i = _c(), a = {width: typeof e == `number` ? `${e}px` : e, height: typeof t == `number` ? `${t}px` : t};
    return (0, X.jsx)(`div`, {
        className: `skeleton skeleton-${r} ${i === `lighttheme` ? `skeleton-light` : ``} ${n}`,
        style: a,
        children: (0, X.jsx)(`div`, {className: `skeleton-shimmer`})
    })
}, yc = ({
             imageUrl: e,
             skeletonWidth: t = `100%`,
             skeletonHeight: n = `100%`,
             skeletonVariant: r = `rect`,
             showSkeleton: i = !0,
             className: a = ``,
             backgroundSize: o = `cover`,
             backgroundPosition: s = `center`,
             style: c,
             ...l
         }) => {
    let [u, d] = (0, Y.useState)(() => !!e), [f, p] = (0, Y.useState)(() => !e), [m, h] = (0, Y.useState)(() => null);
    (0, Y.useEffect)(() => {
        if (!e) {
            let e = setTimeout(() => {
                d(!1), p(!0), h(null)
            }, 0);
            return () => clearTimeout(e)
        }
        let t = !1, n = new Image, r = !1;
        n.onload = () => {
            t || (r = !0, d(!1), p(!1), h(e))
        }, n.onerror = () => {
            t || (d(!1), p(!0), h(null))
        }, n.src = e;
        let i = setTimeout(() => {
            t || (p(!1), h(null), !r && !n.complete ? d(!0) : n.complete && n.naturalWidth > 0 && (d(!1), p(!1), h(e)))
        }, 0);
        return () => {
            t = !0, clearTimeout(i)
        }
    }, [e]);
    let g = e => `url("${e.replace(/\\/g, `\\\\`).replace(/"/g, `\\"`)}")`, _ = {
        ...c,
        backgroundImage: m ? g(m) : void 0,
        backgroundSize: o,
        backgroundPosition: s,
        backgroundRepeat: `no-repeat`
    };
    return (0, X.jsxs)(`div`, {
        ...l,
        className: `background-image-with-skeleton ${a}`,
        style: _,
        children: [u && i && (0, X.jsx)(vc, {
            width: t,
            height: n,
            variant: r,
            className: `background-image-skeleton-overlay`
        }), f && !m && (0, X.jsx)(`div`, {
            className: `background-image-error`,
            children: (0, X.jsx)(`span`, {className: `background-image-error-text`, children: `?`})
        })]
    })
}, bc = ({
             src: e,
             alt: t = ``,
             className: n = ``,
             skeletonWidth: r,
             skeletonHeight: i,
             skeletonVariant: a = `rect`,
             showSkeleton: o = !0,
             ...s
         }) => {
    let [c, l] = (0, Y.useState)(() => !!e), [u, d] = (0, Y.useState)(!1), [f, p] = (0, Y.useState)(e);
    if ((0, Y.useEffect)(() => {
        if (!e) {
            let e = setTimeout(() => {
                l(!1), d(!0), p(void 0)
            }, 0);
            return () => clearTimeout(e)
        }
        let t = !1, n = new Image, r = !1;
        n.onload = () => {
            t || (r = !0, l(!1), d(!1))
        }, n.onerror = () => {
            t || (l(!1), d(!0))
        }, n.src = e;
        let i = setTimeout(() => {
            t || (p(e), d(!1), !r && !(n.complete && n.naturalWidth > 0) ? l(!0) : (l(!1), d(!1)))
        }, 0);
        return () => {
            t = !0, clearTimeout(i)
        }
    }, [e]), u && !f) return (0, X.jsx)(`div`, {
        className: `image-skeleton-error ${n}`,
        style: {width: r, height: i},
        children: (0, X.jsx)(`span`, {className: `image-skeleton-error-text`, children: `?`})
    });
    let {className: m, ...h} = s, g = `image-with-skeleton-img ${c ? `image-loading` : ``} ${m || ``}`.trim();
    return (0, X.jsxs)(`div`, {
        className: `image-with-skeleton ${n}`,
        children: [c && o && (0, X.jsx)(vc, {
            width: r,
            height: i,
            variant: a,
            className: `image-skeleton-overlay`
        }), f && (0, X.jsx)(`img`, {
            ...h, src: f, alt: t, className: g, onLoad: () => {
                l(!1), d(!1)
            }, onError: () => {
                l(!1), d(!0)
            }
        })]
    })
}, xc = 10, Sc = 1200, Cc = 250, wc = 32, Tc = 248;

function Ec(e) {
    return Array.isArray(e) ? e.join(`
`) : e
}

function Dc(e, t) {
    let n = Array.isArray(e) ? e : [e];
    return (0, X.jsxs)(X.Fragment, {
        children: [n.map((e, t) => (0, X.jsxs)(`span`, {children: [e, t < n.length - 1 && (0, X.jsx)(`br`, {})]}, t)), t !== void 0 && (0, X.jsxs)(X.Fragment, {
            children: [(0, X.jsx)(`br`, {}), (0, X.jsxs)(`span`, {
                className: `inventory-effect-hover__cooldown`,
                children: [`Перезарядка: `, t]
            })]
        })]
    })
}

var Oc = 3e4;

function kc({description: e, cooldownLabel: t, measureKey: n}) {
    let r = (0, Y.useRef)(null), [i, a] = (0, Y.useState)(null);
    return (0, Y.useLayoutEffect)(() => {
        let e = r.current;
        if (!e) return;
        let t = () => {
            a(Math.min(e.scrollHeight, Tc))
        };
        t();
        let n = new ResizeObserver(t);
        return n.observe(e), () => n.disconnect()
    }, [n]), (0, X.jsx)(`div`, {
        className: [`inventory-effect-hover__description-scroll-host`, i == null ? `inventory-effect-hover__description-scroll-host--measuring` : void 0].filter(Boolean).join(` `),
        style: i == null ? void 0 : {height: i},
        children: (0, X.jsx)(ao, {
            className: `inventory-effect-hover__description-scroll`,
            children: (0, X.jsx)(`p`, {ref: r, className: `inventory-effect-hover__description`, children: Dc(e, t)})
        })
    })
}

function Ac({data: e, embedded: t = !1}) {
    let n = e.selectedGenreLabel != null, r = e.blockedEffects.length > 0, i = e.allowedStatusDisplays.length > 0,
        a = (0, Y.useRef)(null), o = (0, Y.useRef)(null), s = (0, Y.useRef)(0), [, c] = (0, Y.useState)(0),
        l = Ec(e.description), u = `${l}:${e.cooldownLabel ?? ``}:${e.expiresAt ?? ``}`,
        d = e.expiresAt ? lt(e.expiresAt) : void 0, f = d !== void 0, p = n || r || i || f;
    return (0, Y.useEffect)(() => {
        if (!e.expiresAt) return;
        let t = window.setInterval(() => c(e => e + 1), Oc);
        return () => window.clearInterval(t)
    }, [e.expiresAt]), (0, Y.useEffect)(() => {
        if (t) {
            s.current = 0;
            let e = o.current, t = a.current;
            e && (e.style.transform = ``), t && (t.scrollTop = 0);
            return
        }
        s.current = 0;
        let e = o.current;
        e && (e.style.transform = `translate3d(0, 0, 0)`);
        let n = 0, r = null, i = 1, c = performance.now() + Cc, l = e => {
            let t = a.current, u = o.current;
            if (t && u) {
                let n = Math.max(0, u.offsetHeight - t.clientHeight);
                if (n > 1 && e >= c) {
                    r === null && (r = e);
                    let t = Math.min(e - r, wc);
                    r = e;
                    let a = s.current + i * xc * t / 1e3;
                    a >= n ? (a = n, i = -1, c = e + Sc) : a <= 0 && (a = 0, i = 1, c = e + Sc), s.current = a, u.style.transform = `translate3d(0, ${-a}px, 0)`
                } else r = e
            }
            n = requestAnimationFrame(l)
        };
        return n = requestAnimationFrame(l), () => {
            cancelAnimationFrame(n)
        }
    }, [t, e.effectName, l, e.cooldownLabel, f]), (0, X.jsx)(`div`, {
        className: `inventory-effect-hover${t ? ` inventory-effect-hover--embedded` : ``}`,
        role: t ? `group` : `tooltip`,
        "aria-label": e.displayName,
        children: (0, X.jsxs)(`div`, {
            className: `inventory-effect-hover__main`,
            children: [(0, X.jsx)(`div`, {
                className: `inventory-effect-hover__image`,
                children: (0, X.jsx)(yc, {
                    imageUrl: Yn(e.effectName, `animated`),
                    className: `inventory-effect-hover__image-inner`,
                    skeletonWidth: `100%`,
                    skeletonHeight: `100%`,
                    skeletonVariant: `rect`,
                    backgroundSize: `contain`,
                    showSkeleton: !1
                })
            }), (0, X.jsxs)(`div`, {
                className: `inventory-effect-hover__info`,
                children: [(0, X.jsxs)(`div`, {
                    className: `inventory-effect-hover__title-block`,
                    children: [(0, X.jsx)(`h3`, {
                        className: `inventory-effect-hover__title`,
                        children: e.displayName
                    }), e.isGlobalTarget && (0, X.jsx)(`span`, {
                        className: `inventory-effect-hover__tag`,
                        children: `Глобальное`
                    })]
                }), t ? (0, X.jsx)(kc, {
                    description: e.description,
                    cooldownLabel: e.cooldownLabel,
                    measureKey: u
                }) : (0, X.jsx)(`div`, {
                    ref: a,
                    className: `inventory-effect-hover__description-scroll`,
                    children: (0, X.jsx)(`div`, {
                        ref: o,
                        className: `inventory-effect-hover__description-track`,
                        children: (0, X.jsx)(`p`, {
                            className: `inventory-effect-hover__description`,
                            children: Dc(e.description, e.cooldownLabel)
                        })
                    })
                }), p && (0, X.jsxs)(`div`, {
                    className: `inventory-effect-hover__footer`,
                    children: [(0, X.jsxs)(`div`, {
                        className: `inventory-effect-hover__blocked`,
                        children: [n && (0, X.jsxs)(`div`, {
                            className: `inventory-effect-hover__genre`,
                            children: [(0, X.jsx)(`span`, {
                                className: `inventory-effect-hover__genre-label`,
                                children: `Выбранный Жанр`
                            }), (0, X.jsx)(`span`, {
                                className: `inventory-effect-hover__genre-value`,
                                children: e.selectedGenreLabel
                            })]
                        }), r && (0, X.jsxs)(X.Fragment, {
                            children: [(0, X.jsx)(`span`, {
                                className: `inventory-effect-hover__blocked-label`,
                                children: `Блокирует`
                            }), (0, X.jsx)(`div`, {
                                className: `inventory-effect-hover__blocked-images`,
                                children: e.blockedEffects.map(e => (0, X.jsx)(bc, {
                                    src: e.imageUrl,
                                    alt: e.displayName,
                                    className: `inventory-effect-hover__blocked-image`,
                                    title: e.displayName,
                                    skeletonWidth: `100%`,
                                    skeletonHeight: `100%`,
                                    skeletonVariant: `rect`
                                }, e.name))
                            })]
                        })]
                    }), (0, X.jsxs)(`div`, {
                        className: `inventory-effect-hover__usage`,
                        children: [f && (0, X.jsx)(`div`, {
                            className: `inventory-effect-hover__expiry-row`,
                            children: (0, X.jsx)(`span`, {children: d})
                        }), i && (0, X.jsx)(`div`, {
                            className: `inventory-effect-hover__usage-rows`,
                            children: e.allowedStatusDisplays.map(e => (0, X.jsxs)(`div`, {
                                className: `inventory-effect-hover__usage-row`,
                                children: [(0, X.jsx)(`img`, {
                                    src: e.iconUrl,
                                    alt: ``,
                                    className: `inventory-effect-hover__usage-icon`
                                }), (0, X.jsx)(`span`, {children: e.label})]
                            }, `${e.iconUrl}-${e.label}`))
                        })]
                    })]
                })]
            })]
        })
    })
}

function jc(e) {
    return e?.length ? typeof e[0] == `string` ? e : e.map(e => e.effectName) : []
}

function Mc(e, t) {
    return e.activationConflictEffects?.length ? e.activationConflictEffects : e.conflictEffects?.length ? e.conflictEffects : t?.find(t => t.name === e.name)?.activationConflictEffects ?? null
}

function Nc(e, t, n) {
    let r = Mc(e, n);
    return r?.length ? typeof r[0] == `string` ? r.includes(t) : r.some(e => e.effectName === t) : !1
}

function Pc(e, t, n) {
    let r = Mc(e, n);
    return r?.length ? typeof r[0] == `string` ? r.includes(t) : r.some(e => e.effectName === t && e.mode === `mutual`) : !1
}

function Fc(e, t, n) {
    return Nc(e, t.name, n) ? !0 : Pc(t, e.name, n)
}

function Ic(e) {
    if (e.currentAuctionResult?.type === `movie`) return !0;
    let t = e.currentGame?.data;
    return t != null && `name` in t && !(`id` in t)
}

function Lc(e) {
    let t = e.currentGame?.data;
    if (t && `name` in t && typeof t.name == `string`) return t.name;
    let n = e.currentAuctionResult?.title;
    return typeof n == `string` && n.length > 0 ? n : `Неизвестно`
}

function Rc(e) {
    return e.status === `casino` ? `в казино` : e.status === `idle` ? `аукцион` : e.status === `in_game` ? `в игре: ${Lc(e)}` : e.status === `movie` || e.status === `content` && Ic(e) ? `смотрит: ${Lc(e)}` : e.status === `content` ? `в игре: ${Lc(e)}` : `аукцион`
}

function zc(e) {
    return e.status === `in_game` || e.status === `content` || e.status === `movie` ? Lc(e) : null
}

function Bc(e) {
    let t = e.allowedStatuses;
    if (!t?.length) return [];
    let n = [];
    for (let r of t) {
        if (r === `idle`) {
            n.push({iconUrl: J(`icons/idle.svg`), label: `Использовать во время аукциона`});
            continue
        }
        if (r === `content` || r === `in_game` || r === `casino`) {
            if (e.contentStartPolicy?.contentType === `movie`) continue;
            n.push({iconUrl: J(`icons/in_game.svg`), label: `Использовать во время игры`})
        }
    }
    return n
}

var Vc = [`positive`, `neutral`, `negative`, `gnus`, `judge`],
    Hc = {judge: `Судейское`, gnus: `Гнусность`, positive: `Позитив`, negative: `Негатив`, neutral: `Нейтрал`};

function Uc(e) {
    return e.ownerRole === `judge` ? `judge` : e.pools?.includes(`gnus`) ? `gnus` : e.impact
}

function Wc(e) {
    let t = new Map;
    for (let n of e) {
        let e = Uc(n), r = t.get(e) ?? [];
        r.push(n), t.set(e, r)
    }
    return Vc.filter(e => t.has(e)).map(e => ({
        id: e,
        title: Hc[e],
        effects: t.get(e).slice().sort((e, t) => e.displayName.localeCompare(t.displayName, `ru`))
    }))
}

function Gc(e) {
    return Wc(e)[0]?.effects[0] ?? null
}

function Kc(e) {
    let t = e;
    return {
        effectName: e.name,
        displayName: e.displayName,
        description: e.description,
        effect: t,
        blockedEffects: [],
        allowedStatusDisplays: Bc(t),
        isGlobalTarget: ti(t)
    }
}

function qc(e) {
    return e.name?.trim() || e.displayName?.trim() || `Игрок`
}

function Jc(e, t) {
    if (!t || !e?.length) return null;
    let n = e.find(e => e.id === t);
    return n ? qc(n) : null
}

function Yc(e) {
    if (typeof e != `object` || !e) return null;
    let t = e;
    for (let e of [`sourcePlayerName`, `sourcePlayerNick`, `sourceName`, `playerName`]) {
        let n = t[e];
        if (typeof n == `string` && n.trim().length > 0) return n.trim()
    }
    return null
}

async function Xc(e, t, n) {
    if (!e) return null;
    let r = Yc(n);
    if (r) return r;
    let i = Jc(t, e);
    if (i) return i;
    let a = await de(e);
    return a ? qc(a) : null
}

var Zc = {gamemaster: cn.gamemaster}, Qc = new Set(Object.values(Zc)), $c = {[Zc.gamemaster]: Ps};

function el(e) {
    return typeof e == `object` && !!e && e.type === `effect-input-numeric-map`
}

function tl(e) {
    return e != null && Qc.has(e)
}

function nl(e) {
    return e != null && tl(e.effectName) && el(e.pendingContext)
}

function rl(e) {
    return el(e.pendingContext) && typeof e.pendingContext.field == `string` && e.pendingContext.field ? e.pendingContext.field : `value`
}

function il(e, t) {
    return {data: {[t]: e}}
}

function al(e, t) {
    return nl(e) ? (Zt(t, () => $c[e.effectName]()), !0) : !1
}

function ol(e) {
    return typeof e == `object` && !!e && e.type === `effect-input-numeric-map`
}

function sl(e, t, n, r) {
    if (typeof r.ggpDelta == `number` && r.ggpDelta !== 0 && e(r.ggpDelta > 0 ? ht(r.ggpDelta) : He(r.ggpDelta), r.ggpDelta > 0 ? J(`faces/like.webp`) : J(`faces/dislike.webp`), n), r.resolution.status === `completed`) {
        let {message: r, imagePath: i} = dr({name: t, displayName: n});
        e(r, J(i), `Готово`)
    }
}

var cl = ({isOpen: e, onClose: t, playerId: r, effects: i = [], playersList: a = [], onResolved: o}) => {
    let {play: s} = oe(), {onBackHover: c, withBackClick: l} = n(), u = Ri(), {showNotification: d} = Cn(),
        p = Fe(), {data: m} = as({enabled: e}),
        h = (0, Y.useMemo)(() => a.length > 0 ? a : m ?? [], [a, m]), [g, _] = (0, Y.useState)(!1), [v, y] = (0, Y.useState)(!1), [b, x] = (0, Y.useState)([]), [S, C] = (0, Y.useState)({}), [w, T] = (0, Y.useState)(null), [D, O] = (0, Y.useState)(null), [A, j] = (0, Y.useState)(!1);
    E(e), k(`B`, () => {
        v || t()
    }, e && !v);
    let M = (0, Y.useCallback)(async () => {
        if (!r) {
            x([]);
            return
        }
        _(!0);
        try {
            let e = yo(await p.fetchQuery({queryKey: Er.pendingEffectResolutions, queryFn: le}), r);
            return x(e), e
        } catch (e) {
            return console.error(`EffectResolutionOverlay: load failed`, e), x([]), []
        } finally {
            _(!1)
        }
    }, [r, p]);
    (0, Y.useEffect)(() => {
        if (!e) {
            en(), j(!1);
            return
        }
        T(null), M()
    }, [e, M]), (0, Y.useEffect)(() => {
        u || j(!1)
    }, [u]);
    let N = b[0] ?? null, ee = (0, Y.useMemo)(() => N ? i.find(e => e.name === N.effectName) ?? null : null, [N, i]),
        te = (0, Y.useMemo)(() => N ? ee?.displayName ?? N.effectName : ``, [N, ee]), [ne, P] = (0, Y.useState)(null);
    (0, Y.useEffect)(() => {
        if (!e || !N?.sourcePlayerId) {
            P(null);
            return
        }
        let t = N.sourcePlayerId, n = N.pendingContext, r = Yc(n);
        if (r) {
            P(r);
            return
        }
        let i = Jc(h, t);
        if (i) {
            P(i);
            return
        }
        let a = !1;
        return P(null), Xc(t, h, n).then(e => {
            a || P(e)
        }), () => {
            a = !0
        }
    }, [e, N?.id, N?.sourcePlayerId, N?.pendingContext, h]);
    let re = (0, Y.useMemo)(() => N ? hr(i, N, ne) : null, [N, i, ne]),
        F = (0, Y.useMemo)(() => i.find(e => e.name === N?.effectName) ?? null, [i, N?.effectName]),
        ie = (0, Y.useMemo)(() => {
            if (!N) return null;
            let e = mr(i, N.pendingContext);
            return e ? i.find(t => t.name === e) ?? null : null
        }, [i, N]), I = (0, Y.useCallback)((e, t, n) => Kc(n ?? {
            name: e,
            displayName: t,
            description: ``,
            allowedStatuses: null
        }), []), ae = (0, Y.useMemo)(() => ho(ee), [ee]),
        L = (0, Y.useMemo)(() => N ? Ur(N.steps, N.nextStep ?? N.currentStep ?? null) : null, [N]);
    (0, Y.useEffect)(() => {
        !e || L?.type !== `manual-input` || C(Ir(L))
    }, [e, N?.id, L]), (0, Y.useEffect)(() => {
        if (!e) return;
        if (!ol(N?.pendingContext)) {
            O(null);
            return
        }
        let t = N.pendingContext.values?.[0]?.value;
        O(typeof t == `number` ? t : null)
    }, [e, N?.id, N?.pendingContext]), (0, Y.useEffect)(() => {
        if (L?.type !== `manual-target`) return;
        let e = Gr(L, null);
        if (e) {
            T(e);
            return
        }
        let t = L.candidates.find(e => !(`canTarget` in e) || e.canTarget !== !1), n = t == null ? null : Jr(t);
        n && T(n)
    }, [L, N?.id]);
    let se = (0, Y.useCallback)(async () => {
        let e = await M();
        o?.(), e?.length === 0 && t()
    }, [M, o, t]), R = (0, Y.useCallback)(async e => {
        if (!(!N || v)) {
            y(!0), s(f.Menu_Button_Back_Click);
            try {
                let t = N.nextStep ?? N.currentStep ?? L?.id ?? L?.type ?? null;
                if (!t) {
                    d(`Не удалось определить шаг действия`, J(`faces/angry.webp`), `Ошибка`);
                    return
                }
                let n = await Me({resolutionId: N.id, step: t, ...e});
                n && sl(d, N.effectName, te, n), C({}), T(null), await se()
            } catch (e) {
                console.error(`EffectResolutionOverlay: resolve failed`, e), d(e instanceof mt ? e.description ?? e.message : `Не удалось выполнить действие`, J(`faces/angry.webp`), `Ошибка`)
            } finally {
                y(!1)
            }
        }
    }, [N, L, te, v, s, se, d]), z = () => {
        if (nl(N)) {
            let e = N, t = rl(e);
            j(!0), al(e, async e => {
                try {
                    await R(il(e, t))
                } finally {
                    j(!1)
                }
            });
            return
        }
        if (ol(N?.pendingContext)) {
            if (D == null) return;
            R({data: {[N.pendingContext.field ?? `value`]: D}});
            return
        }
        if (L?.type === `target-random`) {
            R({});
            return
        }
        let e = qr(N?.steps ?? [], L, w);
        if (e && L?.type === `manual-target`) {
            R({targetPlayerId: e});
            return
        }
        Wr(L) || R({})
    }, ce = (0, Y.useMemo)(() => L?.type === `manual-input` ? Lr(L, S) : !1, [L, S]), ue = () => {
        if (ce) {
            if (L?.type === `manual-input` && L.input.length === 1 && L.input[0]?.name === `value`) {
                let e = S.value;
                if (typeof e == `string` || typeof e == `number` || typeof e == `boolean`) {
                    R({data: {value: e}});
                    return
                }
            }
            R({data: S})
        }
    }, de = Zn(N?.pendingContext), fe = v ? `Отправка…` : de ? `Ясно...` : `Подтвердить`;
    return e ? (0, X.jsxs)(`div`, {
        className: `effect-resolution-overlay${u ? ` effect-resolution-overlay--mini-game-active` : ``}`,
        role: `dialog`,
        "aria-modal": `true`,
        "aria-labelledby": `effect-resolution-title`,
        children: [(0, X.jsxs)(`div`, {
            className: `effect-resolution-panel${de ? ` effect-resolution-panel--notice` : ``}`,
            children: [(0, X.jsxs)(`div`, {
                className: `effect-resolution-header${de ? ` effect-resolution-header--notice` : ``}`,
                children: [de ? (0, X.jsxs)(`div`, {
                    className: `effect-resolution-header__lead`,
                    children: [(0, X.jsx)(`h2`, {
                        id: `effect-resolution-title`,
                        className: `effect-resolution-title`,
                        children: `Получен эффект от`
                    }), (0, X.jsx)(`div`, {
                        className: `effect-resolution-notice__avatar-wrap`,
                        children: ne == null ? (0, X.jsx)(`div`, {
                            className: `effect-resolution-notice__avatar effect-resolution-notice__avatar--placeholder`,
                            "aria-hidden": !0
                        }) : (0, X.jsx)(`img`, {
                            className: `effect-resolution-notice__avatar`,
                            src: yi(ne),
                            alt: ne,
                            draggable: !1
                        })
                    }), (0, X.jsx)(`span`, {
                        className: `effect-resolution-notice__player-name`,
                        children: ne ?? `Загрузка…`
                    })]
                }) : (0, X.jsx)(`h2`, {
                    id: `effect-resolution-title`,
                    className: `effect-resolution-title`,
                    children: `Обязательное действие`
                }), (0, X.jsxs)(`button`, {
                    type: `button`,
                    className: `effect-resolution-close`,
                    onClick: l(t),
                    onMouseEnter: c,
                    disabled: v,
                    "aria-label": `Закрыть`,
                    children: [(0, X.jsx)(`span`, {
                        className: `effect-resolution-close-arrow`,
                        "aria-hidden": !0
                    }), (0, X.jsx)(`span`, {children: `Назад`}), (0, X.jsx)(q, {
                        button: `B`,
                        className: `effect-resolution-back-gp-hint`
                    })]
                })]
            }), g && (0, X.jsx)(`p`, {
                className: `effect-resolution-loading`,
                children: `Загрузка…`
            }), !g && N == null && (0, X.jsx)(`p`, {
                className: `effect-resolution-empty`,
                children: `Нет обязательных действий. Можно продолжать.`
            }), !g && N != null && (0, X.jsxs)(X.Fragment, {
                children: [!de && (0, X.jsxs)(`p`, {
                    className: `effect-resolution-subtitle`,
                    children: [`Перед продолжением нужно завершить действие по эффекту «`, te, `».`]
                }), (e => {
                    if (!e) return (0, X.jsx)(`p`, {
                        className: `effect-resolution-empty`,
                        children: `Не удалось определить шаг действия. Попробуйте обновить страницу.`
                    });
                    switch (e.type) {
                        case`random-effect`:
                            return (0, X.jsxs)(X.Fragment, {
                                children: [(0, X.jsx)(`p`, {
                                    className: `effect-resolution-step-label`,
                                    children: `Выпал эффект`
                                }), (0, X.jsx)(`ul`, {
                                    className: `effect-resolution-random-list`,
                                    children: e.candidateNames.map(t => (0, X.jsxs)(`li`, {children: [i.find(e => e.name === t)?.displayName ?? t, e.selectedName === t ? ` ✓` : ``]}, t))
                                }), (0, X.jsx)(`div`, {
                                    className: `effect-resolution-actions`,
                                    children: (0, X.jsx)(`button`, {
                                        type: `button`,
                                        className: `effect-resolution-btn`,
                                        disabled: v,
                                        onClick: z,
                                        children: v ? `Отправка…` : `Продолжить`
                                    })
                                })]
                            });
                        case`manual-target`:
                            return (0, X.jsx)(`p`, {
                                className: `effect-resolution-subtitle`,
                                children: `Выберите игрока в окне выбора цели.`
                            });
                        case`target-random`:
                            return (0, X.jsx)(`p`, {
                                className: `effect-resolution-subtitle`,
                                children: N?.kind === `gnus-wheel` ? `Выберите стримера в окне случайного выбора.` : `Выберите игрока в окне случайного выбора.`
                            });
                        case`manual-input`:
                            return (0, X.jsxs)(X.Fragment, {
                                children: [(0, X.jsx)(`p`, {
                                    className: `effect-resolution-step-label`,
                                    children: e.input.some(e => (e.allowedValues?.length ?? 0) > 0) ? `Выберите значение` : `Введите данные`
                                }), e.input.map(e => {
                                    let t = e.allowedValues ?? [];
                                    if (t.length > 0) {
                                        let n = S[e.name];
                                        return (0, X.jsxs)(`div`, {
                                            className: `effect-resolution-field`,
                                            children: [(0, X.jsx)(`p`, {
                                                className: `effect-resolution-field-label`,
                                                children: e.placeholder || e.name
                                            }), (0, X.jsx)(`div`, {
                                                className: `effect-resolution-choices`,
                                                children: t.map(t => {
                                                    let r = n === t, i = String(t);
                                                    return (0, X.jsx)(`button`, {
                                                        type: `button`,
                                                        className: `effect-resolution-choice ${r ? `effect-resolution-choice--active` : ``}`,
                                                        onClick: () => {
                                                            s(f.Menu_Button_Back_Click), C(n => ({...n, [e.name]: t}))
                                                        },
                                                        onMouseEnter: () => s(f.Menu_Button_Back_Hover),
                                                        children: typeof t == `string` ? Xo(t) : String(t)
                                                    }, i)
                                                })
                                            })]
                                        }, e.name)
                                    }
                                    return (0, X.jsxs)(`div`, {
                                        className: `effect-resolution-field`,
                                        children: [(0, X.jsx)(`label`, {
                                            id: `resolution-${e.name}-label`,
                                            htmlFor: `resolution-${e.name}`,
                                            children: e.placeholder || e.name
                                        }), e.type === `boolean` ? (0, X.jsx)(`div`, {
                                            className: `effect-resolution-boolean`,
                                            role: `radiogroup`,
                                            "aria-labelledby": `resolution-${e.name}-label`,
                                            children: [{value: !0, label: `Да`}, {
                                                value: !1,
                                                label: `Нет`
                                            }].map(({value: t, label: n}) => {
                                                let r = S[e.name] === t;
                                                return (0, X.jsx)(`button`, {
                                                    type: `button`,
                                                    role: `radio`,
                                                    "aria-checked": r,
                                                    className: `effect-resolution-boolean-card ${r ? `effect-resolution-boolean-card--selected` : ``}`,
                                                    onClick: () => {
                                                        s(f.Menu_Button_Back_Click), C(n => ({...n, [e.name]: t}))
                                                    },
                                                    onMouseEnter: () => s(f.Menu_Button_Back_Hover),
                                                    children: n
                                                }, n)
                                            })
                                        }) : e.type === `player-ids` ? (() => {
                                            let t = S[e.name], n = Array.isArray(t) ? t : [],
                                                r = (h ?? []).filter(e => e.role !== `judge`), i = e.maxItems,
                                                a = t => {
                                                    C(n => {
                                                        let r = Array.isArray(n[e.name]) ? n[e.name] : [],
                                                            a = r.includes(t) ? r.filter(e => e !== t) : i != null && r.length >= i ? r : [...r, t];
                                                        return {...n, [e.name]: a}
                                                    })
                                                };
                                            return (0, X.jsx)(`div`, {
                                                className: `effect-resolution-player-grid`,
                                                role: `group`,
                                                "aria-labelledby": `resolution-${e.name}-label`,
                                                children: r.length === 0 ? (0, X.jsx)(`p`, {
                                                    className: `effect-resolution-empty`,
                                                    children: `Нет доступных игроков`
                                                }) : r.map(e => {
                                                    let t = n.includes(e.id), r = !t && i != null && n.length >= i;
                                                    return (0, X.jsxs)(`button`, {
                                                        type: `button`,
                                                        "aria-pressed": t,
                                                        disabled: r,
                                                        className: `effect-resolution-player-card ${t ? `effect-resolution-player-card--selected` : ``} ${r ? `effect-resolution-player-card--disabled` : ``}`,
                                                        title: e.name,
                                                        onClick: () => {
                                                            s(f.Menu_Button_Back_Click), a(e.id)
                                                        },
                                                        onMouseEnter: () => s(f.Menu_Button_Back_Hover),
                                                        children: [(0, X.jsx)(`img`, {
                                                            className: `effect-resolution-player-avatar`,
                                                            src: yi(e.name),
                                                            alt: e.name,
                                                            draggable: !1
                                                        }), (0, X.jsx)(`span`, {
                                                            className: `effect-resolution-player-name`,
                                                            children: e.name
                                                        })]
                                                    }, e.id)
                                                })
                                            })
                                        })() : (0, X.jsx)(`input`, {
                                            id: `resolution-${e.name}`,
                                            type: e.type === `number` ? `number` : `text`,
                                            placeholder: e.placeholder,
                                            value: String(S[e.name] ?? ``),
                                            onChange: t => {
                                                let n = t.target.value;
                                                C(t => ({
                                                    ...t,
                                                    [e.name]: e.type === `number` ? n === `` ? `` : Number(n) : n
                                                }))
                                            }
                                        })]
                                    }, e.name)
                                }), (0, X.jsx)(`div`, {
                                    className: `effect-resolution-actions`,
                                    children: (0, X.jsx)(`button`, {
                                        type: `button`,
                                        className: `effect-resolution-btn`,
                                        disabled: v || !ce,
                                        onClick: ue,
                                        children: v ? `Отправка…` : `Подтвердить`
                                    })
                                })]
                            });
                        case`confirm`: {
                            if (nl(N)) return (0, X.jsxs)(X.Fragment, {
                                children: [(0, X.jsx)(`p`, {
                                    className: `effect-resolution-step-label`,
                                    children: `По нажатию кнопки «Подтвердить» будет начата мини-игра`
                                }), (0, X.jsx)(`div`, {
                                    className: `effect-resolution-actions`,
                                    children: (0, X.jsx)(`button`, {
                                        type: `button`,
                                        className: `effect-resolution-btn`,
                                        disabled: v || A,
                                        onClick: z,
                                        children: v ? `Отправка…` : A ? `Мини-игра…` : `Подтвердить`
                                    })
                                })]
                            });
                            if (ol(N?.pendingContext)) {
                                let e = N.pendingContext.values ?? [];
                                return (0, X.jsxs)(X.Fragment, {
                                    children: [(0, X.jsx)(`p`, {
                                        className: `effect-resolution-step-label`,
                                        children: `Выберите значение`
                                    }), (0, X.jsx)(`div`, {
                                        className: `effect-resolution-candidates`,
                                        children: e.map(e => (0, X.jsxs)(`button`, {
                                            type: `button`,
                                            className: `effect-resolution-candidate ${D === e.value ? `effect-resolution-candidate--active` : ``}`,
                                            onClick: () => O(e.value),
                                            children: [e.value, ` (`, e.amount >= 0 ? `+` : ``, e.amount, `)`]
                                        }, String(e.value)))
                                    }), (0, X.jsx)(`div`, {
                                        className: `effect-resolution-actions`,
                                        children: (0, X.jsx)(`button`, {
                                            type: `button`,
                                            className: `effect-resolution-btn`,
                                            disabled: v || D == null,
                                            onClick: z,
                                            children: v ? `Отправка…` : `Подтвердить`
                                        })
                                    })]
                                })
                            }
                            let e = qr(N?.steps ?? [], L, w), t = Wr(L) || N != null && Kr(N.steps) && e == null;
                            if (de && re != null) {
                                let e = mr(i, N.pendingContext), n = re.lostEffectDisplayName ?? ie?.displayName ?? e,
                                    r = n != null && n !== re.sourceEffectDisplayName,
                                    a = I(N.effectName, re.sourceEffectDisplayName, F),
                                    o = r && e && n ? I(e, n, ie) : null;
                                return (0, X.jsxs)(`div`, {
                                    className: `effect-resolution-notice`,
                                    children: [(0, X.jsxs)(`div`, {
                                        className: `effect-resolution-notice__effects-stack`,
                                        children: [(0, X.jsx)(Ac, {
                                            data: a,
                                            embedded: !0
                                        }), o != null && (0, X.jsxs)(X.Fragment, {
                                            children: [(0, X.jsx)(`div`, {
                                                className: `effect-resolution-notice__effects-arrow-down`,
                                                "aria-hidden": !0,
                                                children: `↓`
                                            }), (0, X.jsx)(Ac, {data: o, embedded: !0})]
                                        })]
                                    }), re.headlineMessage != null && (0, X.jsx)(`p`, {
                                        className: `effect-resolution-notice__headline`,
                                        children: re.headlineMessage
                                    }), (0, X.jsx)(`div`, {
                                        className: `effect-resolution-actions effect-resolution-actions--notice`,
                                        children: (0, X.jsx)(`button`, {
                                            type: `button`,
                                            className: `effect-resolution-btn`,
                                            disabled: v || t,
                                            onClick: z,
                                            children: fe
                                        })
                                    })]
                                })
                            }
                            return (0, X.jsx)(X.Fragment, {
                                children: (0, X.jsx)(`div`, {
                                    className: `effect-resolution-actions`,
                                    children: (0, X.jsx)(`button`, {
                                        type: `button`,
                                        className: `effect-resolution-btn`,
                                        disabled: v || t,
                                        onClick: z,
                                        children: fe
                                    })
                                })
                            })
                        }
                        default:
                            return null
                    }
                })(L)]
            }), !g && b.length > 1 && (0, X.jsxs)(`p`, {
                className: `effect-resolution-subtitle`,
                children: [`Осталось действий: `, b.length]
            })]
        }), L?.type === `target-random` && (0, X.jsx)(Li, {
            isOpen: !0,
            targets: fi(L.candidates),
            predeterminedPlayerId: L.selected == null ? null : Jr(L.selected),
            predeterminedPlayerName: typeof L.selected?.name == `string` ? L.selected.name : null,
            effectDisplayName: te,
            submitting: v,
            onClose: () => {
            },
            onConfirm: z
        }), N?.kind === `gnus-wheel` && L?.type === `manual-target` && (0, X.jsx)(Ai, {
            isOpen: !0,
            targets: ai(fi(L.candidates), h, i),
            effectDisplayName: te,
            effectName: N?.effectName,
            submitting: v,
            multiSelect: ri(ee) || ae != null,
            minSelections: ae?.minItems ?? 0,
            maxSelections: ae?.maxItems,
            onClose: () => {
                v || T(null)
            },
            onConfirm: e => {
                R({targetPlayerId: e})
            },
            onConfirmMulti: e => {
                ae && R({data: {[ae.name]: e}})
            }
        })]
    }) : null
};

function ll(e) {
    return Gn({
        queryKey: Er.pendingEffectResolutions,
        queryFn: le,
        enabled: e?.enabled ?? !0,
        refetchInterval: e?.refetchInterval ?? !1
    })
}

var ul = ({playerId: e, onCheckPendingEffectResolutions: t, label: n = `Выполните действие!`}) => {
        let {play: r} = oe(), {data: i} = ll({enabled: e != null});
        return (0, Y.useMemo)(() => e != null && xo(i, e), [i, e]) ? (0, X.jsx)(`button`, {
            type: `button`,
            className: `pending-effect-notification-button`,
            onClick: () => {
                r(f.Menu_Button_Back_Click), t?.({notify: !1})
            },
            onMouseEnter: () => r(f.Menu_Button_Back_Hover),
            children: n
        }) : null
    }, dl = `gameChoose:autoStartTimer`, fl = (0, Y.memo)(function ({
                                                                        player: e,
                                                                        onRequireEffectResolution: t,
                                                                        onCheckPendingEffectResolutions: n,
                                                                        onShowHistory: r
                                                                    } = {}) {
        let {showNotification: i} = Cn(), a = _c(), {
                play: o,
                playKeyboard: s
            } = oe(), [c, l] = (0, Y.useState)(``), [u, d] = (0, Y.useState)([]), [p, m] = (0, Y.useState)(!1), [h, g] = (0, Y.useState)(!1), [_, v] = (0, Y.useState)(null), [y, b] = (0, Y.useState)(!1), [S, C] = (0, Y.useState)(``), [w, T] = (0, Y.useState)(() => {
                try {
                    let e = localStorage.getItem(dl);
                    return e === null ? !0 : e === `true`
                } catch {
                    return !0
                }
            }), D = (0, Y.useRef)(null), O = (0, Y.useRef)(0), A = (0, Y.useRef)(null), M = (0, Y.useRef)(null),
            N = (0, Y.useCallback)(async e => {
                let t = Date.now();
                if (!(t - O.current < 100)) {
                    O.current = t, m(!0), C(e);
                    try {
                        d(await me(e) || [])
                    } catch {
                        d([])
                    } finally {
                        m(!1)
                    }
                }
            }, []);
        (0, Y.useEffect)(() => {
            if (D.current &&= (clearTimeout(D.current), null), c === ``) {
                d([]), C(``);
                return
            }
            return c !== S && (D.current = window.setTimeout(() => {
                N(c)
            }, 600)), () => {
                D.current &&= (clearTimeout(D.current), null)
            }
        }, [c, S, N]), (0, Y.useEffect)(() => {
            try {
                localStorage.setItem(dl, String(w))
            } catch {
            }
        }, [w]), (0, Y.useEffect)(() => {
            let e = e => {
                A.current && !A.current.contains(e.target) && b(!1)
            };
            return document.addEventListener(`mousedown`, e), () => {
                document.removeEventListener(`mousedown`, e)
            }
        }, []);
        let ee = e => {
            v(e), l(``), b(!1), d([])
        }, ne = j(), [P, re] = (0, Y.useState)(0), F = y && u.length > 0 && !ne;
        E(F);
        let ie = te(), I = ne || ie, ae = u.length > 0 ? Math.min(Math.max(0, P), u.length - 1) : 0;
        (0, Y.useEffect)(() => (x(F), () => x(!1)), [F]), k(`DPadUp`, () => re(e => Math.max(0, e - 1)), F), k(`DPadDown`, () => re(e => Math.min(u.length - 1, e + 1)), F), k(`A`, () => {
            let e = u[ae];
            e && ee(e)
        }, F), k(`A`, () => {
            se()
        }, !!_ && !h && !F && !I), k(`RT`, () => {
            M.current?.focus(), b(!0)
        }, !h && !I), k(`X`, () => {
            !r || h || R()
        }, !!r && !h && !I);
        let L = (0, Y.useCallback)(e => {
            o(f.Menu_Button_Participants_Hover, {panElement: e.currentTarget})
        }, [o]), se = async () => {
            if (!_ || h || (o(f.Menu_Button_Back_Click), n != null && await n())) return;
            g(!0);
            let e = !1;
            try {
                await ve({type: `game`, data: {id: _.id}}), w && await ye({action: `start`}), e = !0, Or()
            } catch (e) {
                if (console.error(`Failed to start game:`, e), Te(e)) {
                    i(`Сначала выполните обязательное действие по эффекту`, J(`faces/angry.webp`), `Нужно действие`, void 0, f.UI_NOT_Error), t?.();
                    return
                }
                if (e instanceof mt && e.statusCode === 400) {
                    let t = `Вы уже играли в эту игру на ивенте`;
                    e.description?.includes(`instance.type does not exactly match expected constant: movie`) ? t = `Вы не можете играть в игру во время действия билета на фильм. Как вы попали в это окно?` : e.description && (t = e.description), i(t, J(`faces/angry.webp`), `Ошибка`, void 0, f.UI_NOT_Error);
                    return
                }
                let n = bt(e);
                n && i(n, J(`faces/angry.webp`), `Ошибка`, void 0, f.UI_NOT_Error)
            } finally {
                e && o(f.Game_Started), g(!1)
            }
        }, R = () => {
            o(f.Menu_Button_Back_Click), r?.()
        }, z = _?.seconds != null && _.seconds > 0 ? Je(_.seconds) : null;
        return (0, X.jsx)(X.Fragment, {
            children: (0, X.jsxs)(`div`, {
                className: `game-choose`,
                children: [(0, X.jsxs)(`div`, {
                    className: `game-choose-section section-top`,
                    children: [(0, X.jsx)(`span`, {
                        className: `game-choose-section-label`,
                        children: `Текущая игра`
                    }), (0, X.jsx)(`div`, {className: `game-choose-section-line`, "aria-hidden": !0})]
                }), (0, X.jsx)(`h2`, {className: `game-choose-title`, children: `Выбор игры`}), (0, X.jsxs)(`div`, {
                    className: `game-select-container`,
                    ref: A,
                    children: [(0, X.jsxs)(`div`, {
                        className: `game-select-box`,
                        onClick: () => {
                            b(!y), y || (l(``), setTimeout(() => {
                                M.current?.focus()
                            }, 0))
                        },
                        children: [(0, X.jsx)(`span`, {
                            className: `game-select-text`,
                            children: _ ? _.name : `Выбрать`
                        }), (0, X.jsx)(q, {
                            button: `RT`,
                            className: `game-select-gp-hint`
                        }), (0, X.jsx)(`img`, {
                            src: J(`ui/arrow_down.svg`),
                            alt: ``,
                            className: `game-select-arrow`,
                            "aria-hidden": !0
                        })]
                    }), y && (0, X.jsxs)(`div`, {
                        className: `game-select-dropdown`,
                        children: [(0, X.jsx)(`input`, {
                            ref: M,
                            type: `text`,
                            className: `game-select-input`,
                            placeholder: `Введите название игры...`,
                            value: c,
                            onChange: e => {
                                l(e.target.value), b(!0)
                            },
                            onKeyDown: e => Fr(e, s),
                            autoFocus: !0
                        }), p && (0, X.jsx)(`div`, {
                            className: `game-select-loading`,
                            children: `Поиск...`
                        }), u.length > 0 && (0, X.jsx)(ao, {
                            className: `game-select-results-scroll`,
                            gamepadScroll: !0,
                            children: (0, X.jsx)(`div`, {
                                className: `game-select-results`, children: u.map((e, t) => {
                                    let n = e.seconds != null && e.seconds > 0 ? Je(e.seconds) : null;
                                    return (0, X.jsxs)(`div`, {
                                        className: `game-select-item${F && t === ae ? ` game-select-item--gamepad-focused` : ``}`,
                                        onClick: () => ee(e),
                                        onMouseEnter: () => {
                                            F && re(t)
                                        },
                                        children: [e.imageUrl && (0, X.jsx)(`img`, {
                                            src: e.imageUrl,
                                            alt: e.name,
                                            className: `game-select-item-image`
                                        }), (0, X.jsxs)(`div`, {
                                            className: `game-select-item-content`,
                                            children: [(0, X.jsx)(`div`, {
                                                className: `game-select-item-name`,
                                                children: e.name
                                            }), (e.releaseYear != null || n) && (0, X.jsxs)(`div`, {
                                                className: `game-select-item-year`,
                                                children: [e.releaseYear != null && (0, X.jsx)(`span`, {children: e.releaseYear === 0 ? `Еще не вышла` : `Год выпуска: ${e.releaseYear}`}), e.releaseYear != null && n && (0, X.jsxs)(X.Fragment, {children: [`\xA0`, `|`, `\xA0`]}), n && (0, X.jsxs)(`span`, {children: [`HLTB - `, n]})]
                                            })]
                                        })]
                                    }, e.id)
                                })
                            })
                        }), c.length > 0 && u.length === 0 && !p && (0, X.jsx)(`div`, {
                            className: `game-select-no-results`,
                            children: `Игры не найдены`
                        })]
                    })]
                }), (0, X.jsxs)(`div`, {
                    className: `buttons-container`, children: [_ && (0, X.jsxs)(`div`, {
                        className: `selected-game-info`,
                        children: [_.imageUrl && (0, X.jsx)(`img`, {
                            src: _.imageUrl,
                            alt: _.name,
                            className: `selected-game-image`
                        }), (0, X.jsxs)(`div`, {
                            className: `selected-game-details`,
                            children: [(0, X.jsxs)(`div`, {
                                className: `selected-game-name`,
                                children: [(0, X.jsx)(`span`, {
                                    className: `selected-game-name-text`,
                                    children: _.name
                                }), _.steamStoreLink && (0, X.jsx)(`a`, {
                                    href: _.steamStoreLink,
                                    target: `_blank`,
                                    rel: `noopener noreferrer`,
                                    onClick: e => e.stopPropagation(),
                                    className: `steam-link-icon`,
                                    children: (0, X.jsx)(`img`, {src: Et(a), alt: `Steam`, className: `steam-icon`})
                                }), _.ggpReward != null && (0, X.jsxs)(`span`, {
                                    className: `selected-game-ggp`,
                                    children: [`{до `, (0, X.jsx)(`span`, {
                                        className: `selected-game-ggp-value`,
                                        children: et(_.ggpReward, `genitive`)
                                    }), `}`]
                                })]
                            }), (_.releaseYear != null || z) && (0, X.jsxs)(`div`, {
                                className: `selected-game-release`,
                                children: [_.releaseYear != null && (0, X.jsx)(`span`, {children: _.releaseYear === 0 ? `Еще не вышла` : `Год выпуска: ${_.releaseYear}`}), _.releaseYear != null && z && (0, X.jsxs)(X.Fragment, {children: [`\xA0`, `|`, `\xA0`]}), z && (0, X.jsxs)(`span`, {children: [`HLTB - `, z]})]
                            })]
                        })]
                    }), (0, X.jsxs)(`div`, {
                        className: `buttons-row`,
                        children: [(0, X.jsxs)(`div`, {
                            className: `button ${!_ || h ? `button-disabled` : ``}`,
                            onClick: se,
                            onMouseEnter: () => {
                                _ && !h && o(f.Menu_Button_Back_Hover)
                            },
                            children: [!h && (0, X.jsx)(q, {
                                button: `A`,
                                className: `button-gamepad-hint`
                            }), h ? `Начинаем` : `Начать`]
                        }), r && (0, X.jsxs)(`div`, {
                            className: `button ${h ? `button-disabled` : ``}`,
                            onClick: h ? void 0 : R,
                            onMouseEnter: () => {
                                h || o(f.Menu_Button_Back_Hover)
                            },
                            children: [!h && (0, X.jsx)(q, {button: `X`, className: `button-gamepad-hint`}), `История`]
                        }), (0, X.jsx)(ul, {playerId: e?.id, onCheckPendingEffectResolutions: n})]
                    }), (0, X.jsxs)(`div`, {
                        className: `auto-start-timer-row`,
                        children: [(0, X.jsx)(`input`, {
                            className: `auto-start-timer-checkbox`,
                            type: `checkbox`,
                            checked: w,
                            onMouseEnter: L,
                            onChange: e => {
                                o(f.Menu_Button_Participants_Click), T(e.target.checked)
                            }
                        }), (0, X.jsx)(`span`, {
                            className: `auto-start-timer-label`,
                            children: `Автоматически активировать таймер`
                        })]
                    })]
                })]
            })
        })
    }), pl = {ggp: `GGP (очки)`, platinumChips: `Premium вращения`, gnusAvailable: `Гнусные вращения`},
    ml = {ggp: `очков`, platinumChips: `жетонов`, gnusAvailable: `Гнусные вращения`},
    hl = ({onClose: e, player: t, field: r, direction: i, loading: a, submitting: o, error: s, onSubmit: c}) => {
        let {play: l, playKeyboard: u} = oe(), {onBackHover: d, withBackClick: p} = n(), [m, h] = (0, Y.useState)(``),
            g = parseInt(m, 10), _ = Number.isFinite(g) && g > 0 && Number.isInteger(g),
            v = r === `gnusAvailable` ? t?.gnusAvailable ?? 0 : t?.[r] ?? 0, y = i === `add` ? `Добавить` : `Убрать`,
            b = i === `add` ? `+` : `−`, x = !_ || o || a, S = () => {
                x || (l(f.Menu_Button_Back_Click), c(g))
            }, C = j();
        return E(!0), k(`A`, S, !x && !C), k(`B`, () => {
            o || (l(f.Menu_Button_Back_Click), e())
        }, !o && !C), (0, X.jsx)(`div`, {
            className: `judge-stat-adjust-overlay`,
            role: `dialog`,
            "aria-modal": `true`,
            "aria-labelledby": `judge-stat-adjust-title`,
            children: (0, X.jsxs)(`div`, {
                className: `judge-stat-adjust-panel`,
                children: [(0, X.jsxs)(`div`, {
                    className: `judge-stat-adjust-header`,
                    children: [(0, X.jsx)(`h2`, {
                        id: `judge-stat-adjust-title`,
                        className: `judge-stat-adjust-title`,
                        children: pl[r]
                    }), (0, X.jsxs)(`button`, {
                        type: `button`,
                        className: `judge-stat-adjust-back`,
                        onClick: p(e),
                        onMouseEnter: d,
                        disabled: o,
                        "aria-label": `Назад`,
                        children: [(0, X.jsx)(`span`, {
                            className: `judge-stat-adjust-back-arrow`,
                            "aria-hidden": !0
                        }), (0, X.jsx)(`span`, {children: `Назад`}), (0, X.jsx)(q, {
                            button: `B`,
                            className: `judge-stat-adjust-back-gp-hint`
                        })]
                    })]
                }), a ? (0, X.jsx)(`p`, {
                    className: `judge-stat-adjust-loading`,
                    children: `Загрузка данных игрока…`
                }) : t ? (0, X.jsxs)(X.Fragment, {
                    children: [(0, X.jsxs)(`div`, {
                        className: `judge-stat-adjust-player`,
                        children: [(0, X.jsx)(`div`, {
                            className: `judge-stat-adjust-avatar-wrap`,
                            children: (0, X.jsx)(`img`, {
                                className: `judge-stat-adjust-avatar`,
                                src: yi(t.name),
                                alt: t.name,
                                draggable: !1
                            })
                        }), (0, X.jsxs)(`div`, {
                            className: `judge-stat-adjust-player-info`,
                            children: [(0, X.jsx)(`span`, {
                                className: `judge-stat-adjust-player-name`,
                                children: t.name
                            }), (0, X.jsxs)(`span`, {
                                className: `judge-stat-adjust-player-current`,
                                children: [(0, X.jsxs)(`span`, {
                                    className: `judge-stat-adjust-player-current-label`,
                                    children: [ml[r], `:`]
                                }), ` `, (0, X.jsx)(`span`, {
                                    className: `judge-stat-adjust-player-current-value`,
                                    children: v
                                })]
                            })]
                        })]
                    }), (0, X.jsxs)(`div`, {
                        className: `judge-stat-adjust-input-row`,
                        children: [(0, X.jsx)(`span`, {
                            className: `judge-stat-adjust-input-sign`,
                            children: b
                        }), (0, X.jsx)(`input`, {
                            className: `judge-stat-adjust-input`,
                            type: `number`,
                            min: 1,
                            step: 1,
                            value: m,
                            disabled: o,
                            placeholder: `Количество`,
                            autoFocus: !0,
                            onChange: e => h(e.target.value),
                            onKeyDown: e => {
                                Fr(e, u), e.key === `Enter` && _ && S()
                            }
                        })]
                    }), s ? (0, X.jsx)(`p`, {
                        className: `judge-stat-adjust-error`,
                        role: `alert`,
                        children: s
                    }) : null, (0, X.jsx)(`div`, {
                        className: `judge-stat-adjust-actions`,
                        children: (0, X.jsxs)(`button`, {
                            type: `button`,
                            className: `judge-stat-adjust-confirm ${x ? `judge-stat-adjust-confirm--disabled` : ``}`,
                            disabled: x,
                            onClick: S,
                            onMouseEnter: () => {
                                x || l(f.Menu_Button_Back_Hover)
                            },
                            children: [!x && !o && (0, X.jsx)(q, {
                                button: `A`,
                                className: `judge-stat-adjust-gp-hint`
                            }), o ? `Сохранение` : y]
                        })
                    })]
                }) : (0, X.jsx)(`p`, {
                    className: `judge-stat-adjust-error`,
                    role: `alert`,
                    children: `Не удалось загрузить данные игрока`
                })]
            })
        })
    }, gl = ({
                 isOpen: e,
                 onClose: t,
                 player: n,
                 field: r,
                 direction: i,
                 loading: a = !1,
                 submitting: o = !1,
                 error: s = null,
                 onSubmit: c
             }) => e ? (0, X.jsx)(hl, {
        onClose: t,
        player: n,
        field: r,
        direction: i,
        loading: a,
        submitting: o,
        error: s,
        onSubmit: c
    }, `${n?.id ?? `none`}-${r}-${i}`) : null, _l = {
        "fastest-player": `Главный угонщик`,
        "wasteland-king": `Главный угонщик`,
        bonus: `Бонус`,
        effect: `Эффект`,
        "effect-auto": `Эффект`,
        penalty: `Штраф`,
        judge: `Жанровая лихорадка`,
        "judge-manual": `Жанровая лихорадка`
    }, vl = {genre_fever: `Жанровая лихорадка`}, yl = new Set([`fastest-player`, `wasteland-king`]), bl = {
        "Fastest player record beaten": `Главный угонщик`,
        "Judge manual adjustment": `Жанровая лихорадка`,
        "Completed game minimum reward": `Минимум`
    }, xl = `Completed game minimum reward`, Sl = (e, t) => {
        let n = e?.[t];
        return typeof n == `string` && n.length > 0 ? n : void 0
    }, Cl = (e, t) => {
        let n = e?.[t];
        return typeof n == `number` && Number.isFinite(n) ? n : void 0
    };

function wl(e) {
    if (e.label?.trim() === xl) {
        let t = Cl(e.metadata, `minReward`);
        if (t != null) return t
    }
    return e.amount
}

var Tl = e => {
    let t = Sl(e.metadata, `challengerPlayerId`), n = Sl(e.metadata, `previousRecordPlayerId`);
    return t && t !== e.playerId ? t : n
};

function El(e, t) {
    let n = e.label?.trim();
    if (n && bl[n]) return Dl(bl[n], e, t);
    let r = e.effectName && vl[e.effectName];
    if (r) return r;
    let i = _l[e.source];
    return i && yl.has(e.source) ? Dl(i, e, t) : n || i || e.source || `Корректировка`
}

var Dl = (e, t, n) => {
    if (t.source !== `fastest-player` || !n?.getPlayerName) return e;
    let r = Tl(t);
    if (!r) return e;
    let i = n.getPlayerName(r);
    return i ? `${e} (${i})` : e
};

function Ol(e, t) {
    return `${El(e, t)}: ${e.amount > 0 ? `+` : ``}${e.amount}`
}

function kl(e, t) {
    return (e ?? []).filter(e => e.amount !== 0).map(e => Ol(e, t))
}

var Al = `Жанровая лихорадка`, jl = `genre_fever`, Ml = `Игр не найдено`,
    Nl = (e, t) => (e.effectsHistory ?? []).includes(t), Pl = e => e.type === `game` && Nl(e, jl), Fl = e => {
        if (!e) return ``;
        let t = new Date(e);
        return Number.isNaN(t.getTime()) ? e : `${t.getDate().toString().padStart(2, `0`)}.${(t.getMonth() + 1).toString().padStart(2, `0`)}.${t.getFullYear()}`
    }, Il = ({player: e, direction: t, onClose: r, onSuccess: i}) => {
        let {play: a, playKeyboard: o} = oe(), {
            onBackHover: s,
            withBackClick: c
        } = n(), [l, u] = (0, Y.useState)(null), [d, p] = (0, Y.useState)(!0), [m, h] = (0, Y.useState)(null), [g, _] = (0, Y.useState)(null), [v, y] = (0, Y.useState)(null), [b, x] = (0, Y.useState)(``), [S, C] = (0, Y.useState)(``), [w, T] = (0, Y.useState)(!1);
        (0, Y.useEffect)(() => {
            if (!e) {
                u(null), p(!1);
                return
            }
            let t = !1;
            return p(!0), h(null), Ce(e.id).then(e => {
                t || u(e ?? [])
            }).catch(() => {
                t || h(`Не удалось загрузить историю аукционов`)
            }).finally(() => {
                t || p(!1)
            }), () => {
                t = !0
            }
        }, [e]);
        let D = (0, Y.useMemo)(() => {
                if (!l) return [];
                let e = [];
                for (let t of l) {
                    if (!Pl(t)) continue;
                    let n = t.scoreAdjustments ?? [];
                    for (let r of n) r.removedAt || r.effectName === jl && e.push({adjustment: r, auction: t})
                }
                return e.sort((e, t) => new Date(t.adjustment.createdAt).getTime() - new Date(e.adjustment.createdAt).getTime())
            }, [l]),
            O = (0, Y.useMemo)(() => l ? [...l].filter(Pl).sort((e, t) => new Date(t.createdAt).getTime() - new Date(e.createdAt).getTime()) : [], [l]),
            A = parseInt(b, 10), M = Number.isFinite(A) && A > 0 && Number.isInteger(A), N = t === `add`,
            ee = N ? `${Al} +` : `${Al} −`, te = N ? `Добавить` : `Убрать`, ne = N ? !!g && M && !w && !d : !!v && !w && !d,
            P = j();
        E(!0), k(`A`, () => {
            re()
        }, ne && !P), k(`B`, () => {
            w || (a(f.Menu_Button_Back_Click), r())
        }, !w && !P);
        let re = async () => {
            if (!(!ne || !e)) {
                a(f.Menu_Button_Back_Click), T(!0), h(null);
                try {
                    if (N) {
                        if (!await De({auctionResultId: g, effectName: jl, amount: A})) {
                            h(`Не удалось добавить корректировку`);
                            return
                        }
                    } else if (!await wt({adjustmentId: v, reason: S.trim() || void 0})) {
                        h(`Не удалось убрать корректировку`);
                        return
                    }
                    Or(e.id), i?.(), r()
                } catch {
                    h(`Ошибка при сохранении`)
                } finally {
                    T(!1)
                }
            }
        };
        return (0, X.jsx)(`div`, {
            className: `judge-genre-fever-overlay`,
            role: `dialog`,
            "aria-modal": `true`,
            "aria-labelledby": `judge-genre-fever-title`,
            children: (0, X.jsxs)(`div`, {
                className: `judge-genre-fever-panel`,
                children: [(0, X.jsxs)(`div`, {
                    className: `judge-genre-fever-header`,
                    children: [(0, X.jsx)(`h2`, {
                        id: `judge-genre-fever-title`,
                        className: `judge-genre-fever-title`,
                        children: ee
                    }), (0, X.jsxs)(`button`, {
                        type: `button`,
                        className: `judge-genre-fever-back`,
                        onClick: c(r),
                        onMouseEnter: s,
                        disabled: w,
                        "aria-label": `Назад`,
                        children: [(0, X.jsx)(`span`, {
                            className: `judge-genre-fever-back-arrow`,
                            "aria-hidden": !0
                        }), (0, X.jsx)(`span`, {children: `Назад`}), (0, X.jsx)(q, {
                            button: `B`,
                            className: `judge-genre-fever-back-gp-hint`
                        })]
                    })]
                }), e ? (0, X.jsxs)(`div`, {
                    className: `judge-genre-fever-player`,
                    children: [(0, X.jsx)(`div`, {
                        className: `judge-genre-fever-avatar-wrap`,
                        children: (0, X.jsx)(`img`, {
                            className: `judge-genre-fever-avatar`,
                            src: yi(e.name),
                            alt: e.name,
                            draggable: !1
                        })
                    }), (0, X.jsxs)(`div`, {
                        className: `judge-genre-fever-player-info`,
                        children: [(0, X.jsx)(`span`, {
                            className: `judge-genre-fever-player-name`,
                            children: e.name
                        }), (0, X.jsxs)(`span`, {
                            className: `judge-genre-fever-player-current`,
                            children: [(0, X.jsx)(`span`, {
                                className: `judge-genre-fever-player-current-label`,
                                children: `очков:`
                            }), ` `, (0, X.jsx)(`span`, {
                                className: `judge-genre-fever-player-current-value`,
                                children: e.ggp
                            })]
                        })]
                    })]
                }) : null, d ? (0, X.jsx)(`p`, {
                    className: `judge-genre-fever-loading`,
                    children: `Загрузка истории аукционов…`
                }) : N ? (0, X.jsxs)(X.Fragment, {
                    children: [(0, X.jsx)(`div`, {
                        className: `judge-genre-fever-section-label`,
                        children: `Выберите аукцион`
                    }), O.length === 0 ? (0, X.jsx)(`p`, {
                        className: `judge-genre-fever-empty`,
                        children: Ml
                    }) : (0, X.jsx)(`div`, {
                        className: `judge-genre-fever-list`, children: O.map(e => {
                            let t = g === e.id, n = H(e.imageUrl);
                            return (0, X.jsxs)(`button`, {
                                type: `button`,
                                className: `judge-genre-fever-card ${t ? `judge-genre-fever-card--selected` : ``}`,
                                onClick: () => {
                                    a(f.Menu_Button_Back_Hover), _(e.id)
                                },
                                disabled: w,
                                children: [n ? (0, X.jsx)(`img`, {
                                    className: `judge-genre-fever-card-image`,
                                    src: n,
                                    alt: ``,
                                    draggable: !1
                                }) : (0, X.jsx)(`div`, {className: `judge-genre-fever-card-image judge-genre-fever-card-image--empty`}), (0, X.jsxs)(`div`, {
                                    className: `judge-genre-fever-card-info`,
                                    children: [(0, X.jsx)(`div`, {
                                        className: `judge-genre-fever-card-title`,
                                        children: e.title || `—`
                                    }), (0, X.jsxs)(`div`, {
                                        className: `judge-genre-fever-card-meta`,
                                        children: [`Ход `, e.turn, ` · `, Fl(e.createdAt)]
                                    })]
                                })]
                            }, e.id)
                        })
                    }), (0, X.jsxs)(`div`, {
                        className: `judge-genre-fever-input-row`,
                        children: [(0, X.jsx)(`span`, {
                            className: `judge-genre-fever-input-sign`,
                            children: `+`
                        }), (0, X.jsx)(`input`, {
                            className: `judge-genre-fever-input`,
                            type: `number`,
                            min: 1,
                            step: 1,
                            value: b,
                            disabled: w,
                            placeholder: `Количество очков`,
                            onChange: e => x(e.target.value),
                            onKeyDown: e => {
                                Fr(e, o), e.key === `Enter` && ne && re()
                            }
                        })]
                    })]
                }) : (0, X.jsxs)(X.Fragment, {
                    children: [(0, X.jsx)(`div`, {
                        className: `judge-genre-fever-section-label`,
                        children: `Выберите корректировку для отмены`
                    }), D.length === 0 ? (0, X.jsx)(`p`, {
                        className: `judge-genre-fever-empty`,
                        children: Ml
                    }) : (0, X.jsx)(`div`, {
                        className: `judge-genre-fever-list`,
                        children: D.map(({adjustment: e, auction: t}) => {
                            let n = v === e.id, r = e.amount > 0 ? `+` : ``;
                            return (0, X.jsx)(`button`, {
                                type: `button`,
                                className: `judge-genre-fever-card ${n ? `judge-genre-fever-card--selected` : ``}`,
                                onClick: () => {
                                    a(f.Menu_Button_Back_Hover), y(e.id)
                                },
                                disabled: w,
                                children: (0, X.jsxs)(`div`, {
                                    className: `judge-genre-fever-card-info`,
                                    children: [(0, X.jsxs)(`div`, {
                                        className: `judge-genre-fever-card-title`,
                                        children: [El(e), ` `, (0, X.jsxs)(`span`, {
                                            className: `judge-genre-fever-card-amount`,
                                            children: [r, e.amount]
                                        })]
                                    }), (0, X.jsxs)(`div`, {
                                        className: `judge-genre-fever-card-meta`,
                                        children: [t.title || `—`, ` · ход `, t.turn, ` ·`, ` `, Fl(e.createdAt)]
                                    }), e.reason ? (0, X.jsx)(`div`, {
                                        className: `judge-genre-fever-card-reason`,
                                        children: e.reason
                                    }) : null]
                                })
                            }, e.id)
                        })
                    }), (0, X.jsx)(`input`, {
                        className: `judge-genre-fever-input judge-genre-fever-input--text`,
                        type: `text`,
                        value: S,
                        disabled: w,
                        placeholder: `Причина отмены (необязательно)`,
                        onChange: e => C(e.target.value),
                        onKeyDown: e => Fr(e, o)
                    })]
                }), m ? (0, X.jsx)(`p`, {
                    className: `judge-genre-fever-error`,
                    role: `alert`,
                    children: m
                }) : null, (0, X.jsx)(`div`, {
                    className: `judge-genre-fever-actions`,
                    children: (0, X.jsxs)(`button`, {
                        type: `button`,
                        className: `judge-genre-fever-confirm ${ne ? `` : `judge-genre-fever-confirm--disabled`}`,
                        disabled: !ne,
                        onClick: () => {
                            re()
                        },
                        onMouseEnter: () => {
                            ne && a(f.Menu_Button_Back_Hover)
                        },
                        children: [ne && !w && (0, X.jsx)(q, {
                            button: `A`,
                            className: `judge-genre-fever-gp-hint`
                        }), w ? `Сохранение` : te]
                    })
                })]
            })
        })
    }, Ll = ({isOpen: e, player: t, direction: n, onClose: r, onSuccess: i}) => e ? (0, X.jsx)(Il, {
        player: t,
        direction: n,
        onClose: r,
        onSuccess: i
    }, `${t?.id ?? `none`}-${n}`) : null,
    Rl = [{value: `regular`, label: `Обычная`}, {value: `platinum`, label: `Premium`}, {
        value: `gnus`,
        label: `Гнусная`
    }, {value: `done`, label: `Завершена`}],
    zl = {regular: `Обычная`, platinum: `Premium`, gnus: `Гнусная`, done: `Завершена`};

function Bl(e) {
    return e ? e.status !== `casino` || e.casinoPhase == null ? `Не в казино` : zl[e.casinoPhase] ?? e.casinoPhase : `—`
}

var Vl = ({player: e, loading: t = !1, onClose: r, onSuccess: i}) => {
        let {play: a} = oe(), {
                onBackHover: o,
                withBackClick: s
            } = n(), [c, l] = (0, Y.useState)(() => e?.status === `casino` && e.casinoPhase != null ? e.casinoPhase : `regular`), [u, d] = (0, Y.useState)(!1), [p, m] = (0, Y.useState)(null),
            h = !!e && !t && !u, g = async () => {
                if (!(!h || !e)) {
                    a(f.Menu_Button_Back_Click), d(!0), m(null);
                    try {
                        let t = await V({...ge(e), status: `casino`, casinoPhase: c});
                        if (!t) {
                            m(`Не удалось сохранить изменения`);
                            return
                        }
                        Or(t.id), i?.(), r()
                    } catch {
                        m(`Ошибка при сохранении`)
                    } finally {
                        d(!1)
                    }
                }
            }, _ = j();
        return E(!0), k(`A`, () => {
            g()
        }, h && !_), k(`B`, () => {
            u || (a(f.Menu_Button_Back_Click), r())
        }, !u && !_), (0, X.jsx)(`div`, {
            className: `judge-casino-phase-overlay`,
            role: `dialog`,
            "aria-modal": `true`,
            "aria-labelledby": `judge-casino-phase-title`,
            children: (0, X.jsxs)(`div`, {
                className: `judge-casino-phase-panel`,
                children: [(0, X.jsxs)(`div`, {
                    className: `judge-casino-phase-header`,
                    children: [(0, X.jsx)(`h2`, {
                        id: `judge-casino-phase-title`,
                        className: `judge-casino-phase-title`,
                        children: `Фаза казино`
                    }), (0, X.jsxs)(`button`, {
                        type: `button`,
                        className: `judge-casino-phase-back`,
                        onClick: s(r),
                        onMouseEnter: o,
                        disabled: u,
                        "aria-label": `Назад`,
                        children: [(0, X.jsx)(`span`, {
                            className: `judge-casino-phase-back-arrow`,
                            "aria-hidden": !0
                        }), (0, X.jsx)(`span`, {children: `Назад`}), (0, X.jsx)(q, {
                            button: `B`,
                            className: `judge-casino-phase-back-gp-hint`
                        })]
                    })]
                }), t ? (0, X.jsx)(`p`, {
                    className: `judge-casino-phase-loading`,
                    children: `Загрузка данных игрока…`
                }) : e ? (0, X.jsxs)(X.Fragment, {
                    children: [(0, X.jsxs)(`div`, {
                        className: `judge-casino-phase-player`,
                        children: [(0, X.jsx)(`div`, {
                            className: `judge-casino-phase-avatar-wrap`,
                            children: (0, X.jsx)(`img`, {
                                className: `judge-casino-phase-avatar`,
                                src: yi(e.name),
                                alt: e.name,
                                draggable: !1
                            })
                        }), (0, X.jsxs)(`div`, {
                            className: `judge-casino-phase-player-info`,
                            children: [(0, X.jsx)(`span`, {
                                className: `judge-casino-phase-player-name`,
                                children: e.name
                            }), (0, X.jsxs)(`span`, {
                                className: `judge-casino-phase-player-current`,
                                children: [`Текущая фаза:`, ` `, (0, X.jsx)(`span`, {
                                    className: `judge-casino-phase-player-current-value`,
                                    children: Bl(e)
                                })]
                            })]
                        })]
                    }), (0, X.jsx)(`div`, {
                        className: `judge-casino-phase-section-label`,
                        children: `Выберите фазу`
                    }), (0, X.jsx)(`div`, {
                        className: `judge-casino-phase-options`,
                        children: Rl.map(({value: e, label: t}) => (0, X.jsx)(`button`, {
                            type: `button`,
                            className: `judge-casino-phase-option ${c === e ? `judge-casino-phase-option--selected` : ``}`,
                            onClick: () => {
                                a(f.Menu_Button_Back_Hover), l(e)
                            },
                            disabled: u,
                            children: t
                        }, e))
                    }), p ? (0, X.jsx)(`p`, {
                        className: `judge-casino-phase-error`,
                        role: `alert`,
                        children: p
                    }) : null, (0, X.jsx)(`div`, {
                        className: `judge-casino-phase-actions`,
                        children: (0, X.jsxs)(`button`, {
                            type: `button`,
                            className: `judge-casino-phase-confirm ${h ? `` : `judge-casino-phase-confirm--disabled`}`,
                            disabled: !h,
                            onClick: () => {
                                g()
                            },
                            onMouseEnter: () => {
                                h && a(f.Menu_Button_Back_Hover)
                            },
                            children: [h && !u && (0, X.jsx)(q, {
                                button: `A`,
                                className: `judge-casino-phase-gp-hint`
                            }), u ? `Сохранение` : `Применить`]
                        })
                    })]
                }) : (0, X.jsx)(`p`, {
                    className: `judge-casino-phase-error`,
                    role: `alert`,
                    children: `Не удалось загрузить данные игрока`
                })]
            })
        })
    }, Hl = ({isOpen: e, player: t, loading: n = !1, onClose: r, onSuccess: i}) => e ? (0, X.jsx)(Vl, {
        player: t,
        loading: n,
        onClose: r,
        onSuccess: i
    }, t?.id ?? `none`) : null,
    Ul = [`avatars/cat_1.webp`, `avatars/cat_2.webp`, `avatars/cat_3.webp`, `avatars/cat_4.webp`],
    Wl = [`Очки +`, `Premium Вращения +`, `Гнусные вращения +`, `Жанровая лихорадка +`],
    Gl = [`Очки -`, `Premium Вращения -`, `Гнусные вращения -`, `Жанровая лихорадка -`], Kl = {
        "Очки +": {kind: `stat`, field: `ggp`, direction: `add`},
        "Очки -": {kind: `stat`, field: `ggp`, direction: `subtract`},
        "Premium Вращения +": {kind: `stat`, field: `platinumChips`, direction: `add`},
        "Premium Вращения -": {kind: `stat`, field: `platinumChips`, direction: `subtract`},
        "Гнусные вращения +": {kind: `stat`, field: `gnusAvailable`, direction: `add`},
        "Гнусные вращения -": {kind: `stat`, field: `gnusAvailable`, direction: `subtract`},
        "Жанровая лихорадка +": {kind: `genre-fever`, direction: `add`},
        "Жанровая лихорадка -": {kind: `genre-fever`, direction: `remove`}
    };

function ql() {
    return Ul[Math.floor(Math.random() * Ul.length)]
}

function Jl(e) {
    return e.filter(e => e.role !== `judge`).map(e => ({
        playerId: e.id,
        name: e.name,
        status: e.status === `idle` || e.status === `content` || e.status === `casino` ? e.status : `idle`,
        canTarget: !0
    }))
}

var Yl = () => {
    let {play: e} = oe(), {data: t} = as(),
        n = (0, Y.useMemo)(() => J(ql()), []), [r, i] = (0, Y.useState)(null), [a, o] = (0, Y.useState)(!1), [s, c] = (0, Y.useState)(!1), [l, u] = (0, Y.useState)(null), [d, p] = (0, Y.useState)(null), [m, h] = (0, Y.useState)(!1), [g, _] = (0, Y.useState)(!1), [v, y] = (0, Y.useState)(!1), [b, x] = (0, Y.useState)(!1), [S, C] = (0, Y.useState)(!1), [w, T] = (0, Y.useState)(null),
        E = (0, Y.useMemo)(() => t ? Jl(t) : [], [t]), D = (0, Y.useCallback)(() => {
            i(null), o(!1), c(!1), u(null), p(null), h(!1), _(!1), y(!1), x(!1), C(!1), T(null)
        }, []), O = t => {
            e(f.Menu_Button_Back_Click);
            let n = Kl[t];
            n && (D(), i(n), o(!0))
        }, k = () => {
            e(f.Menu_Button_Back_Click), D(), i({kind: `casino-phase`}), o(!0)
        }, A = () => {
            s || S || D()
        }, j = async e => {
            if (r) {
                c(!0), u(e), o(!1), r.kind === `stat` ? h(!0) : r.kind === `genre-fever` ? _(!0) : y(!0), x(!0), T(null);
                try {
                    let t = await de(e);
                    p(t), t || T(`Не удалось загрузить данные игрока`)
                } catch {
                    T(`Ошибка при загрузке игрока`)
                } finally {
                    x(!1), c(!1)
                }
            }
        }, M = () => {
            S || D()
        }, N = async e => {
            if (!r || r.kind !== `stat` || !d) return;
            let {field: t, direction: n} = r, i = n === `add` ? e : -e,
                a = t === `gnusAvailable` ? d.gnusAvailable ?? 0 : d[t], o = t === `ggp` ? a + i : Math.max(0, a + i);
            C(!0), T(null);
            try {
                let e = await V({...ge(d), [t]: o});
                if (!e) {
                    T(`Не удалось сохранить изменения`);
                    return
                }
                Or(e.id), D()
            } catch {
                T(`Ошибка при сохранении`)
            } finally {
                C(!1)
            }
        },
        ee = r?.kind === `stat` ? r.field === `ggp` ? r.direction === `add` ? `Выберите игрока для начисления очков` : `Выберите игрока для списания очков` : r.field === `gnusAvailable` ? r.direction === `add` ? `Выберите игрока для начисления гнусных вращений` : `Выберите игрока для списания гнусных вращений` : r.direction === `add` ? `Выберите игрока для начисления жетонов` : `Выберите игрока для списания жетонов` : r?.kind === `genre-fever` ? r.direction === `add` ? `Выберите игрока для жанровой лихорадки +` : `Выберите игрока для отмены жанровой лихорадки` : r?.kind === `casino-phase` ? `Выберите игрока для смены фазы казино` : void 0,
        te = () => {
            e(f.Menu_Button_Back_Hover)
        };
    return (0, X.jsxs)(X.Fragment, {
        children: [(0, X.jsxs)(`div`, {
            className: `judge-panel`,
            children: [(0, X.jsxs)(`div`, {
                className: `judge-panel-section`,
                children: [(0, X.jsx)(`span`, {
                    className: `judge-panel-section-label`,
                    children: `Редактировать параметры`
                }), (0, X.jsx)(`div`, {className: `judge-panel-section-line`, "aria-hidden": !0})]
            }), (0, X.jsxs)(`div`, {
                className: `judge-panel-body`,
                children: [(0, X.jsxs)(`div`, {
                    className: `judge-panel-controls`,
                    children: [(0, X.jsxs)(`div`, {
                        className: `judge-panel-buttons`,
                        children: [(0, X.jsx)(`div`, {
                            className: `judge-panel-buttons-col`,
                            children: Wl.map(e => (0, X.jsx)(`div`, {
                                className: `judge-panel-button`,
                                onClick: () => O(e),
                                onMouseEnter: te,
                                children: e
                            }, e))
                        }), (0, X.jsx)(`div`, {
                            className: `judge-panel-buttons-col`,
                            children: Gl.map(e => (0, X.jsx)(`div`, {
                                className: `judge-panel-button`,
                                onClick: () => O(e),
                                onMouseEnter: te,
                                children: e
                            }, e))
                        })]
                    }), (0, X.jsx)(`div`, {
                        className: `judge-panel-button judge-panel-button--wide`,
                        onClick: k,
                        onMouseEnter: te,
                        children: `Фаза казино`
                    })]
                }), (0, X.jsx)(`img`, {className: `judge-panel-cat`, src: n, alt: ``, draggable: !1})]
            })]
        }), (0, X.jsx)(Ai, {
            isOpen: a, targets: E, submitting: s, effectDisplayName: ee, onClose: A, onConfirm: e => {
                j(e)
            }
        }), r?.kind === `stat` && l ? (0, X.jsx)(gl, {
            isOpen: m,
            player: d,
            field: r.field,
            direction: r.direction,
            loading: b,
            submitting: S,
            error: w,
            onClose: M,
            onSubmit: e => {
                N(e)
            }
        }) : null, r?.kind === `genre-fever` && l ? (0, X.jsx)(Ll, {
            isOpen: g,
            player: d,
            direction: r.direction,
            onClose: M,
            onSuccess: D
        }) : null, r?.kind === `casino-phase` && l ? (0, X.jsx)(Hl, {
            isOpen: v,
            player: d,
            loading: b,
            onClose: M,
            onSuccess: D
        }) : null]
    })
}, Xl = ({currentPlayer: e, onRequireEffectResolution: t, onCheckPendingEffectResolutions: n, onShowHistory: r}) => {
    let {
            play: i,
            playKeyboard: a
        } = oe(), {showNotification: o} = Cn(), [s, c] = (0, Y.useState)(``), [l, u] = (0, Y.useState)(!1),
        d = (0, Y.useRef)(null), p = s.trim(), m = p.length > 0 && !l, h = async () => {
            if (!m || (i(f.Menu_Button_Back_Click), n != null && await n())) return;
            u(!0);
            let e = !1;
            try {
                await ve({type: `movie`, data: {name: p}}), e = !0, Or()
            } catch (e) {
                if (console.error(`Failed to start movie:`, e), Te(e)) {
                    o(`Сначала выполните обязательное действие по эффекту`, J(`faces/angry.webp`), `Нужно действие`, void 0, f.UI_NOT_Error), t?.();
                    return
                }
                e instanceof mt && e.description && o(e.description, J(`faces/angry.webp`), `Ошибка`, void 0, f.UI_NOT_Error)
            } finally {
                e && i(f.Game_Started), u(!1)
            }
        }, g = () => {
            i(f.Menu_Button_Back_Click), r?.()
        }, _ = j(), v = te();
    return k(`A`, () => {
        h()
    }, m && !_ && !v), (0, X.jsxs)(`div`, {
        className: `game-choose movie-choose`,
        children: [(0, X.jsxs)(`div`, {
            className: `game-choose-section`,
            children: [(0, X.jsx)(`span`, {
                className: `game-choose-section-label`,
                children: `Выбор фильма`
            }), (0, X.jsx)(`div`, {className: `game-choose-section-line`, "aria-hidden": !0})]
        }), (0, X.jsxs)(`div`, {
            className: `header-container`,
            children: [(0, X.jsx)(`div`, {className: `platinum-coin-container`}), (0, X.jsx)(`div`, {
                className: `title-container`,
                children: (0, X.jsx)(`h2`, {className: `title-text`, children: `Выбор Фильма`})
            })]
        }), (0, X.jsx)(`div`, {
            className: `movie-select-container`,
            children: (0, X.jsx)(`input`, {
                ref: d,
                type: `text`,
                className: `movie-name-input`,
                placeholder: `Введите название фильма...`,
                value: s,
                onChange: e => c(e.target.value),
                onKeyDown: e => {
                    Fr(e, a), e.key === `Enter` && m && h()
                },
                autoFocus: !0
            })
        }), (0, X.jsx)(`div`, {
            className: `buttons-container`,
            children: (0, X.jsxs)(`div`, {
                className: `buttons-row`,
                children: [(0, X.jsxs)(`div`, {
                    className: `button ${m ? `` : `button-disabled`}`,
                    onClick: () => {
                        h()
                    },
                    onMouseEnter: () => {
                        m && i(f.Menu_Button_Back_Hover)
                    },
                    children: [!l && (0, X.jsx)(q, {
                        button: `A`,
                        className: `button-gamepad-hint`
                    }), l ? `Начинаем` : `Начать`]
                }), r && (0, X.jsx)(`div`, {
                    className: `button ${l ? `button-disabled` : ``}`,
                    onClick: l ? void 0 : g,
                    onMouseEnter: () => {
                        l || i(f.Menu_Button_Back_Hover)
                    },
                    children: `История`
                }), (0, X.jsx)(ul, {playerId: e?.id, onCheckPendingEffectResolutions: n})]
            })
        })]
    })
}, Zl = `Вы действительно хотите начать аукцион?`, Ql = ({
                                                             player: e,
                                                             onShowSlots: t,
                                                             onShowHistory: r,
                                                             onRequireEffectResolution: i,
                                                             onCheckPendingEffectResolutions: a
                                                         }) => {
    let {play: o, playUntilEnd: s} = oe(), {
            onBackHover: c,
            withBackClick: l
        } = n(), {showNotification: u} = Cn(), [d, p] = (0, Y.useState)(!1), [m, h] = (0, Y.useState)(!1), g = () => {
            o(f.Menu_Button_Back_Click), t?.()
        }, _ = () => {
            o(f.Menu_Button_Back_Click), r?.()
        }, v = e?.platinumChips ?? 0, y = e?.gnusAvailable ?? 0, b = e?.casinoPhase === `done`,
        x = e?.status === `casino` && e.casinoPhase != null && e.casinoPhase !== `done`, S = !b && (v > 0 || y > 0),
        C = b ? !1 : S || x, w = async () => {
            d || C || (o(f.Menu_Button_Back_Click), !(a != null && await a()) && h(!0))
        }, T = async () => {
            if (!d) {
                o(f.Menu_Button_Back_Click), p(!0);
                try {
                    await _e(), await s(f.Game_Done), h(!1), Or()
                } catch (e) {
                    if (console.error(`Failed to finish turn:`, e), Te(e)) {
                        u(`Сначала выполните обязательное действие по эффекту`, J(`faces/angry.webp`), `Нужно действие`, void 0, f.UI_NOT_Error), i?.();
                        return
                    }
                    if (e instanceof mt && e.statusCode === 400) {
                        u(e.description?.includes(`pending`) || e.description?.includes(`casino`) ? e.description : `Сначала примите выпавший пункт колеса`, J(`faces/angry.webp`), `Ошибка`, void 0, f.UI_NOT_Error);
                        return
                    }
                    let t = bt(e);
                    t && u(t, J(`faces/angry.webp`), `Ошибка`, void 0, f.UI_NOT_Error)
                } finally {
                    p(!1)
                }
            }
        }, D = j();
    E(m);
    let O = te(), A = !D && !O;
    return k(`A`, () => {
        T()
    }, m && !d && !D), k(`A`, () => {
        w()
    }, !m && !d && b && !C && A), k(`A`, () => {
        g()
    }, !m && !d && !b && A), k(`X`, () => {
        r && _()
    }, !m && !d && !!r && A), k(`B`, () => {
        d || (h(!1), o(f.Menu_Button_Back_Click))
    }, m && !D), (0, X.jsxs)(X.Fragment, {
        children: [m && (0, X.jsx)(`div`, {
            className: `turn-finish-confirm-overlay`, role: `presentation`, onClick: () => {
                d || h(!1)
            }, children: (0, X.jsxs)(`div`, {
                className: `turn-finish-confirm-panel`,
                role: `dialog`,
                "aria-modal": `true`,
                "aria-labelledby": `turn-finish-auction-confirm-title`,
                onClick: e => e.stopPropagation(),
                children: [(0, X.jsxs)(`div`, {
                    className: `turn-finish-confirm-header`,
                    children: [(0, X.jsx)(`h3`, {
                        id: `turn-finish-auction-confirm-title`,
                        className: `turn-finish-confirm-title`,
                        children: `Аукцион`
                    }), (0, X.jsxs)(`button`, {
                        type: `button`,
                        className: `turn-finish-confirm-back`,
                        onClick: l(() => h(!1)),
                        onMouseEnter: c,
                        disabled: d,
                        "aria-label": `Назад`,
                        children: [(0, X.jsx)(`span`, {
                            className: `turn-finish-confirm-back-arrow`,
                            "aria-hidden": !0
                        }), (0, X.jsx)(`span`, {children: `Назад`}), (0, X.jsx)(q, {
                            button: `B`,
                            className: `turn-finish-back-gp-hint`
                        })]
                    })]
                }), (0, X.jsx)(`p`, {
                    className: `turn-finish-confirm-text`,
                    children: Zl
                }), (0, X.jsx)(`div`, {
                    className: `turn-finish-confirm-actions`,
                    children: (0, X.jsxs)(`button`, {
                        type: `button`,
                        className: `turn-finish-confirm-btn ${d ? `turn-finish-confirm-btn--disabled` : ``}`,
                        disabled: d,
                        onClick: () => void T(),
                        onMouseEnter: () => {
                            d || o(f.Menu_Button_Back_Hover)
                        },
                        children: [!d && (0, X.jsx)(q, {
                            button: `A`,
                            className: `turn-finish-gp-hint`
                        }), d ? `Обработка…` : `Да`]
                    })
                })]
            })
        }), (0, X.jsxs)(`div`, {
            className: `turn-finish`,
            children: [(0, X.jsxs)(`div`, {
                className: `slots-section`,
                children: [(0, X.jsx)(`span`, {
                    className: `slots-section-label`,
                    children: `Текущая игра`
                }), (0, X.jsx)(`div`, {className: `slots-section-line`, "aria-hidden": !0})]
            }), (0, X.jsxs)(`div`, {
                className: `turn-finish-main`,
                children: [(0, X.jsx)(`div`, {
                    className: `header-container`,
                    children: (0, X.jsx)(`div`, {
                        className: `title-container`,
                        children: (0, X.jsx)(`h2`, {className: `title-text`, children: `Завершение хода`})
                    })
                }), (0, X.jsx)(`div`, {
                    className: `content-container`,
                    children: (0, X.jsx)(`div`, {
                        className: `confirmation-container`,
                        children: (0, X.jsxs)(`div`, {
                            className: `description-container`,
                            children: [(0, X.jsx)(`p`, {children: `Прокрутите все доступные колеса в казино.`}), v === 0 && !b && (0, X.jsx)(`p`, {children: `Если у вас нет донатных колес, нажмите «Завершить» в казино.`}), (0, X.jsx)(`p`, {children: `После этого начните аукцион.`})]
                        })
                    })
                }), (0, X.jsxs)(`div`, {
                    className: `confirmation-buttons`,
                    children: [b ? (0, X.jsxs)(X.Fragment, {
                        children: [(0, X.jsx)(`span`, {
                            className: `turn-finish-next-step`,
                            children: `В Казино`
                        }), (0, X.jsx)(`span`, {
                            className: `turn-finish-next-arrow`,
                            "aria-hidden": !0,
                            children: `→`
                        }), (0, X.jsxs)(`button`, {
                            type: `button`,
                            className: `turn-finish-btn`,
                            onClick: w,
                            disabled: d || C,
                            onMouseEnter: () => o(f.Menu_Button_Back_Hover),
                            title: C ? S ? `Перед завершением хода прокрутите все колеса` : `Завершите все фазы колеса перед окончанием хода` : void 0,
                            children: [!d && !C && (0, X.jsx)(q, {
                                button: `A`,
                                className: `turn-finish-gp-hint`
                            }), d ? `Обработка` : `Аукцион`]
                        })]
                    }) : (0, X.jsxs)(X.Fragment, {
                        children: [(0, X.jsxs)(`button`, {
                            type: `button`,
                            className: `turn-finish-btn`,
                            onClick: g,
                            onMouseEnter: () => o(f.Menu_Button_Back_Hover),
                            children: [!d && (0, X.jsx)(q, {button: `A`, className: `turn-finish-gp-hint`}), `В Казино`]
                        }), (0, X.jsx)(`span`, {
                            className: `turn-finish-next-arrow`,
                            "aria-hidden": !0,
                            children: `→`
                        }), (0, X.jsx)(`span`, {className: `turn-finish-next-step`, children: `Аукцион`})]
                    }), r && (0, X.jsxs)(X.Fragment, {
                        children: [(0, X.jsx)(`div`, {
                            className: `turn-finish-history-separator`,
                            "aria-hidden": !0
                        }), (0, X.jsxs)(`button`, {
                            type: `button`,
                            className: `turn-finish-btn turn-finish-history-btn`,
                            onClick: _,
                            onMouseEnter: () => o(f.Menu_Button_Back_Hover),
                            children: [(0, X.jsx)(q, {button: `X`, className: `turn-finish-gp-hint`}), `История`]
                        })]
                    }), (0, X.jsx)(ul, {playerId: e?.id, onCheckPendingEffectResolutions: a})]
                })]
            })]
        })]
    })
}, $l = ({
             children: e,
             duration: t = 2e3,
             onComplete: n,
             onProgress: r,
             onHoldStart: i,
             onHoldEnd: a,
             className: o = ``,
             progressClassName: s = ``,
             disabled: c = !1,
             maxWidth: l
         }) => {
    let [u, d] = (0, Y.useState)(!1), [f, p] = (0, Y.useState)(0), [m, h] = (0, Y.useState)(!1),
        g = (0, Y.useRef)(null), _ = (0, Y.useRef)(null), v = (0, Y.useRef)(!1), y = (0, Y.useRef)(n),
        b = (0, Y.useRef)(r);
    (0, Y.useEffect)(() => {
        y.current = n, b.current = r
    }, [n, r]);
    let x = () => {
        if (m && !c) return;
        let e = u;
        v.current = !1, d(!1), _.current = null, p(0), c && h(!1), g.current &&= (clearInterval(g.current), null), e && a?.()
    };
    (0, Y.useEffect)(() => {
        if (u && !c && !m) return v.current = !0, _.current === null && (_.current = performance.now()), g.current = setInterval(() => {
            if (!v.current) {
                g.current &&= (clearInterval(g.current), null);
                return
            }
            let e = performance.now(), n = e - (_.current ?? e), r = Math.min(n / t * 100, 100);
            p(r), b.current && b.current(r), r >= 100 && (p(100), b.current && b.current(100), g.current &&= (clearInterval(g.current), null), v.current && (h(!0), y.current && y.current()))
        }, 16), () => {
            v.current = !1, g.current &&= (clearInterval(g.current), null)
        };
        v.current = !1, _.current = null
    }, [u, t, c]), (0, Y.useEffect)(() => (c && (v.current = !1, d(!1), p(0), h(!1), _.current = null, g.current &&= (clearInterval(g.current), null)), () => {
        v.current = !1, h(!1), g.current &&= (clearInterval(g.current), null)
    }), [c]);
    let S = e => {
        !c && !m && (i?.(e.currentTarget), v.current = !0, d(!0), p(0), h(!1))
    }, C = () => {
        !c && !m && (v.current = !0, d(!0), p(0), h(!1))
    }, w = () => {
        x()
    };
    return (0, X.jsxs)(`div`, {
        className: `hold-progress-wrapper ${o}`,
        onMouseDown: S,
        onMouseUp: w,
        onMouseLeave: () => {
            x()
        },
        onTouchStart: C,
        onTouchEnd: w,
        children: [(0, X.jsx)(`div`, {
            className: `progress-fill ${s}`,
            style: {width: m ? l ? `${l / 16}rem` : `100%` : u ? l ? `${f / 100 * l / 16}rem` : `${f}%` : l ? `0` : `0%`}
        }), e]
    })
};

function eu(e = !0) {
    return Gn({queryKey: Er.effectList, queryFn: Re, enabled: e, staleTime: 1 / 0})
}

function tu(e = !0) {
    let {isAuthenticated: t} = ut();
    return Gn({queryKey: Er.player, queryFn: Ae, enabled: e && t})
}

var nu = new Set([`genre_expert`, `genre_fever`]);

function ru(e, t) {
    if (!nu.has(e.name)) return;
    let n = e.data?.publicParams?.selectedGenre;
    if (typeof n == `string` && n.length > 0) return n;
    if (!t?.activeEffects?.length) return;
    let r = t.activeEffects.find(t => e.id != null && t.id === e.id || e.id == null && t.name === e.name)?.data?.publicParams?.selectedGenre;
    if (typeof r == `string` && r.length > 0) return r
}

function iu(e, t) {
    let n = ru(e, t);
    if (n != null) return Xo(n)
}

function au(e, t) {
    if (e.data?.expiresAt) return e.data.expiresAt;
    if (t?.activeEffects?.length) return t.activeEffects.find(t => e.id != null && t.id === e.id || e.id == null && t.name === e.name)?.data?.expiresAt
}

function ou(e, t) {
    if (ti(e)) return !0;
    let n = t?.find(t => t.name === e.name);
    return ti(n)
}

function su(e, t, n, r, i) {
    let a = i ?? t, o = jc(Mc(e.effect, r)), s = n.filter(e => o.includes(e.effect.name)).map(e => ({
            name: e.effect.name,
            displayName: e.effect.displayName,
            imageUrl: Yn(e.effect.name, `static`)
        })), c,
        l = t?.effects?.some(t => t.name === e.effect.name) || t?.activeEffects?.some(t => t.name === e.effect.name);
    if (t && l && rt(e.effect.name, {effect: e.effect, effectCatalog: r})) {
        let n = pt(e.effect.name, {effectCooldowns: t.effectCooldowns, effectInstanceId: e.effect.id});
        n !== void 0 && (c = se(n))
    }
    return {
        effectName: e.effect.name,
        displayName: e.effect.displayName,
        description: e.effect.description,
        effect: e.effect,
        blockedEffects: s,
        allowedStatusDisplays: Bc(e.effect),
        cooldownLabel: c,
        selectedGenreLabel: iu(e.effect, a),
        expiresAt: au(e.effect, a),
        isGlobalTarget: ou(e.effect, r)
    }
}

var cu = 6, lu = 44;

function uu(e, t, n) {
    if (rt(e.name, {effectCatalog: n})) return Ue(pt(e.name, {
        effectCooldowns: t.effectCooldowns,
        effectInstanceId: e.id
    }))
}

function du(e) {
    return e.behavior === `inventory` || e.type === `active`
}

function fu(e, t, n) {
    let r = jc(Mc(e, n));
    return r.length === 0 ? !1 : t.activeEffects?.some(e => r.includes(e.name)) ?? !1
}

function pu(e, t, n) {
    return du(e.effect) ? e.cooldownLabel ? !0 : fu(e.effect, t, n) : !1
}

function mu(e, t, n) {
    return e.isActive ? 0 : e.canActivate ? 1 : pu(e, t, n) ? 3 : 2
}

function hu(e, t) {
    let n = e.effects || [], r = e.activeEffects || [], i = new Set(r.map(e => e.name)),
        a = new Set(n.map(e => e.name)), o = [], s = 0;
    for (let e of r) a.has(e.name) || (o.push({
        id: `item-${s + 1}`,
        name: e.displayName,
        description: e.description,
        image: Yn(e.name, `static`),
        effect: e,
        canActivate: !1,
        isActive: !0
    }), s++);
    for (let r of n) {
        let n = i.has(r.name), a = uu(r, e, t), c = du(r);
        a ? c = !1 : c && r.allowedStatuses !== null && r.allowedStatuses !== void 0 && (c = r.allowedStatuses.includes(e.status));
        let l = jc(Mc(r, t));
        c && l.length > 0 && e.activeEffects?.some(e => l.includes(e.name)) && (c = !1), c && !r.id && (c = !1), o.push({
            id: r.id ?? `item-${s + 1}`,
            name: r.displayName,
            description: r.description,
            image: Yn(r.name, `static`),
            effect: r,
            canActivate: c,
            isActive: n,
            cooldownLabel: a
        }), s++
    }
    return o.map((e, t) => ({item: e, originalIndex: t})).sort((n, r) => {
        let i = mu(n.item, e, t) - mu(r.item, e, t);
        return i === 0 ? n.originalIndex - r.originalIndex : i
    }).map(({item: e}) => e)
}

var gu = ({player: e, currentPlayer: t, allowActivation: n = !0, onEffectHover: r}) => {
    let {
            play: i,
            fadeOut: a,
            updateSoundPan: o
        } = oe(), {data: s} = tu(e == null), {data: c} = eu(), {showNotification: l} = Cn(), u = e ?? s ?? null,
        d = (0, Y.useMemo)(() => u ? hu(u, c ?? void 0) : [], [u, c]), [p, m] = (0, Y.useState)(!1), [h, g] = (0, Y.useState)(null),
        _ = (0, Y.useRef)(0), [y, b] = (0, Y.useState)(!1), [x, S] = (0, Y.useState)(!1), C = (0, Y.useRef)(null),
        w = (0, Y.useRef)(0), T = (0, Y.useRef)(0), D = (0, Y.useRef)(e?.id), O = (0, Y.useRef)(null),
        M = (0, Y.useRef)(null), ee = (0, Y.useRef)(null);
    (0, Y.useEffect)(() => () => {
        ee.current !== null && clearTimeout(ee.current), r?.(null)
    }, [r]);
    let P = j(), re = te(), ie = N(), [I, ae] = (0, Y.useState)(!1), [L, se] = (0, Y.useState)(0),
        R = d.length > 0 ? Math.min(Math.max(0, L), d.length - 1) : 0, z = e == null || e.userId === t?.userId,
        ce = n && z, le = d.length > 0 && !P && ie.connected, ue = le && !re;
    E(I && le);
    let de = (0, Y.useCallback)(() => {
        O.current = null, g(null), r?.(null)
    }, [r]), fe = (0, Y.useCallback)(() => {
        ee.current !== null && (clearTimeout(ee.current), ee.current = null)
    }, []), pe = (0, Y.useCallback)(() => {
        fe(), ee.current = setTimeout(() => {
            ee.current = null;
            let e = O.current?.effect.name;
            e && ((C.current?.querySelector(`[data-inventory-effect="${e}"]`))?.matches(`:hover`) || de())
        }, 80)
    }, [fe, de]), B = (0, Y.useCallback)(() => {
        let e = C.current?.querySelectorAll(`.inventory-item-art[data-inventory-effect]`);
        if (e?.length) for (let n of e) {
            if (!n.matches(`:hover`)) continue;
            let e = n.dataset.inventoryEffect;
            if (!e) continue;
            let a = d.find(t => t.effect.name === e);
            if (a) {
                fe(), O.current?.effect.name !== a.effect.name && (i(f.Menu_Item_Hover, {panElement: n}), O.current = a, g(a.effect.name), r?.(su(a, t, d, c ?? void 0, u)));
                return
            }
        }
    }, [fe, t, c, d, u, r, i]);
    (0, Y.useEffect)(() => {
        D.current = e?.id
    }, [e?.id]);
    let V = async e => {
        let t = d.find(t => t.id === e);
        if (t && t.canActivate && (t.effect.behavior === `inventory` || t.effect.type === `active`)) {
            if (!t.effect.id) {
                console.error(`Cannot activate effect without instance id:`, t.effect.name);
                return
            }
            await new Promise(e => setTimeout(e, 300));
            try {
                i(f.Menu_Item_Activated);
                let e = await We({effectInstanceId: t.effect.id});
                if (e?.success && e.name) {
                    let {message: n, imagePath: r} = ur(t.effect, e);
                    l(n, J(r), void 0, void 0, f.UI_NOT_Activated)
                }
                Or()
            } catch (e) {
                console.error(`Failed to activate effect:`, e)
            } finally {
                M.current = null, _.current = 0
            }
        }
    }, me = (e, n) => {
        fe(), O.current?.effect.name !== e.effect.name && (i(f.Menu_Item_Hover, {panElement: n}), O.current = e, g(e.effect.name), r?.(su(e, t, d, c ?? void 0, u)))
    }, he = e => {
        let t = e.relatedTarget;
        t instanceof Node && e.currentTarget.contains(t) || pe()
    };
    k(`DPadLeft`, () => {
        ae(!0), se(0)
    }, ue && !I), k(`DPadLeft`, () => {
        se(e => Math.max(0, e - 1))
    }, le && I), k(`DPadRight`, () => {
        se(e => Math.min(d.length - 1, e + 1))
    }, le && I), k(`B`, () => {
        ae(!1), de()
    }, le && I), k(`LB`, () => {
        Ee()
    }, le && I && y), k(`RB`, () => {
        De()
    }, le && I && x), (0, Y.useEffect)(() => {
        if (!I || P) return;
        let e = d[R];
        if (!e) return;
        let t = C.current?.querySelector(`.inventory-items-segment[data-inventory-effect="${e.effect.name}"]`);
        t?.scrollIntoView({inline: `center`, block: `nearest`, behavior: `smooth`});
        let n = t?.querySelector(`.inventory-item-art`);
        n && me(e, n)
    }, [I, R, d, P]);
    let ge = () => {
        let e = d[R];
        return e ? (C.current?.querySelector(`.inventory-items-segment[data-inventory-effect="${e.effect.name}"]`))?.querySelector(`.hold-progress-wrapper`) ?? null : null
    }, _e = (0, Y.useRef)(null);
    F(`A`, () => {
        if (!d[R]?.canActivate) return;
        let e = ge();
        e && (e.dispatchEvent(new MouseEvent(`mousedown`, {bubbles: !0, button: 0})), A(1200, {
            strongMagnitude: .55,
            weakMagnitude: .3
        }), _e.current != null && clearInterval(_e.current), _e.current = window.setInterval(() => {
            A(700, {strongMagnitude: .6, weakMagnitude: .35})
        }, 600))
    }, () => {
        let e = ge();
        e && e.dispatchEvent(new MouseEvent(`mouseup`, {
            bubbles: !0,
            button: 0
        })), _e.current != null && (clearInterval(_e.current), _e.current = null), ne()
    }, le && I && ce);
    let ve = (0, Y.useCallback)((e, t, n) => {
        b(e > 1), S(e + n < t - 1)
    }, []), ye = () => C.current?.querySelector(`.ScrollbarsCustom-Scroller`), be = (0, Y.useCallback)(() => {
        let e = ye();
        if (!e) {
            b(!1), S(!1);
            return
        }
        ve(e.scrollLeft, e.scrollWidth, e.clientWidth)
    }, [ve]);
    (0, Y.useEffect)(() => {
        let e = ye();
        e && (e.scrollLeft = 0, w.current = 0, T.current = 0);
        let t = requestAnimationFrame(() => {
            be()
        });
        return () => cancelAnimationFrame(t)
    }, [d, e?.id, be]);
    let xe = d.length > cu, Se = () => C.current?.querySelector(`.inventory-item-container`)?.offsetWidth ?? 0,
        Ce = (0, Y.useCallback)(e => {
            let t = Se();
            if (t <= 0) return;
            let n = Math.round(e / t);
            n !== T.current && (T.current = n, i(f.Menu_Item_Next))
        }, [i]);
    (0, Y.useEffect)(() => {
        let e = C.current;
        if (!e) return;
        let t = !1, n = 0, r = 0, i = () => e.querySelector(`.ScrollbarsCustom-Scroller`),
            a = e => e instanceof HTMLElement && !!e.closest(`.inventory-items-scroll .ScrollbarsCustom-TrackX, .inventory-items-scroll .ScrollbarsCustom-ThumbX, .inventory-items-scroll .svg-scrollbar-thumb`),
            o = o => {
                if (o.button !== 2 || !a(o.target)) return;
                let s = i();
                if (!(!s || s.scrollWidth <= s.clientWidth)) {
                    o.preventDefault(), t = !0, n = o.clientX, r = s.scrollLeft;
                    try {
                        e.setPointerCapture(o.pointerId)
                    } catch {
                    }
                }
            }, s = e => {
                if (!t) return;
                let a = i();
                if (!a) return;
                let o = Math.max(0, a.scrollWidth - a.clientWidth), s = o / Math.max(1, a.clientWidth), c = e.clientX - n;
                a.scrollLeft = Math.min(o, Math.max(0, r + c * s))
            }, c = n => {
                if (t) {
                    t = !1;
                    try {
                        e.releasePointerCapture(n.pointerId)
                    } catch {
                    }
                }
            }, l = e => {
                a(e.target) && e.preventDefault()
            };
        return e.addEventListener(`pointerdown`, o), e.addEventListener(`pointermove`, s), e.addEventListener(`pointerup`, c), e.addEventListener(`pointercancel`, c), e.addEventListener(`contextmenu`, l), () => {
            e.removeEventListener(`pointerdown`, o), e.removeEventListener(`pointermove`, s), e.removeEventListener(`pointerup`, c), e.removeEventListener(`pointercancel`, c), e.removeEventListener(`contextmenu`, l)
        }
    }, [d.length]);
    let we = (0, Y.useCallback)(() => {
        let e = ye();
        if (!e) return;
        let {scrollLeft: t, scrollWidth: n, clientWidth: r} = e;
        ve(t, n, r), !p && t > 0 && m(!0);
        let i = t !== w.current;
        w.current = t, i && (Ce(t), requestAnimationFrame(() => {
            B()
        }))
    }, [Ce, p, B, ve]), Te = e => {
        let t = ye();
        if (!t) return;
        let n = Se();
        if (n <= 0) return;
        let r = Math.max(0, t.scrollWidth - t.clientWidth), i = Math.min(r, Math.max(0, t.scrollLeft + e * n));
        t.scrollTo({left: i, behavior: `smooth`})
    }, Ee = () => {
        y && Te(-1)
    }, De = () => {
        x && Te(1)
    }, Oe = e => {
        let t = e.currentTarget.closest(`.ScrollbarsCustom-Scroller`);
        if (!t || t.scrollWidth <= t.clientWidth) return;
        let n = Se();
        if (n <= 0) return;
        let r = e.deltaY > 0 ? 1 : -1, i = Math.max(0, t.scrollWidth - t.clientWidth),
            a = Math.min(i, Math.max(0, t.scrollLeft + r * n));
        t.scrollTo({left: a, behavior: `smooth`}), e.preventDefault()
    }, ke = e => {
        switch (e.effect.impact) {
            case`positive`:
                return `inventory-item-impact-positive`;
            case`negative`:
                return `inventory-item-impact-negative`;
            default:
                return `inventory-item-impact-neutral`
        }
    }, Ae = e => 44, je = (0, Y.useCallback)(e => {
        _.current = e;
        let t = M.current;
        t && o(f.Menu_Item_Press, v(t, e, lu))
    }, [o]), H = e => {
        M.current = e, i(f.Menu_Item_Press, {pan: v(e, 0, lu)})
    }, Me = () => {
        let e = _.current, t = M.current;
        e > 0 && e < 100 && (a(f.Menu_Item_Press, .1), i(f.Menu_Item_Unpress, {panElement: t ?? void 0})), e < 100 && (M.current = null, _.current = 0)
    }, Ne = (e, t) => {
        let n = !!e.cooldownLabel && !e.isActive, r = h == null ? void 0 : d.find(e => e.effect.name === h),
            i = r != null && r.effect.name !== e.effect.name && !n && !e.isActive && Fc(r.effect, e.effect, c ?? void 0),
            a = !e.isActive && !n && !i && u != null && fu(e.effect, u, c ?? void 0);
        return (0, X.jsxs)(`div`, {
            className: `inventory-item-art`,
            "data-inventory-effect": e.effect.name,
            onMouseEnter: t => me(e, t.currentTarget),
            onMouseLeave: he,
            children: [(0, X.jsx)(bc, {
                src: e.image,
                className: `inventory-item-image`,
                skeletonWidth: `100%`,
                skeletonHeight: `100%`,
                skeletonVariant: `rect`
            }), i && (0, X.jsx)(`div`, {
                className: `inventory-item-lock`,
                children: (0, X.jsx)(`img`, {
                    src: J(`icons/lock.svg`),
                    alt: `Lock`,
                    className: `inventory-item-lock-icon`
                })
            }), a && (0, X.jsx)(`div`, {
                className: `inventory-item-lock`,
                children: (0, X.jsx)(`img`, {
                    src: J(`icons/lock.svg`),
                    alt: `Lock`,
                    className: `inventory-item-lock-icon`
                })
            }), n && (0, X.jsxs)(`div`, {
                className: `inventory-item-lock inventory-item-cooldown-lock`,
                children: [(0, X.jsx)(`img`, {
                    src: J(`icons/lock.svg`),
                    alt: `Lock`,
                    className: `inventory-item-lock-icon`
                }), (0, X.jsx)(`div`, {className: `inventory-item-cooldown-label`, children: e.cooldownLabel})]
            }), !e.isActive && t && e.canActivate && !n && (0, X.jsx)(`div`, {
                className: `inventory-item-activation-hint`,
                children: `Зажмите чтобы активировать`
            })]
        })
    }, U = r => !n || e?.userId !== t?.userId || !r.canActivate ? (0, X.jsxs)(`div`, {
        className: `inventory-item-container ${r.isActive ? `inventory-item-active` : ``}`,
        children: [Ne(r, !1), (0, X.jsx)(`div`, {
            className: `inventory-item-name`,
            children: r.name
        }), (0, X.jsx)(`div`, {className: `inventory-item-type ${ke(r)}`})]
    }) : (0, X.jsxs)($l, {
        duration: 1e3,
        onComplete: () => V(r.id),
        className: `inventory-item-container cursor-pointer ${r.isActive ? `inventory-item-active` : ``}`,
        maxWidth: Ae(r),
        onHoldStart: H,
        onHoldEnd: Me,
        onProgress: je,
        children: [(0, X.jsxs)(`div`, {
            children: [Ne(r, !0), (0, X.jsx)(`div`, {
                className: `inventory-item-name`,
                children: r.name
            })]
        }), (0, X.jsx)(`div`, {className: `inventory-item-type ${ke(r)}`})]
    }, `${r.id}-${r.effect.name}-${r.canActivate}-${r.isActive}`);
    return (0, X.jsxs)(`div`, {
        className: `inventory`,
        children: [(0, X.jsxs)(`div`, {
            className: `inventory-title-section`,
            children: [(0, X.jsxs)(`span`, {
                className: `inventory-title`,
                children: [`Эффекты [`, (0, X.jsx)(`span`, {
                    className: `inventory-title-count`,
                    children: d.length
                }), `]`]
            }), ue && !I && (0, X.jsxs)(`span`, {
                className: `inventory-title-gp-hint`,
                children: [(0, X.jsx)(q, {button: `DPadLeft`}), ` фокус`]
            }), (0, X.jsx)(`div`, {className: `inventory-title-line`, "aria-hidden": !0})]
        }), (0, X.jsxs)(`div`, {
            className: `inventory-content`,
            children: [xe && y && (0, X.jsx)(`button`, {
                type: `button`,
                className: `inventory-nav-button inventory-nav-left is-visible`,
                onClick: Ee,
                "aria-label": `Предыдущие эффекты`,
                children: le && I && (0, X.jsx)(q, {button: `LB`, className: `inventory-nav-gp-hint`})
            }), xe && x && (0, X.jsx)(`button`, {
                type: `button`,
                className: `inventory-nav-button inventory-nav-right is-visible`,
                onClick: De,
                "aria-label": `Следующие эффекты`,
                children: le && I && (0, X.jsx)(q, {button: `RB`, className: `inventory-nav-gp-hint`})
            }), (0, X.jsx)(`div`, {
                ref: C,
                className: `inventory-items-scroll-wrap`,
                children: (0, X.jsx)(ao, {
                    direction: `horizontal`,
                    className: `inventory-items-scroll`,
                    onUpdate: we,
                    children: (0, X.jsx)(`div`, {
                        className: `inventory-items`, onWheel: Oe, children: d.map((e, t) => {
                            let n = le && I && t === R;
                            return (0, X.jsxs)(`div`, {
                                className: `inventory-items-segment${n ? ` inventory-items-segment--gp-focused` : ``}`,
                                "data-inventory-effect": e.effect.name,
                                children: [U(e), n && ce && e.canActivate && (0, X.jsx)(q, {
                                    button: `A`,
                                    className: `inventory-segment-gp-hint`
                                })]
                            }, e.id)
                        })
                    })
                })
            })]
        })]
    })
}, _u = ({text: e, speed: t = 50, startDelay: n = 0, showCursor: r = !0, punctuationDelay: i = 300}) => {
    let [a, o] = (0, Y.useState)(``), [s, c] = (0, Y.useState)(!1);
    return (0, Y.useEffect)(() => {
        if (!e) return;
        o(``), c(!0);
        let r = setTimeout(() => {
            let n = 0, r = () => {
                n < e.length ? (o(e.slice(0, n + 1)), n++, e[n - 1] === `.` && n < e.length ? setTimeout(r, t + i) : setTimeout(r, t)) : c(!1)
            };
            r()
        }, n);
        return () => {
            clearTimeout(r)
        }
    }, [e, t, n, i]), {displayedText: a, isTyping: s, showCursor: r && s}
}, vu = `intro_block_completed`, yu = () => {
    try {
        return localStorage.getItem(vu) === `true`
    } catch {
        return !1
    }
}, bu = ({fadeOut: e, onContinue: t, isAuthenticated: n, introBootReady: r = !0}) => {
    let a = ft(), {play: o, stopAll: s} = oe(),
        c = _c(), [l, u] = (0, Y.useState)(!1), [d, p] = (0, Y.useState)(!1), [m, h] = (0, Y.useState)(``), [g, _] = (0, Y.useState)(yu()), [v, y] = (0, Y.useState)(!1), [b, x] = (0, Y.useState)(!1), {displayedText: S} = _u({
            text: ``,
            speed: 60,
            startDelay: 500,
            showCursor: !1
        }), {displayedText: C, showCursor: w, isTyping: T} = _u({
            text: ``,
            speed: 60,
            startDelay: 2520,
            showCursor: !0,
            punctuationDelay: 800
        });
    (0, Y.useEffect)(() => {
    }, [g]);
    let E = () => {
        i(), s(), o(f.Menu_Start_Melody), o(f.Menu_Button_Back_Click), t()
    }, D = () => {
        if (i(), o(f.Menu_Start_Melody), n === !1) {
            s(), t();
            return
        }
        p(!0), setTimeout(() => {
            u(!0), p(!1)
        }, 300)
    }, O = e => t => {
        (t.key === `Enter` || t.key === ` `) && (t.preventDefault(), e())
    }, A = te(), j = !v && r && !A, M = !v && !1;
    return k(`A`, E, j), k(`A`, D, M), (0, X.jsxs)(`div`, {
        className: `intro-block ${e ? `fade-out` : ``}`,
        children: [v && (0, X.jsx)(`img`, {
            src: J(`icons/24_hours.webp`),
            alt: `timer`,
            className: `timer-icon ${b ? `timer-icon-fade-out` : ``}`
        }), v ? (0, X.jsxs)(`div`, {
            className: `intro-timer-container ${b ? `timer-fade-out` : ``}`,
            children: [(0, X.jsxs)(`div`, {
                className: `timer-title-container`,
                children: [(0, X.jsx)(`img`, {
                    src: J(c === `lighttheme` ? `icons/left_decor_NV.webp` : `icons/left_decor.webp`),
                    alt: `Left_decor`,
                    className: `decor-icon`
                }), (0, X.jsx)(`h2`, {
                    className: `timer-title-text`,
                    children: `До открытия сайта`
                }), (0, X.jsx)(`img`, {
                    src: J(c === `lighttheme` ? `icons/right_decor_NV.webp` : `icons/right_decor.webp`),
                    alt: `Right_decor`,
                    className: `decor-icon`
                })]
            }), (0, X.jsx)(`div`, {className: `timer-time`, children: m})]
        }) : (0, X.jsx)(`div`, {
            className: `intro-start-container intro-start-fade-in`,
            children: r ? (0, X.jsx)(`div`, {
                className: `intro-start-hitbox`,
                role: `button`,
                tabIndex: 0,
                onClick: E,
                onKeyDown: O(E),
                children: (0, X.jsxs)(`p`, {
                    className: `intro-start-prompt`,
                    children: [!a && (0, X.jsx)(q, {
                        button: `A`,
                        className: `intro-start-gamepad-hint`
                    }), `Нажмите чтобы начать`]
                })
            }) : (0, X.jsx)(`p`, {className: `intro-start-prompt`, children: `Загрузка...`})
        })]
    })
}, Q = function (e) {
    return e.COMPLETED = `completed`, e.DROPPED = `dropped`, e.REROLLED = `rerolled`, e.PLAYING = `playing`, e
}({});

function xu(e) {
    if (!e) return 0;
    let t = Number(e.timerAccumulatedSec) || 0;
    if (!e.timerStartedAt) return t;
    let n = Date.parse(e.timerStartedAt);
    return Number.isNaN(n) ? t : t + Math.floor((Date.now() - n) / 1e3)
}

function Su(e) {
    let t = Math.max(0, Math.floor(e)), n = Math.floor(t / 3600), r = Math.floor(t % 3600 / 60), i = t % 60;
    return `${String(n).padStart(2, `0`)}:${String(r).padStart(2, `0`)}:${String(i).padStart(2, `0`)}`
}

function Cu({
                auctionResult: e,
                allowControls: t = !1,
                className: n,
                onStartSuccess: r,
                refreshAfterStart: i = !0,
                playSoundOnStart: a = !0,
                startSoundId: o = f.Game_Unpause
            }) {
    let {play: s} = oe(), {showNotification: c} = Cn(), [, l] = (0, Y.useState)(0), [u, d] = (0, Y.useState)(!1), [p, m] = (0, Y.useState)(null),
        h = !!e?.timerStartedAt, g = p ?? h;
    (0, Y.useEffect)(() => {
        if (!e?.timerStartedAt) return;
        let t = window.setInterval(() => {
            l(e => e + 1)
        }, 1e3);
        return () => window.clearInterval(t)
    }, [e?.timerStartedAt, e?.timerAccumulatedSec]);
    let _ = Su(xu(e)), v = t, y = g ? `stop` : `start`;
    (0, Y.useEffect)(() => {
        p !== null && p === h && m(null)
    }, [h, p]);
    let b = (0, Y.useCallback)(async () => {
        if (!u) {
            d(!0);
            try {
                await ye({action: y}), m(y === `start`), a && s(y === `start` ? o : f.Game_Pause), i && Or(), y === `start` && r?.()
            } catch (e) {
                m(null), console.error(`postAuctionResultTimer failed:`, e);
                let t = bt(e);
                t && c(t, J(`faces/angry.webp`), `Ошибка`, void 0, f.UI_NOT_Error)
            } finally {
                d(!1)
            }
        }
    }, [u, y, r, s, a, i, c, o]), x = j(), S = te();
    return k(`RT`, () => {
        A(180, {strongMagnitude: .45, weakMagnitude: .25}), b()
    }, t && !u && !x && !S), (0, X.jsx)(`div`, {
        className: [`auction-session-timer`, !v && `auction-session-timer--no-start`, n].filter(Boolean).join(` `),
        children: (0, X.jsxs)(`div`, {
            className: [`auction-timer-frame`, u && `auction-timer-frame--submitting`].filter(Boolean).join(` `),
            "aria-busy": u,
            children: [v ? (0, X.jsxs)(`button`, {
                type: `button`,
                className: `auction-timer-arrow-btn`,
                onClick: () => void b(),
                disabled: u,
                "aria-label": g ? `Остановить таймер сессии` : `Запустить таймер сессии`,
                children: [g ? (0, X.jsx)(`img`, {
                    src: J(`ui/stop.svg`),
                    className: `auction-timer-arrow auction-timer-arrow--stop`,
                    "aria-hidden": !0
                }) : (0, X.jsx)(`img`, {
                    src: J(`ui/play.svg`),
                    className: `auction-timer-arrow`,
                    "aria-hidden": !0
                }), (0, X.jsx)(q, {button: `RT`, className: `auction-timer-gp-hint`})]
            }) : null, (0, X.jsx)(`span`, {className: `auction-timer-display`, "aria-live": `polite`, children: _})]
        })
    })
}

var wu = 30, Tu = e => e.length > wu ? `${e.slice(0, wu)}...` : e, Eu = [Q.COMPLETED, Q.DROPPED, Q.REROLLED],
    Du = ({player: e, onAction: t, showPlayerHistory: n, isSelf: r}) => {
        let {play: i} = oe(), a = _c(), o = j(), s = te();
        k(`X`, () => {
            n && (i(f.Menu_Button_Back_Click), n())
        }, !!n && !o && !s);
        let c = N(), l = e.currentGame?.data != null && r && !o && !s && c.connected, [u, d] = (0, Y.useState)(0);
        (0, Y.useEffect)(() => {
            if (!l) return;
            let e = 0, t = !1, n = () => {
                let r = S(2);
                if (!t && Math.abs(r) > .55) {
                    t = !0;
                    let e = r > 0 ? 1 : -1;
                    d(t => {
                        let n = Math.max(0, Math.min(Eu.length - 1, t + e));
                        return n !== t && i(f.Menu_Button_Participants_Hover), n
                    })
                } else t && Math.abs(r) < .2 && (t = !1);
                e = requestAnimationFrame(n)
            };
            return e = requestAnimationFrame(n), () => cancelAnimationFrame(e)
        }, [l, i]), k(`A`, () => {
            i(f.Menu_Button_Back_Click), t(Eu[u])
        }, l);
        let p = (0, Y.useMemo)(() => {
                let t = e.currentAuctionResult?.currentReward;
                return typeof t == `number` ? t : t && typeof t.ggpReward == `number` ? t.ggpReward : 0
            }, [e.currentAuctionResult?.currentReward]), m = e.status === `idle`, h = e.status === `casino`, g = m || h,
            _ = e.currentAuctionResult?.status === Q.PLAYING || e.currentAuctionResult?.status === `content`,
            v = e.currentGame?.data, y = !g && v && `name` in v && !(`id` in v),
            b = v && Object.keys(v).length === 1 && `name` in v,
            x = y || e.status === `in_game` || e.status === `content` || _,
            C = y || !b && (e.status === `in_game` || e.status === `content` || _),
            w = v && `imageUrl` in v && typeof v.imageUrl == `string` && v.imageUrl || e.currentAuctionResult?.imageUrl || ``,
            T = m ? `Проводит аукцион` : h ? `Крутит колесо` : y ? `Смотрит: ${v && `name` in v ? v.name : `Неизвестно`}` : v && `name` in v ? v.name : `Неизвестно`,
            E = e.currentAuctionResult?.seconds != null && typeof e.currentAuctionResult.seconds == `number` && !Number.isNaN(e.currentAuctionResult.seconds) ? Je(e.currentAuctionResult.seconds) ?? `—` : `—`,
            D = e.currentGame?.ggpBonus && typeof e.currentGame.ggpBonus == `object` ? Object.values(e.currentGame.ggpBonus).reduce((e, t) => e + (typeof t == `number` ? t : 0), 0) : 0,
            O = y ? e.currentGame?.ggpReward ?? 0 : p,
            A = y ? e.currentGame?.ggpReward ?? 0 : e.currentAuctionResult?.ggpReward ?? 0;
        return (0, X.jsxs)(`div`, {
            className: `member-detail`, children: [(0, X.jsx)(`div`, {
                className: `main-content ${g ? `idle-centered` : ``}`, children: (0, X.jsxs)(`div`, {
                    className: `main-content-body`,
                    children: [x && !g && (0, X.jsxs)(`div`, {
                        className: `member-detail-head-row`,
                        children: [(0, X.jsx)(`span`, {
                            className: `status-label`,
                            children: y ? `Текущий фильм` : `Текущая игра`
                        }), (0, X.jsx)(`div`, {
                            className: `members-list-separator member-detail-head-separator`,
                            "aria-hidden": !0
                        }), (0, X.jsx)(`span`, {
                            className: `rewards-label member-detail-head-rewards`,
                            children: y ? `Очки за фильм` : `Очки за игру`
                        })]
                    }), (0, X.jsxs)(`div`, {
                        className: `member-detail-columns`, children: [(0, X.jsxs)(`div`, {
                            className: `left-column`,
                            children: [x && g && (0, X.jsxs)(`div`, {
                                className: `status-section`,
                                children: [(0, X.jsx)(`span`, {
                                    className: `status-label`,
                                    children: y ? `Текущий фильм` : `Текущая игра`
                                }), (0, X.jsx)(`div`, {
                                    className: `members-list-separator status-section-separator`,
                                    "aria-hidden": !0
                                })]
                            }), (0, X.jsxs)(`div`, {
                                className: `game-info-row`,
                                children: [C && (0, X.jsx)(`div`, {
                                    className: `game-cover`,
                                    children: y ? (0, X.jsx)(bc, {
                                        src: J(`icons/movie.webp`),
                                        alt: T,
                                        className: `game-cover-img`,
                                        skeletonWidth: `100%`,
                                        skeletonHeight: `100%`,
                                        skeletonVariant: `rect`
                                    }) : w ? (0, X.jsx)(bc, {
                                        src: he(w),
                                        alt: T,
                                        className: `game-cover-img`,
                                        skeletonWidth: `100%`,
                                        skeletonHeight: `100%`,
                                        skeletonVariant: `rect`,
                                        referrerPolicy: `no-referrer`
                                    }) : (0, X.jsx)(`div`, {className: `cover-placeholder`})
                                }), (0, X.jsxs)(`div`, {
                                    className: `game-text-info`,
                                    children: [(0, X.jsxs)(`div`, {
                                        children: [(0, X.jsxs)(`div`, {
                                            className: `game-title`,
                                            children: [(0, X.jsx)(`span`, {
                                                className: `game-title-text`,
                                                title: T.length > wu ? T : void 0,
                                                children: Tu(T)
                                            }), !g && (0, X.jsx)(`img`, {
                                                src: J(y ? `icons/movie.svg` : `icons/in_game.svg`),
                                                alt: y ? `Movie` : `Game`,
                                                className: `game-title-type-icon`
                                            }), v && `steamStoreLink` in v && v.steamStoreLink && (0, X.jsx)(`a`, {
                                                href: v.steamStoreLink,
                                                target: `_blank`,
                                                rel: `noopener noreferrer`,
                                                className: `steam-link-icon`,
                                                children: (0, X.jsx)(`img`, {
                                                    src: Et(a),
                                                    alt: `Steam`,
                                                    className: `steam-icon`
                                                })
                                            })]
                                        }), !g && !y && (0, X.jsxs)(`div`, {
                                            className: `hltb-hours`,
                                            children: [`HLTB - `, E]
                                        }), !g && !y && (0, X.jsxs)(`div`, {
                                            className: `timer-row`,
                                            children: [(0, X.jsx)(Cu, {
                                                auctionResult: e.currentAuctionResult,
                                                allowControls: r
                                            }), (0, X.jsx)(`span`, {className: `timer-row-label`, children: `таймер`})]
                                        })]
                                    }), !g && !y && (0, X.jsxs)(`div`, {
                                        className: `crown-section`,
                                        children: [(0, X.jsx)(`span`, {
                                            className: `crown-text`,
                                            children: `Главный угонщик:`
                                        }), (0, X.jsx)(`div`, {
                                            className: `crown-result`,
                                            children: e.currentGame?.wastelandKing ? (0, X.jsxs)(X.Fragment, {children: [e.currentGame.wastelandKing.playerName, ` [`, Math.floor(e.currentGame.wastelandKing.bestTimeMinutes / 60), ` `, dt(Math.floor(e.currentGame.wastelandKing.bestTimeMinutes / 60)), ` `, e.currentGame.wastelandKing.bestTimeMinutes % 60, ` `, Be(e.currentGame.wastelandKing.bestTimeMinutes % 60), `]`]}) : `Отсутствует`
                                        })]
                                    })]
                                })]
                            })]
                        }), !g && (0, X.jsx)(`div`, {
                            className: `right-column`,
                            children: (0, X.jsxs)(`div`, {
                                className: `rewards-list`,
                                children: [(0, X.jsxs)(`div`, {
                                    className: `reward-item`,
                                    children: [(0, X.jsx)(`div`, {
                                        className: `reward-item-top`,
                                        children: (0, X.jsxs)(`span`, {
                                            className: `reward-value`,
                                            children: [`+`, g ? 0 : O]
                                        })
                                    }), (0, X.jsx)(`span`, {className: `reward-label`, children: `по таймеру`})]
                                }), (0, X.jsxs)(`div`, {
                                    className: `reward-item`,
                                    children: [(0, X.jsx)(`div`, {
                                        className: `reward-item-top`,
                                        children: (0, X.jsxs)(`span`, {
                                            className: `reward-value`,
                                            children: [`до `, g ? 0 : A]
                                        })
                                    }), (0, X.jsx)(`span`, {className: `reward-label`, children: `максимум`})]
                                }), D > 0 && (0, X.jsxs)(`div`, {
                                    className: `reward-item`,
                                    children: [(0, X.jsx)(`div`, {
                                        className: `reward-item-top`,
                                        children: (0, X.jsxs)(`span`, {className: `reward-value`, children: [`+`, D]})
                                    }), (0, X.jsx)(`span`, {className: `reward-label`, children: `Бонус`})]
                                })]
                            })
                        })]
                    })]
                })
            }), (0, X.jsxs)(`div`, {
                className: `buttons-container ${g ? `idle-centered` : ``} ${(!v || !r) && n ? `history-only` : ``}`,
                children: [v != null && r && l && (0, X.jsxs)(`div`, {
                    className: `member-detail-stick-hint`,
                    "aria-hidden": !0,
                    children: [(0, X.jsx)(`span`, {
                        className: `member-detail-stick-hint-icon`,
                        children: `↔`
                    }), `Правый стик — выбор`]
                }), v != null && r && (0, X.jsxs)(X.Fragment, {
                    children: [(0, X.jsxs)(`div`, {
                        className: `members-list-btn${l && u === 0 ? ` members-list-btn--gp-focused` : ``}`,
                        onClick: e => {
                            i(f.Menu_Button_Back_Click, {panElement: e.currentTarget}), t(Q.COMPLETED)
                        },
                        onMouseEnter: e => {
                            i(f.Menu_Button_Back_Hover, {panElement: e.currentTarget}), l && d(0)
                        },
                        children: [l && u === 0 && (0, X.jsx)(q, {
                            button: `A`,
                            className: `member-detail-action-gp-hint`
                        }), y ? `Посмотрел` : `Прошел`]
                    }), (0, X.jsxs)(`div`, {
                        className: `members-list-btn drop-button${l && u === 1 ? ` members-list-btn--gp-focused` : ``}`,
                        onClick: e => {
                            i(f.Menu_Button_Back_Click, {panElement: e.currentTarget}), t(Q.DROPPED)
                        },
                        onMouseEnter: e => {
                            i(f.Menu_Button_Back_Hover, {panElement: e.currentTarget}), l && d(1)
                        },
                        children: [l && u === 1 && (0, X.jsx)(q, {
                            button: `A`,
                            className: `member-detail-action-gp-hint`
                        }), (0, X.jsx)(`span`, {
                            className: `drop-text`,
                            children: `Дроп`
                        }), (0, X.jsxs)(`span`, {
                            className: `penalty-value`,
                            children: [`-`, e.currentGame?.ggpPenalty ?? 0, ` Очков`]
                        })]
                    }), (0, X.jsxs)(`div`, {
                        className: `members-list-btn${l && u === 2 ? ` members-list-btn--gp-focused` : ``}`,
                        onClick: e => {
                            i(f.Menu_Button_Back_Click, {panElement: e.currentTarget}), t(Q.REROLLED)
                        },
                        onMouseEnter: e => {
                            i(f.Menu_Button_Back_Hover, {panElement: e.currentTarget}), l && d(2)
                        },
                        children: [l && u === 2 && (0, X.jsx)(q, {
                            button: `A`,
                            className: `member-detail-action-gp-hint`
                        }), `Реролл`]
                    })]
                }), v != null && r && n && (0, X.jsx)(`div`, {
                    className: `members-list-separator member-detail-buttons-separator`,
                    "aria-hidden": !0
                }), n && (0, X.jsxs)(`div`, {
                    className: `members-list-btn`,
                    onClick: e => {
                        i(f.Menu_Button_Back_Click, {panElement: e.currentTarget}), n()
                    },
                    onMouseEnter: e => i(f.Menu_Button_Back_Hover, {panElement: e.currentTarget}),
                    children: [(0, X.jsx)(q, {button: `X`, className: `member-detail-gp-hint`}), `История`]
                })]
            })]
        })
    },
    Ou = [{title: `Проведение аука`}, {title: `Внесение названия игры на сайте`}, {title: `Прохождение игры, выставление оценки/написание рецензии`}, {
        title: `Получение очков за прохождение - вращение колеса с обычным крупье (количество вращений формируется за набор суммы донатов во время прохождения игры)`,
        badge: `Позитивные, нейтральные и негативные эффекты`,
        badgeTone: `neutral`
    }, {
        title: `Вращение колеса с премиум крупье(за прохождение игры)`,
        badge: `Позитивные эффекты`,
        badgeTone: `positive`
    }, {
        title: `Вращение колеса с гнусным крупье`,
        badge: `Дается за выпадение соответствующего эффекта`,
        badgeTone: `positive`
    }, {title: `Конец цикла`}], ku = [{
        title: `Судейские полномочия и штрафы`,
        blocks: [{
            type: `paragraph`,
            text: `На протяжении всей длительности мероприятия за участниками будет следить судья. Нарушение, совершенные участниками могут караться как простым предупреждением, так и более серьезными штрафными санкциями, вплоть до требования перепроходить игру с самого начала или лишения очков. Серьезность нарушения субъективно оценивается судьей, однако для ясности ниже описаны примеры нарушений и возможные штрафы (вариаций нарушений на деле может оказаться куда больше):`
        }, {
            type: `labeledBullets`,
            items: [{
                label: `Неправильно выставленная сложность`,
                text: `Любая стадия игры - требование перепройти игру на корректной сложности.`
            }, {
                label: `Манипуляции с колесом во время аукциона (не добавление лотов или таймера за новые лоты)`,
                text: `До начала прохождения игры или на ранней стадии - требование совершить реролл колеса или довести аукцион корректно. Спустя ощутимую часть прохождения игры или во время прохождения уже следующей игры - штраф на очки, в размере, соизмеримым свинству участника.`
            }, {
                label: `Механические ошибки в применении эффектов, вращении колес и т.д`,
                text: `Предупреждение или требование совершить реролл колеса/колес, отмена полученных эффектов.`
            }, {
                label: `Некорректное завершение игры`,
                text: `До начала следующего аукциона - требование допройти игру корректно.`
            }, {
                label: `Нарушение правила получения подсказок (гайдинг)`,
                text: `Предупреждение, при повторных многократных нарушениях - штраф на очки.`
            }]
        }, {
            type: `paragraph`,
            text: `Стоит учитывать, что судья не может физически уследить за всеми нарушениями и ошибками, которые совершают участники. Однако если другой участник или чатерсы увидели нарушение, они могут сообщить судье об этом. Любое нарушение имеет срок мошны, который составляет 24 часа с момента нарушения.`
        }, {
            type: `limitCallout`,
            label: `Примечание`,
            text: `Любой игрок может оспорить вердикт судьи, предоставив четкую доказательную базу.`
        }]
    }, {
        title: `Формула расчета очков`,
        blocks: [{type: `capsHeading`, text: `Время прохождения`}, {
            type: `paragraph`,
            text: `Расчет очков по времени прохождения происходит по циклической системе, где один цикл равен 20 часам. Первые 5 часов оцениваются в 3 очка/час (15 очков максимум), часы с 6 по 10 оцениваются в 4 очка/час (20 макс.), с 11 по 15 - 5о./ч. (25 макс.) и с 16 по 20 - 6о./ч. (30 макс.). Всего за цикл дается 90 очков. `
        }, {
            type: `table`,
            headers: [`Время HLTB`, `Очки за каждый час`, `Максимум за цикл`],
            rows: [[`1 – 5 часов`, `3 очка`, `15 очков`], [`6 – 10 часов`, `4 очка`, `20 очков`], [`11 – 15 часов`, `5 очков`, `25 очков`], [`16 – 20 часов`, `6 очков`, `30 очков`]]
        }, {
            type: `paragraph`,
            text: `Стример не получает выгоды, если проходит игру дольше, чем её время прохождения, указанное на HLTB. В случае если время прохождения игры стримером превышает время по HLTB, то при расчете очков считается так, будто он прошел ее за время по HLTB.`
        }, {type: `capsHeading`, text: `«Налог на скилл»`}, {
            type: `paragraph`,
            text: ` Если игра с длительностью прохождения больше 20 часов пройдена быстрее чем за 20 часов, то применяется «налог на скилл» и берется среднее арифметическое между фактическим временем прохождения и временем по HLTB (при этом, если время прохождения по HLTB больше 20 часов, то оно считается равным 20 часам). Затем очки начисляются исходя из полученного времени:`
        }, {type: `formula`, text: `T_расчетное = (T_факт + T_HLTB) / 2`}, {
            type: `footnote`,
            starred: !0,
            text: `Если игра проходится стримером более 20 часов и имеет более 20 часов по HLTB, то стример получает 90 очков (это очки за первые 20 часов) + очки исходя из указанного времени (“налог на скилл» в данном случае не применяется)`
        }, {
            type: `examplesPanel`, title: `Примеры расчета`, items: [`Пример 1 (Half-Life): 12 часов по HLTB. Стример прошел игру за 4 часа. Согласно формуле он получит: 
(12 + 4) / 2 = 8 часов. Очки: (5 * 3) + (3 * 4) = **27 очков**`, `Пример 2 (Dark Souls Remastered): 29 часов по HLTB. Стример прошел игру за 3 часа. Согласно формуле он получит: 
(20 + 3) / 2 = 11.5 часов. Очки: (5 * 3) + (5 * 4) + (1.5 * 5) = 42.5 → **43 очка.**`]
        }, {
            type: `footnote`,
            text: `Примечание: «Налог на скилл»  не работает на игры-коротыши, что по HLTB короче 6 часов. Для таких игр стример получает сразу фиксированные очки за время прохождения, которые соответствуют времени прохождения на HLTB (не более 15).`
        }, {type: `capsHeading`, text: `Длинные игры (более 20 часов)`}, {
            type: `paragraph`,
            text: `Если же игра проходится стримером более 20 часов и имеет более 20 часов по HLTB, то стример получает 90 очков (это очки за первые 20 часов) + очки исходя из указанного времени ( “налог на скилл” в данном случае не применяется):`
        }, {type: `capsHeading`, text: `Дополнительные очки: Свежесть игры`}, {
            type: `table`,
            headers: [`Год - свежесть`, `Очки`],
            rows: [[`Свежая (2024-2026)`, `1`], [`Не первой свежести (2020-2023)`, `2`], [`Залежавшаяся (2016-2019)`, `3`], [`Жухлая (2010-2015)`, `4`], [`С запашком (2006-2009)`, `5`], [`Сгнившая (2001-2005)`, `7`], [`Кал мамонта (2000 и старше)`, `10`]]
        }, {type: `capsHeading`, text: `Дополнительные очки: Рейтинг HLTB`}, {
            type: `table`,
            headers: [`Рейтинг (%)`, `Очки`],
            rows: [[`90 - 100`, `1`], [`81 - 89`, `2`], [`76 - 80`, `3`], [`71 - 75`, `4`], [`61 - 70`, `5`], [`51 - 60`, `6`], [`45 - 50`, `7`], [`51 – 60`, `6`], [`45 – 50`, `7`], [`44 и ниже`, `10`]]
        }, {
            type: `list`,
            title: `Для гуманитариев`,
            items: [`Игры до 5 часов по HLTB включительно: «налог на скилл» не применяется; стример получает фиксированные максимальные очки независимо от того, как быстро он прошел игру.`, `Игры 6–20 часов по HLTB: «налог на скилл» по формуле (T_факт + T_HLTB) / 2.`, `Игры более 20 часов по HLTB (если пройдены быстрее 20 часов): «налог на скилл» по формуле (T_факт + 20) / 2.`, `Игры более 20 часов по HLTB (если стример прошел игру дольше 20 часов): «налог на скилл» не применяется; за первый цикл — 90 очков, далее очки за фактическое время: 21–25 ч — 3 очка/ч, 26–30 ч — 4 очка/ч, 31–35 ч — 5 очков/ч, 36–40 ч — 6 очков/ч и т.д.`]
        }]
    }, {
        title: `Дроп игры`,
        blocks: [{
            type: `paragraph`,
            text: `**Правило Дропа:** Дроп игры разрешается только спустя один час геймплея. В случае дропа стример теряет 50% от суммы очков, получаемых за прохождение этой игры.`
        }]
    }, {
        title: `Великая кража очков`,
        blocks: [{
            type: `paragraph`,
            text: `Если стримеру выпадает игра, которую проходил другой стример, активируется система «**Великая кража очков**» - соревнование на скоростное прохождение выпавшей игры:`
        }, {
            type: `list`,
            items: [`Пройдя игру быстрее предшественника, стример получит 30% (с верхним порогом в 15 очков) от суммы получаемых очков за прохождение игры, в то время как предшественник потеряет это количество очков.`, `Если на прохождение игры ушло больше времени, то стример отдает 30% (с верхним порогом в 15 очков) от суммы получаемых очков за прохождение игры тому, с кем соревнуется.`, `Если у третьего и, в последствии, любого следующего стримера выпадает эта же игра, то соревнование происходит только с тем, кто показал лучшее время прохождения текущей игры.`, `Великая кража очков не работает, если стример рероллит или дропает игру.`]
        }]
    }, {
        title: `Колесо приколов`,
        blocks: [{
            type: `paragraph`,
            text: `Во время прохождения игры, все донаты, которые получает стример, заполняют шкалу на получение вращений Колеса приколов. Эти вращения используются для получения положительных, нейтральных или отрицательных эффектов и событий после прохождения игры. Стример обязан использовать все накопленные вращений после прохождения игры. Стандартная сумма для получения одного обычного вращения назначена **персонально судьей** для каждого стримера. Сумма каждого следующего вращения увеличивается на **500/1000** рублей от суммы предыдущего и возвращается к исходному виду по итогу начала прохождения следующей игры.`
        }, {
            type: `bulletItems`,
            items: [{
                lead: `Типы Крупье`,
                subItems: [`**Обычный крупье:** Может иметь как позитивный, нейтральный так и негативный эффекты.`, `**Премиум крупье:** Дает исключительно позитивные эффекты.`, `**Судейский крупье:** Доступен в первую очередь судье и, в некоторых ситуациях, остальным участникам. Судейское Колесо приколов имеет уникальные пункты, которых нет у других участников и активируется при заполнении шкалы донатов у судьи. `, `**Гнусный крупье:** Представляет из себя одну из нескольких гнусностей, которую стример или судья использует на других стримеров. Один стример не может получить от других стримеров и судьи суммарно больше четырех гнусностей за сутки.`]
            }, {
                lead: `Правила активации`,
                text: `Все негативные эффекты активируются автоматически при возможности их выполнения. Все позитивные эффекты активируются по желанию стримера за исключением тех, в описании которых есть соответствующее примечание. Позитивный эффект с идентичным способом применения (прим. сложность игры) перекрывают аналогичный негативный эффект и нивелируют друг друга.`
            }, {lead: `Ограничение стака`, text: `Стак позитивных эффектов запрещен, кроме эффектов на получение очков.`}]
        }, {type: `paragraph`, text: `Список эффектов вы можете посмотреть в разделе «Эффекты» на главной странице сайта.`}]
    }, {
        title: `Прохождение игры`,
        blocks: [{
            type: `paragraph`,
            text: `Основным критерием успешного завершения игры является показ финальных титров. Не всегда в играх титры можно увидеть только в конце игр. Вот несколько примеров, в которых :`
        }, {
            type: `warning`,
            title: `⚠️ Титры можно увидеть раньше, и что нужно делать в таких случаях`,
            items: [`а) NieR:Automata — титры появляются после каждого акта. Стримеру надо дойти до момента игры, когда после титров не будет продолжения игры или если после этих титров игрока выбрасывают в открытый мир без сюжетного продолжения.`, `б) Far Cry 5 — титры активируются через 5 минут после начала игры, если стример решает не арестовывать главного антагониста. Такие титры причисляются к категории «шуточные», стример должен дойти до финала игры и получить нормальные титры.`, `в) Bad Ending — в некоторых играх по ходу игры можно активировать «плохой конец» и досрочно закончить игру, сократив определенную часть контента. Данный пункт неоднозначный, поэтому стримеру необходимо проконсультироваться с судьей ивента, чтобы определить, действительно ли при плохой концовке срезается значимая часть сюжета, или это не столь критично.`, `г) The Outer Worlds и ее аналоги - данный пункт очень схож с предыдущим. Если в игре стримеру предлагают закончить игру и потерять как минимум 30-40% доступного сюжетного контента, при этом сама концовкой не относится к Bad Ending’у, стример не может использовать ее как доказательство прохождения игры. Необходимо пройти игру до конца.`]
        }, {
            type: `bulletItems`,
            items: [{
                lead: `Альтернативная концовка`,
                subItems: [`Игра была пройдена на альтернативную концовку, при которой была упущена лишь малая часть сюжетного контента. Для честности прохождения в таком случае стоит проконсультироваться с судьей ивента. `]
            }, {
                lead: `Игры без сюжета, но с уровнями`,
                subItems: [`Если уровни открываются один за другим, необходимо открыть все уровни и пройти финальный.`, `Если все уровни открыты сразу, необходимо пройти каждый из них хотя бы на одну звезду или на минимальное требование игры.  При этом, для зачета о пройденной игры не требуется проходить бонусные и экстра уровни. Также, если уровни разделены на сложности, стримеру необходимо проконсультироваться с судьей, чтобы определить, какие уровни ему нужно проходить.`]
            }, {
                lead: `Игры с программным хайскором`,
                text: `В играх, в которых присутствует программный хайскор (а не хайскор реальных людей), нужно побить этот программный хайскор.`
            }, {
                lead: `Жанр Roguelike`,
                text: `В играх жанра Roguelike необходимо пройти один полноценный игровой цикл. Игровой цикл заканчивается в тот момент, когда после убийства какого-то босса игра начинает повторяться, игрок начинает свой прогресс заново и т.д. Тем не менее, из-за специфика жанра, необходимо уточнить условия прохождения у судьи.`
            }, {
                lead: `Стратегии с кампаниями`,
                text: `В стратегиях с кампаниями необходимо пройти все кампании основной игры, даже если они открыты сразу. Исключением может быть только случай, когда выбор кампании влияет исключительно на то, с перспективы какой стороны стример будет смотреть на основной сюжет игры, а сами миссии будут идентичны или очень похожи на миссии другой кампании. `
            }, {
                lead: `Игры серии LEGO по фильмам`,
                text: `В играх серии LEGO, которые сделаны по фильмам, необходимо пройти все части этих фильмов, даже если они открыты изначально. `
            }, {
                lead: `Игры без очевидного сюжета`,
                text: `В играх, где нет очевидной сюжетной линии (прим. Sea Dogs: City of Abandoned Ships) стримеру разрешается выяснить с помощью интернета об основной сюжетной ветке, а именно о том, как ее активировать, после чего пройти эту сюжетную ветку. `
            }, {
                lead: `Издания GOTY Edition`,
                text: `В версиях игры GOTY Edition и его аналогах, необходимо пройти основную сюжетную линию, без включенных в игру DLC.`
            }, {
                lead: `Эпизодические игры`,
                text: `Если выпала эпизодическая игра, нужно перейти со страницы ролла игр на сайт HLTB и выяснить, ведет ли переход на полную игру или же только на конкретный эпизод. Если на полную - необходимо пройти всю игру, если на конкретный эпизод - можно пройти только его.`
            }]
        }, {type: `capsHeading`, text: `Категорически запрещено`}, {
            type: `bulletItems`,
            items: [{text: `Использовать чит-коды, трейнеры и сторонний софт для облегчения геймплея.`}, {text: `Ограничивать FPS, за исключением случаев, когда это технически необходимо для адекватной работы игр.`}, {text: `Читать внутриигровые мануалы, даже если они встроены в игру или доступны в файлах игры, если у вас нет соответствующего предмета.`}, {text: `Делать скриншоты или фотографии на играх, связанных с памятью (прим. разнообразные пазлы).`}, {text: `Использовать предметы и любое преимущество, которое дает тебе купленная GOTY Edition версия игры, если этого не было в оригинальной игры (прим. прокаченный персонаж в Assasin’s Creed: Origins, разнообразное оружие и броня в Fallout: New Vegas).`}, {text: `Донатить в игру для получения преимущества.`}, {text: `Смотреть прохождение игры, в том числе и у других стримеров.`}, {text: `Проходить игру в кооперативе или на онлайн-серверах, где посторонние игроки могут давать буст.`}, {text: `Даунгрейдить ПК версию игры для более простого прохождения. Играть необходимо на последней (актуальной) версии игры.`}, {text: `Если у игры есть версия с таким же названием на мобильных устройствах, древних консолях или смартфонах, в которых отличается геймплей в пользу критического сокращения времени прохождения, то необходимо играть в ПК версию этой игры.`}, {text: `Включать интеграцию со стримерскими платформами (Twitch, Kick итд), влияющую на оригинальный геймплей.`}, {
                lead: `Использовать подсказки чата, которые`,
                leadPlain: !0,
                subItems: [`а) описывают баги, глитчи, абузы и скипы, о которых вы не были в курсе.`, `б) копируют поэтапное прохождение игры (копипаста с сайтов). `, `в) подсказывают в стиле «Мой дед был машинистом поезда, поэтому каждый раз для запуска он делал это, это и вот это»`]
            }, {text: `Заниматься фармом очков в afk режиме. Таймер должен работать только когда стример фактически находится за компьютером и играет в игру. Пример: Minecraft, стример дошел до Дракона, но вернулся в свое поселение и ушел спать. Таймер оставил включенным. Технически игра запущена, но фактически стример в нее не играет и просто фармит очки за проведенной в игре время. `}]
        }]
    }, {
        title: `Выбор сложности`,
        blocks: [{
            type: `paragraph`,
            text: `При прохождении игры, стримеру необходимо выбрать правильную сложность или режим игры, если он не находится под действием каких-либо эффектов. Выбор сложности обязателен, даже если игра изначально предлагает стримеру пройти себя на легком уровне сложности:`
        }, {
            type: `patternBullets`,
            items: [{
                condition: `3 сложности (Легкая, Средняя, Высокая)`,
                result: `Выбирается Средняя`
            }, {
                condition: `5 сложностей: Очень легкая, Легкая, Средняя, Высокая, Экстрим. `,
                result: `Выбирается Средняя`
            }, {
                condition: `Более 3 сложностей, но первая из них легкая, а вторая - средняя`,
                result: `Выбирается Средняя`
            }, {
                condition: `2 сложности: Легкая и Средняя`,
                result: `Выбирается Средняя`
            }, {
                condition: `2 сложности: Средняя и Высокая`,
                result: `Выбирается Средняя`
            }, {
                condition: `2 сложности: Легкая и Высокая`,
                result: `Выбирается Легкая`
            }, {
                condition: `2 сложности, которые имеют оригинальное название`,
                result: `Выбирается Первая`
            }, {
                condition: `Несколько сложностей, но они имеют не стандартное обозначение, а собственные названия (прим. Call of Duty).`,
                result: `Преимущественно вторая, однако для точной гарантии стоит посовещаться с судьей.`
            }, {
                condition: `Нет сложностей, но есть режимы`,
                result: `Режимы выбираются по аналогии с выбором сложности, за исключением случаев, когда режимов только два. В такой ситуации всегда выбирается более простой. `
            }]
        }, {
            type: `limitCallout`,
            label: `Важное ограничение`,
            text: `В играх запрещено менять любые настройки, влияющие на сложность игры (прим. увеличивать себе количество жизней, понижать силу врагов и т.д.).`
        }]
    }, {
        title: `Аукцион`,
        blocks: [{
            type: `paragraph`,
            text: `Аукцион проходит на специализированной платформе по строго установленным правилам:`
        }, {
            type: `labeledBullets`,
            items: [{
                label: `Платформа`,
                text: `Сайт **auc.nassal.pro** с базовым таймером в **20 минут**.`
            }, {
                label: `Минимальная ставка`,
                text: `Ставка определяется стримером в начале ивента и остается неизменной. Смена ставки производится только в исключительном случае и с разрешения судьи.`
            }, {
                label: `Продление времени`,
                text: `Донат увеличивает время на таймере только в том случае, если остается менее двух минут (**+1 минута** за каждый донат).`
            }, {
                label: `Донаты, поступившие после официального завершения аукциона`,
                text: `Все донаты, которые пришли после завершения аукциона уходят на вращение колес после игры.`
            }, {
                label: `Ограничения участников`,
                text: `Участники ивента не могут заказывать игры друг другу.`
            }, {label: `Лимит времени`, text: `Максимальное время аукциона составляет **1 час**.`}, {
                label: `Замена лота`,
                text: `Если донатер случайно заказал не подходящую игру (например, она слишком длинная или уже была у стримера), он может заменить её на другую, если донат был не анонимным. Возможность смены лота доступна **только до ролла аукциона**.`
            }, {
                label: `Правило реролла`,
                text: `Из-за короткой длительности всего ивента, у стримеров есть возможность рероллить по собственному желанию любые игры, время прохождения которых превышает **30 часов**. Время прохождения определяется по HLTB. Если время прохождения отображается некорректно из-за отсутствующей категории на сайте, легитимность прохождения следует обсудить с судьей. Учитывайте это при заказе игр.`
            }, {
                label: `Верификация`,
                text: `Игра обязана присутствовать на сайте **[HLTB (HowLongToBeat)](https://howlongtobeat.com)** с четко указанным временем прохождения.`
            }, {
                label: `DLC`,
                text: `Запрещено заказывать DLC, только если это не stand-alone DLC, которое запускается без покупки основной игры, и которое корректно ведет на сайт HLTB к DLC, а не основной игре.`
            }]
        }, {type: `capsHeading`, text: `Запрещенные категории игр`}, {
            type: `orderedItems`,
            items: [{text: `VR-игры.`}, {text: `MMO-игры, а также игры с исключительно co-op прохождением, только если у стримера нет эффекта на co-op прохождение.`}, {
                lead: `Civilization и подобные игры`,
                text: `Зачастую это «контурные карты», где игрокам на старте дают возможность детально настроить свой мир, условия старта, количество соперников и т.д.`
            }, {
                lead: `Песочницы без сюжета`,
                text: `Схожий случай, когда на старте игры предлагают выбрать фракцию, установить размеры карты и стартовые параметры.`
            }, {
                lead: `Стратегии или Тайкуны со сценариями`,
                text: `Сценарий не считается полноценным прохождением до тех пор, пока действия происходят на одной карте (нет смены локации от выполнения задач), а выбор меняет только условия старта и другие несюжетные элементы.`
            }, {
                lead: `Визуальные новеллы`,
                text: `К визуальным новеллам не относятся:`,
                subItems: [`а) игры, в которых есть возможность участвовать в мини-играх (и их в достаточном количестве);`, `б) передвигаться или сменять локации по своему выбору на карте;`, `в) игра является видео-квестом;`, `г) серия Danganronpa (после более детального изучения возможностей игр этой серии).`]
            }, {text: `Текстовые квесты, в которых кроме текста и вариантов ответа ничего нет.`}, {text: `Тривии, викторины, а также обучающие языкам программы.`}, {text: `Бесконечные кликеры.`}, {text: `Игры, имеющие возрастные ограничения и запрещенные к трансляции на платформе стримера или в стране его проживания.`}, {text: `Игры, в которых присутствует нагота или порно. Тег «sexual content» или «nudity» не является причиной для реролла до тех пор, пока в игре не будет соответствующего контента, который может привести к бану канала. `}, {text: `Игры, не имеющие ни русского, ни английского интерфейса.`}, {
                lead: `Ранний доступ`,
                text: `Без обозначенного финала актуального сюжетного контента.`
            }, {text: `Порты с мобильных устройств, которые заточенные под микротранзакции.`}, {text: `Игры, которые уже были пройдены данным стримером на следующих смежных ивентах: **NASRAL 2025**, **AUKUS 4**, **Игрополиус**, **GTM**.`}, {text: `Запрещено повторное прохождение одной игр на разных версиях (прим. The Binding of Isaac и The Binding of Isaac: Rebirth). В отдельных случаях проходить одну и ту же игру можно, только если геймплей существенно отличается в зависимости от версии игры. (прим. Mafia и Mafia: Definitive Edition).`}, {
                lead: `Технический реролл`,
                text: `Если игра не запускается, не захватывается программой OBS или критически ломает экраны. Стример обязан совершить разумные попытки исправления (установить фиксы, софт для старых игр, запустить в окне, найти рабочую сборку). Если манипуляции не помогли, стример обращается к судье для официального апрува реролла.`
            }]
        }]
    }, {
        title: `Награды и наказания`,
        blocks: [{
            type: `labeledBullets`,
            items: [{label: `Первое место`, text: `Золотое ведро.`}, {
                label: `Второе место`,
                text: `Серебряное ведро.`
            }, {label: `Третье место`, text: `Бронзовое ведро.`}, {
                label: `Последнее место`,
                text: `Фонтан в виде вагины, из которого нужно будет выпить на видео.`
            }]
        }, {type: `limitCallout`, label: `Примечание`, text: `Ведра пустые.`}]
    }, {title: `Цикл`, blocks: [{type: `cycle`}]}, {title: `Калькулятор`, blocks: []}],
    Au = [`Цикл`, `Аукцион`, `Прохождение игры`, `Выбор сложности`, `Дроп игры`, `Великая кража очков`, `Формула расчета очков`, `Калькулятор`, `Судейские полномочия и штрафы`, `Награды и наказания`, `Колесо приколов`],
    ju = new Map(ku.map(e => [e.title, e])), Mu = Au.map(e => {
        let t = ju.get(e);
        if (t == null) throw Error(`Missing rules section: ${e}`);
        return t
    }), $ = function (e) {
        return e.PLAYERS = `players`, e.RULES = `rules`, e.EFFECTS = `effects`, e.ACHIEVEMENTS = `achievements`, e.CREDITS = `credits`, e
    }({});

function Nu() {
    return Gn({queryKey: Er.achievements, queryFn: gt, staleTime: 1 / 0})
}

function Pu() {
    let {user: e} = ut(), {data: t, isLoading: n} = Nu();
    return {
        displayAchievements: (0, Y.useMemo)(() => {
            if (!t) return [];
            let n = [];
            for (let r of t.achievements) {
                let t = e ? r.achievedPlayers.some(t => t.id === e.id) : !1;
                n.push({achievement: r, isRevealed: !0, isCompleted: t})
            }
            let r = Math.max(0, t.totalCount - t.achievements.length);
            for (let e = 0; e < r; e++) n.push({achievement: null, isRevealed: !1, isCompleted: !1});
            return n
        }, [t, e]), isAchievementsInitialLoad: n && !t
    }
}

var Fu = !1, Iu = new Set;

function Lu() {
    return Fu
}

function Ru(e) {
    Fu !== e && (Fu = e, Iu.forEach(e => e()))
}

function zu(e) {
    return Iu.add(e), () => {
        Iu.delete(e)
    }
}

function Bu() {
    return (0, Y.useSyncExternalStore)(zu, Lu, () => !1)
}

var Vu = {
    wtv: `links/wtv.png`,
    vk: `links/vk.png`,
    vklive: `links/vklive.png`,
    telegram: `links/Telegram.png`,
    twitch: `links/Twitch.png`,
    youtube: `links/Youtube.png`,
    kick: `links/Kick.png`,
    steam: `links/steam.webp`,
    discord: `links/discord.png`,
    goodgame: `links/goodgame.png`
};

function Hu(e) {
    let t = e.trim().toLowerCase(), n = Vu[t];
    if (n != null) {
        let e = J(n);
        if (e) return e
    }
    return J(`links/${t}.png`)
}

function Uu(e) {
    return !1
}

function Wu(e, t) {
    return !1
}

var Gu = 50;

function Ku() {
    let {play: e} = oe(), t = (0, Y.useSyncExternalStore)(y, _, _), n = (0, Y.useRef)(null), r = (0, Y.useRef)(!1),
        i = (0, Y.useCallback)(() => {
            if (t > 0) {
                e(f.Menu_Head_Click), n.current = t, r.current = !0, g(0);
                return
            }
            e(f.Menu_Head_Click);
            let i = n.current, a = r.current && i != null && i > 0;
            r.current = !1, n.current = null, g(a ? i : Gu)
        }, [t, e]);
    return {isMuted: t === 0, toggle: i}
}

var qu = `https://eventlab.dev/nassal`, Ju = `https://twitch.tv/mistafaker`;

function Yu({label: e, tooltip: t}) {
    let n = (0, Y.useRef)(null);
    return (0, X.jsx)(`div`, {
        ref: n, className: `player-status-label`, onMouseEnter: () => {
            let e = n.current;
            !e || !t || (e.title = e.scrollWidth > e.clientWidth ? t : ``)
        }, onMouseLeave: () => {
            n.current && (n.current.title = ``)
        }, children: e
    })
}

var Xu = e => {
        let {play: t} = oe(), n = ft(), {isMuted: r, toggle: i} = Ku(), a = Bu(), {isAuthenticated: o} = ut(),
            s = (e, t) => e ? e.id && t.id ? e.id === t.id : e.userId && t.userId ? e.userId === t.userId : !1 : !1,
            c = (0, Y.useMemo)(() => Mu.filter(e => o || e.title !== `Калькулятор`), [o]), l = e.playersList,
            u = l.length == 0, {displayAchievements: d, isAchievementsInitialLoad: p} = Pu(),
            m = e => (e ?? ``).trim().toLowerCase(), h = (e, t) => {
                if (!e) return !1;
                let n = m(e.name), r = m(t.name);
                if (n && r && n === r) return !0;
                let i = m(e.displayName), a = m(t.displayName);
                return !!i && i === a
            }, g = e.gamepadInputEnabled ?? !0, _ = e.tab === $.PLAYERS, v = e.tab === $.RULES, y = e.tab === $.EFFECTS,
            b = e.tab === $.ACHIEVEMENTS, x = (0, Y.useMemo)(() => {
                let e = {};
                for (let t of l) e[t.id] = Uu(t.stream);
                return e
            }, [l]), S = (0, Y.useMemo)(() => e.effects ? Wc(e.effects) : [], [e.effects]),
            C = (0, Y.useMemo)(() => S.flatMap(e => e.effects), [S]),
            w = (0, Y.useMemo)(() => d.filter(e => e.achievement != null), [d]), T = (0, Y.useMemo)(() => {
                if (e.listHighlightedAchievementId) {
                    let t = w.find(t => t.achievement?.id === e.listHighlightedAchievementId)?.achievement;
                    if (t) return t
                }
                return e.selectedAchievement
            }, [w, e.listHighlightedAchievementId, e.selectedAchievement]), E = e.listHighlightedEffect ?? e.selectedEffect,
            D = (0, Y.useMemo)(() => {
                if (!T || w.length === 0) return 0;
                let e = w.findIndex(e => e.achievement?.id === T.id);
                return e >= 0 ? e : 0
            }, [w, T]), O = (0, Y.useMemo)(() => {
                if (!e.selectedRule || c.length === 0) return 0;
                let t = c.findIndex(t => t.title === e.selectedRule);
                return t >= 0 ? t : 0
            }, [c, e.selectedRule]), A = (0, Y.useMemo)(() => {
                if (!E || C.length === 0) return 0;
                let e = E, t = C.findIndex(t => {
                    let n = m(e.name), r = m(t.name);
                    if (n && r && n === r) return !0;
                    let i = m(e.displayName), a = m(t.displayName);
                    return !!i && i === a
                });
                return t >= 0 ? t : 0
            }, [C, E]), j = (e, t) => t > 0 ? Math.min(Math.max(0, e), t - 1) : 0,
            M = e.listHighlightedMember ?? e.selectedMember,
            N = (0, Y.useMemo)(() => M ? l.findIndex(e => s(M, e)) : -1, [l, M]), [ee, te] = (0, Y.useState)(() => N >= 0 ? N : 0), [ne, P] = (0, Y.useState)(N), [re, F] = (0, Y.useState)(() => O), [ie, I] = (0, Y.useState)(() => A), [ae, L] = (0, Y.useState)(() => D), [se, R] = (0, Y.useState)(e.selectedRule),
            z = e.selectedEffect?.name ?? e.selectedEffect?.displayName ?? null, [ce, le] = (0, Y.useState)(z),
            ue = j(ee, l.length), de = j(re, c.length), fe = j(ie, C.length), pe = j(ae, w.length), B = (0, Y.useRef)(null),
            V = g && _ && !u, me = g && v && c.length > 0, he = g && y && C.length > 0, ge = g && b && w.length > 0,
            _e = V ? ue : -1, ve = () => {
                N >= 0 && te(N)
            }, ye = () => {
                L(D)
            }, be = () => {
                F(O)
            }, xe = () => {
                I(A)
            };
        _ && N !== ne && (P(N), N >= 0 && te(N)), v && e.selectedRule !== se && (R(e.selectedRule), F(O)), y && z !== ce && (le(z), I(A)), (0, Y.useEffect)(() => {
            B.current?.scrollIntoView({block: `nearest`, behavior: `smooth`})
        }, [e.tab, ue, _e, N, de, fe, pe]);
        let Se = e => {
            te(t => {
                let n = t + e;
                return Math.max(0, Math.min(l.length - 1, n))
            })
        };
        k(`DPadUp`, () => Se(-1), V), k(`DPadDown`, () => Se(1), V), k(`DPadRight`, () => {
            let n = l[ue];
            !n || e.memberSwitchLocked || e.selectedMember != null && s(e.selectedMember, n) || (t(f.Menu_Button_Participants_Click), e.onMemberClick(n))
        }, V), k(`Y`, () => {
            t(f.Menu_Button_Back_Click), e.onStatisticsClick()
        }, V);
        let Ce = n => {
            let r = de + n, i = Math.max(0, Math.min(c.length - 1, r));
            F(i);
            let a = c[i];
            a && e.selectedRule !== a.title && (t(f.Menu_Button_Participants_Click), e.onSelectedRule(a.title))
        };
        k(`DPadUp`, () => Ce(-1), me), k(`DPadDown`, () => Ce(1), me);
        let we = n => {
            let r = fe + n, i = Math.max(0, Math.min(C.length - 1, r));
            I(i);
            let a = C[i];
            a && !(e.selectedEffect != null && h(e.selectedEffect, a)) && (t(f.Menu_Button_Participants_Click), e.onSelectedEffect(a))
        };
        k(`DPadUp`, () => we(-1), he), k(`DPadDown`, () => we(1), he);
        let Te = n => {
            let r = pe + n, i = Math.max(0, Math.min(w.length - 1, r));
            L(i);
            let a = w[i]?.achievement;
            a && !(e.selectedAchievement != null && e.selectedAchievement.id === a.id) && (t(f.Menu_Button_Participants_Click), e.onSelectedAchievement(a))
        };
        k(`DPadUp`, () => Te(-1), ge), k(`DPadDown`, () => Te(1), ge);
        let Ee = () => {
            switch (e.tab) {
                case $.PLAYERS:
                    return `Участники`;
                case $.RULES:
                    return `Правила`;
                case $.ACHIEVEMENTS:
                    return `Достижения`;
                case $.EFFECTS:
                    return `Эффекты`
            }
        }, De = (e, n) => {
            s(M, e) || t(f.Menu_Button_Participants_Hover, {panElement: n})
        }, Oe = (n, r) => {
            e.selectedRule !== n && t(f.Menu_Button_Participants_Hover, {panElement: r})
        }, ke = (e, n) => {
            h(E, e) || t(f.Menu_Button_Participants_Hover, {panElement: n})
        }, Ae = (e, n) => {
            T?.id !== e.id && t(f.Menu_Button_Participants_Hover, {panElement: n})
        }, je = e => {
            t(f.Menu_Button_Participants_Hover, {panElement: e})
        }, H = n => {
            if (e.selectedMember != null && s(e.selectedMember, n) || e.memberSwitchLocked) return;
            let r = l.findIndex(e => s(e, n));
            r >= 0 && te(r), t(f.Menu_Button_Participants_Click), e.onMemberClick(n)
        }, Me = (n, r) => {
            e.selectedRule != n && (F(r), t(f.Menu_Button_Participants_Click), e.onSelectedRule(n))
        }, Ne = (n, r) => {
            e.selectedEffect != null && h(e.selectedEffect, n) || (r >= 0 && I(r), t(f.Menu_Button_Participants_Click), e.onSelectedEffect(n))
        }, U = n => {
            if (e.selectedAchievement != null && e.selectedAchievement.id === n.id) return;
            let r = w.findIndex(e => e.achievement?.id === n.id);
            r >= 0 && L(r), t(f.Menu_Button_Participants_Click), e.onSelectedAchievement(n)
        };
        return (0, X.jsxs)(`div`, {
            className: `left-menu`,
            children: [(0, X.jsxs)(`div`, {
                className: `title`,
                children: [Ee(), _ && !n && (0, X.jsxs)(X.Fragment, {
                    children: [(0, X.jsx)(`a`, {
                        className: `left-menu-judge-link`,
                        href: Ju,
                        target: `_blank`,
                        rel: `noopener noreferrer`,
                        "aria-label": `EventLab Nassal`,
                        onMouseEnter: e => t(f.Menu_Head_Hover, {panElement: e.currentTarget}),
                        onClick: () => t(f.Menu_Head_Click)
                    }), (0, X.jsx)(`div`, {className: `left-menu-vertical-divider`}), (0, X.jsx)(`a`, {
                        className: `left-menu-multitranslate-link`,
                        href: qu,
                        target: `_blank`,
                        rel: `noopener noreferrer`,
                        "aria-label": `EventLab Nassal`,
                        onMouseEnter: e => t(f.Menu_Head_Hover, {panElement: e.currentTarget}),
                        onClick: () => t(f.Menu_Head_Click)
                    })]
                }), n && (0, X.jsx)(`button`, {
                    type: `button`,
                    className: `left-menu-volume-toggle${r ? ` is-muted` : ``}`,
                    onClick: i,
                    "aria-label": r ? `Включить звук` : `Выключить звук`,
                    title: r ? `Включить звук` : `Выключить звук`
                }), (V || me || he || ge) && (0, X.jsxs)(`span`, {
                    className: `left-menu-title-gp-hint`,
                    "aria-hidden": !0,
                    children: [(0, X.jsx)(q, {button: `DPadUp`}), (0, X.jsx)(q, {button: `DPadDown`})]
                })]
            }), e.tab == $.PLAYERS && (0, X.jsxs)(`div`, {
                className: `left-menu-players`,
                children: [(0, X.jsx)(`div`, {
                    className: `list players-list`,
                    onMouseLeave: ve,
                    children: u ? (0, X.jsx)(`div`, {
                        className: `loading`,
                        children: `Загрузка...`
                    }) : (0, X.jsx)(`div`, {
                        className: `players-list-scroll`, children: l.map((e, t) => {
                            let n = s(M, e), r = V && t === _e, i = n, o = a && n, c = r && !n;
                            return (0, X.jsxs)(`div`, {
                                ref: r || n ? B : void 0,
                                className: `item${i ? ` selected` : ``}${o ? ` item--insider` : ``}${c ? ` gamepad-focused` : ``}`,
                                onClick: () => H(e),
                                onMouseEnter: n || V && t === ue ? void 0 : t => {
                                    De(e, t.currentTarget)
                                },
                                children: [(0, X.jsx)(`div`, {className: `${x[e.id] ? `online` : `offline`}`}), (0, X.jsx)(`div`, {
                                    className: `position`,
                                    children: t + 1
                                }), (0, X.jsxs)(`div`, {
                                    className: `player-item-info`,
                                    children: [(0, X.jsx)(`div`, {
                                        className: `name`,
                                        children: e.name
                                    }), (0, X.jsx)(Yu, {label: Rc(e), tooltip: zc(e)})]
                                }), (0, X.jsx)(`div`, {
                                    className: `player-item-trailing`,
                                    children: (0, X.jsx)(`div`, {className: `points`, children: o ? 0 : e.ggp})
                                }), c && !n && (0, X.jsx)(q, {button: `DPadRight`, className: `player-item-gp-hint`})]
                            }, e.id)
                        })
                    })
                }), (0, X.jsxs)(`div`, {
                    className: `left-menu-players-footer`,
                    children: [(0, X.jsx)(`div`, {
                        className: `members-list-separator`,
                        children: `//////////////////////////////////`
                    }), (0, X.jsxs)(`div`, {
                        className: `members-list-btn`,
                        onClick: () => {
                            t(f.Menu_Button_Back_Click), e.onStatisticsClick()
                        },
                        onMouseEnter: e => t(f.Menu_Button_Back_Hover, {panElement: e.currentTarget}),
                        children: [`Статистика`, V && (0, X.jsx)(q, {button: `Y`, className: `members-list-btn-gp-hint`})]
                    })]
                })]
            }), e.tab == $.RULES && (0, X.jsxs)(`div`, {
                className: `left-menu-players`,
                children: [(0, X.jsx)(`div`, {
                    className: `list rules-list`,
                    onMouseLeave: be,
                    children: (0, X.jsx)(ao, {
                        children: c.map((t, n) => {
                            let r = e.selectedRule === t.title, i = me && n === de && !r;
                            return (0, X.jsx)(`div`, {
                                ref: i ? B : void 0,
                                className: `item ${r ? `selected` : ``}${i ? ` gamepad-focused` : ``}`,
                                onClick: () => Me(t.title, n),
                                onMouseEnter: r ? void 0 : e => {
                                    Oe(t.title, e.currentTarget)
                                },
                                children: (0, X.jsx)(`div`, {className: `name`, children: t.menuLabel ?? t.title})
                            }, n)
                        })
                    })
                }), (0, X.jsx)(`div`, {
                    className: `left-menu-rules-footer`,
                    children: (0, X.jsxs)(`div`, {
                        className: `records-access-btn`,
                        onClick: () => {
                            t(f.Menu_Button_Back_Click), e.onOpenRecordsAccessGuide()
                        },
                        onMouseEnter: e => t(f.Menu_Button_Back_Hover, {
                            panElement: e.currentTarget,
                            allowDuringTrackPlayback: !0
                        }),
                        children: [(0, X.jsx)(`span`, {children: `Доступ к списку`}), (0, X.jsx)(`span`, {children: `рекордов`})]
                    })
                })]
            }), e.tab == $.EFFECTS && (0, X.jsx)(`div`, {
                className: `left-menu-players`,
                children: (0, X.jsx)(`div`, {
                    className: `list effects-list`,
                    onMouseLeave: xe,
                    children: !e.effects || e.effects.length == 0 ? (0, X.jsx)(`div`, {
                        className: `loading`,
                        children: `Загрузка...`
                    }) : (0, X.jsx)(ao, {
                        children: S.map(e => (0, X.jsxs)(Y.Fragment, {
                            children: [(0, X.jsxs)(`div`, {
                                className: `effects-list-section`,
                                children: [(0, X.jsx)(`span`, {
                                    className: `effects-list-section-label`,
                                    children: e.title
                                }), (0, X.jsx)(`div`, {className: `effects-list-section-line`, "aria-hidden": !0})]
                            }), e.effects.map(e => {
                                let t = h(E, e), n = C.findIndex(t => t.name === e.name), r = he && n === fe && !t;
                                return (0, X.jsx)(`div`, {
                                    ref: r ? B : void 0,
                                    className: `item ${t ? `selected` : ``}${r ? ` gamepad-focused` : ``}`,
                                    onClick: () => Ne(e, n),
                                    onMouseEnter: t ? void 0 : t => {
                                        ke(e, t.currentTarget)
                                    },
                                    children: (0, X.jsx)(`div`, {className: `name effect-name`, children: e.displayName})
                                }, e.name)
                            })]
                        }, e.id))
                    })
                })
            }), e.tab == $.ACHIEVEMENTS && (0, X.jsx)(`div`, {
                className: `left-menu-players`,
                children: (0, X.jsx)(`div`, {
                    className: `list achievements-list`,
                    onMouseLeave: ye,
                    children: p && d.length == 0 ? (0, X.jsx)(`div`, {
                        className: `loading`,
                        children: `Загрузка...`
                    }) : (0, X.jsx)(ao, {
                        children: d.map((e, t) => {
                            let {achievement: n, isRevealed: r} = e;
                            if (!r || n == null) return (0, X.jsx)(`div`, {
                                className: `item item--achievement-hidden`,
                                "aria-label": `Скрытое достижение`,
                                onMouseEnter: e => je(e.currentTarget),
                                children: (0, X.jsx)(`div`, {className: `name`, children: `Неизвестно`})
                            }, `achievement-hidden-${t}`);
                            let i = T?.id === n.id, a = w.findIndex(e => e.achievement?.id === n.id),
                                o = ge && a === pe && !i;
                            return (0, X.jsx)(`div`, {
                                ref: o ? B : void 0,
                                className: `item ${i ? `selected` : ``}${o ? ` gamepad-focused` : ``}`,
                                onClick: () => U(n),
                                onMouseEnter: i ? void 0 : e => {
                                    Ae(n, e.currentTarget)
                                },
                                children: (0, X.jsx)(`div`, {className: `name`, children: n.title})
                            }, n.id)
                        })
                    })
                })
            })]
        })
    }, Zu = ({label: e, items: t, selectedId: n, onSelect: r, onToggleOpen: i, onHeadClick: a, onHeadHover: o}) => {
        let [s, c] = (0, Y.useState)(!1), l = (0, Y.useRef)(null), u = (0, Y.useCallback)(e => {
            c(e), i?.(e)
        }, [i]);
        return (0, Y.useEffect)(() => {
            if (!s) return;
            let e = e => {
                l.current && !l.current.contains(e.target) && u(!1)
            };
            return document.addEventListener(`pointerdown`, e), () => document.removeEventListener(`pointerdown`, e)
        }, [s, u]), t.length <= 1 ? (0, X.jsx)(`span`, {
            className: `title-active`,
            children: e
        }) : (0, X.jsxs)(`div`, {
            className: `title-player-picker`,
            ref: l,
            children: [(0, X.jsxs)(`button`, {
                type: `button`,
                className: `title-active title-active--picker`,
                "aria-expanded": s,
                "aria-haspopup": `listbox`,
                onClick: () => {
                    a?.(), u(!s)
                },
                onMouseEnter: o,
                children: [(0, X.jsx)(`span`, {
                    className: `title-active-name`,
                    children: e
                }), (0, X.jsx)(`span`, {
                    className: `title-picker-chevron${s ? ` title-picker-chevron--open` : ``}`,
                    "aria-hidden": !0
                })]
            }), s && (0, X.jsx)(`div`, {
                className: `title-player-picker-menu`,
                role: `listbox`,
                children: t.map(e => (0, X.jsx)(`button`, {
                    type: `button`,
                    role: `option`,
                    "aria-selected": e.id === n,
                    className: `title-player-picker-item${e.id === n ? ` selected` : ``}`,
                    onClick: () => {
                        a?.(), r(e.id), u(!1)
                    },
                    children: e.label
                }, e.id))
            })]
        })
    }, Qu = /(https?:\/\/)?([\w-]+\.)+[\w-]+(\.[\w-]+)*(\/[\w-._~:/?#[\]@!$&'()*+,;=]*)?/gi,
    $u = /\[([^\]]+)\]\(([^)]+)\)/g;

function ed(e, t, n) {
    return (0, X.jsx)(`a`, {
        href: e.startsWith(`http`) ? e : `https://${e}`,
        target: `_blank`,
        rel: `noopener noreferrer`,
        className: `rules-content__link`,
        children: t
    }, n)
}

function td(e, t) {
    let n = [], r = 0, i, a = 0;
    for (; (i = Qu.exec(e)) !== null;) {
        i.index > r && n.push(e.substring(r, i.index));
        let o = i[0];
        n.push(ed(o, o, `${t}-url-${a}`)), r = i.index + i[0].length, a += 1
    }
    return r < e.length && n.push(e.substring(r)), n.length > 0 ? n : [e]
}

function nd(e, t) {
    let n = [], r = 0, i, a = 0;
    for (; (i = $u.exec(e)) !== null;) i.index > r && n.push(...td(e.substring(r, i.index), `${t}-pre-${a}`)), n.push(ed(i[2], i[1], `${t}-md-${a}`)), r = i.index + i[0].length, a += 1;
    return r < e.length && n.push(...td(e.substring(r), `${t}-tail`)), n.length > 0 ? n : [e]
}

function rd(e, t) {
    let n = e.split(/(\*\*[^*]+\*\*)/g), r = [];
    return n.forEach((e, n) => {
        if (e.startsWith(`**`) && e.endsWith(`**`)) {
            r.push((0, X.jsx)(`strong`, {children: nd(e.slice(2, -2), `${t}-b-${n}`)}, `${t}-b-${n}`));
            return
        }
        e.length !== 0 && r.push(...nd(e, `${t}-t-${n}`))
    }), r
}

function id(e, t, n) {
    let r = n === `ordered` ? `ol` : `ul`,
        i = n === `ordered` ? `rules-content__ordered-list` : `rules-content__bullet-list`,
        a = n === `ordered` ? `rules-content__ordered-item` : `rules-content__bullet-item`;
    return (0, X.jsx)(r, {
        className: i,
        children: e.map((e, r) => `lead` in e ? (0, X.jsxs)(`li`, {
            className: a,
            children: [e.leadPlain ? (0, X.jsxs)(`span`, {children: [e.lead, `:`]}) : (0, X.jsxs)(`strong`, {
                className: `rules-content__list-lead`,
                children: [e.lead, `:`]
            }), e.text != null && (0, X.jsxs)(X.Fragment, {children: [` `, rd(e.text, `${n}-lead-${t}-${r}`)]}), e.subItems != null && (0, X.jsx)(`ul`, {
                className: `rules-content__sub-list`,
                children: e.subItems.map((e, i) => (0, X.jsx)(`li`, {
                    className: `rules-content__sub-item`,
                    children: rd(e, `${n}-sub-${t}-${r}-${i}`)
                }, i))
            })]
        }, r) : (0, X.jsx)(`li`, {className: a, children: rd(e.text, `${n}-${t}-${r}`)}, r))
    })
}

function ad({block: e, index: t}) {
    switch (e.type) {
        case`heading`:
            return (0, X.jsx)(`h3`, {className: `rules-content__heading`, children: e.text});
        case`paragraph`:
            return (0, X.jsx)(`p`, {className: `rules-content__paragraph`, children: rd(e.text, `p-${t}`)});
        case`keyValue`:
            return (0, X.jsxs)(`div`, {
                className: `rules-content__key-value`,
                children: [(0, X.jsx)(`span`, {
                    className: `rules-content__key`,
                    children: e.label
                }), (0, X.jsx)(`span`, {className: `rules-content__value`, children: nd(e.value, `kv-${t}`)})]
            });
        case`list`:
            return (0, X.jsxs)(`div`, {
                className: `rules-content__list-block`,
                children: [e.title != null && (0, X.jsx)(`div`, {
                    className: `rules-content__list-title`,
                    children: e.title
                }), (0, X.jsx)(`ul`, {
                    className: `rules-content__list`,
                    children: e.items.map((e, n) => (0, X.jsx)(`li`, {
                        className: `rules-content__list-item`,
                        children: nd(e, `li-${t}-${n}`)
                    }, n))
                })]
            });
        case`table`:
            return (0, X.jsx)(`div`, {
                className: `rules-content__table-wrap`,
                children: (0, X.jsxs)(`table`, {
                    className: `rules-content__table`,
                    children: [(0, X.jsx)(`thead`, {children: (0, X.jsx)(`tr`, {children: e.headers.map(e => (0, X.jsx)(`th`, {children: e}, e))})}), (0, X.jsx)(`tbody`, {children: e.rows.map((e, t) => (0, X.jsx)(`tr`, {children: e.map((e, t) => (0, X.jsx)(`td`, {children: e}, t))}, t))})]
                })
            });
        case`warning`:
            return (0, X.jsxs)(`div`, {
                className: `rules-content__callout rules-content__callout--warning`,
                children: [e.title != null && (0, X.jsx)(`div`, {
                    className: `rules-content__callout-title`,
                    children: e.title
                }), e.items == null ? e.text == null ? null : (0, X.jsx)(`p`, {
                    className: `rules-content__paragraph rules-content__paragraph--flush`,
                    children: nd(e.text, `warn-p-${t}`)
                }) : (0, X.jsx)(`ul`, {
                    className: `rules-content__letter-list`,
                    children: e.items.map((e, n) => (0, X.jsx)(`li`, {
                        className: `rules-content__letter-item`,
                        children: nd(e, `warn-${t}-${n}`)
                    }, n))
                })]
            });
        case`formula`:
            return (0, X.jsx)(`div`, {className: `rules-content__formula`, children: e.text});
        case`note`:
            return (0, X.jsx)(`p`, {className: `rules-content__note`, children: nd(e.text, `note-${t}`)});
        case`footnote`:
            return (0, X.jsxs)(`p`, {
                className: `rules-content__footnote`,
                children: [e.starred === !0 && (0, X.jsx)(`span`, {
                    className: `rules-content__footnote-star`,
                    children: `*`
                }), rd(e.text, `fn-${t}`)]
            });
        case`examplesPanel`:
            return (0, X.jsxs)(`div`, {
                className: `rules-content__examples-panel`,
                children: [(0, X.jsx)(`div`, {
                    className: `rules-content__examples-panel-title`,
                    children: e.title
                }), e.items.map((e, n) => (0, X.jsx)(`p`, {
                    className: `rules-content__examples-panel-item`, children: e.split(`
`).map((e, r) => (0, X.jsxs)(`span`, {children: [r > 0 && (0, X.jsx)(`br`, {}), rd(e, `ex-${t}-${n}-${r}`)]}, r))
                }, n))]
            });
        case`patternBullets`:
            return (0, X.jsx)(`ul`, {
                className: `rules-content__pattern-bullets`, children: e.items.map((e, t) => {
                    let n = e.emphasizeResult !== !1;
                    return (0, X.jsxs)(`li`, {
                        className: `rules-content__pattern-bullet`,
                        children: [(0, X.jsx)(`span`, {
                            className: `rules-content__pattern-condition`,
                            children: e.condition
                        }), (0, X.jsx)(`span`, {
                            className: `rules-content__pattern-arrow`,
                            "aria-hidden": !0,
                            children: `→`
                        }), (0, X.jsx)(`span`, {
                            className: n ? `rules-content__pattern-result rules-content__pattern-result--emphasized` : `rules-content__pattern-result`,
                            children: e.result
                        })]
                    }, t)
                })
            });
        case`limitCallout`:
            return (0, X.jsx)(`div`, {
                className: `rules-content__limit-callout`,
                children: (0, X.jsxs)(`p`, {
                    className: `rules-content__limit-callout-text`,
                    children: [(0, X.jsxs)(`strong`, {children: [e.label, `:`]}), ` `, (0, X.jsx)(`em`, {children: e.text})]
                })
            });
        case`labeledBullets`:
            return (0, X.jsx)(`ul`, {
                className: `rules-content__labeled-bullets`,
                children: e.items.map((e, n) => (0, X.jsxs)(`li`, {
                    className: `rules-content__labeled-bullet`,
                    children: [(0, X.jsxs)(`strong`, {
                        className: `rules-content__labeled-bullet-label`,
                        children: [e.label, `:`]
                    }), ` `, rd(e.text, `lb-${t}-${n}`)]
                }, n))
            });
        case`capsHeading`:
            return (0, X.jsx)(`h3`, {className: `rules-content__caps-heading`, children: e.text});
        case`orderedItems`:
            return id(e.items, t, `ordered`);
        case`bulletItems`:
            return id(e.items, t, `bullet`);
        case`cycle`:
            return (0, X.jsx)(`div`, {
                className: `rules-content__cycle-panel`,
                children: (0, X.jsx)(`ul`, {
                    className: `rules-content__cycle-list`,
                    children: Ou.map(e => (0, X.jsxs)(`li`, {
                        className: `rules-content__cycle-item`,
                        children: [(0, X.jsx)(`span`, {
                            className: `rules-content__cycle-bullet`,
                            "aria-hidden": !0,
                            children: `→`
                        }), (0, X.jsxs)(`span`, {
                            className: `rules-content__cycle-line`,
                            children: [e.title, e.badge != null && e.badgeTone != null && (0, X.jsx)(`span`, {
                                className: `rules-content__cycle-badge rules-content__cycle-badge--${e.badgeTone}`,
                                children: e.badge
                            })]
                        })]
                    }, e.title))
                })
            });
        default:
            return null
    }
}

function od({section: e}) {
    if (e == null || e.blocks.length === 0) return null;
    let t = e.title === `Аукцион` ? `rules-content--auction` : e.title === `Судейские полномочия и штрафы` ? `rules-content--judge` : e.title === `Прохождение игры` ? `rules-content--gameplay` : e.title === `Выбор сложности` ? `rules-content--difficulty` : e.title === `Формула расчета очков` ? `rules-content--score` : e.title === `Колесо приколов` ? `rules-content--wheel` : ``;
    return (0, X.jsx)(`div`, {
        className: t ? `rules-content ${t}` : `rules-content`,
        children: e.blocks.map((e, t) => (0, X.jsx)(ad, {block: e, index: t}, t))
    })
}

var sd = [3, 4, 5, 6], cd = 20, ld = 6, ud = 15, dd = 20, fd = 90, pd = 10, md = 3,
    hd = [`1-5ч`, `6-10ч`, `11-15ч`, `16-20ч`], gd = e => !Number.isFinite(e) || e <= 0 ? 0 : Math.floor(e + .5),
    _d = e => {
        if (!Number.isFinite(e) || e <= 0) return {segments: [], points: 0};
        let t = [], n = 0, r = e;
        for (; r > 1e-9;) for (let e = 0; e < sd.length && !(r <= 1e-9); e++) {
            let i = sd[e], a = Math.min(r, 5), o = a * i;
            t.push({multiplier: i, hours: a, points: o, rangeLabel: hd[e]}), n += o, r -= a
        }
        return {segments: t, points: n}
    }, vd = e => {
        if (e === null || !Number.isFinite(e)) return {
            label: `Нет данных`,
            rangeLabel: `нет даты релиза`,
            releaseYear: e,
            points: md
        };
        let t = e, n = t > 0, r = 0, i = `—`, a = `—`;
        return n && (t >= 2024 ? (r = 1, i = `Свежая`, a = `2024+`) : t >= 2020 ? (r = 2, i = `Не первой свежести`, a = `2020–2023`) : t >= 2016 ? (r = 3, i = `Залежавшаяся`, a = `2016–2019`) : t >= 2010 ? (r = 4, i = `Жухлая`, a = `2010–2015`) : t >= 2006 ? (r = 5, i = `С запашком`, a = `2006–2009`) : t >= 2001 ? (r = 7, i = `Сгнившая`, a = `2001–2005`) : (r = 10, i = `Кал мамонта`, a = `2000 и старше`)), {
            label: i,
            rangeLabel: a,
            releaseYear: t,
            points: r
        }
    }, yd = e => {
        if (e === null || !Number.isFinite(e)) return {score: e, rangeLabel: `нет оценки`, points: md};
        let t = e;
        return t >= 90 ? {score: t, rangeLabel: `90–100`, points: 1} : t >= 81 ? {
            score: t,
            rangeLabel: `81–89`,
            points: 2
        } : t >= 76 ? {score: t, rangeLabel: `76–80`, points: 3} : t >= 71 ? {
            score: t,
            rangeLabel: `71–75`,
            points: 4
        } : t >= 61 ? {score: t, rangeLabel: `61–70`, points: 5} : t >= 51 ? {
            score: t,
            rangeLabel: `51–60`,
            points: 6
        } : t >= 45 ? {score: t, rangeLabel: `45–50`, points: 7} : {score: t, rangeLabel: `44 и ниже`, points: 10}
    }, bd = [{multiplier: 3, label: `1-5ч`}, {multiplier: 4, label: `6-10ч`}, {
        multiplier: 5,
        label: `11-15ч`
    }, {multiplier: 6, label: `16-20ч`}], xd = [{multiplier: 3, label: `1-10ч`}, {multiplier: 5, label: `11-20ч`}],
    Sd = e => {
        if (e === null || !Number.isFinite(e)) return {
            label: `Нет данных`,
            rangeLabel: `нет даты релиза`,
            releaseYear: e,
            points: md
        };
        let t = e;
        return t > 0 ? t >= 2015 ? {
            label: `Свежая`,
            rangeLabel: `2015+`,
            releaseYear: t,
            points: 1
        } : t >= 2010 ? {
            label: `Старая`,
            rangeLabel: `2010–2014`,
            releaseYear: t,
            points: 2
        } : t >= 2003 ? {label: `Древняя`, rangeLabel: `2003–2009`, releaseYear: t, points: 3} : {
            label: `Кал мамонта`,
            rangeLabel: `2002 и старше`,
            releaseYear: t,
            points: 5
        } : {label: `—`, rangeLabel: `—`, releaseYear: t, points: 0}
    }, Cd = e => {
        if (e === null || !Number.isFinite(e)) return {score: e, rangeLabel: `нет оценки`, points: md};
        let t = e;
        return t >= 80 ? {score: t, rangeLabel: `80–100`, points: 1} : t >= 50 ? {
            score: t,
            rangeLabel: `50–79`,
            points: 2
        } : t >= 10 ? {score: t, rangeLabel: `10–49`, points: 4} : {score: t, rangeLabel: `0–9`, points: 2}
    }, wd = e => {
        let t = Number.isFinite(e.hltbHours) && e.hltbHours > 0 ? e.hltbHours : 0,
            n = Number.isFinite(e.playedHours) && e.playedHours > 0 ? e.playedHours : 0, r = Math.floor(t), i = [],
            a = Math.min(r, 10);
        a > 0 && i.push({multiplier: 3, hours: a, points: a * 3, rangeLabel: `1-10ч`});
        let o = Math.min(Math.max(r - 10, 0), 10);
        o > 0 && i.push({multiplier: 5, hours: o, points: o * 5, rangeLabel: `11-20ч`});
        let s = r > 20 ? pd : 0, c = a * 3 + o * 5 + s, l = Math.min(c, fd), u = c > fd,
            d = s > 0 ? {label: `>20ч бонус`, points: s} : void 0, f = Sd(e.releaseYear), p = Cd(e.ratingScore);
        return {
            mode: `nasral`,
            total: l + f.points + p.points,
            timePoints: l,
            rawTimePoints: c,
            freshnessPoints: f.points,
            ratingPoints: p.points,
            modifier: `none`,
            actualHours: n,
            hltbHours: t,
            cappedActualHours: r,
            effectiveHours: r,
            cycleSegments: i,
            legend: xd,
            bonusBadge: d,
            freshness: f,
            rating: p,
            isShorty: !1,
            isExtendedCycle: r > 20,
            shortyCapApplied: !1,
            timeCapApplied: u
        }
    }, Td = e => {
        if (e.mode === `nasral`) return wd(e);
        let t = Number.isFinite(e.hltbHours) && e.hltbHours > 0 ? e.hltbHours : 0,
            n = Number.isFinite(e.playedHours) && e.playedHours > 0 ? e.playedHours : 0, r = Math.min(n, t), i = `none`,
            a = r, o = [], s = 0, c = !1;
        if (t <= 0 || n <= 0) i = `none`, a = 0, o = [], s = 0; else if (t < ld) {
            i = `shorty`, a = t;
            let {segments: e, points: n} = _d(t);
            n > ud ? (s = ud, c = !0, o = [{multiplier: 3, hours: 5, points: 15, rangeLabel: `1-5ч`}]) : (s = n, o = e)
        } else {
            let e = Math.min(t, dd);
            if (n < e) {
                i = `skill-tax`, a = (e + n) / 2;
                let {segments: t, points: r} = _d(a);
                o = t, s = r
            } else if (n > t) {
                i = `hltb`, a = t;
                let {segments: e, points: n} = _d(t);
                o = e, s = n
            } else {
                i = `none`, a = n;
                let {segments: e, points: t} = _d(n);
                o = e, s = t
            }
        }
        let l = vd(e.releaseYear), u = yd(e.ratingScore), d = gd(s + l.points + u.points), f = gd(s), p = a > cd + 1e-9;
        return {
            mode: `nassal`,
            total: d,
            timePoints: f,
            rawTimePoints: s,
            freshnessPoints: l.points,
            ratingPoints: u.points,
            modifier: i,
            actualHours: n,
            hltbHours: t,
            cappedActualHours: r,
            effectiveHours: a,
            cycleSegments: o,
            legend: bd,
            freshness: l,
            rating: u,
            isShorty: i === `shorty`,
            isExtendedCycle: p,
            shortyCapApplied: c,
            timeCapApplied: !1
        }
    };

function Ed(e) {
    let t = ft(), n = e?.disabled ?? !1, [r, i] = (0, Y.useState)(!1), a = (0, Y.useRef)(null),
        o = (0, Y.useCallback)(() => {
            i(!1)
        }, []);
    (0, Y.useEffect)(() => {
        if (!t || !r) return;
        let e = () => {
            o()
        }, n = t => {
            let n = t.target;
            n instanceof Node && (a.current?.contains(n) || n instanceof Element && n.closest(`[data-pointer-tooltip-root="true"]`) || e())
        };
        return document.addEventListener(`touchstart`, n, {
            capture: !0,
            passive: !0
        }), document.addEventListener(`scroll`, e, !0), document.addEventListener(`touchmove`, e, {
            capture: !0,
            passive: !0
        }), () => {
            document.removeEventListener(`touchstart`, n, !0), document.removeEventListener(`scroll`, e, !0), document.removeEventListener(`touchmove`, e, !0)
        }
    }, [o, t, r]);
    let s = (0, Y.useCallback)(e => n ? e ?? {} : t ? {
        ...e, onClick: t => {
            e?.onClick?.(t), t.stopPropagation(), i(e => !e)
        }
    } : {
        ...e, onMouseEnter: t => {
            e?.onMouseEnter?.(t), i(!0)
        }, onMouseLeave: t => {
            e?.onMouseLeave?.(t), i(!1)
        }
    }, [n, t]);
    return {
        open: n ? !1 : r,
        close: o,
        isMobileLayout: t,
        triggerRef: a,
        tooltipRootProps: {"data-pointer-tooltip-root": `true`},
        getTriggerProps: s
    }
}

var Dd = e => {
        if (!Number.isFinite(e) || e <= 0) return `0ч`;
        let t = Math.round(e * 100) / 100;
        return Number.isInteger(t) ? `${t}ч` : `${t.toFixed(2).replace(/0+$/, ``).replace(/\.$/, ``).replace(`.`, `,`)}ч`
    }, Od = e => {
        let t = Math.round(e * 10) / 10;
        return Number.isInteger(t) ? String(t) : t.toFixed(1).replace(`.`, `,`)
    }, kd = e => e >= 0 ? `+${e}` : String(e), Ad = {
        3: `game-score-tooltip-mult-3`,
        4: `game-score-tooltip-mult-4`,
        5: `game-score-tooltip-mult-5`,
        6: `game-score-tooltip-mult-6`
    }, jd = {shorty: `КОРОТЫШ`, hltb: `HLTB`, "skill-tax": `НАЛОГ НА СКИЛЛ`, none: ``},
    Md = ({data: e, children: t, anchorSelector: n, factoryAssembly: r = !1}) => {
        let i = (0, Y.useRef)(null), a = (0, Y.useRef)(null), o = ft(), {
            open: s,
            getTriggerProps: c,
            tooltipRootProps: l,
            triggerRef: u
        } = Ed(), d = e => {
            i.current = e, u.current = e
        }, [f, p] = (0, Y.useState)(null), m = (0, Y.useMemo)(() => Td(e), [e]);
        return (0, Y.useLayoutEffect)(() => {
            if (!s || !i.current || !a.current) {
                p(null);
                return
            }
            let e = ((n ? i.current.querySelector(n) : null) ?? i.current.firstElementChild ?? i.current).getBoundingClientRect(),
                t = a.current.getBoundingClientRect(), r = window.innerWidth, c = window.innerHeight;
            if (o) {
                let n = Math.min(t.width, r - 16), i = Math.max(8, (r - n) / 2), a = e.bottom + 12;
                a + t.height > c - 8 && (a = Math.max(8, e.top - t.height - 12)), p({
                    top: a,
                    left: i,
                    placement: `right`
                });
                return
            }
            let l = `left`, u = e.left - t.width - 12;
            u < 8 && (l = `right`, u = e.right + 12, u + t.width > r - 8 && (u = Math.max(8, r - t.width - 8)));
            let d = e.top + e.height / 2 - t.height / 2;
            d = Math.max(8, Math.min(d, c - t.height - 8)), p({top: d, left: u, placement: l})
        }, [s, m, n, o]), (0, X.jsxs)(X.Fragment, {
            children: [(0, X.jsx)(`div`, {
                ref: d,
                className: `game-score-tooltip-trigger`, ...c(),
                children: t
            }), s && (0, Xn.createPortal)((0, X.jsx)(`div`, {
                ref: a, ...l,
                className: `game-score-tooltip${o ? ` game-score-tooltip--mobile` : ``}${f ? ` game-score-tooltip--${f.placement}` : ` game-score-tooltip--measuring`}`,
                style: f ? {top: f.top, left: f.left} : {top: -9999, left: -9999},
                role: `tooltip`,
                children: (0, X.jsx)(Nd, {breakdown: m, factoryAssembly: r})
            }), document.body)]
        })
    }, Nd = ({breakdown: e, factoryAssembly: t = !1}) => (0, X.jsxs)(X.Fragment, {
        children: [(0, X.jsx)(Pd, {
            breakdown: e,
            factoryAssembly: t
        }), (0, X.jsx)(Fd, {
            breakdown: e,
            hideWhenNone: !0
        }), (0, X.jsx)(Id, {breakdown: e}), t ? (0, X.jsx)(Rd, {}) : (0, X.jsxs)(X.Fragment, {children: [(0, X.jsx)(Ld, {breakdown: e}), (0, X.jsx)(zd, {breakdown: e})]})]
    }), Pd = ({breakdown: e, factoryAssembly: t = !1}) => {
        let n = t ? e.timePoints : e.total;
        return t ? (0, X.jsxs)(`div`, {
            className: `game-score-tooltip-summary`,
            children: [(0, X.jsx)(Bd, {
                value: e.timePoints,
                label: `ВРЕМЯ`,
                color: `time`
            }), (0, X.jsx)(Vd, {children: `=`}), (0, X.jsx)(Bd, {value: n, label: `ИТОГО`, color: `total`})]
        }) : (0, X.jsxs)(`div`, {
            className: `game-score-tooltip-summary`,
            children: [(0, X.jsx)(Bd, {
                value: e.timePoints,
                label: `ВРЕМЯ`,
                color: `time`
            }), (0, X.jsx)(Vd, {children: `+`}), (0, X.jsx)(Bd, {
                value: e.freshnessPoints,
                label: `СВЕЖЕСТЬ`,
                color: `freshness`
            }), (0, X.jsx)(Vd, {children: `+`}), (0, X.jsx)(Bd, {
                value: e.ratingPoints,
                label: `РЕЙТИНГ`,
                color: `rating`
            }), (0, X.jsx)(Vd, {children: `=`}), (0, X.jsx)(Bd, {value: n, label: `ИТОГО`, color: `total`})]
        })
    }, Fd = ({
                 breakdown: e,
                 hideWhenNone: t = !1
             }) => e.mode === `nasral` ? t ? null : (0, X.jsxs)(`div`, {
        className: `game-score-tooltip-modifier game-score-tooltip-modifier--empty`,
        children: [(0, X.jsxs)(`div`, {
            className: `game-score-tooltip-modifier-header`,
            children: [(0, X.jsx)(`span`, {
                className: `game-score-tooltip-modifier-title`,
                children: `Модификатор`
            }), (0, X.jsx)(`span`, {
                className: `game-score-tooltip-modifier-badge game-score-tooltip-modifier-badge--muted`,
                children: `НЕ ПРИМЕНЯЕТСЯ`
            })]
        }), (0, X.jsx)(`div`, {
            className: `game-score-tooltip-modifier-description`,
            children: `в NASRAL модификаторов нет — время стримера не учитывается, очки берутся только из времени HLTB.`
        })]
    }) : e.modifier === `none` ? t ? null : (0, X.jsxs)(`div`, {
        className: `game-score-tooltip-modifier game-score-tooltip-modifier--empty`,
        children: [(0, X.jsxs)(`div`, {
            className: `game-score-tooltip-modifier-header`,
            children: [(0, X.jsx)(`span`, {
                className: `game-score-tooltip-modifier-title`,
                children: `Модификатор`
            }), (0, X.jsx)(`span`, {
                className: `game-score-tooltip-modifier-badge game-score-tooltip-modifier-badge--muted`,
                children: `НЕТ`
            })]
        }), (0, X.jsx)(`div`, {
            className: `game-score-tooltip-modifier-description`,
            children: `время рассчитывается напрямую — фактическое время прохождения применяется к 20-часовому циклу.`
        })]
    }) : (0, X.jsx)(Hd, {
        modifier: e.modifier,
        actualHours: e.actualHours,
        hltbHours: e.hltbHours,
        effectiveHours: e.effectiveHours,
        label: jd[e.modifier]
    }), Id = ({breakdown: e}) => (0, X.jsxs)(`div`, {
        className: `game-score-tooltip-section game-score-tooltip-section--time`,
        children: [(0, X.jsxs)(`div`, {
            className: `game-score-tooltip-section-header`,
            children: [(0, X.jsx)(`span`, {
                className: `game-score-tooltip-section-title`,
                children: `Время прохождения`
            }), (0, X.jsx)(`span`, {
                className: `game-score-tooltip-section-value game-score-tooltip-section-value--time`,
                children: kd(e.timePoints)
            })]
        }), e.mode === `nasral` ? (0, X.jsxs)(`div`, {
            className: `game-score-tooltip-section-description`,
            children: [`по HLTB `, (0, X.jsxs)(`strong`, {children: [Math.floor(e.hltbHours), `ч`]}), e.bonusBadge && (0, X.jsx)(X.Fragment, {children: ` · +10 бонус за >20ч`}), e.timeCapApplied && (0, X.jsx)(X.Fragment, {children: ` · лимит 90`})]
        }) : e.isShorty ? (0, X.jsxs)(`div`, {
            className: `game-score-tooltip-section-description`,
            children: [`короткая игра (< 6ч по HLTB) — очки начисляются по`, ` `, (0, X.jsx)(`strong`, {children: Dd(e.hltbHours)}), ` HLTB, максимум`, ` `, (0, X.jsx)(`strong`, {children: `15 очков`}), e.shortyCapApplied && (0, X.jsx)(X.Fragment, {children: ` · лимит применён`})]
        }) : (0, X.jsxs)(`div`, {
            className: `game-score-tooltip-section-description`,
            children: [`начислено за `, (0, X.jsx)(`strong`, {children: Dd(e.effectiveHours)}), e.isExtendedCycle && (0, X.jsxs)(X.Fragment, {children: [` `, `— часы свыше 20 идут по тому же `, (0, X.jsx)(`strong`, {children: `20-часовому циклу`})]})]
        }), (0, X.jsx)(Wd, {
            segments: e.cycleSegments,
            bonusBadge: e.bonusBadge
        }), (0, X.jsxs)(`div`, {
            className: `game-score-tooltip-legend`,
            children: [e.legend.map(e => (0, X.jsx)(Gd, {
                mult: e.multiplier,
                label: e.label
            }, `${e.multiplier}-${e.label}`)), e.bonusBadge && (0, X.jsxs)(`div`, {
                className: `game-score-tooltip-legend-item`,
                children: [(0, X.jsx)(`span`, {className: `game-score-tooltip-legend-swatch game-score-tooltip-legend-swatch--bonus`}), (0, X.jsx)(`span`, {
                    className: `game-score-tooltip-legend-label`,
                    children: e.bonusBadge.label
                }), (0, X.jsxs)(`span`, {
                    className: `game-score-tooltip-legend-mult`,
                    children: [`+`, e.bonusBadge.points]
                })]
            })]
        })]
    }), Ld = ({breakdown: e}) => (0, X.jsxs)(`div`, {
        className: `game-score-tooltip-section game-score-tooltip-section--freshness`,
        children: [(0, X.jsxs)(`div`, {
            className: `game-score-tooltip-section-header`,
            children: [(0, X.jsx)(`span`, {
                className: `game-score-tooltip-section-title`,
                children: `Свежесть`
            }), (0, X.jsx)(`span`, {
                className: `game-score-tooltip-section-value game-score-tooltip-section-value--freshness`,
                children: kd(e.freshnessPoints)
            })]
        }), (0, X.jsxs)(`div`, {
            className: `game-score-tooltip-section-description`,
            children: [(0, X.jsx)(`strong`, {children: e.freshness.label}), ` · `, (0, X.jsx)(`span`, {
                className: `game-score-tooltip-muted`,
                children: e.freshness.rangeLabel
            }), ` `, (0, X.jsx)(`span`, {
                className: `game-score-tooltip-muted`,
                children: e.freshness.releaseYear == null ? `(дата релиза неизвестна)` : e.freshness.releaseYear === 0 ? `(ещё не вышла)` : `(релиз ${e.freshness.releaseYear})`
            })]
        })]
    }), Rd = () => (0, X.jsxs)(`div`, {
        className: `game-score-tooltip-section game-score-tooltip-section--effect`,
        children: [(0, X.jsx)(`div`, {
            className: `game-score-tooltip-section-header`,
            children: (0, X.jsx)(`span`, {className: `game-score-tooltip-section-title`, children: `Эффект`})
        }), (0, X.jsxs)(`div`, {
            className: `game-score-tooltip-section-description`,
            children: [(0, X.jsx)(`strong`, {children: `Заводская сборка.`}), ` Текущая игра у выбранного стримера оценивается только по времени прохождения без учета других параметров.`]
        })]
    }), zd = ({breakdown: e}) => (0, X.jsxs)(`div`, {
        className: `game-score-tooltip-section game-score-tooltip-section--rating`,
        children: [(0, X.jsxs)(`div`, {
            className: `game-score-tooltip-section-header`,
            children: [(0, X.jsx)(`span`, {
                className: `game-score-tooltip-section-title`,
                children: `Рейтинг HLTB`
            }), (0, X.jsx)(`span`, {
                className: `game-score-tooltip-section-value game-score-tooltip-section-value--rating`,
                children: kd(e.ratingPoints)
            })]
        }), (0, X.jsx)(`div`, {
            className: `game-score-tooltip-section-description`,
            children: e.rating.score == null ? (0, X.jsxs)(X.Fragment, {children: [`оценка не указана на HLTB →`, ` `, (0, X.jsx)(`strong`, {children: e.rating.points}), ` очка по умолчанию`]}) : (0, X.jsxs)(X.Fragment, {children: [`оценка `, (0, X.jsx)(`strong`, {children: e.rating.score}), ` → диапазон`, ` `, (0, X.jsx)(`strong`, {children: e.rating.rangeLabel})]})
        })]
    }), Bd = ({
                  value: e,
                  label: t,
                  color: n
              }) => (0, X.jsxs)(`div`, {
        className: `game-score-tooltip-summary-cell game-score-tooltip-summary-cell--${n}`,
        children: [(0, X.jsx)(`span`, {
            className: `game-score-tooltip-summary-value`,
            children: e
        }), (0, X.jsx)(`span`, {className: `game-score-tooltip-summary-label`, children: t})]
    }), Vd = ({children: e}) => (0, X.jsx)(`span`, {className: `game-score-tooltip-summary-op`, children: e}),
    Hd = ({modifier: e, actualHours: t, hltbHours: n, effectiveHours: r, label: i}) => {
        let a = r - t, o = Math.abs(a) < 1e-6 ? `neutral` : a > 0 ? `good` : `bad`;
        return (0, X.jsxs)(`div`, {
            className: `game-score-tooltip-modifier`,
            children: [(0, X.jsxs)(`div`, {
                className: `game-score-tooltip-modifier-header`,
                children: [(0, X.jsx)(`span`, {
                    className: `game-score-tooltip-modifier-title`,
                    children: `Модификатор`
                }), (0, X.jsx)(`span`, {className: `game-score-tooltip-modifier-badge`, children: i})]
            }), e === `skill-tax` && (0, X.jsxs)(X.Fragment, {
                children: [(0, X.jsxs)(`div`, {
                    className: `game-score-tooltip-modifier-times`,
                    children: [(0, X.jsx)(Ud, {
                        label: `ФАКТ`,
                        value: Dd(t),
                        kind: `fact`
                    }), (0, X.jsx)(Ud, {
                        label: `HLTB`,
                        value: Dd(n),
                        kind: `hltb`
                    }), (0, X.jsx)(`span`, {
                        className: `game-score-tooltip-modifier-arrow${o === `neutral` ? `` : ` game-score-tooltip-modifier-arrow--${o}`}`,
                        children: `→`
                    }), (0, X.jsx)(Ud, {label: `РАСЧЁТ`, value: Dd(r), kind: `calc`, tone: o})]
                }), (0, X.jsx)(`div`, {
                    className: `game-score-tooltip-modifier-description`,
                    children: `прошёл быстрее порога → расчётное время = среднее между фактом и порогом min(HLTB, 20ч)`
                })]
            }), e === `hltb` && (0, X.jsxs)(X.Fragment, {
                children: [(0, X.jsxs)(`div`, {
                    className: `game-score-tooltip-modifier-times`,
                    children: [(0, X.jsx)(Ud, {
                        label: `ФАКТ`,
                        value: Dd(t),
                        kind: `fact`
                    }), (0, X.jsx)(Ud, {
                        label: `HLTB`,
                        value: Dd(n),
                        kind: `hltb`
                    }), (0, X.jsx)(`span`, {
                        className: `game-score-tooltip-modifier-arrow${o === `neutral` ? `` : ` game-score-tooltip-modifier-arrow--${o}`}`,
                        children: `→`
                    }), (0, X.jsx)(Ud, {label: `РАСЧЁТ`, value: Dd(r), kind: `calc`, tone: o})]
                }), (0, X.jsx)(`div`, {
                    className: `game-score-tooltip-modifier-description`,
                    children: `прошёл дольше, чем по HLTB → время срезано до времени по HLTB`
                })]
            }), e === `shorty` && (0, X.jsx)(`div`, {
                className: `game-score-tooltip-modifier-description`,
                children: `короткая игра (< 6ч по HLTB): очки начисляются по времени HLTB, максимум 15 очков`
            })]
        })
    }, Ud = ({
                 label: e,
                 value: t,
                 kind: n,
                 tone: r
             }) => (0, X.jsxs)(`div`, {
        className: `game-score-tooltip-time-cell game-score-tooltip-time-cell--${n}${n === `calc` && r && r !== `neutral` ? ` game-score-tooltip-time-cell--calc-${r}` : ``}`,
        children: [(0, X.jsx)(`span`, {
            className: `game-score-tooltip-time-label`,
            children: e
        }), (0, X.jsx)(`span`, {className: `game-score-tooltip-time-value`, children: t})]
    }), Wd = ({
                  segments: e,
                  bonusBadge: t
              }) => e.length === 0 && !t || e.reduce((e, t) => e + t.hours, 0) <= 0 && !t ? null : (0, X.jsxs)(`div`, {
        className: `game-score-tooltip-bars`,
        children: [e.map((e, t) => (0, X.jsx)(`div`, {
            className: `game-score-tooltip-bar ${Ad[e.multiplier]}`,
            style: {flex: e.hours},
            title: `×${e.multiplier}`,
            children: (0, X.jsx)(`span`, {className: `game-score-tooltip-bar-value`, children: Od(e.points)})
        }, t)), t && (0, X.jsx)(`div`, {
            className: `game-score-tooltip-bar game-score-tooltip-bar--bonus`,
            style: {flex: `0 0 auto`},
            title: t.label,
            children: (0, X.jsxs)(`span`, {className: `game-score-tooltip-bar-value`, children: [`+`, t.points]})
        })]
    }), Gd = ({mult: e, label: t}) => (0, X.jsxs)(`div`, {
        className: `game-score-tooltip-legend-item`,
        children: [(0, X.jsx)(`span`, {className: `game-score-tooltip-legend-swatch ${Ad[e]}`}), (0, X.jsx)(`span`, {
            className: `game-score-tooltip-legend-label`,
            children: t
        }), (0, X.jsxs)(`span`, {className: `game-score-tooltip-legend-mult`, children: [`·`, e]})]
    }), Kd = .1, qd = 30, Jd = .25, Yd = e => !Number.isFinite(e) || e <= 0 ? qd : Math.max(qd, e + 1),
    Xd = (e, t) => Number.isFinite(e) ? Math.max(Kd, Math.min(t, e)) : Kd, Zd = e => {
        let t = Math.round(e * 4) / 4;
        return Number.isInteger(t) ? `${t} ч` : `${t.toFixed(2).replace(/0+$/, ``).replace(/\.$/, ``).replace(`.`, `,`)} ч`
    }, Qd = e => e != null && e > 0 ? Je(e) : null, $d = () => {
        let {
                play: e,
                playKeyboard: t
            } = oe(), [n, r] = (0, Y.useState)(``), [i, a] = (0, Y.useState)([]), [o, s] = (0, Y.useState)(!1), [c, l] = (0, Y.useState)(``), [u, d] = (0, Y.useState)(null), [p, m] = (0, Y.useState)(!1), [h, g] = (0, Y.useState)(0), [_, v] = (0, Y.useState)(`nassal`),
            y = (0, Y.useRef)(null), b = (0, Y.useRef)(null), x = (0, Y.useCallback)(async e => {
                s(!0), l(e);
                try {
                    a(await me(e) ?? [])
                } catch {
                    a([])
                } finally {
                    s(!1)
                }
            }, []);
        (0, Y.useEffect)(() => {
            if (y.current &&= (clearTimeout(y.current), null), n.trim().length === 0) {
                a([]), l(``);
                return
            }
            return n !== c && (y.current = window.setTimeout(() => {
                x(n.trim())
            }, 500)), () => {
                y.current &&= (clearTimeout(y.current), null)
            }
        }, [n, c, x]), (0, Y.useEffect)(() => {
            let e = e => {
                b.current && !b.current.contains(e.target) && m(!1)
            };
            return document.addEventListener(`mousedown`, e), () => document.removeEventListener(`mousedown`, e)
        }, []);
        let S = e => {
                d(e), m(!1), r(``), a([]), l(``);
                let t = (e.seconds ?? 0) / 3600;
                g(Xd(t, Yd(t)))
            }, C = (0, Y.useMemo)(() => (u?.seconds ?? 0) / 3600, [u]), w = (0, Y.useMemo)(() => Yd(C), [C]),
            T = (0, Y.useMemo)(() => u ? Td({
                releaseYear: u.releaseYear ?? null,
                hltbHours: C,
                playedHours: h,
                ratingScore: u.reviewScore ?? null,
                mode: _
            }) : null, [u, C, h, _]), E = (h - Kd) / (w - Kd) * 100,
            D = u && C > 0 && C <= w ? (C - Kd) / (w - Kd) * 100 : null, O = u ? Qd(u.seconds) : null, k = () => {
                d(null), r(``), a([]), l(``), m(!1), g(0)
            };
        return (0, X.jsxs)(`div`, {
            className: `score-calculator`, children: [(0, X.jsxs)(`div`, {
                className: `score-calculator-search`,
                ref: b,
                children: [u ? (0, X.jsxs)(`div`, {
                    className: `score-calculator-selected`,
                    onClick: () => m(e => !e),
                    role: `button`,
                    tabIndex: 0,
                    children: [u.imageUrl && (0, X.jsx)(`img`, {
                        src: u.imageUrl,
                        alt: u.name,
                        className: `score-calculator-selected-image`
                    }), (0, X.jsxs)(`div`, {
                        className: `score-calculator-selected-content`,
                        children: [(0, X.jsx)(`div`, {
                            className: `score-calculator-selected-name`,
                            children: u.name
                        }), (0, X.jsxs)(`div`, {
                            className: `score-calculator-selected-meta`,
                            children: [u.releaseYear != null && (0, X.jsx)(`span`, {children: u.releaseYear === 0 ? `Еще не вышла` : u.releaseYear}), (0, X.jsx)(`span`, {children: O ? `HLTB ${O}` : `Нет времени`}), u.reviewScore != null && (0, X.jsxs)(`span`, {children: [`Оценка `, u.reviewScore]})]
                        })]
                    }), (0, X.jsx)(`button`, {
                        type: `button`,
                        className: `score-calculator-selected-clear`,
                        "aria-label": `Сбросить выбранную игру`,
                        onClick: t => {
                            t.stopPropagation(), e(f.Menu_Button_Back_Click), k()
                        },
                        onMouseEnter: () => e(f.Menu_Button_Back_Hover),
                        children: `×`
                    })]
                }) : (0, X.jsx)(`input`, {
                    type: `text`,
                    className: `score-calculator-input`,
                    placeholder: `Введите название игры...`,
                    value: n,
                    onChange: e => {
                        r(e.target.value), m(!0)
                    },
                    onFocus: () => m(!0),
                    onKeyDown: e => Fr(e, t),
                    autoFocus: !0
                }), p && !u && (o || i.length > 0 || n.trim().length > 0 && i.length === 0) && (0, X.jsxs)(`div`, {
                    className: `score-calculator-results-wrap`,
                    children: [o && (0, X.jsx)(`div`, {
                        className: `score-calculator-loading`,
                        children: `Поиск...`
                    }), !o && i.length > 0 && (0, X.jsx)(`div`, {
                        className: `score-calculator-results`,
                        children: i.map(e => {
                            let t = Qd(e.seconds);
                            return (0, X.jsxs)(`div`, {
                                className: `score-calculator-result-item`,
                                onMouseDown: t => {
                                    t.preventDefault(), S(e)
                                },
                                children: [e.imageUrl && (0, X.jsx)(`img`, {
                                    src: e.imageUrl,
                                    alt: e.name,
                                    className: `score-calculator-result-image`
                                }), (0, X.jsxs)(`div`, {
                                    className: `score-calculator-result-content`,
                                    children: [(0, X.jsx)(`div`, {
                                        className: `score-calculator-result-name`,
                                        children: e.name
                                    }), (0, X.jsxs)(`div`, {
                                        className: `score-calculator-result-meta`,
                                        children: [e.releaseYear == null ? `Год неизвестен` : e.releaseYear === 0 ? `Еще не вышла` : `${e.releaseYear}`, t ? (0, X.jsxs)(X.Fragment, {children: [` · HLTB `, t]}) : (0, X.jsx)(X.Fragment, {children: ` · Нет времени`}), e.reviewScore != null && (0, X.jsxs)(X.Fragment, {children: [` · оценка `, e.reviewScore]})]
                                    })]
                                })]
                            }, e.id)
                        })
                    }), !o && n.trim().length > 0 && i.length === 0 && (0, X.jsx)(`div`, {
                        className: `score-calculator-no-results`,
                        children: `Игры не найдены`
                    })]
                })]
            }), u && T && (0, X.jsx)(`div`, {
                className: `game-score-tooltip game-score-tooltip--inline game-score-tooltip--calculator`,
                children: (0, X.jsxs)(`div`, {
                    className: `score-calculator-grid`,
                    children: [(0, X.jsxs)(`div`, {
                        className: `score-calculator-grid-cell score-calculator-grid-cell--summary`,
                        children: [(0, X.jsxs)(`div`, {
                            className: `score-calculator-mode-toggle`,
                            role: `tablist`,
                            "aria-label": `Формула расчёта`,
                            children: [(0, X.jsx)(`button`, {
                                type: `button`,
                                role: `tab`,
                                "aria-selected": _ === `nassal`,
                                className: `score-calculator-mode-toggle-btn${_ === `nassal` ? ` score-calculator-mode-toggle-btn--active` : ``}`,
                                onClick: () => {
                                    _ !== `nassal` && (e(f.Menu_Head_Click), v(`nassal`))
                                },
                                onMouseEnter: () => {
                                    _ !== `nassal` && e(f.Menu_Head_Hover)
                                },
                                children: `NASSAL 2026`
                            }), (0, X.jsx)(`button`, {
                                type: `button`,
                                role: `tab`,
                                "aria-selected": _ === `nasral`,
                                className: `score-calculator-mode-toggle-btn${_ === `nasral` ? ` score-calculator-mode-toggle-btn--active` : ``}`,
                                onClick: () => {
                                    _ !== `nasral` && (e(f.Menu_Head_Click), v(`nasral`))
                                },
                                onMouseEnter: () => {
                                    _ !== `nasral` && e(f.Menu_Head_Hover)
                                },
                                children: `NASRAL 2025`
                            })]
                        }), (0, X.jsx)(Pd, {breakdown: T})]
                    }), (0, X.jsx)(`div`, {
                        className: `score-calculator-grid-cell score-calculator-grid-cell--modifier`,
                        children: (0, X.jsx)(Fd, {breakdown: T})
                    }), (0, X.jsxs)(`div`, {
                        className: `score-calculator-grid-cell score-calculator-grid-cell--time`,
                        children: [(0, X.jsx)(Id, {breakdown: T}), _ === `nassal` && (0, X.jsxs)(`div`, {
                            className: `score-calculator-slider`,
                            children: [(0, X.jsxs)(`div`, {
                                className: `score-calculator-slider-header`,
                                children: [(0, X.jsx)(`span`, {
                                    className: `score-calculator-slider-label`,
                                    children: `Время стримера`
                                }), (0, X.jsx)(`span`, {className: `score-calculator-slider-value`, children: Zd(h)})]
                            }), (0, X.jsxs)(`div`, {
                                className: `score-calculator-slider-track-wrap`,
                                children: [(0, X.jsx)(`input`, {
                                    type: `range`,
                                    className: `score-calculator-slider-input`,
                                    min: Kd,
                                    max: w,
                                    step: Jd,
                                    value: Math.min(h, w),
                                    onChange: e => g(Xd(parseFloat(e.target.value), w)),
                                    style: {"--score-calculator-slider-percent": `${E}%`}
                                }), D !== null && (0, X.jsx)(`div`, {
                                    className: `score-calculator-slider-hltb-mark`,
                                    style: {left: `${D}%`},
                                    title: `HLTB ≈ ${Zd(C)}`
                                })]
                            }), (0, X.jsxs)(`div`, {
                                className: `score-calculator-slider-scale`,
                                children: [(0, X.jsxs)(`span`, {children: [Kd, `ч`]}), (0, X.jsx)(`span`, {children: Zd(w)})]
                            })]
                        })]
                    }), (0, X.jsxs)(`div`, {
                        className: `score-calculator-grid-cell score-calculator-grid-cell--side`,
                        children: [(0, X.jsx)(Ld, {breakdown: T}), (0, X.jsx)(zd, {breakdown: T})]
                    })]
                })
            }), !u && (0, X.jsx)(`div`, {
                className: `score-calculator-placeholder`,
                children: `Найдите игру, чтобы посмотреть расчёт очков по формуле.`
            })]
        })
    }, ef = ({
                 onTimeChange: e,
                 initialHours: t = ``,
                 initialMinutes: n = ``,
                 initialSeconds: r = ``,
                 showLabel: i = !0,
                 focusOnMount: a = !1
             }) => {
        let {playKeyboard: o} = oe(), [s, c] = (0, Y.useState)(t), [l, u] = (0, Y.useState)(n), [d, f] = (0, Y.useState)(r),
            p = (0, Y.useRef)(null), m = (0, Y.useRef)(null), h = (0, Y.useRef)(null),
            g = (e, t, n) => Math.max(t, Math.min(n, e)), _ = e => {
                let t = String(e);
                return t.length === 1 ? `0${t}` : t
            };
        (0, Y.useEffect)(() => {
            e && e(parseInt(s) || 0, parseInt(l) || 0, parseInt(d) || 0)
        }, [s, l, d, e]), (0, Y.useEffect)(() => {
            if (!a) return;
            let e = requestAnimationFrame(() => {
                let e = p.current;
                e && (e.focus(), e.setSelectionRange(0, 0))
            });
            return () => cancelAnimationFrame(e)
        }, [a]);
        let v = (e, t, n) => {
            let r = t.replace(/\D/g, ``).slice(0, 2), i = {hours: s, minutes: l, seconds: d, [e]: r};
            if (e === `hours` ? (c(r), r.length === 2 && !n?.commitNow && setTimeout(() => {
                m.current?.focus(), m.current?.select()
            }, 0)) : e === `minutes` ? (u(r), r.length === 2 && !n?.commitNow && setTimeout(() => {
                h.current?.focus(), h.current?.select()
            }, 0)) : f(r), n?.commitNow) {
                let e = i.hours || ``, t = i.minutes || ``, n = i.seconds || ``;
                if (e !== ``) {
                    let t = parseInt(e, 10);
                    isNaN(t) && (t = 0), t = g(t, 0, 99), e = _(t), c(e)
                }
                if (t !== ``) {
                    let e = parseInt(t, 10);
                    isNaN(e) && (e = 0), e = g(e, 0, 59), t = _(e), u(t)
                }
                if (n !== ``) {
                    let e = parseInt(n, 10);
                    isNaN(e) && (e = 0), e = g(e, 0, 59), n = _(e), f(n)
                }
            }
        }, y = (e, t) => {
            let n = e === `hours` ? s : e === `minutes` ? l : d, r = n === `` ? 0 : parseInt(n, 10);
            isNaN(r) && (r = 0), e === `hours` ? (r = (r + t + 100) % 100, c(_(r))) : (r = (r + t + 60) % 60, e === `minutes` ? u(_(r)) : f(_(r)))
        }, b = (e, t) => {
            if (Fr(e, o), e.key === `ArrowUp`) e.preventDefault(), y(t, 1); else if (e.key === `ArrowDown`) e.preventDefault(), y(t, -1); else if (e.key === `:`) e.preventDefault(), t === `hours` ? (m.current?.focus(), m.current?.select()) : t === `minutes` && (h.current?.focus(), h.current?.select()); else if (e.key === `Enter`) e.preventDefault(), v(t, e.target.value, {commitNow: !0}); else if (e.key === `ArrowRight`) {
                let n = e.target;
                t === `hours` ? (n.selectionStart ?? 0) >= (n.value || ``).length && (e.preventDefault(), m.current?.focus(), m.current?.select()) : t === `minutes` && (n.selectionStart ?? 0) >= (n.value || ``).length && (e.preventDefault(), h.current?.focus(), h.current?.select())
            } else if (e.key === `ArrowLeft`) {
                let n = e.target;
                t === `minutes` ? (n.selectionStart ?? 0) === 0 && (e.preventDefault(), p.current?.focus(), p.current?.select()) : t === `seconds` && (n.selectionStart ?? 0) === 0 && (e.preventDefault(), m.current?.focus(), m.current?.select())
            }
        }, x = e => {
            let t = e.clipboardData.getData(`text`), n = t.match(/(\d{1,2})\D?(\d{2})\D?(\d{2})/);
            if (n) {
                e.preventDefault();
                let t = _(g(parseInt(n[1], 10) || 0, 0, 99)), r = _(g(parseInt(n[2], 10) || 0, 0, 59)),
                    i = _(g(parseInt(n[3], 10) || 0, 0, 59));
                c(t), u(r), f(i), h.current?.focus(), h.current?.select();
                return
            }
            let r = t.match(/(\d{1,2})\D?(\d{2})/);
            if (r) {
                e.preventDefault();
                let t = _(g(parseInt(r[1], 10) || 0, 0, 99)), n = _(g(parseInt(r[2], 10) || 0, 0, 59));
                c(t), u(n), f(``), m.current?.focus(), m.current?.select()
            }
        };
        return (0, X.jsxs)(`div`, {
            className: `time-input-container`, children: [(0, X.jsxs)(`div`, {
                className: `time-input-wrapper`,
                children: [(0, X.jsx)(`input`, {
                    ref: p,
                    type: `text`,
                    inputMode: `numeric`,
                    className: `time-input time-input-hours`,
                    "aria-label": `Часы`,
                    value: s,
                    onChange: e => v(`hours`, e.target.value),
                    onKeyDown: e => b(e, `hours`),
                    onBlur: e => v(`hours`, e.target.value, {commitNow: !0}),
                    onPaste: x,
                    maxLength: 2,
                    placeholder: `00`
                }), (0, X.jsx)(`span`, {className: `time-divider`, children: `:`}), (0, X.jsx)(`input`, {
                    ref: m,
                    type: `text`,
                    inputMode: `numeric`,
                    className: `time-input time-input-minutes`,
                    "aria-label": `Минуты`,
                    value: l,
                    onChange: e => v(`minutes`, e.target.value),
                    onKeyDown: e => b(e, `minutes`),
                    onBlur: e => v(`minutes`, e.target.value, {commitNow: !0}),
                    onPaste: x,
                    maxLength: 2,
                    placeholder: `00`
                }), (0, X.jsx)(`span`, {className: `time-divider`, children: `:`}), (0, X.jsx)(`input`, {
                    ref: h,
                    type: `text`,
                    inputMode: `numeric`,
                    className: `time-input time-input-seconds`,
                    "aria-label": `Секунды`,
                    value: d,
                    onChange: e => v(`seconds`, e.target.value),
                    onKeyDown: e => b(e, `seconds`),
                    onBlur: e => v(`seconds`, e.target.value, {commitNow: !0}),
                    onPaste: x,
                    maxLength: 2,
                    placeholder: `00`
                })]
            }), i && (0, X.jsxs)(`span`, {
                className: `time-label`,
                children: [(0, X.jsx)(`span`, {className: `time-label-arrow`, children: `<`}), ` введите время`]
            })]
        })
    }, tf = 359999;

function nf(e) {
    return Math.min(Math.max(0, Math.floor(e)), tf)
}

var rf = ({
              action: e,
              onClose: t,
              onResetDrafts: n,
              reviewDraft: r = ``,
              rerollReasonDraft: i = ``,
              onReviewDraftChange: a,
              onRerollReasonDraftChange: o,
              gameName: s,
              currentGame: c,
              player: l,
              initialDurationSeconds: u,
              onRequireEffectResolution: d,
              onCheckPendingEffectResolutions: p
          }) => {
        let {
                play: m,
                playKeyboard: h
            } = oe(), {showNotification: g} = Cn(), [_, v] = (0, Y.useState)(0), [y, b] = (0, Y.useState)(null), [x, S] = (0, Y.useState)(() => u == null ? 0 : nf(u)),
            C = (0, Y.useMemo)(() => {
                let e = nf(x), t = e => String(e).padStart(2, `0`);
                return {h: t(Math.floor(e / 3600)), m: t(Math.floor(e % 3600 / 60)), s: t(e % 60)}
            }, [x]), w = (0, Y.useMemo)(() => {
                let {h: e, m: t, s: n} = C;
                return `${e}:${t}:${n}`
            }, [C]), [T, D] = (0, Y.useState)(!1), [O, A] = (0, Y.useState)(!1), [M, ee] = (0, Y.useState)(0),
            te = (c ?? l?.currentGame ?? null)?.data,
            ne = te && `name` in te && !(`id` in te) || l?.currentAuctionResult?.type === `movie`,
            P = s ?? (te && `name` in te ? te.name : null) ?? l?.currentAuctionResult?.title ?? null, re = (e, t, n) => {
                S(e * 3600 + t * 60 + n)
            }, F = async a => {
                if (a.preventDefault(), T) return;
                if (e == Q.COMPLETED && !ne && x < 60) {
                    console.warn(`Attempted to submit completed game result with spentSec < 60, blocking submit. totalSeconds =`, x);
                    return
                }
                if (p != null && await p()) return;
                D(!0);
                let o;
                if (e === Q.COMPLETED) o = {
                    status: `completed`,
                    review: r,
                    rating: _,
                    spentSec: ne ? 3600 : Math.max(60, x)
                }; else if (e === Q.REROLLED) o = {
                    status: `rerolled`,
                    review: i
                }; else if (e === Q.DROPPED) o = {status: `dropped`, review: r, rating: _}; else {
                    D(!1);
                    return
                }
                try {
                    let r = await Ke(o);
                    if (r?.data) {
                        let {ggpReward: e, scoreAdjustments: t} = r.data, n = kl(t);
                        e > 0 ? g([ht(e), ...n], J(`faces/like.webp`)) : e < 0 ? g([He(e), ...n], J(`faces/dislike.webp`)) : n.length > 0 && g(n, J((t ?? []).reduce((e, t) => e + t.amount, 0) >= 0 ? `faces/like.webp` : `faces/dislike.webp`))
                    }
                    switch (Or(), e) {
                        case Q.COMPLETED:
                            m(f.Game_Finished);
                            break;
                        case Q.REROLLED:
                            m(f.Game_Rerolled);
                            break;
                        case Q.DROPPED:
                            m(f.Game_Dropped);
                            break
                    }
                    n?.(), t()
                } catch (e) {
                    console.error(`Failed to finish game:`, e), Te(e) ? (g(vo, J(`faces/angry.webp`), _o, void 0, f.UI_NOT_Error), d?.()) : e instanceof mt && e.description && g(e.description, J(`faces/angry.webp`), `Ошибка`, void 0, f.UI_NOT_Error), D(!1)
                }
            }, ie = () => {
                switch (e) {
                    case Q.COMPLETED:
                        return ne ? `просмотрено` : `пройдено`;
                    case Q.REROLLED:
                        return `реролл`;
                    case Q.DROPPED:
                        return `дропнуто`;
                    default:
                        return ``
                }
            }, I = () => {
                switch (e) {
                    case Q.COMPLETED:
                        return `review-form-status review-form-status--completed`;
                    case Q.DROPPED:
                        return `review-form-status review-form-status--dropped`;
                    case Q.REROLLED:
                        return `review-form-status review-form-status--rerolled`;
                    default:
                        return `review-form-status`
                }
            }, ae = e === Q.COMPLETED && !ne, L = e === Q.REROLLED ? i : r, se = j(), R = N();
        E(!T);
        let z = (0, Y.useRef)(null), ce = e !== Q.REROLLED;
        return k(`A`, () => {
            F({
                preventDefault: () => {
                }
            })
        }, !T && !(e === Q.COMPLETED && !ne && x < 60) && !se), k(`X`, () => {
            z.current?.focus()
        }, !T && !se), k(`DPadRight`, () => {
            m(f.Menu_Button_Back_Click), v(e => e >= 10 ? 1 : e + 1)
        }, ce && !T && !se), (0, X.jsx)(`div`, {
            className: `game-result`, children: (0, X.jsxs)(`form`, {
                className: `review-form`, onSubmit: F, children: [(0, X.jsxs)(`div`, {
                    className: `review-form-header`,
                    children: [(0, X.jsx)(`h2`, {
                        className: `review-form-title`,
                        children: P ? (0, X.jsxs)(X.Fragment, {
                            children: [(0, X.jsx)(`span`, {
                                className: `review-form-game-name`,
                                children: P
                            }), (0, X.jsxs)(`span`, {className: I(), children: [` `, `- `, ie()]})]
                        }) : (0, X.jsx)(`span`, {className: I(), children: ie()})
                    }), ae && (0, X.jsxs)(`div`, {
                        className: `review-form-timer`,
                        children: [(0, X.jsxs)(`span`, {
                            className: `review-form-timer-label`,
                            children: [`время`, (0, X.jsx)(`br`, {}), `таймера`]
                        }), (0, X.jsxs)(`div`, {
                            className: `review-form-timer-frame ${O ? `review-form-timer-frame--editing` : ``}`,
                            children: [(0, X.jsx)(`button`, {
                                type: `button`,
                                className: `review-form-timer-edit-btn`,
                                "aria-label": O ? `Закрыть редактор времени` : `Изменить время`,
                                "aria-expanded": O,
                                disabled: T,
                                onClick: () => {
                                    m(f.Menu_Head_Click), A(e => (e || ee(e => e + 1), !e))
                                },
                                onMouseEnter: () => {
                                    T || m(f.Menu_Head_Hover)
                                },
                                children: (0, X.jsx)(`img`, {
                                    src: J(`ui/edit.svg`),
                                    className: `review-form-timer-icon`,
                                    alt: ``,
                                    "aria-hidden": !0
                                })
                            }), O ? (0, X.jsx)(ef, {
                                onTimeChange: re,
                                showLabel: !1,
                                focusOnMount: !0,
                                initialHours: C.h,
                                initialMinutes: C.m,
                                initialSeconds: C.s
                            }, `timer-editor-${M}`) : (0, X.jsx)(`span`, {
                                className: `review-form-timer-display`,
                                children: w
                            })]
                        })]
                    })]
                }), (0, X.jsxs)(`div`, {
                    className: `review-textarea-wrap`,
                    children: [(0, X.jsx)(`textarea`, {
                        ref: z,
                        className: `review-textarea`,
                        placeholder: e == Q.REROLLED ? `Напишите причину реролла` : `Рецензия`,
                        value: L,
                        onChange: t => {
                            let n = t.target.value;
                            e === Q.REROLLED ? o?.(n) : a?.(n)
                        },
                        onKeyDown: e => Fr(e, h),
                        disabled: T
                    }), !T && L === `` && R.connected && (0, X.jsxs)(`span`, {
                        className: `review-textarea-gp-hint`,
                        "aria-hidden": !0,
                        onClick: () => z.current?.focus(),
                        children: [(0, X.jsx)(q, {button: `X`}), ` начать ввод`]
                    })]
                }), e != Q.REROLLED && (0, X.jsxs)(`div`, {
                    className: `review-form-footer rating-row`,
                    children: [!T && R.connected && (0, X.jsx)(`span`, {
                        className: `rating-gp-hint`,
                        "aria-hidden": !0,
                        children: (0, X.jsx)(q, {button: `DPadRight`})
                    }), (0, X.jsx)(`div`, {
                        className: `rating-cells`,
                        children: Array.from({length: 10}, (e, t) => t + 1).map(e => {
                            let t = e <= _;
                            return (0, X.jsx)(`button`, {
                                type: `button`,
                                className: `rating-cell ${t ? `rating-cell-active` : ``} ${y !== null && e <= y && !t ? `rating-cell-hover` : ``}`,
                                onClick: () => {
                                    m(f.Menu_Button_Back_Click), v(e)
                                },
                                onMouseEnter: () => {
                                    m(f.Menu_Button_Back_Hover), b(e)
                                },
                                onMouseLeave: () => b(null),
                                disabled: T,
                                children: e
                            }, e)
                        })
                    }), (0, X.jsx)(`div`, {
                        className: `rating-section-line rating-row-separator`,
                        "aria-hidden": !0
                    }), (0, X.jsx)(`div`, {
                        className: `finish-button-container`,
                        children: (0, X.jsxs)(`button`, {
                            type: `submit`,
                            className: `finish-button`,
                            disabled: T || e === Q.COMPLETED && !ne && x < 60,
                            title: e === Q.COMPLETED && !ne && x < 60 ? `Введите время` : void 0,
                            onClick: () => {
                                !T && !(e === Q.COMPLETED && !ne && x < 60) && m(f.Menu_Button_Back_Click)
                            },
                            onMouseEnter: () => {
                                !T && !(e === Q.COMPLETED && !ne && x < 60) && m(f.Menu_Button_Back_Hover)
                            },
                            children: [!T && (0, X.jsx)(q, {
                                button: `A`,
                                className: `button-gamepad-hint`
                            }), T ? `Завершаем` : `Завершить`]
                        })
                    })]
                }), e === Q.REROLLED && (0, X.jsx)(`div`, {
                    className: `review-form-footer review-form-footer--solo`,
                    children: (0, X.jsx)(`div`, {
                        className: `finish-button-container`,
                        children: (0, X.jsxs)(`button`, {
                            type: `submit`,
                            className: `finish-button`,
                            disabled: T,
                            onClick: () => {
                                T || m(f.Menu_Button_Back_Click)
                            },
                            onMouseEnter: () => {
                                T || m(f.Menu_Button_Back_Hover)
                            },
                            children: [!T && (0, X.jsx)(q, {
                                button: `A`,
                                className: `button-gamepad-hint`
                            }), T ? `Завершаем` : `Завершить`]
                        })
                    })
                })]
            })
        })
    }, af = `Если вы прошли игру на самом легком уровне сложности используя Анаболики то активируйте их до окончания хода`,
    of = `Анаболики`;

function sf({onClose: e}) {
    let {play: t} = oe(), {onBackHover: r, withBackClick: i} = n(), a = j();
    return E(!0), k(`A`, () => {
        t(f.Menu_Button_Back_Click), e()
    }, !a), k(`B`, () => {
        t(f.Menu_Button_Back_Click), e()
    }, !a), (0, Y.useEffect)(() => {
        let t = t => {
            (t.key === `Escape` || t.key === `Enter`) && (t.preventDefault(), e())
        };
        return window.addEventListener(`keydown`, t), () => window.removeEventListener(`keydown`, t)
    }, [e]), (0, X.jsx)(`div`, {
        className: `anabolics-warning-overlay`, role: `presentation`, onClick: e, children: (0, X.jsxs)(`div`, {
            className: `anabolics-warning-panel`,
            role: `dialog`,
            "aria-modal": `true`,
            "aria-labelledby": `anabolics-warning-title`,
            onClick: e => e.stopPropagation(),
            children: [(0, X.jsxs)(`div`, {
                className: `anabolics-warning-header`,
                children: [(0, X.jsx)(`h3`, {
                    id: `anabolics-warning-title`,
                    className: `anabolics-warning-title`,
                    children: of
                }), (0, X.jsxs)(`button`, {
                    type: `button`,
                    className: `anabolics-warning-back`,
                    onClick: i(e),
                    onMouseEnter: r,
                    "aria-label": `Закрыть`,
                    children: [(0, X.jsx)(`span`, {
                        className: `anabolics-warning-back-arrow`,
                        "aria-hidden": !0
                    }), (0, X.jsx)(`span`, {children: `Назад`}), (0, X.jsx)(q, {
                        button: `B`,
                        className: `anabolics-warning-back-gp-hint`
                    })]
                })]
            }), (0, X.jsx)(`p`, {
                className: `anabolics-warning-text`,
                children: af
            }), (0, X.jsx)(`div`, {
                className: `anabolics-warning-actions`,
                children: (0, X.jsxs)(`button`, {
                    type: `button`,
                    className: `anabolics-warning-btn`,
                    onClick: () => {
                        t(f.Menu_Button_Back_Click), e()
                    },
                    onMouseEnter: () => t(f.Menu_Button_Back_Hover),
                    children: [(0, X.jsx)(q, {button: `A`, className: `anabolics-warning-gp-hint`}), `ОК`]
                })
            })]
        })
    })
}

var cf = `Если вы прошли игру на максимальном уровне сложности (за исключением сложности с одной жизнью на всю игру) используя Один из О’Нилов то активируйте его до окончания хода`,
    lf = `Один из О’Нилов`;

function uf({onClose: e}) {
    let {play: t} = oe(), {onBackHover: r, withBackClick: i} = n(), a = j();
    return E(!0), k(`A`, () => {
        t(f.Menu_Button_Back_Click), e()
    }, !a), k(`B`, () => {
        t(f.Menu_Button_Back_Click), e()
    }, !a), (0, Y.useEffect)(() => {
        let t = t => {
            (t.key === `Escape` || t.key === `Enter`) && (t.preventDefault(), e())
        };
        return window.addEventListener(`keydown`, t), () => window.removeEventListener(`keydown`, t)
    }, [e]), (0, X.jsx)(`div`, {
        className: `one-of-oneils-warning-overlay`, role: `presentation`, onClick: e, children: (0, X.jsxs)(`div`, {
            className: `one-of-oneils-warning-panel`,
            role: `dialog`,
            "aria-modal": `true`,
            "aria-labelledby": `one-of-oneils-warning-title`,
            onClick: e => e.stopPropagation(),
            children: [(0, X.jsxs)(`div`, {
                className: `one-of-oneils-warning-header`,
                children: [(0, X.jsx)(`h3`, {
                    id: `one-of-oneils-warning-title`,
                    className: `one-of-oneils-warning-title`,
                    children: lf
                }), (0, X.jsxs)(`button`, {
                    type: `button`,
                    className: `one-of-oneils-warning-back`,
                    onClick: i(e),
                    onMouseEnter: r,
                    "aria-label": `Закрыть`,
                    children: [(0, X.jsx)(`span`, {
                        className: `one-of-oneils-warning-back-arrow`,
                        "aria-hidden": !0
                    }), (0, X.jsx)(`span`, {children: `Назад`}), (0, X.jsx)(q, {
                        button: `B`,
                        className: `one-of-oneils-warning-back-gp-hint`
                    })]
                })]
            }), (0, X.jsx)(`p`, {
                className: `one-of-oneils-warning-text`,
                children: cf
            }), (0, X.jsx)(`div`, {
                className: `one-of-oneils-warning-actions`,
                children: (0, X.jsxs)(`button`, {
                    type: `button`,
                    className: `one-of-oneils-warning-btn`,
                    onClick: () => {
                        t(f.Menu_Button_Back_Click), e()
                    },
                    onMouseEnter: () => t(f.Menu_Button_Back_Hover),
                    children: [(0, X.jsx)(q, {button: `A`, className: `one-of-oneils-warning-gp-hint`}), `ОК`]
                })
            })]
        })
    })
}

var df = `Если вы производите реролл используя эффект Реролло-фетишист то активируйте его до окончания хода`,
    ff = `Реролло-фетишист`;

function pf({onClose: e}) {
    let {play: t} = oe(), {onBackHover: r, withBackClick: i} = n(), a = j();
    return E(!0), k(`A`, () => {
        t(f.Menu_Button_Back_Click), e()
    }, !a), k(`B`, () => {
        t(f.Menu_Button_Back_Click), e()
    }, !a), (0, Y.useEffect)(() => {
        let t = t => {
            (t.key === `Escape` || t.key === `Enter`) && (t.preventDefault(), e())
        };
        return window.addEventListener(`keydown`, t), () => window.removeEventListener(`keydown`, t)
    }, [e]), (0, X.jsx)(`div`, {
        className: `reroll-fetishist-warning-overlay`, role: `presentation`, onClick: e, children: (0, X.jsxs)(`div`, {
            className: `reroll-fetishist-warning-panel`,
            role: `dialog`,
            "aria-modal": `true`,
            "aria-labelledby": `reroll-fetishist-warning-title`,
            onClick: e => e.stopPropagation(),
            children: [(0, X.jsxs)(`div`, {
                className: `reroll-fetishist-warning-header`,
                children: [(0, X.jsx)(`h3`, {
                    id: `reroll-fetishist-warning-title`,
                    className: `reroll-fetishist-warning-title`,
                    children: ff
                }), (0, X.jsxs)(`button`, {
                    type: `button`,
                    className: `reroll-fetishist-warning-back`,
                    onClick: i(e),
                    onMouseEnter: r,
                    "aria-label": `Закрыть`,
                    children: [(0, X.jsx)(`span`, {
                        className: `reroll-fetishist-warning-back-arrow`,
                        "aria-hidden": !0
                    }), (0, X.jsx)(`span`, {children: `Назад`}), (0, X.jsx)(q, {
                        button: `B`,
                        className: `reroll-fetishist-warning-back-gp-hint`
                    })]
                })]
            }), (0, X.jsx)(`p`, {
                className: `reroll-fetishist-warning-text`,
                children: df
            }), (0, X.jsx)(`div`, {
                className: `reroll-fetishist-warning-actions`,
                children: (0, X.jsxs)(`button`, {
                    type: `button`,
                    className: `reroll-fetishist-warning-btn`,
                    onClick: () => {
                        t(f.Menu_Button_Back_Click), e()
                    },
                    onMouseEnter: () => t(f.Menu_Button_Back_Hover),
                    children: [(0, X.jsx)(q, {button: `A`, className: `reroll-fetishist-warning-gp-hint`}), `ОК`]
                })
            })]
        })
    })
}

function mf(e) {
    return e == null || !(e.effects?.some(e => e.name === `anabolics`) ?? !1) ? !1 : !(e.activeEffects?.some(e => e.name === `anabolics`) ?? !1)
}

function hf(e) {
    return e == null || !(e.effects?.some(e => e.name === `one_of_oneils`) ?? !1) ? !1 : !(e.activeEffects?.some(e => e.name === `one_of_oneils`) ?? !1)
}

function gf(e) {
    return e == null || !(e.effects?.some(e => e.name === `reroll_fetishist`) ?? !1) ? !1 : !(e.activeEffects?.some(e => e.name === `reroll_fetishist`) ?? !1)
}

var _f = e => {
    if (e.status === `idle` || e.status === `casino`) return !1;
    if (Ic(e)) return !0;
    let t = e.currentAuctionResult;
    return t ? t.status === Q.PLAYING || t.status === `content` : !1
}, vf = e => {
    if (!_f(e)) return;
    if (Ic(e)) {
        let t = e.currentGame?.ggpReward;
        return typeof t == `number` ? t : void 0
    }
    let t = e.currentAuctionResult;
    if (!t) return;
    let n = t.currentReward;
    if (typeof n == `number`) return n;
    if (n && typeof n.ggpReward == `number`) return n.ggpReward
}, yf = e => (e.ggp ?? 0) + (vf(e) ?? 0), bf = e => {
    let t = Math.max(0, e.timeInGame ?? 0);
    return t <= 0 ? null : yf(e) / (t / 3600)
}, xf = e => {
    if (e == null) return `—`;
    let t = Math.round(e * 10) / 10;
    return Number.isInteger(t) ? String(t) : t.toFixed(1).replace(`.`, `,`)
}, Sf = e => e > 0 ? `(+${e})` : e < 0 ? `(${e})` : `(0)`, Cf = e => {
    let t = Math.round(e * 10) / 10;
    return Number.isInteger(t) ? String(t) : t.toFixed(1).replace(`.`, `,`)
}, wf = ({player: e}) => {
    let t = (0, Y.useRef)(null), {
        open: n,
        getTriggerProps: r,
        tooltipRootProps: i,
        triggerRef: a
    } = Ed(), [o, s] = (0, Y.useState)(null), c = vf(e), l = Ic(e);
    return (0, Y.useLayoutEffect)(() => {
        if (!n || !t.current) {
            s(null);
            return
        }
        let e = t.current.getBoundingClientRect();
        s({top: e.top, left: e.left})
    }, [n, e.id, c]), c == null ? (0, X.jsx)(X.Fragment, {children: e.ggp}) : (0, X.jsxs)(`span`, {
        ref: e => {
            t.current = e, a.current = e
        },
        className: `statistics-ggp-cell`, ...r(),
        children: [(0, X.jsx)(`span`, {children: e.ggp}), (0, X.jsx)(`span`, {
            className: `statistics-ggp-current`,
            children: Sf(c)
        }), n && o && (0, Xn.createPortal)((0, X.jsx)(`div`, {
            ...i,
            className: `statistics-ggp-tooltip`,
            style: {top: o.top, left: o.left},
            role: `tooltip`,
            children: l ? `Очки за текущий фильм` : `Неполученная награда за текущую игру`
        }), document.body)]
    })
}, Tf = ({onPlayerHistoryClick: e, playersList: t}) => {
    let {play: n} = oe(), {data: r, isLoading: i} = as({enabled: t == null}),
        a = (0, Y.useMemo)(() => t ?? r ?? [], [t, r]),
        o = t == null && i && a.length === 0, [s, c] = (0, Y.useState)(`ggp`), [l, u] = (0, Y.useState)(`desc`), [d, p] = (0, Y.useState)(`balance`),
        m = e => {
            s === e ? u(e => e === `asc` ? `desc` : `asc`) : (c(e), u(e === `name` ? `asc` : `desc`))
        }, h = (e, t) => {
            let n = e[t];
            return typeof n == `number` ? n : 0
        }, g = (0, Y.useMemo)(() => [...a].sort((e, t) => {
            let n, r;
            if (s === `achievements` ? (n = e.achievements?.length ?? 0, r = t.achievements?.length ?? 0) : s === `kpd` ? (n = bf(e) ?? -1 / 0, r = bf(t) ?? -1 / 0) : s === `timeInGame` || s === `ggpNetFromGames` || s === `avgRollsPerTurn` || s === `rollsPositive` || s === `rollsNegative` || s === `rollsNeutral` ? (n = h(e, s), r = h(t, s)) : (n = e[s], r = t[s]), n === r) return 0;
            if (s === `name`) {
                let e = String(n).toLocaleLowerCase(), t = String(r).toLocaleLowerCase();
                if (e === t) return 0;
                let i = e < t ? -1 : 1;
                return l === `asc` ? i : -i
            }
            let i = n < r ? -1 : 1;
            return l === `asc` ? i : -i
        }), [a, s, l]), _ = (0, Y.useMemo)(() => {
            let e = [...a].sort((e, t) => {
                let n = e.ggp ?? 0;
                return (t.ggp ?? 0) - n
            }), t = new Map, n = 1, r = null;
            return e.forEach((e, i) => {
                let a = e.ggp ?? 0;
                r !== null && a !== r && (n = i + 1), t.set(e.id, n), r = a
            }), t
        }, [a]), v = (e, t) => s !== e || e === `ggp` && t && d !== t ? null : (0, X.jsx)(`span`, {
            className: `sort-arrow`,
            children: l === `asc` ? ` ▲` : ` ▼`
        });
    return (0, X.jsxs)(`div`, {
        className: `statistics`,
        children: [(0, X.jsx)(`div`, {
            className: `statistics-table-separator`,
            "aria-hidden": !0
        }), (0, X.jsxs)(`table`, {
            className: `statistics-table`, children: [(0, X.jsx)(`thead`, {
                children: (0, X.jsxs)(`tr`, {
                    children: [(0, X.jsxs)(`th`, {
                        onClick: () => {
                            p(`place`), m(`ggp`)
                        }, children: [`Место `, v(`ggp`, `place`)]
                    }), (0, X.jsxs)(`th`, {
                        onClick: () => m(`name`),
                        children: [`Участник `, v(`name`)]
                    }), (0, X.jsxs)(`th`, {
                        onClick: () => {
                            p(`balance`), m(`ggp`)
                        }, children: [`Очки `, v(`ggp`, `balance`)]
                    }), (0, X.jsxs)(`th`, {
                        onClick: () => m(`ggpNetFromGames`),
                        children: [(0, X.jsx)(`span`, {
                            className: `header-multiline`,
                            children: `Оч.за игры`
                        }), ` `, v(`ggpNetFromGames`)]
                    }), (0, X.jsxs)(`th`, {
                        onClick: () => m(`timeInGame`),
                        children: [(0, X.jsx)(`span`, {
                            className: `header-multiline`,
                            children: `Время в играх`
                        }), ` `, v(`timeInGame`)]
                    }), (0, X.jsxs)(`th`, {
                        onClick: () => m(`kpd`),
                        children: [(0, X.jsx)(`span`, {
                            className: `header-multiline`,
                            children: `КПД (о/ч)`
                        }), ` `, v(`kpd`)]
                    }), (0, X.jsxs)(`th`, {
                        onClick: () => m(`gamesCompleted`),
                        children: [`Пройдено `, v(`gamesCompleted`)]
                    }), (0, X.jsxs)(`th`, {
                        onClick: () => m(`gamesDropped`),
                        children: [`Дропы `, v(`gamesDropped`)]
                    }), (0, X.jsxs)(`th`, {
                        onClick: () => m(`gameStreak`),
                        children: [`Стрик `, v(`gameStreak`)]
                    }), (0, X.jsxs)(`th`, {
                        onClick: () => m(`achievements`),
                        children: [`Ачивки `, v(`achievements`)]
                    }), (0, X.jsxs)(`th`, {
                        onClick: () => m(`avgRollsPerTurn`),
                        children: [(0, X.jsx)(`span`, {
                            className: `header-multiline`,
                            children: `Avg.Колес в ход`
                        }), ` `, v(`avgRollsPerTurn`)]
                    }), (0, X.jsxs)(`th`, {
                        onClick: () => m(`rollsPositive`),
                        children: [`Роллы + `, v(`rollsPositive`)]
                    }), (0, X.jsxs)(`th`, {
                        onClick: () => m(`rollsNegative`),
                        children: [`Роллы − `, v(`rollsNegative`)]
                    }), (0, X.jsxs)(`th`, {
                        onClick: () => m(`rollsNeutral`),
                        children: [`Роллы = `, v(`rollsNeutral`)]
                    })]
                })
            }), (0, X.jsx)(`tbody`, {
                children: o && g.length === 0 ? (0, X.jsx)(`tr`, {
                    children: (0, X.jsx)(`td`, {
                        colSpan: 14,
                        className: `text-center`,
                        children: `Загрузка...`
                    })
                }) : g.length === 0 ? (0, X.jsx)(`tr`, {
                    children: (0, X.jsx)(`td`, {
                        colSpan: 14,
                        className: `text-center`,
                        children: `Нет данных`
                    })
                }) : g.map(t => {
                    let r = h(t, `timeInGame`), i = h(t, `avgRollsPerTurn`), a = h(t, `rollsPositive`),
                        o = h(t, `rollsNegative`), s = h(t, `rollsNeutral`), c = bf(t),
                        l = _f(t) ? `(очки + неполученная награда) / время в играх` : `очки / время в играх`;
                    return (0, X.jsxs)(`tr`, {
                        onClick: () => {
                            n(f.Menu_Button_Participants_Click), e(t)
                        },
                        onMouseEnter: () => n(f.Menu_Button_Participants_Hover),
                        className: `player-row`,
                        children: [(0, X.jsx)(`td`, {children: _.get(t.id) ?? `-`}), (0, X.jsx)(`td`, {children: t.name}), (0, X.jsx)(`td`, {children: (0, X.jsx)(wf, {player: t})}), (0, X.jsx)(`td`, {children: h(t, `ggpNetFromGames`)}), (0, X.jsx)(`td`, {children: it(r)}), (0, X.jsx)(`td`, {
                            title: l,
                            children: xf(c)
                        }), (0, X.jsx)(`td`, {children: t.gamesCompleted}), (0, X.jsx)(`td`, {children: t.gamesDropped}), (0, X.jsx)(`td`, {children: t.gameStreak}), (0, X.jsx)(`td`, {children: t.achievements?.length ?? 0}), (0, X.jsx)(`td`, {children: Cf(i)}), (0, X.jsx)(`td`, {children: a}), (0, X.jsx)(`td`, {children: o}), (0, X.jsx)(`td`, {children: s})]
                    }, t.id)
                })
            })]
        })]
    })
};

function Ef(e) {
    return Gn({
        queryKey: e ? Er.playerActionLog(e) : [`player`, `none`, `actionLog`],
        queryFn: () => pe(e),
        enabled: !!e,
        staleTime: 1 / 0
    })
}

function Df(e) {
    return Gn({
        queryKey: e ? Er.playerGameHistory(e) : [`player`, `none`, `gameHistory`],
        queryFn: () => Ce(e),
        enabled: !!e,
        staleTime: 1 / 0
    })
}

function Of(e) {
    return Gn({
        queryKey: e ? Er.playerPreviousEventGames(e) : [`player`, `none`, `previousEventGames`],
        queryFn: () => R(e),
        enabled: !!e,
        staleTime: 1 / 0
    })
}

var kf = /^([+-]?)(\d+)$/, Af = /^\+(\d+)\s+gnus spin$/i, jf = /^[a-z][a-z0-9_]*\s+received$/i,
    Mf = {regular: `Обычное`, gnus: `Гнусное`, platinum: `Премиум`},
    Nf = {gnus: `Гнусное колесо`, judge: `Судейское колесо`},
    Pf = {ggp: `очки`, platinumChips: `Premium вращения`, gnusAvailable: `Гнусные вращения`}, Ff = {judge: `Судья`},
    If = e => e.filter(e => !!e?.trim()).join(` — `), Lf = (e, t) => t ? e.find(e => e.id === t)?.name ?? null : null,
    Rf = (e, t, n) => n?.trim() ? n : t ? e[t]?.displayName ?? t : `неизвестный эффект`,
    zf = e => e ? Mf[e] ?? e : null, Bf = e => e ? Nf[e] ?? e : null,
    Vf = e => typeof e == `number` ? `${e + 1}-й спин в ходу` : null, Hf = e => {
        let t = e?.removedEffectNames?.filter(e => typeof e == `string` && e.length > 0);
        if (t != null && t.length > 0) return t;
        let n = e?.removedEffectName;
        return typeof n == `string` && n.length > 0 ? [n] : []
    }, Uf = e => {
        let t = kf.exec(e.trim());
        if (t == null) return null;
        let n = t[1], r = Number.parseInt(t[2], 10);
        return Number.isNaN(r) || r === 0 ? `без изменения очков` : n === `-` ? He(-r) : ht(r)
    }, Wf = (e, t) => {
        let n = t?.hookEffectName, r = t?.blockedEffectName;
        return !n || !r ? null : `«${Rf(e, n)}» заблокировал «${Rf(e, r)}»`
    }, Gf = (e, t) => {
        let n = Hf(t);
        if (n.length === 0) return null;
        let r = n.map(t => Rf(e, t)).map(e => `«${e}»`).join(`, `);
        return n.length === 1 ? `потерян эффект ${r}` : `потеряны эффекты ${r}`
    }, Kf = (e, t) => {
        if (!t?.annihilated) return null;
        let n = t.incomingEffectName, r = t.removedEffectName;
        return !n || !r ? null : `«${Rf(e, n)}» аннигилировал «${Rf(e, r)}»`
    }, qf = (e, t, n) => {
        if (!n) return null;
        let r = n.selectedEffectName ?? n.wheel?.selected;
        if (!r) return null;
        let i = Rf(e, r), a = n.target?.selected?.name ?? Lf(t, n.selectedTargetPlayerId ?? n.target?.selected?.playerId);
        return If([`выпал «${i}»`, a ? `цель ${a}` : null, Yf(e, n.subResult)])
    }, Jf = e => {
        let t = Pf[e.field] ?? e.field, n = e.after - e.before;
        if (e.field === `ggp` && n !== 0) {
            let r = n > 0 ? ht(n) : He(n);
            return `${t}: ${e.before} → ${e.after} (${r})`
        }
        return `${t}: ${e.before} → ${e.after}`
    }, Yf = (e, t) => {
        if (!t || typeof t.message != `string`) return null;
        let n = t.message.trim();
        if (!n) return null;
        let r = Wf(e, t.payload);
        if (r) return r;
        let i = Kf(e, t.payload);
        if (i) return i;
        let a = Gf(e, t.payload);
        if (a) return a;
        let o = Uf(n);
        if (o) return o;
        let s = Af.exec(n);
        if (s) {
            let e = Number.parseInt(s[1], 10);
            if (!Number.isNaN(e) && e > 0) return `получено${e === 1 ? `` : `о`} ${e} ${e === 1 ? `гнусное вращение` : `гнусных вращений`}`
        }
        return jf.test(n) || $n(n) ? null : n
    }, Xf = (e, t) => {
        let n = zf(e);
        return If([n ? `${n} вращение` : null, Vf(t)]) || null
    }, Zf = e => {
        switch (e) {
            case`system`:
                return `системой`;
            case`chat-bot`:
                return `чат-ботом`;
            default:
                return `игроком`
        }
    }, Qf = e => {
        switch (e) {
            case`completed`:
                return `пройдена`;
            case`dropped`:
                return `дропнута`;
            case`rerolled`:
                return `реролльнута`;
            case`playing`:
                return `в процессе`
        }
    }, $f = (e, {players: t, effectsByName: n}) => {
        switch (e.actionType) {
            case`timer-start`:
                return `Таймер запущен ${Zf(e.actionData.source)}`;
            case`timer-stop`: {
                let t = Zf(e.actionData.source),
                    n = e.actionData.elapsedSec > 0 ? Je(e.actionData.elapsedSec) ?? Le(e.actionData.elapsedSec) : null;
                return If([`Таймер остановлен ${t}`, n ? `прошло ${n}` : null])
            }
            case`auc-result-start`:
                return If([`${e.actionData.type === `movie` ? `Фильм` : `Игра`} «${e.actionData.name}» начат${e.actionData.type === `movie` ? `` : `а`}`]);
            case`auc-result-finish`: {
                let t = e.actionData.type === `movie` ? `Фильм` : `Игра`, n = e.actionData,
                    r = typeof n.rating == `number` ? `оценка ${n.rating}/10` : null,
                    i = typeof n.ggpReward == `number` ? ht(n.ggpReward) : null,
                    a = typeof n.gameStreak == `number` && n.gameStreak > 0 ? `серия без дропов ${n.gameStreak}` : null,
                    o = typeof n.finalSpentSec == `number` && n.finalSpentSec > 0 ? `время прохождения ${Je(n.finalSpentSec) ?? Le(n.finalSpentSec)}` : null,
                    s = typeof n.scoreAdjustmentTotal == `number` && n.scoreAdjustmentTotal !== 0 ? n.scoreAdjustmentTotal > 0 ? ht(n.scoreAdjustmentTotal) : He(n.scoreAdjustmentTotal) : null,
                    c = typeof n.ggpFastestPlayerReward == `number` && n.ggpFastestPlayerReward > 0 ? `бонус угонщика ${et(n.ggpFastestPlayerReward)}` : null;
                return If([`${t} «${n.name}» завершён${n.type === `movie` ? `` : `а`}`, r, i ? `${i} за прохождение` : null, a, o, s ? `${s} от эффектов` : null, c])
            }
            case`casino-accept`: {
                let t = Rf(n, e.actionData.effectName, e.actionData.effectDisplayName), r = e.actionData.result;
                return If([`Принят эффект «${t}»`, Yf(n, r)])
            }
            case`casino-roll`: {
                let t = Rf(n, e.actionData.effectName, e.actionData.effectDisplayName), r = Xf(e.actionData.kind);
                return If([r ? `${r}: выпал эффект «${t}»` : `В казино выпал эффект «${t}»`])
            }
            case`casino-reroll`: {
                let t = Rf(n, e.actionData.previousEffectName), r = Xf(e.actionData.kind);
                return If([r ? `${r}: зареролен эффект «${t}»` : `В казино зареролен эффект «${t}»`])
            }
            case`effect-receive`: {
                let r = Rf(n, e.actionData.effectName, e.actionData.effectDisplayName),
                    i = Lf(t, e.actionData.fromPlayerId), a = i ? `от ${i}` : null, o = Bf(e.actionData.sourceKind),
                    s = e.actionData.result, c = e.actionData.effectName === `targeted_gnus` ? qf(n, t, s?.payload) : null;
                return If([o ? `${o}` : null, `${o ? `п` : `П`}олучен эффект «${r}»`, a, c ?? Yf(n, s)])
            }
            case`effect-send`: {
                let r = Rf(n, e.actionData.effectName, e.actionData.effectDisplayName),
                    i = Lf(t, e.actionData.toPlayerId) ?? `неизвестному игроку`, a = Bf(e.actionData.sourceKind),
                    o = e.actionData.result;
                return If([a ? `${a}: «${r}»` : `Эффект «${r}»`, `отправлен игроку ${i}`, Yf(n, o)])
            }
            case`effect-activate`:
                return `Эффект «${Rf(n, e.actionData.effectName, e.actionData.effectDisplayName)}» активирован`;
            case`effect-instance-activate`:
                return If([`Эффект «${Rf(n, e.actionData.effectName, e.actionData.effectDisplayName)}» активирован`]);
            case`effect-score-bonus`:
            case`effect-score-penalty`: {
                let r = Rf(n, e.actionData.effectName, e.actionData.effectDisplayName),
                    i = Lf(t, e.actionData.sourcePlayerId);
                return If([`Эффект «${r}» — ${e.actionType === `effect-score-bonus` ? ht(e.actionData.amount) : He(e.actionData.amount)}`, i ? `источник ${i}` : null])
            }
            case`effect-source-bonus`: {
                let r = Rf(n, e.actionData.effectName, e.actionData.effectDisplayName),
                    i = Lf(t, e.actionData.targetPlayerId) ?? `неизвестному игроку`;
                return `Бонус ${et(e.actionData.amount)} от эффекта «${r}» за «${e.actionData.contentTitle}» получен игроком ${i}`
            }
            case`effect-resolution-resolve`: {
                let t = Rf(n, e.actionData.effectName, e.actionData.effectDisplayName), r = e.actionData.result?.type,
                    i = e.actionData.kind;
                return `Эффект «${t}» ${r === `submitted` ? i === `obligation` ? `выполнен` : `отправлен` : `разрешён`}`
            }
            case`staff-action`: {
                let {actor: t, action: n, changes: r} = e.actionData;
                if (n === `player-update` && r != null && r.length > 0) {
                    let e = t?.role ? Ff[t.role] ?? t.role : `Staff`, n = t?.login ? `${e} ${t.login}` : e, i = r.map(Jf);
                    return If([`${n} изменил`, ...i])
                }
                return `Действие судьи`
            }
            case`game-finish`:
                return `Игра «${e.actionData.gameName}» ${Qf(e.actionData.status)}`;
            case`game-start`:
                return `${e.actionData.gameType == `game` ? `Игра` : `Фильм`} «${e.actionData.gameName}» ${e.actionData.gameType == `game` ? `начата` : `начался`}`;
            case`turn-finish`:
                return `Ход ${e.actionData.turn} закончен`;
            case`fastest-player-challenge-lost`:
                return `Титул Главного угонщика проигран игроку ${Lf(t, e.actionData.otherPlayerId) ?? `неизвестному игроку`} в игре «${e.actionData.gameName}» — ${He(-e.actionData.ggpFastestPlayerReward)}`;
            case`fastest-player-challenge-won`:
                return `Титул Главного угонщика выигран у игрока ${Lf(t, e.actionData.otherPlayerId) ?? `неизвестного игрока`} в игре «${e.actionData.gameName}» — ${ht(e.actionData.ggpFastestPlayerReward)}`;
            case`fastest-player-record-defended`: {
                let n = Lf(t, e.actionData.otherPlayerId) ?? `неизвестного игрока`,
                    r = e.actionData.currentFinalSpentSec > 0 ? Je(e.actionData.currentFinalSpentSec) : null,
                    i = e.actionData.previousFinalSpentSec > 0 ? Je(e.actionData.previousFinalSpentSec) : null,
                    a = r && i ? `время ${r} (рекорд ${i})` : r ? `время ${r}` : null;
                return If([`Отстоял титул Главного угонщика против игрока ${n} в игре «${e.actionData.gameName}»`, ht(e.actionData.ggpFastestPlayerReward), a])
            }
            case`wasteland-king-duel-defended`:
                return `Отстоял титул Главного Авторитета против игрока ${Lf(t, e.actionData.opponentPlayerId) ?? `неизвестного игрока`} в игре «${e.actionData.gameName}»`;
            case`wasteland-king-duel-lost`:
                return `Титул Главного Авторитета проигран игроку ${Lf(t, e.actionData.opponentPlayerId) ?? `неизвестного игрока`} в игре «${e.actionData.gameName}» — ${He(-e.actionData.ggpReward)}`;
            case`wasteland-king-duel-won`:
                return `Титул Главного Авторитета выигран у игрока ${Lf(t, e.actionData.opponentPlayerId) ?? `неизвестного игрока`} в игре «${e.actionData.gameName}» — ${ht(e.actionData.ggpReward)}`
        }
    }, ep = e => e > 0 ? `+${e}` : e < 0 ? `${e}` : `0`,
    tp = {nasral: `NASRAL`, gtm: `GTM`, aukus4: `Аукус 4`, igropolius: `Игрополиус`},
    np = e => e && (tp[e.toLowerCase()] ?? e),
    rp = [`января`, `февраля`, `марта`, `апреля`, `мая`, `июня`, `июля`, `августа`, `сентября`, `октября`, `ноября`, `декабря`],
    ip = 40,
    ap = new Set([`factory_assembly`, `anabolics`, `city_map`, `number_9_extra`, `genre_expert`, `one_of_oneils`, `speedometer`, `snatched`]),
    op = e => e.filter(e => ap.has(e)), sp = e => e.length <= ip ? e : `${e.slice(0, ip - 3)}...`,
    cp = ({name: e, linkHref: t, detectOverflow: n = !1, tooltipCoverSrc: r}) => {
        let i = (0, Y.useRef)(null), {play: a} = oe(), o = sp(e), s = n ? e : o,
            c = !n && o !== e, [l, u] = (0, Y.useState)(!1), d = c || l, {
                open: p,
                getTriggerProps: m,
                tooltipRootProps: h,
                triggerRef: g
            } = Ed({disabled: !d}), [_, v] = (0, Y.useState)(null), y = e => {
                a(f.Menu_Button_Back_Click, {panElement: e})
            }, b = e => {
                a(f.Menu_Button_Back_Hover, {panElement: e})
            }, x = e => {
                i.current = e, g.current = e
            };
        (0, Y.useLayoutEffect)(() => {
            if (!n || !i.current) {
                u(!1);
                return
            }
            let e = i.current, t = () => {
                u(e.scrollWidth > e.clientWidth + 1)
            };
            t();
            let r = new ResizeObserver(t);
            return r.observe(e), e.parentElement && r.observe(e.parentElement), () => r.disconnect()
        }, [e, n]), (0, Y.useLayoutEffect)(() => {
            if (!p || !i.current) {
                v(null);
                return
            }
            let e = i.current.getBoundingClientRect();
            v({top: e.top, left: e.left})
        }, [p, e, l]);
        let S = (0, X.jsx)(`span`, {
            ref: x, className: `player-history-game-name-text`, ...m({
                onMouseEnter: e => {
                    t && b(e.currentTarget)
                }, onClick: e => {
                    t && y(e.currentTarget)
                }
            }), children: s
        });
        return (0, X.jsxs)(`span`, {
            className: `player-history-game-name-text-wrap`,
            children: [t ? (0, X.jsx)(`a`, {
                href: t,
                target: `_blank`,
                rel: `noopener noreferrer`,
                className: `player-history-game-name-link`,
                children: S
            }) : S, p && _ && d && (0, Xn.createPortal)((0, X.jsx)(`div`, {
                ...h,
                className: `player-history-game-name-tooltip${r ? ` player-history-game-name-tooltip--with-cover` : ``}`,
                style: {top: _.top, left: _.left},
                role: `tooltip`,
                children: r ? (0, X.jsxs)(X.Fragment, {
                    children: [(0, X.jsx)(`img`, {
                        src: r,
                        alt: ``,
                        className: `player-history-game-name-tooltip-cover`
                    }), (0, X.jsx)(`span`, {className: `player-history-game-name-tooltip-title`, children: e})]
                }) : e
            }), document.body)]
        })
    }, lp = ({effectsHistory: e, effectsByName: t}) => null, up = ({game: e, players: t}) => {
        let n = e.ggpReward ?? 0, r = e.ggpPenalty ?? 0, i = e.scoreAdjustments ?? [], a = e.status === Q.DROPPED;
        return (e.status === Q.COMPLETED || a) && i.length > 0 ? (0, X.jsxs)(`div`, {
            className: `player-history-game-points-breakdown`,
            children: [(0, X.jsxs)(`div`, {
                className: `player-history-game-points-breakdown-item`,
                children: [(0, X.jsx)(`span`, {
                    className: `player-history-game-points-breakdown-value ${a ? `player-history-game-points-breakdown-value--negative` : `player-history-game-points-breakdown-value--positive`}`,
                    children: a ? `-${r}` : `+${n}`
                }), (0, X.jsx)(`span`, {className: `player-history-game-points-breakdown-label`, children: `базово`})]
            }), i.map(e => {
                let n = wl(e);
                return (0, X.jsxs)(`div`, {
                    className: `player-history-game-points-breakdown-item`,
                    children: [(0, X.jsxs)(`span`, {
                        className: `player-history-game-points-breakdown-value ${n >= 0 ? `player-history-game-points-breakdown-value--positive` : `player-history-game-points-breakdown-value--negative`}`,
                        children: [n > 0 ? `+` : ``, n]
                    }), (0, X.jsx)(`span`, {
                        className: `player-history-game-points-breakdown-label`,
                        children: El(e, {
                            getPlayerName: e => {
                                let n = t.find(t => t.id === e);
                                return n?.displayName ?? n?.name
                            }
                        })
                    })]
                }, e.id)
            })]
        }) : null
    }, dp = ({game: e, players: t, statsPlayer: n}) => {
        if (e.status === Q.REROLLED) return null;
        let r = e.ggpReward ?? 0, i = e.ggpPenalty ?? 0, a = (e.scoreAdjustments ?? []).reduce((e, t) => e + t.amount, 0),
            o = e.status === Q.PLAYING, s = e.status === Q.DROPPED, c = e.status === Q.COMPLETED, l = fp(e),
            u = (e.effectsHistory ?? []).includes(`factory_assembly`), d = mp(e, l), f = n.currentAuctionResult,
            p = f?.id === e.id ? f?.currentReward : void 0,
            m = typeof p == `number` ? p : p && typeof p.ggpReward == `number` ? p.ggpReward : void 0,
            h = c ? r + a : s ? -i + a : r,
            g = !o && h < 0 ? ` player-history-game-points--negative` : ` player-history-game-points--positive`, _ = {
                releaseYear: e.releaseYear ?? null,
                hltbHours: pp(e),
                playedHours: l / 3600,
                ratingScore: e.reviewScore ?? null
            }, v = (0, X.jsxs)(`span`, {
                className: `player-history-game-points-label`,
                children: [`очки`, d && (0, X.jsx)(`span`, {
                    className: `player-history-game-points-tooltip-hint`,
                    "aria-hidden": !0,
                    children: `?`
                })]
            });
        return (0, X.jsxs)(`div`, {
            className: `player-history-game-points${g}`,
            children: [d ? (0, X.jsx)(Md, {
                data: _,
                factoryAssembly: u,
                children: v
            }) : v, o ? (0, X.jsxs)(`div`, {
                className: `player-history-game-points-value player-history-game-points-value--playing`,
                children: [m != null && (0, X.jsx)(`span`, {
                    className: `player-history-game-points-current`,
                    children: ep(m)
                }), (0, X.jsxs)(`span`, {className: `player-history-game-points-max`, children: [`до `, r, ` максимум`]})]
            }) : (0, X.jsxs)(X.Fragment, {
                children: [(0, X.jsx)(`div`, {
                    className: `player-history-game-points-value`,
                    children: (0, X.jsx)(`span`, {children: ep(h)})
                }), (0, X.jsx)(up, {game: e, players: t})]
            })]
        })
    }, fp = e => e.finalSpentSec || e.timerAccumulatedSec || 0, pp = e => {
        let t = e.seconds;
        return t != null && t > 0 ? t / 3600 : 0
    }, mp = (e, t) => e.status === Q.COMPLETED && e.type === `game` && t > 0, hp = e => {
        let t = [];
        if (e.type === `game`) {
            let n = e.finalSpentSec || e.timerAccumulatedSec || 0;
            (n > 0 || e.status === Q.PLAYING) && t.push(Le(n)), e.seconds != null && e.seconds > 0 && t.push(`HLTB - ${Le(e.seconds)}`), e.releaseYear != null && t.push(e.releaseYear === 0 ? `Еще не вышла` : String(e.releaseYear))
        }
        return t.join(` | `)
    }, gp = (e, t) => {
        if (!t) return null;
        let n = t.toLowerCase(), r = e.find(e => e.name?.toLowerCase() === n);
        return r?.displayName ?? r?.name ?? t
    }, _p = (e, t) => {
        if (e.fastestPlayerId && t.id) return e.fastestPlayerId === t.id;
        let n = e.fastestPlayerName?.trim().toLowerCase();
        return n ? t.name?.trim().toLowerCase() === n : !1
    }, vp = ({game: e, players: t, statsPlayer: n}) => {
        if (e.status === Q.REROLLED) return (0, X.jsx)(`div`, {
            className: `player-history-game-points player-history-game-points--mobile-compact player-history-game-points--mobile-reroll`,
            children: (0, X.jsx)(`div`, {
                className: `player-history-game-points-mobile-wrap`,
                children: (0, X.jsx)(`div`, {
                    className: `player-history-game-points-value`,
                    children: (0, X.jsx)(`span`, {children: `Реролл`})
                })
            })
        });
        let r = e.ggpReward ?? 0, i = e.ggpPenalty ?? 0, a = (e.scoreAdjustments ?? []).reduce((e, t) => e + t.amount, 0),
            o = e.status === Q.PLAYING, s = e.status === Q.DROPPED, c = e.status === Q.COMPLETED, l = fp(e),
            u = (e.effectsHistory ?? []).includes(`factory_assembly`), d = mp(e, l), f = n.currentAuctionResult,
            p = f?.id === e.id ? f?.currentReward : void 0,
            m = typeof p == `number` ? p : p && typeof p.ggpReward == `number` ? p.ggpReward : void 0,
            h = c ? r + a : s ? -i + a : r, g = o && m != null ? m : h,
            _ = o ? ` player-history-game-points--mobile-playing` : s || h < 0 ? ` player-history-game-points--negative` : ` player-history-game-points--positive`,
            v = (0, X.jsx)(`span`, {
                className: `player-history-game-points-tooltip-hint${d ? `` : ` player-history-game-points-tooltip-hint--spacer`}`,
                "aria-hidden": !0,
                children: d ? `?` : ``
            }), y = (0, X.jsxs)(`div`, {
                className: `player-history-game-points-mobile-stack`,
                children: [(0, X.jsxs)(`div`, {
                    className: `player-history-game-points-value-row`,
                    children: [(0, X.jsx)(`div`, {
                        className: `player-history-game-points-value`,
                        children: (0, X.jsx)(`span`, {children: ep(g)})
                    }), v]
                }), o && (0, X.jsxs)(`div`, {
                    className: `player-history-game-points-max player-history-game-points-max--mobile`,
                    children: [`до `, r]
                }), !o && (0, X.jsx)(up, {game: e, players: t})]
            });
        return (0, X.jsx)(`div`, {
            className: `player-history-game-points player-history-game-points--mobile-compact${_}`,
            children: (0, X.jsx)(`div`, {
                className: `player-history-game-points-mobile-wrap`,
                children: d ? (0, X.jsx)(Md, {
                    data: {
                        releaseYear: e.releaseYear ?? null,
                        hltbHours: pp(e),
                        playedHours: l / 3600,
                        ratingScore: e.reviewScore ?? null
                    },
                    factoryAssembly: u,
                    anchorSelector: `.player-history-game-points-tooltip-hint:not(.player-history-game-points-tooltip-hint--spacer)`,
                    children: y
                }) : y
            })
        })
    }, yp = ({game: e, players: t, statsPlayer: n, effectsByName: r}) => {
        let i = hp(e),
            a = e.imageUrl || e.type !== `game` ? e.type === `game` ? H(e.imageUrl) : J(`icons/movie.webp`) : void 0,
            o = e.fastestFinalSpentSec ?? e.fastestTimerAccumulatedSec ?? 0, s = gp(t, e.fastestPlayerName),
            c = s != null && o > 0, l = _p(e, n);
        return (0, X.jsx)(`div`, {
            className: `player-history-action-top player-history-action-top--mobile-compact`, children: (0, X.jsxs)(`div`, {
                className: `player-history-game-row`,
                children: [(0, X.jsx)(`div`, {
                    className: `player-history-game-row-cover`,
                    children: e.imageUrl || e.type !== `game` ? (0, X.jsx)(bc, {
                        src: e.type == `game` ? H(e.imageUrl) : J(`icons/movie.webp`),
                        alt: e.title,
                        className: `player-history-game-img`,
                        skeletonWidth: `100%`,
                        skeletonHeight: `100%`,
                        skeletonVariant: `rect`
                    }) : (0, X.jsx)(`div`, {className: `player-history-cover-placeholder`})
                }), (0, X.jsxs)(`div`, {
                    className: `player-history-game-row-main`,
                    children: [(0, X.jsxs)(`div`, {
                        className: `player-history-game-row-title`,
                        children: [(0, X.jsx)(`img`, {
                            src: e.type === `game` ? J(`icons/in_game.svg`) : J(`icons/movie.svg`),
                            alt: e.type === `game` ? `Game` : `Movie`,
                            className: `player-history-game-type-icon`
                        }), (0, X.jsx)(cp, {name: e.title, detectOverflow: !0, tooltipCoverSrc: a})]
                    }), i && (0, X.jsx)(`div`, {
                        className: `player-history-game-row-meta`,
                        children: i
                    }), e.type === `game` && (0, X.jsxs)(`div`, {
                        className: `player-history-game-row-best`,
                        children: [(0, X.jsxs)(`span`, {
                            className: `player-history-game-row-best-label`,
                            children: [`Главный угонщик:`, ` `]
                        }), c ? (0, X.jsxs)(X.Fragment, {
                            children: [(0, X.jsx)(`span`, {
                                className: `player-history-game-row-best-name${l ? ` player-history-best-player` : ``}`,
                                children: s
                            }), (0, X.jsxs)(`span`, {
                                className: `player-history-game-row-best-time`,
                                children: [`[`, Le(o), `]`]
                            })]
                        }) : (0, X.jsx)(`span`, {
                            className: `player-history-game-row-best-missing`,
                            children: `Отсутствует`
                        })]
                    }), e.type === `game` && op(e.effectsHistory ?? []).length > 0 && (0, X.jsx)(lp, {
                        effectsHistory: e.effectsHistory ?? [],
                        effectsByName: r
                    })]
                }), (0, X.jsx)(vp, {game: e, players: t, statsPlayer: n})]
            })
        })
    }, bp = ({text: e}) => {
        let t = ft(), {play: n} = oe(), [r, i] = (0, Y.useState)(!1);
        return t ? (0, X.jsx)(`div`, {
            className: `player-history-review-block`,
            children: r ? (0, X.jsxs)(`div`, {
                className: `player-history-review-expanded`,
                children: [(0, X.jsx)(`div`, {
                    className: `player-history-review`,
                    children: e
                }), (0, X.jsx)(`button`, {
                    type: `button`,
                    className: `player-history-review-collapse`,
                    "aria-label": `Свернуть отзыв`,
                    onClick: e => {
                        n(f.Menu_Button_Back_Click, {panElement: e.currentTarget}), i(!1)
                    },
                    children: (0, X.jsx)(`span`, {className: `player-history-review-collapse-chevron`, "aria-hidden": !0})
                })]
            }) : (0, X.jsx)(`button`, {
                type: `button`, className: `player-history-review-toggle`, onClick: e => {
                    n(f.Menu_Button_Back_Click, {panElement: e.currentTarget}), i(!0)
                }, children: `Показать отзыв`
            })
        }) : (0, X.jsx)(`div`, {className: `player-history-review`, children: e})
    }, xp = ({player: e, selfPlayer: t, initialPlace: n = null}) => {
        let {play: r} = oe(), i = ft(), a = _c(), {data: o, isLoading: s} = as(),
            c = (0, Y.useMemo)(() => o ? [...o].sort((e, t) => t.ggp - e.ggp) : [], [o]), {
                data: l,
                isLoading: u
            } = Df(e.id), d = l ?? [], {data: p} = Of(e.id), m = p ?? [], {data: h} = Ef(e.id), g = h ?? [], {data: _} = eu(),
            v = (0, Y.useMemo)(() => _ ? _.reduce((e, t) => (e[t.name] = t, e), {}) : {}, [_]),
            y = (0, Y.useRef)(!1), [b, x] = (0, Y.useState)(`games`),
            S = vi(e.name), [C, w] = (0, Y.useState)(S.length - 1), T = S[C] ?? _i(e.name), E = C > 0,
            D = C < S.length - 1, [O, A] = (0, Y.useState)(``), M = e => {
                r(f.Menu_Button_Back_Click, {panElement: e})
            }, N = e => {
                r(f.Menu_Button_Participants_Hover, {panElement: e})
            }, ee = () => {
                r(f.Menu_Button_Participants_Click)
            }, ne = j(), P = te(), re = !ne && !P;
        k(`DPadLeft`, () => {
            b !== `games` && (r(f.Menu_Button_Back_Click), x(`games`))
        }, b === `effects` && re), k(`DPadRight`, () => {
            b !== `effects` && (r(f.Menu_Button_Back_Click), x(`effects`))
        }, b === `games` && re);
        let F = e, ie = c.find(e => e.id === F.id) ?? F, I = s || u;
        (0, Y.useEffect)(() => {
            if (!t || t.id === e.id || c.length === 0 || y.current) return;
            y.current = !0;
            let n = JSON.parse(localStorage.getItem(`secrets`) || `{}`), r = n._achievementGranted === !0, i = n[e.id] || 0;
            n[e.id] = i + 1, localStorage.setItem(`secrets`, JSON.stringify(n)), !r && c.filter(e => e.id !== t.id).every(e => (n[e.id] || 0) >= 2) && (n._achievementGranted = !0, localStorage.setItem(`secrets`, JSON.stringify(n)), xe({
                playerId: t.id,
                achievementId: `a16`
            }))
        }, [t, e.id, c]);
        let ae = (e => {
                if (c.length === 0) return null;
                let t = 0, n = null, r = 0, i = new Map;
                for (let e of c) r += 1, (n === null || e.ggp !== n) && (t = r, n = e.ggp), i.set(e.id, t);
                return i.get(e) ?? null
            })(F.id) ?? n, L = (e, t) => {
                if (!e) return null;
                let n = new Date(e);
                if (Number.isNaN(n.getTime())) return null;
                let r = t?.includeYear === !0, i = r ? String(n.getDate()) : String(n.getDate()).padStart(2, `0`),
                    a = rp[n.getMonth()], o = String(n.getHours()).padStart(2, `0`),
                    s = String(n.getMinutes()).padStart(2, `0`);
                return r ? `${i} ${a} (${n.getFullYear()}), ${o}:${s}` : `${i} ${a}, ${o}:${s}`
            }, se = (e, t) => {
                if (t === `movie`) switch (e) {
                    case Q.COMPLETED:
                        return `Просмотрено`;
                    case Q.DROPPED:
                        return `Дропнуто`;
                    case Q.REROLLED:
                        return `Реролл`;
                    case Q.PLAYING:
                        return `В процессе`
                }
                switch (e) {
                    case Q.COMPLETED:
                        return `Пройдено`;
                    case Q.DROPPED:
                        return `Дропнуто`;
                    case Q.REROLLED:
                        return `Реролл`;
                    case Q.PLAYING:
                        return `В процессе`
                }
            }, R = e => {
                let t = se(e.status, e.type);
                if (e.status === Q.PLAYING) return {statusText: t, timeText: L(e.timerStartedAt ?? e.createdAt)};
                let n = L(e.createdAt), r = L(e.updatedAt);
                return {statusText: t, timeText: n && r ? `${n} - ${r}` : n ?? r}
            }, z = e => {
                switch (e) {
                    case Q.COMPLETED:
                        return `Пройдено`;
                    case Q.DROPPED:
                        return `Дропнуто`;
                    case Q.REROLLED:
                        return `Реролл`;
                    case Q.PLAYING:
                        return `В процессе`;
                    default:
                        return e
                }
            }, ce = e => $f(e, {players: c, effectsByName: v}), le = O.trim().toLowerCase(),
            ue = le ? d.filter(e => e.title.toLowerCase().includes(le)) : d,
            de = le ? m.filter(e => e.title.toLowerCase().includes(le)) : m,
            fe = le ? g.filter(e => ce(e).toLowerCase().includes(le)) : g, pe = () => {
                if (F == null) return null;
                let e = Math.max(0, F.platinumChips ?? 0);
                if (e === 0) return (0, X.jsx)(`span`, {className: `tokens-zero`, children: `0`});
                let t = [];
                for (let n = 0; n < e; n++) t.push((0, X.jsx)(`div`, {
                    className: `token-icon`,
                    style: {left: `${n * 10}px`, zIndex: 100 - n}
                }, n));
                return t
            }, B = () => F?.socialLinks == null || F.socialLinks.length === 0 ? null : (0, X.jsx)(`div`, {
                className: `streamer-links`,
                children: F.socialLinks.map(({
                                                 platform: e,
                                                 url: t
                                             }, n) => (0, X.jsx)(`a`, {
                    className: `streamer-link${Wu(e, F.stream) ? ` streamer-link--online` : ``}`,
                    href: t,
                    target: `_blank`,
                    rel: `noopener noreferrer`,
                    onMouseEnter: e => N(e.currentTarget),
                    onClick: ee,
                    style: {backgroundImage: `url(${Hu(e)})`}
                }, `${e}-${t}-${n}`))
            }), V = b === `effects` ? fe.length === 0 : ue.length === 0 && de.length === 0, me = (0, X.jsxs)(`div`, {
                className: `player-history-statistics`,
                children: [(0, X.jsxs)(`div`, {
                    className: `player-history-statistics-item`,
                    children: [(0, X.jsx)(`span`, {
                        className: `player-history-statistics-item-label`,
                        children: `Время в играх`
                    }), (0, X.jsx)(`span`, {
                        className: `player-history-statistics-item-value`,
                        children: it(ie.timeInGame ?? 0)
                    })]
                }), (0, X.jsxs)(`div`, {
                    className: `player-history-statistics-item`,
                    children: [(0, X.jsx)(`span`, {
                        className: `player-history-statistics-item-label`,
                        children: `Пройдено`
                    }), (0, X.jsx)(`span`, {className: `player-history-statistics-item-value`, children: F.gamesCompleted})]
                }), (0, X.jsxs)(`div`, {
                    className: `player-history-statistics-item`,
                    children: [(0, X.jsx)(`span`, {
                        className: `player-history-statistics-item-label`,
                        children: `Дропы`
                    }), (0, X.jsx)(`span`, {className: `player-history-statistics-item-value`, children: F.gamesDropped})]
                }), (0, X.jsxs)(`div`, {
                    className: `player-history-statistics-item`,
                    children: [(0, X.jsx)(`span`, {
                        className: `player-history-statistics-item-label`,
                        children: `Стрик`
                    }), (0, X.jsx)(`span`, {className: `player-history-statistics-item-value`, children: F.gameStreak})]
                }), (0, X.jsxs)(`div`, {
                    className: `player-history-statistics-item`,
                    children: [(0, X.jsx)(`span`, {
                        className: `player-history-statistics-item-label`,
                        children: `Ачивки`
                    }), (0, X.jsx)(`span`, {
                        className: `player-history-statistics-item-value`,
                        children: F.achievements.length
                    })]
                }), (0, X.jsxs)(`div`, {
                    className: `player-history-statistics-item`,
                    children: [(0, X.jsx)(`span`, {
                        className: `player-history-statistics-item-label`,
                        children: `Роллы`
                    }), (0, X.jsx)(`img`, {
                        className: `player-history-statistics-item-icon`,
                        src: J(`faces/like.webp`),
                        alt: `like`
                    }), (0, X.jsx)(`span`, {
                        className: `player-history-statistics-item-value`,
                        children: ie.rollsPositive ?? 0
                    })]
                }), (0, X.jsxs)(`div`, {
                    className: `player-history-statistics-item`,
                    children: [(0, X.jsx)(`span`, {
                        className: `player-history-statistics-item-label`,
                        children: `Роллы`
                    }), (0, X.jsx)(`img`, {
                        className: `player-history-statistics-item-icon`,
                        src: J(`faces/dislike.webp`),
                        alt: `dislike`
                    }), (0, X.jsx)(`span`, {
                        className: `player-history-statistics-item-value`,
                        children: ie.rollsNegative ?? 0
                    })]
                }), (0, X.jsxs)(`div`, {
                    className: `player-history-statistics-item`,
                    children: [(0, X.jsx)(`span`, {
                        className: `player-history-statistics-item-label`,
                        children: `Роллы`
                    }), (0, X.jsx)(`img`, {
                        className: `player-history-statistics-item-icon`,
                        src: J(`faces/angry.webp`),
                        alt: `angry`
                    }), (0, X.jsx)(`span`, {className: `player-history-statistics-item-value`, children: ie.rollsNeutral ?? 0})]
                })]
            }), he = (0, X.jsxs)(`div`, {
                className: `player-history-balance${F.ggp === 0 ? ` player-history-balance--zero` : ``}`,
                children: [`Очки: `, F.ggp]
            }), ge = (0, X.jsxs)(`div`, {
                className: `player-history-tokens`,
                children: [`Premium вращения`, (0, X.jsx)(`span`, {className: `tokens`, children: pe()})]
            }), _e = i ? (0, X.jsxs)(X.Fragment, {
                children: [(0, X.jsxs)(`div`, {
                    className: `player-history-header player-history-header--mobile-stack`,
                    children: [he, ge, B()]
                }), me]
            }) : (0, X.jsxs)(X.Fragment, {
                children: [(0, X.jsxs)(`div`, {
                    className: `player-history-header`,
                    children: [he, B()]
                }), ge, me]
            });
        return (0, X.jsxs)(`div`, {
            className: `player-history`, children: [(0, X.jsxs)(`div`, {
                className: `player-history-left`,
                children: [(0, X.jsx)(`div`, {className: `player-history-bg`}), (0, X.jsx)(`img`, {
                    className: `player-history-avatar`,
                    src: T,
                    alt: F.name
                }), S.length > 1 && (0, X.jsxs)(`div`, {
                    className: `player-history-avatar-nav`,
                    children: [(0, X.jsx)(`button`, {
                        className: `player-history-avatar-nav-btn${D ? `` : ` player-history-avatar-nav-btn--disabled`}`,
                        onClick: () => {
                            D && (r(f.Menu_Button_Back_Click), w(C + 1))
                        },
                        "aria-label": `Более новый аватар`,
                        children: `‹`
                    }), (0, X.jsx)(`span`, {
                        className: `player-history-avatar-nav-dots`, children: S.map((e, t) => {
                            let n = S.length - 1 - t;
                            return (0, X.jsx)(`span`, {className: `player-history-avatar-nav-dot${n === C ? ` player-history-avatar-nav-dot--active` : ``}`}, n)
                        })
                    }), (0, X.jsx)(`button`, {
                        className: `player-history-avatar-nav-btn${E ? `` : ` player-history-avatar-nav-btn--disabled`}`,
                        onClick: () => {
                            E && (r(f.Menu_Button_Back_Click), w(C - 1))
                        },
                        "aria-label": `Более старый аватар`,
                        children: `›`
                    })]
                }), (0, X.jsx)(`div`, {
                    className: `player-history-gold-text`,
                    children: ae === null ? `` : `№${ae}`
                }), (0, X.jsxs)(`div`, {
                    className: `player-history-online-status`,
                    children: [(0, X.jsx)(`div`, {className: `${Uu(F.stream) ? `player-history-online` : `player-history-offline`}`}), (0, X.jsx)(`div`, {
                        className: `player-history-online-status-text`,
                        children: Uu(F.stream) ? `online` : `offline`
                    })]
                })]
            }), (0, X.jsxs)(`div`, {
                className: `player-history-right`, children: [_e, (0, X.jsxs)(`div`, {
                    className: `player-history-title`,
                    children: [(0, X.jsx)(`div`, {className: `player-history-search-icon`}), (0, X.jsx)(`input`, {
                        type: `text`,
                        className: `player-history-search-input`,
                        placeholder: `Поиск...`,
                        value: O,
                        onChange: e => A(e.target.value)
                    }), (0, X.jsx)(`div`, {
                        className: `player-history-title-line`,
                        children: `///////////////////////////////////////////////////////////////////////`
                    }), (0, X.jsxs)(`label`, {
                        children: [(0, X.jsx)(q, {
                            button: `DPadLeft`,
                            className: `player-history-tab-gp-hint`
                        }), (0, X.jsx)(`span`, {children: `Игры`}), (0, X.jsx)(`input`, {
                            type: `radio`,
                            name: `history-radio`,
                            value: `games`,
                            className: `player-history-radio-input`,
                            checked: b == `games`,
                            onChange: e => {
                                b !== `games` && (M(e.currentTarget.closest(`label`) ?? e.currentTarget), x(`games`))
                            }
                        }), (0, X.jsx)(`img`, {
                            className: `player-history-radio-icon`,
                            src: sn(a, b == `games`),
                            width: 14,
                            height: 14
                        })]
                    }), (0, X.jsxs)(`label`, {
                        children: [(0, X.jsx)(`span`, {children: `Эффекты`}), (0, X.jsx)(q, {
                            button: `DPadRight`,
                            className: `player-history-tab-gp-hint`
                        }), (0, X.jsx)(`input`, {
                            type: `radio`,
                            name: `history-radio`,
                            value: `effects`,
                            className: `player-history-radio-input`,
                            checked: b == `effects`,
                            onChange: e => {
                                b !== `effects` && (M(e.currentTarget.closest(`label`) ?? e.currentTarget), x(`effects`))
                            }
                        }), (0, X.jsx)(`img`, {
                            className: `player-history-radio-icon`,
                            src: sn(a, b == `effects`),
                            width: 14,
                            height: 14
                        })]
                    })]
                }), (0, X.jsx)(`div`, {
                    className: `player-history-actions`,
                    children: I ? (0, X.jsx)(`div`, {
                        className: `player-history-loading`,
                        children: `Загрузка`
                    }) : V ? (0, X.jsx)(`div`, {
                        className: `player-history-loading player-history-loading--static`,
                        children: `Записи отсутствуют...`
                    }) : (0, X.jsxs)(ao, {
                        gamepadScroll: !0,
                        children: [b == `effects` && fe.map((e, t) => (0, X.jsxs)(`div`, {
                            children: [(0, X.jsxs)(`div`, {
                                className: `player-log-action`,
                                style: {marginTop: t == 0 ? `10px` : `0`},
                                children: [(0, X.jsx)(`div`, {
                                    className: `player-log-game-text`,
                                    children: ce(e)
                                }), (0, X.jsx)(`div`, {
                                    className: `player-log-game-date`,
                                    children: new Date(e.createdAt).toLocaleString(`ru-RU`, {
                                        day: `2-digit`,
                                        month: `2-digit`,
                                        hour: `2-digit`,
                                        minute: `2-digit`,
                                        second: `2-digit`
                                    })
                                })]
                            }), t != fe.length - 1 && (0, X.jsx)(`div`, {
                                className: `player-history-divider`,
                                children: (0, X.jsx)(`div`, {className: `player-history-divider-img`})
                            })]
                        }, e.id)), b == `games` && (0, X.jsxs)(X.Fragment, {
                            children: [ue.map((e, t) => (0, X.jsxs)(`div`, {
                                className: `player-history-action`,
                                children: [i ? (0, X.jsx)(yp, {
                                    game: e,
                                    players: c,
                                    statsPlayer: ie,
                                    effectsByName: v
                                }) : (0, X.jsx)(`div`, {
                                    className: `player-history-action-top`, children: (0, X.jsxs)(`div`, {
                                        className: `player-history-game-content`,
                                        children: [(0, X.jsx)(`div`, {
                                            className: `player-history-game-cover`,
                                            children: e.imageUrl || e.type !== `game` ? (0, X.jsx)(bc, {
                                                src: e.type == `game` ? H(e.imageUrl) : J(`icons/movie.webp`),
                                                alt: e.title,
                                                className: `player-history-game-img`,
                                                skeletonWidth: `100%`,
                                                skeletonHeight: `100%`,
                                                skeletonVariant: `rect`
                                            }) : (0, X.jsx)(`div`, {className: `player-history-cover-placeholder`})
                                        }), (0, X.jsxs)(`div`, {
                                            className: `player-history-game-right`,
                                            children: [(0, X.jsxs)(`div`, {
                                                className: `player-history-game-name`,
                                                children: [(0, X.jsx)(`img`, {
                                                    src: e.type === `game` ? J(`icons/in_game.svg`) : J(`icons/movie.svg`),
                                                    alt: e.type === `game` ? `Game` : `Movie`,
                                                    className: `player-history-game-type-icon`
                                                }), (0, X.jsx)(cp, {
                                                    name: e.title,
                                                    linkHref: e.type === `game` && e.steamAppId > 0 ? `https://store.steampowered.com/app/${e.steamAppId}` : void 0
                                                })]
                                            }), (0, X.jsxs)(`div`, {
                                                className: `player-history-game-body`, children: [(0, X.jsxs)(`div`, {
                                                    className: `player-history-game-info${e.type === `game` ? `` : ` player-history-game-info--compact`}`,
                                                    children: [e.type === `game` && (0, X.jsx)(`div`, {
                                                        className: `player-history-game-data`,
                                                        children: (() => {
                                                            let t = e.finalSpentSec || e.timerAccumulatedSec || 0,
                                                                n = t > 0 ? Le(t) : null, r = n != null,
                                                                i = e.seconds != null && e.seconds > 0 ? Le(e.seconds) : null,
                                                                a = e.releaseYear == null ? null : e.releaseYear === 0 ? `Еще не вышла` : `Год выпуска: ${e.releaseYear}`;
                                                            return (0, X.jsxs)(X.Fragment, {
                                                                children: [r && (0, X.jsx)(`span`, {
                                                                    className: `player-history-game-data-item`,
                                                                    children: n
                                                                }), r && (i != null || a != null) && (0, X.jsxs)(X.Fragment, {children: [`\xA0`, `|`, `\xA0`]}), i != null && (0, X.jsxs)(`span`, {
                                                                    className: `player-history-game-data-item`,
                                                                    children: [`HLTB - `, i]
                                                                }), i != null && a != null && (0, X.jsxs)(X.Fragment, {children: [`\xA0`, `|`, `\xA0`]}), a != null && (0, X.jsx)(`span`, {
                                                                    className: `player-history-game-data-item`,
                                                                    children: a
                                                                })]
                                                            })
                                                        })()
                                                    }), e.type === `game` && (0, X.jsxs)(`div`, {
                                                        className: `player-history-game-best`,
                                                        children: [(0, X.jsx)(`div`, {
                                                            className: `player-history-game-best-top`,
                                                            children: (0, X.jsxs)(`span`, {
                                                                className: `player-history-best-text`,
                                                                children: [`Главный угонщик:`, ` `]
                                                            })
                                                        }), (() => {
                                                            let t = e.fastestFinalSpentSec ?? e.fastestTimerAccumulatedSec;
                                                            return e.fastestPlayerName && t > 0
                                                        })() ? (0, X.jsxs)(`div`, {
                                                            className: `player-history-game-best-bottom`,
                                                            children: [(0, X.jsx)(`span`, {
                                                                className: `player-history-best-player`,
                                                                children: (() => {
                                                                    let t = e.fastestPlayerName.toLowerCase(),
                                                                        n = c.find(e => e.name?.toLowerCase() === t);
                                                                    return n?.displayName ?? n?.name ?? e.fastestPlayerName
                                                                })()
                                                            }), (0, X.jsxs)(`span`, {
                                                                className: `player-history-best-time`,
                                                                children: [`[`, Le(e.fastestFinalSpentSec ?? e.fastestTimerAccumulatedSec), `]`]
                                                            })]
                                                        }) : (0, X.jsx)(`span`, {children: `Отсутствует`})]
                                                    }), (0, X.jsxs)(`div`, {
                                                        className: `player-history-status`,
                                                        children: [e.playerRating != null && e.status !== Q.REROLLED && (0, X.jsxs)(`div`, {
                                                            className: `player-history-score${e.status === Q.DROPPED ? ` player-history-score--dropped` : ``}`,
                                                            children: [e.playerRating, `/10`]
                                                        }), (() => {
                                                            let {statusText: t, timeText: n} = R(e);
                                                            return (0, X.jsxs)(`div`, {
                                                                className: `player-history-status-text`,
                                                                children: [(0, X.jsx)(`span`, {children: t}), n && (0, X.jsx)(`span`, {
                                                                    className: `player-history-status-time`,
                                                                    children: n
                                                                })]
                                                            })
                                                        })(), e.type === `game` && op(e.effectsHistory ?? []).length > 0 && (0, X.jsx)(lp, {
                                                            effectsHistory: e.effectsHistory ?? [],
                                                            effectsByName: v
                                                        })]
                                                    })]
                                                }), (0, X.jsx)(dp, {game: e, players: c, statsPlayer: ie})]
                                            })]
                                        })]
                                    })
                                }), e.playerReview && (0, X.jsx)(bp, {text: e.playerReview}), t != ue.length - 1 && (0, X.jsx)(`div`, {
                                    className: `player-history-divider`,
                                    children: (0, X.jsx)(`div`, {className: `player-history-divider-img`})
                                })]
                            }, e.id)), de.length > 0 && (0, X.jsxs)(`div`, {
                                className: `player-history-previous-events-block`,
                                children: [(0, X.jsx)(`div`, {
                                    className: `player-history-divider`,
                                    children: (0, X.jsx)(`div`, {className: `player-history-divider-img`})
                                }), (0, X.jsx)(`div`, {
                                    className: `player-history-previous-events-title`,
                                    children: `Игры с прошлых ивентов`
                                }), de.map((e, t) => {
                                    let n = H(e.imageUrl);
                                    return (0, X.jsxs)(`div`, {
                                        className: `player-history-action`, children: [(0, X.jsxs)(`div`, {
                                            className: `player-history-action-top`, children: [(0, X.jsxs)(`div`, {
                                                className: `player-history-game-content`,
                                                children: [(0, X.jsx)(`div`, {
                                                    className: `player-history-game-cover`,
                                                    children: n ? (0, X.jsx)(bc, {
                                                        src: n,
                                                        alt: e.title,
                                                        className: `player-history-game-img`,
                                                        skeletonWidth: `100%`,
                                                        skeletonHeight: `100%`,
                                                        skeletonVariant: `rect`
                                                    }) : (0, X.jsx)(`div`, {className: `player-history-cover-placeholder`})
                                                }), (0, X.jsxs)(`div`, {
                                                    className: `player-history-game-right`,
                                                    children: [(0, X.jsxs)(`div`, {
                                                        className: `player-history-game-name`,
                                                        children: [(0, X.jsx)(`img`, {
                                                            src: J(`icons/in_game.svg`),
                                                            alt: `Game`,
                                                            className: `player-history-game-type-icon`
                                                        }), (0, X.jsx)(cp, {name: e.title})]
                                                    }), (0, X.jsxs)(`div`, {
                                                        className: `player-history-game-body`,
                                                        children: [(0, X.jsxs)(`div`, {
                                                            className: `player-history-game-info player-history-game-info--compact`,
                                                            children: [(0, X.jsx)(`div`, {
                                                                className: `player-history-game-data`,
                                                                children: e.spentSec > 0 && (0, X.jsx)(`span`, {
                                                                    className: `player-history-game-data-item`,
                                                                    children: Je(e.spentSec)
                                                                })
                                                            }), (0, X.jsxs)(`div`, {
                                                                className: `player-history-status`,
                                                                children: [e.rating >= 0 && e.status !== Q.REROLLED && (0, X.jsxs)(`div`, {
                                                                    className: `player-history-score${e.status === Q.DROPPED ? ` player-history-score--dropped` : ``}`,
                                                                    children: [e.rating, `/10`]
                                                                }), (0, X.jsxs)(`div`, {
                                                                    className: `player-history-status-text`,
                                                                    children: [(0, X.jsx)(`span`, {children: z(e.status)}), (0, X.jsx)(`span`, {
                                                                        className: `player-history-status-time`,
                                                                        children: L(e.playedAt, {includeYear: !0})
                                                                    })]
                                                                })]
                                                            })]
                                                        }), (0, X.jsxs)(`div`, {
                                                            className: `player-history-game-points player-history-game-points--event`,
                                                            children: [(0, X.jsx)(`span`, {
                                                                className: `player-history-game-points-label`,
                                                                children: `Ивент`
                                                            }), (0, X.jsx)(`div`, {
                                                                className: `player-history-game-points-event`,
                                                                children: np(e.eventName)
                                                            })]
                                                        })]
                                                    })]
                                                })]
                                            }), e.comment && (0, X.jsx)(bp, {text: e.comment})]
                                        }), t != de.length - 1 && (0, X.jsx)(`div`, {
                                            className: `player-history-divider`,
                                            children: (0, X.jsx)(`div`, {className: `player-history-divider-img`})
                                        })]
                                    }, e.id)
                                })]
                            })]
                        })]
                    })
                })]
            })]
        })
    }, Sp = {
        night: J(`backgrounds/night.webm`),
        morning: J(`backgrounds/morning.webm`),
        day: J(`backgrounds/day.webm`),
        evening: J(`backgrounds/evening.webm`)
    }, Cp = {
        night: J(`backgrounds/night.webp`),
        morning: J(`backgrounds/morning.webp`),
        day: J(`backgrounds/day.webp`),
        evening: J(`backgrounds/evening.webp`)
    }, wp = J(`backgrounds/casino.webm`), Tp = J(`backgrounds/casino.webp`);

function Ep(e) {
    return e >= 22 || e < 6 ? `night` : e < 12 ? `morning` : e < 18 ? `day` : `evening`
}

function Dp() {
    return Ep(new Date().getHours())
}

function Op(e) {
    return Sp[e]
}

function kp(e) {
    return Cp[e]
}

var Ap = 6e4;

function jp({
                laptopVisible: e,
                menuAmbientEnabled: t,
                casinoOpen: n,
                casinoInteractionBlocked: i = !1,
                afkSuppressed: a = !1
            }) {
    (0, Y.useEffect)(() => {
        if (!e) return;
        if (a) {
            s();
            return
        }
        let o = Dp(), c = () => {
            !t || n || D(b[o])
        }, l = () => {
            if (r()) {
                if (!t) {
                    O(!1, {playClosedAmbient: !1});
                    return
                }
                n || c(), O(n, {playClosedAmbient: !1, casinoInteractionBlocked: i}), d()
            }
        };
        l();
        let u = window.setInterval(() => {
            let e = Dp();
            e !== o && (o = e, n || c())
        }, Ap), f = () => {
            l()
        }, p = () => {
            document.visibilityState === `visible` && l()
        }, m = e => {
            e.detail === !1 && l()
        };
        return window.addEventListener(I, f), window.addEventListener(h, m), document.addEventListener(`visibilitychange`, p), window.addEventListener(`focus`, p), () => {
            window.clearInterval(u), window.removeEventListener(I, f), window.removeEventListener(h, m), document.removeEventListener(`visibilitychange`, p), window.removeEventListener(`focus`, p)
        }
    }, [e, t, n, i, a])
}

function Mp() {
    let [e, t] = (0, Y.useState)(() => document.hidden);
    return (0, Y.useEffect)(() => {
        let e = () => {
            t(document.hidden)
        };
        return document.addEventListener(`visibilitychange`, e), () => document.removeEventListener(`visibilitychange`, e)
    }, []), e
}

var Np = `settings_screensaver_enabled`, Pp = `screensaverEnabledChanged`, Fp = e => {
    window.dispatchEvent(new CustomEvent(Pp, {detail: e}))
};

function Ip(e = !0) {
    try {
        let t = localStorage.getItem(Np);
        return t == null ? e : t === `1` || t === `true`
    } catch {
        return e
    }
}

function Lp(e) {
    try {
        localStorage.setItem(Np, e ? `1` : `0`)
    } catch {
    }
    return Fp(e), e
}

function Rp(e) {
    return window.addEventListener(Pp, e), () => window.removeEventListener(Pp, e)
}

var zp = ({hide: e, showScreensaverToggle: t = !0}) => {
    let n = (0, Y.useSyncExternalStore)(Rp, Ip, () => !0);
    return (0, X.jsxs)(`div`, {
        className: `site-brand-container ${e ? `site-brand-slide-out` : ``}`,
        children: [(0, X.jsx)(`img`, {
            src: J(`init/nassal.webp`),
            alt: `Nassal`,
            className: `site-brand-image`
        }), t && (0, X.jsxs)(`div`, {
            className: `site-brand-screensaver${n ? ` is-on` : ``}`,
            children: [(0, X.jsx)(`span`, {
                className: `site-brand-screensaver-label`,
                children: `screensaver`
            }), (0, X.jsx)(`button`, {
                type: `button`,
                role: `switch`,
                "aria-checked": n,
                "aria-label": n ? `Отключить скринсейвер` : `Включить скринсейвер`,
                className: `site-brand-screensaver-toggle`,
                onClick: () => Lp(!n),
                children: (0, X.jsx)(`span`, {className: `site-brand-screensaver-toggle-thumb`})
            })]
        })]
    })
};

function Bp() {
    let [e, t] = (0, Y.useState)(() => Date.now()), n = U(e), r = W(e), [i, a] = (0, Y.useState)(!1);
    return (0, Y.useEffect)(() => {
        if (n) return;
        let e = () => {
            let e = Date.now();
            t(e), U(e) && a(!0)
        }, r = W();
        if (r <= 0) {
            let t = window.setTimeout(e, 0);
            return () => window.clearTimeout(t)
        }
        let i = window.setInterval(e, 1e3), o = window.setTimeout(e, r);
        return () => {
            window.clearInterval(i), window.clearTimeout(o)
        }
    }, [n]), {now: e, reached: n, remainingMs: r, countdownJustExpired: i}
}

var Vp = [`января`, `февраля`, `марта`, `апреля`, `мая`, `июня`, `июля`, `августа`, `сентября`, `октября`, `ноября`, `декабря`];

function Hp(e) {
    return `${e.getDate()} ${Vp[e.getMonth()]}`
}

function Up(e) {
    return e.getHours().toString().padStart(2, `0`)
}

function Wp(e) {
    return e.getMinutes().toString().padStart(2, `0`)
}

function Gp(e) {
    let t = e % 10, n = e % 100;
    return n >= 11 && n <= 14 ? `дней` : t === 1 ? `день` : t >= 2 && t <= 4 ? `дня` : `дней`
}

function Kp(e) {
    if (e <= 0) return `событие наступило`;
    let t = Math.floor(e / 1e3), n = Math.floor(t / 86400);
    t %= 86400;
    let r = Math.floor(t / 3600);
    t %= 3600;
    let i = Math.floor(t / 60), a = t % 60, o = [];
    return n > 0 && o.push(`${n} ${Gp(n)}`), o.push(`${r} ч`), o.push(`${i} мин`), o.push(`${a} сек`), o.join(`, `)
}

function qp(e) {
    if (e <= 0) return `0:0:0:0`;
    let t = Math.floor(e / 1e3), n = Math.floor(t / 86400);
    t %= 86400;
    let r = Math.floor(t / 3600);
    return t %= 3600, `${n}:${r}:${Math.floor(t / 60)}:${t % 60}`
}

function Jp() {
    return `nssl_os_v_1.5.6`
}

var Yp = [`twitch`, `kick`], Xp = {twitch: `Twitch`, kick: `Kick`},
    Zp = {twitch: `links/GrayTwitch.png`, kick: `links/GrayKick.png`}, Qp = ({active: e}) => {
        let t = `#5a5a60`, n = `#cfcfd2`, r = e ? {
            y: {fill: `#d4a017`, label: `#fff`},
            x: {fill: `#2273c4`, label: `#fff`},
            b: {fill: `#c8312b`, label: `#fff`},
            a: {fill: `#1e8c3b`, label: `#fff`}
        } : {y: {fill: t, label: n}, x: {fill: t, label: n}, b: {fill: t, label: n}, a: {fill: t, label: n}};
        return (0, X.jsx)(`svg`, {
            viewBox: `0 0 26 26`,
            width: `20`,
            height: `20`,
            "aria-hidden": !0,
            children: (0, X.jsxs)(`g`, {
                fontFamily: `system-ui, -apple-system, Arial, sans-serif`,
                fontWeight: `700`,
                fontSize: `6`,
                textAnchor: `middle`,
                children: [(0, X.jsx)(`circle`, {cx: `13`, cy: `4`, r: `4`, fill: r.y.fill}), (0, X.jsx)(`text`, {
                    x: `13`,
                    y: `6`,
                    fill: r.y.label,
                    children: `Y`
                }), (0, X.jsx)(`circle`, {cx: `4`, cy: `13`, r: `4`, fill: r.x.fill}), (0, X.jsx)(`text`, {
                    x: `4`,
                    y: `15`,
                    fill: r.x.label,
                    children: `X`
                }), (0, X.jsx)(`circle`, {cx: `22`, cy: `13`, r: `4`, fill: r.b.fill}), (0, X.jsx)(`text`, {
                    x: `22`,
                    y: `15`,
                    fill: r.b.label,
                    children: `B`
                }), (0, X.jsx)(`circle`, {cx: `13`, cy: `22`, r: `4`, fill: r.a.fill}), (0, X.jsx)(`text`, {
                    x: `13`,
                    y: `24`,
                    fill: r.a.label,
                    children: `A`
                })]
            })
        })
    }, $p = () => {
        let {play: e} = oe(), {user: t} = ut(), n = ft(), r = T(), i = N(), a = (0, Y.useCallback)(t => {
                e(f.Menu_Button_Participants_Hover, {panElement: t.currentTarget})
            }, [e]), o = (0, Y.useCallback)(() => {
                e(f.Menu_Button_Participants_Click)
            }, [e]), s = !n && (!t || !Pe(t)), {reached: c, remainingMs: l} = Bp(), [u, d] = (0, Y.useState)(() => new Date),
            p = n ? qp(l) : Kp(l);
        (0, Y.useEffect)(() => {
            let e = window.setInterval(() => d(new Date), 1e3);
            return () => window.clearInterval(e)
        }, []);
        let h = e => {
            try {
                localStorage.setItem(K, e)
            } catch {
            }
            window.location.assign(at(e))
        };
        return (0, X.jsxs)(`div`, {
            className: `content-meta`,
            children: [n ? (0, X.jsxs)(X.Fragment, {
                children: [(0, X.jsx)(`span`, {
                    className: `content-meta-version`,
                    children: Jp()
                }), c ? null : (0, X.jsxs)(`span`, {className: `content-meta-countdown`, children: [`Осталось `, p]})]
            }) : (0, X.jsxs)(X.Fragment, {
                children: [(0, X.jsx)(`span`, {
                    className: `content-meta-version-group`,
                    children: (0, X.jsx)(`span`, {className: `content-meta-version`, children: Jp()})
                }), c ? null : (0, X.jsxs)(`span`, {
                    className: `content-meta-countdown`,
                    children: [`Система будет отключена через `, p]
                })]
            }), !n && (0, X.jsxs)(`span`, {
                className: `content-meta-datetime-group`,
                children: [s && (0, X.jsx)(`button`, {
                    type: `button`,
                    className: `content-meta-viewer-btn content-meta-gamepad-toggle${r ? ` is-on` : ``}${r && i.connected ? ` is-connected` : ``}`,
                    onMouseEnter: a,
                    onClick: () => {
                        o(), m(!r)
                    },
                    "aria-label": r ? `Выключить геймпад` : `Включить геймпад`,
                    title: r ? i.connected ? `Геймпад подключён` : `Геймпад включён, ожидание подключения` : `Включить управление геймпадом`,
                    children: (0, X.jsx)(Qp, {active: r})
                }), (0, X.jsxs)(`span`, {
                    className: `content-meta-datetime`,
                    children: [Up(u), (0, X.jsx)(`span`, {
                        className: `content-meta-blink-dot`,
                        children: `:`
                    }), Wp(u), ` \\ `, Hp(u)]
                }), (0, X.jsx)(`span`, {
                    className: `content-meta-viewer-auth`,
                    children: t ? null : Yp.map(e => (0, X.jsx)(`button`, {
                        type: `button`,
                        className: `content-meta-viewer-btn`,
                        onMouseEnter: a,
                        onClick: () => {
                            o(), h(e)
                        },
                        "aria-label": `Войти через ${Xp[e]}`,
                        title: `Войти через ${Xp[e]}`,
                        style: {backgroundImage: `url(${J(Zp[e])})`}
                    }, e))
                })]
            })]
        })
    }, em = 6e4, tm = 2, nm = new Set, rm = new Map;

function im(e) {
    if (!e) return Promise.reject(Error(`Missing image source`));
    if (nm.has(e)) return Promise.resolve(e);
    let t = rm.get(e);
    if (t) return t;
    let n = new Promise((t, n) => {
        let r = new Image;
        r.onload = () => {
            nm.add(e), rm.delete(e), t(e)
        }, r.onerror = () => {
            rm.delete(e), n(Error(`Failed to load image: ${e}`))
        }, r.src = e
    });
    return rm.set(e, n), n
}

function am(e, t) {
    return e.dataset.backgroundSrc === t && e.readyState >= tm ? Promise.resolve() : new Promise((n, r) => {
        let i = !1, a = () => {
            e.removeEventListener(`loadeddata`, s), e.removeEventListener(`canplay`, s), e.removeEventListener(`error`, c)
        }, o = () => {
            i || (i = !0, a(), n())
        };

        function s() {
            o()
        }

        function c() {
            i || (i = !0, a(), r(Error(`Failed to load video: ${t}`)))
        }

        e.addEventListener(`loadeddata`, s), e.addEventListener(`canplay`, s), e.addEventListener(`error`, c), e.dataset.backgroundSrc === t ? e.readyState >= tm && o() : (e.dataset.backgroundSrc = t, e.src = t, e.load())
    })
}

async function om(e, t) {
    await am(e, t);
    try {
        await e.play()
    } catch {
    }
}

async function sm(e, t) {
    if (t != null) {
        await om(e, t);
        return
    }
    try {
        await e.play()
    } catch {
    }
}

function cm(e) {
    e != null && (delete e.dataset.backgroundSrc, e.src && (e.removeAttribute(`src`), e.load()))
}

function lm(e, t, n) {
    e?.pause(), t?.pause(), n?.pause()
}

function um({casinoOpen: e = !1, isAuthenticated: t = !1}) {
    let n = (0, Y.useRef)(null), r = (0, Y.useRef)(null), i = (0, Y.useRef)(null), a = (0, Y.useRef)(Dp()),
        o = (0, Y.useRef)(null), s = (0, Y.useRef)(0), c = (0, Y.useRef)(!1),
        l = (0, Y.useRef)([]), [u, d] = (0, Y.useState)(!0),
        f = Mp(), [p, m] = (0, Y.useState)(() => kp(Dp())), [h, g] = (0, Y.useState)(null), _ = f && h != null ? h : p,
        v = (0, Y.useCallback)(e => {
            if (r.current = e, e != null) {
                if (f) {
                    e.pause(), cm(e);
                    return
                }
                sm(e, Op(a.current))
            }
        }, [f]), y = (0, Y.useCallback)(() => {
            l.current.forEach(e => window.clearTimeout(e)), l.current = []
        }, []), b = (0, Y.useCallback)(async (e, t, n, a) => {
            if (f) return !1;
            let o = r.current, u = i.current;
            if (o == null || u == null || c.current) return !1;
            let p = () => s.current === a;
            y();
            try {
                if (!n) return await Promise.all([im(t), om(o, e)]), p() ? (m(t), d(!0), u.pause(), !0) : !1;
                if (c.current = !0, await Promise.all([im(t), om(u, e)]), !p()) return c.current = !1, u.pause(), !1;
                m(t), d(!1);
                let r = window.setTimeout(() => {
                    if (!p()) {
                        c.current = !1;
                        return
                    }
                    om(o, e).then(() => {
                        p() && (d(!0), u.pause(), c.current = !1)
                    }).catch(() => {
                        p() && (c.current = !1)
                    })
                }, 650);
                return l.current.push(r), !0
            } catch (e) {
                return p() && (c.current = !1, console.warn(`[AppBackgroundVideos] Background load failed`, e)), !1
            }
        }, [y, f]), x = (0, Y.useCallback)((e, t) => {
            if (e === a.current && o.current == null || o.current === e || c.current) return;
            o.current = e;
            let n = s.current + 1;
            s.current = n;
            let r = kp(e), i = Op(e);
            if (f) {
                im(r).then(() => {
                    s.current === n && (m(r), a.current = e)
                }).catch(e => {
                    s.current === n && console.warn(`[AppBackgroundVideos] Background poster load failed`, e)
                }).finally(() => {
                    s.current === n && (o.current = null)
                });
                return
            }
            b(i, r, t, n).then(t => {
                s.current === n && t && (a.current = e)
            }).finally(() => {
                s.current === n && (o.current = null)
            })
        }, [b, f]);
    (0, Y.useEffect)(() => {
        if (e) return;
        let t = window.setInterval(() => {
            if (document.hidden) return;
            let e = Dp();
            x(e, a.current !== null && e !== a.current)
        }, em);
        return () => window.clearInterval(t)
    }, [e, x]), (0, Y.useEffect)(() => {
        if (!f) {
            g(null), x(Dp(), !1);
            return
        }
        g(p), o.current = null, s.current += 1, y(), c.current = !1, d(!0), lm(r.current, i.current, n.current)
    }, [f, x, y]), (0, Y.useEffect)(() => {
        if (!e) {
            n.current?.pause();
            return
        }
        !f && n.current != null && om(n.current, wp)
    }, [e, t, f]), (0, Y.useEffect)(() => {
        if (f || e) return;
        let t = r.current, n = i.current;
        t?.play(), c.current && n != null && n.play()
    }, [f, e]), (0, Y.useEffect)(() => () => {
        y()
    }, [y]);
    let S = f ? `app-background-video-container--use-poster` : ``,
        C = f && _ ? {backgroundImage: `url("${_}")`} : void 0,
        w = e && f && Tp ? {backgroundImage: `url("${Tp}")`} : void 0;
    return (0, X.jsxs)(`div`, {
        id: `app-background-videos`,
        className: [`app-background-video-layer`, e ? `app-background-video-layer--casino-open` : ``].filter(Boolean).join(` `),
        "aria-hidden": !0,
        children: [(0, X.jsxs)(`div`, {
            className: `app-background-video-container ${S}`,
            style: w,
            children: [Tp && e && t ? (0, X.jsx)(`img`, {
                src: Tp,
                alt: ``,
                className: `app-background-poster`,
                "aria-hidden": !0
            }) : null, t && (0, X.jsx)(`video`, {
                ref: n,
                className: `app-background-video app-background-video--active`,
                muted: !0,
                loop: !0,
                playsInline: !0,
                preload: `none`
            })]
        }), (0, X.jsxs)(`div`, {
            className: `app-background-video-container app-background-video-container--time ${S}`,
            style: C,
            children: [_ ? (0, X.jsx)(`img`, {
                src: _,
                alt: ``,
                className: `app-background-poster`,
                "aria-hidden": !0
            }) : null, (0, X.jsx)(`video`, {
                ref: v,
                className: `app-background-video ${u ? `app-background-video--active` : ``}`,
                muted: !0,
                loop: !0,
                playsInline: !0,
                preload: `none`
            }), (0, X.jsx)(`video`, {
                ref: i,
                className: `app-background-video ${u ? `` : `app-background-video--active`}`,
                muted: !0,
                loop: !0,
                playsInline: !0,
                preload: `none`
            })]
        })]
    })
}

var dm = ({title: e, message: t, imageUrl: n, show: r, onHide: i, duration: a = 5e3, sound: o}) => {
    let [s, c] = (0, Y.useState)(!1), u = (0, Y.useRef)(null), d = (0, Y.useRef)(null), f = (0, Y.useRef)(i);
    return (0, Y.useEffect)(() => {
        f.current = i
    }, [i]), (0, Y.useEffect)(() => (r ? (u.current &&= (clearTimeout(u.current), null), d.current &&= (clearTimeout(d.current), null), d.current = window.setTimeout(() => {
        c(!0), o && l(o)
    }, 10), u.current = window.setTimeout(() => {
        c(!1), setTimeout(() => {
            f.current()
        }, 500)
    }, a)) : setTimeout(() => {
        c(!1)
    }, 0), () => {
        u.current &&= (clearTimeout(u.current), null), d.current &&= (clearTimeout(d.current), null)
    }), [r, a, o]), !r && !s ? null : (0, X.jsx)(`div`, {
        className: `notification-container ${s ? `notification-visible` : ``}`,
        children: (0, X.jsxs)(`div`, {
            className: `notification-content`,
            children: [(0, X.jsx)(`img`, {
                src: n,
                alt: `Notification`,
                className: `notification-image`
            }), (0, X.jsxs)(`div`, {
                className: `notification-text`,
                children: [e && (0, X.jsx)(`div`, {
                    className: `notification-title`,
                    children: e
                }), Array.isArray(t) ? t.map(e => (0, X.jsx)(`div`, {
                    className: `notification-message`,
                    children: e
                }, e)) : (0, X.jsx)(`div`, {className: `notification-message`, children: t})]
            })]
        })
    })
}, fm = 1e4;

function pm() {
    let {isAuthenticated: e} = ut(), t = Fe();
    (0, Y.useEffect)(() => {
        if (U()) return;
        if (!e) {
            let e = window.setInterval(() => {
                t.invalidateQueries({queryKey: Er.playerList}), t.invalidateQueries({queryKey: Er.effectList}), t.invalidateQueries({queryKey: Er.achievements})
            }, fm);
            return () => {
                window.clearInterval(e)
            }
        }
        let n = () => {
            kr(t), Or(void 0, t)
        }, r = $e.onDataRefresh(() => {
            Or(void 0, t), t.invalidateQueries({queryKey: Er.effectList}), kr(t)
        }), i = $e.onAchievementReceived(() => {
            n()
        }), a = $e.onPlayerUpdated(e => {
            Or(e.id, t)
        });
        return () => {
            r(), i(), a()
        }
    }, [e, t])
}

var mm = /\/achievements\/image\/([^/?#]+)$/i;

function hm(e) {
    return e.replace(/^\/+/, ``).trim()
}

function gm(e) {
    if (e.iconFileName) return hm(e.iconFileName);
    for (let t of [e.iconUrl, e.pipBoyUrl]) {
        if (!t) continue;
        let e = t.match(mm);
        if (e?.[1]) return hm(e[1]);
        let n = t.lastIndexOf(`/`);
        if (n >= 0) {
            let e = t.slice(n + 1);
            if (e) return hm(e)
        }
        if (/\.webp$/i.test(t)) return hm(t)
    }
    return ``
}

function _m(e) {
    let t = gm(e);
    return t ? `achievements/${t}` : ``
}

function vm(e) {
    let t = _m(e);
    return t ? J(t) : ``
}

var ym = function (e) {
    return e.CENTER = `center`, e.TOP = `top`, e.HIDDEN = `hidden`, e
}({});

function bm() {
    let [e, t] = (0, Y.useState)(window.innerHeight > window.innerWidth);
    return (0, Y.useEffect)(() => {
        let e = () => {
            t(window.innerHeight > window.innerWidth)
        };
        return window.addEventListener(`resize`, e), () => window.removeEventListener(`resize`, e)
    }, []), e
}

function xm() {
    return (0, Y.useSyncExternalStore)(ct, nt, () => !1)
}

var Sm = 72, Cm = .6, wm = .42, Tm = 2, Em = .82, Dm = 1.55;

function Om() {
    return Math.max(window.innerWidth * Em, Sm * 2.4)
}

function km(e) {
    return Math.min(Math.max(e / Om(), 0), 1) ** +Dm
}

function Am(e) {
    let t = e;
    for (; t instanceof HTMLElement;) {
        let {overflowX: e} = getComputedStyle(t);
        if ((e === `auto` || e === `scroll` || e === `overlay`) && t.scrollWidth > t.clientWidth + Tm) return t;
        t = t.parentElement
    }
    return null
}

function jm(e) {
    let t = Am(e instanceof Element ? e : null);
    return t != null && t.scrollLeft > Tm
}

function Mm(e) {
    return e instanceof Element ? !!e.closest(`input, textarea, select, button, a, label, [role="slider"], [role="listbox"], .title-player-picker-menu, .ScrollbarsCustom-ThumbX, .ScrollbarsCustom-TrackX`) : !1
}

function Nm(e, t) {
    return e >= Sm && Math.abs(t) <= Math.max(e * Cm, 24)
}

function Pm(e, t, n) {
    let r = n?.interactive ?? !1,
        i = (0, Y.useRef)(t), [a, o] = (0, Y.useState)(0), [s, c] = (0, Y.useState)(!1), [l, u] = (0, Y.useState)(!1),
        d = (0, Y.useRef)(!1);
    (0, Y.useEffect)(() => {
        i.current = t
    }, [t]);
    let f = (0, Y.useCallback)(() => {
        d.current = !1, u(!0), c(!0), i.current({fromSwipe: !0}), o(0), requestAnimationFrame(() => {
            c(!1), u(!1)
        })
    }, []);
    (0, Y.useEffect)(() => {
        if (!e) return;
        let t = 0, n = 0, a = !1, s = null, l = e => {
            if (e.touches.length !== 1 || d.current || Mm(e.target) || jm(e.target)) return;
            let i = e.touches[0];
            t = i.clientX, n = i.clientY, a = !0, s = Am(e.target instanceof Element ? e.target : null), r && c(!0)
        }, u = () => {
            a = !1, s = null, r && (c(!1), o(0))
        }, p = e => {
            if (!a || e.touches.length !== 1) return;
            if (s && s.scrollLeft > Tm) {
                u();
                return
            }
            let i = e.touches[0], c = i.clientX - t, l = i.clientY - n;
            if (c < 0 || Math.abs(l) > Math.max(Math.abs(c) * Cm, 24)) {
                u();
                return
            }
            r && o(km(c))
        }, m = (e, t) => {
            if (!a) return;
            if (a = !1, s = null, !Nm(e, t)) {
                r && (c(!1), o(0));
                return
            }
            if (!r) {
                i.current();
                return
            }
            let n = km(e), l = n >= wm || e >= Sm * 1.75;
            if (c(!1), l) {
                if (n >= .999) {
                    f();
                    return
                }
                d.current = !0, o(1);
                return
            }
            o(0)
        }, h = e => {
            let i = e.changedTouches[0];
            if (!i) {
                r && c(!1), a = !1;
                return
            }
            m(i.clientX - t, i.clientY - n)
        };
        return document.addEventListener(`touchstart`, l, {passive: !0}), document.addEventListener(`touchmove`, p, {passive: !0}), document.addEventListener(`touchend`, h, {passive: !0}), document.addEventListener(`touchcancel`, h, {passive: !0}), () => {
            document.removeEventListener(`touchstart`, l), document.removeEventListener(`touchmove`, p), document.removeEventListener(`touchend`, h), document.removeEventListener(`touchcancel`, h)
        }
    }, [e, r, f]);
    let p = (0, Y.useCallback)(e => {
        e.propertyName !== `transform` || !d.current || e.currentTarget === e.target && f()
    }, [f]);
    return {
        progress: e ? a : 0,
        isDragging: s,
        isSwipeFinalizing: l,
        interactive: r,
        foregroundStyle: r ? {"--mobile-swipe-progress": String(e ? a : 0)} : void 0,
        onForegroundTransitionEnd: p
    }
}

var Fm = {
    hero: {title: `Идейный вдохновитель`, name: `HoneyMad`, imageUrl: J(`avatars/MAD.webp`)},
    team: [{role: `Team Lead`, names: `Virtuoz`}, {role: `Frontend`, names: `SQshka / Sm1T`}, {
        role: `Backend`,
        names: `madf12`
    }, {role: `WebGL`, names: `swathingsoap`}, {role: `Аудио`, names: `Zeyders`}, {
        role: `Моделинг, Анимация`,
        names: `Xpanec`
    }, {role: `Art, Design, UI`, names: `IlyaSnow_`}],
    organization: {title: `Организация и судейство`, name: `Mistafaker`, imageUrl: J(`credits/FAK.webp`)},
    sponsor: {imageUrl: J(`credits/sponsor.webp`)},
    teamSeparator: {imageUrl: J(`credits/hpgTeam.webp`), backgroundImageUrl: J(`credits/gradient.svg`)}
}, Im = () => (0, X.jsx)(`div`, {
    className: `credits-page`, children: (0, X.jsx)(ao, {
        gamepadScroll: !0, children: (0, X.jsxs)(`div`, {
            className: `credits-page-scroll`,
            children: [(0, X.jsxs)(`div`, {
                className: `credits-hero`,
                children: [(0, X.jsx)(`img`, {
                    className: `credits-hero-sponsor`,
                    src: Fm.sponsor.imageUrl,
                    alt: `Спонсор`
                }), (0, X.jsx)(`img`, {
                    className: `credits-hero-image`,
                    src: Fm.hero.imageUrl,
                    alt: Fm.hero.name
                }), (0, X.jsx)(`img`, {
                    className: `credits-hero-dollar`,
                    src: J(`credits/dollar.png`),
                    alt: ``,
                    "aria-hidden": !0
                }), (0, X.jsx)(`div`, {
                    className: `credits-hero-right`,
                    children: (0, X.jsxs)(`div`, {
                        className: `credits-hero-text`,
                        children: [(0, X.jsx)(`div`, {
                            className: `credits-hero-title`,
                            children: Fm.hero.title
                        }), (0, X.jsx)(`div`, {className: `credits-hero-name`, children: Fm.hero.name})]
                    })
                })]
            }), (0, X.jsxs)(`div`, {
                className: `credits-separator-band`,
                children: [(0, X.jsx)(`img`, {
                    className: `credits-separator-bg`,
                    src: Fm.teamSeparator.backgroundImageUrl,
                    alt: ``,
                    "aria-hidden": !0
                }), (0, X.jsxs)(`div`, {
                    className: `credits-separator`,
                    children: [(0, X.jsx)(`span`, {
                        className: `credits-separator-line`,
                        "aria-hidden": !0,
                        children: `//////////////////////////////////////////////////////////////`
                    }), (0, X.jsx)(`img`, {
                        className: `credits-separator-logo`,
                        src: Fm.teamSeparator.imageUrl,
                        alt: `HPG Team`
                    }), (0, X.jsx)(`span`, {
                        className: `credits-separator-line`,
                        "aria-hidden": !0,
                        children: `//////////////////////////////////////////////////////////////`
                    })]
                })]
            }), (0, X.jsxs)(`div`, {
                className: `credits-team`,
                children: [(0, X.jsx)(`div`, {
                    className: `credits-team-jobs`,
                    children: Fm.team.map(e => (0, X.jsxs)(`div`, {
                        className: `credits-team-row`,
                        children: [(0, X.jsx)(`div`, {
                            className: `credits-team-role`,
                            children: e.role
                        }), (0, X.jsx)(`div`, {className: `credits-team-name`, children: e.names})]
                    }, e.role))
                }), (0, X.jsxs)(`div`, {
                    className: `credits-team-org`,
                    children: [(0, X.jsx)(`img`, {
                        className: `credits-team-org-image`,
                        src: Fm.organization.imageUrl,
                        alt: Fm.organization.name
                    }), (0, X.jsx)(`div`, {
                        className: `credits-team-org-title`,
                        children: Fm.organization.title
                    }), (0, X.jsx)(`div`, {className: `credits-team-org-name`, children: Fm.organization.name})]
                })]
            })]
        })
    })
});

function Lm(e) {
    return Gn({queryKey: Er.publicPlayerStats, queryFn: Ye, enabled: e?.enabled ?? !0})
}

var Rm = e => e.trim().toLowerCase();

function zm(e) {
    return e ?? []
}

function Bm(e, t) {
    let n = e.find(e => e.id === t.playerId);
    if (n) return n;
    let r = new Set([Rm(t.name)].filter(Boolean));
    return e.find(e => [e.name, e.displayName, e.twitchId].map(e => e == null ? `` : Rm(e)).filter(Boolean).some(e => r.has(e))) ?? null
}

function Vm(e, t) {
    if (!t?.length || !e.length) return null;
    let n = null;
    for (let r of t) {
        let t = r.fromLibertyToLosSantos?.averageScore;
        if (t == null || !Number.isFinite(t)) continue;
        let i = Bm(e, r);
        i && (!n || t > n.averageScore) && (n = {player: i, score: Math.floor(t), averageScore: t})
    }
    return n ? {player: n.player, score: n.score} : null
}

function Hm(e, t) {
    if (!t?.length || !e.length) return null;
    let n = null;
    for (let r of t) {
        let t = r.bestVoiceTargetCount ?? 0;
        if (t <= 0) continue;
        let i = Bm(e, r);
        i && (!n || t > n.score) && (n = {player: i, score: t})
    }
    return n
}

function Um(e, t) {
    if (!t?.length || !e.length) return null;
    let n = null;
    for (let r of t) {
        let t = r.beachEpisode?.averageScore;
        if (t == null || !Number.isFinite(t)) continue;
        let i = Bm(e, r);
        i && (!n || t > n.averageScore) && (n = {player: i, score: Math.floor(t), averageScore: t})
    }
    return n ? {player: n.player, score: n.score} : null
}

function Wm(e, t) {
    if (!t?.length || !e.length) return null;
    let n = null;
    for (let r of t) {
        let t = r.ggpLostTotal ?? 0;
        if (t <= 0) continue;
        let i = Bm(e, r);
        i && (!n || t > n.score) && (n = {player: i, score: t})
    }
    return n
}

function Gm(e, t) {
    if (!t?.length || !e.length) return null;
    let n = null;
    for (let r of t) {
        let t = r.mainThief?.totalScore ?? 0;
        if (t <= 0) continue;
        let i = Bm(e, r);
        i && (!n || t > n.score) && (n = {player: i, score: t})
    }
    return n
}

function Km(e, t) {
    if (!t?.length || !e.length) return null;
    let n = null;
    for (let r of t) {
        let t = r.helicopterMaxScore ?? 0;
        if (t <= 0) continue;
        let i = Bm(e, r);
        i && (!n || t > n.score) && (n = {player: i, score: t})
    }
    return n
}

var qm = e => e.length === 0 ? null : e.reduce((e, t) => (t.rollsPositive ?? 0) > (e.rollsPositive ?? 0) ? t : e),
    Jm = e => e.length === 0 ? null : e.reduce((e, t) => (t.rollsGnus ?? 0) > (e.rollsGnus ?? 0) ? t : e),
    Ym = e => e.length === 0 ? null : e.reduce((e, t) => (t.gamesDropped ?? 0) > (e.gamesDropped ?? 0) ? t : e),
    Xm = e => {
        if (e.length === 0) return null;
        let t = null;
        for (let n of e) {
            let e = Math.max(0, n.timeInGame ?? 0);
            (!t || e > t.seconds) && (t = {player: n, seconds: e})
        }
        return !t || t.seconds <= 0 ? null : {player: t.player, hours: Math.floor(t.seconds / 3600)}
    }, Zm = e => e.length === 0 ? null : e.reduce((e, t) => (t.rollsTotal ?? 0) > (e.rollsTotal ?? 0) ? t : e),
    Qm = e => {
        if (e.length === 0) return null;
        let t = null;
        for (let n of e) {
            let e = bf(n);
            e != null && (!t || e > t.kpd) && (t = {player: n, kpd: e})
        }
        return t
    }, $m = e => vi(e)[0] ?? _i(e), eh = ({
                                              player: e,
                                              categoryTitle: t,
                                              statLabelTop: n,
                                              statLabelBottom: r,
                                              value: i
                                          }) => (0, X.jsx)(`div`, {
        className: `results-category`,
        children: (0, X.jsxs)(`div`, {
            className: `results-category-player`,
            children: [(0, X.jsx)(`div`, {
                className: `results-category-avatar-wrap`,
                children: (0, X.jsx)(`img`, {className: `results-category-avatar`, src: $m(e.name), alt: e.name})
            }), (0, X.jsxs)(`div`, {
                className: `results-podium-info results-category-info`,
                children: [(0, X.jsx)(`div`, {
                    className: `results-podium-place`,
                    children: t
                }), (0, X.jsx)(`div`, {
                    className: `results-podium-name`,
                    children: e.name
                }), (0, X.jsxs)(`div`, {
                    className: `results-category-stat-row`,
                    children: [(0, X.jsx)(`div`, {
                        className: `results-podium-score`,
                        children: i
                    }), (0, X.jsxs)(`div`, {
                        className: `results-category-stat-label`,
                        children: [n ? (0, X.jsx)(`span`, {children: n}) : null, (0, X.jsx)(`span`, {children: r})]
                    })]
                })]
            })]
        })
    }), th = {
        Melharucos: [`final/Mel_gold.webp`, `final/Mel_silver.webp`, `final/Mel_bronze.webp`],
        F1ashko: [`final/Flash_gold.webp`, `final/Flash_silver.webp`, `final/Flash_bronze.webp`],
        KarmikKoala: [`final/Karm_gold.webp`, `final/Karm_silver.webp`, `final/Karm_bronze.webp`],
        Dunduk: [`final/Dund_gold.webp`, `final/Dund_silver.webp`, `final/Dund_bronze.webp`]
    }, nh = {1: J(`icons/molnia-gold.svg`), 2: J(`icons/molnia-silver.svg`), 3: J(`icons/molnia-bronze.svg`)},
    rh = [{place: 1, className: `results-podium-player--first`}, {
        place: 2,
        className: `results-podium-player--second`
    }, {place: 3, className: `results-podium-player--third`}], ih = (e, t) => {
        let n = t - 1, r = Object.keys(th).find(t => t.toLowerCase() === e.toLowerCase());
        return r ? J(th[r][n]) : _i(e)
    }, ah = ({players: e}) => {
        let t = (0, Y.useRef)(null), [n, r] = (0, Y.useState)(0), i = (0, Y.useRef)(!1), a = e.slice(0, 3), o = qm(e),
            s = Jm(e), c = Ym(e), l = Xm(e), u = Zm(e), d = Qm(e), {data: f} = Lm(), p = zm(f?.data.array), m = Km(e, p),
            h = Vm(e, p), g = Um(e, p), _ = Hm(e, p), v = Wm(e, p), y = Gm(e, p);
        return (0, Y.useEffect)(() => {
            i.current = !1
        }, []), (0, Y.useEffect)(() => {
            let e = t.current;
            if (!e || i.current) return;
            e.scrollTop = 0;
            let n = requestAnimationFrame(() => {
                let e = t.current;
                e && !i.current && (e.scrollTop = 0)
            });
            return () => cancelAnimationFrame(n)
        }, [n, f]), (0, Y.useEffect)(() => {
            let e = t.current;
            if (!e) return;
            let n = () => {
                e.scrollTop > 0 && (i.current = !0)
            };
            return e.addEventListener(`scroll`, n, {passive: !0}), () => e.removeEventListener(`scroll`, n)
        }, [n]), (0, X.jsx)(`div`, {
            className: `results-page`, children: (0, X.jsx)(ao, {
                gamepadScroll: !0, scrollerRef: e => {
                    t.current !== e && (t.current = e, e && r(e => e + 1))
                }, children: (0, X.jsxs)(`div`, {
                    className: `results-page-scroll`,
                    children: [(0, X.jsx)(`div`, {
                        className: `results-podium`,
                        children: rh.map(({place: e, className: t}) => {
                            let n = a[e - 1];
                            return n ? (0, X.jsxs)(`div`, {
                                className: `results-podium-player ${t}`,
                                children: [(0, X.jsx)(`img`, {
                                    className: `results-podium-avatar`,
                                    src: ih(n.name, e),
                                    alt: n.name
                                }), (0, X.jsxs)(`div`, {
                                    className: `results-podium-info`,
                                    children: [(0, X.jsxs)(`div`, {
                                        className: `results-podium-place`,
                                        children: [e, ` место`]
                                    }), (0, X.jsxs)(`div`, {
                                        className: `results-podium-name-row`,
                                        children: [(0, X.jsx)(`img`, {
                                            className: `results-podium-molnia`,
                                            src: nh[e],
                                            alt: ``,
                                            "aria-hidden": !0
                                        }), (0, X.jsx)(`div`, {
                                            className: `results-podium-name`,
                                            children: n.name
                                        }), (0, X.jsx)(`img`, {
                                            className: `results-podium-molnia results-podium-molnia--mirrored`,
                                            src: nh[e],
                                            alt: ``,
                                            "aria-hidden": !0
                                        })]
                                    }), (0, X.jsx)(`div`, {className: `results-podium-score`, children: n.ggp})]
                                })]
                            }, n.id) : null
                        })
                    }), (0, X.jsx)(`div`, {
                        className: `results-divider`,
                        children: `///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////`
                    }), (0, X.jsxs)(`div`, {
                        className: `results-container`,
                        children: [o != null && o.rollsPositive != null ? (0, X.jsx)(eh, {
                            player: o,
                            categoryTitle: `Самый везучий`,
                            statLabelTop: `позитивных`,
                            statLabelBottom: `роллов`,
                            value: o.rollsPositive
                        }) : null, s != null && s.rollsGnus != null ? (0, X.jsx)(eh, {
                            player: s,
                            categoryTitle: `Самый гнусный`,
                            statLabelTop: `гнусностей`,
                            statLabelBottom: `брошено`,
                            value: s.rollsGnus
                        }) : null, d == null ? null : (0, X.jsx)(eh, {
                            player: d.player,
                            categoryTitle: `Самый эффективный`,
                            statLabelTop: `очки`,
                            statLabelBottom: `в час`,
                            value: xf(d.kpd)
                        }), c != null && (c.gamesDropped ?? 0) > 0 ? (0, X.jsx)(eh, {
                            player: c,
                            categoryTitle: `Мистер Дроп`,
                            statLabelTop: ``,
                            statLabelBottom: `дропов`,
                            value: c.gamesDropped ?? 0
                        }) : null]
                    }), (0, X.jsxs)(`div`, {
                        className: `results-container`,
                        children: [l == null ? null : (0, X.jsx)(eh, {
                            player: l.player,
                            categoryTitle: `Самый крепкозадый`,
                            statLabelTop: `часов`,
                            statLabelBottom: `в игре`,
                            value: l.hours
                        }), u != null && (u.rollsTotal ?? 0) > 0 ? (0, X.jsx)(eh, {
                            player: u,
                            categoryTitle: `Казинозависимый`,
                            statLabelTop: `прокручено`,
                            statLabelBottom: `колес`,
                            value: u.rollsTotal ?? 0
                        }) : null, h == null ? null : (0, X.jsx)(eh, {
                            player: h.player,
                            categoryTitle: `Путешественник`,
                            statLabelTop: `средний счет`,
                            statLabelBottom: `в геогессере`,
                            value: h.score
                        })]
                    }), (0, X.jsxs)(`div`, {
                        className: `results-container`,
                        children: [_ == null ? null : (0, X.jsx)(eh, {
                            player: _.player,
                            categoryTitle: `Певец №1`,
                            statLabelTop: `спето`,
                            statLabelBottom: `песен`,
                            value: _.score
                        }), g == null ? null : (0, X.jsx)(eh, {
                            player: g.player,
                            categoryTitle: `Звезда танцпола`,
                            statLabelTop: `средний счет`,
                            statLabelBottom: `в танцах`,
                            value: g.score
                        })]
                    }), (0, X.jsxs)(`div`, {
                        className: `results-container`,
                        children: [v == null ? null : (0, X.jsx)(eh, {
                            player: v.player,
                            categoryTitle: `Потерпевший`,
                            statLabelTop: `потеряно`,
                            statLabelBottom: `очков`,
                            value: v.score
                        }), m == null ? null : (0, X.jsx)(eh, {
                            player: m.player,
                            categoryTitle: `Лучший пилот`,
                            statLabelTop: `рекорд`,
                            statLabelBottom: `в вертолетике`,
                            value: m.score
                        }), y == null ? null : (0, X.jsx)(eh, {
                            player: y.player,
                            categoryTitle: `Величайший угонщик`,
                            statLabelTop: `украдено`,
                            statLabelBottom: `очков`,
                            value: y.score
                        })]
                    }), (0, X.jsx)(`div`, {
                        className: `results-divider`,
                        children: `///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////`
                    }), (0, X.jsx)(`div`, {
                        className: `results-text`, children: `Спасибо всем участникам за борьбу, зрителям за поддержку, донатерам за щедрость, а судье за терпение. Увидимся зимой!

И помните главное...`
                    }), (0, X.jsxs)(`div`, {
                        className: `results-hova-container`,
                        children: [(0, X.jsx)(`img`, {
                            className: `results-hova`,
                            src: J(`final/hova.webp`),
                            alt: `hova`
                        }), (0, X.jsx)(`div`, {className: `results-hova-text`, children: `...БЫЛО И БЫЛО`})]
                    })]
                })
            })
        })
    };

function oh() {
    return `/webgl/nassal-phone/index.html?v=1.5.6`
}

var sh = 2500, ch = [`localhost`, `127.0.0.1`, `::1`],
    lh = [`Local Lasqa`, `Debug Dund`, `Test Fak`, `Dev Cake`, `Mock Mel`, `Sandbox Mad`],
    uh = () => ch.includes(window.location.hostname), dh = e => {
        e.console.log = () => {
        }
    }, fh = e => e[Math.floor(Math.random() * e.length)], ph = () => {
        let e = crypto.randomUUID?.() ?? `local-${Date.now()}-${Math.random().toString(36).slice(2)}`,
            t = `${fh(lh)} ${Math.floor(1e3 + Math.random() * 9e3)}`;
        return {
            id: e,
            kind: `viewer`,
            login: t.toLowerCase().replace(/\s+/g, `_`),
            displayName: t,
            createdAt: new Date().toISOString()
        }
    }, mh = Math.ceil(Sn.length / 2), hh = ({forceHidden: e = !1}) => {
        let [t, r] = (0, Y.useState)(!1), [i, a] = (0, Y.useState)(!1),
            o = oh(), [s, c] = (0, Y.useState)(!1), [d, m] = (0, Y.useState)(!1), h = (0, Y.useRef)(null),
            g = (0, Y.useRef)(!1), _ = (0, Y.useRef)(!1), v = (0, Y.useRef)(null),
            y = Ri(), [b] = (0, Y.useState)(() => uh() ? ph() : null), {user: x} = ut(), {
                onBackHover: S,
                withBackClick: C
            } = n(), w = (0, Y.useCallback)(e => {
                let t = h.current?.contentWindow;
                t && t.postMessage(e, window.location.origin)
            }, []), T = (0, Y.useCallback)(() => {
                w({type: ss, paused: !1})
            }, [w]), E = (0, Y.useRef)(T);
        (0, Y.useEffect)(() => {
            E.current = T
        }, [T]);
        let D = (0, Y.useCallback)(() => {
            w(Es(x ?? b))
        }, [w, x, b]), O = (0, Y.useCallback)(() => {
            v.current != null && (window.clearTimeout(v.current), v.current = null), a(!1)
        }, []), k = (0, Y.useCallback)(() => {
            v.current != null && (window.clearTimeout(v.current), v.current = null), a(!0), E.current()
        }, []), A = (0, Y.useCallback)(() => {
            v.current != null && window.clearTimeout(v.current), v.current = window.setTimeout(() => {
                v.current = null, k()
            }, sh)
        }, [k]);
        (0, Y.useEffect)(() => () => {
            v.current != null && window.clearTimeout(v.current)
        }, []), (0, Y.useEffect)(() => {
            D()
        }, [D]), (0, Y.useEffect)(() => {
            let e = e => {
                let t = e.detail?.payload;
                typeof t != `object` || !t || w(t)
            };
            return window.addEventListener(As, e), () => {
                window.removeEventListener(As, e)
            }
        }, [w]), (0, Y.useEffect)(() => {
            g.current = t
        }, [t]), (0, Y.useEffect)(() => {
            w({type: cs, mode: t ? `fullscreen` : `docked`})
        }, [t, w]), (0, Y.useEffect)(() => {
            if (!t) return;
            T();
            let e = requestAnimationFrame(() => {
                h.current?.contentWindow?.focus()
            });
            return () => cancelAnimationFrame(e)
        }, [t, T]), (0, Y.useEffect)(() => {
            i && T()
        }, [i, T]), (0, Y.useEffect)(() => {
            let e = e => {
                if (e.origin !== window.location.origin) return;
                let t = h.current?.contentWindow;
                if (!t || e.source !== t) return;
                let n = e.data;
                if (!n || typeof n != `object`) return;
                let i = n;
                if (ae(), i.type === `nassal-afk-activity`) return;
                if (i.type === `nassal-webgl-ready`) {
                    _.current = !0, D(), w({
                        type: cs,
                        mode: g.current ? `fullscreen` : `docked`
                    }), g.current ? k() : (w({type: ss, paused: !0}), A());
                    return
                }
                if (i.type === `nassal-webgl-widget`) {
                    let e = i.expanded;
                    if (typeof e != `boolean`) return;
                    e && _.current && k(), r(t => t === e ? t : e);
                    return
                }
                if (i.type === `nassal-play-sound`) {
                    let e = i;
                    typeof e.sound == `string` && e.sound && l(e.sound, {
                        volume: 1,
                        randomPitch: e.randomPitch === !0,
                        randomVolume: e.randomVolume === !0,
                        allowDuringTrackPlayback: !0
                    });
                    return
                }
                if (i.type === `nassal-stop-sound`) {
                    let e = i.sound;
                    typeof e == `string` && e && p(e);
                    return
                }
                if (i.type === `nassal-get-sound-duration`) {
                    let e = i;
                    if (typeof e.sound == `string` && typeof e.requestId == `string`) {
                        let n = e.sound, r = e.requestId;
                        u(n).then(e => {
                            t.postMessage({type: ps, requestId: r, duration: e}, window.location.origin)
                        })
                    }
                    return
                }
                if (i.type === `nassal-open-dance-game`) {
                    c(!0);
                    return
                }
                if (Ss(n)) {
                    Ru(!0);
                    return
                }
                let a = xs(n);
                a && (async () => {
                    try {
                        let e = await ws(a);
                        t.postMessage(e, window.location.origin)
                    } catch (e) {
                        t.postMessage(Ts(a, e), window.location.origin)
                    }
                })()
            };
            return window.addEventListener(`message`, e), () => window.removeEventListener(`message`, e)
        }, [T, D, w, k, A]), (0, Y.useEffect)(() => {
            t && ae()
        }, [t]), (0, Y.useEffect)(() => {
            let e = h.current;
            if (!e) return;
            let t = () => ae();
            return e.addEventListener(`focus`, t), () => e.removeEventListener(`focus`, t)
        }, [i]), (0, Y.useEffect)(() => {
            if (!t) return;
            let e = document.body.style.overflow;
            return document.body.style.overflow = `hidden`, () => {
                document.body.style.overflow = e
            }
        }, [t]);
        let j = (0, Y.useCallback)(() => {
                _.current = !1, O(), D();
                let e = h.current?.contentWindow;
                e && dh(e)
            }, [O, D]), M = t || y, N = i || t,
            ee = `${t ? ` webgl-phone-anchor--expanded` : ``}${N ? ` webgl-phone-anchor--unity-ready` : ` webgl-phone-anchor--unity-loading`}`,
            te = `${e && !M ? ` webgl-phone-root--force-hidden` : ``}${t ? ` webgl-phone-root--expanded` : ``}`,
            ne = `${t ? ` webgl-widget--expanded` : ``}${N ? ` webgl-widget--unity-ready` : ` webgl-widget--unity-loading`}`,
            P = () => {
                c(!1), m(!1)
            }, re = () => {
                P(), dn()
            }, F = () => {
                P(), _n()
            }, ie = () => {
                m(!0)
            }, I = e => {
                P(), Yt(e)
            }, L = (0, Y.useCallback)(() => {
                ae(), !t && l(f.Menu_Phone_Up)
            }, [t]), oe = (0, Y.useCallback)(() => {
                t || l(f.Menu_Phone_Down)
            }, [t]);
        return (0, X.jsxs)(X.Fragment, {
            children: [s ? (0, X.jsx)(`div`, {
                className: `audio-chart-uploader-overlay`, role: `presentation`, onClick: P, children: (0, X.jsx)(`div`, {
                    className: `audio-chart-uploader-modal${d ? ` audio-chart-uploader-modal--track-picker` : ``}`,
                    role: `dialog`,
                    "aria-modal": `true`,
                    "aria-labelledby": `dance-upload-modal-text dance-upload-modal-random-text dance-upload-modal-select-text`,
                    onClick: e => e.stopPropagation(),
                    children: d ? (0, X.jsxs)(X.Fragment, {
                        children: [(0, X.jsx)(`p`, {
                            className: `audio-chart-uploader-modal-sub`,
                            children: `Выберите трек, который хотите сыграть`
                        }), (0, X.jsxs)(`div`, {
                            className: `audio-chart-uploader-track-columns`,
                            children: [(0, X.jsx)(`ul`, {
                                className: `audio-chart-uploader-track-list`,
                                role: `listbox`,
                                "aria-label": `Список треков, левая колонка`,
                                children: Sn.slice(0, mh).map((e, t) => (0, X.jsx)(`li`, {
                                    role: `presentation`,
                                    children: (0, X.jsx)(`button`, {
                                        type: `button`,
                                        role: `option`,
                                        className: `audio-chart-uploader-track-item`,
                                        onClick: C(() => I(t)),
                                        onMouseEnter: S,
                                        children: e.songName
                                    })
                                }, e.fileName))
                            }), (0, X.jsx)(`ul`, {
                                className: `audio-chart-uploader-track-list`,
                                role: `listbox`,
                                "aria-label": `Список треков, правая колонка`,
                                children: Sn.slice(mh).map((e, t) => (0, X.jsx)(`li`, {
                                    role: `presentation`,
                                    children: (0, X.jsx)(`button`, {
                                        type: `button`,
                                        role: `option`,
                                        className: `audio-chart-uploader-track-item`,
                                        onClick: C(() => I(mh + t)),
                                        onMouseEnter: S,
                                        children: e.songName
                                    })
                                }, e.fileName))
                            })]
                        }), (0, X.jsx)(`div`, {
                            className: `audio-chart-uploader-modal-actions`,
                            children: (0, X.jsx)(`button`, {
                                type: `button`,
                                className: `audio-chart-uploader-modal-cancel`,
                                onClick: C(() => m(!1)),
                                onMouseEnter: S,
                                children: `Назад`
                            })
                        })]
                    }) : (0, X.jsxs)(X.Fragment, {
                        children: [(0, X.jsx)(`p`, {
                            id: `dance-upload-modal-text`,
                            className: `audio-chart-uploader-modal-sub`,
                            children: `Выберите аудиофайл (MP3, WAV, OGG или WebM) чтобы сыграть в танцевальную мини игру`
                        }), (0, X.jsxs)(`div`, {
                            className: `audio-chart-uploader-modal-actions`,
                            children: [(0, X.jsx)(`button`, {
                                type: `button`,
                                className: `audio-chart-uploader-modal-cancel`,
                                onClick: C(P),
                                onMouseEnter: S,
                                children: `Отмена`
                            }), (0, X.jsx)(`button`, {
                                type: `button`,
                                className: `audio-chart-uploader-btn`,
                                onClick: C(re),
                                onMouseEnter: S,
                                children: `Загрузить`
                            })]
                        }), (0, X.jsx)(`p`, {
                            id: `dance-upload-modal-random-text`,
                            className: `audio-chart-uploader-modal-sub`,
                            children: `Или сыграйте случайный трек дня`
                        }), (0, X.jsx)(`div`, {
                            className: `audio-chart-uploader-modal-actions`,
                            children: (0, X.jsx)(`button`, {
                                type: `button`,
                                className: `audio-chart-uploader-btn`,
                                onClick: C(F),
                                onMouseEnter: S,
                                children: `Сыграть`
                            })
                        }), (0, X.jsx)(`p`, {
                            id: `dance-upload-modal-select-text`,
                            className: `audio-chart-uploader-modal-sub`,
                            children: `Или выберите трек дня, который хотите сыграть`
                        }), (0, X.jsx)(`div`, {
                            className: `audio-chart-uploader-modal-actions`,
                            children: (0, X.jsx)(`button`, {
                                type: `button`,
                                className: `audio-chart-uploader-btn`,
                                onClick: C(ie),
                                onMouseEnter: S,
                                children: `Выбрать`
                            })
                        })]
                    })
                })
            }) : null, (0, Xn.createPortal)((0, X.jsx)(`div`, {
                className: `webgl-phone-anchor${ee}`,
                children: (0, X.jsxs)(`div`, {
                    className: `webgl-phone-root${te}`,
                    onMouseEnter: L,
                    onMouseLeave: oe,
                    onPointerDown: () => ae(),
                    children: [(0, X.jsx)(`img`, {
                        className: `webgl-phone-antenna`,
                        src: J(`ui/phone-antenna.png`),
                        alt: ``,
                        "aria-hidden": !0
                    }), (0, X.jsxs)(`div`, {
                        className: `webgl-widget${ne}`,
                        "aria-label": `NASSALPhone WebGL`,
                        children: [t ? (0, X.jsx)(`div`, {
                            className: `webgl-widget__backdrop`,
                            "aria-hidden": !0
                        }) : null, (0, X.jsx)(`div`, {
                            className: `webgl-widget__inner`,
                            onClick: e => e.stopPropagation(),
                            role: t ? `dialog` : void 0,
                            "aria-modal": t ? !0 : void 0,
                            "aria-label": t ? `NASSALPhone enlarged` : void 0,
                            children: (0, X.jsx)(`iframe`, {
                                ref: h,
                                className: `webgl-widget__frame`,
                                src: o,
                                title: `NASSALPhone WebGL`,
                                onLoad: j
                            })
                        })]
                    })]
                })
            }), document.body)]
        })
    }, gh = Object.values(Object.assign({
        "./img/screensavers/Ara_1.webp": tn,
        "./img/screensavers/Ara_2.webp": Qt,
        "./img/screensavers/Cake_1.webp": xn,
        "./img/screensavers/Cake_2.webp": ln,
        "./img/screensavers/Dund_1.webp": Xt,
        "./img/screensavers/Dund_2.webp": vn,
        "./img/screensavers/Dund_3.webp": mn,
        "./img/screensavers/Flash_1.webp": Jt,
        "./img/screensavers/Flash_2.webp": an,
        "./img/screensavers/Glad_1.webp": gn,
        "./img/screensavers/Glad_2.webp": fn,
        "./img/screensavers/Karm_1.webp": yn,
        "./img/screensavers/Karm_2.webp": $t,
        "./img/screensavers/Karm_3.webp": Tt,
        "./img/screensavers/Lasqa_1.webp": zt,
        "./img/screensavers/Lasqa_2.webp": Kt,
        "./img/screensavers/Lasqa_3.webp": Wt,
        "./img/screensavers/Lasqa_4.webp": Gt,
        "./img/screensavers/Mad_1.webp": Mt,
        "./img/screensavers/Mad_2.webp": hn,
        "./img/screensavers/Mad_3.webp": Nt,
        "./img/screensavers/Mel_1.webp": kt,
        "./img/screensavers/Mel_2.webp": Ut,
        "./img/screensavers/Mel_3.webp": Vt,
        "./img/screensavers/Mel_4.webp": At,
        "./img/screensavers/Norm_1.webp": Ht,
        "./img/screensavers/Norm_2.webp": Dt,
        "./img/screensavers/Norm_3.webp": wn,
        "./img/screensavers/Norm_4.webp": Bt,
        "./img/screensavers/Prad_1.webp": Pt,
        "./img/screensavers/Prad_2.webp": jt,
        "./img/screensavers/Prad_3.webp": Ot,
        "./img/screensavers/Prad_4.webp": Rt,
        "./img/screensavers/Zu_1.webp": It,
        "./img/screensavers/Zu_2.webp": Ft,
        "./img/screensavers/Zu_3.webp": on
    })).filter(e => typeof e == `string` && e.length > 0), _h = 1e4, vh = 1400, yh = 1200, bh = 800;

function xh(e) {
    return e % 2 == 0 ? `left` : `right`
}

function Sh(e) {
    let t = gh;
    if (t.length === 0) return ``;
    if (t.length === 1) return t[0];
    let n = t[Math.floor(Math.random() * t.length)], r = 0;
    for (; n === e && r < 8;) n = t[Math.floor(Math.random() * t.length)], r += 1;
    return n
}

function Ch({active: e}) {
    let [t, n] = (0, Y.useState)(null), r = (0, Y.useRef)(0), i = (0, Y.useRef)(0), a = (0, Y.useRef)(``),
        o = (0, Y.useRef)(() => void 0), s = (0, Y.useRef)(null), c = (0, Y.useRef)(null), l = (0, Y.useRef)(null),
        u = (0, Y.useCallback)(() => {
            s.current != null && (window.clearTimeout(s.current), s.current = null), c.current != null && (window.clearTimeout(c.current), c.current = null), l.current != null && (window.cancelAnimationFrame(l.current), l.current = null)
        }, []), d = (0, Y.useCallback)(() => {
            n(e => e && {...e, shown: !1})
        }, []), f = (0, Y.useCallback)(e => {
            n({...e, shown: !1}), l.current = window.requestAnimationFrame(() => {
                n(t => t && t.key === e.key ? {...t, shown: !0} : t)
            })
        }, []), p = (0, Y.useCallback)(() => {
            let e = r.current, t = Sh(a.current || void 0);
            a.current = t, r.current += 1, i.current += 1, f({key: i.current, url: t, side: xh(e), shown: !1});
            let n = Math.max(0, _h - bh);
            c.current = window.setTimeout(() => {
                d()
            }, n), s.current = window.setTimeout(() => {
                o.current()
            }, _h)
        }, [d, f]);
    (0, Y.useEffect)(() => {
        o.current = p
    }, [p]);
    let m = e && gh.length > 0, h = (0, Y.useCallback)(() => {
        r.current = 0, i.current = 0, a.current = ``
    }, []);
    return (0, Y.useEffect)(() => {
        if (!m) {
            u(), h();
            return
        }
        return p(), () => {
            u(), h()
        }
    }, [m, u, h, p]), !m || !t ? null : (0, X.jsxs)(`div`, {
        className: `screensaver`,
        "aria-hidden": !0,
        style: {
            "--screensaver-overlay-fade-in-ms": `${vh}ms`,
            "--screensaver-fade-in-ms": `${yh}ms`,
            "--screensaver-fade-out-ms": `${bh}ms`,
            "--screensaver-hold-ms": `${_h}ms`
        },
        children: [(0, X.jsx)(`div`, {
            className: `screensaver__stage`,
            children: (0, X.jsx)(`div`, {
                className: `screensaver__frame screensaver__frame--${t.side}`,
                children: (0, X.jsx)(`div`, {
                    className: `screensaver__zoom`,
                    children: (0, X.jsx)(`img`, {
                        className: [`screensaver__image`, t.shown ? `screensaver__image--shown` : ``].filter(Boolean).join(` `),
                        src: t.url,
                        alt: ``,
                        draggable: !1
                    })
                })
            }, t.key)
        }), (0, X.jsx)(`div`, {
            className: `screensaver__brand`,
            children: (0, X.jsx)(zp, {hide: !1, showScreensaverToggle: !1})
        })]
    })
}

var wh = [`mousemove`, `mousedown`, `keydown`, `touchstart`, `wheel`, `scroll`, `click`], Th = 14e4;

function Eh({timeoutMs: e = Th, enabled: t = !0} = {}) {
    let [n, r] = (0, Y.useState)(!1), i = (0, Y.useRef)(!1), a = (0, Y.useRef)(null), o = (0, Y.useCallback)(() => {
        a.current != null && (window.clearTimeout(a.current), a.current = null)
    }, []), s = (0, Y.useCallback)(() => {
        i.current && (i.current = !1, r(!1))
    }, []), c = (0, Y.useCallback)(() => {
        o(), i.current = !1, queueMicrotask(() => {
            r(!1)
        })
    }, [o]), l = (0, Y.useCallback)(() => {
        o(), t && (a.current = window.setTimeout(() => {
            i.current = !0, r(!0)
        }, e))
    }, [o, t, e]), u = (0, Y.useCallback)(() => {
        s(), l()
    }, [l, s]);
    return (0, Y.useEffect)(() => {
        if (!t) {
            c();
            return
        }
        l();
        let e = () => u();
        for (let t of wh) window.addEventListener(t, e, {passive: !0});
        return window.addEventListener(L, e), () => {
            for (let t of wh) window.removeEventListener(t, e);
            window.removeEventListener(L, e), o()
        }
    }, [o, t, u, c, l]), t && n
}

function Dh(e) {
    let t = Math.min(100, Math.max(0, e));
    if (t <= 0) return 0;
    let n = 0;
    for (let e = 0; e < 7; e++) t >= e / 6 * 100 && (n += 1);
    return n
}

var Oh = 100, kh = `https://auc.nassal.pro`, Ah = J(`laptop/opening.webm`), jh = J(`laptop/opening.webp`),
    Mh = J(`laptop/opening_gold.webm`), Nh = J(`laptop/opening_gold.webp`), Ph = J(`laptop/overlay.webp`),
    Fh = J(`laptop/result_button.webm`), Ih = `settings_ambient_enabled`, Lh = () => {
        try {
            return localStorage.getItem(Ih) !== `0`
        } catch {
            return !0
        }
    }, Rh = Dh, zh = () => {
        let n = bm(), r = ft(), i = xm(), {play: l} = oe(), {isAuthenticated: u, loading: d} = ut(), p = te(), m = _c(),
            h = d ? null : u, {notification: v, hideNotification: b, showNotification: x} = Cn();
        pm();
        let S = Fe(), C = !d && h === !0, {data: w, refetch: T, isLoading: E} = tu(C), {data: D, refetch: O} = as(),
            A = (0, Y.useMemo)(() => D ? [...D].sort((e, t) => t.ggp - e.ggp) : [], [D]), j = (0, Y.useCallback)(e => {
                if (A.length === 0) return null;
                let t = 0, n = null, r = 0;
                for (let i of A) if (r += 1, (n === null || i.ggp !== n) && (t = r, n = i.ggp), i.id === e) return t;
                return null
            }, [A]), {data: M} = eu(),
            N = (0, Y.useMemo)(() => M ?? [], [M]), {displayAchievements: ne} = Pu(), {data: P} = ll({
                enabled: C && w?.id != null,
                refetchInterval: go
            }), [F, ie] = (0, Y.useState)($.PLAYERS), [I, ae] = (0, Y.useState)(null), [L, se] = (0, Y.useState)(null), [R, z] = (0, Y.useState)(!1), [ce, le] = (0, Y.useState)(!1), {
                reached: ue,
                countdownJustExpired: de
            } = Bp(), fe = !r && ue && !ce && F !== $.RULES, [pe, B] = (0, Y.useState)(!1);
        (0, Y.useEffect)(() => {
            if (!fe) {
                B(!1);
                return
            }
            let e = 0, t = 0;
            return e = requestAnimationFrame(() => {
                t = requestAnimationFrame(() => {
                    B(!0)
                })
            }), () => {
                cancelAnimationFrame(e), cancelAnimationFrame(t), B(!1)
            }
        }, [fe]), (0, Y.useEffect)(() => {
            de && O()
        }, [de, O]);
        let [V, me] = (0, Y.useState)(!0), [he, ge] = (0, Y.useState)(!1), [_e, ve] = (0, Y.useState)(!1), [ye, be] = (0, Y.useState)(!1),
            xe = (0, Y.useRef)(null),
            Se = Mp(), [Ce, we] = (0, Y.useState)(!1), [, Te] = (0, Y.useState)(!1), [, De] = (0, Y.useState)(!1), [Oe, ke] = (0, Y.useState)(Mu[0].title), [Ae, je] = (0, Y.useState)(!1), [H, Me] = (0, Y.useState)(null), [Ne, U] = (0, Y.useState)(null), [W, Pe] = (0, Y.useState)(null),
            G = w ?? null, Le = G?.id != null && j(G.id) === 1, Re = Le ? Mh : Ah, ze = Le ? Nh : jh,
            Be = C && E && G == null, [Ve, He] = (0, Y.useState)(!1), [Ue, We] = (0, Y.useState)(!1);
        (0, Y.useEffect)(() => {
            let e = !1, n = window.setTimeout(() => {
                e || We(!0)
            }, 3e3);
            return t().then(() => {
                e || He(!0)
            }), () => {
                e = !0, window.clearTimeout(n)
            }
        }, []);
        let Ge = (Ve || Ue) && !d && (!u || !Be), [Ke, qe] = (0, Y.useState)(null), [Je, Ye] = (0, Y.useState)(null), [Xe, Ze] = (0, Y.useState)(``), [Qe, et] = (0, Y.useState)(``), [tt, K] = (0, Y.useState)(!1), [nt, rt] = (0, Y.useState)(!1), [it, at] = (0, Y.useState)(!1), [ot, st] = (0, Y.useState)(!1), [ct, lt] = (0, Y.useState)(!1), [dt, pt] = (0, Y.useState)(null), [mt, ht] = (0, Y.useState)(null), [gt, _t] = (0, Y.useState)(null), [vt, yt] = (0, Y.useState)(!1), [bt, xt] = (0, Y.useState)(!1), [St, Ct] = (0, Y.useState)(!1), [wt, Tt] = (0, Y.useState)(!1), [Et, Dt] = (0, Y.useState)(null), [Ot, kt] = (0, Y.useState)(null), [, At] = (0, Y.useState)(ym.CENTER),
            jt = (0, Y.useRef)(!1), [Mt, Nt] = (0, Y.useState)(!1), [Pt, Ft] = (0, Y.useState)(!1), [It, Lt] = (0, Y.useState)(!1), [Rt, zt] = (0, Y.useState)(!1),
            Bt = (0, Y.useRef)(null), Vt = (0, Y.useRef)(null), Ht = (0, Y.useRef)(H), Ut = (0, Y.useRef)(null),
            Wt = (0, Y.useRef)(!1), Gt = (0, Y.useRef)(null),
            Kt = (0, Y.useRef)(!1), [qt, Jt] = (0, Y.useState)(() => Ie(localStorage.getItem(`settings_colorScheme`))),
            Yt = (0, Y.useSyncExternalStore)(y, _, ee), [Xt, Zt] = (0, Y.useState)(() => Lh()),
            Qt = (0, Y.useSyncExternalStore)(Rp, Ip, () => !0), $t = ye && !V && !r, tn = Eh({enabled: $t && !Mt && Qt}),
            nn = $t && tn;
        jp({
            laptopVisible: $t,
            menuAmbientEnabled: Xt,
            casinoOpen: Mt,
            casinoInteractionBlocked: Pt,
            afkSuppressed: tn
        }), (0, Y.useEffect)(() => {
            let e = cn.current;
            a(Mt), re(Mt), e !== null && e !== Mt && l(f.Menu_Casino_Change), cn.current = Mt
        }, [Mt, l]);
        let rn = (0, Y.useRef)(Yt), an = (0, Y.useRef)(Yt), on = (0, Y.useRef)(Rh(Yt)), sn = (0, Y.useRef)(null),
            cn = (0, Y.useRef)(null), ln = (0, Y.useRef)(null), un = (0, Y.useRef)(!1), dn = (0, Y.useCallback)(e => {
                let t = Ut.current;
                t && t.querySelectorAll(`.header-volume-bar`).forEach((t, n) => {
                    let r = n / 6 * 100;
                    t.classList.toggle(`filled`, e !== 0 && e >= r)
                })
            }, []);
        (0, Y.useEffect)(() => {
            if (F !== $.EFFECTS || N.length === 0 || r) return;
            let e = Gc(N);
            e && ae(t => t ?? e)
        }, [F, N, r]);
        let fn = (0, Y.useCallback)(async () => (await T()).data ?? null, [T]);
        (0, Y.useEffect)(() => {
            G && H?.userId === G.userId && Me(G)
        }, [G, H?.userId]), (0, Y.useEffect)(() => {
            d || Dt(u)
        }, [u, d]), (0, Y.useEffect)(() => {
            localStorage.setItem(`settings_colorScheme`, qt), qt === `lighttheme` ? document.body.classList.add(`lighttheme`) : document.body.classList.remove(`lighttheme`), window.dispatchEvent(new CustomEvent(`colorSchemeChanged`))
        }, [qt]), (0, Y.useEffect)(() => {
            let e = Ie(localStorage.getItem(`settings_colorScheme`));
            e === `lighttheme` ? document.body.classList.add(`lighttheme`) : document.body.classList.remove(`lighttheme`), Jt(e)
        }, []), (0, Y.useEffect)(() => {
            Jt(m)
        }, [m]), (0, Y.useEffect)(() => {
            if (!_e || A.length === 0) return;
            let e = h ?? Et;
            if (r) {
                if (e === !1 && !jt.current) {
                    jt.current = !0;
                    let e = A[0];
                    Promise.resolve().then(() => {
                        kt(e)
                    })
                }
                return
            }
            if (e === !1 && !jt.current) {
                jt.current = !0;
                let e = A[0];
                Promise.resolve().then(() => {
                    kt(e), Me(e)
                });
                return
            }
            if (e !== !0 || Be || G != null && H == null || Ee(A, H)) return;
            let t = Ee(A, G) ?? A[0] ?? null;
            t && Promise.resolve().then(() => {
                kt(e => e ?? t), Me(t)
            })
        }, [A, _e, h, Et, r, G, Be, H]), (0, Y.useEffect)(() => {
            Ht.current = H
        }, [H]), (0, Y.useEffect)(() => {
            dn(Yt)
        }, [Yt, dn]), (0, Y.useEffect)(() => {
            let e = Ht.current;
            if (!e?.userId || !A.length) return;
            let t = Ee(A, e);
            if (t) {
                let n = (e.activeEffects || []).map(e => e.name).sort().join(`,`),
                    r = (t.activeEffects || []).map(e => e.name).sort().join(`,`);
                (t.ggp !== e.ggp || t.gamesCompleted !== e.gamesCompleted || t.gamesDropped !== e.gamesDropped || t.gameStreak !== e.gameStreak || t.status !== e.status || n !== r || JSON.stringify(e.effects) !== JSON.stringify(t.effects) || JSON.stringify(e.currentGame) !== JSON.stringify(t.currentGame)) && setTimeout(() => {
                    Me(t)
                }, 0)
            }
        }, [A]), (0, Y.useEffect)(() => {
            let e = $e.onAchievementReceived(e => {
                let t = e?.achievement;
                if (!t) {
                    console.error(`Achievement data is missing:`, e);
                    return
                }
                let n = vm(t);
                x(t.description, n, t.title, void 0, f.UI_NOT_Achivement)
            });
            return () => {
                e()
            }
        }, [x]);
        let pn = (0, Y.useCallback)(async e => {
                (await bo(e)).length !== 0 && zt(!0)
            }, []), mn = async () => {
                let e = u, t = null;
                e && (t = await fn(), r || Me(t)), ve(!0), ge(!0), Te(!0), De(!0), me(!1), xt(!0), At(ym.TOP), we(!0), t?.id && await pn(t.id)
            }, hn = () => {
                Ne && U(null), z(!0)
            }, gn = e => {
                U(e), Ct(!0)
            }, _n = e => {
                U(e), Ct(!1), Tt(!1)
            }, vn = () => {
                G && (U(G), Ct(!1), Tt(!1))
            }, yn = () => {
                Ct(!1), At(ym.TOP), l(f.Menu_Head_Click), we(!0), De(!0), Te(!0), U(null), Tt(!1)
            }, bn = () => {
                if (Et === !1) {
                    Te(!0), At(ym.TOP), l(f.Menu_Head_Click), we(!0), De(!0), U(null), Ct(!1), Tt(!1);
                    return
                }
                if (Ne) {
                    At(ym.TOP), l(f.Menu_Head_Click), we(!0), De(!0), Te(!0), Ne && !St && !wt ? (xn(Ne), U(null), Ct(!1), Tt(!1)) : (U(null), Ct(!1), Tt(!1));
                    return
                }
                At(ym.TOP), l(f.Menu_Head_Click), we(!0), Te(!0), De(!0), U(null), Ct(!1), Tt(!1)
            }, xn = e => {
                qe(null), Ye(null), !(H && H.userId === e.userId) && (pt(e.userId), yt(!0), st(!0), setTimeout(() => {
                    Me(e), st(!1), lt(!0), setTimeout(() => {
                        lt(!1), pt(null), yt(!1)
                    }, Oh)
                }, Oh))
            }, Sn = e => {
                Ke && Ke === e || (async () => {
                    await Dr() || (e === Q.COMPLETED && mf(G) && rt(!0), e === Q.COMPLETED && hf(G) && at(!0), e === Q.REROLLED && gf(G) && K(!0), st(!0), setTimeout(() => {
                        e === Q.COMPLETED ? Ye(xu(G?.currentAuctionResult)) : Ye(null), qe(e), st(!1), lt(!0), setTimeout(() => {
                            lt(!1)
                        }, Oh)
                    }, Oh))
                })()
            }, wn = e => {
                if (K(!1), rt(!1), at(!1), e?.skipFade) {
                    qe(null), Ye(null), st(!1), lt(!1);
                    return
                }
                st(!0), setTimeout(() => {
                    qe(null), Ye(null), st(!1), lt(!0), setTimeout(() => {
                        lt(!1)
                    }, Oh)
                }, Oh)
            }, Tn = h ?? Et, En = G?.role === `judge`, Dn = Tn === !0 && !Be && En,
            On = Tn === !0 && !Be && !En && G?.status === `idle`, kn = Tn === !0 && !Be && !En && G?.status === `casino`,
            An = G?.activeEffects?.some(e => e.name === `movie_ticket` || e.name === `movie_pause`) ?? !1, jn = On && An,
            Mn = On && !An, Nn = G != null && (H === null || H.userId === G.userId), Pn = Ke != null && Nn && Tn === !0,
            Fn = A[0] ?? null, In = (0, Y.useMemo)(() => {
                if (A.length === 0) return H ?? G ?? Ot ?? null;
                if (H != null) return Ee(A, H) ?? Ee(A, G) ?? Ee(A, Ot) ?? Fn;
                if (dt != null) {
                    let e = Ee(A, {userId: dt});
                    if (e) return e
                }
                return G == null ? Ee(A, Ot) ?? Fn : Ee(A, G) ?? G
            }, [A, H, G, Ot, Fn, dt]), Ln = (0, Y.useCallback)(e => e.id ?? e.userId ?? e.name, []),
            Rn = (0, Y.useMemo)(() => A.map(e => ({id: Ln(e), label: e.name})), [A, Ln]),
            zn = (0, Y.useMemo)(() => Wc(N).flatMap(e => e.effects.map(e => ({id: e.name, label: e.displayName}))), [N]),
            Bn = (0, Y.useMemo)(() => Mu.filter(e => Tn === !0 || e.title !== `Калькулятор`).map(e => ({
                id: e.title,
                label: e.title
            })), [Tn]), Vn = (0, Y.useMemo)(() => ne.filter(e => e.isRevealed && e.achievement != null).map(e => ({
                id: e.achievement.id,
                label: e.achievement.title
            })), [ne]);
        (0, Y.useEffect)(() => {
            Pe(null)
        }, [In?.id, F, R, Ne?.id]);
        let Hn = (0, Y.useMemo)(() => {
                if (dt != null) {
                    let e = Ee(A, {userId: dt});
                    if (e) return e
                }
                return In
            }, [dt, A, In]), Un = En && !Nn, Wn = Tn === !0 && !En && !Nn && G != null,
            Gn = (0, Y.useMemo)(() => mt == null ? I : N.find(e => e.name === mt) ?? I, [mt, N, I]),
            Kn = r && F === $.RULES && (Oe != null || Ae), qn = r && F === $.EFFECTS && I != null,
            Jn = r && F === $.ACHIEVEMENTS && L != null,
            Xn = r && !R && !ce && Ne == null && (F === $.PLAYERS && H == null || F === $.RULES && Oe == null && !Ae || F === $.EFFECTS && I == null || F === $.ACHIEVEMENTS && L == null),
            Zn = !R && !ce && Ne == null && (F === $.PLAYERS ? H == null : F === $.EFFECTS ? I == null : F === $.ACHIEVEMENTS ? L == null : F === $.RULES ? Oe == null && !Ae : !1),
            Qn = r && H != null && F === $.PLAYERS && !R && !ce && Ne == null && !Pn,
            $n = En && F === $.PLAYERS && Nn && !R && !ce && Ne == null && Ke == null, er = e => {
                if (!(!En || $n)) {
                    if (l(f.Menu_Head_Click), e?.skipFade) {
                        Me(null), ie($.PLAYERS), U(null), z(!1), Ct(!1), Tt(!1), qe(null), Ye(null), ae(null), se(null), Nt(!1), zt(!1), st(!1), lt(!1), pt(null), yt(!1);
                        return
                    }
                    yt(!0), st(!0), setTimeout(() => {
                        Me(null), ie($.PLAYERS), U(null), z(!1), Ct(!1), Tt(!1), qe(null), Ye(null), ae(null), se(null), Nt(!1), zt(!1), st(!1), lt(!0), setTimeout(() => {
                            lt(!1), pt(null), yt(!1)
                        }, Oh)
                    }, Oh)
                }
            }, tr = e => {
                if (!(!G || Nn)) {
                    if (l(f.Menu_Head_Click), e?.skipFade) {
                        Me(null), pt(G.userId), st(!1), lt(!1), yt(!1);
                        return
                    }
                    yt(!0), st(!0), setTimeout(() => {
                        Me(null), pt(G.userId), st(!1), lt(!0), setTimeout(() => {
                            lt(!1), pt(null), yt(!1)
                        }, Oh)
                    }, Oh)
                }
            }, nr = e => {
                let t = Ne ?? H;
                if (U(null), z(!1), Ct(!1), Tt(!1), je(!1), ie($.PLAYERS), t == null) {
                    l(f.Menu_Head_Click);
                    return
                }
                let n = (Ee(A, t) ?? t).userId;
                if (l(f.Menu_Head_Click), e?.skipFade) {
                    Me(null), pt(n), st(!1), lt(!1), yt(!1);
                    return
                }
                yt(!0), st(!0), setTimeout(() => {
                    Me(null), pt(n), st(!1), lt(!0), setTimeout(() => {
                        lt(!1), pt(null), yt(!1)
                    }, Oh)
                }, Oh)
            }, rr = e => {
                H != null && nr(e)
            }, ir = () => {
                I != null && (l(f.Menu_Head_Click), ht(I.name), ae(null))
            }, ar = () => {
                L != null && (l(f.Menu_Head_Click), _t(L.id), se(null))
            }, or = () => {
                if (Ae) {
                    l(f.Menu_Head_Click), je(!1);
                    return
                }
                Oe != null && (l(f.Menu_Head_Click), ke(null))
            }, sr = e => {
                ht(null), ae(e)
            }, cr = e => {
                _t(null), se(e)
            }, lr = e => {
                let t = A.find(t => Ln(t) === e);
                t && xn(t)
            }, ur = (0, Y.useCallback)(e => {
                let t = A.find(t => Ln(t) === e);
                t && U(t)
            }, [A, Ln]), dr = (0, Y.useCallback)(e => {
                let t = N.find(t => t.name === e);
                t && sr(t)
            }, [N]), fr = (0, Y.useCallback)(e => {
                je(!1), ke(e)
            }, []), pr = (0, Y.useCallback)(e => {
                let t = ne.find(t => t.achievement?.id === e)?.achievement;
                t && cr(t)
            }, [ne]);
        (0, Y.useEffect)(() => {
            if (sn.current === null) {
                sn.current = kn;
                return
            }
            kn && !sn.current && Nn && l(f.Menu_Casino_Change), sn.current = kn
        }, [kn, Nn, l]), (0, Y.useEffect)(() => {
            if (!Nn || !G?.id) return;
            let e = G.gnusAvailable ?? 0, t = Vt.current;
            if (t === null) {
                Vt.current = e;
                return
            }
            e > t && !Mt && G.casinoPhase !== `done` && Nt(!0), Vt.current = e
        }, [Nn, Mt, G, l]);
        let mr = (0, Y.useCallback)(e => {
            let t = Math.min(100, Math.max(0, e)), n = Rh(t);
            n !== on.current && (on.current = n, l(f.Menu_Button_Back_Click, {volume: .45})), dn(t), o(t), rn.current = t
        }, [l, dn]), gr = (0, Y.useCallback)(e => {
            let t = g(e);
            on.current = Rh(t), an.current = t, rn.current = t, dn(t)
        }, [dn]), _r = (0, Y.useCallback)((e, t) => {
            let n = Math.min(100, Math.max(0, e));
            if (Kt.current = !1, n > 0 && (Gt.current = null), t) {
                gr(n);
                return
            }
            mr(n)
        }, [gr, mr]), vr = (0, Y.useCallback)((e, t) => {
            let n = Ut.current;
            if (!n) return;
            let r = n.getBoundingClientRect();
            if (r.width <= 0) return;
            let i = Math.min(Math.max(e - r.left, 0), r.width);
            _r(Math.round(i / r.width * 100), t)
        }, [_r]), yr = e => {
            e.preventDefault(), Wt.current = !0, vr(e.clientX, !1)
        }, br = () => {
            if (Yt > 0) {
                l(f.Menu_Head_Click), Gt.current = Yt, Kt.current = !0, window.setTimeout(() => {
                    gr(0)
                }, 50);
                return
            }
            l(f.Menu_Head_Click);
            let e = Gt.current, t = Kt.current && e != null && e > 0;
            if (Kt.current = !1, Gt.current = null, t) {
                gr(e);
                return
            }
            gr(50)
        };
        (0, Y.useEffect)(() => {
            let e = e => {
                Wt.current && vr(e.clientX, !1)
            }, t = () => {
                Wt.current && (Wt.current = !1, gr(rn.current))
            };
            return window.addEventListener(`mousemove`, e), window.addEventListener(`mouseup`, t), () => {
                window.removeEventListener(`mousemove`, e), window.removeEventListener(`mouseup`, t)
            }
        }, [gr, vr]);
        let xr = (0, Y.useCallback)(() => {
                Lt(!1), Nt(!1)
            }, []), Sr = (0, Y.useCallback)(() => {
                V || (en(), zt(!0))
            }, [V]), Cr = (0, Y.useCallback)(() => {
                !G?.id || V || pn(G.id)
            }, [G?.id, V, pn]), wr = (0, Y.useCallback)(() => {
                zt(!1)
            }, []), Tr = (0, Y.useCallback)(async (e, t) => Xc(e, A, t), [A]), Er = (0, Y.useCallback)(() => {
                let e = (G?.id == null ? [] : yo(P, G.id))[0];
                e ? (async () => {
                    let {title: t, message: n, imagePath: r} = hr(N, e, await Tr(e.sourcePlayerId, e.pendingContext));
                    x(n, J(r), t, void 0, f.UI_NOT_Effect)
                })() : x(vo, J(`faces/angry.webp`), _o, void 0, f.UI_NOT_Error), Sr()
            }, [G?.id, P, N, Tr, x, Sr]),
            Dr = (0, Y.useCallback)(async e => !C || !G?.id || Mt && !It || (await bo(G.id)).length === 0 ? !1 : (e?.notify === !1 ? Sr() : Er(), !0), [C, G, Mt, It, Er, Sr]);
        (0, Y.useEffect)(() => {
            if (!C || !G?.id || V) return;
            let e = yo(P, G.id), t = new Set(e.map(e => e.id));
            if (Bt.current === null) {
                Bt.current = t;
                return
            }
            let n = Bt.current;
            for (let t of e) n.has(t.id) || (n.add(t.id), (async () => {
                let {title: e, message: n, imagePath: r} = hr(N, t, await Tr(t.sourcePlayerId, t.pendingContext));
                x(n, J(r), e, void 0, f.UI_NOT_Effect)
            })());
            for (let e of [...n]) t.has(e) || n.delete(e)
        }, [C, G?.id, P, N, Tr, V, x]), (0, Y.useEffect)(() => {
            if (!(!C || !G?.id)) {
                if (V) {
                    zt(!1);
                    return
                }
                if (Mt && !It) {
                    zt(!1);
                    return
                }
                if (yo(P, G.id).length > 0) {
                    zt(!0);
                    return
                }
                zt(!1)
            }
        }, [C, G, P, On, kn, Pn, Mt, It, V]), (0, Y.useEffect)(() => {
            let e = h === !0, t = ln.current;
            ln.current = e, !(!e || t || V || !_e || !G?.id) && pn(G.id)
        }, [h, V, _e, G?.id, pn]);
        let kr = (0, Y.useCallback)(() => {
            (async () => {
                G?.id && await Dr() || Nt(!0)
            })()
        }, [Dr, G?.id]), Ar = ye && !n && (h ?? Et) === !0, jr = (0, Y.useCallback)(() => {
            Ar && (l(f.Menu_Casino_Click), kr())
        }, [Ar, l, kr]), Mr = (0, Y.useCallback)(e => {
            e.currentTarget.classList.contains(`header-tab-active`) || l(f.Menu_Head_Hover, {panElement: e.currentTarget})
        }, [l]), Nr = (0, Y.useCallback)(() => {
            l(f.Menu_Head_Click)
        }, [l]), Pr = (0, Y.useCallback)(e => {
            l(f.Menu_Button_Participants_Hover, {panElement: e.currentTarget})
        }, [l]), Fr = (0, Y.useCallback)(() => {
            l(f.Menu_Button_Participants_Click)
        }, [l]), Ir = (0, Y.useCallback)(e => {
            let t = e.currentTarget;
            t.classList.contains(`active`) && t.classList.contains(`header-theme-toggle`) || l(f.Menu_Button_Back_Hover, {panElement: t})
        }, [l]), Lr = (0, Y.useCallback)(() => {
            l(f.Menu_Button_Back_Click)
        }, [l]), Rr = (0, Y.useCallback)(() => {
            Nr();
            let e = !Xt;
            Zt(e);
            try {
                localStorage.setItem(Ih, e ? `1` : `0`)
            } catch {
            }
            e || s()
        }, [Xt, Nr]), zr = () => Pn ? (0, X.jsxs)(X.Fragment, {
            children: [(0, X.jsx)(rf, {
                action: Ke,
                onClose: wn,
                onResetDrafts: () => {
                    Ze(``), et(``)
                },
                reviewDraft: Xe,
                rerollReasonDraft: Qe,
                onReviewDraftChange: Ze,
                onRerollReasonDraftChange: et,
                onRequireEffectResolution: Er,
                onCheckPendingEffectResolutions: Dr,
                gameName: G?.currentGame?.data && `name` in G.currentGame.data ? G.currentGame.data.name : null,
                currentGame: G?.currentGame ?? null,
                player: G,
                initialDurationSeconds: Je
            }), (0, X.jsx)(gu, {player: G, currentPlayer: G, allowActivation: !0, onEffectHover: Pe}, G?.id)]
        }) : jn && Nn ? (0, X.jsxs)(X.Fragment, {
            children: [(0, X.jsx)(Xl, {
                currentPlayer: G,
                onRequireEffectResolution: Er,
                onCheckPendingEffectResolutions: Dr,
                onShowHistory: vn
            }), (0, X.jsx)(gu, {currentPlayer: G, player: G, onEffectHover: Pe}, G?.id)]
        }) : Mn && Nn ? (0, X.jsxs)(X.Fragment, {
            children: [(0, X.jsx)(fl, {
                onRequireEffectResolution: Er,
                onCheckPendingEffectResolutions: Dr,
                onShowHistory: vn,
                player: G
            }), (0, X.jsx)(gu, {currentPlayer: G, player: G, onEffectHover: Pe}, G?.id)]
        }) : Dn && Nn ? (0, X.jsxs)(X.Fragment, {
            children: [(0, X.jsx)(Yl, {}, `judge-panel-${H?.userId ?? `self`}`), (0, X.jsx)(gu, {
                currentPlayer: G,
                player: G,
                allowActivation: !0,
                onEffectHover: Pe
            }, G?.id)]
        }) : kn && Nn ? (0, X.jsxs)(X.Fragment, {
            children: [(0, X.jsx)(Ql, {
                player: G,
                onShowSlots: kr,
                onShowHistory: vn,
                onRequireEffectResolution: Er,
                onCheckPendingEffectResolutions: Dr
            }), (0, X.jsx)(gu, {player: G, currentPlayer: G, allowActivation: !0, onEffectHover: Pe}, G?.id)]
        }) : In == null ? null : (0, X.jsxs)(X.Fragment, {
            children: [(0, X.jsx)(Du, {
                player: In,
                showPlayerHistory: _n.bind(null, In),
                onAction: Sn,
                isSelf: In.userId == G?.userId
            }), (0, X.jsx)(gu, {
                player: In,
                currentPlayer: G,
                allowActivation: In.userId == G?.userId,
                onEffectHover: Pe
            }, In.id)]
        }), Br = e => {
            e.currentTarget.pause(), be(!0)
        };
        (0, Y.useEffect)(() => {
            !r || V || be(!0)
        }, [r, V]), (0, Y.useEffect)(() => {
            V || un.current || r || (un.current = !0, c(), Xt && e())
        }, [V, r, Xt]), (0, Y.useEffect)(() => {
            !ye || r || l(f.Menu_Start_Screen)
        }, [ye, l, r]), (0, Y.useEffect)(() => {
            if (V || !ze) return;
            let e = new Image;
            e.src = ze
        }, [V, ze]), (0, Y.useEffect)(() => {
            if (V || !ye || !Ph) return;
            let e = new Image;
            e.src = Ph
        }, [V, ye]), (0, Y.useEffect)(() => {
            let e = xe.current;
            if (e) {
                if (V) {
                    let t = () => {
                        try {
                            e.pause(), e.currentTime = 0
                        } catch {
                        }
                    };
                    e.readyState >= HTMLMediaElement.HAVE_METADATA ? t() : e.addEventListener(`loadedmetadata`, t, {once: !0});
                    return
                }
                try {
                    e.currentTime = 0
                } catch {
                }
                e.play().catch(() => {
                })
            }
        }, [V]), (0, Y.useEffect)(() => {
            if (V || Se || ye) return;
            let e = xe.current;
            e != null && e.paused && e.play().catch(() => {
            })
        }, [V, Se, ye]), (0, Y.useEffect)(() => {
            V || ye || !Se || be(!0)
        }, [V, ye, Se]);
        let Vr = Oe ? Mu.find(e => e.title == Oe) : void 0, Hr = L == null ? `` : vm(L), Ur = (e, t = !0) => {
                !t && (R || Ne != null || ce) || (e !== F && Nr(), ie(e), U(null), z(!1), le(!1), Ct(!1), Tt(!1), je(!1), r && (e === $.RULES && ke(null), e === $.EFFECTS && (ae(null), ht(null)), e === $.ACHIEVEMENTS && (se(null), _t(null))))
            }, Wr = (0, Y.useMemo)(() => [$.PLAYERS, $.RULES, $.EFFECTS, $.ACHIEVEMENTS, $.CREDITS], []),
            Gr = !Mt && !V && !R && !ce && Ne == null && !p;
        k(`LB`, () => {
            let e = Wr[(Wr.indexOf(F) - 1 + Wr.length) % Wr.length];
            Ur(e)
        }, Gr), k(`RB`, () => {
            let e = Wr[(Wr.indexOf(F) + 1) % Wr.length];
            Ur(e)
        }, Gr);
        let Kr = () => {
                U(null), z(!0), Ct(!1), Tt(!1)
            }, qr = () => {
                St ? Kr() : (U(null), z(!1), Ct(!1), Tt(!1))
            }, Jr = (0, Y.useCallback)(() => {
                Lr(), le(!1)
            }, [Lr]), Yr = (0, Y.useCallback)(() => {
                Nr(), z(!1), U(null), Ct(!1), Tt(!1), le(!0)
            }, [Nr]), Xr = e => {
                let t = e?.fromSwipe ?? !1, n = t, r = () => {
                    t || Lr()
                };
                return Ae ? (r(), je(!1), !0) : Ne == null ? ce ? (r(), le(!1), !0) : R ? (r(), nr({skipFade: n}), !0) : Pn ? (r(), wn({skipFade: n}), !0) : Un ? (r(), er({skipFade: n}), !0) : Qn ? (r(), rr({skipFade: n}), !0) : Kn ? (r(), or(), !0) : qn ? (r(), ir(), !0) : Jn ? (r(), ar(), !0) : Wn ? (r(), tr({skipFade: n}), !0) : !1 : (r(), qr(), !0)
            }, Zr = r && !V && (R || ce || Ne != null || Pn || Un || Qn || Kn || qn || Jn || Wn) && !Mt && !p && !p,
            Qr = r && !V && !Mt && !p && (Ae || Ne != null || R || ce || Pn || Un || Qn || Kn || qn || Jn || Wn), {
                progress: $r,
                isDragging: ei,
                isSwipeFinalizing: ti,
                interactive: ni,
                onForegroundTransitionEnd: ri
            } = Pm(Qr, Xr, {interactive: Zr}), ii = F == $.PLAYERS && In != null && !R && !ce && (!r || H != null),
            ai = r && Ne != null, oi = ai && !St, si = ai && St && R, ci = ii && (Ne == null || oi),
            li = R && !ce && (Ne == null || si), ui = (e, t, n) => r ? (0, X.jsx)(`div`, {
                className: t ? `mobile-stack-host mobile-swipe-underlay mobile-swipe-underlay--live` : `mobile-stack-host`,
                "aria-hidden": n || void 0,
                children: e
            }) : e, di = !ti && (ei || $r > .001), fi = Zr && di && (Qn || Kn || qn || Jn || R && Ne == null || Wn),
            pi = Zr && di && !fi && (Pn || Un), mi = {
                onMemberClick: xn,
                onStatisticsClick: Kr,
                tab: F,
                onSelectedRule: e => {
                    je(!1), ke(e)
                },
                selectedRule: Oe,
                onOpenRecordsAccessGuide: () => je(!0),
                selectedMember: H,
                listHighlightedMember: Hn,
                memberSwitchLocked: vt,
                playersList: A,
                effects: N,
                selectedEffect: I,
                onSelectedEffect: sr,
                selectedAchievement: L,
                onSelectedAchievement: cr,
                listHighlightedEffect: Gn,
                listHighlightedAchievementId: gt,
                gamepadInputEnabled: !Mt && !V && !p
            };
        k(`B`, () => {
            Xr()
        }, !Mt && !V && !p), k(`LT`, () => {
            jr()
        }, Ar && !Mt && !V && !p);
        let hi = () => In == null ? null : gi(In), gi = e => {
            let t = Math.max(0, e.platinumChips ?? 0);
            if (t === 0) return (0, X.jsx)(`span`, {className: `tokens-zero`, children: `0`});
            let n = [];
            for (let e = 0; e < t; e++) n.push((0, X.jsx)(`div`, {
                className: `token-icon`,
                style: {left: `${e * 14}px`, zIndex: 100 - e}
            }, e));
            return (0, X.jsx)(`span`, {
                className: `tokens`,
                style: {width: `${20 + (t - 1) * 14}px`, minWidth: `${20 + (t - 1) * 14}px`},
                children: n
            })
        }, _i = () => In == null || In.socialLinks == null ? null : (0, X.jsx)(`div`, {
            className: `streamer-links`,
            children: In.socialLinks.map(({
                                              platform: e,
                                              url: t
                                          }, n) => (0, X.jsx)(`a`, {
                className: `streamer-link${Wu(e, In.stream) ? ` streamer-link--online` : ``}`,
                href: t,
                target: `_blank`,
                rel: `noopener noreferrer`,
                onMouseEnter: Pr,
                onClick: Fr,
                style: {backgroundImage: `url(${Hu(e)})`}
            }, `${e}-${t}-${n}`))
        });
        return (0, X.jsxs)(X.Fragment, {
            children: [(0, X.jsxs)(`div`, {
                className: `main${r ? ` main--mobile-layout` : ``}`,
                children: [(0, X.jsx)(um, {
                    casinoOpen: Mt,
                    isAuthenticated: Tn === !0
                }), !r && (0, X.jsx)(zp, {hide: !Ce && n || Mt}), (0, X.jsx)(dm, {
                    title: v.title,
                    message: v.message,
                    imageUrl: v.imageUrl,
                    show: v.show,
                    onHide: b,
                    duration: v.duration,
                    sound: v.sound
                }), (0, X.jsx)(cl, {
                    isOpen: Rt && !V,
                    onClose: wr,
                    playerId: G?.id ?? null,
                    effects: N,
                    playersList: A,
                    onResolved: () => {
                        Or(void 0, S)
                    }
                }), nt && (0, X.jsx)(sf, {onClose: () => rt(!1)}), it && (0, X.jsx)(uf, {onClose: () => at(!1)}), tt && (0, X.jsx)(pf, {onClose: () => K(!1)}), (0, X.jsxs)(`div`, {
                    className: `main-content-container ${Mt ? `main-content-container-casino-active` : ``}`,
                    children: [(0, X.jsx)(`div`, {
                        className: `casino-container`,
                        children: (0, X.jsx)(gc, {
                            isOpen: Mt,
                            onClose: xr,
                            player: G,
                            effects: N,
                            refreshPlayer: fn,
                            slotsTerminalMistakesCount: null,
                            setSlotsTerminalMistakesCount: () => {
                            },
                            onResolutionFlowActiveChange: Lt,
                            onPendingResolutionNeedsUi: Cr,
                            onCasinoInteractionBlockedChange: Ft
                        })
                    }), (0, X.jsxs)(`div`, {
                        className: `laptop-container${nn ? ` laptop-container--afk-hidden` : ``}`,
                        children: [(0, X.jsxs)(`div`, {
                            className: `laptop`,
                            children: [!r && (0, X.jsxs)(`div`, {
                                className: `laptop-opening-container ${Se ? `laptop-opening-container--use-poster` : ``}`,
                                children: [!V && ze ? (0, X.jsx)(`img`, {
                                    src: ze,
                                    alt: ``,
                                    className: `laptop-opening-poster ${ye ? `laptop-opening-poster--visible` : ``}`,
                                    "aria-hidden": !0
                                }) : null, (0, X.jsx)(`video`, {
                                    ref: xe,
                                    className: `laptop-opening-video ${ye ? `laptop-opening-video--hidden` : ``}`,
                                    muted: !0,
                                    playsInline: !0,
                                    preload: `auto`,
                                    onEnded: Br,
                                    children: (0, X.jsx)(`source`, {src: Re, type: `video/webm`})
                                }, Re)]
                            }), ye && (0, X.jsxs)(`div`, {
                                className: `laptop-screen`, children: [(0, X.jsxs)(`div`, {
                                    className: `header`,
                                    children: [Gr && (0, X.jsx)(q, {
                                        button: `LB`,
                                        className: `header-tab-gp-hint header-tab-gp-hint--left`
                                    }), (0, X.jsx)(`div`, {
                                        className: `header-tab ${F == $.PLAYERS ? `header-tab-active` : ``}`,
                                        onClick: () => Ur($.PLAYERS),
                                        onMouseEnter: Mr,
                                        children: `Участники`
                                    }), (0, X.jsx)(`div`, {className: `header-separator`}), (0, X.jsx)(`div`, {
                                        className: `header-tab ${F == $.RULES ? `header-tab-active` : ``}`,
                                        onClick: () => Ur($.RULES),
                                        onMouseEnter: Mr,
                                        children: `Правила`
                                    }), (0, X.jsx)(`div`, {className: `header-separator`}), (0, X.jsx)(`div`, {
                                        className: `header-tab ${F == $.EFFECTS ? `header-tab-active` : ``}`,
                                        onClick: () => Ur($.EFFECTS),
                                        onMouseEnter: Mr,
                                        children: `Эффекты`
                                    }), (0, X.jsx)(`div`, {className: `header-separator`}), (0, X.jsx)(`div`, {
                                        className: `header-tab ${F == $.ACHIEVEMENTS ? `header-tab-active` : ``}`,
                                        onClick: () => Ur($.ACHIEVEMENTS),
                                        onMouseEnter: Mr,
                                        children: `Достижения`
                                    }), (0, X.jsx)(`div`, {className: `header-separator`}), (0, X.jsx)(`div`, {
                                        className: `header-tab ${F == $.CREDITS ? `header-tab-active` : ``}`,
                                        onClick: () => Ur($.CREDITS),
                                        onMouseEnter: Mr,
                                        children: `Создатели`
                                    }), Gr && (0, X.jsx)(q, {
                                        button: `RB`,
                                        className: `header-tab-gp-hint header-tab-gp-hint--right`
                                    }), (0, X.jsx)(`div`, {className: `header-separator`}), (0, X.jsx)(`a`, {
                                        className: `header-tab header-tab-link`,
                                        href: kh,
                                        target: `_blank`,
                                        rel: `noopener noreferrer`,
                                        onClick: Nr,
                                        onMouseEnter: Mr,
                                        children: (0, X.jsxs)(`div`, {
                                            className: `auction-button-text`,
                                            children: [`Аукцион `, (0, X.jsx)(`div`, {className: `auction-arrow`})]
                                        })
                                    }), (0, X.jsxs)(`div`, {
                                        className: `header-mode`,
                                        children: [En && (0, X.jsx)(`button`, {
                                            type: `button`,
                                            className: `header-admin-icon`,
                                            onClick: () => {
                                                Nr(), er()
                                            },
                                            onMouseEnter: Mr,
                                            "aria-label": `Панель судьи`
                                        }), (0, X.jsx)(`button`, {
                                            type: `button`,
                                            className: `header-theme-toggle header-dark ${qt === `darktheme` ? `active` : ``}`,
                                            onClick: () => {
                                                qt !== `darktheme` && (Nr(), Jt(`darktheme`))
                                            },
                                            onMouseEnter: Mr,
                                            "aria-label": `Use default dark theme`,
                                            title: `Тёмная тема`
                                        }), (0, X.jsx)(`button`, {
                                            type: `button`,
                                            className: `header-theme-toggle header-light ${qt === `lighttheme` ? `active` : ``}`,
                                            onClick: () => {
                                                qt !== `lighttheme` && (Nr(), Jt(`lighttheme`))
                                            },
                                            onMouseEnter: Mr,
                                            "aria-label": `Use light theme`,
                                            title: `Светлая тема`
                                        })]
                                    }), (0, X.jsxs)(`div`, {
                                        className: `header-volume`,
                                        children: [(0, X.jsx)(`div`, {
                                            className: `header-volume-icon header-volume-icon-ambient ${Xt ? `` : `muted`}`,
                                            onClick: Rr,
                                            onMouseEnter: Mr,
                                            title: Xt ? `Отключить звук окружения` : `Включить звук окружения`
                                        }), (0, X.jsx)(`div`, {
                                            className: `header-volume-icon ${Yt === 0 ? `muted` : ``}`,
                                            onClick: br,
                                            onMouseEnter: Mr,
                                            title: Yt === 0 ? `Включить звук` : `Выключить звук`
                                        }), (0, X.jsx)(`div`, {
                                            ref: Ut,
                                            className: `header-volume-bars`,
                                            onMouseDown: yr,
                                            onMouseEnter: Mr,
                                            title: `Громкость`,
                                            children: Array.from({length: 7}).map((e, t) => {
                                                let n = t / 6 * 100;
                                                return (0, X.jsx)(`div`, {className: `header-volume-bar ${Yt != 0 && Yt >= n ? `filled` : ``}`}, t)
                                            })
                                        })]
                                    })]
                                }), (0, X.jsxs)(`div`, {
                                    className: `content`, children: [(0, X.jsxs)(`div`, {
                                        className: `content-body${Xn ? ` content-body--mobile-list-only` : ``}${ni ? ` content-body--mobile-swipe` : ``}${ei || ti ? ` is-swipe-dragging` : ``}${ti ? ` is-swipe-finalizing` : ``}`,
                                        style: ni ? {"--mobile-swipe-progress": String(ti ? 0 : $r)} : void 0,
                                        children: [Qr && (0, X.jsx)(`button`, {
                                            type: `button`,
                                            className: `mobile-swipe-back-hint${ei || ti ? ` mobile-swipe-back-hint--hidden` : ``}`,
                                            "aria-label": `Назад`,
                                            onClick: () => {
                                                Xr()
                                            }
                                        }), !R && !ce && Ne == null && F !== $.CREDITS && (!r || Zn) && (0, X.jsx)(Xu, {...mi}), fi && (0, X.jsx)(`div`, {
                                            className: `mobile-swipe-underlay mobile-swipe-underlay--menu`,
                                            "aria-hidden": !0,
                                            children: (0, X.jsx)(Xu, {...mi})
                                        }), pi && (0, X.jsx)(`div`, {
                                            className: `mobile-swipe-underlay mobile-swipe-underlay--fallback`,
                                            "aria-hidden": !0
                                        }), (0, X.jsxs)(`div`, {
                                            className: `content-body-right${ni && !ai ? ` mobile-swipe-foreground` : ``}${ai ? ` content-body-right--history-stack` : ``}${ai && (ei || ti) ? ` is-swipe-dragging` : ``}${ai && ti ? ` is-swipe-finalizing` : ``} ${ot && !ni ? `fade-out` : ``} ${(ct || bt) && !ni ? `fade-in` : ``}`,
                                            onTransitionEnd: ai ? void 0 : ri,
                                            onAnimationEnd: e => {
                                                e.animationName === `infoFadeIn` && xt(!1)
                                            },
                                            children: [ci && ui((0, X.jsxs)(`div`, {
                                                className: `main-content`, children: [(0, X.jsxs)(`div`, {
                                                    className: `title`,
                                                    children: [(0, X.jsx)(`div`, {
                                                        className: `title-left`,
                                                        children: Dn && Nn ? (0, X.jsxs)(X.Fragment, {
                                                            children: [(0, X.jsx)(`span`, {
                                                                className: `title-judge-name`,
                                                                children: G?.name ?? In.name
                                                            }), (0, X.jsx)(`span`, {className: `title-separator`}), (0, X.jsx)(`span`, {
                                                                className: `title-judge-suffix`,
                                                                children: `Панель судьи`
                                                            })]
                                                        }) : r ? (0, X.jsxs)(X.Fragment, {
                                                            children: [Qn && (0, X.jsxs)(X.Fragment, {
                                                                children: [(0, X.jsx)(`span`, {
                                                                    className: `title-clickable`,
                                                                    onClick: () => {
                                                                        Nr(), rr({skipFade: !0})
                                                                    },
                                                                    onMouseEnter: Mr,
                                                                    children: `Участники`
                                                                }), (0, X.jsx)(`span`, {className: `arrow-right`})]
                                                            }), In != null && (0, X.jsx)(Zu, {
                                                                label: In.name,
                                                                items: Rn,
                                                                selectedId: Ln(In),
                                                                onSelect: lr,
                                                                onHeadClick: Nr,
                                                                onHeadHover: () => l(f.Menu_Head_Hover)
                                                            })]
                                                        }) : (0, X.jsxs)(X.Fragment, {
                                                            children: [(0, X.jsx)(`span`, {
                                                                className: `title-member-identity`,
                                                                children: (0, X.jsx)(`span`, {
                                                                    className: `title-active`,
                                                                    children: In.name
                                                                })
                                                            }), (0, X.jsx)(`span`, {className: `title-separator`}), (0, X.jsx)(`span`, {
                                                                className: `title-token-text`,
                                                                children: `Premium вращения`
                                                            }), hi()]
                                                        })
                                                    }), In != null && (Pn ? (0, X.jsxs)(`div`, {
                                                        className: `title-right`,
                                                        onClick: () => {
                                                            Lr(), wn()
                                                        },
                                                        onMouseEnter: Ir,
                                                        children: [(0, X.jsx)(`span`, {className: `arrow-left`}), (0, X.jsx)(`span`, {children: `Назад`}), (0, X.jsx)(q, {
                                                            button: `B`,
                                                            className: `back-btn-gp-hint`
                                                        })]
                                                    }) : Qn ? (0, X.jsxs)(`div`, {
                                                        className: `title-right`,
                                                        onClick: () => {
                                                            Lr(), rr()
                                                        },
                                                        onMouseEnter: Ir,
                                                        children: [(0, X.jsx)(`span`, {className: `arrow-left`}), (0, X.jsx)(`span`, {children: `Участники`})]
                                                    }) : Un ? (0, X.jsxs)(`div`, {
                                                        className: `title-right`,
                                                        onClick: () => {
                                                            Lr(), er()
                                                        },
                                                        onMouseEnter: Ir,
                                                        children: [(0, X.jsx)(`span`, {className: `arrow-left`}), (0, X.jsx)(`span`, {children: `Назад`}), (0, X.jsx)(q, {
                                                            button: `B`,
                                                            className: `back-btn-gp-hint`
                                                        })]
                                                    }) : Wn ? (0, X.jsxs)(`div`, {
                                                        className: `title-right`,
                                                        onClick: () => {
                                                            Lr(), tr()
                                                        },
                                                        onMouseEnter: Ir,
                                                        children: [(0, X.jsx)(`span`, {className: `arrow-left`}), (0, X.jsx)(`span`, {children: `Назад`}), (0, X.jsx)(q, {
                                                            button: `B`,
                                                            className: `back-btn-gp-hint`
                                                        })]
                                                    }) : !r && _i())]
                                                }), zr(), W != null && (0, X.jsx)(Ac, {data: W})]
                                            }), oi, oi), F == $.RULES && !ce && (!r || Oe != null || Ae) && (0, X.jsxs)(`div`, {
                                                className: `main-content`, children: [(0, X.jsxs)(`div`, {
                                                    className: `title`,
                                                    children: [(0, X.jsx)(`div`, {
                                                        className: `title-left`,
                                                        children: r ? Ae ? (0, X.jsxs)(X.Fragment, {
                                                            children: [(0, X.jsx)(`span`, {
                                                                className: `title-clickable`,
                                                                onClick: () => {
                                                                    Nr(), je(!1)
                                                                },
                                                                onMouseEnter: Mr,
                                                                children: `Правила`
                                                            }), (0, X.jsx)(`span`, {className: `arrow-right`}), (0, X.jsx)(`span`, {
                                                                className: `title-active`,
                                                                children: `Доступ к списку рекордов`
                                                            })]
                                                        }) : (0, X.jsxs)(X.Fragment, {
                                                            children: [(0, X.jsx)(`span`, {
                                                                className: `title-clickable`,
                                                                onClick: () => {
                                                                    Nr(), or()
                                                                },
                                                                onMouseEnter: Mr,
                                                                children: `Правила`
                                                            }), (0, X.jsx)(`span`, {className: `arrow-right`}), Oe != null && (0, X.jsx)(Zu, {
                                                                label: Oe,
                                                                items: Bn,
                                                                selectedId: Oe,
                                                                onSelect: fr,
                                                                onHeadClick: Nr,
                                                                onHeadHover: () => l(f.Menu_Head_Hover)
                                                            })]
                                                        }) : Ae ? `Доступ к списку рекордов` : Oe
                                                    }), Ae ? (0, X.jsxs)(`div`, {
                                                        className: `title-right`,
                                                        onClick: () => {
                                                            Lr(), je(!1)
                                                        },
                                                        onMouseEnter: Ir,
                                                        children: [(0, X.jsx)(`span`, {className: `arrow-left`}), (0, X.jsx)(`span`, {children: `Назад`})]
                                                    }) : Kn && (0, X.jsxs)(`div`, {
                                                        className: `title-right`,
                                                        onClick: () => {
                                                            Lr(), or()
                                                        },
                                                        onMouseEnter: Ir,
                                                        children: [(0, X.jsx)(`span`, {className: `arrow-left`}), (0, X.jsx)(`span`, {children: `Правила`})]
                                                    })]
                                                }), (0, X.jsx)(`div`, {
                                                    className: `main-content-scrollable${Oe === `Калькулятор` && h === !0 && !Ae ? ` main-content-scrollable--calculator` : ``}`,
                                                    children: Ae ? (0, X.jsx)(ao, {
                                                        gamepadScroll: !0, children: (0, X.jsxs)(`div`, {
                                                            className: `main-content-text rules-main-content-text records-access-guide`,
                                                            children: [(0, X.jsxs)(`p`, {children: [`Спасибо, что поддерживаете стримеров ивента`, ` `, (0, X.jsx)(`strong`, {children: `NASSAL`}), `.`]}), (0, X.jsx)(`p`, {children: `Чтобы подогреть интерес, мы сделали для топ донатеров доступ к лидербордам всех мини-игр. Попади в список наравне со своими любимыми стримерами.`}), (0, X.jsx)(`h3`, {
                                                                className: `records-access-guide__heading`,
                                                                children: `Правила`
                                                            }), (0, X.jsxs)(`ul`, {
                                                                className: `records-access-guide__list`,
                                                                children: [(0, X.jsx)(`li`, {children: `Задонать любому стримеру.`}), (0, X.jsxs)(`li`, {children: [`Набери общую сумму доната в`, ` `, (0, X.jsx)(`strong`, {children: `25 000 ₽`}), `.`]}), (0, X.jsx)(`li`, {children: `Зайди через Twitch или Kick справа снизу ноутбука.`})]
                                                            }), (0, X.jsxs)(`div`, {
                                                                className: `records-access-guide__warning`,
                                                                children: [(0, X.jsx)(`strong`, {children: `ВНИМАНИЕ:`}), ` Ваш ник на платформе Twitch или Kick должен совпадать с ником, под которым вы донатите.`]
                                                            }), (0, X.jsxs)(`p`, {
                                                                children: [`Если вы опечатались или ошиблись, но при этом задонатили большую сумму — свяжитесь с нами. Самый простой способ — через Twitch-канал`, ` `, (0, X.jsx)(`a`, {
                                                                    href: `https://www.twitch.tv/mistafaker`,
                                                                    target: `_blank`,
                                                                    rel: `noopener noreferrer`,
                                                                    className: `records-access-guide__link`,
                                                                    children: `mistafaker`
                                                                }), `. Мы вам поможем.`]
                                                            })]
                                                        })
                                                    }, `records-access-guide`) : Oe === `Калькулятор` && h === !0 ? (0, X.jsx)($d, {}) : Vr ? (0, X.jsxs)(ao, {
                                                        gamepadScroll: !0,
                                                        children: [(0, X.jsx)(`div`, {
                                                            className: `main-content-text rules-main-content-text`,
                                                            children: (0, X.jsx)(od, {section: Vr})
                                                        }), r && (0, X.jsx)(`div`, {
                                                            className: `rules-records-access-footer`,
                                                            children: (0, X.jsxs)(`button`, {
                                                                type: `button`,
                                                                className: `records-access-btn`,
                                                                onClick: () => {
                                                                    Lr(), je(!0)
                                                                },
                                                                onMouseEnter: Ir,
                                                                children: [(0, X.jsx)(`span`, {children: `Доступ к списку`}), (0, X.jsx)(`span`, {children: `рекордов`})]
                                                            })
                                                        })]
                                                    }, Oe) : null
                                                })]
                                            }), F == $.EFFECTS && !ce && I != null && (() => {
                                                let e = Bc(I),
                                                    t = jc(I.activationConflictEffects).map(e => N.find(t => t.name === e)).filter(e => e != null);
                                                return (0, X.jsxs)(`div`, {
                                                    className: `main-content`,
                                                    children: [(0, X.jsxs)(`div`, {
                                                        className: `title`,
                                                        children: [(0, X.jsx)(`div`, {
                                                            className: `title-left`,
                                                            children: r ? (0, X.jsxs)(X.Fragment, {
                                                                children: [(0, X.jsx)(`span`, {
                                                                    className: `title-clickable`,
                                                                    onClick: () => {
                                                                        Nr(), ir()
                                                                    },
                                                                    onMouseEnter: Mr,
                                                                    children: `Эффекты`
                                                                }), (0, X.jsx)(`span`, {className: `arrow-right`}), (0, X.jsx)(Zu, {
                                                                    label: I.displayName,
                                                                    items: zn,
                                                                    selectedId: I.name,
                                                                    onSelect: dr,
                                                                    onHeadClick: Nr,
                                                                    onHeadHover: () => l(f.Menu_Head_Hover)
                                                                })]
                                                            }) : (0, X.jsx)(`span`, {
                                                                className: `title-active`,
                                                                children: I.displayName
                                                            })
                                                        }), qn && (0, X.jsxs)(`div`, {
                                                            className: `title-right`,
                                                            onClick: () => {
                                                                Lr(), ir()
                                                            },
                                                            onMouseEnter: Ir,
                                                            children: [(0, X.jsx)(`span`, {className: `arrow-left`}), (0, X.jsx)(`span`, {children: `Эффекты`})]
                                                        })]
                                                    }), (0, X.jsxs)(`div`, {
                                                        className: `main-content-with-img effect-detail`,
                                                        children: [(0, X.jsx)(`div`, {
                                                            className: `effect-detail-image`,
                                                            children: (0, X.jsx)(yc, {
                                                                imageUrl: Yn(I.name, `animated`),
                                                                className: `main-content-image`,
                                                                skeletonWidth: `100%`,
                                                                skeletonHeight: `100%`,
                                                                skeletonVariant: `rect`,
                                                                backgroundSize: `contain`
                                                            })
                                                        }), (0, X.jsxs)(`div`, {
                                                            className: `effect-detail-panel`,
                                                            children: [(0, X.jsx)(`div`, {
                                                                className: `effect-detail-description-scroll`,
                                                                children: (0, X.jsx)(ao, {
                                                                    gamepadScroll: !0,
                                                                    children: (0, X.jsx)(`div`, {
                                                                        className: `effect-detail-description-inner`,
                                                                        children: (0, X.jsx)(`div`, {
                                                                            className: `effect-detail-description`,
                                                                            children: Array.isArray(I.description) ? I.description.map((e, t) => (0, X.jsx)(`div`, {children: e}, t)) : I.description
                                                                        })
                                                                    })
                                                                })
                                                            }), (0, X.jsx)(`div`, {
                                                                className: `effect-detail-divider`,
                                                                children: `////////////////////////////////////////////////////////////////////////////////////////////////`
                                                            }), e.map(e => (0, X.jsxs)(`div`, {
                                                                className: `effect-detail-allowed-row`,
                                                                children: [(0, X.jsx)(`img`, {
                                                                    src: e.iconUrl,
                                                                    alt: ``,
                                                                    className: `effect-detail-allowed-icon`
                                                                }), (0, X.jsx)(`span`, {children: e.label})]
                                                            }, `${e.iconUrl}-${e.label}`)), t.length > 0 && (0, X.jsxs)(X.Fragment, {
                                                                children: [(0, X.jsx)(`div`, {
                                                                    className: `effect-detail-blocked-label`,
                                                                    children: `Блокирует:`
                                                                }), (0, X.jsx)(`div`, {
                                                                    className: `effect-detail-blocked-images`,
                                                                    children: t.map(e => (0, X.jsx)(bc, {
                                                                        src: Yn(e.name, `static`),
                                                                        alt: e.displayName,
                                                                        className: `effect-detail-blocked-image`,
                                                                        title: e.displayName,
                                                                        skeletonWidth: `100%`,
                                                                        skeletonHeight: `100%`,
                                                                        skeletonVariant: `rect`
                                                                    }, e.name))
                                                                })]
                                                            })]
                                                        })]
                                                    })]
                                                })
                                            })(), F == $.ACHIEVEMENTS && !ce && L != null && (0, X.jsxs)(`div`, {
                                                className: `main-content`,
                                                children: [(0, X.jsxs)(`div`, {
                                                    className: `title`,
                                                    children: [(0, X.jsx)(`div`, {
                                                        className: `title-left`,
                                                        children: r ? (0, X.jsxs)(X.Fragment, {
                                                            children: [(0, X.jsx)(`span`, {
                                                                className: `title-clickable`,
                                                                onClick: () => {
                                                                    Nr(), ar()
                                                                },
                                                                onMouseEnter: Mr,
                                                                children: `Достижения`
                                                            }), (0, X.jsx)(`span`, {className: `arrow-right`}), (0, X.jsx)(Zu, {
                                                                label: L.title,
                                                                items: Vn,
                                                                selectedId: L.id,
                                                                onSelect: pr,
                                                                onHeadClick: Nr,
                                                                onHeadHover: () => l(f.Menu_Head_Hover)
                                                            })]
                                                        }) : (0, X.jsx)(`span`, {
                                                            className: `title-active`,
                                                            children: L.title
                                                        })
                                                    }), Jn && (0, X.jsxs)(`div`, {
                                                        className: `title-right`,
                                                        onClick: () => {
                                                            Lr(), ar()
                                                        },
                                                        onMouseEnter: Ir,
                                                        children: [(0, X.jsx)(`span`, {className: `arrow-left`}), (0, X.jsx)(`span`, {children: `Достижения`})]
                                                    })]
                                                }), (0, X.jsxs)(`div`, {
                                                    className: `main-content-with-img achievement-detail`,
                                                    children: [(0, X.jsx)(yc, {
                                                        imageUrl: Hr,
                                                        className: `main-content-image`,
                                                        skeletonWidth: `100%`,
                                                        skeletonHeight: `100%`,
                                                        skeletonVariant: `rect`,
                                                        backgroundSize: `contain`
                                                    }), (0, X.jsx)(ao, {
                                                        gamepadScroll: !0,
                                                        children: (0, X.jsxs)(`div`, {
                                                            className: `achievement-detail-content`,
                                                            children: [(0, X.jsx)(`div`, {
                                                                className: `main-content-text achievement-detail-text`,
                                                                children: L.description
                                                            }), L.achievedPlayers.length > 0 && (0, X.jsxs)(X.Fragment, {
                                                                children: [(0, X.jsx)(`div`, {
                                                                    className: `achievement-detail-divider`,
                                                                    children: `////////////////////////////////////////////////////////////////////////////////////////////////`
                                                                }), (0, X.jsxs)(`div`, {
                                                                    className: `achievement-detail-players`,
                                                                    children: [(0, X.jsxs)(`span`, {
                                                                        className: `achievement-detail-players-label`,
                                                                        children: [`Получилии:`, ` `]
                                                                    }), L.achievedPlayers.map(e => e.name).join(`, `)]
                                                                })]
                                                            })]
                                                        })
                                                    })]
                                                })]
                                            }), li && ui((0, X.jsxs)(`div`, {
                                                className: `full-content`,
                                                children: [(0, X.jsxs)(`div`, {
                                                    className: `title`,
                                                    children: [(0, X.jsxs)(`div`, {
                                                        className: `title-left`,
                                                        children: [(0, X.jsx)(`span`, {
                                                            className: `title-clickable`,
                                                            onClick: () => {
                                                                nr({skipFade: !0})
                                                            },
                                                            onMouseEnter: Mr,
                                                            children: `Участники`
                                                        }), (0, X.jsx)(`span`, {className: `arrow-right`}), (0, X.jsx)(`span`, {
                                                            className: `title-statistics-current-player`,
                                                            children: `Статистика`
                                                        })]
                                                    }), (0, X.jsxs)(`div`, {
                                                        className: `title-right`,
                                                        onClick: () => {
                                                            Lr(), nr({skipFade: !0})
                                                        },
                                                        onMouseEnter: Ir,
                                                        children: [(0, X.jsx)(`span`, {className: `arrow-left`}), (0, X.jsx)(`span`, {children: `Назад`}), (0, X.jsx)(q, {
                                                            button: `B`,
                                                            className: `back-btn-gp-hint`
                                                        })]
                                                    })]
                                                }), (0, X.jsx)(ao, {
                                                    gamepadScroll: !0,
                                                    children: (0, X.jsx)(Tf, {onPlayerHistoryClick: gn, playersList: A})
                                                })]
                                            }), si, si), Ne != null && !ce && (ai ? (0, X.jsx)(`div`, {
                                                className: `mobile-swipe-foreground`,
                                                onTransitionEnd: ri,
                                                children: (0, X.jsxs)(`div`, {
                                                    className: `full-content`, children: [(0, X.jsxs)(`div`, {
                                                        className: `title`,
                                                        children: [(0, X.jsx)(`div`, {
                                                            className: `title-left`,
                                                            children: r ? (0, X.jsxs)(X.Fragment, {
                                                                children: [(0, X.jsx)(`span`, {
                                                                    className: `title-clickable`,
                                                                    onClick: () => {
                                                                        nr({skipFade: !0})
                                                                    },
                                                                    onMouseEnter: Mr,
                                                                    children: `Участники`
                                                                }), St && (0, X.jsxs)(X.Fragment, {
                                                                    children: [(0, X.jsx)(`span`, {className: `arrow-right`}), (0, X.jsx)(`span`, {
                                                                        className: `title-clickable`,
                                                                        onClick: () => {
                                                                            Nr(), qr()
                                                                        },
                                                                        onMouseEnter: Mr,
                                                                        children: `Статистика`
                                                                    })]
                                                                }), (0, X.jsx)(`span`, {className: `arrow-right`}), (0, X.jsx)(Zu, {
                                                                    label: Ne.name,
                                                                    items: Rn,
                                                                    selectedId: Ln(Ne),
                                                                    onSelect: ur,
                                                                    onHeadClick: Nr,
                                                                    onHeadHover: () => l(f.Menu_Head_Hover)
                                                                })]
                                                            }) : (0, X.jsxs)(X.Fragment, {
                                                                children: [(0, X.jsx)(`span`, {
                                                                    className: `title-clickable`,
                                                                    onClick: () => {
                                                                        nr({skipFade: !0})
                                                                    },
                                                                    onMouseEnter: Mr,
                                                                    children: `Участники`
                                                                }), (0, X.jsx)(`span`, {className: `arrow-right`}), (0, X.jsx)(`span`, {
                                                                    className: `title-clickable`,
                                                                    onClick: () => {
                                                                        Nr(), Kr()
                                                                    },
                                                                    onMouseEnter: Mr,
                                                                    children: `Статистика`
                                                                }), (0, X.jsx)(`span`, {className: `arrow-right`}), (0, X.jsx)(`span`, {
                                                                    className: `title-active`,
                                                                    children: Ne.name
                                                                })]
                                                            })
                                                        }), (0, X.jsxs)(`div`, {
                                                            className: `title-right`,
                                                            onClick: () => {
                                                                Lr(), qr()
                                                            },
                                                            onMouseEnter: Ir,
                                                            children: [(0, X.jsx)(`span`, {className: `arrow-left`}), (0, X.jsx)(`span`, {children: `Назад`}), (0, X.jsx)(q, {
                                                                button: `B`,
                                                                className: `back-btn-gp-hint`
                                                            })]
                                                        })]
                                                    }), (0, X.jsx)(xp, {
                                                        player: Ne,
                                                        initialPlace: j(Ne.id),
                                                        showStatistics: hn,
                                                        openedFromStatistics: St,
                                                        openedFromGameCard: wt,
                                                        backToTopScreen: bn,
                                                        backToGameCard: yn,
                                                        selfPlayer: G ?? void 0
                                                    })]
                                                })
                                            }) : (0, X.jsxs)(`div`, {
                                                className: `full-content`,
                                                children: [(0, X.jsxs)(`div`, {
                                                    className: `title`,
                                                    children: [(0, X.jsxs)(`div`, {
                                                        className: `title-left`,
                                                        children: [(0, X.jsx)(`span`, {
                                                            className: `title-clickable`,
                                                            onClick: () => {
                                                                nr({skipFade: !0})
                                                            },
                                                            onMouseEnter: Mr,
                                                            children: `Участники`
                                                        }), (0, X.jsx)(`span`, {className: `arrow-right`}), (0, X.jsx)(`span`, {
                                                            className: `title-clickable`,
                                                            onClick: () => {
                                                                Nr(), Kr()
                                                            },
                                                            onMouseEnter: Mr,
                                                            children: `Статистика`
                                                        }), (0, X.jsx)(`span`, {className: `arrow-right`}), (0, X.jsx)(`span`, {
                                                            className: `title-active`,
                                                            children: Ne.name
                                                        })]
                                                    }), (0, X.jsxs)(`div`, {
                                                        className: `title-right`,
                                                        onClick: () => {
                                                            Lr(), qr()
                                                        },
                                                        onMouseEnter: Ir,
                                                        children: [(0, X.jsx)(`span`, {className: `arrow-left`}), (0, X.jsx)(`span`, {children: `Назад`}), (0, X.jsx)(q, {
                                                            button: `B`,
                                                            className: `back-btn-gp-hint`
                                                        })]
                                                    })]
                                                }), (0, X.jsx)(xp, {
                                                    player: Ne,
                                                    initialPlace: j(Ne.id),
                                                    showStatistics: hn,
                                                    openedFromStatistics: St,
                                                    openedFromGameCard: wt,
                                                    backToTopScreen: bn,
                                                    backToGameCard: yn,
                                                    selfPlayer: G ?? void 0
                                                })]
                                            })), ce && (0, X.jsxs)(`div`, {
                                                className: `full-content`,
                                                children: [(0, X.jsxs)(`div`, {
                                                    className: `title`,
                                                    children: [(0, X.jsx)(`div`, {
                                                        className: `title-left`,
                                                        children: (0, X.jsx)(`span`, {
                                                            className: `title-active`,
                                                            children: `Итоги`
                                                        })
                                                    }), (0, X.jsxs)(`div`, {
                                                        className: `title-right`,
                                                        onClick: Jr,
                                                        onMouseEnter: Ir,
                                                        children: [(0, X.jsx)(`span`, {className: `arrow-left`}), (0, X.jsx)(`span`, {children: `Назад`}), (0, X.jsx)(q, {
                                                            button: `B`,
                                                            className: `back-btn-gp-hint`
                                                        })]
                                                    })]
                                                }), (0, X.jsx)(ah, {players: A})]
                                            }), F == $.CREDITS && !R && !ce && Ne == null && (0, X.jsxs)(`div`, {
                                                className: `full-content`,
                                                children: [(0, X.jsx)(`div`, {
                                                    className: `title`,
                                                    children: `Создатели`
                                                }), (0, X.jsx)(Im, {})]
                                            })]
                                        })]
                                    }), (0, X.jsx)($p, {})]
                                }), fe && (0, X.jsx)(`button`, {
                                    type: `button`,
                                    className: `laptop-result-button${pe ? ` laptop-result-button--visible` : ``}`,
                                    onClick: Yr,
                                    onMouseEnter: Mr,
                                    "aria-label": `Итоги`,
                                    children: (0, X.jsx)(`video`, {
                                        src: Fh,
                                        autoPlay: !0,
                                        loop: !0,
                                        muted: !0,
                                        playsInline: !0
                                    })
                                }), Ph && !r ? (0, X.jsx)(`img`, {
                                    src: Ph,
                                    alt: ``,
                                    className: `laptop-screen-overlay`,
                                    "aria-hidden": !0
                                }) : null]
                            })]
                        }), Tn === !0 && (0, X.jsx)(`button`, {
                            type: `button`,
                            className: `casino-sign`,
                            onClick: jr,
                            onMouseEnter: e => {
                                Ar && l(f.Menu_Casino_Hover, {panElement: e.currentTarget})
                            },
                            "aria-label": `Открыть казино`,
                            children: Ar && (0, X.jsx)(q, {button: `LT`, className: `casino-sign-gp-hint`})
                        }), V && (0, X.jsx)(bu, {fadeOut: he, onContinue: mn, isAuthenticated: Et, introBootReady: Ge})]
                    })]
                })]
            }), i && (0, X.jsx)(hh, {forceHidden: V || Mt || nn}), !r && (0, X.jsx)(Ch, {active: $t && tn})]
        })
    };
export {zh as default};