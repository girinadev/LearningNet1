//https://trello.com/en
//https://learn.javascript.ru/drag-and-drop-objects
class TaskBoard {

  static dragTask = {};
  static taskFactory = new AvatarFactory();

  static onMouseDown(e) {
    if (e.which !== 1)
      return false;

    const elem = e.target.closest('.card');
    if (!elem)
      return false;

    TaskBoard.dragTask.elem = elem;
    TaskBoard.dragTask.downX = e.pageX;
    TaskBoard.dragTask.downY = e.pageY;

    return false;
  }

  static onMouseMove(e) {
    if (!TaskBoard.dragTask.elem)
      return false;

    if (!TaskBoard.dragTask.avatar) {
      // если мышь передвинулась в нажатом состоянии недостаточно далеко
      if (Math.abs(e.pageX - TaskBoard.dragTask.downX) < 5 && Math.abs(e.pageY - TaskBoard.dragTask.downY) < 5) 
        return false;
      
      TaskBoard.dragTask.avatar = TaskBoard.taskFactory.createAvatar(TaskBoard.dragTask.elem, e);
      if (!TaskBoard.dragTask.avatar) { // отмена переноса, нельзя "захватить" за эту часть элемента
        TaskBoard.dragTask = {};
        return false;
      }

      // создать вспомогательные свойства shiftX/shiftY
      const rect = TaskBoard.dragTask.avatar.getBoundingClientRect();
      TaskBoard.dragTask.shiftX = TaskBoard.dragTask.downX - (rect.left + pageXOffset);
      TaskBoard.dragTask.shiftY = TaskBoard.dragTask.downY - (rect.top + pageYOffset);

      const avatar = TaskBoard.dragTask.avatar;
      // инициировать начало переноса
      document.body.appendChild(avatar);
      avatar.style.zIndex = 9999;
      avatar.style.position = 'absolute';
    }

    // отобразить перенос объекта при каждом движении мыши
    TaskBoard.dragTask.avatar.style.left = e.pageX - TaskBoard.dragTask.shiftX + 'px';
    TaskBoard.dragTask.avatar.style.top = e.pageY - TaskBoard.dragTask.shiftY + 'px';

    return false;
  }

  static onMouseUp(e) {
    if (TaskBoard.dragTask.avatar) {

      // спрячем переносимый элемент
      TaskBoard.dragTask.avatar.hidden = true;
      // получить самый вложенный элемент под курсором мыши
      const elem = document.elementFromPoint(event.clientX, event.clientY);
      // показать переносимый элемент обратно
      TaskBoard.dragTask.avatar.hidden = false;
      const dropElem = elem === null || elem.closest('.column') === null ? null : elem.closest('.column').querySelector('.tasks');

      if (!dropElem) {
        TaskBoard.dragTask.avatar.rollback();
      } else {
        TaskBoard.dragTask.elem.style = null;
        dropElem.appendChild(TaskBoard.dragTask.elem);
      }
    }

    TaskBoard.dragTask = {};
  }
}

document.onmousemove = TaskBoard.onMouseMove;
document.onmouseup = TaskBoard.onMouseUp;
document.onmousedown = TaskBoard.onMouseDown;