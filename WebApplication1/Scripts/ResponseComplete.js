
$(document).ready(function () {

    var width = $('.inner-container').parent().width();
    $('.inner-container').width(width);
    $("#reportViewer1").width(width);

    if ($('#ShowOQChart').val().toLowerCase() == 'true') {
        $("#reportViewer1").telerik_ReportViewer({
            serviceUrl: $('#ServiceUrl').val(),
            templateUrl: $('#TempateUrl').val(),
            reportSource: {
                report: "SnapReports.OQAreaChart, SnapReports",
                parameters:
                    {
                        IntakeID: $('#IntakeID').val(),
                        ClientID: $('#ClientID').val(),
                    }
            },
            viewMode: 'INTERACTIVE',
            scaleMode: 'FIT_PAGE_WIDTH',
            renderEnd: rescaleBorder(),
            updateUi: function () { expandReport(); } //hideControls(); the whole nav section is now hidden via css
        });
    }
    else {
        $('.inner-container').hide();
    }
});

function rescaleBorder() {
    //fixes bottom border on mobile
    setTimeout(function () {
        $('.trv-pages-area').height($('.trv-pages-area').height() + 1);
    }, 500);
}
function expandReport() {
    //fixes report width being slightly too small
    $('.trv-report-page').width($('.trv-report-page').width() + 16);
}
function hideControls() {
    
    //$('[data-command="telerik_ReportViewer_refresh"]').parent().addClass('k-state-disabled');
    //$('[data-command="telerik_ReportViewer_togglePrintPreview"]').parent().addClass('k-state-disabled');
    //$('[data-command="telerik_ReportViewer_export"]').parent().addClass('k-state-disabled');
    //$('[data-command="telerik_ReportViewer_print"]').parent().addClass('k-state-disabled');
    //$('[data-command="telerik_ReportViewer_zoomIn"]').parent().addClass('k-state-disabled');
    //$('[data-command="telerik_ReportViewer_zoomOut"]').parent().addClass('k-state-disabled');
    //$('[data-command="telerik_ReportViewer_toggleZoomMode"]').parent().addClass('k-state-disabled');
}