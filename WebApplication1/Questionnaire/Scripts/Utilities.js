(function () {
    var root = this;
    var prevUl = root.Utilities;
    var Utilities = function (obj) {
        if (obj instanceof Utilities) return obj;
        if (!(this instanceof Utilities)) return new Utilities(obj);
        this._wrapped = obj;
    };
    if (typeof exports !== 'undefined') {
        if (typeof module !== 'undefined' && module.exports) {
            exports = module.exports = Utilities;
        }
        exports.Utilities = Utilities;
    } else {
        root.Utilities = Utilities;
    }

    Utilities.setup = {
        fixPlaceholderNumberInputs: function () {
            
            if ($('body.Safari').length > 0) {
                //fix for browsers that cant dipslay placeholder text in 'number' type inputs
                $("input[type='number']").each(function(i, el) {
                    el.type = "text";
                    el.onfocus = function() { this.type = "number"; };
                    el.onblur = function() { this.type = "text"; };
                });
            }
        },

        setupPageDropDownStyling: function (wrapper_Element_Id) {
            $.each($(wrapper_Element_Id).find("select > option[value='']"), function (i, e) {
                $(e).attr('data-disable', true);
            });
            $.each($(wrapper_Element_Id).find('select'), function (i, val) {
                var $el = $(val);

                if (!$el.val()) {
                    $el.addClass("not_chosen");
                }
                $el.on("change", function () {
                    if (!$el.val())
                        $el.addClass("not_chosen");
                    else
                        $el.removeClass("not_chosen");
                });
            });

        },

    };

    Utilities.validation = {
        
        ValidateForm: function ($form, rules, messages, validateHidden) {
            if (ul.utils.isNullOrEmpty($form) || $form.length == 0)
                return;
            
            var validator = $form.validate({
                rules: rules,
                messages: messages,
                errorPlacement: Utilities.validation._ErrorPlacement,
                errorClass: Utilities.validation._ErrorClass,
                errorElement: Utilities.validation._ErrorElement
            });

            if (validateHidden) {
                validator.settings.ignore = [];
            }

            return validator;
        },

        _ErrorPlacement: function (error, inputElement) {
            var container = $(inputElement[0].form).find("[data-valmsg-for='" + inputElement[0].name + "']"),
            replaceAttrValue = container.attr("data-valmsg-replace"),
            replace = replaceAttrValue ? $.parseJSON(replaceAttrValue) !== false : null;

            container.removeClass("field-validation-valid").addClass("field-validation-error");
            error.data("unobtrusiveContainer", container);

            if (replace) {
                container.empty();
                error.removeClass("input-validation-error").appendTo(container);
            }
            else {
                error.hide();
            }
            
        },

        _ErrorClass: 'error',
        _ErrorElement:'span',
        
    };

    Utilities.utils = {
        baseUrl: function () {
            return $("meta[name='BaseURL']").prop('content');
        },
    };

    $(function () {

    });

}).call(this);
