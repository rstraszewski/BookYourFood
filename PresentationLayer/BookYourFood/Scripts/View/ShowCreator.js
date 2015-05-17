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

            $.get("/Ingredient/GetPriceForIngredients", JSON.stringify({ ingredients: ingredients }), function (result) {
                alert(result.price);
                that.getModel().set("isVisible", true);
                that.getModel().set("numberOfCustom", that.getModel().get("numberOfCustom") + 1);
                var list = $("#customMeals .list-group");
                var item = '<li class="list-group-item">';
                item += '<h3>Ingredients: ' + ingredients.toString() + '</h3>';
                item += '<h4>' + that.getModel().get("customMealComment");
                item += "</h4>";
                item += '<input hidden value="' + that.getModel().get("numberOfCustom") + '" name="test"/>';
                item += 'Price: testPrice ';
                item += '<input id="customMeal' + that.getModel().get("numberOfCustom") + '" type="number" name="test" value="0" min="0" max="10" step="1" />';
                item += '</li>';
                list.append(item);
            });

            $("#customMeal" + that.getModel().get("numberOfCustom")).kendoNumericTextBox();

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