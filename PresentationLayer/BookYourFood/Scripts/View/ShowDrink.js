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
            alert("aiufaal");
        }
    };

    this.selected = null;

    this.drinkChange = function () {
        var selectedItem = this.dataItem(this.select());
        that.selected = selectedItem;
        if (that.selected && that.selected.Id !== 0) {
            $.get("/HashTag/GetHashTagsForDrink", { drinkId: selectedItem.Id }, function(hashTags) {
                var multiselect = $("#hashtags").data("kendoMultiSelect");
                multiselect.value([]);
                multiselect.value(hashTags);
            });
        }
    };

    this.multiSelectHashChange = function () {
        if (that.selected && that.selected.Id !== 0) {
            var multiselect = $("#hashtags").data("kendoMultiSelect");
            $.post("/Drink/SetHashTags", { drinkId: that.selected.Id, tagsIds: multiselect.value() });
        }
    }
};

var BYF = BYF || {};
BYF.ShowDrink = new ViewModel();