class ElementFactory {

  createAvatar(elem) {
    const avatar = elem;
    const old = {
      parent: avatar.parentNode,
      nextSibling: avatar.nextSibling,
      position: avatar.position || '',
      left: avatar.left || '',
      top: avatar.top || '',
      zIndex: avatar.zIndex || ''
    };

    avatar.cancel = () => {
      old.parent.insertBefore(avatar, old.nextSibling);
      avatar.style.position = old.position;
      avatar.style.left = old.left;
      avatar.style.top = old.top;
      avatar.style.zIndex = old.zIndex;
    };

    return avatar;
  }

  createShadow(elem) {
    const shadowElem = document.createElement('div');
    shadowElem.setAttribute('class', 'card-shadow');
    shadowElem.innerHTML = "&nbsp;";
    elem.insertAdjacentElement("afterend", shadowElem);

    shadowElem.cancel = () => {
      shadowElem.remove();
    };

    return shadowElem;
  }
}