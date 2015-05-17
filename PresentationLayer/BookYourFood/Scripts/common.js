var ViewModel = function (options) {
    return options;
};

/*$(document).ajaxSuccess(function (event, xhr, settings, data) {
    debugger;
    var notificationWidget = $("#notification").data("kendoNotification");
    notificationWidget.show(data.message.content, data.message.type);
    data = data.result;
});*/

$.ajaxSetup({
    dataFilter: function (origdata, type) {
        //the type is determined by the type you indicated in the ajax call
        //if not json, we return the data unharmed
        
        //data filter receives the raw response. since we have determined it's json
        //we parse it using jQuery's parseJSON to check the contents
        if (origdata) {
            var data = $.parseJSON(origdata);
            var notificationWidget = $("#notification").data("kendoNotification");
            if (data.message) {
                notificationWidget.show(data.message.content, data.message.type);
            }
            if (data.messages) {
                data.messages.forEach(function(elem) {
                    notificationWidget.show(elem.content, elem.type);
                });
            }

            if (data.result)
                return JSON.stringify(data.result);
            return JSON.stringify(data);
        }
        return origdata;
    }
});

/*$.postOperationResult = function (path, param, success) {
    if ($.isFunction(param)) {
        $.post(path, function (data) {
            var notificationWidget = $("#notification").data("kendoNotification");
            notificationWidget.show(data.message.content, data.message.type);
            if (success)
                success(data.result);
        });
    } else {
        $.post(path, param, function (data) {
            var notificationWidget = $("#notification").data("kendoNotification");
            notificationWidget.show(data.message.content, data.message.type);
            if (success)
                success(data.result);
        });
    }
};

$.getOperationResult = function (path, param, success) {
    if ($.isFunction(param)) {
        $.get(path, function (data) {
            var notificationWidget = $("#notification").data("kendoNotification");
            notificationWidget.show(data.message.content, data.message.type);
            if (success)
                success(data.result);
        });
    } else {
        $.get(path, param, function (data) {
            var notificationWidget = $("#notification").data("kendoNotification");
            notificationWidget.show(data.message.content, data.message.type);
            if (success)
                success(data.result);
        });
    }
};

$.ajaxOperationResult = function (options) {
    var notificationWidget = $("#notification").data("kendoNotification");
    $.ajax({
        contents: options.contents,
        contentType: options.contentType,
        crossDomain: options.crossDomain,
        data: options.data,
        dataType: options.dataType,
        error: function(data) {
            if (options.error) options.error(data.result);
        },
        headers: options.headers,
        statusCode: options.statusCode,
        success: function (data) {
            notificationWidget.show(data.message.content, data.message.type);
            if(options.success) options.success(data.result);
        },
        url: options.url,
        complete: function (data) {
            if(options.complete) options.complete(data.result);
        }

    });
}*/

function notificationOnShow(e) {
    /*if (!$("." + e.sender._guid)[1]) {
        var element = e.element.parent(),
            eWidth = element.width(),
            wWidth = $(window).width();

        var newLeft = Math.floor(wWidth / 2 - eWidth / 2);
        e.element.parent().css({ left: newLeft });
    }*/
}


$(function () {

});