var FlowTemplateModel = function() {
    this.Id = ko.observable(0);
    this.Name = ko.observable("");
    this.Steps = ko.observableArray();
};

var FlowTemplateStepModel = function() {
    this.Id = ko.observable(0);
    this.Name = ko.observable("");
    this.type = ko.observable(1);
};

var FlowTemplateIndexModel = function () {
    var self = this;
    self.Templates = ko.observableArray();
    self.Delete = function (template) {
        $.ajax({
            url: "/api/FlowTemplates/" + template.Id,
            type: "DELETE"
        }).success(function() {
            self.Templates.remove(template);
        }).error(function() {
            console.log("Unable to delete template " + template.Id);
        });
    };
    self.Add = function(template) {
        var data = ko.toJSON(template);
        $.ajax({
            url: "/api/FlowTemplates/",
            data: data, 
            type: "POST",
            contentType: "application/json",
        }).success(function (e) {
            template.Id = e.Id;
            self.Templates.push(template);
        }).error(function(err) {
            console.log("Unable to add template " + err);
        });
    };
    self.Update = function(template) {
        var data = ko.toJSON(template);
        $.ajax({
            url: "/api/FlowTemplates/" + template.Id, 
            data: data,
            type: "PUT",
            contentType: "application/json"
        }).success(function () {

            for (var i = 0; i < self.Templates.length; i++) {
                if (self.Templates[i].Id == template.Id) {
                    self.Templates[i] = template;
                }
            }

        }).error(function () {
            console.log("Unable to update template " + template.Id);
        });
    };


    // init
    console.log("Fetching FlowTemplates for the first time");
    $.ajax("/api/FlowTemplates").success(function(data) {
        data.forEach(function(e) {
            self.Templates.push(e);
        });
    }).error(function() {
        console.log("Unable to get FlowTemplates");
    });
};