/// <reference path="Home.js" />
/// <reference path="../kendo/2014.3.1411/kendo.all-vsdoc.js" />
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
        onclick: function() {
            alert("asdas");
        }
    };
};

var BYF = BYF || {};
BYF.Home = new ViewModel();