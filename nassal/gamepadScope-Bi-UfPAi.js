var e = Object.create, t = Object.defineProperty, n = Object.getOwnPropertyDescriptor, r = Object.getOwnPropertyNames,
    i = Object.getPrototypeOf, a = Object.prototype.hasOwnProperty,
    o = (e, t) => () => (t || e((t = {exports: {}}).exports, t), t.exports), s = (e, n) => {
        let r = {};
        for (var i in e) t(r, i, {get: e[i], enumerable: !0});
        return n || t(r, Symbol.toStringTag, {value: `Module`}), r
    }, c = (e, i, o, s) => {
        if (i && typeof i == `object` || typeof i == `function`) for (var c = r(i), l = 0, u = c.length, d; l < u; l++) d = c[l], !a.call(e, d) && d !== o && t(e, d, {
            get: (e => i[e]).bind(null, d),
            enumerable: !(s = n(i, d)) || s.enumerable
        });
        return e
    }, l = (n, r, a) => (a = n == null ? {} : e(i(n)), c(r || !n || !n.__esModule ? t(a, `default`, {
        value: n,
        enumerable: !0
    }) : a, n)), u = o((e => {
        var t = Symbol.for(`react.transitional.element`), n = Symbol.for(`react.portal`), r = Symbol.for(`react.fragment`),
            i = Symbol.for(`react.strict_mode`), a = Symbol.for(`react.profiler`), o = Symbol.for(`react.consumer`),
            s = Symbol.for(`react.context`), c = Symbol.for(`react.forward_ref`), l = Symbol.for(`react.suspense`),
            u = Symbol.for(`react.memo`), d = Symbol.for(`react.lazy`), ee = Symbol.for(`react.activity`),
            f = Symbol.iterator;

        function p(e) {
            return typeof e != `object` || !e ? null : (e = f && e[f] || e[`@@iterator`], typeof e == `function` ? e : null)
        }

        var m = {
            isMounted: function () {
                return !1
            }, enqueueForceUpdate: function () {
            }, enqueueReplaceState: function () {
            }, enqueueSetState: function () {
            }
        }, h = Object.assign, g = {};

        function _(e, t, n) {
            this.props = e, this.context = t, this.refs = g, this.updater = n || m
        }

        _.prototype.isReactComponent = {}, _.prototype.setState = function (e, t) {
            if (typeof e != `object` && typeof e != `function` && e != null) throw Error(`takes an object of state variables to update or a function which returns an object of state variables.`);
            this.updater.enqueueSetState(this, e, t, `setState`)
        }, _.prototype.forceUpdate = function (e) {
            this.updater.enqueueForceUpdate(this, e, `forceUpdate`)
        };

        function v() {
        }

        v.prototype = _.prototype;

        function y(e, t, n) {
            this.props = e, this.context = t, this.refs = g, this.updater = n || m
        }

        var te = y.prototype = new v;
        te.constructor = y, h(te, _.prototype), te.isPureReactComponent = !0;
        var ne = Array.isArray;

        function b() {
        }

        var x = {H: null, A: null, T: null, S: null}, re = Object.prototype.hasOwnProperty;

        function S(e, n, r) {
            var i = r.ref;
            return {$$typeof: t, type: e, key: n, ref: i === void 0 ? null : i, props: r}
        }

        function ie(e, t) {
            return S(e.type, t, e.props)
        }

        function C(e) {
            return typeof e == `object` && !!e && e.$$typeof === t
        }

        function w(e) {
            var t = {"=": `=0`, ":": `=2`};
            return `$` + e.replace(/[=:]/g, function (e) {
                return t[e]
            })
        }

        var T = /\/+/g;

        function E(e, t) {
            return typeof e == `object` && e && e.key != null ? w(`` + e.key) : t.toString(36)
        }

        function ae(e) {
            switch (e.status) {
                case`fulfilled`:
                    return e.value;
                case`rejected`:
                    throw e.reason;
                default:
                    switch (typeof e.status == `string` ? e.then(b, b) : (e.status = `pending`, e.then(function (t) {
                        e.status === `pending` && (e.status = `fulfilled`, e.value = t)
                    }, function (t) {
                        e.status === `pending` && (e.status = `rejected`, e.reason = t)
                    })), e.status) {
                        case`fulfilled`:
                            return e.value;
                        case`rejected`:
                            throw e.reason
                    }
            }
            throw e
        }

        function D(e, r, i, a, o) {
            var s = typeof e;
            (s === `undefined` || s === `boolean`) && (e = null);
            var c = !1;
            if (e === null) c = !0; else switch (s) {
                case`bigint`:
                case`string`:
                case`number`:
                    c = !0;
                    break;
                case`object`:
                    switch (e.$$typeof) {
                        case t:
                        case n:
                            c = !0;
                            break;
                        case d:
                            return c = e._init, D(c(e._payload), r, i, a, o)
                    }
            }
            if (c) return o = o(e), c = a === `` ? `.` + E(e, 0) : a, ne(o) ? (i = ``, c != null && (i = c.replace(T, `$&/`) + `/`), D(o, r, i, ``, function (e) {
                return e
            })) : o != null && (C(o) && (o = ie(o, i + (o.key == null || e && e.key === o.key ? `` : (`` + o.key).replace(T, `$&/`) + `/`) + c)), r.push(o)), 1;
            c = 0;
            var l = a === `` ? `.` : a + `:`;
            if (ne(e)) for (var u = 0; u < e.length; u++) a = e[u], s = l + E(a, u), c += D(a, r, i, s, o); else if (u = p(e), typeof u == `function`) for (e = u.call(e), u = 0; !(a = e.next()).done;) a = a.value, s = l + E(a, u++), c += D(a, r, i, s, o); else if (s === `object`) {
                if (typeof e.then == `function`) return D(ae(e), r, i, a, o);
                throw r = String(e), Error(`Objects are not valid as a React child (found: ` + (r === `[object Object]` ? `object with keys {` + Object.keys(e).join(`, `) + `}` : r) + `). If you meant to render a collection of children, use an array instead.`)
            }
            return c
        }

        function O(e, t, n) {
            if (e == null) return e;
            var r = [], i = 0;
            return D(e, r, ``, ``, function (e) {
                return t.call(n, e, i++)
            }), r
        }

        function oe(e) {
            if (e._status === -1) {
                var t = e._result;
                t = t(), t.then(function (t) {
                    (e._status === 0 || e._status === -1) && (e._status = 1, e._result = t)
                }, function (t) {
                    (e._status === 0 || e._status === -1) && (e._status = 2, e._result = t)
                }), e._status === -1 && (e._status = 0, e._result = t)
            }
            if (e._status === 1) return e._result.default;
            throw e._result
        }

        var k = typeof reportError == `function` ? reportError : function (e) {
            if (typeof window == `object` && typeof window.ErrorEvent == `function`) {
                var t = new window.ErrorEvent(`error`, {
                    bubbles: !0,
                    cancelable: !0,
                    message: typeof e == `object` && e && typeof e.message == `string` ? String(e.message) : String(e),
                    error: e
                });
                if (!window.dispatchEvent(t)) return
            } else if (typeof process == `object` && typeof process.emit == `function`) {
                process.emit(`uncaughtException`, e);
                return
            }
            console.error(e)
        }, se = {
            map: O, forEach: function (e, t, n) {
                O(e, function () {
                    t.apply(this, arguments)
                }, n)
            }, count: function (e) {
                var t = 0;
                return O(e, function () {
                    t++
                }), t
            }, toArray: function (e) {
                return O(e, function (e) {
                    return e
                }) || []
            }, only: function (e) {
                if (!C(e)) throw Error(`React.Children.only expected to receive a single React element child.`);
                return e
            }
        };
        e.Activity = ee, e.Children = se, e.Component = _, e.Fragment = r, e.Profiler = a, e.PureComponent = y, e.StrictMode = i, e.Suspense = l, e.__CLIENT_INTERNALS_DO_NOT_USE_OR_WARN_USERS_THEY_CANNOT_UPGRADE = x, e.__COMPILER_RUNTIME = {
            __proto__: null,
            c: function (e) {
                return x.H.useMemoCache(e)
            }
        }, e.cache = function (e) {
            return function () {
                return e.apply(null, arguments)
            }
        }, e.cacheSignal = function () {
            return null
        }, e.cloneElement = function (e, t, n) {
            if (e == null) throw Error(`The argument must be a React element, but you passed ` + e + `.`);
            var r = h({}, e.props), i = e.key;
            if (t != null) for (a in t.key !== void 0 && (i = `` + t.key), t) !re.call(t, a) || a === `key` || a === `__self` || a === `__source` || a === `ref` && t.ref === void 0 || (r[a] = t[a]);
            var a = arguments.length - 2;
            if (a === 1) r.children = n; else if (1 < a) {
                for (var o = Array(a), s = 0; s < a; s++) o[s] = arguments[s + 2];
                r.children = o
            }
            return S(e.type, i, r)
        }, e.createContext = function (e) {
            return e = {
                $$typeof: s,
                _currentValue: e,
                _currentValue2: e,
                _threadCount: 0,
                Provider: null,
                Consumer: null
            }, e.Provider = e, e.Consumer = {$$typeof: o, _context: e}, e
        }, e.createElement = function (e, t, n) {
            var r, i = {}, a = null;
            if (t != null) for (r in t.key !== void 0 && (a = `` + t.key), t) re.call(t, r) && r !== `key` && r !== `__self` && r !== `__source` && (i[r] = t[r]);
            var o = arguments.length - 2;
            if (o === 1) i.children = n; else if (1 < o) {
                for (var s = Array(o), c = 0; c < o; c++) s[c] = arguments[c + 2];
                i.children = s
            }
            if (e && e.defaultProps) for (r in o = e.defaultProps, o) i[r] === void 0 && (i[r] = o[r]);
            return S(e, a, i)
        }, e.createRef = function () {
            return {current: null}
        }, e.forwardRef = function (e) {
            return {$$typeof: c, render: e}
        }, e.isValidElement = C, e.lazy = function (e) {
            return {$$typeof: d, _payload: {_status: -1, _result: e}, _init: oe}
        }, e.memo = function (e, t) {
            return {$$typeof: u, type: e, compare: t === void 0 ? null : t}
        }, e.startTransition = function (e) {
            var t = x.T, n = {};
            x.T = n;
            try {
                var r = e(), i = x.S;
                i !== null && i(n, r), typeof r == `object` && r && typeof r.then == `function` && r.then(b, k)
            } catch (e) {
                k(e)
            } finally {
                t !== null && n.types !== null && (t.types = n.types), x.T = t
            }
        }, e.unstable_useCacheRefresh = function () {
            return x.H.useCacheRefresh()
        }, e.use = function (e) {
            return x.H.use(e)
        }, e.useActionState = function (e, t, n) {
            return x.H.useActionState(e, t, n)
        }, e.useCallback = function (e, t) {
            return x.H.useCallback(e, t)
        }, e.useContext = function (e) {
            return x.H.useContext(e)
        }, e.useDebugValue = function () {
        }, e.useDeferredValue = function (e, t) {
            return x.H.useDeferredValue(e, t)
        }, e.useEffect = function (e, t) {
            return x.H.useEffect(e, t)
        }, e.useEffectEvent = function (e) {
            return x.H.useEffectEvent(e)
        }, e.useId = function () {
            return x.H.useId()
        }, e.useImperativeHandle = function (e, t, n) {
            return x.H.useImperativeHandle(e, t, n)
        }, e.useInsertionEffect = function (e, t) {
            return x.H.useInsertionEffect(e, t)
        }, e.useLayoutEffect = function (e, t) {
            return x.H.useLayoutEffect(e, t)
        }, e.useMemo = function (e, t) {
            return x.H.useMemo(e, t)
        }, e.useOptimistic = function (e, t) {
            return x.H.useOptimistic(e, t)
        }, e.useReducer = function (e, t, n) {
            return x.H.useReducer(e, t, n)
        }, e.useRef = function (e) {
            return x.H.useRef(e)
        }, e.useState = function (e) {
            return x.H.useState(e)
        }, e.useSyncExternalStore = function (e, t, n) {
            return x.H.useSyncExternalStore(e, t, n)
        }, e.useTransition = function () {
            return x.H.useTransition()
        }, e.version = `19.2.5`
    })), d = o(((e, t) => {
        t.exports = u()
    })), ee = o((e => {
        var t = Symbol.for(`react.transitional.element`), n = Symbol.for(`react.fragment`);

        function r(e, n, r) {
            var i = null;
            if (r !== void 0 && (i = `` + r), n.key !== void 0 && (i = `` + n.key), `key` in n) for (var a in r = {}, n) a !== `key` && (r[a] = n[a]); else r = n;
            return n = r.ref, {$$typeof: t, type: e, key: i, ref: n === void 0 ? null : n, props: r}
        }

        e.Fragment = n, e.jsx = r, e.jsxs = r
    })), f = o(((e, t) => {
        t.exports = ee()
    })), p = l(d(), 1), m = (0, p.createContext)(void 0), h = `/assets/Letter_15-BYT5d7pr.webp`,
    g = `/assets/Letter_19-D4oI_QO6.webp`, _ = `/assets/letter_open-BWJHPivW.webp`,
    v = `/assets/letter_button-DgfpPnXp.webp`, y = `/assets/letter_button_viewer-yOGPL0Yb.webp`,
    te = `/assets/evening-BIINJgfM.webm`, ne = Object.assign({
        "./sounds/AMB_Casino_Closed.flac": `/assets/AMB_Casino_Closed-CmnEDDCK.flac`,
        "./sounds/AMB_Casino_Open.flac": `/assets/AMB_Casino_Open-ALHwmWBe.flac`,
        "./sounds/AMB_Loop_Laptop.flac": `/assets/AMB_Loop_Laptop-x5quB14q.flac`,
        "./sounds/AMB_Menu_Day.flac": `/assets/AMB_Menu_Day-Dfuhz7jH.flac`,
        "./sounds/AMB_Menu_Evening.flac": `/assets/AMB_Menu_Evening-D95RzGK9.flac`,
        "./sounds/AMB_Menu_Morning.flac": `/assets/AMB_Menu_Morning-BuKa6GWl.flac`,
        "./sounds/AMB_Menu_Night.flac": `/assets/AMB_Menu_Night-r_nn0zJ5.flac`,
        "./sounds/AMB_Start_Laptop.flac": `/assets/AMB_Start_Laptop-Gbjvf4Tr.flac`,
        "./sounds/Button_Participants_Hover.flac": `data:audio/flac;base64,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`,
        "./sounds/Casino_Button_Click.flac": `data:audio/flac;base64,ZkxhQwAAACIQABAAAAUUAAUUCsRC8AAAA2ogCGyhQyw32NRE0eczV+D1hAAADgYAAABSRUFQRVIAAAAA//h5iAADaXcQAqAEEYgxJiXESfE9IsS4mNKWkN3ZF0etNASiWhNLykRI0BiOf2TEGyHZnewX2Vle799U8xYmz7VOz1Gmz05RN1oesZWdQm8eFKy5tZu2SoKW3HBRWsL01BCLQLLAJKbEyqNLEKlXhqYpN+YOpYiJBp0jr4lDpEZfTXrUVLcWUomscEVWFcH2DeOehKYKqEyiZRG0dNL4+BdrA4y9CjWxsrT42agdTuXgZOM/gMQH5B84RPM791VHUXOD02C1x+ou2BBMaxGEtbFl8DDmQK9E7jUYPzg3CjuDCADeQEykGcs7/RP3qjbYJA4F5jN4/y6g82rwamCjOfkeJMeqFAWwVIjtQBSgpECoofRODxJDmv+j3H1k4sYgjeSl3MZ2FQGOy181xf7ULjaj/HD8Rqgj3GAymCGblLQQ3o3fNEqcUVDa5ZQWOBwyoi6bY/IAUYe0SsLcn9X4YonCfw30Ri2E1ogqLD7NPpJJYjFrEvqCGLDLRrFBJ/0JL2NpOoMTKY/QZtkXzhXGE0h+QPj9cGLnw0pXE6Bc4ubf5ZKJjaK5pOLnw9225tRF0xjB4kZKdB5kfmKtiEVx7940mLOIu9gQv4MKQjzKguY/X50tIfyyGzni1rF7DEjDQ9lZNby70limtdmoTQMn6XESWxK6LZ4ZpDC1vJCKolGU24/eX+IRGq1RCTfbyO9FRB6Fv717WD4Lwjda+7h6nMMnPmBSCcT0St8rbLKXDZ5U5uVUqoVfoZOcTHbjwlu1sDvGLd4oZoScca4QyVPIZoxIGbjh7XXjnBkSCuEGJmYIJOtJkb8SNrRYmzgmC2XWLuZW3KHjE6WNkfAKodIdkMsOVuIw/Tdl1A9qMjT4PaBQpBLJDPd/hfzmrYoouMaodniB0QrgtNSCleXUOSyKVN1mbumLvfykcJif0dbqDJ/ljAU/0MZVIuUKZDYwfYIYolqm6j4ZCoh5GhRODtEFQlqRIMqvDtyBSBHMq7n2JBAVr2asosVKQdvg33ilN8Oqy3YzQB3wrlkK5sRZmYQ8VCrVnFku9EwcvDP9TSfdbKretxddzV0CSGAwNVxhjv0008VT6LV7s9IWc0eljKLJJx0pniHVXevEJ3YKweRjvem3xW2qHNpEaXT6inKH7KP/3UDRfuTh6E8hkcca2i69L0luu7FJNDTVS5DLZQKgdW/Li/VIWRuHmRZj4QdIUsjDJ2bkadzxd5UoApxhNQ+ZBBCcB2dSmLtTcwejwWX+YQwpFGFzA5V1mO/VLPj5IyX/DuLKce9Y2/jOhF5LAFblSMnSh1OqrDO66vt2tfSmgNPO1CnBW65WD5MR86lh7XUqduZyIH7H3mv3I8lU8BpTSb5Ksrtsa0oG7nMIq6b6b1mKuPraWsjHbHA9Z+B8HgYn9zS6uSe2/NSly7VuX/aNnXi0giTJnQjBjk20WW3Z877y08jZxj92rktVD2P50fElNxKcludv1+7dDWylkIId2nCzTd+7U4qKomtWxz/9x6DkwEIEO3Z0UYH+uS8yB4SPTF8ub7dqWnap8HP10JMio949HRNzNfyws9k8MTLi//k9EGTt/qadQQi9FwqT6k6c8udtNhaE3qjPGmwGIYV6CnNZ/wKeOHaeW1z7tpMbw2D99zNvjFOfMlY3pA0dvxtab0ddLVElnHmSkvcAUIm0pRjDMcaLhUb8viGuto4VwmqFWCm0cIcRcg9AOIUj4dgAAAAq4A==`,
        "./sounds/Casino_Button_Hover.flac": `data:audio/flac;base64,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Yq74bqINbptLKbMtL4Y7No+5csvLpZZ7YbNsbprKLoLYppKa9Jpc7745IYpksMP2sk8cN1F8kMM2ksccU9SqKqW3a7CvyOfuqipCk463zxnq8hnl21vujkit3jml5kmsmkl7ji2y1rx47p14pqiTXy48QwYz7VXX8R182TVTxRSa1SXc28zoqa1Pl8NsE8k1Gu/OWMkl08seE2GFktMqKmHHqWSDDG+uGGS2mWWOCCCCKSHGKuOCjDKmWeWuKOnOSKqSefOCKiLmyWKKiiifGGyeGayee2uSuCaeCCHGyCCKOOe7W2/aGK2S+K6WeWSCOSiiCOeKSKGmaiy2ySXqCfeCjSZ7BIRWoJXLT+BQscu2xi4UPTyKM5DA5etsMaRY/uxiQwQJVlmE8NFXaqOm1JJLnRl4lypj7WWSK0kM11EecGcnFss9FuF33skO1ukPveV21O02G8ceFcs8P08N+GMs1lE08N9OUEdNu0U29ksMUtM9kUMslkcE0UcsEsUlMEk0dFVUNs8vUdUdMcNE+mFHcMGFkE0UEsMXEE0t9kcNXkKNJNRLhFNJhbPT1TBPVPJdDDTfdhZfpZJbHRRFDBHJJZZVLVFFTFDDXFR1LVDNBVldvHTpDdNL9FJbpFVNjPH1nBEitn10vlpy3jhSQSY0UTQQUTQQQS0wSUSSyw4UVR1wVTSVwSwTYVRWxx0U0Q0xxwQxSwYSWyxVSRVxUXTQ0SURySQ2RQz1QUT22XUXyR0YzVYYzQTRzRWVwRywyWQ0004VWRWzwXYT0QQTTSTUUVWTxSUWTR3xTZZ2XdTzzxaQxXwyw1VywzQxTSWwxWT0QSRwRVVSSQzSxwWQ0QTRQQQSSQQxSQxQQyQxCAA1VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVKqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqjx9w==`,
        "./sounds/Casino_Dealer_Faker_1.flac": `/assets/Casino_Dealer_Faker_1-DNRUfiDA.flac`,
        "./sounds/Casino_Dealer_Faker_2.flac": `/assets/Casino_Dealer_Faker_2-CJXzpQDr.flac`,
        "./sounds/Casino_Dealer_Faker_3.flac": `/assets/Casino_Dealer_Faker_3-BDJV3kBQ.flac`,
        "./sounds/Casino_Dealer_Faker_Idle.flac": `/assets/Casino_Dealer_Faker_Idle-rhmZ5C9U.flac`,
        "./sounds/Casino_Dealer_Faker_Roll.flac": `/assets/Casino_Dealer_Faker_Roll-BYzghHOK.flac`,
        "./sounds/Casino_Dealer_Ozon_1.flac": `/assets/Casino_Dealer_Ozon_1-C5icjdDe.flac`,
        "./sounds/Casino_Dealer_Ozon_2.flac": `/assets/Casino_Dealer_Ozon_2-CJHD6Ocg.flac`,
        "./sounds/Casino_Dealer_Ozon_3.flac": `/assets/Casino_Dealer_Ozon_3-C6VYHdui.flac`,
        "./sounds/Casino_Dealer_Ozon_Idle.flac": `/assets/Casino_Dealer_Ozon_Idle-yru5_uDL.flac`,
        "./sounds/Casino_Dealer_Ozon_Roll.flac": `/assets/Casino_Dealer_Ozon_Roll-DLWs-wQT.flac`,
        "./sounds/Casino_Dealer_Rin_0.flac": `/assets/Casino_Dealer_Rin_0-BT0zOa6f.flac`,
        "./sounds/Casino_Dealer_Rin_1.flac": `/assets/Casino_Dealer_Rin_1-D_PmMv0U.flac`,
        "./sounds/Casino_Dealer_Rin_2.flac": `/assets/Casino_Dealer_Rin_2-DMguJxWy.flac`,
        "./sounds/Casino_Dealer_Rin_4.flac": `/assets/Casino_Dealer_Rin_4-8pKhmjuu.flac`,
        "./sounds/Casino_Dealer_Rin_5.flac": `/assets/Casino_Dealer_Rin_5-DJZADY1R.flac`,
        "./sounds/Casino_Dealer_Rin_6.flac": `/assets/Casino_Dealer_Rin_6-A0RbEGHz.flac`,
        "./sounds/Casino_Dealer_Shev_0.flac": `/assets/Casino_Dealer_Shev_0-GgINsRPy.flac`,
        "./sounds/Casino_Dealer_Shev_1.flac": `/assets/Casino_Dealer_Shev_1-CDRgUTPQ.flac`,
        "./sounds/Casino_Dealer_Shev_2.flac": `/assets/Casino_Dealer_Shev_2-BkLYDFPc.flac`,
        "./sounds/Casino_Dealer_Shev_3.flac": `/assets/Casino_Dealer_Shev_3-BCCa8A56.flac`,
        "./sounds/Casino_Wheel_End (ver. 2).flac": `/assets/Casino_Wheel_End%20(ver.%202)-Bqdpqulw.flac`,
        "./sounds/Casino_Wheel_End.flac": `/assets/Casino_Wheel_End-CUwG43-E.flac`,
        "./sounds/Casino_Wheel_Item.flac": `data:audio/flac;base64,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`,
        "./sounds/Casino_Wheel_Start.flac": `/assets/Casino_Wheel_Start-OX5lNqu3.flac`,
        "./sounds/Casino_Wheel_Switch.flac": `data:audio/flac;base64,ZkxhQwAAACIQABAAAAcBAAcBCsRC8AAAAtmh1O5K5M0WvrnyvXESi6+OhAAADgYAAABSRUFQRVIAAAAA//h5qAAC2LJEAAAAPv51tViFmWzYECzAYIAAE3ABfAR8ASKrAAbt8wch1QeYsROQ0otuoNiIwAIKAigArQPIBuC1hX1Nop43Zb6yIMOBTwKkAZlrxNhcBMAV2mh3QSwYPc9sGd5txHPADCD7HyojNow77DOdOOFrjZOEggOoNGOEYEKpd09dhcRqElfKHfs1QRIp4exul81qTvJ6xOgZUSgHpFPKKXdzwTAIcHXw72g3VmBGBTVhPODUB6Dts7Jj8AFWAKw7IVqjglTALV+5qCUDwmPOXQuulwSFKLcGmGYEHBGhNgR7pmQ4fyeqbAZcr+ZmsjAOSY0EYmpiwRoWcRgIaCUKQkQWQM9SQNyRICLh2A1AAgIUEowAeQRg0d134KsAFq4CCndFMpCLhCRwxFBhnxARcSQFsH8PcAMQVsAx60hlY0AAnKKOQAAAZoB1QAAAPuAFpAsgABZBawsDWAiQAAxYWYA4wAbACFAEDGJV0EoW0CP9EAl5pACfACnXoA64IxFGAdIGMHK2oA2olgAEiAeAAAFNAISAECekTsAATMAvwFI28+AtYX5w0GF4LpxAm+BArgloAXspWuPwARkF85wP2gCRgD1ZUN+fUB3KcB+QB8wNEBRNGAs+sB0nhGKAbFoRAkRVhXri1S4h7gTkW0HUBOgPGAZ2BA2SCsSL/ngDrHpaNhwaBQj7mSIpEjFHTAhogo+UDAIGMb7AK0BrBmxw+gABWAqwDQkAZ9BLRwAg4tIQmcKCHiPWfMFCYICZApZLCPlBAxU1AHDDO3QFm1IwYJTAgBxEABayShIg7BLzVQIT8DVHBnMfXo/INKLcIIzKcjF6oEkOMacAesQ4GgKsswZPEi5E6OiAqeEyEGrJXRc1mGWmv3CAUweNHuCN4VniM70XI4AnfAeTE9E6ReMGD/U9aHeu0cMnkghxl6IM6upcI3IRARsjQdmTaMl9uFjaolexPqCpzlfJLYerGCGDTJuVGTieDOBERPSx5gfTIgPTyV+DYWkte6WIaomTnu/e53IxnTQC+xe4tiUZYNLsU0Oho8KiKRtlMGcBkX42XhMmV8g+v3MBftXbX2egoy7a8zU3mxhbMLnMBdn/5ghbAd6piXD7mqI4geCERC5E1l88FNeYxO72NpGUcnCzSy8JNFeOO3HJbEiPTI/iDevtwTEr6Z9nBYIusRO1r7H+PYPuj1dk/jCOsigPyEc+aSoNhJ1OfcZFfZh8InXy5oj3ROPb3StumaJGJc9IqpdPrxMmU+nLYu6HhAV1RATADogSAXV7CwAIDEvhZy7ApXlW8DU27SZ4ncrG4DeEwyxtemihfZOzyBWRWKEZ4nY25VCmjxBa+WTc4RWSQuyYVir0RMO10sJlGlKYZ1RpXO7z07RfvA71j61NvGYSWFSFEAAB//P/+1qp6vO4vAbrB18pkrnOpZLTPEq9qkOJOVosa/JjKljBPQZELBfRqVaNlkVFylFywLEG3KzVME2HZwdN3CkhQJlK9RgZIEOzUrWqkqDZjSLkGJZv30u/FvTZe2BktTtKf/t5Y0HBSl++dCNIoaLm2DBgSqmyVQtSYMbJuubqXCPxn0kjl0SLE+zG0YPVCfz7xb8uzFhYZEaDCiVrO+rDkW6ueneiUUG7erTteu7dt54nBM5SZN7RIxxYESdDgy7nzZWm1umjRM4YoNO7LiZPlSLG3bIFLByp6b+3Hl690vHatTdelBQ+NGDIsSVl5WZ+jpem4+vlDyoyta5sfOd+9YlRcvW6f5O2dtkFBQ7dWbHvTKtDN21ZoWujape23bt11atf/1y55cSjltyIlf3Ema9/GVStR7uDdqj97/1n73lpEySpEqUrIFzn1h2KkylsxcsdmbeRqFDlWg4+cGzK3WKkq/HpcrFWLVz4Z6E+Nny1Nj7evXTaZL1OHk7aJ1SfzhYcO2PTkTfsVKXNUPA5IkRBQfKaXxLM9nBPgVSU5HIzPxQfkFPSyu3tM2VD5ZFRwZuiEYQOCaFUkPYVFA5+/YN6VJDAcQTDiFKGAr0lnevZWeoIRSF8TSM0RwGUZQFsiGYCkQA4A3moKAOZuJYSgWAuhRHgwjtYTQI4xfAfQGKqFBZ6gqCIFUJo0p9E7AvhFBkAwqACyjg+bAZUpzxpA7GHUwqQCwAFMDR4JoBJ7LyFtC8TDISwN15oABWBZgZXQAwmE4ZhrTjZ7AjuJeDyJKM2CGAmMA71gGWwBjFMZQYyGASqRSJQJiiEwCaASQfgyAPikK4fTW/kofhoKwsJkPyEgCYD+AujjGfd4wzLdRbwwhDZAIcYQKZZTXvBZFpJWYo9UAugZnlZBr6vlCaCIQcGYXkoLTkN7plEYFl/2ZaUpaI4g2NxHsREJpJSVTzkumYcsunKyJ0=`,
        "./sounds/Easter_0b36e534b9057652e59e81d0573785a9c8961759a4ec50bdd4aac7c26264992a.flac": `/assets/Easter_0b36e534b9057652e59e81d0573785a9c8961759a4ec50bdd4aac7c26264992a-C5aOBVyL.flac`,
        "./sounds/Easter_0d0ce253e9897fd763e28d548af5edfc373085e3c5a6085eb2d41365b01bb80f.flac": `/assets/Easter_0d0ce253e9897fd763e28d548af5edfc373085e3c5a6085eb2d41365b01bb80f-DX6f_CTu.flac`,
        "./sounds/Easter_0fbd37ba491b8994454fa46058c2b47742048c69f92511c2a1f265c4fd386e49.flac": `/assets/Easter_0fbd37ba491b8994454fa46058c2b47742048c69f92511c2a1f265c4fd386e49-BDBhFJc-.flac`,
        "./sounds/Easter_13dd424f94fb6853ad8a31fc0b7aa53f6fb38e3a04d54afc3ea08378cf112bd7.flac": `/assets/Easter_13dd424f94fb6853ad8a31fc0b7aa53f6fb38e3a04d54afc3ea08378cf112bd7-Cj430owV.flac`,
        "./sounds/Easter_175e03ead6a287266ba5a066bc8170bf68db18c8e34bcd14af5e4d7c9f10dfe2.flac": `/assets/Easter_175e03ead6a287266ba5a066bc8170bf68db18c8e34bcd14af5e4d7c9f10dfe2-DP-yTT1B.flac`,
        "./sounds/Easter_1dfdd663ac38bf530e22374311edd6ee209086b740d13b076e94572692e4454d.flac": `/assets/Easter_1dfdd663ac38bf530e22374311edd6ee209086b740d13b076e94572692e4454d-BYgUUGGR.flac`,
        "./sounds/Easter_24686dd3fb0d9d1c30b668c2bd647c8b36fd88efa1dddc66d7a878908720e3ef.flac": `/assets/Easter_24686dd3fb0d9d1c30b668c2bd647c8b36fd88efa1dddc66d7a878908720e3ef-FTxN1kZO.flac`,
        "./sounds/Easter_25c163248ba46b4c305afd3bd4edf8dd8990d0f0eb2d267de16248fd47edb72c.flac": `/assets/Easter_25c163248ba46b4c305afd3bd4edf8dd8990d0f0eb2d267de16248fd47edb72c-KSaoJaGm.flac`,
        "./sounds/Easter_26ded0381cd50f5eb2dbd2954958d050b79bc9a6afd3f5fc356580f9510f4483.flac": `/assets/Easter_26ded0381cd50f5eb2dbd2954958d050b79bc9a6afd3f5fc356580f9510f4483-CL6hvCyB.flac`,
        "./sounds/Easter_279b97a7b07df410bea86430d2600e8ac627427c4d9a5febfde57c8d47e6b87f.flac": `/assets/Easter_279b97a7b07df410bea86430d2600e8ac627427c4d9a5febfde57c8d47e6b87f-tO4NGAEg.flac`,
        "./sounds/Easter_2d6a1f5865cc4bc0125b5d395b214687cefe5f13c2dacea26d58c7b53bc7d102.flac": `/assets/Easter_2d6a1f5865cc4bc0125b5d395b214687cefe5f13c2dacea26d58c7b53bc7d102-CSKFkr9a.flac`,
        "./sounds/Easter_32ed40a09c33c1f072e74fbed208523c6b5e04ff648e24cc96a9a008b50d5bd0.flac": `/assets/Easter_32ed40a09c33c1f072e74fbed208523c6b5e04ff648e24cc96a9a008b50d5bd0-BUebUEDa.flac`,
        "./sounds/Easter_412019e3b1ed17a3bc637aa4f10b03d89b42e59ab0afcbbffa608ead227200f5.flac": `/assets/Easter_412019e3b1ed17a3bc637aa4f10b03d89b42e59ab0afcbbffa608ead227200f5-Co0iBxi-.flac`,
        "./sounds/Easter_41cfc0d1f2d127b04555b7246d84019b4d27710a3f3aff6e7764375b1e06e05d.flac": `/assets/Easter_41cfc0d1f2d127b04555b7246d84019b4d27710a3f3aff6e7764375b1e06e05d-B56yFGBm.flac`,
        "./sounds/Easter_4247bff9d41c0f2da68ef43c5624531da9ca5bc31b39760a67e32265082e1ba8.flac": `/assets/Easter_4247bff9d41c0f2da68ef43c5624531da9ca5bc31b39760a67e32265082e1ba8-BXNZwvdO.flac`,
        "./sounds/Easter_49d180ecf56132819571bf39d9b7b342522a2ac6d23c1418d3338251bfe469c8.flac": `/assets/Easter_49d180ecf56132819571bf39d9b7b342522a2ac6d23c1418d3338251bfe469c8-CxNqd5Ck.flac`,
        "./sounds/Easter_4dea5c7cb70f50322ec9d734aa4aa078be9227c05251e18991c596f387552370.flac": `/assets/Easter_4dea5c7cb70f50322ec9d734aa4aa078be9227c05251e18991c596f387552370-GlHnFYMJ.flac`,
        "./sounds/Easter_4e4f57794c31f072d6aa11ba6b2f4022ecad5b71b626347c6353aa26a6906433.flac": `/assets/Easter_4e4f57794c31f072d6aa11ba6b2f4022ecad5b71b626347c6353aa26a6906433-B3R7IDLK.flac`,
        "./sounds/Easter_5223854359885b20565c171708980b834fb6b2bf2a7e12f46970fe720a8e6258.flac": `/assets/Easter_5223854359885b20565c171708980b834fb6b2bf2a7e12f46970fe720a8e6258-2xPt94IG.flac`,
        "./sounds/Easter_64d11827b9235ff89530b1643b99d3f645191d6b4a41f8e027a5ad2b0eed956b.flac": `/assets/Easter_64d11827b9235ff89530b1643b99d3f645191d6b4a41f8e027a5ad2b0eed956b-BK7YPPHW.flac`,
        "./sounds/Easter_6aa5e7e2c0abbd85be077826d4d1b899a661bd9b6fb4b687756a5220ac71018e.flac": `/assets/Easter_6aa5e7e2c0abbd85be077826d4d1b899a661bd9b6fb4b687756a5220ac71018e-BTs49GDg.flac`,
        "./sounds/Easter_775a87eb0e8e46586923dc6fb931c932f4dad28917fd8ed597f5b2421c290f53.flac": `/assets/Easter_775a87eb0e8e46586923dc6fb931c932f4dad28917fd8ed597f5b2421c290f53-B9bHLysN.flac`,
        "./sounds/Easter_78eddb33a6946b856887c8e036ede1479414335ad3fda504c7c7768d8b692d10.flac": `/assets/Easter_78eddb33a6946b856887c8e036ede1479414335ad3fda504c7c7768d8b692d10-T1-6sy4Q.flac`,
        "./sounds/Easter_80efbd0043a10cfdb73a3a75bc3674db81e9a79d24e1d30ed6f2e5a1f98e4c1e.flac": `/assets/Easter_80efbd0043a10cfdb73a3a75bc3674db81e9a79d24e1d30ed6f2e5a1f98e4c1e-2P3IKYWi.flac`,
        "./sounds/Easter_8630e351a30b8c297a1c811bf90de9657a33344e103ffc2a5c1f50a282a3ada5.flac": `/assets/Easter_8630e351a30b8c297a1c811bf90de9657a33344e103ffc2a5c1f50a282a3ada5-DbJd1wAr.flac`,
        "./sounds/Easter_88113be27aaf842777c30fb66dc60035a5c4e44a47520491905dc6fa833048b0.flac": `/assets/Easter_88113be27aaf842777c30fb66dc60035a5c4e44a47520491905dc6fa833048b0-CMg8asTS.flac`,
        "./sounds/Easter_8ce6a11414dfb44a396d535000244defdd3c18a059ccc4b1a30cc327c1cc9558.flac": `/assets/Easter_8ce6a11414dfb44a396d535000244defdd3c18a059ccc4b1a30cc327c1cc9558-CatKy4Nx.flac`,
        "./sounds/Easter_8d73898b303c21649db0df5b4cbc0a6af97f15d08f8da0050e095ef222c54738.flac": `/assets/Easter_8d73898b303c21649db0df5b4cbc0a6af97f15d08f8da0050e095ef222c54738-DYrBgh27.flac`,
        "./sounds/Easter_a0eb3ddfa5807780a562b9c313b2537f1e8dc621e9a524f8c1ffcf07a79e35c7.flac": `/assets/Easter_a0eb3ddfa5807780a562b9c313b2537f1e8dc621e9a524f8c1ffcf07a79e35c7-Bg53vRbf.flac`,
        "./sounds/Easter_a5620ebe49f40cff9b5f77fafeda84fe3c6d677cc22161e50f1431de7cebe68e.flac": `/assets/Easter_a5620ebe49f40cff9b5f77fafeda84fe3c6d677cc22161e50f1431de7cebe68e-B6uimsg6.flac`,
        "./sounds/Easter_a7be8e1fe282a37cd666e0632b17d933fa13f21addf4798fc0455bc166e2488c.flac": `/assets/Easter_a7be8e1fe282a37cd666e0632b17d933fa13f21addf4798fc0455bc166e2488c-Dk8EA3Zl.flac`,
        "./sounds/Easter_ab822805178670272818673637378cc5c291c91d569a89b9149afee16647d0d3.flac": `/assets/Easter_ab822805178670272818673637378cc5c291c91d569a89b9149afee16647d0d3-Cu8XRuFk.flac`,
        "./sounds/Easter_ae0c69138400ab6709515936298ba052111856859b1e25f4a05b00c081688ab3.flac": `/assets/Easter_ae0c69138400ab6709515936298ba052111856859b1e25f4a05b00c081688ab3-DB1cqLY-.flac`,
        "./sounds/Easter_b8643090fe2b7457542d320b2d21f664acc3ed38c2a591a0b07f83babad0a33e.flac": `/assets/Easter_b8643090fe2b7457542d320b2d21f664acc3ed38c2a591a0b07f83babad0a33e-qMSas-nH.flac`,
        "./sounds/Easter_bf511643f9ac25c6d6ec61e0af29cb561a0e6cdcd8afef273ab9fd2523f69cd5.flac": `/assets/Easter_bf511643f9ac25c6d6ec61e0af29cb561a0e6cdcd8afef273ab9fd2523f69cd5-_QLJjb0P.flac`,
        "./sounds/Easter_c4262fdf9ff247c7ea9a1a2dc4fc18d6d258cf98dc340c15c2e707be37f763cc.flac": `/assets/Easter_c4262fdf9ff247c7ea9a1a2dc4fc18d6d258cf98dc340c15c2e707be37f763cc-BJZOwhiz.flac`,
        "./sounds/Easter_c965e980d9656777756c0d42a1a8549c12bea4aca542a4c3c4c3a1d9457d56a3.flac": `/assets/Easter_c965e980d9656777756c0d42a1a8549c12bea4aca542a4c3c4c3a1d9457d56a3-FfBDj15z.flac`,
        "./sounds/Easter_ca35b55b825673c486aa4321bd92690ca053028ba6a5f03789086166c9c20287.flac": `/assets/Easter_ca35b55b825673c486aa4321bd92690ca053028ba6a5f03789086166c9c20287-9P529wjE.flac`,
        "./sounds/Easter_d2b41b5598353f4d937a9602a7d7de024533aba77f0214341e093fd4daa888b3.flac": `/assets/Easter_d2b41b5598353f4d937a9602a7d7de024533aba77f0214341e093fd4daa888b3-DIfpArEt.flac`,
        "./sounds/Easter_d36dc13d6fd2cbba62af2ed87cff175df0f2db4b3be7030ffeeb047c1213c83e.flac": `/assets/Easter_d36dc13d6fd2cbba62af2ed87cff175df0f2db4b3be7030ffeeb047c1213c83e-DlLC4tDF.flac`,
        "./sounds/Easter_e39119d6d64662662ba636641f1a4d5cdeb928b1a8ac8e607af5ff53164b27e3.flac": `/assets/Easter_e39119d6d64662662ba636641f1a4d5cdeb928b1a8ac8e607af5ff53164b27e3-DhyJ88Hy.flac`,
        "./sounds/Easter_e64320622579d32a9f5f4d9cfbdf090929d6aa9b3d9f7cf21df9713df0a21b3e.flac": `/assets/Easter_e64320622579d32a9f5f4d9cfbdf090929d6aa9b3d9f7cf21df9713df0a21b3e-CJZpi9Ch.flac`,
        "./sounds/Easter_ea8b157efab1fe7da57ba599835baed707b381c8398642aa6c47d3fa5430789c.flac": `/assets/Easter_ea8b157efab1fe7da57ba599835baed707b381c8398642aa6c47d3fa5430789c-By2ABAuO.flac`,
        "./sounds/Easter_ebcb41ac2b02d33b358e38967a3b9c05c84213c294038f469b14f95a87928c5a.flac": `/assets/Easter_ebcb41ac2b02d33b358e38967a3b9c05c84213c294038f469b14f95a87928c5a-DBZpdT__.flac`,
        "./sounds/Easter_ee4d9393b20ca612f40239f49d8205f5a47915637f75fe4296c538d8ae5ca230.flac": `/assets/Easter_ee4d9393b20ca612f40239f49d8205f5a47915637f75fe4296c538d8ae5ca230-cPxH0Ukl.flac`,
        "./sounds/Easter_f10d91a7596bf5a6773579ff1306afdc363b0be08602c768907c09261cad3a56.flac": `/assets/Easter_f10d91a7596bf5a6773579ff1306afdc363b0be08602c768907c09261cad3a56-BplDJym1.flac`,
        "./sounds/Easter_f1cfa5ebb149e8099d561aae57beed6c68f990f45a910ea9d7b460dbcc5350be.flac": `/assets/Easter_f1cfa5ebb149e8099d561aae57beed6c68f990f45a910ea9d7b460dbcc5350be-lRIJWam8.flac`,
        "./sounds/Easter_f37f3f2b0dc57a86dee4ba6ff855283bb4d2f0dea1c5bd1b708853444c2ffcec.flac": `/assets/Easter_f37f3f2b0dc57a86dee4ba6ff855283bb4d2f0dea1c5bd1b708853444c2ffcec-p8f8S2hP.flac`,
        "./sounds/Easter_f6b819bdd152dfb8b4c2f3d327d35c259a47cf9f1d0f31661287fb56404cf3f9.flac": `/assets/Easter_f6b819bdd152dfb8b4c2f3d327d35c259a47cf9f1d0f31661287fb56404cf3f9-Dd75nCA4.flac`,
        "./sounds/Easter_fab86cee21280793ed2c2b0ef159bc6f7ad0e2576a797bdb558c0bb4eee92f83.flac": `/assets/Easter_fab86cee21280793ed2c2b0ef159bc6f7ad0e2576a797bdb558c0bb4eee92f83-D7oW42ZV.flac`,
        "./sounds/Game_Done.flac": `/assets/Game_Done-DWBVcsd4.flac`,
        "./sounds/Game_Dropped (ver. 2).flac": `/assets/Game_Dropped%20(ver.%202)-CYkPwNFF.flac`,
        "./sounds/Game_Dropped.flac": `/assets/Game_Dropped-BFCX5qb2.flac`,
        "./sounds/Game_Finished.flac": `/assets/Game_Finished-49Od7OFk.flac`,
        "./sounds/Game_Pause.flac": `/assets/Game_Pause-aBRCe3l2.flac`,
        "./sounds/Game_Rerolled.flac": `/assets/Game_Rerolled-DhE_8J4V.flac`,
        "./sounds/Game_Started.flac": `/assets/Game_Started-u8z8ANY2.flac`,
        "./sounds/Game_Unpause.flac": `/assets/Game_Unpause-DDThSM1_.flac`,
        "./sounds/Menu_Button+Back_Click.flac": `data:audio/flac;base64,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`,
        "./sounds/Menu_Button+Back_Hover.flac": `data:audio/flac;base64,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`,
        "./sounds/Menu_Button_Participants_Click.flac": `data:audio/flac;base64,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`,
        "./sounds/Menu_Casino_Change.flac": `/assets/Menu_Casino_Change-BXHFRS2c.flac`,
        "./sounds/Menu_Casino_Click.flac": `/assets/Menu_Casino_Click-DB8-tA5s.flac`,
        "./sounds/Menu_Casino_Hover.flac": `/assets/Menu_Casino_Hover-Cq2k6EGM.flac`,
        "./sounds/Menu_Head_Click.flac": `data:audio/flac;base64,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`,
        "./sounds/Menu_Head_Hover.flac": `data:audio/flac;base64,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`,
        "./sounds/Menu_Item_Activated.flac": `/assets/Menu_Item_Activated-JtsZQ-SJ.flac`,
        "./sounds/Menu_Item_Hover.flac": `/assets/Menu_Item_Hover-Dyuiu_8p.flac`,
        "./sounds/Menu_Item_Next.flac": `/assets/Menu_Item_Next-QPMkCut-.flac`,
        "./sounds/Menu_Item_Press.flac": `/assets/Menu_Item_Press-BWinnCdV.flac`,
        "./sounds/Menu_Item_Unpress.flac": `data:audio/flac;base64,ZkxhQwAAACIQABAAAAAOAAbDCsRC8AAAVPzvKQaaHFELYEgl5RtWf39UhAAADgYAAABSRUFQRVIAAAAA//jJGADCFP/7//sEP/L8vl5fL8v/5f//+n/+n/T9P+n///+X////6f////////L//5f/L//L////p///0//0/9P/T/6f////y//L/L8vy/L8v8v//9P+n/T/p/T9Pp9Pp9P/T/8v/5fy/L5fLy8vL8v/lT/+n+n6f/L5csspSmnT/LLNKdOXLzppoXy5SmlPT/yoXQrDBgVyJ32VAQEcvsvTS/SlloRNNlLLl5cspTT+Up8pTLPlOU88pKeec+ZTKSnPPPPPPPNClJT58plnyn5SnllNP5cspSlKUpZZf/Sll6UvpfZaX333vvfdkS96VL33S999++l6WVNl00v9KUv/p0ppSlLKUpSlNKaafT8uWUpTp+WWU0/5ZSmn/LLNNP+WWUppp9Pl/LlllllKUpSlKU0ppp009P/8uWUpTp+XKU/8pp8s0+Up8pT8p+U/KaSzTlKflPyn+U/lKfllP+WU6f5cspSmlNNNNKaUsuX9NLL9KX6Uumyppel9L0vpdL6XSyJv2RNL0v0vSl9LLTsv6Usv+mmlKUpSllKaU6f8spp/KU/lNOUpD808pplnQspoXNPKU+Up/KaeWUp/yzSn/+XLLLKWWWXl/p0pSy/TS5adL/Sl+lL9KX9KX9KX+ml/9KUsv/6dNNKaUppSmmmnT//llKU6fy5SnTy5SmnyyzT/KUpp8uUpp/8spTTT//5csuWWWUssssuXLy//p6dKaUpZZZeX///0/p/0/5flyllKaaf/LKUpp/5SzTp5cspp/+WUppp/+XLKWUpSlKUpSllLLy/TpSly6aUv6Uv6X6UvSl9L9L9l6WWlL9LKmllTpf6WX6bLL+mmlLLLLy+XLllKUpp/yymnyyn8pT5SnlKfKfzoWU+U/KflPlKeUp+Up/KafLKU/+WUpp0/T///9PTSlKWX/TTZf+lKX/SlL/TSy/TSlL/ppSly/06aaUspZcuXl5eXlyyyylKUpp0//LllKU00//lyylKU0009P9P/6f6enTTSlLLL/+mlKXL+nSll/9KUpf/pSlLy+nppSlly/lp/09PT6fp/l8sspTTp+XKUp08uUp0+WUpp8spSn/LKadOXllKadP/5eXLLLLLLLl5f/TppSly/00pZf6aUv/TZctOmly/TSlLl/TTSlLLKKf/5//gIf/l+X5fL/L///////+n/p/p//p/8v//////9Cp/kTkSX//////L//L//y////p///+n//p/9P/0///////y/+X/L+X8v+X////6f/T/p/0/p+n6f0/9P/+X/5f5fl+Xy+Xy/L////p/p+n///ly5SymlPT+XLKU0+nyy80pTp6f/yJlp+lNQvS+lpiIDEKVOy/spfp0pZcvy/lllKaflKflPzTKUynz55888+cpmhZlPPKZTz88p5T5pylPlnTLlKU6f5fy//00pS/02XTS+l9L7KlKm++++lpeypffS6X76XpS9LLTS/0pS/p0pZZeX//T////y8uWUspTSnTp//LyyylKUpTTpp6f6f//L///l/p//0//T/+Xy5ZSmmn5cpT/lNPLNOWU+U0llPlPlPzSWU5SnlPynyn5SnLNOWU/lKacuU00/LllKUpTTTTSlKUpcv00pf6UvTf0vpfZab0vsqUtKWlLSlpS03pS036X0sulL+lLlT0pZflp/T6f/+WUpTplylNOWU6FymgZZT8pTyzTlmnLKfylPyzT+WU0/8spSmmnT6fp9OnSlKUvL6aaXL00pcielL/Sl/psv00sv9NLLL/TppSlKUsssssspZSlKU06f/LLNKf/LKaf8pSmnyyzT/llNPQvllKaf/8sspSlKUppTTSmlNKUpZZZcv/06aUpSy//p0ppZZcv5en/T6fp///LlLKaaf/KUpT/5Smn+UpTpy5Smn+WUpT/8sspppp9P/T+npppSy/+lLlTpS/Sl9KX0sqaXpS9KXSl6UvSl6UvpS+lL/Sy9OlL5E+mlLLLLy8vLlylKU0/8pSn8pTTLKaSylOUp+U+U/mmWaZZ/KflKcsp/KU/LKf+WU0//lllLKUpSlKUpZS5cv9OmlLl/ppS5fTpZctOmly/TpSy//TSlKXLy//0/p+n/T/l+XLKUpTTp/8uWUpTTT//y5ZSylKU0pTSmlKaUpZSyy/LT6aUpZf9NKWX9NKWX06UuXp0pZf9NKWX/6aaUssuX+X///+XlllKU0//LKU6fLKU0+WUp0y5Sn/KU0/LKU9MvKUpp//LLKWU0pTSmmlNKUpZS5f/00pS5fTpSy/ppZf6UpeVOmy5aelKWX/TpSlKWUImd//jJmAFzEgAFghs/v3/39l00ulpv3ZUvvfe903u9Lft9LTH0ul3um9L7s2m/TZez/f//5/zTT5TLn5zTlOfnPmefnKc5ynKc8+Uyn5zTlP85eU0/yy8vy//7/33LpvS6Wm7+03S3pd77stKl7uV/u/S9KUvSy/0/T/yz+aefPKZZnPPyZyzJTOZlOcPM5zM5nOZ5mZznk55nmec858885Z8/PLLP/m8spf9PfZvpdLsu7su90r3b3u+7u3e32ltLvd+993799KX7L7/6aUpf////l5Zuf/56fyz+WU0+U0+WbP/P/LyzSlKU0/////pp3nf6dLL/6WX6dneX7nv/07Nyy//0suX//TTSlNOn/Tp/LL59PylKUy5+fs8s0yylP5/KU/KUpylNKfzZp+f/Z//8ps3n3Omn9P//6dNP///y+b+XlyyzTTSmmnp//8s2bPZ///5uabPT/+XyzZ3P75///5eXL8uWXL8v6dnc7/T7/9neXTZf6Uv0pfpTS/svpsv02Wmy/6b+llL0pSlKX4JpZf/Z9////+nTTTT/5SlKUpSylPT+WU000yyyzT8s06aZZZT/8pSn/8pSmn+WWbOz/zfn+//8//+e/n3n3Ps+n6f//l8vL5eby83l5vL5fL5fT/9n3PvPf5f/uf/7////7+f+/z3/N/y////+n/p////5v///z///////+///P///++f///53///P9//z/7/8//v/8/+///+f7//////////////////////////////N/////////////////////////7////////z/////////////////////////////////////////////////+/////P///////////////////////////////////////////////////////////////FP/L/8cEJf5f/+X5ZZZSmnT+WWaafllNP8s0/ylNPllKdJeUpp/LLNNP8uUs0pp0/T////p9NNNKWXL/TTSy/6aUv/pSy/TSll/TSly/TTSy5f09KaUsssvL8v/8v+XlyyylKU06f/yyylNNP/yyylNNP6S/llllKUpSmmmmmnTTpp000ppTSllLLLl//6aaaWUsvl+n000pSyyy8v//0/T6fT9P//5eXKWUppp6fy8spSmnT/LllKU0+nL5ZZSlNOn//y8ssspZSlLKUpSlLKUsssuXl/+nppTZSy8v09NKUpZf/6dKbKWXL//09OmmlNKaU0ppTSmmnTp9P/Ly5SlKU00//lyylKaafpl/LlLKUpSmmnTp6fp/0/6fp6emmmlKUpSy5f/9NNKWXL/06UpZZf+nSlKUuX//p000ppSlKWUpZSyllKUspTSlNKdNPp/5fLLKUpSnT6eXyyylNKdP/y5ZZSlNNOn6fy/l5csuWWUssspZZZZZcuXL5f///T006U0pSlKWUuXLy/5en/0+nT06dNOmnTpp09PT6f/Ty/+Xl5ZZZZZSlKUpSmlNKaaadNOnTp06dOnTTp0000pppSlKUpSgWUssuXL8v//6fT06aaaU0pSlKUpSlLKWUsssssssuWWXLly5csuWWWWUsspSylKUpSlKaU00006enp6f0///////y/+X+X8vy/L8vl+Xy8vl8vL5eXy8vl8vl5fl8vy/l/L/L/5f/+X/////0//+n/6f+n/T/p/6f+n/0//6f///0////////L///////L/////l////5f///y////L///8v////y//////////////////////////////p/////////////////////////8v///////////////////////////////////////////////////////////////T///////////////////////////////////////////////////////////////gF0x//jJGALMAAAAAAAAFgH/+MkYA8sAAAAAAACBdP/4yRgE3gAAAAAAAGU1//h5GAUE+30AAAAAAACwQw==`,
        "./sounds/Menu_Key_1.flac": `/assets/Menu_Key_1-CWQUfe4S.flac`,
        "./sounds/Menu_Key_2.flac": `/assets/Menu_Key_2-BI0PlTqT.flac`,
        "./sounds/Menu_Key_3.flac": `/assets/Menu_Key_3-BeZ1W25a.flac`,
        "./sounds/Menu_Key_4.flac": `/assets/Menu_Key_4-D1SdRXDT.flac`,
        "./sounds/Menu_Key_5.flac": `/assets/Menu_Key_5-B6M6740U.flac`,
        "./sounds/Menu_Key_6.flac": `/assets/Menu_Key_6-BI9CZYQU.flac`,
        "./sounds/Menu_Key_7.flac": `data:audio/flac;base64,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`,
        "./sounds/Menu_Phone_Button_0.flac": `data:audio/flac;base64,ZkxhQwAAACIQABAAAARhAARhCsRC8AAACJ3rqIbk0rZvzedFYwz0bsVUhAAADgYAAABSRUFQRVIAAAAA//h5iAAInCUYAAD/5f/J/7AAl081+7ftYnxCkqdLJOnuVPN05HJlEVE7XdPZVyx2Knk0SkppmaZIl6Lbyo7z0pFqTNxNBKzRrkdPfVXP8+u5OSkhI1M4hUse3lyuj1FJUxGxsRmiMpCyHquXur+ruiZkMzCYRobRPatKvP+77uZW4RwlEo3GcRU8pZTx1n9Tk4kIzIbG5N5HqioF5afrcuNWEQxDCIZiU3l6qXiqV1Pa8SUTEYjCYSjKS5bC/nx/Co5PBFG2MyIiVPioPnLz4XL43ZsE4NwhGYkJbnrl4qnUrv8cnZsQxGMIyCRtvOtFw+CsFY+nasyIRsJWS6e/nQWHxd+iUiMQmDKGkGdL8FYfi8pXp87WmbCGYgxGISEyovej4L3LT5ekzY0EJiETZvv/BcVYfe3ZERDYhqJySW0+Oqiw/Vc7RGxhMMQhBIRrotfi9x6cv73XIyGYhsESBHEXRf1UsP/X3apBuQRQkTN4h2Fvd15UPqzjcaCMZBsQkzOhY514fBepfFSkyCIMghDNgjmvQtfldSr1fXokJiMSCZG7RZXpcOh8VPUbiQxEYhqRKn95Vdz0e8+80IhsEQZBDIxOSrOeD9zu/frpEgiYZGEhsm1+LFLC90FZUkZGRiY0JWsVD1+Lh4fFmVkRiIEwhmYjRIqHruLA9KXv5aSEwjGIQhGQna3/lgvV6WWyNoNEMjQmki5X/dVW+9q0IRiIITCU1iFQ/cvFcvPe6ckw2EYwjGI2MpHQv5XdT8tWuZURGJhIEUTbSfx1p48OQ7SE40IxoJTbkX48Oru6njxUkE4aCEGwyCQmuqryuV3KfD79KjMhDMw0YTtJavp/3X10taIzYSjKEcykOir3V+VcWWNSMgmDUNRkyIrCsK9Fc6uevfuaiQZmGIxCIiS4uKvKdAtgrLNkajYmI2SJ8+df3e0ulhKRBIY2M1E6e1+V1KLDqvtSsTCMYRAmMTIzp6Lh8H/FX9btxlGRBMJmZO0WUtV4V2FyKmxEZGE41ROjoelg9K/X+6NmNgjICcNDVpOqwPh1Kei4UhzKxuGgiBII2JNULj4pa6rV7OTic2EjM0bssXC2j53hdnNMQiEEQyCUT5FXD4UvH7nr2WiKExDCYNAkI3J6sKru6lWlzpq2NmIShK1J5esL+62vpWSmJAnGaESpfXriw5XgVjorTCYgjGEYzM2jHYrwLB+fixXZcmNDEYyEJDKb+fv1XnQucxSQ1NBEahHN6j1VflXio9sTYhkIYSBobInQsUrqLQ6B0HYqdmo2EYTDM2EcQ5jxX7qWv1R4RxuRCNRpiK3QUw+LXr2uRUzUJxGJjYnN+hUHqu56p9ymXEhEGozGgRRlIrFnfuqv1XFbwijKImYkaNaz5fi/FzsUxwhxNGaNiOIus/qw+fxbvtGhphuxqm2vRfVw5D2PryFEVsiRskSaS5yHSrl1smsbpqRU0sYreWWdPuykVrozm9TFELrgAAAAlpE=`,
        "./sounds/Menu_Phone_Button_1.flac": `data:audio/flac;base64,ZkxhQwAAACIQABAAAAQdAAQdCsRC8AAACJ3yn2TCAuc9c/61g/E7onQ8hAAADgYAAABSRUFQRVIAAAAA//h5iAAInCUYAAAAAQAHABEAksu+imeN9InIqaopPU+8ls6LNZKyktbcliS5HTPs+izLuyp23aNc1ZTKzkWX5fK6L9FS7ak0Ro0xFaxHYuhUeddflRHEUlZIhFN0mvY7K+X3/y5yXJGTG4kJzRFIu8fHR1/hUdHM7bJmTIbttEqpXlLnqzqn0puzYlGpITSJ0eL4KhSsPv7dUjUSNhI2JEmssX/168981hOTITY2ghRnS2FYdhb1fH1t1M43GpEE5m2zOYu9VYfFhZV3ZW2QnG4kzJU6e1YXHirHlkUYokJRoTGyJkutj391/fx7JU5pBKaG0Nqm7F3pUPi2rcuiTEpkxqJEbtUdC5Xheve+FSTUiQ0EolGcI7J6hWPcOx4vs9SpsjJmJSJWlTqjrFOhf3RfpEyQSGoSszRInmKhfHLlLlsVJGpIzJiaCO2vRfXQdCxVkXao1Y1MjIhIm1yo648LBUL/lu02JkQ0Q1JWunsX1Vq3qjplJoTIamgjhHIpFvjw8fvV/u9J2bhKg3M2gipvv7S4+OwqPrKRTUmbEhqiZTHIWd8LlOxaqeamuEjETCbNtWWVY/71/Vv3SpszUSiVtiFJH0Oilz+lIK1ubsSmTCRmonjfo/h8e8+f55EsNWwzhlE2at2KQ6PDsOnOy2SRoyZmxqTaMuiwqC4v7qhexSdqakQ1M2JCXaWPnwsruh0WXak5s0NGatEVlnW06C5/fPq0yEyGRGQlJKQqV86/q8Ox90czjOJTZGamm1YrKuUqFrzx0saoiM0E2I3MrRZCof/nxYdl6dNmo3EzY0TJdP0L8e+lR6jtNGTMSmSm3M7OwpBYq968eRU3NxKQkM0E5q1yp+sLhcr57vU0TM1GUTRk+18Xw6Cz99XMUmpEaGJwykJU3brlWvS14WefVpGMpiRkaklS2+rHWP8XWZROSkMoSkxK00t8eq/p8eexxDiSjVBKJENNknmePHhb1Q7OYpIjUjZkTIykdn3Kh7yx/H1uyU2QjZjbMlTezrCwsLf76xRHNthOJSZNpU/tVcLK6ov00SCKErCKGcapW8v/g6F698vmsaIiG4mbY3jeodOXP6VHniKyUmZoZuNcyk6oq3lvFYr6uytIyIzJhuJxFbVSFj53zr891rVMSiczUiVuQqUrHvelz/oVqnE5CURMMomStZFnX9VX1fc5FJUaGjNCbZHMqseV/SoffFN8kZsgnEhErRPPFhYfFr33yVKbsSmzJm0zFI6F3X+//O3iaIiImMoytNIuq+97SodikeEUlbGUasiRuYpjmOjodCyFRde3aREhJRFbciyWdUe2rvUdE+ao1aEqRukuXTqj5067O3SxIziduRSfTeb3VUvmvb7citJtNyLt9cqKxSOllRW7fRLSKAAAAOOB`,
        "./sounds/Menu_Phone_Button_2.flac": `data:audio/flac;base64,ZkxhQwAAACIQABAAAARgAARgCsRC8AAACJ3OtOuT3YUR3l0vZyRZiKNkhAAADgYAAABSRUFQRVIAAAAA//h5iAAInCUYAAAAAAADAAsAlb2ZUt0rKRxFStFEOZ0vQpHqPs5PEkzdtSKms6f1DoqHM+kZSQlxlGKMUluWUrHnWV29TRqRuNc21Ty+HYv8X6yk5MhM2NWyKkKx4WF/OQcj6k5qQmY2gzjHN1n70t5ZCtY1EpCIRENzeIq4/5Vfv1QrVtBsRDERCaNXZS2joFYW2/JSIhsIiGbiLkOgqDwVeeq6LtBOIaDIYkEpPM8d/x47x7uiYkCUJw1JiOY7F6qfuXljzkczQRDEIwlMkT2KXDw6qvWyTGjEY2EyGUnyrCvyupYXo+JUEgjGIzGybWL+7q7qvfebMjIJAlM0beR6XqLDrTotZRlDKCUZiGo1SZ8/U8Hpa63dI2EoyEZsI4jovg7B8d4dUeJSYiCYJiCUStLaW8XuoqDo+6Q3GoZQSkJTWsdHelflXuh23NkGoRDMxMSdKqrRaPD0t91aMbEIzCcSkdI6wr8qr1XTsmMjCMwkGqEKRVql4tKWFdOoioJRME4TMhHEKjwvU+Hq0rLkhGZBsIyEaJFY+HQdVPB51ziKaENBmZBKSTeqvUr1yoKY5JmJjEYhsxJpnvdSqXi3v0ckwnCIxDITI31r0sFpV/dPSSBKQaiCcamVLV6lVTvT0qfslMzMNRiQ22s/eHVX5cdLUzIg1GYkE20sfvReUtcsq5rhMZBEEg0bVl8L1d+X9bumZEMgiMTE1SHIPju9HrvnkSMyEEQhCEokuneF5eV8OxU6JRMRjYbjatn3deV3ei2FSpRlDUQzMZCQikOQpUFeBUFtdFJWxCIbDQkN4hYvKru/PP7GpGZiCYJRExHMWU+Hh+eFtcxRnCcaBKYmJW+qr1Kqnrrfu0JhMIxmY2aJ5D4/c8Fr1cpOxKEhDIxKJ6leutCoPXrzKSEgjGwTEI1N5YWCwWnXlvsRRIahqYTiNU3noqDwp8Kh0VuRMNmCQaDVm8xcLhefu/edHNkw0EbDbMnR0L1flfqv34iiQzEMgiIaJUKwsLxaU+FlKZybGQjENmJE3kL6fu/V55MmNhMNhEZkk3/d4Hx3V4v2pMwlGJBkxKzkPnVXVS3lZabkRjYJQmQiS6OsHh34vpexWyYyENhsREzOi1Vfl7vHl1ZoQTGQTG2S79eeD91FYKR9SUjEQRDMzZEXVepcHYe/uRycThIQzMREIrewVBXovFQfLOjcxqERjEZESt3Qq9Rbzx5aiUSCYxEMkIjpC4fFP1FvLqpuxEYxGIZqEVJP15e7qr4qWJiIgiMJRuayV8LDqosL1zptjIxmMTGJSKY7F/d+ev1zpEoyhqZBIhqnRaq0WC1X/s3NxshjYShKiVOudV5VrrYuTkglIxMhNEsqxXrqv/VL2yIhoZDUjclio95YpUFR9ZWjQ3EhNRHJ5Ft60qPflTds1ErIjcykWbqh0e/xUWJU5uSZpGqdF0/l88tieIim7atUlT69P95FZyO1iSpNcnm7OgAAAADLFQ==`,
        "./sounds/Menu_Phone_Button_3.flac": `data:audio/flac;base64,ZkxhQwAAACIQABAAAAShAAShCsRC8AAACJ0TijfU40SGS4aDINhTUGpyhAAADgYAAABSRUFQRVIAAAAA//h5iAAInCVOAAAACwASABMAEAAKAAP//bSijweDPu6N3qAh/xAAeVFRVxc3cXcXU1PWiMjo6Kmoi6uYuLq6joRCMR3YjO6sipmpuY2riKiozoysjLyZuNu72b2YmMns7u5CuRnZ1RVVcxMTF9GVlYisyqyLq43U7t7+3MxUdHI5GKpUKrpyMmryavIzqzEYjq6qrLmfn9Xad+/mZiqruzkdyK7Mmoy8mKiMru7lYzFYjOio2N1Mttepv5m4qMyORVdXRkRETFXl5md2IUhDOZyM7Mi7u9//1P3s3M1FVldWVkVETF1Ebl9XYjlIQzmIZioRHXG797v/93O3EVEVmVUZOXW19XW1FVmIQhXOUhisZnZUxcbu7t/ezN3VRVRWRERMXdzc3F1GV3YzmII5iuVyK66mNndvbu9i6q8mqyamYmPmb2fqZiq7kIQhDGKUjldiIqZmdu9m7mYmojMmKiaubn5v7u9q8qMVyGc5ClO5GI6Mmau5uZu8uKqoyOiriZvZ+U6mPj4mozOZzEKYpmMxFdFVcTdbFxGXkVmRkVUxd7P7v/8/M3WZDKchXEcxCORWdNTF1tTFTVVXT1VkVMbXynU2n/3ZiajsRykKUhCkdnR0VGTMTk9aI6VSiET0xNff6m1+uU7d3UZiMVykcru7M7L66qJqMqqrkVyKzIyJmfvf/0+m/j7mozoVyORyOyuzpqKioqoyu7O7EdnZGTMzf/6dT6b+Pi8pEqpGIxGZ1VUVUTl9GVmdikI5FI7ImJnb9OpTup29ranIRSKRiMquqKiJyajIRnYhDEchHKiqqZ+//9T/p3Y+Iiq6FV0dHZEREVMROQikUjlIVyFdiIyJmb/VwvU7u3NxE5kZmZUVkTE1MxE5Gd2IQh3Ox2OlUddfSZTv3+3s3MzkZFR0ZEVEzcRN1eRmIrlKYhClMR3dEVM3e3+/Pz9bV9vTkdeXV1cbF3MXGRnchHMYpSkMR3VURE7t38/ezMxERkZURETMXd7dzcxFYjOQhTFKUxDMRXZE3Oz8/OxczFVEVUZM1c3ez8/s3MVVIzmK4hDEOxmIhF1M7Fxs3sVE5PRU5EVdbH397t7O1eV2K5SkKUpiu7M7Jrc2bq5i6jJ6cjIqJna+/1Mp3Z+rqszkcpDuZykd0dGb1xMxUXk9GSiqqoirm9+/1OplM7ExlYhWM5iGYzFVlVERERF1FVWZWdHVEVM7u3/p/9/buIiq5Cu5SEI7uyuqpioqYjK6OzO6kZORMzu6nU2vU387k1mchHI5SEZmdORU5EVGUiVYju7kQiavNn9306vf3Z+JyqxGcjEIzOrKqonJqKhFdiORyu7q6ImL3f/0/qfu9z8qq6uVKMroyUbl9uTmdSMQhXIQjEVlVcXe/u/v787W5vWierp6KqpqJqJiqrq5FIQjmYpHdkVFTdzt/e3szMTEVPX05NXERFzM5fX0KllKV3I7kZXVk5cXG183FxdREVNVdTExMxczV1M1kZmd2I7FdmZWZERUxNTUTUVVTkZUxExN3FzczEzOVlZHdWZXVVVGTlzVRkRldmZXZUdOVMRdxc3O4AAAACsB`,
        "./sounds/Menu_Phone_Button_4.flac": `data:audio/flac;base64,ZkxhQwAAACIQABAAAAQgAAQgCsRC8AAACJ0rsoLSGzUka+otYuZR9orbhAAADgYAAABSRUFQRVIAAAAA//h5iAAInCUYAAD//gACAAwAqyyS7p0s01ZSWRJ8n6RdF1slulS7Tamdtyfmebyz1CkK15F2yKI5pzK1mtstXOhU6pFzK2pq0JEbtt0+9qxbSovzkWk+bU1JGqN0u8iz+/ofIVqnZlEiJiVMzk9ixfVKjvuqetUTYSIbY2hOkn6Fq16+LbFZWkMokbMkQhTVWfFy1/i50+qbY1IyIhIkIUQVl/XqpcVUFSzmc2ZEY0NkbJdIs/fHxfassm2SCdjVCKIqW5fHh0PKcwqIuJyNsEURGyJt9Fj+lp8e90VTFNyJRqRmzIlM6Z91irXndFmU3amjIjRttJ91xf+9X/FIuaoSGxENDUjKTy4Vi94eHlLvN0bmSEbM0Qjkios6ws632yVuTImNsbmqSRULi1eWr//rycmxGQkCKEU2mqPPBWHQq+rbd0hOaMjJDclT7odHq+r+hdqlZohoRthFM4hUWdVpaWOVFVPEpIyEaGJCZIRUXer+8UqPejoxQhRpBIiYlmRZ53ir4fexTOyZCQSiQTm1a5cv609f97V1pO2JRqQkNTKIul8618fFYVqpFJzVmyNkawi1FXP+/h57fk7ZDKGpCQ2J2Tyf1V/Pa75Z0tNzU0I2Rsk2+/nfHQsdO4hxFQymREQkxLNikL4WHhcLv+nyaGhCQTEglRKnnq+C4tfxb0tJWozhnG4nbXZctq0sgpDp9yKaI2I0I2hJdHZysLD3v1ttmSIhINxGhskbs7HY+UsWllXdO0xOZqRMkIsZ86Hz0qHkeapybGjGjGpq0lY+pYX8sPHQ6XckxqJwkwkgit09vqre+Lvayo3JiU2gnasrL0LVpcVcXfs4jmSjRiIxKJyXW5XQWPT3r/l1SEUakRsTJCWp5VtKh8VCp6iiKyY3G5tkS61DoXw/gsquQrdtRKJDIhoaNzKjovp0K8frnmUnRMyJiJDdEt1XFQ+LP11ukbYmQjRiSEVvY9pf37188sStTYiYRMREplyPK+Ogs7xbd9ojRszQkySZUKj/0uOx9ZYmbjUhMyESslln/VafDsLcdJeMUSjOGrG40prlIVC5XxauLJZNobicyRmlJfvix46+lQu80yYmQyIRoaZLo+714dY5SDsqKjcmbEajc1RnaOxZ197z2fbiOZIJzJDKMcR0WLeW9Szr5a3xIxMTCQ2RMn0XFxfS/re7fkVkhMjImJURSFIXPHxeg5jpa1hMmNGJGhO1UjrOr1XvKx0tSoTmxERmRIS6osXP/6WQ7fpUkZNBOyaN9lRb4q546LlIqJMzU0QymqVlmKQcgqFjx2PkKZUTUjVDdCdpye2Wrl+/rHM5FRvidpmUitbJVT+LrVI+0kbklMplJO0slzp4qOi6KkulJ412rfa6KRcpHjFayp07KRUlwAAAANIE`,
        "./sounds/Menu_Phone_Button_5.flac": `data:audio/flac;base64,ZkxhQwAAACIQABAAAARaAARaCsRC8AAACJ3AAAXNmvLI07/YGKz2reD7hAAADgYAAABSRUFQRVIAAAAA//h5iAAInCUYAAD//f/+AAUAr9nIVrOk1hHMpFJ9lIUi/RZFTzdJptkrSt0+8sdFtyo5vTKa43aGKTeWxVxcWXqy5FGc2ZMyZNJvKVi15/5ZWrJTYkRCOTFIXPX/f185CiOJoNkEhsSbZ2d0HxXh4t+yjKQkIRERCO2fPD4r1VVH0biQhGYyMzUio+OgWFd1V4VIolw1GxmJBu1T+HQXq9Lb5MiM2CUbMSJrfD08DoP1X/SmpiIYhkExqyOz1Sw793h0vakgmYyEwnab+rz9T1y5bIRQlGw2GZBKSIuVeVVPz0tq5ORGQyCYiNoyzrXflorC6OI5mzDYxEIbklQsdXdy86/uljQ2MxkEQmbRFtOhy4eOqHSJSMjEIzMhPmOxe6u7loqOqTZBIIwiGY0Jqi4+Kp4LXedWuaCQTGQiJEzo/VXUvdY8qbEwjMIRCGjRFy8vdyq5b312glGzDUJzJJJ1WuWnwudPoyMRmEgzUSSTvjlotHr8qO3ZGZiGYhoJMlxcrq9P3hyPo2QjEwiE2M6WHx+5Xp3x2XQ3GQjDYwmRsni3ioFpV58+szYiMZCNiTa+vCuquvfLGUTYZkEIgiZGvzwdB+UtFv6t0RGITCEgTsknzvjvBYX5eSojMQ0GRmROkeOWC8dA85UK2iGxmMwmMmJZbXeC0+CkHZU7Y3DcQ1ExO3R/XLT8WKdOplJjMgzMYhITSWq9PR4Lh0LqiiRiMgkGpNpL1706r3XO3RoZiMY0CUjdLV6l5T9RYv6KIpiYgmMREJzFbveeqer/4lRGYjMZCIm2Ww9K/FVXWFSpTIhjIYhoJtpb1Xi4eqvVPCKNmQaiNCbWzv1PBVWlsVojMhhIGhjU0iPYUsF56LFWXbYamJhobJTFR8KgvdVX2qbozMZDEQSCOM8x4pXU6vxdSoysaGIg0EZEydF61y9VeujtWhGZiEQjQn0WHhXLz9+v1SExkGQhmI0JLl9c6B8V1i9LkaBONiGpMis9peXl5euOTtGIhmEZhIZK3t7vxYPSrXytWxGQSBKZokyoePzw/Wujk7EhDIYiGRM1VFdefirzrhSLCRCGwjMNTInarhWC4pa/VCpCchEIzGzNpUXqquVVPX8vJTQxCMNBmxJkdC+i08C4sLro0MjMZGZMllpcdVUq/uxSVCYhmIZBExu1dj9XP3Kr1ey5mzGJhoZMRWWx3ru9f+yRsajEgSDcTdqt7l5XctPFzkiEg2CUZjU20sKgsK7w/97OamSBMRDbEuVHVe5S4LOVlrJiIY2GQTMhHarC9+LBaq3o4iibGNRoRI3R0POLj06CkO1biJAkMxszRLusLT0pcOh0dumJRMbEJRORWroWP16++XRMyYSmxImqLlXHXCwuKjsqYijQkE2MoitVSlkPioWWK1SJDcmbkymKQuhyCsfX9LdkpKzVCWIip0Kzzx2OmKRW+StKZzfSIsR2KRey6WJdWsyfbsUQUzgAAAB47w==`,
        "./sounds/Menu_Phone_Button_6.flac": `data:audio/flac;base64,ZkxhQwAAACIQABAAAASxAASxCsRC8AAACJ0sTcFqNmpDw6jge4PdFbAFhAAADgYAAABSRUFQRVIAAAAA//h5iAAInCUYAAAACQANAA4AvoxxF7dSPuyp0RTWklI5ioh265Z4qRWiVoStGsb27lZ6uWSVqZRISISpdP7RWOt6xUyiSDRiNmyS7rHWnsFS/JSMmCcREhike9XU+Cxes22ExjIQamSJ7efq4qBWHJ6hKNiGIhsmQpBfyi4FQerZSUxkGIwaCIS6+lUWBVU//1xoIxiGQRMkqwp+46l5fFMoSGEIEQIhibS8tFUVxVLT6xsJhDEEQaiWsLnOp3O686SMhBDMEMQbMS3Hg/OKo6l7zpjIgJQRBMIlbvino6nejs+yEwQhAiDEJmqFh1cPgenL3k2YgjEDQEo1Msx1oXuK9PZc2EIQwzCEQ119F49BYC1y37YyGGYQwiEJWdD0pVFdTx+kRBGYYRjEZGVH7lKUVQsHjyJjIMwQhiBONdYeHKpz8r45nEhDEMIgzMmqF6i8U56Hx8URRKCYQYghiEhlRVdTuUp+670TjIIQgjCZMi1+K7nO54eVIxmEMYIxjM0iF7uVwsHeurclEIQgyGEptY+C4dxeKqvK3MJAhhkCGZBFSryucqhVU+O0hsIwjBMJiRHyldRXPynY7VmYxhkBMEgTsrFLBReFoeHq7UxMMYyBoZuZ6vKVzqdV6onEIxhiGGZiJLCwfilFUXioVOaCIMgZBGIiSx1cruKqlzxGg2GMYRjGyVjxynUU51Kq2ojIMgZBmEbNlQr0V3O6vfUajEYQRATiGcy/uVxSuPxb1SkQTBDGMYyET6F5VOVR8OhUjQxmEIIRBpGdD8XjqKfnWLszGYQZghGITVC4/Ho6lL1t2JhsEIwjGrIXK5+F4pVK6OmQgiDDIGQJTJZT4fjqU79fycyGQIgzEQ1mVX46lHgWFl0EgxkBkEMZGSzvzlc5TqpUKbkYhBGEYaIS391F46lLS1ihFGMwxjDII2Is6no5XH53hyKzIQbBCMNRJr1pyqc9FWPsiMMRghhEMapnx1KcpReFxbdiIYhjCEIgimfFgvFU78XKnQSCGIMYhDIk3V3c5zudT23ZBkYMgTBqZOQ8UrqFpXLK4iiIQQhhCENsyxTwK5RVFU6FSYyGGwNAjEJUQ+dz85+LCyVkYxkDIYTEkY9U7ncU/F8KmZCGIGQYgmNkdFcr8Lxe/duThEIYRhCYjOQtK6hePx6WzthEIMMgw2GpuivBS0Oqi9b82wTDEMxmJyFYWHceii0r5cmGoIRgg0BIhvaquKorld3ywmwyDEYQjNUQVguHUej8tdK2IYRARBAlCUzovPznOpylz9MyEMYhhMNSTxVUeBVK/LlJzMYghjBMYS5ngqAsCucrusKaQbDMIZhCQl3Xl4WnPSrdWwmCEMYJBDdpS1xVFOo6q8UnMzDIEYbGJNioV3cWiqq5UgmYYhiDYTJMvdSnKUpXX+JCIIwyBEIJM3venLxYPizdmQjGGoRCU1ch+VSuLAs5UqURCEIYhsJIR5CvSqVeqoqaCZBsRCNNUVYLHFx1q3XNmMiDQSidrPVpap0Lb0nEqCKNs0a5Zj4uFR/Za3ZoyMkNWUz7xbV96CiFapScyiK0RSLLPc+vT6TRu1IqRFyFIVv9imAAAADe5w==`,
        "./sounds/Menu_Phone_Button_7.flac": `data:audio/flac;base64,ZkxhQwAAACIQABAAAAPKAAPKCsRC8AAACJ2NxH7bQsBYHBByfwxwEKkohAAADgYAAABSRUFQRVIAAAAA//h5iAAInCVMAAD/4//K/7f/q/+o/660ox85+qd3c1I98BAFpNpEkmSRLttLpksqTXSbt0tS67aNMkSIkiNEm2281ip57LPLrqmiRsmZEaIiZNppvp9Z/LbdJbomiZNETSJklapJomuya5NNNdJ5vbL7Pd942SMyIjEQiMjIkqTe+//9f/FqfTEqIxIRMRGRE2yWpd7rvJJdak1qTWSSaWSJJERNEZGTMjNskTWLL6/Vff/5bEzRmQiGRiMRmyZJrb9+vq32/SWZJEjNEjJNGm01TTZJGabJtJNJfPVt/8vkiTZmzMRkMTERsmmuqxfS/+v7PLEjZGREQmxEbRMm+lSzXSzaSprrpJN7Unsmm3ZETIREMhEIjJNERfPFX9evvsskzNkIzEYmIRNkTSXX3zrvfN5NIkyRJkqSatUu0TTNCREQm2TZJZN1/q9/fP5tIRsyGZmYhEQmSJJ5v1i9f/fZpcmyQmjQ02zTTa6TRJNEkSJZqnluviz2fe7SYkRGZBERkMiNGlkyr1jrXvWvpJEzQhMRGNGbEuSTTWpfLLdLdpZolm7eSJ5dtUiNWRmzMmZkyba3339Vv73yXTIjIhEMhCIzERK00tntWLauWWS6JJGzTZGuyabaaIkStETNUiTSWS1b71tv9STQmZGMRmIxsyYl1iVa9patX+3pdk2RGRmRERNG210muks10100klqeTz2LvJNImZkRCMzEZszbTS+XqvWr1WeWTIiIyGxkQmNs0SbXeeX2WeTpLqm6VNUifSJJNtGiZEZEREzRqlk88Wv+se+66pkbMxCEZiERkZJpdff7V62/yaXIiREzaMjSJGqTaaJNE222iSW6X2xZ/LfNLEREZmQxEIhGyNksm96wtXqs9t0jVmgkJmNiZkzVEk+1lst006Sp2lSyWb6ydLEm2NERjZGZGyJElk//qr1X1Z5K2mYhIZCGxMZE2b6yee+z22zWXJdMlTaJtI02m2SbIibJmSJWk339/v9eX66TYiIhDMzMQiZk2mtt/q1avvtlkTRGbYjJs0zbVNJGl1TbXRIlkqS37PPc6eJZETYiMhCIzERoSbdPF9evX9/8murIjYiIZCJmZIib6b/W/6WKkm6TJdJtJIl00uTaM20JiaNCTab6ePq8e3+/TaNiRiIyMZGbNMml+vq3z2+Wa6ZI0aJskaJckT5LpEk0iRJUiS67S3XfeSSyZJsiImRmTJtkbypbbPvbIss1b5KSJozbJkqJUqT6pbZrJpZsl2RKlTTSJElkTWTapZIkl2STaYAAAACoBw==`,
        "./sounds/Menu_Phone_Button_8.flac": `data:audio/flac;base64,ZkxhQwAAACIQABAAAARbAARbCsRC8AAACJ2ZlTsZS9DrZ3hM15khdeeMhAAADgYAAABSRUFQRVIAAAAA//h5iAAInCUYAAD/4f/G/7EAruxdvJZpcnbtdEva27yeTopHRnMplJY1krFRZVyxdZyFGVGjZESJpWeWFnWFzsKIc3TZCTE2TVl19q+15bKlbQTmyI0pF1W9VWnx0LpcmZCMhDZCJzLnQtfl68tts2gzhIxMbkieeK+PXq+eJWTCQgkNBLmdF68paLB7SySkxEMghsY0JGVixVV1Ph8VDk6ZoYkExqasqfrXLCvXjs5uajIQhCMRG12POfnd3cteeZqJAkEIaBNRljCkFr0WutLp8mZsQTjJRPsuLVdTw9LcVpjcJhEEIwlMklv5+5S8Wl8KT5kQSDIQ2ZGUxyCoeqKwVhd0VERsYkGhtklteHdVK71f0lEhCGYgTgkRm+XO/KpXdVVUdqyIxsYmEUIUR5CoPnOg+7s7JxqNDGxDUkiPVg/U6n6varajUQiDEQZMaJ8sPD0XqVYvic1NBqNmbNYjxV7qrrC7xNiYZECQajVpZToKrnVTvXnkJSCUIhiEQiJun7w/fnhb99DKGcEUZGyJWLu9PSqpY8tzZDIIxkCUxKmWQdB+4tFrl1VtTExCCYyG4it71h1pV/XOS5CJhoZkRE6f9X5VP1Ln7KaYJRiEIQzYymWd654Frnhbe1zMTEExEZqk9ha6qve+KIUzQxMNQyiaJ5S1RaUr89rpdjIghCEINTNql/5aOgXr38tIo1IxqEonE9IW4WF6uteouRsxCEYyEZNJi4/UXlKpaqzc1MhDEQJBERNJ/+XlXuVHsTm41MRGQkRF0LHenXlvWaQjQIjCMZmTN0LnLAsHp3X1aRSITDQJBsRImhWPC0/Xqu9J2QiMRDZCdlY+LB67lpauUnIjGIggmGhtmdFeqL3K797VatmY2MTCQnMsj3lpafFy6omZiExjUTiFJ3jqp35TwsWampiMYhBMNGao7CoL1Fwdh7aqbZjQzMRIbkdCsPKFYfHlqiFCKMiDYxGxIn3Kg/KpSvy32RobCEQZBIE7MuwrCwWHV8LMuyJjYhoQnEcixYKg9dVeLKlE42EwahMxIkt78quPh16ui5qxkYRDERjKa9C2D4/V6tsyUhKEo1E2ayx6XlXjxVXdpQkYZDEMTCVt8VBe7lUXDzrEU2QiCIRiZsluf/l79eqO0hEQShIJRrk+VYU9cWnrrvqaiYJQyGYSjKZZFfqfuel/i7JCIJwmNRlNbKvp+vPi71JhOMjDUJCYis7DsFgsD1T/rNyQyMNhCEJjaaV4/P1PVez7RmbENiZk5FtXq6qWF+XcRREIgjGQThim6Fx4dXPz08WK2yEZjMQmMkSWU6OfH9LKrdExMSBFGqJLc64fFev/ZaasyCIhjQmaZzCzlgv5f+qO0biQiEgkzIqZ5CxfSo9Y5rshOJTUm5nT4qHnVasXeMpuZKZtBKlIs3rsKj/F33m24im7MpJJEK1WWfp7E+0SbRrGXMVLikVW97VW3StYRzO1RUtwAAAA2C4=`,
        "./sounds/Menu_Phone_Button_9.flac": `data:audio/flac;base64,ZkxhQwAAACIQABAAAAStAAStCsRC8AAACJ33KNE44JU1w8X7D5sLed1mhAAADgYAAABSRUFQRVIAAAAA//h5iAAInCUYAAD/7f/V/7kAsjk8ll6LU8qVKmiakVrUXeLly63TaYlhviFbFZ8WQstvIpOJxONERNrlzwsPj2F0sbYmIjImSp/aXHqripY3GiGQSmSNbfTrRVfn3czmzENhBIIiMpC515Xflr7wijYiCMhtiOi1y0V+d4WathMMwTBsJiTkOtO4qnd3W3bEIwjEGTCSov7lU/KWFZ2TCMQZhkEbGKYWOVwtHF4sLYmQwjDEMGhiZFR3oqndz1VS5iIYxiEYRRP46nU5yuVSscRTEIYQQwzDY0Rcei8U5SuWvUpMYyDEMhkZW70pePzrzkKSEQZAhDCGQ0S4/UfhXFUVVqkUbMGwwhmMzVniu5VOXlVDm5swTDEIQibRafnOVxSqryZkIYIgQgbGImixTqcpSuer41QJRBsMgkRJ3wvC4c9KtlxsNgxBCBMImqKpXc47nUVYvNWMQgxCCERm9Q6qoqiw/XVMRiCGIQRicY6c/OoVzqFj+wyhoCYYIwjDc09VXKU7qL6UngkEIwhDISEXKqlKoXuUqHN0IwxCBBGGZm3Q8FUVQrqFilbkoiBIGQZCMll6ngfBaKgqdshmGQIQgmJt/T8PgdXHVVtamIQwTAjGQbtY9Op+Keu9ymiBKEIQzIRFRVVzuPRT170xIEIYIwgTCYxTHji0Ko/FeCtawnBKGIxDJtIWC05XKr101ExjCGIMRmM5FXncpzj0de+mw2GQEQwSGIpnqqoqlcq92VEIgjGGwjNpKqn47nKV1WzIRiBsGMIRhNNHXj0c/Fen12IjCCYZjQ3RcfuPg9Hx7lJjIINgbBEEyTFji8Ko7uPHspoIxBjMERmSTr3K56Fx8+amQQjBCII20j0VXHKdTnxYyiIYxhiBEGbGVF6lK53K5Y6doIzGMZBqIrVVUvC85VeuToQ2DGYMQgiIlj9R1FcUpe6xKxGMYxBCYySx6VTu5VVZciEMwRBiEImSveO7haC0WC7ciEMIQQwRGGu15YF6h8Fj+SiZBBEIEglN7hUCvQfDlp99DYQxhBmCII3ELKLyiuKoqqs5FCQbBCMIhqm/V3U56LXtbmQRhBGCINCSQsOU7judxUFSo2GQIwRAiMa6KsCwUqlf/s4RQiGMQxGZMqFpyuUdSufLSUyCGCIEMIyCdFjuVyj85e8tNxmQIzBNhIqUVA8OctFvshsIgQgyAmYZzLC8pylHU56XeG4aDEGIZjIm8VVceCqe1dSYRCDEIIhE1Q8O7haFcqllqMRAhDDGEIxMizu5SuPTlxaTsgjEMZjI2u7qp3O5VLfEomCMYYwhmI03/O5RVHcWqcxTURmCYMhBubewvO/Cw6qhTViEIEgY0CRrnw/OoLA/FgrLkMjBmGYYRGbWLRaKotHqruTiMxjMQSE0nVVO5Sl4qHSZmMwggiBoNSZ0KvFcp1K/WSNAnCEwRMbSWHhVc8KtvRIJmCQJjIyXf9y0dXL3q7VjQg0EIzQ3Tx6/UtLi6yNmRmJBOTK3Wvh7Rc8VO0QmYjIbiVJ8uL6pUPlQpnEVm0NqSSSexVYVlik/EVGiZI3I7VU6o8vl30RTWIim8jOn317OgAAAAEgX`,
        "./sounds/Menu_Phone_Button_Note_0.flac": `data:audio/flac;base64,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`,
        "./sounds/Menu_Phone_Button_Note_1.flac": `data:audio/flac;base64,ZkxhQwAAACIQABAAAAecAAecCsRC8AAADltceZjHotNB8cihtYKtIZuihAAADgYAAABSRUFQRVIAAAAA//h5iAAOWgcWAAAAAAABAIGAABmAAAACgCAgAOACABQQOilwVh8KIUgcMcY5hxlCKTCY0abAEAAAJgAAAAA4AAgGAKAAQAoMcNqxQwCkKwAowUg4IODA6OxwKCgEAcMObeE7D8KjsWvllGMIKIQI5nYSBijGaMn3S1xGMQlo0bt6FIocuVyRlOngFAUCHtohc2qlP/uq3FRhjfECbkKCUJIEZIZZN8uMQRso0J0yjogFLFVq2C6UUAUxAog4T8kylKrty45C6BhPCCCFMUJAmoRCbJ5vvxkGZiiMlSQesFC5XTQgrkfgCqDAUY7MU1SnUfKx4+W4YxCiME+rEYYoZCJtLWW0jCEawyJdeFGFD+rdmUUjwBQcBA+nM7TYrjzoW92fMGZWMJInCUEKMxtm+7OxzMYRMo2I7ZxWoF/6tScVPA4BwZSs4ixpnc7ydaUh0QYZaMIiiHDUNIMzZJOnQqGYZEUTEsRT1gUWd0jYXo9ABSCBzFNeSpylL9h0eeYwaqEQZWUTDJRmZEqLt7TIEwhSMjOSlFGCj/rpiHfXgFFBg7KjOiTHKWuIFQsvwhhBRDCduhkCFCMSEys5FimQZjLBoRyVQoxw6x+mI4reAUBwEHk5nRKQLAt+73xYYQR8MZnEOGwRWCZs1tSx2IQRsojaaweMB19dISjp4KAOMgpBQyqJ2Koq248djogg3oxDOIcZBtQjNRF3S2KIQRmKNBF2grjAU9pcmQ6ovAHoYFMUiHGUiucsUgVizyCBM4hBnMqCIMUMzQ1s1sUTBGI5EJJo44wodcWJTFKiwBw4GBW9EKmSHcq3cs9tMEIUhhNyKNgxRiMiaT7LWQIgiwamVIorUCrHuhnLLA4AoGKuUZ6bopyvJ3x2fYEIqEGSo4Jw0gxM2uskXEMIiKIjfJx4wOX1dTQKnjgAohgUYUS1NlUf/kCoVFqDCRQiBCmczCJRiJBF3SLlGwxGcjNumHcYFHtKmog6osAC8IPrDHJEpXFtQ+OnViBGOMw10g2CFCMasl6W4o2EE0hkTpFHIoKtXtGUUj4DgcCHtohyTILx6pCvlvTBjOhht242COMgkZPJuhyGYRso2bpOLGAWr1TJg5HwoAUNhzHG/JMpTrbhUKj7BBKoQghTFCYbQQjQlsm/xCCMxRsTpUDxg4tW3Iw9n4AXDBWVGKasp+PJDodFqGCZxCBCk7GQYoZiUklklcJAjEciE3ancgoWqumZRzLwFBQED6czompVHxTH9XLiBCFIYa6UJAxRiE2a9LKpCEMZcaCFacKZwX/qmbiouDgDgML0cRY0zlK8nePvSCDLQhCKQoaBlYZGhl3S34xBERRoTpKPsDqv+hKOlpwAozBTHG/JWLxS748XPsIEvEIIrKNQ2oTCc1s3R4ZhCM5GJa1FUg4fL+0IcrLAAqggKIdmKTanUdCiBc8towzHGYIrdBMEKEI0JvJvFMgRDLjIzk/HMUF96piFKi4DgKAg8ihBWyQri1SHq2LmDM5DGSpxoCFGYiZrayzoQxm3MjdvA5IB/6UlJR9KFAFBKKIOGeNWK51twqHIdoEE8MIRxDhIGkENkb7pbYwhmYoyJUkH5A486xMwWsWgB2EDmON+23KUsVhUPqpCBM4hhHMrEIIUExqaz7X8RBmI5GRLodyHB93pOIOqfgOFAwKyoxSRlUos6HxZbRBiFIIbXQaBihENkTyb4oiCGb42a60KZwVY9jaBUlAoCgYeRRipkquKsi+vlyBiKhhEuo0DKwzZGtm8sMwTJzYliKLsDv6sxIF0ooAoiDiCiWpsrnf6CsKQsQIacQgxTOJgmoRCRt6ln42DYjkIkmgqmBRY9uRh+xcAHoQKQpMrRlKotqHvt+EGY4xBKikIwxQyEzS7pfWMQRFIhJNHFMUHnXtRBXYrAHDgQKnplhOwqi1WfrY6ECM5BGJ9QlBCjMaMnk1qsYhiXhKZyXhSQBYvVIZRU8DgFDcpDjfkmdR/bC47PkGEqhmEKTiQNIMyNpZvlxkCIxQlEXJx4QClq/RsPJXADmIFEFEtzWCqLKgueesQJihkCFMUZCCFBEbNvJJ6QgjGOZmmIBg4ABQEOAcEByFZIoow7AHKGPJRBRu35+uy29imwbdjEmqMxnERE0u6LviIyWkmu9ORxbf25H8sFHEWTmXJd+tqfouuRmuQk1hlGcma7XWXfZGrOSpanfY9numtsvOmVFJalT32TKipa2STInZWplSNKlm+u6ZLpNultkWfeab3W/p0+m7W5d0t3S1Jk6ROytcnSapZvpdUk0qWayfRdd9WWaz3S66SpFZ28130mmlSSSJ0qdJU+l10uieJ0+k6Wy666dOnuks2VrLpal30qWJ9Iit9N0kqdOn0l01m7Kkskks1qXdPpdZNd281msk3SyTS6SVOlkmlms0sms0s0s1mskkkklTp4n0uklT6Xb6VLU6dLJJulkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkgAAACxvQ==`,
        "./sounds/Menu_Phone_Button_Note_2.flac": `data:audio/flac;base64,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`,
        "./sounds/Menu_Phone_Button_Note_3.flac": `data:audio/flac;base64,ZkxhQwAAACIQABAAAAgbAAgbCsRC8AAADlvuPaH6A8/OJiyiNglvLypphAAADgYAAABSRUFQRVIAAAAA//h5iAAOWgcWAAAAAAAAAJQ4OYikjSNCEITGMQjNURTt4xK9YoDgoEAsBwAAegAAAwUBQAIAAHAQAcM6u6HNQDKRQBChBw1ARwmAxwRyFI7CjMGETmNonOcwHDsLSRg8hQCgoZzm5hQQ5Adxah1rykAiVgZE7EDYhA0JsqefBDDEUhmuhxTAUFY/ZMKucBQHBlHCHCBQYrApy2OrxYQGaoBMRzYGxGBKJWK3ywMQI1QRrEOOQOD51RkHXKAoCjQKM4goZcOO7kO9W8wYS0DEnMgRiYDc2lkjoIIETQZNUOKhwHY9zYopjgoAobiiFCCiXHDuu5XzxAIycCE0zBmJgbMmXb+DGDJMEm2OfHA8f7GFOlA4BxGBwxwg40rh3KzqXPoBCIoGYjswQ2IGxG+stpgghrA1IpDjo4DznJCFFZQUA+CAURRDmRaBech6W+4GMigwiKTBmNQNCJUXTqGIEJqMjKzj5QC53REUXcKALBgUzjPifg6l347F1BhEcCCaoxgmQEJRnIcQ7FCEDEkGaTUPKA/5UzUFJwoAvDA5K14YXAWjs9PFsBBkcGEb4hhCYMTG+7OhwzAjM4ZEuoXoAf0+ZOPqKAO4MBxOmUIdAdxWXnizggRHBBkqIYImDDKN0VFRyDBCJxDbtx1QBywsRoCpBwDigQHbkjhi4HUchYLP4MCI4YYnzEDIhgTmk6fFEIGEyhG0Tj/AKfHiJgtwoDg4DAXNWODCsDuLrxcVFCBDOYGSRDBEIwMokzkWKwww0ohI3w7sBwrDpCQLcUA4cEHrcwoIcgFUW53WLRgRKwES5kBGxgZwnRU+KYwwycg1ZWHKgcCkP2TCujgOAoEF7KMFBngUo6fl65TARKgG2mMGxCBqJWK1qsYMQziEaxB3wUDmHiZh3YoCgOBHHGKGDgiwUUWR1fFRAI1QGZOzBDZgIoRSPpHQQYYjmMmqCnYKB2PchArs4HAODKKIUIOEtFFKuV1qxgQT4DQzkYIxMBlES/b+DDGE8DbXOFMFAVnKTMKUhwUA40ChDjHEyuFKqO7odIBmRQERu2BCYgJDRPJbRBAyTDJIworHAfKsZiipwUAcxgKEKEFMncF52fisduBiIoEMikwIhoDIySKRfBBBjWBMkjiocD+lRGOVHHACsMBwhRisR+CqchVePqBDI4GNqjCDZghsT7p44QwQm4bJUcfKA/h0xHHSA4BeGAonb4yqHcqPyxbAYRHAhm+IYJkBGxujkfKIYMSQZNpQ8oCvXjJwrcKAdwYDiKZYMdAPR2VSxZQgJnBhEqIIMmBkZqip44xhhrQiaJwvQArwsQjnlFAFOBAKZSWiPgLx2XnxUcCGMUEEJ8xgiIIGUIu7PlMIINuIaRFO6AO9OmTB9TgFBQGA5JTCgwrAVRyFh/bwgIjhAySIIMhCAjhlRdKpBBhGUxkug5SAKOw6QlFUgcBQUDAsZRhwQuDlHI9P4tCBDOYG2jMGbCBqaoqfFMIIMlYRLqcrAUFIfsmHUgUCgOAwepGODCsCuOnV//DAyVgJGqMDQhATiTO1qsIMMnIIliBVQChUFmJB1IKAoCgYdTOIKGXBzlt5f7WAyVAIidiDEZgbm2XSOgwgTRBtrijkDg+L2THKigKAUGUUQ4IFDOg47uzl9WMDGtARNSYEQiA1Eqep/AhgjXBNNHDiBwLn8IgpWKCgDglChBQQOCLCh3XKp2OmBhPgIjdCBGJgJTaWS2iBDCKxiXQp8cBUKqNinZQOA5kBQQcMOEtKH507lnqAg3wIbTIDYjBkZMu38GIGMqCNpaCkOA6O6YwpdwUAcRgUYoxWanCqch+8fQECeAxCOzDGJAbEa2p44QQY1gza7h0UAs5yQjhScFAFwQCiKIVDPQfjkVT5bAIZHAxGUmGGzBDQ1sk/EECE3DQn3CyACyikplCpQoA8GBTKZ2IXA6jmLRUO3BBCOGCaogxiIMZGk6e0ggYkghJpx7QFXlTEcVFOAF4YHJWtEeA/HZVevqGCajBG+YQZEGCKEVHa/mECEThM1RRVQAq06ZMCtxQBSgwHEtMKDCsB+Oy0dHnDBCOMEJYggREECUSxSLHEEEG3EIl0D3AKeF0JR6gUBw4GB0TkKGLgVxyFwVF9DDGKEDbRiBEIQNwjkOIXdBhhpCG2lOcgCioPsbj1BQCg4CB+ZxBQYVgdR0Wnv8EBErAakczBmwgbiTO1qkICEbmNtUCugKCsLMSndjgOBwMOpOYUEOQHKHAVgAwHUIDgnAJqYM2iEGJBBqSZZv4MQTKQlR2FKgcWP20KqOHBwQvSM4zsK6yVblTAmrDQithM2NkqKisVCIa6NrUK6OFYqdr1TjimhSKZUmdb0/31QhPhK1iEiZtEt1uqIiWJrM9ivlqSVU6naKk3ayFRdL06VmsZJK22iJdJ2XVMlS6ayOz22Sa/WlyKlRWls9S2SVI1iRLI0qbdJLN9ptLtdZ95bJrlSLZN0s0lsklkkiSUit0ummqWTdPpqk7fXS2XfS6WSKk6XWIrLpdd0uqWJZE8ZW+3ZU6WSIrWp0+lku7Klkk6WSSTXfSXSXVLNYlumsieaSzS6p4lukusm6dOnTzS6XXSWJ5pdLpJKlkm6WayTWSTdLJJJJdJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJAAAAAi+Q=`,
        "./sounds/Menu_Phone_Button_Note_4.flac": `data:audio/flac;base64,ZkxhQwAAACIQABAAAAhpAAhpCsRC8AAADltiCzHwyxMgHul9tSAtUTizhAAADgYAAABSRUFQRVIAAAAA//h5iAAOWgcWAAAAAAAAAPTTUdKJ6qymc6XKVysnOJqL7qlaniy3E3WiwA/gABwGgWBgBoHA1z5jQqDDQZpTyrCtdwxZn56k14rhERxV9cH14TdpRmGzhoa04/OmoY3z99BN/SVOFJwUqaxzHScrERcVd0U4jLcjIj3wsL5PqqH3QkJSUeqIyyCfEwniZ+NQYReDMjKIYyDlQydyNnnEERDkRGJL4sby9VlvmhoXDhLIqe5qzZ/ib2PcQYtx3WiCmKo5GdM1M3hhCu87IqFtosby46oPRYSEqospbPVS2mzdF43NYcxJRXTmGIVgikZMxWyggpl4RlWg3YFmqNVHaTlhY90klLZ7t8tt7hvNzzCGJMK7UYUpCDKVV1FxrCnO3CqqaJ+h76XSKS1Dxo8iSSluWUnT07Mvj68w6pKKqcQZyuOpEVuTcsKYQlTqqaL2Rr+JVXNaokJEmLSQlSTi6G/cem9mDCs8cjJ5BnYQVLJiLz4IIYzVMqosmdC5trJUWlRYaWcTKvV5T7GwvPXc3Bxl0czL5RSMIIl1xNXdEOKqEOqas3FhMy1lUXEFh6xAtFRlkE6JhqHymZwwq6GdGUwrsORTLi++IIMV0qZU1pdLC+Xfdbbo8P0wW68nub4mFofacSo518cy6wpTIEdirmpm8KcRVkKmK8viQtj55GkxQ0NxQkxbPVS0iYSo2E1qjCluO6JU5iqEdStqLr0FEMiCqyca/DROLlGWXljQtHHmLct0+W+7h8JpJhTElFI1FFMRxnZ0XETrHGdkORMYSmhr4TKq9/eEhJEGmLcspOkl19E0mNOcrTCMnOMVijM7tipuVFEKvndNQXtDyef2ct6EhosxLSkq936a+Yt83cEGISKZkQwpGOI7MmLz4cQ5nsV05r9gNu191LXzws85+s9iyl2L+5ts3uFFVJxE1iClUU6kVMxScc4rp52RMGxoXOpRXE/xIeSINrtYs4uRsNUTP3NGFRBzoinEIwplMuamP5zistisiNLxYTNpRmGzRoW04SjL6WPsbCeffxvHEXQxE1jiO4pWK3Ny7xxikeUiKjyceFqfFIg2KPBNnDyL68Q3RsfxM/GoMK3hCJyiCMQUjkbU1eocYrvM6KhL6JHqJhU4fkjQmGHkei+5/i8Wo+vm2FFLcIqo4pmKKRnRuRtqIIZUkZ10t/CRKH1iPJ7xYarjSPUki1rPr8JnZ0g5SSikXnEOzhUIyNyblBjHTRGVNNbg0nG13LXWhY8qySPV7vs9f565+vKOQk4jIhRSsUdXZMxX0gohV8Rl9b08H/RKqxLc8PPI9bvYspPmruNbHxZhXWY7rxRDKcZbomYi5c4zrQ6qiS7gN2HqrmtQ8NLKbaL63L81O4v9m4MMiRDKnlEKxxGdFbk7TDGK9yqqvJpIXFtZGP2nglphddNQx8jZSgm9ncOK3jmTVEORhToVFxVzJRTOtyMmrNpIahuqqi5wSFrEDWT104uRsLUXC8SoozcEKnIKd3FMpEbkXqGGKRpHZOLbDx/LvukupPD4cLK9F9z7H4nib2lkEFewpm9jiMQZ2M+KmEsYYyIIzJxZOEj+TaKg3tFhdOFler1Utpd7xcJr3GELcIycwgjMFZSrmpn3OIdMEImoa3hZvE53JbWjQtmHlexbpSS/Zoul5rjlLEO6IQUzEHZiLmK22FMI3mImoJ/AklBPZnkqWFi3SSUtyylyev6tt3cOOQk4jrzjkYwrEVdTFywghnqdHXRewEphvZzV8sNFiG+q0JMbpq7mXzsSQIixDqiHOZijpZNbk7SjHItBEVME/QbstZGLaosNJMf5CVeU/TU7XrnZooyJEMqMQYjHGaRNxFJwhzlap0Rmm4kLjVsqnpUeFkiD+qMtzdFynm/O3xR38IRFcgzucVbriYjaIKYjyFRUaXSQ3k+jpfvCQ3zBbIqcU/Rc+gu7neEO1BzN7FGYhxEsuamEqIc7LIRVoeuHj+bFK18weCacJdeRSGpPlKjYTiWGFNKIZMoId2HciqupuUMMZVldU0knjxOLjOsmrFh7MPMWz2SlYmv42vnSCiGmFI3ucRnHdiLvti+MIVEFZU4SmhJ6H12aWpYsNIl7kuWRvvNi4fPxZBiNKIReYcrECIVFbkzrCClXzsqJavgtcNRCFt5Iaey3lJUkr0ltuNXPxZRnacxX8pzqYdbM2KnaQcpk8zKiSbsevCVQp7ULDzzF+z2NEEyL38SuE54p3aZLhm8KhmCtRn5uLRRyFLdnbWvuwmN/oxaUEiSTH11o1y5Lj++5mccRNEIy+51YpUddTFxHKVV90TWriz62aqPuknyx+ZFSpcLjc2Yus5k0pE1CMRKEWi4mpvMRWWicupXnrq7yKTUJXKF5kX1bOzNXMVqM6c7JqqROZkVNXlzmQiZ6JqLuYXUxPbNXfz35FTVTs1vbEROZk5HRfRXX0RkxMVMonInIiJi4m5moivyN2s/pipy43JiYiIqqnKqYqoyaqKvLy4qoqJyJqZq5iLy8mYrZjLionJi6mpiJzUXkRGTEZMZNROTNTURU5dVMRExMTm9cRU3U1NRNRE1eTNRNREZdRUTUVMZM5MRE5eTUX11NTERMRV9tRExV5F5NTEVeXl5dVNTGTOXkxFRNTUxU1F9dTU1NTEVdRV1E5dRNRNRE1ERNROTNVeXkxERUxERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERAAAAAyH0=`,
        "./sounds/Menu_Phone_Button_Note_5.flac": `data:audio/flac;base64,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+aNz7ZukqGhKRAtmTnSt7+u2oWjhXQw6axTlUQqUTdXNuYrLV0XCV0JXje603BJ9uLddZUL9fbCcusEXiCJnkKrHVCNiJi1IREdl5f/hs3vVKatb6r0TUZP/MTO5vU6cRlRSMyGTlTNTMd3TGd/Xez+zFTX1K/ydVtZMX8VcxF5nVKqnIzKrMmoiYmq6ojJqLjaTMROTtQm6i8icibmLybiqlV5VVNdNV15ExV1FVl5FTExM7V1E5/bNxV9vXlxM1M1F9ORVRU1UZOTl1ETFRkxUVM5tTdTOTNTFxOb11FXV1ExERGTGTkxGTGRMZM1NTVRNRERExF1NTm5dRuRE1OXm5MTkzUVNVfb15MVERV1E5eTFTU1MVdTUxEVdRNTEVMVNTEREVeTFTURETkXkTFRfbk1NTUXkTEVeXURERNRE1E5dROXUREROTV5MREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREAAAAAF46`,
        "./sounds/Menu_Phone_Button_Note_6.flac": `data:audio/flac;base64,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6QlGSJLfWk7PoCnaUFfnHFIcKpWfGJogY5VmIilk6aF4uSktbwHi1QWIX0iSXC7cPU3OHFWYI61OEUw5FZsR8MMdksRce9Kx8fbvSnA0SqGkb2Qsn5pdzVinSg6qpjsQRlds5s4Z3WrpmW2k2ZSQmG4Wbkb0VUSTcx9fVMVkFVUs7sUi0bFXaEdlQrau/1Ka1E3+NfKE9ORV/t5e1Fs7IVk9CMzOiomriEZ01VRO3P/n9FzNvqrqKiKTMxG1E9mVSLyMRaIyJqYierryKi62durqtq6flbWXVRszFbUTmVFdNRkZUZNTV1FVl5FTW1c3ETlxF3ExFTUVsTUzFTVZeZMVVTlTl5cVE9NTk1ebFbET21MXOXFRFXV1F5MTk5EVERVTVTl1OXFVNREVdRebGb1xERvXFRNVuTERNRERVTFTkxU5MTkxOTFTGXGbUTUxEVdRMVeTUxUxEREREVNRETkxUxUTV9cRm9vXFTFXl5dRERNTlxl1ERNREROTEVeTERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERERAAAAAIrz`,
        "./sounds/Menu_Phone_Button_Note_7.flac": `data:audio/flac;base64,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`,
        "./sounds/Menu_Phone_Button_Note_8.flac": `data:audio/flac;base64,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`,
        "./sounds/Menu_Phone_Button_Note_9.flac": `data:audio/flac;base64,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qoLEtMPLoaPYpQwdWJSWbZ3JtSDDKpdHCh0eSAs0ZY4IUIomjOosW1xJdCwtjECAyMtJ5OlK/VKcdHNo4QEcl4Gjx0igpR2PgzqJPLIefB4XzFHBnVaSzfIZu4og6sXJggKUtIHho6igVwpD9OynnnlLPkkPxiigzs1Lz1kO/cYQdXJScIHK1YFgsZBQM4QpaTuQ88SR4uiw+HIMCmRTXmrYYn0cQIztaIMCmasFgSIxwMQc5azFY089Vi5eG65BQQRNLee1AhP9zhGZbTjAgpKQ8DSCMByDne85GJPPzB9iQ0w7goUIMM3kyUMq+Uwrr01SCqJNBIBMUsZNYs1XTZb0IJyCYMKEBQxTOrZUd08jisr28hSHtGg/eWdGVr8yrWuWNqGyUwoUYxHRsKd1QjmRUrruUvywv0LZkVbcyr1cdd8m3KYUQhUdcYRVRlMyITCK60tP3PVl9fx0R9xm1VyhSmEZ2VNUrKjMzImZ7MuVvTl101Ox11e121U2jEKZ2VVTSMjKjsmLiq651e5qIiq2Kirm+iIrUZyO7KqorMqozKiriqyZtN5MZFTUxkTER11EVnZndFVNdk1WRWTERVRMbdVPXUTGXkTGVeRNdHZWRlRVVUVVVFRMZFTGzkxkRERUVUxVX0TXTmZVTlRlTWRFVU1VVExMREZNVM5U5ORk1UR0VkZVRWVkRVVETkVGXkTOTGREVFTkRVRUVFZUZUZUVFVU5NVE5NVNRFRERUREVU1NVk1FVUVVVFZNVUVEREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREREQAAAAJ/s`,
        "./sounds/Menu_Phone_Button_Note_Pound.flac": `data:audio/flac;base64,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`,
        "./sounds/Menu_Phone_Button_Note_Star.flac": `data:audio/flac;base64,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`,
        "./sounds/Menu_Phone_Button_Pound.flac": `data:audio/flac;base64,ZkxhQwAAACIQABAAAASwAASwCsRC8AAACJ0TsScnhb5ycjJibvkMxZ8ThAAADgYAAABSRUFQRVIAAAAA//h5iAAInCUYAAD/8P/Y/7gAiK3T7zLOi7p2VtxHbapLvJ7PZ0VKkyTNI1qW+LjsXsWJaa42xCVDfVi56qvX//TpSJjQbjUkTp6Vj98VUXkcSthIxMmT5Y/d6LC+KnNxGIxhEMhMzpS91Hpz9X+tEUNRDMZBI2mVapVfni3psSBMQyCcZyOgsKdSlPT+fJjGYQYxBmMiVOfDuofiuevFQlIE4NjDQxOkdBe5eL+chyUjQEgyBMTGKYXFPx3Fc6v1MUZGECMMIMQhtCFvF46iiqV1VOjYbCDYQ0NpP3g70VXhU5nBOMYyBsQ0icsOU6hSlcq3pkMQQwxhjGJhFReosBYH4tFR9TQyBIEZiNGdHg9c5YPYsyEYyAkBIGpoxUPynFUc5XUs3YSgmDGEMIbG1nV+HwLB+X52pmgZGGRjKI8rvxeKvFy6ZiGIYZgiDQmsdXcLw6jqenS5mMQwbAjCYapFjld3HosFZ0hMwyEMRDUjsWuVTuUtLbERhEGEMMw2NkWKV3CqFUU/V0uMhBBCCCGQbm9j8WjwPHVdUxMIbDExmUz+CwKriqVf5kQxBBDCCGEgytVVzuKUdzqeLEhkGYwxCBONUtXueiur/qRxuNhkGwiEqWv3FUo6qPFlxGYYxghAiDY317nUVwvCq/eRoQxCGCYxmrT1+VyqouexNhEEQIhmJMjwqlOVwqqLjtKMhBAiBhDENjKzvBSi0HgWD527IQhiEGbBFJHY68p6KsOjkhMIwyDMJBLMqqdznOdRaVHJiMIIYYQxhEZIs4qnUcrnpbU5mQwmCIbErd0F7ldy17JDYQjDDQMiMn+Hg5TuHoWu+1IbBkBGGEYxuYpD8/Ocrue9WqIIjBMY0EUixX5TqotLljUZGCEEIIZsS5+7ilKOdR4LpyYbBkCDIEYiMrPy0LTi9S+dqbGIQyCY1icscfBVK9OnyMxhGDMMZiIyyHdxSnHUV1XpWYxCBsCMw2xCkFw+D9RXwsRTcbDQIzERIhceClKpzwXW4jEIMIYQwyIaylg6jqFcV3VUVDYgTBDEMZo1Q8U6ufi4tlQmIwyCIYijL47qKopS0LqqMjDIGEYEwaG0xao/FHUUpe86YmDQIwgiIJYhcV+Lz91s5uIhmGQRiJMh8LQsB8C0/dHNRoCYEYIIw2JouKVynFcov5UuQzIMJgiMTZWPClgeqVc+aEwaDCYIiNraLSilKOp1VZGxiCGCIBKDUxFRYd3OdxVKq1HG4SCGIZDIm6FpaPRV7u340QNQRjEMRNJSq5QvCqKeFtTEYQwgzBiGxkt9Fpx6KUsPdWoyIEQTCJk6Fh+Urup0OicTCMMYxhMNzevB1KOdxTqrzcajEEGYIYhkap1V1FU7nh5OyQJQ2MwlNZnp4OrnXn1qMhBkCMGgRkhF9FopR+OUtLakIxmGIYJBGTVDoLSuLjllVuyGxkEaCRPI/Pg8cWwcQqEo2MMhhEIyS5VUqlOpVLHT5kQTCIJhOJai3q/o6K+urQiYzQkJbJ3jv+/89RKaGwmYkJNUKwqH9d8dnRzXNDUiQnMUQ7ern3XWTk6N0appJJbfL86RSOb7JMlyKR9l1+qkcAAAAHsk`,
        "./sounds/Menu_Phone_Button_Press.flac": `data:audio/flac;base64,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`,
        "./sounds/Menu_Phone_Button_Star.flac": `data:audio/flac;base64,ZkxhQwAAACIQABAAAANzAANzCsRC8AAACJ23FmBZ8vYQ4kriVJl7cTN8hAAADgYAAABSRUFQRVIAAAAA//h5iAAInCVKAAD/5v/N/7b/pP+XtKKQMNTQMKKHAIA0qkmi8S4SUkJcJS0rVpNKrKSoUVEtFUulyRxKksRGIKRSEoicXpP1+mkmkIpCiFQItCmhPW5+1OonESSEJJEiJJaVJ9yeXitKIqKETRVxfLi2ImhUFIkJZC1XUdb6topKEJCIhCJCKRL0jc/Ho+5aIkhCIQogJZCVJXNqO3a4pC0FYhREshOLROWyLqEskioSMhOL5lvV6VEWQkIkRIQlkS9ZrH749TIpEggUgQUhBKEtbLPmZrP5ekSIiQSQhRCoU0W0ty+ypKkikqLQmxXcrioSxC0JEWQuuJvW3JmrUShIQSIQREJILymtvjHo9NkpEiIQkghCIkJLS6/021PF5C0iFISKiK0tE5atUUxFQkqSq1PqeXiXERJBFCCSJIVpPs6MdP+SLIIiIgRIIUIki9btvz/5LSIiiCSERJEVKUaEbqW0JyyFwiXELlSqrJXISiJIiKKKSmvRmvfXRFIQoQkIIhIiKVUfHM/c7kpEJEIiCEQiIoi7tyM6d1LwqEkJISxFxK9EzEuUmQWwlIkmStXkeumllZAqEESIRISkU63v82my8ikIKECJAhIIoSyv/H797lJKIShCEiIiKImi3X1r4uSSiLJZFlaLaLcROJJREUgrCWqX3+em7ipIQkQiIIoglJL72dn7duJcIiChBIIkIoispzpuftI0TILiESSEVEVpWkaE5cUSoRcFYuFtet1JyiSIiQgUgiiFYo18zTe2n0VBIgUIEkEELILRbLPj8+9xcJYRQhJBCkIrRd3X6usRPCUkhaJSXpei9JURZESSFJaF81NZrd1ookQgkggUgQsisrcfZ73byUiIiQREEUELQk5KPW327RbIUkRIhRFkWkuXJpaktCoiolSXeWatUtFEKIiIhBZCSKvUbTbTOjSZJESCFBBIgiKIqWcmc+NrMlxJCEiERIIpCorSdepsUZC8ikKiKVknSVpWoRSSCiJJIXqo5Z9OvEmQkIiEIhEIhLEQyI9eb9vjSyWQRSCCKIEkURNE9b5+5aVoiUShKRUil5KlxJkkkshOC2imuJ9pdCWRRLCC4hKIuJa2Wcv5KsiRSJESFISiWl6UaJ6qimQvSJckKolRSsUyLkVkJ4TxeXiXSUlkpUEmQoAAAADefQ==`,
        "./sounds/Menu_Phone_Button_Unpress.flac": `data:audio/flac;base64,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`,
        "./sounds/Menu_Phone_Call_Accept_1.flac": `/assets/Menu_Phone_Call_Accept_1-CW9qIXPq.flac`,
        "./sounds/Menu_Phone_Call_Accept_2.flac": `/assets/Menu_Phone_Call_Accept_2-DTO_8ZTc.flac`,
        "./sounds/Menu_Phone_Call_Accept_3.flac": `/assets/Menu_Phone_Call_Accept_3-CU6ZMw5K.flac`,
        "./sounds/Menu_Phone_Call_Accept_4.flac": `/assets/Menu_Phone_Call_Accept_4-D2KNxMs4.flac`,
        "./sounds/Menu_Phone_Call_Busy.flac": `/assets/Menu_Phone_Call_Busy-CCrdmG-x.flac`,
        "./sounds/Menu_Phone_Call_Decline_1.flac": `/assets/Menu_Phone_Call_Decline_1-DL8gB_38.flac`,
        "./sounds/Menu_Phone_Call_Decline_2.flac": `/assets/Menu_Phone_Call_Decline_2-CdhBKfue.flac`,
        "./sounds/Menu_Phone_Call_Decline_3.flac": `/assets/Menu_Phone_Call_Decline_3-BqcgbppA.flac`,
        "./sounds/Menu_Phone_Call_Decline_4.flac": `/assets/Menu_Phone_Call_Decline_4-DMetvsmd.flac`,
        "./sounds/Menu_Phone_Call_End.flac": `/assets/Menu_Phone_Call_End-0p85feCv.flac`,
        "./sounds/Menu_Phone_Call_Incoming_Ringtone.flac": `/assets/Menu_Phone_Call_Incoming_Ringtone-tcfr0-Ub.flac`,
        "./sounds/Menu_Phone_Call_Incoming_Vibrato.flac": `/assets/Menu_Phone_Call_Incoming_Vibrato-BVDJIInk.flac`,
        "./sounds/Menu_Phone_Call_Outcoming.flac": `/assets/Menu_Phone_Call_Outcoming-DroCibZq.flac`,
        "./sounds/Menu_Phone_Call_Unavailable.flac": `/assets/Menu_Phone_Call_Unavailable-Bhfb_NoH.flac`,
        "./sounds/Menu_Phone_Down.flac": `/assets/Menu_Phone_Down-DMtwIBkX.flac`,
        "./sounds/Menu_Phone_Game_Bird_End.flac": `/assets/Menu_Phone_Game_Bird_End-DLoeghHc.flac`,
        "./sounds/Menu_Phone_Game_Bird_Pass.flac": `/assets/Menu_Phone_Game_Bird_Pass-BAXTHoaO.flac`,
        "./sounds/Menu_Phone_Game_Bird_Tap.flac": `/assets/Menu_Phone_Game_Bird_Tap-CjgRge-O.flac`,
        "./sounds/Menu_Phone_Game_Button.flac": `data:audio/flac;base64,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`,
        "./sounds/Menu_Phone_Game_Exit.flac": `data:audio/flac;base64,ZkxhQwAAACIQABAAAAJ4AAJ4CsRC8AAAAcZaFQKOyD0i7NmDBUN1d0BKhAAADgYAAABSRUFQRVIAAAAA//h5iAABxRASAAACUJ8EUYa2KVj+myViyHy8eCFwCXwAKgAAD1g1gMhAAAc+LDAAP46SJAwAAcaD3w+AlAoMRqx8SoW0GchNpORpwHMAMm1UFtgeKF9nCAFeBnYE9gswExHcENgUe/gI4lLxCTQIpdLJuUCmivq3amTfiLyF3IyfGWxDR27stQZEvgKT6BgBUZNB+uhALmLzBkrwerl5lDDJ3UseGP/ecEOYIQTGX9bs5NqqVrMUarhMWT6UaCiMw5hGZd7D1fHnhitXChhcBDAtEcnFnfW8oDWl3NcWTeuRsJdU1T8f/vGw39WYaWjHwCNmVMTNP4p1SGKu421fpXb+TBvypk+s8/PjuFeegkByKD4DJV0onZCH4Tw9v2VBIajZeVTlSx5tmgUxcXr8TrirWJ0hQjQFxaekuz4URBTy/a+yCK4rsOprqq+FNU0L9JFcm/mF1LX1C2R/XINVQtdauZDbRzVi1tN+b0zSdSXFaFBXpG8gzLQiHJ0+dDNO4RR0ET+qeCscRIjjHfLkGUPPnHFIZlagsDge7oi2iFsnnSacx4nn7kUlsVMVdEc+6HoeB0HW0LyVgOw7EgS+JhlGIJGoqNYWxePvIr2U+hy+kLglSXMQvCssvtOGcZwOIo84WaOsh6aLIurd6SyZEtIVMh/Pt4zOXpa3OCry92QU4SlSE8bHQFt//L0Z3zc2QQjAE22jbV/RNV125rLsxblS2oz7qkZRveuByVCFKU1RMqw9f0xaDDtqR9GsIRpOcbwHQsnTVcMWwNU85WdTUEzHRNEZJDUZwlAEJwLXFUV/c8yX5M8+0HKtGQ7I8VSvE0JAAAAAoMs=`,
        "./sounds/Menu_Phone_Game_GTG_Key.flac": `data:audio/flac;base64,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`,
        "./sounds/Menu_Phone_Game_Lose.flac": `/assets/Menu_Phone_Game_Lose-BTbL82rd.flac`,
        "./sounds/Menu_Phone_Game_Win.flac": `/assets/Menu_Phone_Game_Win-D88zyerw.flac`,
        "./sounds/Menu_Phone_Game_Wordly_Delete.flac": `data:audio/flac;base64,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`,
        "./sounds/Menu_Phone_Game_Wordly_Enter.flac": `data:audio/flac;base64,ZkxhQwAAACIQABAAAAOgAAOgCsRC8AAAAt8bK1Zu+d9QPGyXQ5WToA0qhAAADgYAAABSRUFQRVIAAAAA//h5iAAC3m4UAAD/+wInHakCsmt4ZpjxKovprbGLRHnXO2RcVwPFChdRJUKDZWIGDH9GIAPlBQANaCfgOUAAcQADtAA7IAAsYF/D/AECEBAHzAAIaAGaAA/QDuACwABtwE1AKsY7uBrwAeMZAAQ4WcA9oAD6l2AAABoj3AAAz4BeQB7AAQIBngF4AD2gAAqJUgAVgAZwDNgAUkAmBcgDvgaUCsgDlAA1gBgQfUBnCTO6AuiIi4jAB27CJxnBAgLaHtBjk1EBqTt0TcEFIOEaAfAqT9iA8wI06oS1YCoUMeEFNzZIq/25DOmMLoTbdEkZBVSh5IsU6LWOi3eVa1O0oEOzRT5QJ0WCyU2OVqYQa/cD9irmBqvcpS/7PWdK7McpVMmEIY7qSWAMHzbL47upqF3boY1Y/svSwaa55CReL8Q3nq/L/RGWdeGS/ymcXY2lEjmSZZaL5m0Wzfv3O0swVvJux6QQ3U43KowjUzq9sQJLuVj7TbJBKtf86Lk6fJfXN4lVM5EofhEi6FAtsGwVKleR1+VltjwdVlsakUdmV40F2l1KpFwjEJitpsMtnd/qc5nMxl8Kn1MqlOqFArk6lVClk3nNBotHuNNss1nczpE0llAt2MmVGnlImUdttHl8PledvdxjEYhEojEmn1kuEmmlajsAhU3gFhp9NtcclcZjczllYo1EiM2rkEoE2gsZgkrt8MiGbv81jk0isQlUZllitFkh0Rg0AkEco9dvM5h8rhsChEPiMyrVwl0dhFOjkSgURnd1ksFgMZltdg88lEWm0+hEMlkWmkbjcOnslgEHlcfhMMm8OnkJj0Wh0OgcogcZhsDjcWg8CgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgEAgAAAABkOw==`,
        "./sounds/Menu_Phone_Icon.flac": `data:audio/flac;base64,ZkxhQwAAACIQABAAAATZAATZCsRC8AAABAATFLmzkz2vIV2pPMYsItI1hAAADgYAAABSRUFQRVIAAAAA//ipiADmFAAA//gB4GKqIK7pZ6KsMSTVcusuCeTcNgMnFgK5QIuINCtSPlC1jRM2JgzWLDV9EKWOehWY4lRuSZKidsHM6e8AoooFSLAcIF+NgB/bYXKMwSvLAIIESTYFoyTZE0ZpoiOkVY+Dlvbuidi513Ii07POhFTceo0jbbO07kWz403MQR10FmCdD9kAaAEAAI4RgD7AyQMUOkSQKw4gHoYhTgPMBcu4FSOYAAeXaJtTAZ40WQYbG6ROBATYBpGSBPitAYpCgosUG0HtPDkYAMUzQHGBp0bUeAAdgAWBSdpfAZ4AAAABHgAVQAOgAaiDAAcYEsABTs0ACmAe5NgDMBBgAAdoACOABHOcAAAK4AAEdgAAAAAKsAf4AAAbQAAAAAAAAiwAAAARIAAAAAAAAAAABZAd4AgwAAAAAsAAS4AAAABRgAuwAawACJAAAQIAAAAAEwAKQAA3wAAAAADquQAAG2APISoAcsUAPkAB9IAABugAMjRALUCKAAB7gAAC9ACCAH4IgADCAAGwd4AcpEg2h+k2AABgWQAD9FJtA5Qc4AF6A4wAGIAABiGOAAHGYICYNG0AFYP0AJ8AI4AAvn+CwGwACNEewR8sdggtA6QAboAZgAfABHgL0xCfEOBnBnSYEYDyFvaV4MdFopoxy6gq8a8Yrwud9Gyg/B+PSFFMLhvk3d+2A9f5pjNEJOoWtdanSaq+bsUGDguZJHJ8Pv+0o1AveBn5bc91Kle9OJCX/bfCyvxDVOLB1vorEVRWtDLwnT/t1dzdbCLbohNCglZLMgoX6uaFNRWjRR23N2N7UzN3N2dvJWF5UQl5eXH5oQEBJ0cfYzNLm3dXARlLS1c3FUGViWlRaY1VYQ97cx8bK1MbC19DI2NXTRENFxWBdWlhYR0NLQk7CxtrLz97RwMTbx9ZGxuVlQMTGzmJJRt5HT9xGZ87JcO3JRtjB5URg011ZzGZGxGVEwmBYRujGTMrH2cfc0MJAxUNMyU5VV0xgQ9TSalBN1cfN1NLKzdfAy8RATdlTSN1TT1JATEFAQkTHR9zcxMXKz1DkyeVIV+VDT9RYVkBFUUVCwszPyMrAQkXY2sJHwM5VQMTCUlJXQs9uZODUTGrFVVXVz8vDQFnH1ktBSW5F2/pJUcNGQMHNxMLexshTzkVZxUjN00NKaknNVFfKUFLNRs7QQE3RzNDGxMnOVUbORUNBz9dWQEDcRsjF29JJ0s1J1sHMQkXIxMlYQ0lKQ8jAV0BCxE3SQUnD48JL1tBWQ8jF0FxZy0VL3UTC2kTc9Umo9CCp5aJnY2mh5DFmcy/4vDXwIC5lKWQmsekxsuz0JdGz0ZJSmPzYkbWRObH0csiouLkMOniqeFhoO2jZeqo4XOzaWcgrKmgJybmZyrkofbqt6Zhouz347iq4aSgIOw4LmjoLGPpLCKnYuLpcWOqsOamJCuj6WmwuGS2LuQxKWHjKmRgIGvk4WZspCCgpSCh7iBpZ6KjrDHr7rBnrLpnJ7LqeyIyZyKjZ60g4mOh4CGjpqng5OfubCov6qFg4ahiZCatKeN2KOAqJGEmY+lmICdrLuBhJ2uiaXsirGswYzOn5ConcGRmIKTgaCPqaKTjJCpwJKHnZSclYaBm5OOkI+EiIuCgoaAAAAAA6EM=`,
        "./sounds/Menu_Phone_Up.flac": `data:audio/flac;base64,ZkxhQwAAACIQABAAAAAQAAaRCsRC8AAAM64DLBvxsOLukkACczMK74PnhAAADgYAAABSRUFQRVIAAAAA//jJiAAjFAAAAAAMP////////////////////////////////////////////////////////////////////////////////////w//////////////////////////////////////Qun//5f//////////////////////////T//y9C/////L//8v///w/9JU/8qf///6f/0//8v/T//6cv///l//L/9C/9PT5ehf/lp5f0l//0/lp/00//8v9Pl/5fT8tNJeX6F0/KXy8vKXLL/8vp//0p//T9Kfp006fpKn00+helKenlkEsvKaUpS+UvpKkpeX9z6U+kv/TSz06aU/L5ebQs3L5dDfc+n09NCJ8rPKkqfSkuUtNNC6SpypTcpQjlNnspnfTKRMucrpPp6fuaXl5bKUypTLOMpsnbClylsl/TI/yXnyoVYWzOlkT/K/TwuFLs0qHaYWnSy0NpMZPUvpapa168ko1ypTKq8qdJLOS1WkuaX15K18uJdL5qpE3RK5VJyLp3VLi9LS9XSS6TaJPlIX0ttKtZL0W4lH3Ii6aKbSviRM9IqTpLk+otVo5Fvko9JFn0ReqSZVvWtUZFOkicrmkzIKcLlDWQodIreQkb+RUvQSfpW6S0orm9E/UkJ7ZFZb2RaEQsbGxCConU2lKekJ03hBPjxbMIti0twobiJDgU6y2wqLMiJNqPYQRDL6WJfm2kXar4k6obfSVVxCUVU+2IRXs0pWEN6kp/khVhCIX5tyqjSRpPZkX2MZk54ybrDIv8GTRXkeaJtayPUyI60m/xM1cv7DZ6iKeaInJC7+TM2uTHFvQQnpjQ7yyCXsRx6RHtmYZmCh5oxhKPQQRxbWYpYITnpPJBCXu5jdblVEKYGNXiiuSFOycwRTxBELKnjIFHIGNRVENw4VAJDvzAQTFHKZB6EbxVxBMrjwYRBxTE5UMsaGEU6Ec8dhlOQwoFdJRAzPQSDlFcgMZylqxGEGKKoghxYrAMCCCh+YYcHsQEBMHBaQIcUgkgYJBcBmAo/qc9Ag3uRjHB1AgYoODhxgGCaBqCvkIfhHA4PjU6mZShgESH5SihyNqf/SGLSUQRV3GF5pLXRhSvaYqiQ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k8upNlEuqXsiS7pmktJ6Qvc0ovoi7Inx/FtF5JZ5JRSm6JRyV3pEck5dSIL2puIs0tWtpRccSNkRN5UlZKtTW9JJ+q5amVahKOk7pKySbS/RS1NpO4i+rEUbxTryKWqXtXaRF0qdktrpbkqaJST1q3Re+Qqi9a5fkpKTsIJEwtyUpNrIF0YFOLmRMhTUIxsCGsjSmUJxPHGSFnC6KSdI0JLNtpwsOkQ6RGSly5JtkuIYUF5LP5WFpIYonwO5sOME1zDNC+LzJK5tqmEkqyk+92F0hKV7UNCzN3pklYdqMLIUtllSSEYJMkxWqSBChUVhyly5yI05hlpGnNwrKGGiqcDDjN/EkWTP9LPokELmfpulzJmiqTQkv0xuWSTScRT05yns2VD1KTftCkvOEY0mslmZeOhc56kSVkkrmcVlIdpTyJeUPNXnTcvkmJSJTLGeZ+895SpTcoXL7nKaV86eWb4nlk9PX8zQpEj+SlSdp06F/PubaUMym7qZLn7K0oczxK6U5mWlWYQppbKaWUpyl5ZSlTl00yl+5SU/iRPNNCz3UnL0pZ6ZdC9mmWUiadOUoVNK5NN6WdlOdpT8v/lylylsp83+zll985SmlTSlMpS0iZZzSlIlKeUpafTwqd+mhSl9LPKXpc6Z39NJcuX0/5ellMppZfp5SyJT+n//wy/p8spdKU/5fc+X8iaf5aaf/08v/TpSn5fQqZZfpp+U0vT6ZeX0y8v//y/0LKXT0/5enSn8tP/L9NKf/In//yJl80vLT///+ROnQv/L0+X/T+XLT//+mX8qf0/Ihf/y/Sn+UAAAAILC//jJiAItEgABBD+38/fP5//8v/n+//z/+f/s//v93///9////87/8//v/P//775///+5+//////5//f+f///5///n//+f///+5/+////9/n/99+b/3////P9//+b/8///O/5//n///5f+b//3P//9///////T/n2f/mn//f/////7///7//n/9/z////+z3nv8+/+f////////8v//5//7//////v/////////8/////8/////vP//9//////////////////////////3/////l////////////////+f//////z//vN//nc///9/O5f///////////////////////////////////////8///3+f/f//////////////D/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////AAAAAND//h5GAMDrc4AAAAAAAC6xA==`,
        "./sounds/Menu_Start_Laptop_1.flac": `/assets/Menu_Start_Laptop_1-BPgkQm6U.flac`,
        "./sounds/Menu_Start_Laptop_2.flac": `/assets/Menu_Start_Laptop_2-D3rieBGe.flac`,
        "./sounds/Menu_Start_Laptop_3.flac": `/assets/Menu_Start_Laptop_3-CLMlRzpu.flac`,
        "./sounds/Menu_Start_Laptop_4.flac": `/assets/Menu_Start_Laptop_4-8mTzqugX.flac`,
        "./sounds/Menu_Start_Laptop_5.flac": `/assets/Menu_Start_Laptop_5-BGGGzAUO.flac`,
        "./sounds/Menu_Start_Melody.flac": `/assets/Menu_Start_Melody-DtVqenkY.flac`,
        "./sounds/Menu_Start_Screen.flac": `/assets/Menu_Start_Screen-D4Ey7PmJ.flac`,
        "./sounds/UI_NOT_Achivement.flac": `/assets/UI_NOT_Achivement-DsskPtzJ.flac`,
        "./sounds/UI_NOT_Activated.flac": `/assets/UI_NOT_Activated-qwUBt7Qy.flac`,
        "./sounds/UI_NOT_Effect.flac": `/assets/UI_NOT_Effect-CAPz58Mz.flac`,
        "./sounds/UI_NOT_Error.flac": `/assets/UI_NOT_Error-BtWibJX2.flac`
    }), b = new Map;
for (let [e, t] of Object.entries(ne)) {
    let n = e.replace(/^\.\/sounds\//, ``).replace(/\\/g, `/`);
    b.set(n, t)
}

function x(e) {
    let t = e.replace(/^\/sounds\//, ``).replace(/^\//, ``);
    return b.get(t) || (console.warn(`[soundUrl] Asset not found: ${t}`), ``)
}

var re = o((e => {
        (function () {
            var t = function () {
                this.init()
            };
            t.prototype = {
                init: function () {
                    var e = this || n;
                    return e._counter = 1e3, e._html5AudioPool = [], e.html5PoolSize = 10, e._codecs = {}, e._howls = [], e._muted = !1, e._volume = 1, e._canPlayEvent = `canplaythrough`, e._navigator = typeof window < `u` && window.navigator ? window.navigator : null, e.masterGain = null, e.noAudio = !1, e.usingWebAudio = !0, e.autoSuspend = !0, e.ctx = null, e.autoUnlock = !0, e._setup(), e
                }, volume: function (e) {
                    var t = this || n;
                    if (e = parseFloat(e), t.ctx || u(), e !== void 0 && e >= 0 && e <= 1) {
                        if (t._volume = e, t._muted) return t;
                        t.usingWebAudio && t.masterGain.gain.setValueAtTime(e, n.ctx.currentTime);
                        for (var r = 0; r < t._howls.length; r++) if (!t._howls[r]._webAudio) for (var i = t._howls[r]._getSoundIds(), a = 0; a < i.length; a++) {
                            var o = t._howls[r]._soundById(i[a]);
                            o && o._node && (o._node.volume = o._volume * e)
                        }
                        return t
                    }
                    return t._volume
                }, mute: function (e) {
                    var t = this || n;
                    t.ctx || u(), t._muted = e, t.usingWebAudio && t.masterGain.gain.setValueAtTime(e ? 0 : t._volume, n.ctx.currentTime);
                    for (var r = 0; r < t._howls.length; r++) if (!t._howls[r]._webAudio) for (var i = t._howls[r]._getSoundIds(), a = 0; a < i.length; a++) {
                        var o = t._howls[r]._soundById(i[a]);
                        o && o._node && (o._node.muted = e ? !0 : o._muted)
                    }
                    return t
                }, stop: function () {
                    for (var e = this || n, t = 0; t < e._howls.length; t++) e._howls[t].stop();
                    return e
                }, unload: function () {
                    for (var e = this || n, t = e._howls.length - 1; t >= 0; t--) e._howls[t].unload();
                    return e.usingWebAudio && e.ctx && e.ctx.close !== void 0 && (e.ctx.close(), e.ctx = null, u()), e
                }, codecs: function (e) {
                    return (this || n)._codecs[e.replace(/^x-/, ``)]
                }, _setup: function () {
                    var e = this || n;
                    if (e.state = e.ctx && e.ctx.state || `suspended`, e._autoSuspend(), !e.usingWebAudio) if (typeof Audio < `u`) try {
                        var t = new Audio;
                        t.oncanplaythrough === void 0 && (e._canPlayEvent = `canplay`)
                    } catch {
                        e.noAudio = !0
                    } else e.noAudio = !0;
                    try {
                        var t = new Audio;
                        t.muted && (e.noAudio = !0)
                    } catch {
                    }
                    return e.noAudio || e._setupCodecs(), e
                }, _setupCodecs: function () {
                    var e = this || n, t = null;
                    try {
                        t = typeof Audio < `u` ? new Audio : null
                    } catch {
                        return e
                    }
                    if (!t || typeof t.canPlayType != `function`) return e;
                    var r = t.canPlayType(`audio/mpeg;`).replace(/^no$/, ``),
                        i = e._navigator ? e._navigator.userAgent : ``, a = i.match(/OPR\/(\d+)/g),
                        o = a && parseInt(a[0].split(`/`)[1], 10) < 33,
                        s = i.indexOf(`Safari`) !== -1 && i.indexOf(`Chrome`) === -1, c = i.match(/Version\/(.*?) /),
                        l = s && c && parseInt(c[1], 10) < 15;
                    return e._codecs = {
                        mp3: !!(!o && (r || t.canPlayType(`audio/mp3;`).replace(/^no$/, ``))),
                        mpeg: !!r,
                        opus: !!t.canPlayType(`audio/ogg; codecs="opus"`).replace(/^no$/, ``),
                        ogg: !!t.canPlayType(`audio/ogg; codecs="vorbis"`).replace(/^no$/, ``),
                        oga: !!t.canPlayType(`audio/ogg; codecs="vorbis"`).replace(/^no$/, ``),
                        wav: !!(t.canPlayType(`audio/wav; codecs="1"`) || t.canPlayType(`audio/wav`)).replace(/^no$/, ``),
                        aac: !!t.canPlayType(`audio/aac;`).replace(/^no$/, ``),
                        caf: !!t.canPlayType(`audio/x-caf;`).replace(/^no$/, ``),
                        m4a: !!(t.canPlayType(`audio/x-m4a;`) || t.canPlayType(`audio/m4a;`) || t.canPlayType(`audio/aac;`)).replace(/^no$/, ``),
                        m4b: !!(t.canPlayType(`audio/x-m4b;`) || t.canPlayType(`audio/m4b;`) || t.canPlayType(`audio/aac;`)).replace(/^no$/, ``),
                        mp4: !!(t.canPlayType(`audio/x-mp4;`) || t.canPlayType(`audio/mp4;`) || t.canPlayType(`audio/aac;`)).replace(/^no$/, ``),
                        weba: !!(!l && t.canPlayType(`audio/webm; codecs="vorbis"`).replace(/^no$/, ``)),
                        webm: !!(!l && t.canPlayType(`audio/webm; codecs="vorbis"`).replace(/^no$/, ``)),
                        dolby: !!t.canPlayType(`audio/mp4; codecs="ec-3"`).replace(/^no$/, ``),
                        flac: !!(t.canPlayType(`audio/x-flac;`) || t.canPlayType(`audio/flac;`)).replace(/^no$/, ``)
                    }, e
                }, _unlockAudio: function () {
                    var e = this || n;
                    if (!(e._audioUnlocked || !e.ctx)) {
                        e._audioUnlocked = !1, e.autoUnlock = !1, !e._mobileUnloaded && e.ctx.sampleRate !== 44100 && (e._mobileUnloaded = !0, e.unload()), e._scratchBuffer = e.ctx.createBuffer(1, 1, 22050);
                        var t = function (n) {
                            for (; e._html5AudioPool.length < e.html5PoolSize;) try {
                                var r = new Audio;
                                r._unlocked = !0, e._releaseHtml5Audio(r)
                            } catch {
                                e.noAudio = !0;
                                break
                            }
                            for (var i = 0; i < e._howls.length; i++) if (!e._howls[i]._webAudio) for (var a = e._howls[i]._getSoundIds(), o = 0; o < a.length; o++) {
                                var s = e._howls[i]._soundById(a[o]);
                                s && s._node && !s._node._unlocked && (s._node._unlocked = !0, s._node.load())
                            }
                            e._autoResume();
                            var c = e.ctx.createBufferSource();
                            c.buffer = e._scratchBuffer, c.connect(e.ctx.destination), c.start === void 0 ? c.noteOn(0) : c.start(0), typeof e.ctx.resume == `function` && e.ctx.resume(), c.onended = function () {
                                c.disconnect(0), e._audioUnlocked = !0, document.removeEventListener(`touchstart`, t, !0), document.removeEventListener(`touchend`, t, !0), document.removeEventListener(`click`, t, !0), document.removeEventListener(`keydown`, t, !0);
                                for (var n = 0; n < e._howls.length; n++) e._howls[n]._emit(`unlock`)
                            }
                        };
                        return document.addEventListener(`touchstart`, t, !0), document.addEventListener(`touchend`, t, !0), document.addEventListener(`click`, t, !0), document.addEventListener(`keydown`, t, !0), e
                    }
                }, _obtainHtml5Audio: function () {
                    var e = this || n;
                    if (e._html5AudioPool.length) return e._html5AudioPool.pop();
                    var t = new Audio().play();
                    return t && typeof Promise < `u` && (t instanceof Promise || typeof t.then == `function`) && t.catch(function () {
                        console.warn(`HTML5 Audio pool exhausted, returning potentially locked audio object.`)
                    }), new Audio
                }, _releaseHtml5Audio: function (e) {
                    var t = this || n;
                    return e._unlocked && t._html5AudioPool.push(e), t
                }, _autoSuspend: function () {
                    var e = this;
                    if (!(!e.autoSuspend || !e.ctx || e.ctx.suspend === void 0 || !n.usingWebAudio)) {
                        for (var t = 0; t < e._howls.length; t++) if (e._howls[t]._webAudio) {
                            for (var r = 0; r < e._howls[t]._sounds.length; r++) if (!e._howls[t]._sounds[r]._paused) return e
                        }
                        return e._suspendTimer && clearTimeout(e._suspendTimer), e._suspendTimer = setTimeout(function () {
                            if (e.autoSuspend) {
                                e._suspendTimer = null, e.state = `suspending`;
                                var t = function () {
                                    e.state = `suspended`, e._resumeAfterSuspend && (delete e._resumeAfterSuspend, e._autoResume())
                                };
                                e.ctx.suspend().then(t, t)
                            }
                        }, 3e4), e
                    }
                }, _autoResume: function () {
                    var e = this;
                    if (!(!e.ctx || e.ctx.resume === void 0 || !n.usingWebAudio)) return e.state === `running` && e.ctx.state !== `interrupted` && e._suspendTimer ? (clearTimeout(e._suspendTimer), e._suspendTimer = null) : e.state === `suspended` || e.state === `running` && e.ctx.state === `interrupted` ? (e.ctx.resume().then(function () {
                        e.state = `running`;
                        for (var t = 0; t < e._howls.length; t++) e._howls[t]._emit(`resume`)
                    }), e._suspendTimer &&= (clearTimeout(e._suspendTimer), null)) : e.state === `suspending` && (e._resumeAfterSuspend = !0), e
                }
            };
            var n = new t, r = function (e) {
                var t = this;
                if (!e.src || e.src.length === 0) {
                    console.error(`An array of source files must be passed with any new Howl.`);
                    return
                }
                t.init(e)
            };
            r.prototype = {
                init: function (e) {
                    var t = this;
                    return n.ctx || u(), t._autoplay = e.autoplay || !1, t._format = typeof e.format == `string` ? [e.format] : e.format, t._html5 = e.html5 || !1, t._muted = e.mute || !1, t._loop = e.loop || !1, t._pool = e.pool || 5, t._preload = typeof e.preload == `boolean` || e.preload === `metadata` ? e.preload : !0, t._rate = e.rate || 1, t._sprite = e.sprite || {}, t._src = typeof e.src == `string` ? [e.src] : e.src, t._volume = e.volume === void 0 ? 1 : e.volume, t._xhr = {
                        method: e.xhr && e.xhr.method ? e.xhr.method : `GET`,
                        headers: e.xhr && e.xhr.headers ? e.xhr.headers : null,
                        withCredentials: e.xhr && e.xhr.withCredentials ? e.xhr.withCredentials : !1
                    }, t._duration = 0, t._state = `unloaded`, t._sounds = [], t._endTimers = {}, t._queue = [], t._playLock = !1, t._onend = e.onend ? [{fn: e.onend}] : [], t._onfade = e.onfade ? [{fn: e.onfade}] : [], t._onload = e.onload ? [{fn: e.onload}] : [], t._onloaderror = e.onloaderror ? [{fn: e.onloaderror}] : [], t._onplayerror = e.onplayerror ? [{fn: e.onplayerror}] : [], t._onpause = e.onpause ? [{fn: e.onpause}] : [], t._onplay = e.onplay ? [{fn: e.onplay}] : [], t._onstop = e.onstop ? [{fn: e.onstop}] : [], t._onmute = e.onmute ? [{fn: e.onmute}] : [], t._onvolume = e.onvolume ? [{fn: e.onvolume}] : [], t._onrate = e.onrate ? [{fn: e.onrate}] : [], t._onseek = e.onseek ? [{fn: e.onseek}] : [], t._onunlock = e.onunlock ? [{fn: e.onunlock}] : [], t._onresume = [], t._webAudio = n.usingWebAudio && !t._html5, n.ctx !== void 0 && n.ctx && n.autoUnlock && n._unlockAudio(), n._howls.push(t), t._autoplay && t._queue.push({
                        event: `play`,
                        action: function () {
                            t.play()
                        }
                    }), t._preload && t._preload !== `none` && t.load(), t
                }, load: function () {
                    var e = this, t = null;
                    if (n.noAudio) {
                        e._emit(`loaderror`, null, `No audio support.`);
                        return
                    }
                    typeof e._src == `string` && (e._src = [e._src]);
                    for (var r = 0; r < e._src.length; r++) {
                        var a, s;
                        if (e._format && e._format[r]) a = e._format[r]; else {
                            if (s = e._src[r], typeof s != `string`) {
                                e._emit(`loaderror`, null, `Non-string found in selected audio sources - ignoring.`);
                                continue
                            }
                            a = /^data:audio\/([^;,]+);/i.exec(s), a ||= /\.([^.]+)$/.exec(s.split(`?`, 1)[0]), a &&= a[1].toLowerCase()
                        }
                        if (a || console.warn(`No file extension was found. Consider using the "format" property or specify an extension.`), a && n.codecs(a)) {
                            t = e._src[r];
                            break
                        }
                    }
                    if (!t) {
                        e._emit(`loaderror`, null, `No codec support for selected audio sources.`);
                        return
                    }
                    return e._src = t, e._state = `loading`, window.location.protocol === `https:` && t.slice(0, 5) === `http:` && (e._html5 = !0, e._webAudio = !1), new i(e), e._webAudio && o(e), e
                }, play: function (e, t) {
                    var r = this, i = null;
                    if (typeof e == `number`) i = e, e = null; else if (typeof e == `string` && r._state === `loaded` && !r._sprite[e]) return null; else if (e === void 0 && (e = `__default`, !r._playLock)) {
                        for (var a = 0, o = 0; o < r._sounds.length; o++) r._sounds[o]._paused && !r._sounds[o]._ended && (a++, i = r._sounds[o]._id);
                        a === 1 ? e = null : i = null
                    }
                    var s = i ? r._soundById(i) : r._inactiveSound();
                    if (!s) return null;
                    if (i && !e && (e = s._sprite || `__default`), r._state !== `loaded`) {
                        s._sprite = e, s._ended = !1;
                        var c = s._id;
                        return r._queue.push({
                            event: `play`, action: function () {
                                r.play(c)
                            }
                        }), c
                    }
                    if (i && !s._paused) return t || r._loadQueue(`play`), s._id;
                    r._webAudio && n._autoResume();
                    var l = Math.max(0, s._seek > 0 ? s._seek : r._sprite[e][0] / 1e3),
                        u = Math.max(0, (r._sprite[e][0] + r._sprite[e][1]) / 1e3 - l), d = u * 1e3 / Math.abs(s._rate),
                        ee = r._sprite[e][0] / 1e3, f = (r._sprite[e][0] + r._sprite[e][1]) / 1e3;
                    s._sprite = e, s._ended = !1;
                    var p = function () {
                        s._paused = !1, s._seek = l, s._start = ee, s._stop = f, s._loop = !!(s._loop || r._sprite[e][2])
                    };
                    if (l >= f) {
                        r._ended(s);
                        return
                    }
                    var m = s._node;
                    if (r._webAudio) {
                        var h = function () {
                            r._playLock = !1, p(), r._refreshBuffer(s);
                            var e = s._muted || r._muted ? 0 : s._volume;
                            m.gain.setValueAtTime(e, n.ctx.currentTime), s._playStart = n.ctx.currentTime, m.bufferSource.start === void 0 ? s._loop ? m.bufferSource.noteGrainOn(0, l, 86400) : m.bufferSource.noteGrainOn(0, l, u) : s._loop ? m.bufferSource.start(0, l, 86400) : m.bufferSource.start(0, l, u), d !== 1 / 0 && (r._endTimers[s._id] = setTimeout(r._ended.bind(r, s), d)), t || setTimeout(function () {
                                r._emit(`play`, s._id), r._loadQueue()
                            }, 0)
                        };
                        n.state === `running` && n.ctx.state !== `interrupted` ? h() : (r._playLock = !0, r.once(`resume`, h), r._clearTimer(s._id))
                    } else {
                        var g = function () {
                            m.currentTime = l, m.muted = s._muted || r._muted || n._muted || m.muted, m.volume = s._volume * n.volume(), m.playbackRate = s._rate;
                            try {
                                var i = m.play();
                                if (i && typeof Promise < `u` && (i instanceof Promise || typeof i.then == `function`) ? (r._playLock = !0, p(), i.then(function () {
                                    r._playLock = !1, m._unlocked = !0, t ? r._loadQueue() : r._emit(`play`, s._id)
                                }).catch(function () {
                                    r._playLock = !1, r._emit(`playerror`, s._id, `Playback was unable to start. This is most commonly an issue on mobile devices and Chrome where playback was not within a user interaction.`), s._ended = !0, s._paused = !0
                                })) : t || (r._playLock = !1, p(), r._emit(`play`, s._id)), m.playbackRate = s._rate, m.paused) {
                                    r._emit(`playerror`, s._id, `Playback was unable to start. This is most commonly an issue on mobile devices and Chrome where playback was not within a user interaction.`);
                                    return
                                }
                                e !== `__default` || s._loop ? r._endTimers[s._id] = setTimeout(r._ended.bind(r, s), d) : (r._endTimers[s._id] = function () {
                                    r._ended(s), m.removeEventListener(`ended`, r._endTimers[s._id], !1)
                                }, m.addEventListener(`ended`, r._endTimers[s._id], !1))
                            } catch (e) {
                                r._emit(`playerror`, s._id, e)
                            }
                        };
                        m.src === `data:audio/wav;base64,UklGRigAAABXQVZFZm10IBIAAAABAAEARKwAAIhYAQACABAAAABkYXRhAgAAAAEA` && (m.src = r._src, m.load());
                        var _ = window && window.ejecta || !m.readyState && n._navigator.isCocoonJS;
                        if (m.readyState >= 3 || _) g(); else {
                            r._playLock = !0, r._state = `loading`;
                            var v = function () {
                                r._state = `loaded`, g(), m.removeEventListener(n._canPlayEvent, v, !1)
                            };
                            m.addEventListener(n._canPlayEvent, v, !1), r._clearTimer(s._id)
                        }
                    }
                    return s._id
                }, pause: function (e) {
                    var t = this;
                    if (t._state !== `loaded` || t._playLock) return t._queue.push({
                        event: `pause`, action: function () {
                            t.pause(e)
                        }
                    }), t;
                    for (var n = t._getSoundIds(e), r = 0; r < n.length; r++) {
                        t._clearTimer(n[r]);
                        var i = t._soundById(n[r]);
                        if (i && !i._paused && (i._seek = t.seek(n[r]), i._rateSeek = 0, i._paused = !0, t._stopFade(n[r]), i._node)) if (t._webAudio) {
                            if (!i._node.bufferSource) continue;
                            i._node.bufferSource.stop === void 0 ? i._node.bufferSource.noteOff(0) : i._node.bufferSource.stop(0), t._cleanBuffer(i._node)
                        } else (!isNaN(i._node.duration) || i._node.duration === 1 / 0) && i._node.pause();
                        arguments[1] || t._emit(`pause`, i ? i._id : null)
                    }
                    return t
                }, stop: function (e, t) {
                    var n = this;
                    if (n._state !== `loaded` || n._playLock) return n._queue.push({
                        event: `stop`, action: function () {
                            n.stop(e)
                        }
                    }), n;
                    for (var r = n._getSoundIds(e), i = 0; i < r.length; i++) {
                        n._clearTimer(r[i]);
                        var a = n._soundById(r[i]);
                        a && (a._seek = a._start || 0, a._rateSeek = 0, a._paused = !0, a._ended = !0, n._stopFade(r[i]), a._node && (n._webAudio ? a._node.bufferSource && (a._node.bufferSource.stop === void 0 ? a._node.bufferSource.noteOff(0) : a._node.bufferSource.stop(0), n._cleanBuffer(a._node)) : (!isNaN(a._node.duration) || a._node.duration === 1 / 0) && (a._node.currentTime = a._start || 0, a._node.pause(), a._node.duration === 1 / 0 && n._clearSound(a._node))), t || n._emit(`stop`, a._id))
                    }
                    return n
                }, mute: function (e, t) {
                    var r = this;
                    if (r._state !== `loaded` || r._playLock) return r._queue.push({
                        event: `mute`, action: function () {
                            r.mute(e, t)
                        }
                    }), r;
                    if (t === void 0) if (typeof e == `boolean`) r._muted = e; else return r._muted;
                    for (var i = r._getSoundIds(t), a = 0; a < i.length; a++) {
                        var o = r._soundById(i[a]);
                        o && (o._muted = e, o._interval && r._stopFade(o._id), r._webAudio && o._node ? o._node.gain.setValueAtTime(e ? 0 : o._volume, n.ctx.currentTime) : o._node && (o._node.muted = n._muted ? !0 : e), r._emit(`mute`, o._id))
                    }
                    return r
                }, volume: function () {
                    var e = this, t = arguments, r, i;
                    if (t.length === 0) return e._volume;
                    t.length === 1 || t.length === 2 && t[1] === void 0 ? e._getSoundIds().indexOf(t[0]) >= 0 ? i = parseInt(t[0], 10) : r = parseFloat(t[0]) : t.length >= 2 && (r = parseFloat(t[0]), i = parseInt(t[1], 10));
                    var a;
                    if (r !== void 0 && r >= 0 && r <= 1) {
                        if (e._state !== `loaded` || e._playLock) return e._queue.push({
                            event: `volume`,
                            action: function () {
                                e.volume.apply(e, t)
                            }
                        }), e;
                        i === void 0 && (e._volume = r), i = e._getSoundIds(i);
                        for (var o = 0; o < i.length; o++) a = e._soundById(i[o]), a && (a._volume = r, t[2] || e._stopFade(i[o]), e._webAudio && a._node && !a._muted ? a._node.gain.setValueAtTime(r, n.ctx.currentTime) : a._node && !a._muted && (a._node.volume = r * n.volume()), e._emit(`volume`, a._id))
                    } else return a = i ? e._soundById(i) : e._sounds[0], a ? a._volume : 0;
                    return e
                }, fade: function (e, t, r, i) {
                    var a = this;
                    if (a._state !== `loaded` || a._playLock) return a._queue.push({
                        event: `fade`, action: function () {
                            a.fade(e, t, r, i)
                        }
                    }), a;
                    e = Math.min(Math.max(0, parseFloat(e)), 1), t = Math.min(Math.max(0, parseFloat(t)), 1), r = parseFloat(r), a.volume(e, i);
                    for (var o = a._getSoundIds(i), s = 0; s < o.length; s++) {
                        var c = a._soundById(o[s]);
                        if (c) {
                            if (i || a._stopFade(o[s]), a._webAudio && !c._muted) {
                                var l = n.ctx.currentTime, u = l + r / 1e3;
                                c._volume = e, c._node.gain.setValueAtTime(e, l), c._node.gain.linearRampToValueAtTime(t, u)
                            }
                            a._startFadeInterval(c, e, t, r, o[s], i === void 0)
                        }
                    }
                    return a
                }, _startFadeInterval: function (e, t, n, r, i, a) {
                    var o = this, s = t, c = n - t, l = Math.abs(c / .01), u = Math.max(4, l > 0 ? r / l : r),
                        d = Date.now();
                    e._fadeTo = n, e._interval = setInterval(function () {
                        var i = (Date.now() - d) / r;
                        d = Date.now(), s += c * i, s = Math.round(s * 100) / 100, s = c < 0 ? Math.max(n, s) : Math.min(n, s), o._webAudio ? e._volume = s : o.volume(s, e._id, !0), a && (o._volume = s), (n < t && s <= n || n > t && s >= n) && (clearInterval(e._interval), e._interval = null, e._fadeTo = null, o.volume(n, e._id), o._emit(`fade`, e._id))
                    }, u)
                }, _stopFade: function (e) {
                    var t = this, r = t._soundById(e);
                    return r && r._interval && (t._webAudio && r._node.gain.cancelScheduledValues(n.ctx.currentTime), clearInterval(r._interval), r._interval = null, t.volume(r._fadeTo, e), r._fadeTo = null, t._emit(`fade`, e)), t
                }, loop: function () {
                    var e = this, t = arguments, n, r, i;
                    if (t.length === 0) return e._loop;
                    if (t.length === 1) if (typeof t[0] == `boolean`) n = t[0], e._loop = n; else return i = e._soundById(parseInt(t[0], 10)), i ? i._loop : !1; else t.length === 2 && (n = t[0], r = parseInt(t[1], 10));
                    for (var a = e._getSoundIds(r), o = 0; o < a.length; o++) i = e._soundById(a[o]), i && (i._loop = n, e._webAudio && i._node && i._node.bufferSource && (i._node.bufferSource.loop = n, n && (i._node.bufferSource.loopStart = i._start || 0, i._node.bufferSource.loopEnd = i._stop, e.playing(a[o]) && (e.pause(a[o], !0), e.play(a[o], !0)))));
                    return e
                }, rate: function () {
                    var e = this, t = arguments, r, i;
                    t.length === 0 ? i = e._sounds[0]._id : t.length === 1 ? e._getSoundIds().indexOf(t[0]) >= 0 ? i = parseInt(t[0], 10) : r = parseFloat(t[0]) : t.length === 2 && (r = parseFloat(t[0]), i = parseInt(t[1], 10));
                    var a;
                    if (typeof r == `number`) {
                        if (e._state !== `loaded` || e._playLock) return e._queue.push({
                            event: `rate`, action: function () {
                                e.rate.apply(e, t)
                            }
                        }), e;
                        i === void 0 && (e._rate = r), i = e._getSoundIds(i);
                        for (var o = 0; o < i.length; o++) if (a = e._soundById(i[o]), a) {
                            e.playing(i[o]) && (a._rateSeek = e.seek(i[o]), a._playStart = e._webAudio ? n.ctx.currentTime : a._playStart), a._rate = r, e._webAudio && a._node && a._node.bufferSource ? a._node.bufferSource.playbackRate.setValueAtTime(r, n.ctx.currentTime) : a._node && (a._node.playbackRate = r);
                            var s = e.seek(i[o]),
                                c = ((e._sprite[a._sprite][0] + e._sprite[a._sprite][1]) / 1e3 - s) * 1e3 / Math.abs(a._rate);
                            (e._endTimers[i[o]] || !a._paused) && (e._clearTimer(i[o]), e._endTimers[i[o]] = setTimeout(e._ended.bind(e, a), c)), e._emit(`rate`, a._id)
                        }
                    } else return a = e._soundById(i), a ? a._rate : e._rate;
                    return e
                }, seek: function () {
                    var e = this, t = arguments, r, i;
                    if (t.length === 0 ? e._sounds.length && (i = e._sounds[0]._id) : t.length === 1 ? e._getSoundIds().indexOf(t[0]) >= 0 ? i = parseInt(t[0], 10) : e._sounds.length && (i = e._sounds[0]._id, r = parseFloat(t[0])) : t.length === 2 && (r = parseFloat(t[0]), i = parseInt(t[1], 10)), i === void 0) return 0;
                    if (typeof r == `number` && (e._state !== `loaded` || e._playLock)) return e._queue.push({
                        event: `seek`,
                        action: function () {
                            e.seek.apply(e, t)
                        }
                    }), e;
                    var a = e._soundById(i);
                    if (a) if (typeof r == `number` && r >= 0) {
                        var o = e.playing(i);
                        o && e.pause(i, !0), a._seek = r, a._ended = !1, e._clearTimer(i), !e._webAudio && a._node && !isNaN(a._node.duration) && (a._node.currentTime = r);
                        var s = function () {
                            o && e.play(i, !0), e._emit(`seek`, i)
                        };
                        if (o && !e._webAudio) {
                            var c = function () {
                                e._playLock ? setTimeout(c, 0) : s()
                            };
                            setTimeout(c, 0)
                        } else s()
                    } else if (e._webAudio) {
                        var l = e.playing(i) ? n.ctx.currentTime - a._playStart : 0,
                            u = a._rateSeek ? a._rateSeek - a._seek : 0;
                        return a._seek + (u + l * Math.abs(a._rate))
                    } else return a._node.currentTime;
                    return e
                }, playing: function (e) {
                    var t = this;
                    if (typeof e == `number`) {
                        var n = t._soundById(e);
                        return n ? !n._paused : !1
                    }
                    for (var r = 0; r < t._sounds.length; r++) if (!t._sounds[r]._paused) return !0;
                    return !1
                }, duration: function (e) {
                    var t = this, n = t._duration, r = t._soundById(e);
                    return r && (n = t._sprite[r._sprite][1] / 1e3), n
                }, state: function () {
                    return this._state
                }, unload: function () {
                    for (var e = this, t = e._sounds, r = 0; r < t.length; r++) t[r]._paused || e.stop(t[r]._id), e._webAudio || (e._clearSound(t[r]._node), t[r]._node.removeEventListener(`error`, t[r]._errorFn, !1), t[r]._node.removeEventListener(n._canPlayEvent, t[r]._loadFn, !1), t[r]._node.removeEventListener(`ended`, t[r]._endFn, !1), n._releaseHtml5Audio(t[r]._node)), delete t[r]._node, e._clearTimer(t[r]._id);
                    var i = n._howls.indexOf(e);
                    i >= 0 && n._howls.splice(i, 1);
                    var o = !0;
                    for (r = 0; r < n._howls.length; r++) if (n._howls[r]._src === e._src || e._src.indexOf(n._howls[r]._src) >= 0) {
                        o = !1;
                        break
                    }
                    return a && o && delete a[e._src], n.noAudio = !1, e._state = `unloaded`, e._sounds = [], e = null, null
                }, on: function (e, t, n, r) {
                    var i = this, a = i[`_on` + e];
                    return typeof t == `function` && a.push(r ? {id: n, fn: t, once: r} : {id: n, fn: t}), i
                }, off: function (e, t, n) {
                    var r = this, i = r[`_on` + e], a = 0;
                    if (typeof t == `number` && (n = t, t = null), t || n) for (a = 0; a < i.length; a++) {
                        var o = n === i[a].id;
                        if (t === i[a].fn && o || !t && o) {
                            i.splice(a, 1);
                            break
                        }
                    } else if (e) r[`_on` + e] = []; else {
                        var s = Object.keys(r);
                        for (a = 0; a < s.length; a++) s[a].indexOf(`_on`) === 0 && Array.isArray(r[s[a]]) && (r[s[a]] = [])
                    }
                    return r
                }, once: function (e, t, n) {
                    var r = this;
                    return r.on(e, t, n, 1), r
                }, _emit: function (e, t, n) {
                    for (var r = this, i = r[`_on` + e], a = i.length - 1; a >= 0; a--) (!i[a].id || i[a].id === t || e === `load`) && (setTimeout(function (e) {
                        e.call(this, t, n)
                    }.bind(r, i[a].fn), 0), i[a].once && r.off(e, i[a].fn, i[a].id));
                    return r._loadQueue(e), r
                }, _loadQueue: function (e) {
                    var t = this;
                    if (t._queue.length > 0) {
                        var n = t._queue[0];
                        n.event === e && (t._queue.shift(), t._loadQueue()), e || n.action()
                    }
                    return t
                }, _ended: function (e) {
                    var t = this, r = e._sprite;
                    if (!t._webAudio && e._node && !e._node.paused && !e._node.ended && e._node.currentTime < e._stop) return setTimeout(t._ended.bind(t, e), 100), t;
                    var i = !!(e._loop || t._sprite[r][2]);
                    if (t._emit(`end`, e._id), !t._webAudio && i && t.stop(e._id, !0).play(e._id), t._webAudio && i) {
                        t._emit(`play`, e._id), e._seek = e._start || 0, e._rateSeek = 0, e._playStart = n.ctx.currentTime;
                        var a = (e._stop - e._start) * 1e3 / Math.abs(e._rate);
                        t._endTimers[e._id] = setTimeout(t._ended.bind(t, e), a)
                    }
                    return t._webAudio && !i && (e._paused = !0, e._ended = !0, e._seek = e._start || 0, e._rateSeek = 0, t._clearTimer(e._id), t._cleanBuffer(e._node), n._autoSuspend()), !t._webAudio && !i && t.stop(e._id, !0), t
                }, _clearTimer: function (e) {
                    var t = this;
                    if (t._endTimers[e]) {
                        if (typeof t._endTimers[e] != `function`) clearTimeout(t._endTimers[e]); else {
                            var n = t._soundById(e);
                            n && n._node && n._node.removeEventListener(`ended`, t._endTimers[e], !1)
                        }
                        delete t._endTimers[e]
                    }
                    return t
                }, _soundById: function (e) {
                    for (var t = this, n = 0; n < t._sounds.length; n++) if (e === t._sounds[n]._id) return t._sounds[n];
                    return null
                }, _inactiveSound: function () {
                    var e = this;
                    e._drain();
                    for (var t = 0; t < e._sounds.length; t++) if (e._sounds[t]._ended) return e._sounds[t].reset();
                    return new i(e)
                }, _drain: function () {
                    var e = this, t = e._pool, n = 0, r = 0;
                    if (!(e._sounds.length < t)) {
                        for (r = 0; r < e._sounds.length; r++) e._sounds[r]._ended && n++;
                        for (r = e._sounds.length - 1; r >= 0; r--) {
                            if (n <= t) return;
                            e._sounds[r]._ended && (e._webAudio && e._sounds[r]._node && e._sounds[r]._node.disconnect(0), e._sounds.splice(r, 1), n--)
                        }
                    }
                }, _getSoundIds: function (e) {
                    var t = this;
                    if (e === void 0) {
                        for (var n = [], r = 0; r < t._sounds.length; r++) n.push(t._sounds[r]._id);
                        return n
                    } else return [e]
                }, _refreshBuffer: function (e) {
                    var t = this;
                    return e._node.bufferSource = n.ctx.createBufferSource(), e._node.bufferSource.buffer = a[t._src], e._panner ? e._node.bufferSource.connect(e._panner) : e._node.bufferSource.connect(e._node), e._node.bufferSource.loop = e._loop, e._loop && (e._node.bufferSource.loopStart = e._start || 0, e._node.bufferSource.loopEnd = e._stop || 0), e._node.bufferSource.playbackRate.setValueAtTime(e._rate, n.ctx.currentTime), t
                }, _cleanBuffer: function (e) {
                    var t = this, r = n._navigator && n._navigator.vendor.indexOf(`Apple`) >= 0;
                    if (!e.bufferSource) return t;
                    if (n._scratchBuffer && e.bufferSource && (e.bufferSource.onended = null, e.bufferSource.disconnect(0), r)) try {
                        e.bufferSource.buffer = n._scratchBuffer
                    } catch {
                    }
                    return e.bufferSource = null, t
                }, _clearSound: function (e) {
                    /MSIE |Trident\//.test(n._navigator && n._navigator.userAgent) || (e.src = `data:audio/wav;base64,UklGRigAAABXQVZFZm10IBIAAAABAAEARKwAAIhYAQACABAAAABkYXRhAgAAAAEA`)
                }
            };
            var i = function (e) {
                this._parent = e, this.init()
            };
            i.prototype = {
                init: function () {
                    var e = this, t = e._parent;
                    return e._muted = t._muted, e._loop = t._loop, e._volume = t._volume, e._rate = t._rate, e._seek = 0, e._paused = !0, e._ended = !0, e._sprite = `__default`, e._id = ++n._counter, t._sounds.push(e), e.create(), e
                }, create: function () {
                    var e = this, t = e._parent, r = n._muted || e._muted || e._parent._muted ? 0 : e._volume;
                    return t._webAudio ? (e._node = n.ctx.createGain === void 0 ? n.ctx.createGainNode() : n.ctx.createGain(), e._node.gain.setValueAtTime(r, n.ctx.currentTime), e._node.paused = !0, e._node.connect(n.masterGain)) : n.noAudio || (e._node = n._obtainHtml5Audio(), e._errorFn = e._errorListener.bind(e), e._node.addEventListener(`error`, e._errorFn, !1), e._loadFn = e._loadListener.bind(e), e._node.addEventListener(n._canPlayEvent, e._loadFn, !1), e._endFn = e._endListener.bind(e), e._node.addEventListener(`ended`, e._endFn, !1), e._node.src = t._src, e._node.preload = t._preload === !0 ? `auto` : t._preload, e._node.volume = r * n.volume(), e._node.load()), e
                }, reset: function () {
                    var e = this, t = e._parent;
                    return e._muted = t._muted, e._loop = t._loop, e._volume = t._volume, e._rate = t._rate, e._seek = 0, e._rateSeek = 0, e._paused = !0, e._ended = !0, e._sprite = `__default`, e._id = ++n._counter, e
                }, _errorListener: function () {
                    var e = this;
                    e._parent._emit(`loaderror`, e._id, e._node.error ? e._node.error.code : 0), e._node.removeEventListener(`error`, e._errorFn, !1)
                }, _loadListener: function () {
                    var e = this, t = e._parent;
                    t._duration = Math.ceil(e._node.duration * 10) / 10, Object.keys(t._sprite).length === 0 && (t._sprite = {__default: [0, t._duration * 1e3]}), t._state !== `loaded` && (t._state = `loaded`, t._emit(`load`), t._loadQueue()), e._node.removeEventListener(n._canPlayEvent, e._loadFn, !1)
                }, _endListener: function () {
                    var e = this, t = e._parent;
                    t._duration === 1 / 0 && (t._duration = Math.ceil(e._node.duration * 10) / 10, t._sprite.__default[1] === 1 / 0 && (t._sprite.__default[1] = t._duration * 1e3), t._ended(e)), e._node.removeEventListener(`ended`, e._endFn, !1)
                }
            };
            var a = {}, o = function (e) {
                var t = e._src;
                if (a[t]) {
                    e._duration = a[t].duration, l(e);
                    return
                }
                if (/^data:[^;]+;base64,/.test(t)) {
                    for (var n = atob(t.split(`,`)[1]), r = new Uint8Array(n.length), i = 0; i < n.length; ++i) r[i] = n.charCodeAt(i);
                    c(r.buffer, e)
                } else {
                    var o = new XMLHttpRequest;
                    o.open(e._xhr.method, t, !0), o.withCredentials = e._xhr.withCredentials, o.responseType = `arraybuffer`, e._xhr.headers && Object.keys(e._xhr.headers).forEach(function (t) {
                        o.setRequestHeader(t, e._xhr.headers[t])
                    }), o.onload = function () {
                        var t = (o.status + ``)[0];
                        if (t !== `0` && t !== `2` && t !== `3`) {
                            e._emit(`loaderror`, null, `Failed loading audio file with status: ` + o.status + `.`);
                            return
                        }
                        c(o.response, e)
                    }, o.onerror = function () {
                        e._webAudio && (e._html5 = !0, e._webAudio = !1, e._sounds = [], delete a[t], e.load())
                    }, s(o)
                }
            }, s = function (e) {
                try {
                    e.send()
                } catch {
                    e.onerror()
                }
            }, c = function (e, t) {
                var r = function () {
                    t._emit(`loaderror`, null, `Decoding audio data failed.`)
                }, i = function (e) {
                    e && t._sounds.length > 0 ? (a[t._src] = e, l(t, e)) : r()
                };
                typeof Promise < `u` && n.ctx.decodeAudioData.length === 1 ? n.ctx.decodeAudioData(e).then(i).catch(r) : n.ctx.decodeAudioData(e, i, r)
            }, l = function (e, t) {
                t && !e._duration && (e._duration = t.duration), Object.keys(e._sprite).length === 0 && (e._sprite = {__default: [0, e._duration * 1e3]}), e._state !== `loaded` && (e._state = `loaded`, e._emit(`load`), e._loadQueue())
            }, u = function () {
                if (n.usingWebAudio) {
                    try {
                        typeof AudioContext < `u` ? n.ctx = new AudioContext : typeof webkitAudioContext < `u` ? n.ctx = new webkitAudioContext : n.usingWebAudio = !1
                    } catch {
                        n.usingWebAudio = !1
                    }
                    n.ctx || (n.usingWebAudio = !1);
                    var e = /iP(hone|od|ad)/.test(n._navigator && n._navigator.platform),
                        t = n._navigator && n._navigator.appVersion.match(/OS (\d+)_(\d+)_?(\d+)?/),
                        r = t ? parseInt(t[1], 10) : null;
                    if (e && r && r < 9) {
                        var i = /safari/.test(n._navigator && n._navigator.userAgent.toLowerCase());
                        n._navigator && !i && (n.usingWebAudio = !1)
                    }
                    n.usingWebAudio && (n.masterGain = n.ctx.createGain === void 0 ? n.ctx.createGainNode() : n.ctx.createGain(), n.masterGain.gain.setValueAtTime(n._muted ? 0 : n._volume, n.ctx.currentTime), n.masterGain.connect(n.ctx.destination)), n._setup()
                }
            };
            typeof define == `function` && define.amd && define([], function () {
                return {Howler: n, Howl: r}
            }), e !== void 0 && (e.Howler = n, e.Howl = r), typeof global < `u` ? (global.HowlerGlobal = t, global.Howler = n, global.Howl = r, global.Sound = i) : typeof window < `u` && (window.HowlerGlobal = t, window.Howler = n, window.Howl = r, window.Sound = i)
        })(), (function () {
            HowlerGlobal.prototype._pos = [0, 0, 0], HowlerGlobal.prototype._orientation = [0, 0, -1, 0, 1, 0], HowlerGlobal.prototype.stereo = function (e) {
                var t = this;
                if (!t.ctx || !t.ctx.listener) return t;
                for (var n = t._howls.length - 1; n >= 0; n--) t._howls[n].stereo(e);
                return t
            }, HowlerGlobal.prototype.pos = function (e, t, n) {
                var r = this;
                if (!r.ctx || !r.ctx.listener) return r;
                if (t = typeof t == `number` ? t : r._pos[1], n = typeof n == `number` ? n : r._pos[2], typeof e == `number`) r._pos = [e, t, n], r.ctx.listener.positionX === void 0 ? r.ctx.listener.setPosition(r._pos[0], r._pos[1], r._pos[2]) : (r.ctx.listener.positionX.setTargetAtTime(r._pos[0], Howler.ctx.currentTime, .1), r.ctx.listener.positionY.setTargetAtTime(r._pos[1], Howler.ctx.currentTime, .1), r.ctx.listener.positionZ.setTargetAtTime(r._pos[2], Howler.ctx.currentTime, .1)); else return r._pos;
                return r
            }, HowlerGlobal.prototype.orientation = function (e, t, n, r, i, a) {
                var o = this;
                if (!o.ctx || !o.ctx.listener) return o;
                var s = o._orientation;
                if (t = typeof t == `number` ? t : s[1], n = typeof n == `number` ? n : s[2], r = typeof r == `number` ? r : s[3], i = typeof i == `number` ? i : s[4], a = typeof a == `number` ? a : s[5], typeof e == `number`) o._orientation = [e, t, n, r, i, a], o.ctx.listener.forwardX === void 0 ? o.ctx.listener.setOrientation(e, t, n, r, i, a) : (o.ctx.listener.forwardX.setTargetAtTime(e, Howler.ctx.currentTime, .1), o.ctx.listener.forwardY.setTargetAtTime(t, Howler.ctx.currentTime, .1), o.ctx.listener.forwardZ.setTargetAtTime(n, Howler.ctx.currentTime, .1), o.ctx.listener.upX.setTargetAtTime(r, Howler.ctx.currentTime, .1), o.ctx.listener.upY.setTargetAtTime(i, Howler.ctx.currentTime, .1), o.ctx.listener.upZ.setTargetAtTime(a, Howler.ctx.currentTime, .1)); else return s;
                return o
            }, Howl.prototype.init = (function (e) {
                return function (t) {
                    var n = this;
                    return n._orientation = t.orientation || [1, 0, 0], n._stereo = t.stereo || null, n._pos = t.pos || null, n._pannerAttr = {
                        coneInnerAngle: t.coneInnerAngle === void 0 ? 360 : t.coneInnerAngle,
                        coneOuterAngle: t.coneOuterAngle === void 0 ? 360 : t.coneOuterAngle,
                        coneOuterGain: t.coneOuterGain === void 0 ? 0 : t.coneOuterGain,
                        distanceModel: t.distanceModel === void 0 ? `inverse` : t.distanceModel,
                        maxDistance: t.maxDistance === void 0 ? 1e4 : t.maxDistance,
                        panningModel: t.panningModel === void 0 ? `HRTF` : t.panningModel,
                        refDistance: t.refDistance === void 0 ? 1 : t.refDistance,
                        rolloffFactor: t.rolloffFactor === void 0 ? 1 : t.rolloffFactor
                    }, n._onstereo = t.onstereo ? [{fn: t.onstereo}] : [], n._onpos = t.onpos ? [{fn: t.onpos}] : [], n._onorientation = t.onorientation ? [{fn: t.onorientation}] : [], e.call(this, t)
                }
            })(Howl.prototype.init), Howl.prototype.stereo = function (t, n) {
                var r = this;
                if (!r._webAudio) return r;
                if (r._state !== `loaded`) return r._queue.push({
                    event: `stereo`, action: function () {
                        r.stereo(t, n)
                    }
                }), r;
                var i = Howler.ctx.createStereoPanner === void 0 ? `spatial` : `stereo`;
                if (n === void 0) if (typeof t == `number`) r._stereo = t, r._pos = [t, 0, 0]; else return r._stereo;
                for (var a = r._getSoundIds(n), o = 0; o < a.length; o++) {
                    var s = r._soundById(a[o]);
                    if (s) if (typeof t == `number`) s._stereo = t, s._pos = [t, 0, 0], s._node && (s._pannerAttr.panningModel = `equalpower`, (!s._panner || !s._panner.pan) && e(s, i), i === `spatial` ? s._panner.positionX === void 0 ? s._panner.setPosition(t, 0, 0) : (s._panner.positionX.setValueAtTime(t, Howler.ctx.currentTime), s._panner.positionY.setValueAtTime(0, Howler.ctx.currentTime), s._panner.positionZ.setValueAtTime(0, Howler.ctx.currentTime)) : s._panner.pan.setValueAtTime(t, Howler.ctx.currentTime)), r._emit(`stereo`, s._id); else return s._stereo
                }
                return r
            }, Howl.prototype.pos = function (t, n, r, i) {
                var a = this;
                if (!a._webAudio) return a;
                if (a._state !== `loaded`) return a._queue.push({
                    event: `pos`, action: function () {
                        a.pos(t, n, r, i)
                    }
                }), a;
                if (n = typeof n == `number` ? n : 0, r = typeof r == `number` ? r : -.5, i === void 0) if (typeof t == `number`) a._pos = [t, n, r]; else return a._pos;
                for (var o = a._getSoundIds(i), s = 0; s < o.length; s++) {
                    var c = a._soundById(o[s]);
                    if (c) if (typeof t == `number`) c._pos = [t, n, r], c._node && ((!c._panner || c._panner.pan) && e(c, `spatial`), c._panner.positionX === void 0 ? c._panner.setPosition(t, n, r) : (c._panner.positionX.setValueAtTime(t, Howler.ctx.currentTime), c._panner.positionY.setValueAtTime(n, Howler.ctx.currentTime), c._panner.positionZ.setValueAtTime(r, Howler.ctx.currentTime))), a._emit(`pos`, c._id); else return c._pos
                }
                return a
            }, Howl.prototype.orientation = function (t, n, r, i) {
                var a = this;
                if (!a._webAudio) return a;
                if (a._state !== `loaded`) return a._queue.push({
                    event: `orientation`, action: function () {
                        a.orientation(t, n, r, i)
                    }
                }), a;
                if (n = typeof n == `number` ? n : a._orientation[1], r = typeof r == `number` ? r : a._orientation[2], i === void 0) if (typeof t == `number`) a._orientation = [t, n, r]; else return a._orientation;
                for (var o = a._getSoundIds(i), s = 0; s < o.length; s++) {
                    var c = a._soundById(o[s]);
                    if (c) if (typeof t == `number`) c._orientation = [t, n, r], c._node && (c._panner || (c._pos ||= a._pos || [0, 0, -.5], e(c, `spatial`)), c._panner.orientationX === void 0 ? c._panner.setOrientation(t, n, r) : (c._panner.orientationX.setValueAtTime(t, Howler.ctx.currentTime), c._panner.orientationY.setValueAtTime(n, Howler.ctx.currentTime), c._panner.orientationZ.setValueAtTime(r, Howler.ctx.currentTime))), a._emit(`orientation`, c._id); else return c._orientation
                }
                return a
            }, Howl.prototype.pannerAttr = function () {
                var t = this, n = arguments, r, i, a;
                if (!t._webAudio) return t;
                if (n.length === 0) return t._pannerAttr;
                if (n.length === 1) if (typeof n[0] == `object`) r = n[0], i === void 0 && (r.pannerAttr ||= {
                    coneInnerAngle: r.coneInnerAngle,
                    coneOuterAngle: r.coneOuterAngle,
                    coneOuterGain: r.coneOuterGain,
                    distanceModel: r.distanceModel,
                    maxDistance: r.maxDistance,
                    refDistance: r.refDistance,
                    rolloffFactor: r.rolloffFactor,
                    panningModel: r.panningModel
                }, t._pannerAttr = {
                    coneInnerAngle: r.pannerAttr.coneInnerAngle === void 0 ? t._coneInnerAngle : r.pannerAttr.coneInnerAngle,
                    coneOuterAngle: r.pannerAttr.coneOuterAngle === void 0 ? t._coneOuterAngle : r.pannerAttr.coneOuterAngle,
                    coneOuterGain: r.pannerAttr.coneOuterGain === void 0 ? t._coneOuterGain : r.pannerAttr.coneOuterGain,
                    distanceModel: r.pannerAttr.distanceModel === void 0 ? t._distanceModel : r.pannerAttr.distanceModel,
                    maxDistance: r.pannerAttr.maxDistance === void 0 ? t._maxDistance : r.pannerAttr.maxDistance,
                    refDistance: r.pannerAttr.refDistance === void 0 ? t._refDistance : r.pannerAttr.refDistance,
                    rolloffFactor: r.pannerAttr.rolloffFactor === void 0 ? t._rolloffFactor : r.pannerAttr.rolloffFactor,
                    panningModel: r.pannerAttr.panningModel === void 0 ? t._panningModel : r.pannerAttr.panningModel
                }); else return a = t._soundById(parseInt(n[0], 10)), a ? a._pannerAttr : t._pannerAttr; else n.length === 2 && (r = n[0], i = parseInt(n[1], 10));
                for (var o = t._getSoundIds(i), s = 0; s < o.length; s++) if (a = t._soundById(o[s]), a) {
                    var c = a._pannerAttr;
                    c = {
                        coneInnerAngle: r.coneInnerAngle === void 0 ? c.coneInnerAngle : r.coneInnerAngle,
                        coneOuterAngle: r.coneOuterAngle === void 0 ? c.coneOuterAngle : r.coneOuterAngle,
                        coneOuterGain: r.coneOuterGain === void 0 ? c.coneOuterGain : r.coneOuterGain,
                        distanceModel: r.distanceModel === void 0 ? c.distanceModel : r.distanceModel,
                        maxDistance: r.maxDistance === void 0 ? c.maxDistance : r.maxDistance,
                        refDistance: r.refDistance === void 0 ? c.refDistance : r.refDistance,
                        rolloffFactor: r.rolloffFactor === void 0 ? c.rolloffFactor : r.rolloffFactor,
                        panningModel: r.panningModel === void 0 ? c.panningModel : r.panningModel
                    };
                    var l = a._panner;
                    l ||= (a._pos ||= t._pos || [0, 0, -.5], e(a, `spatial`), a._panner), l.coneInnerAngle = c.coneInnerAngle, l.coneOuterAngle = c.coneOuterAngle, l.coneOuterGain = c.coneOuterGain, l.distanceModel = c.distanceModel, l.maxDistance = c.maxDistance, l.refDistance = c.refDistance, l.rolloffFactor = c.rolloffFactor, l.panningModel = c.panningModel
                }
                return t
            }, Sound.prototype.init = (function (e) {
                return function () {
                    var t = this, n = t._parent;
                    t._orientation = n._orientation, t._stereo = n._stereo, t._pos = n._pos, t._pannerAttr = n._pannerAttr, e.call(this), t._stereo ? n.stereo(t._stereo) : t._pos && n.pos(t._pos[0], t._pos[1], t._pos[2], t._id)
                }
            })(Sound.prototype.init), Sound.prototype.reset = (function (e) {
                return function () {
                    var t = this, n = t._parent;
                    return t._orientation = n._orientation, t._stereo = n._stereo, t._pos = n._pos, t._pannerAttr = n._pannerAttr, t._stereo ? n.stereo(t._stereo) : t._pos ? n.pos(t._pos[0], t._pos[1], t._pos[2], t._id) : t._panner && (t._panner.disconnect(0), t._panner = void 0, n._refreshBuffer(t)), e.call(this)
                }
            })(Sound.prototype.reset);
            var e = function (e, t) {
                t ||= `spatial`, t === `spatial` ? (e._panner = Howler.ctx.createPanner(), e._panner.coneInnerAngle = e._pannerAttr.coneInnerAngle, e._panner.coneOuterAngle = e._pannerAttr.coneOuterAngle, e._panner.coneOuterGain = e._pannerAttr.coneOuterGain, e._panner.distanceModel = e._pannerAttr.distanceModel, e._panner.maxDistance = e._pannerAttr.maxDistance, e._panner.refDistance = e._pannerAttr.refDistance, e._panner.rolloffFactor = e._pannerAttr.rolloffFactor, e._panner.panningModel = e._pannerAttr.panningModel, e._panner.positionX === void 0 ? e._panner.setPosition(e._pos[0], e._pos[1], e._pos[2]) : (e._panner.positionX.setValueAtTime(e._pos[0], Howler.ctx.currentTime), e._panner.positionY.setValueAtTime(e._pos[1], Howler.ctx.currentTime), e._panner.positionZ.setValueAtTime(e._pos[2], Howler.ctx.currentTime)), e._panner.orientationX === void 0 ? e._panner.setOrientation(e._orientation[0], e._orientation[1], e._orientation[2]) : (e._panner.orientationX.setValueAtTime(e._orientation[0], Howler.ctx.currentTime), e._panner.orientationY.setValueAtTime(e._orientation[1], Howler.ctx.currentTime), e._panner.orientationZ.setValueAtTime(e._orientation[2], Howler.ctx.currentTime))) : (e._panner = Howler.ctx.createStereoPanner(), e._panner.pan.setValueAtTime(e._stereo, Howler.ctx.currentTime)), e._panner.connect(e._node), e._paused || e._parent.pause(e._id, !0).play(e._id, !0)
            }
        })()
    })), S = {
        Menu_Start_Laptop: [`Menu_Start_Laptop_1`, `Menu_Start_Laptop_2`, `Menu_Start_Laptop_3`, `Menu_Start_Laptop_4`, `Menu_Start_Laptop_5`],
        Menu_Start_Melody: `Menu_Start_Melody`,
        Menu_Start_Screen: `Menu_Start_Screen`,
        Menu_Head_Hover: `Menu_Head_Hover`,
        Menu_Head_Click: `Menu_Head_Click`,
        Menu_Button_Participants_Hover: `Button_Participants_Hover`,
        Menu_Button_Participants_Click: `Menu_Button_Participants_Click`,
        Menu_Button_Back_Hover: `Menu_Button+Back_Hover`,
        Menu_Button_Back_Click: `Menu_Button+Back_Click`,
        Menu_Item_Hover: `Menu_Item_Hover`,
        Menu_Item_Press: `Menu_Item_Press`,
        Menu_Item_Unpress: `Menu_Item_Unpress`,
        Menu_Item_Next: `Menu_Item_Next`,
        Menu_Item_Activated: `Menu_Item_Activated`,
        Menu_Key: [`Menu_Key_1`, `Menu_Key_2`, `Menu_Key_3`, `Menu_Key_4`, `Menu_Key_5`, `Menu_Key_6`, `Menu_Key_7`],
        Menu_Casino_Hover: `Menu_Casino_Hover`,
        Menu_Casino_Click: `Menu_Casino_Click`,
        Menu_Casino_Change: `Menu_Casino_Change`,
        Casino_Button_Hover: `Casino_Button_Hover`,
        Casino_Button_Click: `Casino_Button_Click`,
        Casino_Wheel_Switch: `Casino_Wheel_Switch`,
        Casino_Wheel_Start: `Casino_Wheel_Start`,
        Casino_Wheel_Item: `Casino_Wheel_Item`,
        Casino_Wheel_End: `Casino_Wheel_End`,
        Casino_Dealer_Faker_Idle: `Casino_Dealer_Faker_Idle`,
        Casino_Dealer_Faker_1: `Casino_Dealer_Faker_1`,
        Casino_Dealer_Faker_2: `Casino_Dealer_Faker_2`,
        Casino_Dealer_Faker_3: `Casino_Dealer_Faker_3`,
        Casino_Dealer_Faker_Roll: `Casino_Dealer_Faker_Roll`,
        Casino_Dealer_Ozon_Idle: `Casino_Dealer_Ozon_Idle`,
        Casino_Dealer_Ozon_1: `Casino_Dealer_Ozon_1`,
        Casino_Dealer_Ozon_2: `Casino_Dealer_Ozon_2`,
        Casino_Dealer_Ozon_3: `Casino_Dealer_Ozon_3`,
        Casino_Dealer_Ozon_Roll: `Casino_Dealer_Ozon_Roll`,
        Casino_Dealer_Rin_0: `Casino_Dealer_Rin_0`,
        Casino_Dealer_Rin_1: `Casino_Dealer_Rin_1`,
        Casino_Dealer_Rin_2: `Casino_Dealer_Rin_2`,
        Casino_Dealer_Rin_4: `Casino_Dealer_Rin_4`,
        Casino_Dealer_Rin_5: `Casino_Dealer_Rin_5`,
        Casino_Dealer_Rin_6: `Casino_Dealer_Rin_6`,
        Casino_Dealer_Shev_0: `Casino_Dealer_Shev_0`,
        Casino_Dealer_Shev_1: `Casino_Dealer_Shev_1`,
        Casino_Dealer_Shev_2: `Casino_Dealer_Shev_2`,
        Casino_Dealer_Shev_3: `Casino_Dealer_Shev_3`,
        UI_NOT_Effect: `UI_NOT_Effect`,
        UI_NOT_Activated: `UI_NOT_Activated`,
        UI_NOT_Achivement: `UI_NOT_Achivement`,
        UI_NOT_Error: `UI_NOT_Error`,
        Game_Started: `Game_Started`,
        Game_Pause: `Game_Pause`,
        Game_Unpause: `Game_Unpause`,
        Game_Dropped: `Game_Dropped`,
        Game_Rerolled: `Game_Rerolled`,
        Game_Finished: `Game_Finished`,
        Game_Done: `Game_Done`,
        AMB_Menu_Morning: `AMB_Menu_Morning`,
        AMB_Menu_Day: `AMB_Menu_Day`,
        AMB_Menu_Evening: `AMB_Menu_Evening`,
        AMB_Menu_Night: `AMB_Menu_Night`,
        AMB_Casino_Open: `AMB_Casino_Open`,
        AMB_Casino_Closed: `AMB_Casino_Closed`,
        AMB_Start_Laptop: `AMB_Start_Laptop`,
        AMB_Loop_Laptop: `AMB_Loop_Laptop`,
        Menu_Phone_Icon: `Menu_Phone_Icon`,
        Menu_Phone_Up: `Menu_Phone_Up`,
        Menu_Phone_Down: `Menu_Phone_Down`,
        Menu_Phone_Button_Press: `Menu_Phone_Button_Press`,
        Menu_Phone_Button_Unpress: `Menu_Phone_Button_Unpress`,
        Menu_Phone_Call_Outcoming: `Menu_Phone_Call_Outcoming`,
        Menu_Phone_Call_Busy: `Menu_Phone_Call_Busy`,
        Menu_Phone_Call_End: `Menu_Phone_Call_End`,
        Menu_Phone_Game_Button: `Menu_Phone_Game_Button`,
        Menu_Phone_Game_Exit: `Menu_Phone_Game_Exit`,
        Menu_Phone_Game_Win: `Menu_Phone_Game_Win`,
        Menu_Phone_Game_Lose: `Menu_Phone_Game_Lose`,
        Menu_Phone_Game_Bird_Tap: `Menu_Phone_Game_Bird_Tap`,
        Menu_Phone_Game_Bird_Pass: `Menu_Phone_Game_Bird_Pass`,
        Menu_Phone_Game_Bird_End: `Menu_Phone_Game_Bird_End`,
        Menu_Phone_Game_Wordly_Delete: `Menu_Phone_Game_Wordly_Delete`,
        Menu_Phone_Game_Wordly_Enter: `Menu_Phone_Game_Wordly_Enter`,
        Menu_Phone_Game_GTG_Key: `Menu_Phone_Game_GTG_Key`
    }, ie = {morning: S.AMB_Menu_Morning, day: S.AMB_Menu_Day, evening: S.AMB_Menu_Evening, night: S.AMB_Menu_Night},
    C = [...S.Menu_Start_Laptop, S.Menu_Start_Melody, S.Menu_Start_Screen, S.Menu_Head_Hover, S.Menu_Head_Click, S.Menu_Button_Participants_Hover, S.Menu_Button_Participants_Click, S.Menu_Button_Back_Hover, S.Menu_Button_Back_Click, ...S.Menu_Key, S.Menu_Item_Hover, S.Menu_Item_Press, S.Menu_Item_Unpress, S.Menu_Item_Next, S.Menu_Item_Activated, S.Menu_Casino_Hover, S.Menu_Casino_Click, S.Menu_Casino_Change, S.Casino_Button_Hover, S.Casino_Button_Click, S.Casino_Wheel_Switch, S.Casino_Wheel_Start, S.Casino_Wheel_Item, S.Casino_Wheel_End, S.Casino_Dealer_Shev_0, S.Casino_Dealer_Shev_1, S.Casino_Dealer_Shev_2, S.Casino_Dealer_Shev_3, S.Casino_Dealer_Rin_0, S.Casino_Dealer_Rin_1, S.Casino_Dealer_Rin_2, S.Casino_Dealer_Rin_4, S.Casino_Dealer_Rin_5, S.Casino_Dealer_Rin_6, S.Casino_Dealer_Faker_Idle, S.Casino_Dealer_Faker_1, S.Casino_Dealer_Faker_2, S.Casino_Dealer_Faker_3, S.Casino_Dealer_Faker_Roll, S.Casino_Dealer_Ozon_Idle, S.Casino_Dealer_Ozon_1, S.Casino_Dealer_Ozon_2, S.Casino_Dealer_Ozon_3, S.Casino_Dealer_Ozon_Roll, S.UI_NOT_Effect, S.UI_NOT_Activated, S.UI_NOT_Achivement, S.UI_NOT_Error, S.Game_Started, S.Game_Pause, S.Game_Unpause, S.Game_Dropped, S.Game_Rerolled, S.Game_Finished, S.Game_Done, S.AMB_Menu_Morning, S.AMB_Menu_Day, S.AMB_Menu_Evening, S.AMB_Menu_Night, S.AMB_Casino_Open, S.AMB_Casino_Closed, S.AMB_Start_Laptop, S.AMB_Loop_Laptop, S.Menu_Phone_Icon, S.Menu_Phone_Up, S.Menu_Phone_Down, S.Menu_Phone_Button_Press, S.Menu_Phone_Button_Unpress, S.Menu_Phone_Call_Outcoming, S.Menu_Phone_Call_Busy, S.Menu_Phone_Call_End, S.Menu_Phone_Game_Button, S.Menu_Phone_Game_Exit, S.Menu_Phone_Game_Win, S.Menu_Phone_Game_Lose, S.Menu_Phone_Game_Bird_Tap, S.Menu_Phone_Game_Bird_Pass, S.Menu_Phone_Game_Bird_End, S.Menu_Phone_Game_Wordly_Delete, S.Menu_Phone_Game_Wordly_Enter, S.Menu_Phone_Game_GTG_Key],
    w = re(), T = .1, E = e => e < -T ? -T : e > T ? T : e;

function ae(e, t = window.innerWidth) {
    if (t <= 0) return 0;
    let n = t / 2, r = (e - n) / n;
    return r < -1 ? r = -1 : r > 1 && (r = 1), E(r)
}

function D(e, t, n) {
    let r = e.getBoundingClientRect(), i = Math.min(100, Math.max(0, t));
    return ae(r.left + (r.width - n) / 2 + i / 100 * n)
}

function O(e, t = window.innerWidth) {
    if (t <= 0) return 0;
    let n = t / 2, r = e.getBoundingClientRect(), i = (r.left + r.width / 2 - n) / n;
    return i < -1 ? i = -1 : i > 1 && (i = 1), E(i)
}

var oe = `settings_volume`, k = `volumeChanged`, se = `settings_rhythm_music_volume`, ce = `rhythmMusicVolumeChanged`,
    le = null, ue = e => Math.min(100, Math.max(0, Math.round(e))), de = e => {
        window.dispatchEvent(new CustomEvent(k, {detail: e}))
    };

function fe() {
    return le ?? pe()
}

function pe(e = 40) {
    try {
        let t = localStorage.getItem(oe);
        if (!t) return e;
        let n = parseInt(t, 10);
        return Number.isFinite(n) ? Math.min(100, Math.max(0, n)) : e
    } catch {
        return e
    }
}

function me(e) {
    let t = ue(e);
    return le = t, de(t), t
}

function he(e) {
    let t = ue(e);
    le = null;
    try {
        localStorage.setItem(oe, t.toString())
    } catch {
    }
    return de(t), t
}

function ge() {
    return fe() / 100
}

function _e(e) {
    return window.addEventListener(k, e), () => window.removeEventListener(k, e)
}

function ve(e = 50) {
    try {
        let t = localStorage.getItem(se);
        if (!t) return e;
        let n = parseInt(t, 10);
        return Number.isFinite(n) ? Math.min(100, Math.max(0, n)) : e
    } catch {
        return e
    }
}

function ye(e) {
    let t = Math.min(100, Math.max(0, Math.round(e)));
    try {
        localStorage.setItem(se, t.toString())
    } catch {
    }
    return window.dispatchEvent(new CustomEvent(ce, {detail: t})), t
}

function be(e) {
    return Math.min(100, Math.max(0, e)) / 100
}

var A = new Map, j = new Map, xe = [], Se = null, Ce = !1, M = !1, N = !1, P = null, F = null, I = null, we = 0, Te = 0,
    Ee = !1, L = null, De = 10 ** (-18 / 20),
    Oe = new Set([S.AMB_Menu_Morning, S.AMB_Menu_Day, S.AMB_Menu_Evening, S.AMB_Menu_Night, S.AMB_Casino_Open, S.AMB_Casino_Closed, S.AMB_Loop_Laptop]),
    ke = e => e.startsWith(`Game_`),
    Ae = e => !!(e.startsWith(`AMB_`) || e.startsWith(`Casino_`) || e.startsWith(`Menu_Casino`) || e.startsWith(`Menu_Button+Back_`) || e.startsWith(`Menu_Phone_`) || e.startsWith(`Easter_`) || e.startsWith(`Button_Participants_`) || e.startsWith(`Menu_Button_Participants_`) || e.startsWith(`UI_NOT_`) || ke(e)),
    je = () => {
        A.forEach((e, t) => {
            if (!Oe.has(t) && !ke(t)) try {
                e.stop()
            } catch {
            }
        })
    }, Me = e => {
        if (e) {
            L != null && (clearTimeout(L), L = null), Ee = !0, je();
            return
        }
        L != null && clearTimeout(L), L = setTimeout(() => {
            Ee = !1, L = null
        }, 300)
    }, Ne = `soundSystemEnabled`, Pe = `trackPlaybackLockChanged`, Fe = () => M, Ie = e => {
        N !== e && (N = e, e && Re(), window.dispatchEvent(new CustomEvent(Pe, {detail: e})))
    }, Le = () => typeof document > `u` ? !0 : document.visibilityState === `visible`, Re = () => {
        A.forEach(e => {
            try {
                e.stop()
            } catch {
            }
        }), P = null, F = null, I = null
    };

function ze(e) {
    return 1.5 - Math.min(1, Math.max(0, e))
}

var Be = () => {
        try {
            return w.Howler.codecs(`flac`)
        } catch {
            return !1
        }
    }, Ve = (e, t) => Math.floor(Math.random() * (t - e + 1)) + e, He = e => e.length > 0 ? e[Ve(0, e.length - 1)] : null,
    Ue = (e, t, n, r, i, a = !1) => {
        let o = new w.Howl({src: [t], format: [`flac`], preload: !0, html5: n, loop: a, volume: 1, pool: n ? 3 : 24});
        return o.once(`load`, () => {
            A.set(e, o), r(o)
        }), o.once(`loaderror`, (e, t) => {
            i(t)
        }), o.on(`playerror`, () => {
            M && V().then(() => {
                o.play()
            })
        }), o
    }, R = (e, t, n = !1) => {
        if (!t) return Promise.resolve(null);
        if (A.has(e)) return Promise.resolve(A.get(e));
        if (j.has(e)) return j.get(e);
        let r = new Promise(r => {
            let i = e => {
                r(e)
            }, a = r => {
                if (!Be()) {
                    console.warn(`[useSounds] Failed to load: ${e}`, r), i(null);
                    return
                }
                Ue(e, t, !0, e => i(e), t => {
                    console.warn(`[useSounds] Failed to load: ${e}`, r, t), i(null)
                }, n)
            };
            Ue(e, t, !1, e => i(e), e => a(e), n)
        }).finally(() => {
            j.delete(e)
        });
        return j.set(e, r), r
    }, We = async e => {
        let t = A.get(e);
        return t?.state() === `loaded` ? t : j.get(e) || null
    }, z = (e, t = 400) => e ? new Promise(n => {
        try {
            let r = e.howl.volume(e.id), i = typeof r == `number` ? r : 0;
            e.howl.fade(i, 0, t, e.id), window.setTimeout(() => {
                try {
                    e.howl.stop(e.id)
                } catch {
                }
                n()
            }, t + 30)
        } catch {
            n()
        }
    }) : Promise.resolve(), B = e => {
        e && z(e)
    }, Ge = () => {
        Je(), qe()
    }, Ke = () => (Ge(), Se ?? Promise.resolve()), qe = () => {
        if (Se) return;
        let e = async (e, t, n = !1) => await R(e, t, n) != null, t = async (t, n = 5, r = 50) => {
                for (let i = 0; i < t.length; i += n) {
                    let a = t.slice(i, i + n);
                    await Promise.all(a.map(t => e(t.name, t.path, t.loop ?? !1))), i + n < t.length && await new Promise(e => setTimeout(e, r))
                }
            },
            n = [S.Menu_Button_Back_Click, S.Menu_Button_Back_Hover, S.Menu_Button_Participants_Hover, S.Menu_Head_Hover, S.Menu_Head_Click, S.Casino_Button_Hover, S.Casino_Button_Click, S.Casino_Wheel_Start, S.Casino_Wheel_Item, S.Casino_Wheel_End, S.Casino_Wheel_Switch, S.UI_NOT_Effect, S.UI_NOT_Activated, S.UI_NOT_Achivement, S.UI_NOT_Error, S.Menu_Casino_Hover, S.Menu_Casino_Click, S.Menu_Casino_Change, S.Menu_Phone_Up, S.Menu_Phone_Down],
            r = [S.AMB_Casino_Open, S.AMB_Casino_Closed, S.AMB_Menu_Morning, S.AMB_Menu_Day, S.AMB_Menu_Evening, S.AMB_Menu_Night, S.AMB_Loop_Laptop],
            i = (e, t = !1) => e.map(e => ({name: e, path: x(`${e}.flac`), loop: t})), a = new Set([...n, ...r]),
            o = C.filter(e => !a.has(e));
        Se = (async () => {
            await t(i(n), n.length, 0), await t(i(r, !0), r.length, 0), await t(i(o), 5, 50), xe = S.Menu_Key.filter(e => A.has(e))
        })()
    }, V = async () => {
        if (!M) return;
        let e = w.Howler.ctx;
        if (e?.state === `suspended`) try {
            await e.resume()
        } catch {
        }
    }, Je = () => {
        if (Ce) return;
        Ce = !0, w.Howler.autoUnlock = !0, w.Howler.autoSuspend = !1, w.Howler.volume(ge()), window.addEventListener(`volumeChanged`, () => {
            w.Howler.volume(ge())
        });
        let e = () => {
            M && Re()
        };
        document.addEventListener(`visibilitychange`, () => {
            document.visibilityState !== `visible` && e()
        })
    }, Ye = () => {
        if (!M) {
            M = !0, Je(), qe(), V(), window.dispatchEvent(new CustomEvent(Ne));
            return
        }
        V()
    }, H = async (e, t = {}) => {
        if (!M || !Le()) return null;
        await V();
        let n = await We(e) ?? await R(e, x(`${e}.flac`));
        if (!n || n.state() !== `loaded`) return null;
        let {
            volume: r = .5,
            rate: i,
            pan: a,
            panElement: o,
            randomPitch: s = i == null,
            randomVolume: c = !0,
            allowDuringTrackPlayback: l = !1
        } = t;
        if (N && !l || Ee && !Ae(e)) return null;
        try {
            let e = n.play();
            if (e == null) return null;
            let t = i ?? (s ? .9 + Math.random() * .14 : 1);
            n.rate(t, e);
            let l = 1;
            c && (l = 10 ** ((-.6 + Math.random() * 1.2) / 20)), n.volume(r * l, e);
            let u = a ?? (o == null ? void 0 : O(o));
            return u != null && typeof n.stereo == `function` && n.stereo(u, e), e
        } catch (t) {
            return console.error(`Error playing sound ${e}:`, t), null
        }
    }, U = (e, t = {}) => {
        H(e, t)
    }, Xe = (e, t = {}) => new Promise(n => {
        (async () => {
            let r = await H(e, t);
            if (r == null) {
                n();
                return
            }
            let i = A.get(e);
            if (!i) {
                n();
                return
            }
            let a = !1, o = () => {
                a || (a = !0, n())
            };
            i.once(`end`, o, r);
            let s = i.duration();
            if (s > 0) {
                let e = i.rate(r), t = typeof e == `number` && e > 0 ? e : 1;
                window.setTimeout(() => o(), Math.ceil(s / t * 1e3) + 200)
            } else window.setTimeout(() => o(), 5e3)
        })()
    }), Ze = e => {
        let t = A.get(e);
        t && t.stop()
    }, Qe = async e => {
        let t = A.get(e);
        if (t?.state() === `loaded`) return t.duration();
        let n = x(`${e}.flac`);
        if (!n) return 0;
        let r = await R(e, n);
        return r ? r.duration() : 0
    }, $e = (e, t = {}) => {
        let n = He(e.filter(e => A.has(e)));
        n && U(n, t)
    }, et = (e = {}) => {
        $e(S.Menu_Start_Laptop, e)
    }, tt = (e = !1) => {
        let t = xe.filter(e => e.startsWith(`Menu_Key_`)), n = He(e && t.length > 0 ? t : xe);
        n && U(n, {volume: .3})
    }, nt = async (e, t = .35) => {
        if (!M || !Le() || N) return null;
        await V();
        let n = await We(e) ?? await R(e, x(`${e}.flac`), !0);
        if (!n || n.state() !== `loaded`) return null;
        try {
            let r = n.play();
            return r == null ? null : (n.loop(!0, r), n.volume(t * De, r), {howl: n, id: r, soundName: e})
        } catch {
            return null
        }
    }, rt = e => {
        let t = ++we;
        (async () => {
            if (P?.soundName === e) return;
            let n = P;
            P = null, B(n);
            let r = await nt(e, .5);
            if (t !== we) {
                B(r);
                return
            }
            P = r
        })()
    }, it = (e, t = {}) => {
        let {playClosedAmbient: n = !0, casinoInteractionBlocked: r = !1} = t, i = ++Te;
        (async () => {
            let t = e && !r ? S.AMB_Casino_Open : S.AMB_Casino_Closed;
            if (e) {
                we++;
                let e = P;
                P = null, B(e)
            }
            if (!e && !n) {
                if (i !== Te) return;
                let e = F;
                F = null, B(e);
                return
            }
            if (F?.soundName === t) return;
            let a = F;
            F = null, B(a);
            let o = await nt(t, 3);
            if (i !== Te) {
                B(o);
                return
            }
            F = o
        })()
    }, at = () => {
        (async () => {
            U(S.AMB_Start_Laptop, {volume: .45}), await z(I), I = null, I = await nt(S.AMB_Loop_Laptop, .3)
        })()
    }, ot = () => {
        (async () => {
            I ||= await nt(S.AMB_Loop_Laptop, .3)
        })()
    }, st = () => {
        Ze(S.AMB_Start_Laptop), Promise.all([z(P), z(F), z(I)]).then(() => {
            P = null, F = null, I = null
        })
    }, ct = () => {
        let e = (0, p.useRef)(new Set), t = (0, p.useRef)(new Map), n = (0, p.useCallback)((n, r, i) => {
            let a = {howl: i, id: r, soundName: n};
            e.current.add(a), t.current.has(n) || t.current.set(n, new Set), t.current.get(n).add(a), i.once(`end`, () => {
                e.current.delete(a), t.current.get(n)?.delete(a)
            }, r)
        }, []), r = (0, p.useCallback)((e, t = {}) => {
            (async () => {
                let r = await H(e, t);
                if (r == null) return;
                let i = A.get(e);
                i && n(e, r, i)
            })()
        }, [n]);
        return {
            play: r,
            playAsync: (0, p.useCallback)(async (e, t = {}) => {
                let r = await H(e, t);
                if (r == null) return null;
                let i = A.get(e);
                return i && n(e, r, i), r
            }, [n]),
            playUntilEnd: (0, p.useCallback)((e, t = {}) => Xe(e, t), []),
            playKeyboard: (0, p.useCallback)((e = !1) => {
                tt(e)
            }, []),
            playCasinoWheelItem: (0, p.useCallback)((e, t = {}) => {
                r(S.Casino_Wheel_Item, {...t, rate: ze(e), randomPitch: !1})
            }, [r]),
            playRandom: $e,
            playMenuStartLaptop: et,
            stopAll: (0, p.useCallback)(() => {
                e.current.forEach(e => {
                    try {
                        e.howl.stop(e.id)
                    } catch {
                    }
                }), e.current.clear(), t.current.clear()
            }, []),
            resumeAll: (0, p.useCallback)(() => {
                V()
            }, []),
            fadeOut: (0, p.useCallback)((n, r = .1) => {
                let i = t.current.get(n);
                if (!i || i.size === 0) return;
                let a = r * 1e3;
                for (let r of [...i]) try {
                    let i = r.howl.volume(r.id), o = typeof i == `number` ? i : 0;
                    r.howl.fade(o, 0, a, r.id), setTimeout(() => {
                        try {
                            r.howl.stop(r.id)
                        } catch {
                        }
                        e.current.delete(r), t.current.get(n)?.delete(r)
                    }, a + 50)
                } catch {
                    e.current.delete(r), t.current.get(n)?.delete(r)
                }
            }, []),
            updateSoundPan: (0, p.useCallback)((e, n) => {
                let r = t.current.get(e);
                if (r) {
                    for (let e of r) if (typeof e.howl.stereo == `function`) try {
                        e.howl.stereo(n, e.id)
                    } catch {
                    }
                }
            }, [])
        }
    };

function lt() {
    let {play: e} = ct(), t = (0, p.useCallback)(t => {
        let n = t.currentTarget;
        n instanceof HTMLButtonElement && n.disabled || e(S.Menu_Button_Back_Hover, {
            panElement: n,
            allowDuringTrackPlayback: !0
        })
    }, [e]), n = (0, p.useCallback)(() => {
        e(S.Menu_Button_Back_Click, {allowDuringTrackPlayback: !0})
    }, [e]);
    return {
        onBackHover: t, onBackClick: n, withBackClick: (0, p.useCallback)(e => () => {
            n(), e()
        }, [n])
    }
}

var ut = `settings_gamepad_enabled`, dt = `gamepadEnabledChanged`, ft = e => {
    window.dispatchEvent(new CustomEvent(dt, {detail: e}))
};

function pt(e = !1) {
    try {
        let t = localStorage.getItem(ut);
        return t == null ? e : t === `1` || t === `true`
    } catch {
        return e
    }
}

function mt(e) {
    try {
        localStorage.setItem(ut, e ? `1` : `0`)
    } catch {
    }
    return ft(e), e
}

function ht(e) {
    return window.addEventListener(dt, e), () => window.removeEventListener(dt, e)
}

var gt = `nassal-afk-activity`, _t = `nassal-afk-activity`;

function vt() {
    window.dispatchEvent(new Event(gt))
}

var yt = !1, bt = new Set;

function xt(e) {
    yt !== e && (yt = e, bt.forEach(t => t(e)))
}

function St() {
    return yt
}

function Ct(e) {
    return bt.add(e), () => {
        bt.delete(e)
    }
}

var wt = !1, Tt = new Set;

function Et(e) {
    return Tt.add(e), () => {
        Tt.delete(e)
    }
}

function Dt() {
    return wt
}

function Ot(e) {
    wt !== e && (wt = e, Tt.forEach(e => e()))
}

function kt() {
    return (0, p.useSyncExternalStore)(Et, Dt, () => !1)
}

var At = .35, jt = {
        A: 0,
        B: 1,
        X: 2,
        Y: 3,
        LB: 4,
        RB: 5,
        LT: 6,
        RT: 7,
        Back: 8,
        Start: 9,
        LStick: 10,
        RStick: 11,
        DPadUp: 12,
        DPadDown: 13,
        DPadLeft: 14,
        DPadRight: 15
    }, Mt = Object.keys(jt), Nt = .55, Pt = .5,
    Ft = {X: 0, A: 1, B: 2, Y: 3, LB: 4, RB: 5, LT: 6, RT: 7, Back: 8, Start: 9, LStick: 10, RStick: 11},
    It = `gp:statusChanged`, W = `gp:press`, Lt = `gp:release`, G = {connected: !1, vendor: `generic`}, Rt = null,
    zt = ``, K = [], q = null, J = !1;

function Y(e) {
    let t = e.toLowerCase();
    return t.includes(`dualshock`) || t.includes(`dualsense`) || t.includes(`playstation`) || t.includes(`ps4`) || t.includes(`ps5`) || t.includes(`sony`) || t.includes(`interactive entertainment`) || t.includes(`054c`) || t.includes(`0ce6`) || t.includes(`0df2`) || t.includes(`05c4`) || t.includes(`09cc`) || /\(054c[-:]0ce6/i.test(e) || /\(054c[-:]05c4/i.test(e) || /\(054c[-:]09cc/i.test(e) ? `playstation` : t.includes(`xbox`) || t.includes(`xinput`) || t.includes(`045e`) ? `xbox` : `generic`
}

function X(e, t, n = Pt) {
    let r = e.buttons[t];
    return r ? r.pressed || r.value > n : !1
}

function Z(e, t) {
    let n = e.axes[t] ?? 0;
    return Math.abs(n) > Nt
}

function Bt(e) {
    return e === 0 ? {
        DPadUp: !1,
        DPadDown: !1,
        DPadLeft: !1,
        DPadRight: !1
    } : {
        DPadUp: e >= -1 && e < -.7 || e >= .95 && e <= 1,
        DPadRight: e >= -.75 && e < -.1,
        DPadDown: e >= -.2 && e < .45,
        DPadLeft: e >= .4 && e <= 1
    }
}

function Vt(e, t) {
    let n = jt[t];
    if (X(e, n)) return !0;
    let r = e.axes[9];
    if (r !== void 0 && r !== 0) return Bt(r)[t] ?? !1;
    let i = e.axes[6] ?? 0, a = e.axes[7] ?? 0;
    if (Math.abs(i) > .5 || Math.abs(a) > .5) switch (t) {
        case`DPadUp`:
            return a < -.5;
        case`DPadDown`:
            return a > .5;
        case`DPadLeft`:
            return i < -.5;
        case`DPadRight`:
            return i > .5;
        default:
            return !1
    }
    return !1
}

function Ht(e, t) {
    if (t === `DPadUp` || t === `DPadDown` || t === `DPadLeft` || t === `DPadRight`) return Vt(e, t);
    let n = Ft[t];
    return n == null ? !1 : X(e, n) ? !0 : t === `LT` ? Z(e, 3) : t === `RT` ? Z(e, 4) : !1
}

function Ut(e, t) {
    if (e.mapping === `standard`) {
        let n = jt[t];
        return t === `DPadUp` || t === `DPadDown` || t === `DPadLeft` || t === `DPadRight` ? Vt(e, t) : t === `LT` || t === `RT` ? X(e, n) ? !0 : Z(e, t === `LT` ? 6 : 7) : X(e, n)
    }
    if (Y(e.id) === `playstation`) return Ht(e, t);
    let n = jt[t];
    return X(e, n) ? !0 : t === `LT` || t === `RT` ? Z(e, t === `LT` ? 6 : 7) ? !0 : Z(e, t === `LT` ? 3 : 4) : t === `DPadUp` || t === `DPadDown` || t === `DPadLeft` || t === `DPadRight` ? Vt(e, t) : !1
}

function Wt(e) {
    if (e.buttons.length === 0 && e.axes.length === 0) return -1;
    let t = e.buttons.length + e.axes.length * .1;
    e.connected && (t += 200), e.mapping === `standard` && (t += 1e3);
    let n = Y(e.id);
    (n === `playstation` || n === `xbox`) && (t += 500);
    let r = e.id.toLowerCase();
    return r.includes(`mouse`) || r.includes(`keyboard`) ? -1 : t
}

function Q() {
    if (!navigator.getGamepads) return null;
    let e = navigator.getGamepads(), t = null, n = -1;
    for (let r of e) {
        if (!r) continue;
        let e = Wt(r);
        e > n && (n = e, t = r)
    }
    return t
}

function Gt(e) {
    return Mt.map(t => Ut(e, t))
}

function Kt(e) {
    G.connected === e.connected && G.vendor === e.vendor || (G = e, window.dispatchEvent(new CustomEvent(It)))
}

function qt() {
    if (!J) {
        q = null;
        return
    }
    let e = Q();
    if (!e) {
        G.connected && (Rt = null, zt = ``, K = [], Kt({
            connected: !1,
            vendor: `generic`
        })), q = requestAnimationFrame(qt);
        return
    }
    if (e.index !== Rt || e.id !== zt) Rt = e.index, zt = e.id, K = Gt(e), Kt({connected: !0, vendor: Y(e.id)}); else {
        let t = !1;
        for (let n = 0; n < Mt.length; n++) {
            let r = Mt[n], i = Ut(e, r);
            i !== !!K[n] && (K[n] = i, i && (t = !0), window.dispatchEvent(new CustomEvent(i ? W : Lt, {detail: {button: r}})))
        }
        if (!t) {
            for (let n = 0; n < e.axes.length; n++) if (Math.abs(e.axes[n] ?? 0) > At) {
                t = !0;
                break
            }
        }
        t && vt()
    }
    q = requestAnimationFrame(qt)
}

function Jt() {
    J || (J = !0, q ??= requestAnimationFrame(qt))
}

function Yt() {
    J = !1, q != null && (cancelAnimationFrame(q), q = null), G.connected && (Rt = null, zt = ``, K = [], Kt({
        connected: !1,
        vendor: `generic`
    }))
}

typeof window < `u` && window.addEventListener(`gamepadconnected`, () => {
    J && q == null && (q = requestAnimationFrame(qt))
});

function Xt(e, t) {
    let n = Q()?.vibrationActuator;
    if (!(!n || typeof n.playEffect != `function`)) try {
        n.playEffect(`dual-rumble`, {
            duration: Math.max(0, Math.min(e, 5e3)),
            startDelay: 0,
            strongMagnitude: t?.strongMagnitude ?? .7,
            weakMagnitude: t?.weakMagnitude ?? .4
        })
    } catch {
    }
}

function Zt() {
    let e = Q()?.vibrationActuator;
    if (e) try {
        typeof e.reset == `function` ? e.reset() : typeof e.playEffect == `function` && e.playEffect(`dual-rumble`, {duration: 0})
    } catch {
    }
}

function $() {
    return (0, p.useSyncExternalStore)(ht, pt, () => !1)
}

function Qt() {
    let e = $();
    (0, p.useEffect)(() => {
        if (!e) {
            Yt();
            return
        }
        return Jt(), () => {
            Yt()
        }
    }, [e])
}

function $t() {
    let e = Q();
    if (e) return Y(e.id);
    if (!navigator.getGamepads) return G.vendor;
    for (let e of navigator.getGamepads()) {
        if (!e || e.buttons.length === 0 && e.axes.length === 0) continue;
        let t = Y(e.id);
        if (t !== `generic`) return t
    }
    return G.vendor
}

function en() {
    let e = $(), [t, n] = (0, p.useState)(G);
    return (0, p.useEffect)(() => {
        if (!e) return;
        let t = () => n({...G});
        return t(), window.addEventListener(It, t), window.addEventListener(`gamepadconnected`, t), () => {
            window.removeEventListener(It, t), window.removeEventListener(`gamepadconnected`, t)
        }
    }, [e]), e ? t : {connected: !1, vendor: `generic`}
}

function tn(e) {
    let t = Q();
    return t ? t.axes[e] ?? 0 : 0
}

function nn(e, t) {
    let n = $(), r = t?.orientation ?? `vertical`, i = t?.axisIndex ?? (r === `horizontal` ? 2 : 3), a = t?.speed ?? 14,
        o = t?.deadzone ?? .18, s = (0, p.useRef)(e);
    (0, p.useEffect)(() => {
        s.current = e
    }), (0, p.useEffect)(() => {
        if (!n) return;
        let e = null, t = () => {
            let n = s.current();
            if (n) {
                let e = tn(i);
                if (Math.abs(e) > o) {
                    let t = (e > 0 ? 1 : -1) * ((Math.abs(e) - o) / (1 - o)) * a;
                    r === `horizontal` ? n.scrollLeft += t : n.scrollTop += t
                }
            }
            e = requestAnimationFrame(t)
        };
        return e = requestAnimationFrame(t), () => {
            e != null && cancelAnimationFrame(e)
        }
    }, [n, i, a, o, r])
}

function rn(e, t, n = !0, r) {
    let i = (0, p.useRef)(t);
    (0, p.useEffect)(() => {
        i.current = t
    });
    let a = $(), o = kt(), s = r?.allowDuringKeyboard ?? !1;
    (0, p.useEffect)(() => {
        if (!n || !a || o && !s) return;
        let t = t => {
            t.detail.button === e && i.current()
        };
        return window.addEventListener(W, t), () => window.removeEventListener(W, t)
    }, [e, n, a, o, s])
}

function an(e, t, n, r = !0, i) {
    let a = (0, p.useRef)(t), o = (0, p.useRef)(n);
    (0, p.useEffect)(() => {
        a.current = t, o.current = n
    });
    let s = $(), c = kt(), l = i?.allowDuringKeyboard ?? !1;
    (0, p.useEffect)(() => {
        if (!r || !s || c && !l) return;
        let t = !1, n = n => {
            n.detail.button === e && !t && (t = !0, a.current())
        }, i = n => {
            n.detail.button === e && t && (t = !1, o.current())
        };
        return window.addEventListener(W, n), window.addEventListener(Lt, i), () => {
            window.removeEventListener(W, n), window.removeEventListener(Lt, i), t && (t = !1, o.current())
        }
    }, [e, r, s, c, l])
}

var on = !1, sn = new Set;

function cn(e) {
    return sn.add(e), () => {
        sn.delete(e)
    }
}

function ln() {
    return on
}

function un(e) {
    on !== e && (on = e, sn.forEach(e => e()))
}

function dn() {
    return (0, p.useSyncExternalStore)(cn, ln, () => !1)
}

var fn = 0, pn = new Set;

function mn(e) {
    return pn.add(e), () => {
        pn.delete(e)
    }
}

function hn() {
    return fn > 0
}

function gn() {
    return fn += 1, pn.forEach(e => e()), () => {
        fn = Math.max(0, fn - 1), pn.forEach(e => e())
    }
}

function _n(e) {
    (0, p.useEffect)(() => {
        if (e) return gn()
    }, [e])
}

function vn() {
    return (0, p.useSyncExternalStore)(mn, hn, () => !1)
}

export {
    te as $,
    at as A,
    Ke as B,
    lt as C,
    Fe as D,
    Ye as E,
    Me as F,
    be as G,
    ye as H,
    Ie as I,
    me as J,
    ve as K,
    st as L,
    et as M,
    U as N,
    Qe as O,
    ot as P,
    S as Q,
    Ze as R,
    mt as S,
    Pe as T,
    he as U,
    ce as V,
    fe as W,
    D as X,
    _e as Y,
    ie as Z,
    xt as _,
    tn as a,
    m as at,
    _t as b,
    nn as c,
    o as ct,
    $ as d,
    y as et,
    Qt as f,
    Ot as g,
    St as h,
    _n as i,
    h as it,
    rt as j,
    it as k,
    rn as l,
    s as lt,
    Xt as m,
    dn as n,
    _ as nt,
    $t as o,
    f as ot,
    en as p,
    pe as q,
    vn as r,
    g as rt,
    Zt as s,
    d as st,
    un as t,
    v as tt,
    an as u,
    l as ut,
    Ct as v,
    Ne as w,
    vt as x,
    gt as y,
    ct as z
};