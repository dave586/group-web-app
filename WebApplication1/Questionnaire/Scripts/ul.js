(function () {
    var root = this;
    var prevUl = root.ul;
    var ul = function (obj) {
        if (obj instanceof ul) return obj;
        if (!(this instanceof ul)) return new ul(obj);
        this._wrapped = obj;
    };

    if (typeof exports !== 'undefined') {
        if (typeof module !== 'undefined' && module.exports) {
            exports = module.exports = ul;
        }
        exports.ul = ul;
    } else {
        root.ul = ul;
    }

    var arrayProto = Array.prototype,
        stringProto = String.prototype,
        objProto = Object.prototype,
        funcProto = Function.prototype;

    if (!arrayProto.isArray) {
        arrayProto.isArray = function (vArg) {
            return Object.prototype.toString.call(vArg) === "[object Array]";
        };
    }

    if (!stringProto.trim) {
        stringProto.trim = function () {
            return this.replace(/^\s+|\s+$/g, '');
        };
    }

    //stringProto.format = function () {
    //    var s = arguments[0];
    //    for (var i = 0; i < arguments.length - 1; i++) {
    //        var reg = new RegExp("\\{" + i + "\\}", "gm");
    //        s = s.replace(reg, arguments[i + 1]);
    //    }

    //    return s;
    //};

    if (!String.format) {
        String.format = function (format) {
            var args = Array.prototype.slice.call(arguments, 1);
            return format.replace(/{(\d+)}/g, function (match, number) {
                return typeof args[number] != 'undefined'
                  ? args[number]
                  : match
                ;
            });
        };
    }

    stringProto.replaceIgnoreCase = function (strReplace, strWith) {
        var reg = new RegExp(strReplace, 'ig');
        return this.replace(reg, strWith);
    };

    stringProto.CapAndSpaceCamelCase = function () {
        return this.replace(/([A-Z])/g, ' $1')
            .replace(/^./, function (str) { return str.toUpperCase(); });
    };

    stringProto.contains = function (key) {
        return this.indexOf(key) !== -1;
    };

    stringProto.startsWith = function (prefix) {
        return (this.substr(0, prefix.length) === prefix);
    };

    stringProto.endsWith = function (suffix) {
        return (this.substr(this.length - suffix.length) === suffix);
    };

    stringProto.removeWhiteSpace = function () {
        return this.replace(/\s+/g, '');
    };

    stringProto.toInt = function () {
        var i = parseInt(this, 10);
        if (!i || isNaN(i)) {
            return 0;
        } else {
            return i;
        }
    };

    stringProto.toMinutes = function (minutes) {
        if (minutes < 10) {
            minutes = '0' + minutes;
        }
        return minutes;
    },

    stringProto.toDecimal = function () {
        var i = parseFloat(this);
        if (!i || isNaN(i)) {
            return 0.0;
        } else {
            return i;
        }
    };

    ul.inherit = function (proto) {
        function f() { }
        f.prototype = proto;
        return new f;
    };

    ul.extend = function (child, parent) {
        child.prototype = ul.inherit(parent.prototype);
        child.prototype.constructor = child;
        child.parent = parent.prototype;
    };

    ul.utils = {

        isNullOrEmpty: function (val) {
            if (null === val || '' === val || undefined === val || 'null' === val) {
                return true;
            } else {
                return false;
            }
        },

        isNullOrEmptyWhiteSpace: function (val) {
            if (!val) {
                return true;
            } else {
                return this.isNullOrEmpty(val.trim());
            }
        },

        defaultIfNull: function (val, def) {
            if (this.isNullOrEmpty(val)) {
                return def;
            } else {
                return val;
            }
        },

        isNumeric: function (val) {
            return !isNaN(parseFloat(val)) && isFinite(val);
        },

        convertToInt32: function (val, def) {
            var currentInt = parseInt(val);
            if (currentInt == undefined || isNaN(currentInt)) {
                if (this.isNullOrEmpty(def)) {
                    return 0;
                }
                else {
                    return def;
                }
            }
            else {
                return currentInt;
            }
        },

        convertToDecimal: function (val, def) {
            var currentDec = parseFloat(val);
            if (currentDec == undefined || isNaN(currentDec)) {
                if (this.isNullOrEmpty(def)) {
                    return 0;
                }
                else {
                    return def;
                }
            }
            else {
                return currentDec;
            }
        },

        decimalDefaultIfZero: function (val, def) {
            if (this.convertToDecimal(val, -1) < 0) {
                return def;
            }
            else {
                return val;
            }
        },

        capitalise: function (val) {
            if (!ul.utils.isNullOrEmptyWhiteSpace(val)) {
                val = val.replace(/\w\S*/g, function (txt) { return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase(); });
            }
            return val;
        },

        emptyVal: function (val) {
            if (this.isNullOrEmpty(val)) {
                return '';
            } else {
                return val;
            }
        },

        arrayPosition: function (arrayVar, val) {
            for (var i = 0; i < arrayVar.length; ++i) {
                if (arrayVar[i] === val) {
                    return i;
                }
            }
            return -1;
        },

        today: function () {
            var now = new Date();
            return now.getFullYear() + "-" + now.getMonth() + "-" + now.getDay();
        },

        sequentialGuid: function () {
            var result, i, j;
            result = '';
            for (j = 0; j < 32; j++) {
                if (j == 8 || j == 12 || j == 16 || j == 20) {
                    result = result + '-';
                }
                i = Math.floor(Math.random() * 16).toString(16).toLowerCase();
                result = result + i;
            }
            return result;
        }
    };

    ul.web = {

        safePopup: function (url, name, param, close) {
            var windowRef = window.open(url, name, param);
            if (ul.utils.isNullOrEmpty(windowRef)) {
                alert('It looks like you are using windows popup blocking software in your browser. Please turn it off or always allow popups for this site.');
            } else {
                if (close) windowRef.close();
            }
            return windowRef;
        },

        htmlEncode: function (html) {
            var h = html.replace(/&/g, '&amp;')
                        .replace(/"/g, '&quot;')
                        .replace(/'/g, '&#39;')
                        .replace(/</g, '&lt;')
                        .replace(/>/g, '&gt;');
            return h;
        },

        htmlDecode: function (html) {
            var h = html.replace(/&quot;/g, '"')
                        .replace(/&#39;/g, "'")
                        .replace(/&lt;/g, '<')
                        .replace(/&gt;/g, '>')
                        .replace(/&amp;/g, '&');
            return h;
        },

        updateQueryStringParam: function (key, value, url) {
            if (!url) url = window.location.href;
            var re = new RegExp("([?|&])" + key + "=.*?(&|#|$)(.*)", "gi");

            if (re.test(url)) {
                if (typeof value !== 'undefined' && value !== null)
                    return url.replace(re, '$1' + key + "=" + value + '$2$3');
                else {
                    return url.replace(re, '$1$3').replace(/(&|\?)$/, '');
                }
            }
            else {
                if (typeof value !== 'undefined' && value !== null) {
                    var separator = url.indexOf('?') !== -1 ? '&' : '?',
                        hash = url.split('#');
                    url = hash[0] + separator + key + '=' + value;
                    if (hash[1]) url += '#' + hash[1];
                    return url;
                }
                else
                    return url;
            }
        },

        urlRoot: function (s) {
            //if (ul.utils.isNullOrEmptyWhiteSpace(s)) {
            //    s = $("meta[name='BaseURL']").attr("content");
            //}
            if (ul.utils.isNullOrEmptyWhiteSpace(s)) {
                s = window.location;
            } else {
                if (s.endsWith('/')) {
                    return s;
                }
                else {
                    return s + '/';
                }
            }
            var loc = s;
            var pathName = loc.pathname.substring(0, loc.pathname.lastIndexOf('/') + 1);
            return loc.href.substring(0, loc.href.length - ((loc.pathname + loc.search + loc.hash).length - pathName.length));
        },

        urlPageAndQuery: function (s) {
            if (ul.utils.isNullOrEmptyWhiteSpace(s)) {
                s = top.location;
            }
            return s.pathname + s.search;
        },

        urlPage: function (s) {
            if (ul.utils.isNullOrEmptyWhiteSpace(s)) {
                s = top.location;
            }
            return s.pathname;
        },

        getParameterByName: function (name) {
            name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                results = regex.exec(location.search);
            return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        },

        rgb2hex: function (rgb) {
            if (rgb.search("rgb") == -1) return "#FFFFFF";
            rgb = rgb.match(/^rgb\((\d+),\s*(\d+),\s*(\d+)\)$/);
            return "#" + this.hex(rgb[1]) + this.hex(rgb[2]) + this.hex(rgb[3]);
        },

        hex: function (x) {
            var hexDigits = new Array("0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f");
            return isNaN(x) ? "00" : hexDigits[(x - x % 16) / 16] + hexDigits[x % 16];
        }
    };

    ul.browser = (function () {
        var self = this;
        function getBrowserVersion() {
            return parseVersion(navigator.userAgent) || parseVersion(navigator.appVersion) || "Unknown";
        }
        function getDeviceName() {

            return parseString(dataDevice) || "Other";
        }
        function parseVersion(dataString) {
            var index = dataString.indexOf(self.versionSearchString);
            if (index == -1) return undefined;
            return parseFloat(dataString.substring(index + self.versionSearchString.length + 1));
        }

        function parseString(data) {
            for (var i = 0; i < data.length; i++) {
                var dataString = data[i].string;
                self.versionSearchString = data[i].subString;
                if (dataString.indexOf(data[i].subString) != -1) {
                    return data[i].identity;
                }
            }
            return null;
        }

        function getName() {
            return parseString(dataBrowser) || "Other";
        }

        var dataBrowser =
        [
            { string: navigator.userAgent, subString: "Chrome", identity: "Chrome" },
            { string: navigator.userAgent, subString: "MSIE", identity: "Explorer" },
            { string: navigator.userAgent, subString: "Trident", identity: "Explorer11" },
            { string: navigator.userAgent, subString: "Firefox", identity: "Firefox" },
            { string: navigator.userAgent, subString: "Safari", identity: "Safari" },
            { string: navigator.userAgent, subString: "Opera", identity: "Opera" }
        ];
        var dataDevice =
        [
            { string: navigator.userAgent, subString: "iPad", identity: "Ipad" },
            { string: navigator.userAgent, subString: "iPod", identity: "Ipod" },
            { string: navigator.userAgent, subString: "iPhone", identity: "Iphone" },
            { string: navigator.userAgent, subString: "Android", identity: "Android" },
            { string: navigator.userAgent, subString: "Windows Phone", identity: "Winmo" },
            { string: navigator.userAgent, subString: "Windows NT", identity: "Windows" }
        ];
        return {
            name: getName(),
            version: getBrowserVersion(),
            device: getDeviceName(),
            setBrowserClass: function () {
                var d = this.device;
                var b = this.name;
                var v = this.version;
                if (b != "" && v != "" && d != "") {
                    v = "v" + v;
                    var body = $("body");
                    if (body.length <= 0) return;
                    if (body.hasClass(b) || body.hasClass(v) || body.hasClass(d)) return;
                    $("body").addClass(b);
                    $("body").addClass(v);
                    $("body").addClass(d);
                }
            },

        };
    }());

}).call(this);