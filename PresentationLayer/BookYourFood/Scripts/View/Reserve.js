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
        this._model = $.extend(that.modelToBind, { model: this._model });
        this.modelObservable = new kendo.data.ObservableObject(this._model);
        kendo.bind(this._container, this.modelObservable);
    };

    this.modelToBind = {
        chooseTable: function(e) {
            var id = e.currentTarget.id;
            var input = $("input#" + id);
            var tableNumber = $("input#" + id + "number").val();

            that.getModel().set("chosedTableNumber", input.val());
            that.getModel().set("choosedTableText", "You choosed table number: " + tableNumber);
            that.getModel().set("isInvisible", false);
            that.getModel().set("isSubmitEnabled", true);
            //$(".reservation-container a").css("background-color", "white");
            $(".reservation-container a").removeClass("selected");
            //$("#" + id).css("background-color", "#EEEEEE");;
            $("#" + id).addClass("selected");
            input.attr("checked", "checked");
        },
        dateFrom: null,
        /* dateTo: $("#dateTimeTo").kendoDateTimePicker().val(),*/
        howLong: 0,
        defaultColor: "white",
        isInvisible: true,
        isSubmitEnabled: false,
        choosedTableText: null,
        choosedTableNumber: null,
        chosedTable: null,
        checkAvailability: function() {
            $.get("/Reservation/CheckAvailability", { dateTimeFrom: that.getModel().get("dateFrom").toISOString(), howLong: that.getModel().get("howLong") })
                .done(function(result) {
                    $(".reservation-container a").addClass("disabled");
                
                result.tables.forEach(function (value) {
                    //TODO: If table avaiable unblock submit button (block before)
                    $("#table" + value).removeClass("disabled");
                });
            });
            //$.get("/Reservation/CheckAvailability", { dateTimeFrom: that.getModel().get("dateFrom"), dateTimeTo: that.getModel().get("dateTo") }).error(function (a,b,c) { debugger; });
        }
    };
};

var BYF = BYF || {};
BYF.Reserve = new ViewModel();