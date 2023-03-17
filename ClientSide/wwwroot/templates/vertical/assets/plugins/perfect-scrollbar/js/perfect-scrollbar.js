! function (t, e) {
    "object" == typeof exports && "undefined" != typeof module ? module.exports = e() : "function" == typeof define && define.amd ? define(e) : t.PerfectScrollbar = e()
}(this, function () {
    "use strict";

    function t(t) {
        return getComputedStyle(t)
    }

    function e(t, e) {
        for (var i in e) {
            var r = e[i];
            "number" == typeof r && (r += "px"), t.style[i] = r
        }
        return t
    }

    function i(t) {
        var e = document.createElement("div");
        return e.className = t, e
    }
    var r = "undefined" != typeof Element && (Element.prototype.matches || Element.prototype.webkitMatchesSelector || Element.prototype.mozMatchesSelector || Element.prototype.msMatchesSelector);

    function l(t, e) {
        if (!r) throw new Error("No element matching method supported");
        return r.call(t, e)
    }

    function n(t) {
        t.remove ? t.remove() : t.parentNode && t.parentNode.removeChild(t)
    }

    function o(t, e) {
        return Array.prototype.filter.call(t.children, function (t) {
            return l(t, e)
        })
    }
    var s = {
            main: "ps",
            element: {
                thumb: function (t) {
                    return "ps__thumb-" + t
                },
                rail: function (t) {
                    return "ps__rail-" + t
                },
                consuming: "ps__child--consume"
            },
            state: {
                focus: "ps--focus",
                clicking: "ps--clicking",
                active: function (t) {
                    return "ps--active-" + t
                },
                scrolling: function (t) {
                    return "ps--scrolling-" + t
                }
            }
        },
        a = {
            x: null,
            y: null
        };

    function c(t, e) {
        var i = t.element.classList,
            r = s.state.scrolling(e);
        i.contains(r) ? clearTimeout(a[e]) : i.add(r)
    }

    function h(t, e) {
        a[e] = setTimeout(function () {
            return t.isAlive && t.element.classList.remove(s.state.scrolling(e))
        }, t.settings.scrollingThreshold)
    }
    var u = function (t) {
            this.element = t, this.handlers = {}
        },
        d = {
            isEmpty: {
                configurable: !0
            }
        };
    u.prototype.bind = function (t, e) {
        void 0 === this.handlers[t] && (this.handlers[t] = []), this.handlers[t].push(e), this.element.addEventListener(t, e, !1)
    }, u.prototype.unbind = function (t, e) {
        var i = this;
        this.handlers[t] = this.handlers[t].filter(function (r) {
            return !(!e || r === e) || (i.element.removeEventListener(t, r, !1), !1)
        })
    }, u.prototype.unbindAll = function () {
        for (var t in this.handlers) this.unbind(t)
    }, d.isEmpty.get = function () {
        var t = this;
        return Object.keys(this.handlers).every(function (e) {
            return 0 === t.handlers[e].length
        })
    }, Object.defineProperties(u.prototype, d);
    var f = function () {
        this.eventElements = []
    };

    function p(t) {
        if ("function" == typeof window.CustomEvent) return new CustomEvent(t);
        var e = document.createEvent("CustomEvent");
        return e.initCustomEvent(t, !1, !1, void 0), e
    }
    f.prototype.eventElement = function (t) {
        var e = this.eventElements.filter(function (e) {
            return e.element === t
        })[0];
        return e || (e = new u(t), this.eventElements.push(e)), e
    }, f.prototype.bind = function (t, e, i) {
        this.eventElement(t).bind(e, i)
    }, f.prototype.unbind = function (t, e, i) {
        var r = this.eventElement(t);
        r.unbind(e, i), r.isEmpty && this.eventElements.splice(this.eventElements.indexOf(r), 1)
    }, f.prototype.unbindAll = function () {
        this.eventElements.forEach(function (t) {
            return t.unbindAll()
        }), this.eventElements = []
    }, f.prototype.once = function (t, e, i) {
        var r = this.eventElement(t),
            l = function (t) {
                r.unbind(e, l), i(t)
            };
        r.bind(e, l)
    };
    var b = function (t, e, i, r, l) {
        var n;
        if (void 0 === r && (r = !0), void 0 === l && (l = !1), "top" === e) n = ["contentHeight", "containerHeight", "scrollTop", "y", "up", "down"];
        else {
            if ("left" !== e) throw new Error("A proper axis should be provided");
            n = ["contentWidth", "containerWidth", "scrollLeft", "x", "left", "right"]
        }! function (t, e, i, r, l) {
            var n = i[0],
                o = i[1],
                s = i[2],
                a = i[3],
                u = i[4],
                d = i[5];
            void 0 === r && (r = !0);
            void 0 === l && (l = !1);
            var f = t.element;
            t.reach[a] = null, f[s] < 1 && (t.reach[a] = "start");
            f[s] > t[n] - t[o] - 1 && (t.reach[a] = "end");
            e && (f.dispatchEvent(p("ps-scroll-" + a)), e < 0 ? f.dispatchEvent(p("ps-scroll-" + u)) : e > 0 && f.dispatchEvent(p("ps-scroll-" + d)), r && function (t, e) {
                c(t, e), h(t, e)
            }(t, a));
            t.reach[a] && (e || l) && f.dispatchEvent(p("ps-" + a + "-reach-" + t.reach[a]))
        }(t, i, n, r, l)
    };

    function g(t) {
        return parseInt(t, 10) || 0
    }
    var v = {
            isWebKit: "undefined" != typeof document && "WebkitAppearance" in document.documentElement.style,
            supportsTouch: "undefined" != typeof window && ("ontouchstart" in window || window.DocumentTouch && document instanceof window.DocumentTouch),
            supportsIePointer: "undefined" != typeof navigator && navigator.msMaxTouchPoints,
            isChrome: "undefined" != typeof navigator && /Chrome/i.test(navigator && navigator.userAgent)
        },
        m = function (t) {
            var i = t.element,
                r = Math.floor(i.scrollTop),
                l = i.getBoundingClientRect();
            t.containerWidth = Math.ceil(l.width), t.containerHeight = Math.ceil(l.height), t.contentWidth = i.scrollWidth, t.contentHeight = i.scrollHeight, i.contains(t.scrollbarXRail) || (o(i, s.element.rail("x")).forEach(function (t) {
                    return n(t)
                }), i.appendChild(t.scrollbarXRail)), i.contains(t.scrollbarYRail) || (o(i, s.element.rail("y")).forEach(function (t) {
                    return n(t)
                }), i.appendChild(t.scrollbarYRail)), !t.settings.suppressScrollX && t.containerWidth + t.settings.scrollXMarginOffset < t.contentWidth ? (t.scrollbarXActive = !0, t.railXWidth = t.containerWidth - t.railXMarginWidth, t.railXRatio = t.containerWidth / t.railXWidth, t.scrollbarXWidth = Y(t, g(t.railXWidth * t.containerWidth / t.contentWidth)), t.scrollbarXLeft = g((t.negativeScrollAdjustment + i.scrollLeft) * (t.railXWidth - t.scrollbarXWidth) / (t.contentWidth - t.containerWidth))) : t.scrollbarXActive = !1, !t.settings.suppressScrollY && t.containerHeight + t.settings.scrollYMarginOffset < t.contentHeight ? (t.scrollbarYActive = !0, t.railYHeight = t.containerHeight - t.railYMarginHeight, t.railYRatio = t.containerHeight / t.railYHeight, t.scrollbarYHeight = Y(t, g(t.railYHeight * t.containerHeight / t.contentHeight)), t.scrollbarYTop = g(r * (t.railYHeight - t.scrollbarYHeight) / (t.contentHeight - t.containerHeight))) : t.scrollbarYActive = !1, t.scrollbarXLeft >= t.railXWidth - t.scrollbarXWidth && (t.scrollbarXLeft = t.railXWidth - t.scrollbarXWidth), t.scrollbarYTop >= t.railYHeight - t.scrollbarYHeight && (t.scrollbarYTop = t.railYHeight - t.scrollbarYHeight),
                function (t, i) {
                    var r = {
                            width: i.railXWidth
                        },
                        l = Math.floor(t.scrollTop);
                    i.isRtl ? r.left = i.negativeScrollAdjustment + t.scrollLeft + i.containerWidth - i.contentWidth : r.left = t.scrollLeft;
                    i.isScrollbarXUsingBottom ? r.bottom = i.scrollbarXBottom - l : r.top = i.scrollbarXTop + l;
                    e(i.scrollbarXRail, r);
                    var n = {
                        top: l,
                        height: i.railYHeight
                    };
                    i.isScrollbarYUsingRight ? i.isRtl ? n.right = i.contentWidth - (i.negativeScrollAdjustment + t.scrollLeft) - i.scrollbarYRight - i.scrollbarYOuterWidth : n.right = i.scrollbarYRight - t.scrollLeft : i.isRtl ? n.left = i.negativeScrollAdjustment + t.scrollLeft + 2 * i.containerWidth - i.contentWidth - i.scrollbarYLeft - i.scrollbarYOuterWidth : n.left = i.scrollbarYLeft + t.scrollLeft;
                    e(i.scrollbarYRail, n), e(i.scrollbarX, {
                        left: i.scrollbarXLeft,
                        width: i.scrollbarXWidth - i.railBorderXWidth
                    }), e(i.scrollbarY, {
                        top: i.scrollbarYTop,
                        height: i.scrollbarYHeight - i.railBorderYWidth
                    })
                }(i, t), t.scrollbarXActive ? i.classList.add(s.state.active("x")) : (i.classList.remove(s.state.active("x")), t.scrollbarXWidth = 0, t.scrollbarXLeft = 0, i.scrollLeft = 0), t.scrollbarYActive ? i.classList.add(s.state.active("y")) : (i.classList.remove(s.state.active("y")), t.scrollbarYHeight = 0, t.scrollbarYTop = 0, i.scrollTop = 0)
        };

    function Y(t, e) {
        return t.settings.minScrollbarLength && (e = Math.max(e, t.settings.minScrollbarLength)), t.settings.maxScrollbarLength && (e = Math.min(e, t.settings.maxScrollbarLength)), e
    }

    function X(t, e) {
        var i = e[0],
            r = e[1],
            l = e[2],
            n = e[3],
            o = e[4],
            a = e[5],
            u = e[6],
            d = e[7],
            f = e[8],
            p = t.element,
            b = null,
            g = null,
            v = null;

        function Y(e) {
            p[u] = b + v * (e[l] - g), c(t, d), m(t), e.stopPropagation(), e.preventDefault()
        }

        function X() {
            h(t, d), t[f].classList.remove(s.state.clicking), t.event.unbind(t.ownerDocument, "mousemove", Y)
        }
        t.event.bind(t[o], "mousedown", function (e) {
            b = p[u], g = e[l], v = (t[r] - t[i]) / (t[n] - t[a]), t.event.bind(t.ownerDocument, "mousemove", Y), t.event.once(t.ownerDocument, "mouseup", X), t[f].classList.add(s.state.clicking), e.stopPropagation(), e.preventDefault()
        })
    }
    var w = {
            "click-rail": function (t) {
                t.event.bind(t.scrollbarY, "mousedown", function (t) {
                    return t.stopPropagation()
                }), t.event.bind(t.scrollbarYRail, "mousedown", function (e) {
                    var i = e.pageY - window.pageYOffset - t.scrollbarYRail.getBoundingClientRect().top > t.scrollbarYTop ? 1 : -1;
                    t.element.scrollTop += i * t.containerHeight, m(t), e.stopPropagation()
                }), t.event.bind(t.scrollbarX, "mousedown", function (t) {
                    return t.stopPropagation()
                }), t.event.bind(t.scrollbarXRail, "mousedown", function (e) {
                    var i = e.pageX - window.pageXOffset - t.scrollbarXRail.getBoundingClientRect().left > t.scrollbarXLeft ? 1 : -1;
                    t.element.scrollLeft += i * t.containerWidth, m(t), e.stopPropagation()
                })
            },
            "drag-thumb": function (t) {
                X(t, ["containerWidth", "contentWidth", "pageX", "railXWidth", "scrollbarX", "scrollbarXWidth", "scrollLeft", "x", "scrollbarXRail"]), X(t, ["containerHeight", "contentHeight", "pageY", "railYHeight", "scrollbarY", "scrollbarYHeight", "scrollTop", "y", "scrollbarYRail"])
            },
            keyboard: function (t) {
                var e = t.element;
                t.event.bind(t.ownerDocument, "keydown", function (i) {
                    if (!(i.isDefaultPrevented && i.isDefaultPrevented() || i.defaultPrevented) && (l(e, ":hover") || l(t.scrollbarX, ":focus") || l(t.scrollbarY, ":focus"))) {
                        var r, n = document.activeElement ? document.activeElement : t.ownerDocument.activeElement;
                        if (n) {
                            if ("IFRAME" === n.tagName) n = n.contentDocument.activeElement;
                            else
                                for (; n.shadowRoot;) n = n.shadowRoot.activeElement;
                            if (l(r = n, "input,[contenteditable]") || l(r, "select,[contenteditable]") || l(r, "textarea,[contenteditable]") || l(r, "button,[contenteditable]")) return
                        }
                        var o = 0,
                            s = 0;
                        switch (i.which) {
                            case 37:
                                o = i.metaKey ? -t.contentWidth : i.altKey ? -t.containerWidth : -30;
                                break;
                            case 38:
                                s = i.metaKey ? t.contentHeight : i.altKey ? t.containerHeight : 30;
                                break;
                            case 39:
                                o = i.metaKey ? t.contentWidth : i.altKey ? t.containerWidth : 30;
                                break;
                            case 40:
                                s = i.metaKey ? -t.contentHeight : i.altKey ? -t.containerHeight : -30;
                                break;
                            case 32:
                                s = i.shiftKey ? t.containerHeight : -t.containerHeight;
                                break;
                            case 33:
                                s = t.containerHeight;
                                break;
                            case 34:
                                s = -t.containerHeight;
                                break;
                            case 36:
                                s = t.contentHeight;
                                break;
                            case 35:
                                s = -t.contentHeight;
                                break;
                            default:
                                return
                        }
                        t.settings.suppressScrollX && 0 !== o || t.settings.suppressScrollY && 0 !== s || (e.scrollTop -= s, e.scrollLeft += o, m(t), function (i, r) {
                            var l = Math.floor(e.scrollTop);
                            if (0 === i) {
                                if (!t.scrollbarYActive) return !1;
                                if (0 === l && r > 0 || l >= t.contentHeight - t.containerHeight && r < 0) return !t.settings.wheelPropagation
                            }
                            var n = e.scrollLeft;
                            if (0 === r) {
                                if (!t.scrollbarXActive) return !1;
                                if (0 === n && i < 0 || n >= t.contentWidth - t.containerWidth && i > 0) return !t.settings.wheelPropagation
                            }
                            return !0
                        }(o, s) && i.preventDefault())
                    }
                })
            },
            wheel: function (e) {
                var i = e.element;

                function r(r) {
                    var l = function (t) {
                            var e = t.deltaX,
                                i = -1 * t.deltaY;
                            return void 0 !== e && void 0 !== i || (e = -1 * t.wheelDeltaX / 6, i = t.wheelDeltaY / 6), t.deltaMode && 1 === t.deltaMode && (e *= 10, i *= 10), e != e && i != i && (e = 0, i = t.wheelDelta), t.shiftKey ? [-i, -e] : [e, i]
                        }(r),
                        n = l[0],
                        o = l[1];
                    if (! function (e, r, l) {
                            if (!v.isWebKit && i.querySelector("select:focus")) return !0;
                            if (!i.contains(e)) return !1;
                            for (var n = e; n && n !== i;) {
                                if (n.classList.contains(s.element.consuming)) return !0;
                                var o = t(n);
                                if ([o.overflow, o.overflowX, o.overflowY].join("").match(/(scroll|auto)/)) {
                                    var a = n.scrollHeight - n.clientHeight;
                                    if (a > 0 && !(0 === n.scrollTop && l > 0 || n.scrollTop === a && l < 0)) return !0;
                                    var c = n.scrollWidth - n.clientWidth;
                                    if (c > 0 && !(0 === n.scrollLeft && r < 0 || n.scrollLeft === c && r > 0)) return !0
                                }
                                n = n.parentNode
                            }
                            return !1
                        }(r.target, n, o)) {
                        var a = !1;
                        e.settings.useBothWheelAxes ? e.scrollbarYActive && !e.scrollbarXActive ? (o ? i.scrollTop -= o * e.settings.wheelSpeed : i.scrollTop += n * e.settings.wheelSpeed, a = !0) : e.scrollbarXActive && !e.scrollbarYActive && (n ? i.scrollLeft += n * e.settings.wheelSpeed : i.scrollLeft -= o * e.settings.wheelSpeed, a = !0) : (i.scrollTop -= o * e.settings.wheelSpeed, i.scrollLeft += n * e.settings.wheelSpeed), m(e), (a = a || function (t, r) {
                            var l = Math.floor(i.scrollTop),
                                n = 0 === i.scrollTop,
                                o = l + i.offsetHeight === i.scrollHeight,
                                s = 0 === i.scrollLeft,
                                a = i.scrollLeft + i.offsetWidth === i.scrollWidth;
                            return !(Math.abs(r) > Math.abs(t) ? n || o : s || a) || !e.settings.wheelPropagation
                        }(n, o)) && !r.ctrlKey && (r.stopPropagation(), r.preventDefault())
                    }
                }
                void 0 !== window.onwheel ? e.event.bind(i, "wheel", r) : void 0 !== window.onmousewheel && e.event.bind(i, "mousewheel", r)
            },
            touch: function (e) {
                if (v.supportsTouch || v.supportsIePointer) {
                    var i = e.element,
                        r = {},
                        l = 0,
                        n = {},
                        o = null;
                    v.supportsTouch ? (e.event.bind(i, "touchstart", u), e.event.bind(i, "touchmove", d), e.event.bind(i, "touchend", f)) : v.supportsIePointer && (window.PointerEvent ? (e.event.bind(i, "pointerdown", u), e.event.bind(i, "pointermove", d), e.event.bind(i, "pointerup", f)) : window.MSPointerEvent && (e.event.bind(i, "MSPointerDown", u), e.event.bind(i, "MSPointerMove", d), e.event.bind(i, "MSPointerUp", f)))
                }

                function a(t, r) {
                    i.scrollTop -= r, i.scrollLeft -= t, m(e)
                }

                function c(t) {
                    return t.targetTouches ? t.targetTouches[0] : t
                }

                function h(t) {
                    return !(t.pointerType && "pen" === t.pointerType && 0 === t.buttons || (!t.targetTouches || 1 !== t.targetTouches.length) && (!t.pointerType || "mouse" === t.pointerType || t.pointerType === t.MSPOINTER_TYPE_MOUSE))
                }

                function u(t) {
                    if (h(t)) {
                        var e = c(t);
                        r.pageX = e.pageX, r.pageY = e.pageY, l = (new Date).getTime(), null !== o && clearInterval(o)
                    }
                }

                function d(o) {
                    if (h(o)) {
                        var u = c(o),
                            d = {
                                pageX: u.pageX,
                                pageY: u.pageY
                            },
                            f = d.pageX - r.pageX,
                            p = d.pageY - r.pageY;
                        if (function (e, r, l) {
                                if (!i.contains(e)) return !1;
                                for (var n = e; n && n !== i;) {
                                    if (n.classList.contains(s.element.consuming)) return !0;
                                    var o = t(n);
                                    if ([o.overflow, o.overflowX, o.overflowY].join("").match(/(scroll|auto)/)) {
                                        var a = n.scrollHeight - n.clientHeight;
                                        if (a > 0 && !(0 === n.scrollTop && l > 0 || n.scrollTop === a && l < 0)) return !0;
                                        var c = n.scrollLeft - n.clientWidth;
                                        if (c > 0 && !(0 === n.scrollLeft && r < 0 || n.scrollLeft === c && r > 0)) return !0
                                    }
                                    n = n.parentNode
                                }
                                return !1
                            }(o.target, f, p)) return;
                        a(f, p), r = d;
                        var b = (new Date).getTime(),
                            g = b - l;
                        g > 0 && (n.x = f / g, n.y = p / g, l = b),
                            function (t, r) {
                                var l = Math.floor(i.scrollTop),
                                    n = i.scrollLeft,
                                    o = Math.abs(t),
                                    s = Math.abs(r);
                                if (s > o) {
                                    if (r < 0 && l === e.contentHeight - e.containerHeight || r > 0 && 0 === l) return 0 === window.scrollY && r > 0 && v.isChrome
                                } else if (o > s && (t < 0 && n === e.contentWidth - e.containerWidth || t > 0 && 0 === n)) return !0;
                                return !0
                            }(f, p) && o.preventDefault()
                    }
                }

                function f() {
                    e.settings.swipeEasing && (clearInterval(o), o = setInterval(function () {
                        e.isInitialized ? clearInterval(o) : n.x || n.y ? Math.abs(n.x) < .01 && Math.abs(n.y) < .01 ? clearInterval(o) : (a(30 * n.x, 30 * n.y), n.x *= .8, n.y *= .8) : clearInterval(o)
                    }, 10))
                }
            }
        },
        y = function (r, l) {
            var n = this;
            if (void 0 === l && (l = {}), "string" == typeof r && (r = document.querySelector(r)), !r || !r.nodeName) throw new Error("no element is specified to initialize PerfectScrollbar");
            for (var o in this.element = r, r.classList.add(s.main), this.settings = {
                    handlers: ["click-rail", "drag-thumb", "keyboard", "wheel", "touch"],
                    maxScrollbarLength: null,
                    minScrollbarLength: null,
                    scrollingThreshold: 1e3,
                    scrollXMarginOffset: 0,
                    scrollYMarginOffset: 0,
                    suppressScrollX: !1,
                    suppressScrollY: !1,
                    swipeEasing: !0,
                    useBothWheelAxes: !1,
                    wheelPropagation: !0,
                    wheelSpeed: 1
                }, l) n.settings[o] = l[o];
            this.containerWidth = null, this.containerHeight = null, this.contentWidth = null, this.contentHeight = null;
            var a, c, h = function () {
                    return r.classList.add(s.state.focus)
                },
                u = function () {
                    return r.classList.remove(s.state.focus)
                };
            this.isRtl = "rtl" === t(r).direction, this.isNegativeScroll = (c = r.scrollLeft, r.scrollLeft = -1, a = r.scrollLeft < 0, r.scrollLeft = c, a), this.negativeScrollAdjustment = this.isNegativeScroll ? r.scrollWidth - r.clientWidth : 0, this.event = new f, this.ownerDocument = r.ownerDocument || document, this.scrollbarXRail = i(s.element.rail("x")), r.appendChild(this.scrollbarXRail), this.scrollbarX = i(s.element.thumb("x")), this.scrollbarXRail.appendChild(this.scrollbarX), this.scrollbarX.setAttribute("tabindex", 0), this.event.bind(this.scrollbarX, "focus", h), this.event.bind(this.scrollbarX, "blur", u), this.scrollbarXActive = null, this.scrollbarXWidth = null, this.scrollbarXLeft = null;
            var d = t(this.scrollbarXRail);
            this.scrollbarXBottom = parseInt(d.bottom, 10), isNaN(this.scrollbarXBottom) ? (this.isScrollbarXUsingBottom = !1, this.scrollbarXTop = g(d.top)) : this.isScrollbarXUsingBottom = !0, this.railBorderXWidth = g(d.borderLeftWidth) + g(d.borderRightWidth), e(this.scrollbarXRail, {
                display: "block"
            }), this.railXMarginWidth = g(d.marginLeft) + g(d.marginRight), e(this.scrollbarXRail, {
                display: ""
            }), this.railXWidth = null, this.railXRatio = null, this.scrollbarYRail = i(s.element.rail("y")), r.appendChild(this.scrollbarYRail), this.scrollbarY = i(s.element.thumb("y")), this.scrollbarYRail.appendChild(this.scrollbarY), this.scrollbarY.setAttribute("tabindex", 0), this.event.bind(this.scrollbarY, "focus", h), this.event.bind(this.scrollbarY, "blur", u), this.scrollbarYActive = null, this.scrollbarYHeight = null, this.scrollbarYTop = null;
            var p = t(this.scrollbarYRail);
            this.scrollbarYRight = parseInt(p.right, 10), isNaN(this.scrollbarYRight) ? (this.isScrollbarYUsingRight = !1, this.scrollbarYLeft = g(p.left)) : this.isScrollbarYUsingRight = !0, this.scrollbarYOuterWidth = this.isRtl ? function (e) {
                var i = t(e);
                return g(i.width) + g(i.paddingLeft) + g(i.paddingRight) + g(i.borderLeftWidth) + g(i.borderRightWidth)
            }(this.scrollbarY) : null, this.railBorderYWidth = g(p.borderTopWidth) + g(p.borderBottomWidth), e(this.scrollbarYRail, {
                display: "block"
            }), this.railYMarginHeight = g(p.marginTop) + g(p.marginBottom), e(this.scrollbarYRail, {
                display: ""
            }), this.railYHeight = null, this.railYRatio = null, this.reach = {
                x: r.scrollLeft <= 0 ? "start" : r.scrollLeft >= this.contentWidth - this.containerWidth ? "end" : null,
                y: r.scrollTop <= 0 ? "start" : r.scrollTop >= this.contentHeight - this.containerHeight ? "end" : null
            }, this.isAlive = !0, this.settings.handlers.forEach(function (t) {
                return w[t](n)
            }), this.lastScrollTop = Math.floor(r.scrollTop), this.lastScrollLeft = r.scrollLeft, this.event.bind(this.element, "scroll", function (t) {
                return n.onScroll(t)
            }), m(this)
        };
    return y.prototype.update = function () {
        this.isAlive && (this.negativeScrollAdjustment = this.isNegativeScroll ? this.element.scrollWidth - this.element.clientWidth : 0, e(this.scrollbarXRail, {
            display: "block"
        }), e(this.scrollbarYRail, {
            display: "block"
        }), this.railXMarginWidth = g(t(this.scrollbarXRail).marginLeft) + g(t(this.scrollbarXRail).marginRight), this.railYMarginHeight = g(t(this.scrollbarYRail).marginTop) + g(t(this.scrollbarYRail).marginBottom), e(this.scrollbarXRail, {
            display: "none"
        }), e(this.scrollbarYRail, {
            display: "none"
        }), m(this), b(this, "top", 0, !1, !0), b(this, "left", 0, !1, !0), e(this.scrollbarXRail, {
            display: ""
        }), e(this.scrollbarYRail, {
            display: ""
        }))
    }, y.prototype.onScroll = function (t) {
        this.isAlive && (m(this), b(this, "top", this.element.scrollTop - this.lastScrollTop), b(this, "left", this.element.scrollLeft - this.lastScrollLeft), this.lastScrollTop = Math.floor(this.element.scrollTop), this.lastScrollLeft = this.element.scrollLeft)
    }, y.prototype.destroy = function () {
        this.isAlive && (this.event.unbindAll(), n(this.scrollbarX), n(this.scrollbarY), n(this.scrollbarXRail), n(this.scrollbarYRail), this.removePsClasses(), this.element = null, this.scrollbarX = null, this.scrollbarY = null, this.scrollbarXRail = null, this.scrollbarYRail = null, this.isAlive = !1)
    }, y.prototype.removePsClasses = function () {
        this.element.className = this.element.className.split(" ").filter(function (t) {
            return !t.match(/^ps([-_].+|)$/)
        }).join(" ")
    }, y
});