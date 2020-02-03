function Binding(b) {
  let self = this;
  this.elementBindings = [];
  this.value = b.bindingModel[b.property];
  this.valueGetter = function () {
    return self.value;
  };
  this.valueSetter = function (val) {
    self.value = val;
    for (let i = 0; i < self.elementBindings.length; i++) {
      let binding = self.elementBindings[i];
      binding.element[binding.attribute] = val;
    }
  };
  this.addBinding = function (element, attribute, event) {
    let binding = {
      element: element,
      attribute: attribute
    };
    if (event) {
      element.addEventListener(event,
        function (event) {
          self.valueSetter(element[attribute]);
        });
      binding.event = event;
    }
    this.elementBindings.push(binding);
    element[attribute] = self.value;
    return self;
  };
  this.addEventBinding = function (element, eventName, eventFunc) {
    if (eventName && eventFunc) {
      element.addEventListener(eventName, eventFunc);
    }
    return self;
  };

  Object.defineProperty(b.bindingModel, b.property, {
    get: this.valueGetter,
    set: this.valueSetter
  });

  b.bindingModel[b.property] = this.value;
}

(function (parameters) {

})();