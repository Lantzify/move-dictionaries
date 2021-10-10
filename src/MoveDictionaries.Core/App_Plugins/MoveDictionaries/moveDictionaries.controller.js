angular.module("umbraco").controller("moveDictionaries.controller", function ($scope, $http, $routeParams, dictionaryResource, navigationService, notificationsService) {

	var vm = this;
	var dictionaries = [];

	vm.dictionary = {};
	vm.rootKey = "00000000-0000-0000-0000-000000000000";
	vm.selectedKey = "";

	$scope.moveDictionariesTreeApi = {};

	//Get clicked dictionary
	dictionaryResource.getById($scope.model.dictionaryId).then(function (response) {
		vm.dictionary = response;
		console.log(response)
	});

	dictionaryResource.getList().then(function (response) {
		//getList dosen't return dictionary key.
		response.forEach(function (dictionary) {
			dictionaryResource.getById(dictionary.id).then(function (additionalInfoDictionary) {
				dictionaries.push(additionalInfoDictionary);
				
			});
		});
	});

	var nodeSelectHandler = function (args) {

		if (args && args.event) {
			args.event.preventDefault();
			args.event.stopPropagation();
		}

		if ($scope.target) {
			$scope.target.selected = false;
		}

		$scope.target = args.node;
		$scope.target.selected = true;

		if ($scope.target.id === "-1") {
			vm.selectedKey = vm.rootKey;
		} else {
			dictionaries.forEach(function (child) {
				if ($scope.target.id == child.id) {
					vm.selectedKey = child.key;

					return;
				}
			});
		}
	}

	var nodeExpandedHandler = function (args) {
		args.children.forEach(function (child, index) {
			if (child.name === vm.dictionary.name) {
				args.children.splice(index, 1);
			}
		});
	}

	$scope.onTreeInit = function () {
		$scope.moveDictionariesTreeApi.callbacks.treeNodeSelect(nodeSelectHandler);
		$scope.moveDictionariesTreeApi.callbacks.treeNodeExpanded(nodeExpandedHandler);
	};

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
					var path = [];

					if ($routeParams.method === "list") {
						path.push("list");
					} else {
						path.push("edit", $routeParams.id);
					}
					navigationService.syncTree({ "tree": "dictionary", "path": path, "forceReload": true });
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