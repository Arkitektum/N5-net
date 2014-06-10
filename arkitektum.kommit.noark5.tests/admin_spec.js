/// <reference path="Scripts/jasmine.js"/>
/// <reference path="lib/superagent/superagent.js"/>

//var request = require('lib/superagent');
var rootApi = "http://n5test.kxml.no/api";

describe("administrasjon", function () {

    it("sjekke om støtter administrasjon og arkivstruktur", function () {
        
        var doneFn = jasmine.createSpy("success");
        var xhr = new XMLHttpRequest();
        xhr.onreadystatechange = function (arguments) {
            if (this.readyState == this.DONE) {
                doneFn(this.responseText);
                console.log(this.responseText);
            }
        };
        xhr.open("GET", rootApi, false);
        xhr.send();
        expect(doneFn).toHaveBeenCalled();
        //expect(doneFn).toEqual(jasmine.objectContaining({
        //    "rel": "http://rel.kxml.no/noark5/v4/Arkivstruktur"
        //}));
 
    });
   

    it("registrere arkiv", function () {
        var doneFn = jasmine.createSpy("success_get");
        var doneFn2 = jasmine.createSpy("success_post");
        var xhr = new XMLHttpRequest();
        xhr.onreadystatechange = function (arguments) {
            if (this.readyState == this.DONE) {
                doneFn(this.responseText);
                console.log(this.responseText);
            }
        };
        xhr.open("GET", rootApi + "/arkivstruktur/nytt-arkiv", false);
        xhr.send();
        expect(doneFn).toHaveBeenCalled();
        var xhr2 = new XMLHttpRequest();
        xhr2.onreadystatechange = function (arguments) {
            if (this.readyState == this.DONE) {
                doneFn2(this.responseText);
                console.log(this.responseText);
            }
        };
        xhr2.open("POST", rootApi + "/arkivstruktur/nytt-arkiv", false)
        xhr2.send(doneFn);
        expect(doneFn2).toHaveBeenCalled();
        expect(xhr2.status).toBe(201);
    });

    it("registrere arkivdel", function () {
        expect(true).toBe(true);
    });
});