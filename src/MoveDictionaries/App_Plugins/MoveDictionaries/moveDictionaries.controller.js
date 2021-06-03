angular.module("umbraco").controller("moveDictionaries.controller", function ($scope, $http, $routeParams, dictionaryResource, navigationService, notificationsService) {

	var vm = this;

	vm.dictionaries = [];
	vm.dictionary = {};
	vm.rootKey = "00000000-0000-0000-0000-000000000000";
	vm.selectedKey = "";

	//Get clicked dictionary
	dictionaryResource.getById($scope.model.dictionaryId).then(function (response) {
		vm.dictionary = response;
		console.log(response)
	});

	dictionaryResource.getList().then(function (response) {
		//getList dosen't return dictionary key.
		response.forEach(function (dictionary) {
			dictionaryResource.getById(dictionary.id).then(function (additionalInfoDictionary) {
				vm.dictionaries.push(additionalInfoDictionary);
				console.log(additionalInfoDictionary)
			});
		});
	});

	vm.toggleSelect = function (dictionaryKey) {

		if (vm.selectedKey === dictionaryKey) {
			vm.selectedKey = "";
		} else {
			vm.selectedKey = dictionaryKey;
		}
	}

	vm.submit = function () {

		
		vm.buttonState = "busy";

		if ($scope.model.submit) {
			$http({
				url: "/umbraco/backoffice/api/MoveDictionariesApi/MoveDictionary",
				method: "POST",
				data: {
					Id: vm.dictionary.id,
					ParentKey: vm.selectedKey
				}
			}).then(function (response) {
				if (response.data) {
					navigationService.syncTree({ "tree": "dictionary", "path": [-1, $routeParams.id], "forceReload": true });
					notificationsService.success("Dictionary", "Successfully saved dictionary item!");
				} else {
					notificationsService.error("Dictionary", "Failed to save dictionary item");
				}

				$scope.model.submit($scope.model);
			});
		}
	};

	vm.close = function () {
		if ($scope.model.close) {
			$scope.model.close();
		}
	};
});