/// <reference path="Home.js" />
/// <reference path="../kendo/2015.1.318/kendo.all-vsdoc.js" />
/// <reference path="../jquery-1.10.2.intellisense.js" />
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
        createMeal: function () {
            

            var multiselect = $("#ingridents").data("kendoMultiSelect");
            var ingredients = multiselect.value();

            $.post("/Ingredient/GetInformationForIngredients", { ingredients: ingredients }, function (result) {
                that.getModel().set("isVisible", true);

                var number = that.getModel().get("numberOfCustom");
                var list = $("#customMeals .list-group");
                var name = "Ingredients: " + result.Names;
                var description = that.getModel().get("customMealComment");
                var customMeal = "customMeals[" + number + "]"; 

                var item = '<li class="list-group-item">';

                item += '<h3>' + name + '</h3>';
                item += '<h4>' + description;
                item += "</h4>";
                item += '<input hidden value="' + name + '" name="' + customMeal + '.Name"/>';
                item += '<input hidden value="' + description + '" name="' + customMeal + '.Description"/>';
                item += '<input hidden value="' + result.Price + '" name="' + customMeal + '.Price"/>';
                item += 'Price: $' + result.Price.toFixed(2);
                item += '<input id="customMeal' + number + '" type="number" name="' + customMeal + '.Count" value="0" min="0" max="10" step="1" />';
                item += '</li>';
                list.append(item);
                $("#customMeal" + number).kendoNumericTextBox();

                that.getModel().set("numberOfCustom", number + 1);
            });

            

        },
        customMealComment: "",
        isVisible: false,
        numberOfCustom: 0
    };

    this.mealChange = function() {
        var mealDropDownList = $("#mealToEdit").data("kendoDropDownList");

        $.get("/Ingredient/GetIngredientsForMeal", { mealId: mealDropDownList.value() }, function (ingredients) {
            var multiselect = $("#ingridents").data("kendoMultiSelect");
            multiselect.value(ingredients);
        });
    };
};

var BYF = BYF || {};
BYF.ShowCreator = new ViewModel();