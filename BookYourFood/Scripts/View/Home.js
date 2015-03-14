var BYF = BYF || {};
var ViewModel = function (options) {
	return options;
}

$(function () {
	BYF.Home = new ViewModel({
		init: function (model, container) {
			this._model = model;
			this._container = container;
			this.rebindModel();
		},

		rebindModel: function() {
			kendo.unbind(this._container);

			var modelToBind = {
				onclick: function() {
				    $.post("/Home/Create");
				},
                tomek: "Tomasz Grochmal"
			};

			this._model = $.extend(modelToBind, this._model);
			this.modelObservable = new kendo.data.ObservableObject(this._model);
			kendo.bind(this._container, this.modelObservable);
		},

		modelObservable: {}
	});
});

