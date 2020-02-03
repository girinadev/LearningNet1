class Binder {
  static bindingContexts = {};

  static init(viewModel) {
    var dataBindElements = document.querySelectorAll('[data-bind]');

    for (const element of dataBindElements) {

      const bindAttr = element.getAttribute('data-bind');
      if (!bindAttr)
        continue;

      const bindParams = bindAttr.split(':');
      if (bindParams.length < 2)
        continue;

      const viewModelProp = bindParams[1].trim();
      if (!viewModel.hasOwnProperty(viewModelProp))
        continue;

      let bindingContext;
      if (Binder.bindingContexts[viewModelProp]) {
        bindingContext = Binder.bindingContexts[viewModelProp];
      } else {
        bindingContext = new Binding({
          bindingModel: viewModel,
          property: viewModelProp
        });
        Binder.bindingContexts[viewModelProp] = bindingContext;
      }

      const elementEvent = mapBindingEvent(element, bindParams[0].trim());
      if (elementEvent) {
        bindingContext.addEventBinding(element, elementEvent, viewModelProp);
        continue;
      }

      //if () { //select
      //  //todo:
      //  continue;
      //}

      const elementAttribute = mapBindingAttribute(element, bindParams[0].trim());
      bindingContext.addBinding(element, elementAttribute, 'keyup');
    }

    function mapBindingAttribute(element, attribute) {
      const supportTexts = ['input', 'textarea'];
      if (supportTexts.includes(element.tagName.toLowerCase())) {
        if (attribute === 'text') return 'value';
      }
      return 'innerHTML';
    }

    function mapBindingEvent(element, attribute) {
      const supportEvents = ["keyup", "click"];
      if (supportEvents.includes(attribute))
        return attribute;
      return undefined;
    }
  }
}

function Binding(b) {
  let self = this;
  this.elementBindings = [];
  this.bindingModel = b.bindingModel;

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
      element.addEventListener(event, function (event) {
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
      element.addEventListener(eventName, function (event) {
        self.value.call(self.bindingModel, event);
      });
    }
    return self;
  };

  Object.defineProperty(b.bindingModel, b.property, {
    get: this.valueGetter,
    set: this.valueSetter
  });

  b.bindingModel[b.property] = this.value;
}