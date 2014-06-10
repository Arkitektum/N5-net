var n5App = angular.module('noarkApp', ['ngRoute', 'n5Controllers']);
 
n5App.config(function ($routeProvider, $locationProvider) {
    $locationProvider.html5Mode(false);

    $routeProvider
    .when('/link/:href*', {
        templateUrl: '/Scripts/templates/linkliste.html',
        controller: 'LinkController'
    })
    .when('/mappe/:href*', {
        templateUrl: '/Scripts/templates/arkivstruktur.html',
        controller: 'MappeController'
    })
    .when('/arkiv/:href*', {
        templateUrl: '/Scripts/templates/arkiv.html',
        controller: 'LinkController'
    })
    .when('/filter/:href*', {
        templateUrl: '/Scripts/templates/filter.html',
        controller: 'FilterController'
    })
    .otherwise({
        templateUrl: '/Scripts/templates/main.html',
        controller: 'RootController'
    });
   
});

var n5Controllers = angular.module('n5Controllers', []);

n5Controllers.controller('MainController', function ($scope, $route, $routeParams, $location) {
    $scope.$route = $route;
    $scope.$location = $location;
    $scope.$routeParams = $routeParams;
});

n5Controllers.controller('MappeController', function ($scope, $routeParams, $http, $sce) {
    $scope.name = "MappeController";
    $scope.params = $routeParams;
    $scope.data_url = $sce.trustAsResourceUrl($routeParams.href);
    $http.get($scope.data_url).success(function (data) {
        $scope.data = data;
    });
});

n5Controllers.controller('RootController', function ($scope, $routeParams, $http) {
    $scope.name = "RootController";
    $scope.params = $routeParams;
    var backend_url = "http://localhost:49708/api";
    $http.get(backend_url).success(function (data) {
        $scope.links = data;
    });
});

n5Controllers.controller('FilterController', function ($scope, $routeParams, $http, $sce) {
    $scope.name = "FilterController";
    $scope.params = $routeParams;
    //$scope.data_url = $sce.trustAsResourceUrl($routeParams.href);
    //Fjerne template parametre
    //$scope.data_url = "http://localhost:49708/api";
    //$http.get($scope.data_url).success(function (data) {
    //    $scope.data = data;
    //});
    $scope.search = function (data) {
        alert("søk");
    };
});

n5Controllers.controller('LinkController', function ($scope, $routeParams, $http, $sce) {
    $scope.name = "LinkController";
    $scope.params = $routeParams;
    $scope.data_url = $sce.trustAsResourceUrl($routeParams.href);
    $http.get($scope.data_url).success(function (data) {
        $scope.data= data;
    });
    $scope.save = function (data) {
        $http.post($scope.data_url).success(function (data) {
            $scope.data = data;
        });
        alert("lagret");
    };
});




