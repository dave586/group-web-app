(function () {
    var root = this;
    var prevUl = root.signals;
    var signals = function (obj) {
        if (obj instanceof signals) return obj;
        if (!(this instanceof signals)) return new signals(obj);
        this._wrapped = obj;
    };

    if (typeof exports !== 'undefined') {
        if (typeof module !== 'undefined' && module.exports) {
            exports = module.exports = signals;
        }
        exports.signals = signals;
    } else {
        root.signals = signals;
    }

    signals.utils = {
        setBrowser: function () {
            var browser = ul.browser;
            browser.setBrowserClass();
        }
    }
}).call(this);