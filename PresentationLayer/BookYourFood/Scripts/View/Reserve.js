/// <reference path="Home.js" />
/// <reference path="../kendo/2014.3.1411/kendo.all-vsdoc.js" />
///<reference path="../jquery-1.10.2.intellisense.js"/>
var ViewModel = function() {
    var that = this;

    this.init = function(model, container) {
        this._model = model;
        this._container = container;
        this.rebindModel();
    };

    this.getModel = function() {
        return that.modelObservable;
    };

    this.rebindModel = function() {
        kendo.unbind(this._container);
        this._model = $.extend(that.modelToBind, this._model);
        this.modelObservable = new kendo.data.ObservableObject(this._model);
        kendo.bind(this._container, this.modelObservable);
    };

    this.modelToBind = {
        chooseTable: function (e) {
            var id = e.currentTarget.id;
            var input = $("input#" + id);
            input.attr("checked", "checked");
        },
        dateFrom: $("#dateTimeFrom").kendoDateTimePicker().val(),
        dateTo: $("#dateTimeTo").kendoDateTimePicker().val()
    };
};

var BYF = BYF || {};
BYF.Reserve = new ViewModel();