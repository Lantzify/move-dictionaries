angular.module("umbraco").factory("moveDictionariesManager", function ($timeout, editorService, navigationService) {

	return {
		dictionaryMove: dictionaryMove
	};

	function dictionaryMove(options, cb) {
		editorService.open({
			title: "Move dictionary",
			dictionaryId: options.entity.id,
			view: options.action.metaData.actionView,
			size: "small",
			moveRight: true,
			submit: function (done) {
				editorService.close();
				navigationService.hideNavigation();
				if (cb !== undefined) {
					cb(true);
				}
			},
			close: function () {
				editorService.close();
				navigationService.hideNavigation();
				if (cb !== undefined) {
					cb(false);
				}
			}
		});
	}
});