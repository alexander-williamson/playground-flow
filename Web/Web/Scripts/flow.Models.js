var FlowTemplateModel = function (pubsub, data, steps) {
    var self = this;
    self.Id = ko.observable(data ? data.Id : null);
    self.Name = ko.observable(data ? data.Name : null);
    self.Steps = ko.observableArray(steps);
    self.Delete = function() {
        pubsub.publish("flowtemplate.delete", self);
    };
    return self;
};

var FlowTemplateStepModel = function() {
    this.Id = ko.observable(0);
    this.Name = ko.observable("");
    this.type = ko.observable(1);
};

var FlowTemplateIndexModel = function() {
    this.Templates = ko.observableArray();
};