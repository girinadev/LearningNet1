class Binder {
  static bindingContexts = {};

  static applyBindings(viewModel) {
    var dataBindElements = document.querySelectorAll('[data-bind]');

    for (const element of dataBindElements) {
      const attrDict = getMappingDict(element);
      if (attrDict.length === 0)
        continue;

      const viewModelPropKey = Object.keys(attrDict)[0];
      const viewModelPropValue = attrDict[viewModelPropKey];
      if (!viewModel.hasOwnProperty(viewModelPropValue))
        continue;

      let bindingContext;
      if (Binder.bindingContexts[viewModelPropValue]) {
        bindingContext = Binder.bindingContexts[viewModelPropValue];
      } else {
        bindingContext = new Binding({
          bindingModel: viewModel,
          property: viewModelPropValue
        });
        Binder.bindingContexts[viewModelPropValue] = bindingContext;
      }

      const elementSelect = mapBindingSelect(element, attrDict, viewModel);
      if (elementSelect) {
        bindingContext.addBinding(element, 'value', 'change');
        continue;
      }

      const elementEvent = mapBindingEvent(element, viewModelPropKey);
      if (elementEvent) {
        bindingContext.addEventBinding(element, elementEvent, viewModelPropValue);
        continue;
      }

      const elementAttribute = mapBindingAttribute(element, viewModelPropKey);
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
      const supportEvents = ["keyup", "click", "change"];
      if (supportEvents.includes(attribute))
        return attribute;
      return undefined;
    }

    function mapBindingSelect(element, attrDict, viewModel) {
      if (!attrDict['options'])
        return undefined;

      const optionsSource = viewModel[attrDict['options']];
      for (var optionSource of optionsSource) {
        let optionElement = document.createElement('option');

        if (attrDict['optionsText']) {
          optionElement.innerText = optionSource[attrDict['optionsText']];
        } else {
          optionElement.innerText = optionSource;
        }

        if (attrDict['text']) {
          optionElement.value = optionSource[attrDict['text']];
        } else {
          optionElement.value = optionSource;
        }

        element.appendChild(optionElement);
      }

      return element;
    }

    function getMappingDict(element) {
      const attributes = element.getAttribute('data-bind');

      let attrDict = {};

      attributes.split(',').forEach(m => {
        const [k, v] = m.split(':');
        attrDict[k.trim()] = v.trim();
      });

      return attrDict;
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
  }
}

Date.prototype.addDays = function (days) {
  const date = new Date(this.valueOf());
  date.setDate(date.getDate() + days);
  return date;
};

Date.prototype.toStringWithFormat = function () {
  const date = new Date(this.valueOf());
  return ((date.getMonth() > 8) ? (date.getMonth() + 1) : ('0' + (date.getMonth() + 1))) +
    '/' +
    ((date.getDate() > 9) ? date.getDate() : ('0' + date.getDate())) +
    '/' +
    date.getFullYear();
};

String.prototype.isValidDate = function () {
  const dateString = this.valueOf();
  
  if (!/^\d{1,2}\/\d{1,2}\/\d{4}$/.test(dateString))
    return false;
  
  var parts = dateString.split("/");
  var day = parseInt(parts[1], 10);
  var month = parseInt(parts[0], 10);
  var year = parseInt(parts[2], 10);
  
  if (year < 1000 || year > 3000 || month === 0 || month > 12)
    return false;

  var monthLength = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];  
  if (year % 400 === 0 || (year % 100 !== 0 && year % 4 === 0))
    monthLength[1] = 29;
  
  return day > 0 && day <= monthLength[month - 1];
};

String.prototype.parseDateWithFormat = function () {
  const mdy = this.valueOf().split('/');
  return new Date(mdy[2], mdy[0] - 1, mdy[1]);
};