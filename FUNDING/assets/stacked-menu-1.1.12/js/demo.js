'use strict'

// StackedMenu
var menu = new StackedMenu();
var qsCompact = document.querySelectorAll('[data-js="compact"]');
var qsHoverable = document.querySelector('[data-js="hoverable"]');
var qsCloseOther = document.querySelector('[data-js="close-other"]');
var qsAlign = document.querySelector('[data-js="align"]');

menu.each.call(qsCompact, function(el) {
  menu._on(el, 'click', function() {
    menu.compact(!menu.isCompact());
  });
});
menu._on(qsHoverable, 'click', function() {
  menu.hoverable(!menu.isHoverable());
});
menu._on(qsCloseOther, 'click', function() {
  menu.options.closeOther = !menu.options.closeOther;
});
menu._on(qsAlign, 'click', function() {
  var align = menu.options.align === 'left' ? 'right' : 'left';
  menu.align(align);
});
