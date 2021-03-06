﻿/// <reference path="Home.js" />
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
        if (selectedItem && selectedItem.Id !== 0) {
            $.get("/Ingredient/GetIngredientsForMeal", { mealId: selectedItem.Id }, function(ingredients) {
                var multiselect = $("#ingridents").data("kendoMultiSelect");
                multiselect.value([]);
                multiselect.value(ingredients);
            });
            $.get("/HashTag/GetHashTagsForMeal", { mealId: selectedItem.Id }, function(hashTags) {
                var multiselect = $("#hashtags").data("kendoMultiSelect");
                multiselect.value([]);
                multiselect.value(hashTags);
            });
        }
    };

    this.multiSelectIngChange = function() {
        if (that.selected && that.selected.Id !== 0) {
            var multiselect = $("#ingridents").data("kendoMultiSelect");
            $.post("/Meal/SetIngridents", { mealId: that.selected.Id, ingIds: multiselect.value() });
        }
    }

    this.multiSelectHashChange = function () {
        if (that.selected && that.selected.Id !== 0) {
            var multiselect = $("#hashtags").data("kendoMultiSelect");
            $.post("/Meal/SetHashTags", { mealId: that.selected.Id, ingIds: multiselect.value() });
        }
    }

    this.saveImage = function(e) {
        var id = that.selected.Id;
        e.data = { mealId: id };
    }

    this.refreshGrid = function () {
        var grid = $("#meals").data("kendoGrid");
        grid.dataSource.read();
    }
};

var BYF = BYF || {};
BYF.ShowMeal = new ViewModel();