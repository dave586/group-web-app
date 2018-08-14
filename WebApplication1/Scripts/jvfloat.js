/*
 * JVFloat.js
 * modified on: 29/01/2014
 */

(function ($) {
    "use strict";

    // Init Plugin Functions
    $.fn.jvFloat = function () {
        // Check input type - filter submit buttons.
        return this.filter('input:not([type=submit]), textarea, select').each(function () {
            // Wrap the input in div.jvFloat

            var $el = $(this)
                .wrap('<div class=jvFloat>');

            var forId = $el.attr('id');
            if (!forId)
                forId = createIdOnElement($el);

            // Store the placeholder text in span.placeHolder
            // added `required` input detection and state
            var required = $el.attr('required') || '';
            var placeholder = $('<label class="placeHolder ' + required + '" for="' + forId + '">' + $el.attr('placeholder') + '</label>')
              .insertBefore($el);
            // checks to see if inputs are pre-populated and adds active to span.placeholder
            setState();

            if (($el[0].nodeName.toLowerCase() === 'select') || $el.hasClass('date-picker')) {
                $el.bind('keyup blur change', setState);
            }
            else {
                $el.bind('keyup blur', setState);
            }

            function setState() {
                // change span.placeHolder to span.placeHolder.active
                placeholder.toggleClass('active', $el.val() !== '');
            }
            function generateUIDNotMoreThan1million() {
                do {
                    var id = ("0000" + (Math.random() * Math.pow(36, 4) << 0).toString(36)).substr(-4);
                } while (!!$('#' + id).length);

                return id;
            };
            function createIdOnElement($el) {
                var id = generateUIDNotMoreThan1million();

                $el.prop('id', id);

                return id;
            };
        });
    };

    // Make Zeptojs & jQuery Compatible
})(window.jQuery || window.Zepto || window.$);
