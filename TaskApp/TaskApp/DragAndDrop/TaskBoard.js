﻿class TaskBoard {

  static dragCard = {};
  static elementsFactory = new ElementFactory();
  static taskService = new TaskService();

  static init() {

    document.querySelectorAll('.add-card')
      .forEach((item) => { item.onclick = TaskBoard.newCard; });

    TaskBoard.taskService.getTasks()
      .forEach((t) => { TaskBoard.addCard(t); });

    document.onmousemove = TaskBoard.mouseMove;
    document.onmouseup = TaskBoard.mouseUp;
    document.onmousedown = TaskBoard.mouseDown;
    document.onkeydown = (e) => { if (e.keyCode === 27) TaskBoard.cancelCardEditor(); };
  }

  static addCard(c) {
    const columnElem = document.querySelector('[data-column-status="' + c.status + '"] .cards');
    if (columnElem) {
      const cardInnerElem = document.createElement('span');
      cardInnerElem.innerText = c.title;
      cardInnerElem.setAttribute('class', 'card-text');

      //var editCardElem = document.createElement('a');
      //editCardElem.innerHTML = '<i class="fa fa-pencil"></i>';
      //editCardElem.setAttribute('class', 'edit-card');
      //editCardElem.onclick = TaskBoard.editCard;

      var cardElem = document.createElement('div');
      cardElem.appendChild(cardInnerElem);
      //cardElem.appendChild(editCardElem);
      cardElem.setAttribute('class', 'card');
      cardElem.setAttribute('data-status', c.status);
      cardElem.setAttribute('data-id', c.id);

      columnElem.appendChild(cardElem);
    }
  }

  static editCard(e) {
    const cardElem = e.target.closest('.card');
    if (!cardElem) return;

    const task = TaskBoard.taskService.getTask(cardElem.getAttribute('data-id'));
    if (!task) return;

    TaskBoard.cancelCardEditor();

    const cardInputElem = document.createElement('input');
    cardInputElem.setAttribute('type', 'text');
    cardInputElem.setAttribute('class', 'card-text');
    cardInputElem.setAttribute('placeholder', 'Enter a title for this card…');
    cardInputElem.value = task.title;
    cardInputElem.onkeydown = (e) => {
      if (e.keyCode === 13) {
        TaskBoard.cancelCardEditor();
        cardElem.style.display = null;
        cardElem.querySelector('.card-text').innerText = cardInputElem.value;

        task.title = cardInputElem.value;
        TaskBoard.taskService.editTask(task);
      }
    };

    let cardEditorElem = document.createElement('div');
    cardEditorElem.appendChild(cardInputElem);
    cardEditorElem.setAttribute('class', 'card-editor');

    cardElem.parentNode.insertBefore(cardEditorElem, cardElem.nextSibling);

    cardInputElem.focus();

    cardElem.style.display = 'none';
  }

  static newCard(e) {
    const columnElem = e.target.closest('.column');
    if (!columnElem) return;

    TaskBoard.cancelCardEditor();

    const cardInputElem = document.createElement('input');
    cardInputElem.setAttribute('type', 'text');
    cardInputElem.setAttribute('class', 'card-text');
    cardInputElem.setAttribute('placeholder', 'Enter a title for this card…');
    cardInputElem.onkeydown = (e) => {
      if (e.keyCode === 13) {
        const task = { title: cardInputElem.value, status: columnElem.getAttribute('data-column-status') };
        TaskBoard.addCard(task);
        TaskBoard.cancelCardEditor();
        TaskBoard.taskService.addTask(task);
      }
    };

    let cardEditorElem = document.createElement('div');
    cardEditorElem.appendChild(cardInputElem);
    cardEditorElem.setAttribute('class', 'card-editor');

    columnElem.appendChild(cardEditorElem);

    cardInputElem.focus();

    columnElem.querySelector('.add-card').style.display = 'none';
  }

  static cancelCardEditor(e) {
    let cardEditorElem = document.querySelector('.card-editor');
    if (cardEditorElem) {
      cardEditorElem.parentNode.removeChild(cardEditorElem);

      document.querySelectorAll('.add-card')
        .forEach(i => { i.style.display = null; });

      document.querySelectorAll('.card')
        .forEach(i => { i.style.display = null; });
    }
  }

  static mouseDown(e) {
    if (e.which !== 1)
      return false;

    const elem = e.target.closest('.card');
    if (!elem)
      return false;

    TaskBoard.dragCard.elem = elem;
    TaskBoard.dragCard.downX = e.pageX;
    TaskBoard.dragCard.downY = e.pageY;

    return false;
  }

  static mouseMove(e) {
    if (!TaskBoard.dragCard.elem)
      return false;

    if (!TaskBoard.dragCard.avatar) {
      if (Math.abs(e.pageX - TaskBoard.dragCard.downX) < 5 && Math.abs(e.pageY - TaskBoard.dragCard.downY) < 5)
        return false;

      TaskBoard.dragCard.avatar = TaskBoard.elementsFactory.createAvatar(TaskBoard.dragCard.elem, e);
      TaskBoard.dragCard.shadow = TaskBoard.elementsFactory.createShadow(TaskBoard.dragCard.elem, e);
      if (!TaskBoard.dragCard.avatar || !TaskBoard.dragCard.shadow) {
        TaskBoard.dragCard = {};
        return false;
      }

      const rect = TaskBoard.dragCard.avatar.getBoundingClientRect();
      TaskBoard.dragCard.shiftX = TaskBoard.dragCard.downX - (rect.left + pageXOffset);
      TaskBoard.dragCard.shiftY = TaskBoard.dragCard.downY - (rect.top + pageYOffset);

      const avatar = TaskBoard.dragCard.avatar;
      document.body.appendChild(avatar);
      avatar.style.zIndex = 9999;
      avatar.style.position = 'absolute';
    }

    TaskBoard.dragCard.avatar.style.left = e.pageX - TaskBoard.dragCard.shiftX + 'px';
    TaskBoard.dragCard.avatar.style.top = e.pageY - TaskBoard.dragCard.shiftY + 'px';

    TaskBoard.dragCard.avatar.hidden = true;
    const elem = document.elementFromPoint(event.clientX, event.clientY);
    TaskBoard.dragCard.avatar.hidden = !TaskBoard.dragCard.avatar.hidden;


    if (elem && elem.closest('.column')) {
      const isKeepCurrentCardShadow = elem.classList.contains('cards');

      if (!isKeepCurrentCardShadow) {

        const isKeepCurrentCardShadow = elem.classList.contains('cards');
        if (!isKeepCurrentCardShadow) {
          if (elem.closest('.card')) {
            elem.closest('.card').insertAdjacentElement("afterend", TaskBoard.dragCard.shadow);
          }
          else if (elem.closest('.column').querySelector('.cards').childElementCount === 0) {
            elem.closest('.column').querySelector('.cards').insertAdjacentElement("beforeend", TaskBoard.dragCard.shadow);
          }
        }
      }
      else {
        const rect1 = TaskBoard.dragCard.avatar.getBoundingClientRect();
        const elem2 = document.elementFromPoint(event.clientX, rect1.top - 15);
        if (elem2.closest('.card'))
          elem2.closest('.card').insertAdjacentElement("afterend", TaskBoard.dragCard.shadow);
      }
    }

    return false;
  }

  static mouseUp(e) {
    if (TaskBoard.dragCard.avatar) {
      TaskBoard.dragCard.avatar.hidden = true;
      const elem = document.elementFromPoint(event.clientX, event.clientY);
      TaskBoard.dragCard.avatar.hidden = !TaskBoard.dragCard.avatar.hidden;

      const cardsElem = elem === null || elem.closest('.column') === null
        ? null
        : elem.closest('.column').querySelector('.cards');

      if (!cardsElem) {
        TaskBoard.dragCard.avatar.cancel();
        TaskBoard.dragCard.shadow.cancel();
      } else {

        if (!TaskBoard.dragCard.shadow) {
          TaskBoard.dragCard.avatar.cancel();
          TaskBoard.dragCard.shadow.cancel();
        } else {
          TaskBoard.dragCard.elem.style = null;
          TaskBoard.dragCard.shadow.parentNode.replaceChild(TaskBoard.dragCard.elem, TaskBoard.dragCard.shadow);

          //if (elem.closest('.column')) {
          //  const task = TaskBoard.taskService.getTask(TaskBoard.dragCard.elem.getAttribute('data-id'));
          //  task.status = elem.closest('.column').getAttribute('data-column-status');
          //  TaskBoard.taskService.editTask(task);
          //}
        }
      }
    }

    TaskBoard.dragCard = {};
  }
}

TaskBoard.init();