// home-index.js
var module = angular.module('homeIndex', ['ngRoute']);

module.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when("/", {
        controller: 'expensesController',
        templateUrl: '/templates/expensesView.html'
    });

    $routeProvider.when('/newexpense', {
        controller: 'newExpenseController',
        templateUrl: '/templates/newExpenseView.html'
    });

    $routeProvider.otherwise({ redirectTo: "/" });
}]);

module.factory('dataService', ['$http', '$q', function ($http, $q) {

    var _expenses = [];
    
    var _getExpenses = function (categoryId) {
        var deferred = $q.defer();
        var uri = '/api/v1/expenses';
        if (categoryId != 0) {
            uri = uri + '?categoryId=' + categoryId;
        }
        $http.get(uri)
            .then(function(result) {
                // success
                angular.copy(result.data, _expenses);
                deferred.resolve();
            },
            function() {
                // error    
                deferred.reject();
            });
        return deferred.promise;
    };

    var _addExpense = function(newExpense) {
        var deferred = $q.defer();
        $http.post('/api/v1/expenses', newExpense)
            .then(function (result) {
                // success    
                var newlyCreatedExpense = result.data;
                _expenses.splice(0, 0, newlyCreatedExpense);
                deferred.resolve(newlyCreatedExpense);
            },
            function () {
                // error
                deferred.reject();
            });
        return deferred.promise;
    };

    var _categories = [];
    var _isInitCategories = false;

    var _isReadyCategories = function () {
        return _isInitCategories;
    };

    var _getCategories = function () {
        var deferred = $q.defer();
        $http.get('/api/v1/categories')
            .then(function (result) {
                // success
                angular.copy(result.data, _categories);
                _isInitCategories = true;
                deferred.resolve();
            },
            function () {
                // error
                deferred.reject();
            });
        return deferred.promise;
    };

    return {
        expenses: _expenses,
        getExpenses: _getExpenses,
        addExpense: _addExpense,
        categories: _categories,
        isReadyCategories: _isReadyCategories,
        getCategories: _getCategories
    };

}]);

var expensesController = ['$scope', '$http', 'dataService',
    function ($scope, $http, dataService) {
        $scope.data = dataService;

        InitCategories(dataService);

        $scope.isBusy = true;
        dataService.getExpenses(0)
            .then(function() {
                // success
            },
            function() {
                // error
                alert('could not load expenses');
            })
            .then(function() {
                $scope.isBusy = false;
            });
        
        $scope.selectedCategoryChange = function () {
            $scope.isBusy = true;
            dataService.getExpenses($scope.selectedCategoryId)
                .then(function () {
                    // success
                },
                function () {
                    // error
                    alert('could not load expenses');
                })
                .then(function () {
                    $scope.isBusy = false;
                });
        };
 }];

var InitCategories = function(dataService) {
    if (dataService.isReadyCategories() == false) {
        dataService.getCategories()
            .then(function() {
                // success
            },
            function() {
                // error
                alert('could not load categories');
            });
    }
};

var newExpenseController = ['$scope', '$http', '$window', 'dataService',
    function ($scope, $http, $window, dataService) {

        $scope.data = dataService;
        $scope.newExpense = {};

        InitCategories(dataService);

        $scope.save = function() {
            dataService.addExpense($scope.newExpense)
                .then(function() {
                    // success
                    $window.location = '#/';
                },
                function() {
                    // error
                    alert('could not save new expense');
                });
        };
}];