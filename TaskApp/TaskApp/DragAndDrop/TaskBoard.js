//https://trello.com/en
//https://learn.javascript.ru/drag-and-drop-objects
class Board {

 dragObject;

constructor() {
    this.dragObject = {
      elem: null,
      avatar: null,
      downX: null,
      downY: null
    };

    document.onmousemove = this.onMouseMove;
    document.onmouseup = this.onMouseUp;
    document.onmousedown = this.onMouseDown;
  }

  onMouseDown(e) {
    if (e.which != 1)
      return false;

    var elem = e.target.closest('.draggable');
    if (!elem)
      return false;

    this.dragObject.elem = elem;
    this.dragObject.downX = e.pageX;
    this.dragObject.downY = e.pageY;

    return false;
  }

  onMouseMove(e) {
    if (!this.dragObject || !this.dragObject.elem)
      return false;

    if (!this.dragObject.avatar) { // если перенос не начат...
      var moveX = e.pageX - this.dragObject.downX;
      var moveY = e.pageY - this.dragObject.downY;

      // если мышь передвинулась в нажатом состоянии недостаточно далеко
      if (Math.abs(moveX) < 3 && Math.abs(moveY) < 3) {
        return false;
      }

      this.dragObject.avatar = createAvatar(e);
      if (!this.dragObject.avatar) { // отмена переноса, нельзя "захватить" за эту часть элемента
        this.dragObject = {};
        return false;
      }

      // создать вспомогательные свойства shiftX/shiftY
      var coords = getCoords(this.dragObject.avatar);
      this.dragObject.shiftX = this.dragObject.downX - coords.left;
      this.dragObject.shiftY = this.dragObject.downY - coords.top;

      startDrag(e);
    }

    // отобразить перенос объекта при каждом движении мыши
    this.dragObject.avatar.style.left = e.pageX - this.dragObject.shiftX + 'px';
    this.dragObject.avatar.style.top = e.pageY - this.dragObject.shiftY + 'px';

    return false;
  }

  onMouseUp(e) {
    if (this.dragObject.avatar)
      this.finishDrag(e);

    this.dragObject = {};
  }

  //

  createAvatar(e) {

    // запомнить старые свойства, чтобы вернуться к ним при отмене переноса
    var avatar = this.dragObject.elem;
    var old = {
      parent: avatar.parentNode,
      nextSibling: avatar.nextSibling,
      position: avatar.position || '',
      left: avatar.left || '',
      top: avatar.top || '',
      zIndex: avatar.zIndex || ''
    };

    // функция для отмены переноса
    avatar.rollback = function () {
      old.parent.insertBefore(avatar, old.nextSibling);
      avatar.style.position = old.position;
      avatar.style.left = old.left;
      avatar.style.top = old.top;
      avatar.style.zIndex = old.zIndex;
    };

    return avatar;
  }

  startDrag(e) {
    var avatar = this.dragObject.avatar;

    // инициировать начало переноса
    document.body.appendChild(avatar);
    avatar.style.zIndex = 9999;
    avatar.style.position = 'absolute';
  }

  finishDrag(e) {
    var dropElem = findDroppable(e);

    if (!dropElem) {
      this.onDragCancel(this.dragObject);
    } else {
      this.onDragEnd(this.dragObject, dropElem);
    }
  }

  findDroppable(event) {
    // спрячем переносимый элемент
    this.dragObject.avatar.hidden = true;

    // получить самый вложенный элемент под курсором мыши
    var elem = document.elementFromPoint(event.clientX, event.clientY);

    // показать переносимый элемент обратно
    this.dragObject.avatar.hidden = false;

    if (elem == null) {
      // такое возможно, если курсор мыши "вылетел" за границу окна
      return null;
    }

    return elem.closest('.column').querySelector('.tasks');
  }

  getCoords(elem) {
    var box = elem.getBoundingClientRect();
    return {
      top: box.top + pageYOffset,
      left: box.left + pageXOffset
    };
  }
}



var board = new Board();

//var DragManager = new function () {

//  /**
//   * составной объект для хранения информации о переносе:
//   * {
//   *   elem - элемент, на котором была зажата мышь
//   *   avatar - аватар
//   *   downX/downY - координаты, на которых был mousedown
//   *   shiftX/shiftY - относительный сдвиг курсора от угла элемента
//   * }
//   */
//  var dragObject = {};

//  var self = this;

//  function onMouseDown(e) {
//    if (e.which != 1) return false;

//    var elem = e.target.closest('.draggable');
//    if (!elem) return false;

//    dragObject.elem = elem;

//    // запомним, что элемент нажат на текущих координатах pageX/pageY
//    dragObject.downX = e.pageX;
//    dragObject.downY = e.pageY;

//    return false;
//  }

//  function onMouseMove(e) {
//    if (!dragObject.elem) return; // элемент не зажат

//    if (!dragObject.avatar) { // если перенос не начат...
//      var moveX = e.pageX - dragObject.downX;
//      var moveY = e.pageY - dragObject.downY;

//      // если мышь передвинулась в нажатом состоянии недостаточно далеко
//      if (Math.abs(moveX) < 3 && Math.abs(moveY) < 3) {
//        return;
//      }

//      // начинаем перенос
//      dragObject.avatar = createAvatar(e); // создать аватар
//      if (!dragObject.avatar) { // отмена переноса, нельзя "захватить" за эту часть элемента
//        dragObject = {};
//        return;
//      }

//      // аватар создан успешно
//      // создать вспомогательные свойства shiftX/shiftY
//      var coords = getCoords(dragObject.avatar);
//      dragObject.shiftX = dragObject.downX - coords.left;
//      dragObject.shiftY = dragObject.downY - coords.top;

//      startDrag(e); // отобразить начало переноса
//    }

//    // отобразить перенос объекта при каждом движении мыши
//    dragObject.avatar.style.left = e.pageX - dragObject.shiftX + 'px';
//    dragObject.avatar.style.top = e.pageY - dragObject.shiftY + 'px';

//    return false;
//  }

//  function onMouseUp(e) {
//    if (dragObject.avatar) { // если перенос идет
//      finishDrag(e);
//    }

//    // перенос либо не начинался, либо завершился
//    // в любом случае очистим "состояние переноса" dragObject
//    dragObject = {};
//  }

//  function createAvatar(e) {

//    // запомнить старые свойства, чтобы вернуться к ним при отмене переноса
//    var avatar = dragObject.elem;
//    var old = {
//      parent: avatar.parentNode,
//      nextSibling: avatar.nextSibling,
//      position: avatar.position || '',
//      left: avatar.left || '',
//      top: avatar.top || '',
//      zIndex: avatar.zIndex || ''
//    };

//    // функция для отмены переноса
//    avatar.rollback = function () {
//      old.parent.insertBefore(avatar, old.nextSibling);
//      avatar.style.position = old.position;
//      avatar.style.left = old.left;
//      avatar.style.top = old.top;
//      avatar.style.zIndex = old.zIndex
//    };

//    return avatar;
//  }

//  function startDrag(e) {
//    var avatar = dragObject.avatar;

//    // инициировать начало переноса
//    document.body.appendChild(avatar);
//    avatar.style.zIndex = 9999;
//    avatar.style.position = 'absolute';
//  }

//  function finishDrag(e) {
//    var dropElem = findDroppable(e);

//    if (!dropElem) {
//      self.onDragCancel(dragObject);
//    } else {
//      self.onDragEnd(dragObject, dropElem);
//    }
//  }

//  function findDroppable(event) {
//    // спрячем переносимый элемент
//    dragObject.avatar.hidden = true;

//    // получить самый вложенный элемент под курсором мыши
//    var elem = document.elementFromPoint(event.clientX, event.clientY);

//    // показать переносимый элемент обратно
//    dragObject.avatar.hidden = false;

//    if (elem == null) {
//      // такое возможно, если курсор мыши "вылетел" за границу окна
//      return null;
//    }

//    return elem.closest('.column').querySelector('.tasks');
//  }

//  document.onmousemove = onMouseMove;
//  document.onmouseup = onMouseUp;
//  document.onmousedown = onMouseDown;

//  this.onDragEnd = function (dragObject, dropElem) {
//    dragObject.elem.style = null;
//    dropElem.appendChild(dragObject.elem);
//  };

//  this.onDragCancel = function (dragObject) {
//    dragObject.avatar.rollback();
//  };
//};


//function getCoords(elem) {
//  var box = elem.getBoundingClientRect();

//  return {
//    top: box.top + pageYOffset,
//    left: box.left + pageXOffset
//  };

//}

