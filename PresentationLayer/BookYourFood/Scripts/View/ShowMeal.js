/// <reference path="Home.js" />
/// <reference path="../kendo/2014.3.1411/kendo.all-vsdoc.js" />
/// <reference path="../jquery-1.10.2.intellisense.js"/>
var ViewModel = function () {
    var that = this;

    this.init = function (model, container) {
        this._model = model;
        this._container = container;
        this.rebindModel();
    };

    this.getModel = function () {
        return that.modelObservable;
    };

    this.rebindModel = function () {
        kendo.unbind(this._container);
        this._model = $.extend(that.modelToBind, this._model);
        this.modelObservable = new kendo.data.ObservableObject(this._model);
        kendo.bind(this._container, this.modelObservable);
    };

    this.modelToBind = {
        onclick: function () {
            alert("asdas");
        }
    };

    this.selected = null;

    this.mealChange = function() {
        var selectedItem = this.dataItem(this.select());
        that.selected = selectedItem;
        $.get("/Ingredient/GetIngridents", { mealId: selectedItem.Id }, function (ingredients) {
            var multiselect = $("#ingridents").data("kendoMultiSelect");
            multiselect.value([]);
            multiselect.value(ingredients);
        });
    };

    this.multiSelectIngChange = function() {
        if (that.selected) {
            var multiselect = $("#ingridents").data("kendoMultiSelect");
            $.post("/Meal/SetIngridents", { mealId: selected.Id, ingIds: multiselect.value() });
        }
    }
};

var BYF = BYF || {};
BYF.ShowMeal = new ViewModel();