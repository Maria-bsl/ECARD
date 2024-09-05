$(document).ready(function () {
  var sidebar = $("#sidebar");
  var appMain = $(".app-main");
  var hambugerButton = $("#hamburger");
  var dropDownLinks = $(".app-link__dropdown");
  var arrayDropDownLinks = dropDownLinks.toArray();

  /* These variable works with header section on small screen */
  var ellipsisButton = $("#ellipsisBtn");
  var clickable_User_Logo = $("#user-logo");

  /* These two elements should be hidden on screen resize events */
  var user_info = $(clickable_User_Logo).next();
  var sm_head_wrapper = $("#sm-header-wrapper");

  $('[data-toggle="tooltip"]').tooltip();

  hambugerButtonClick(hambugerButton, sidebar, appMain, dropDownLinks);

  sidebarHover(hambugerButton, sidebar, appMain, dropDownLinks);

  resizeEvents(hambugerButton, sidebar, appMain);

  dropdownLinkClickAction(arrayDropDownLinks);

  headerManuvers(
    ellipsisButton,
    sm_head_wrapper,
    clickable_User_Logo,
    user_info
  );

  $(window).resize(function () {
    $(user_info).removeClass("active");
    $(sm_head_wrapper).removeClass("active");
  });
});

/**
 * This function manuvers the hide and show of various elements on the header on smaller and medium screen.
 *
 * @param {*} ellipsisButton The visible ellipsis button on the header
 * @param {*} sm_head_wrapper the container sm-header-wrapper
 * @param {*} clickable_User_Logo the user icon logo on the header which allows click action
 * @param {*} user_info the user information description container
 */
function headerManuvers(
  ellipsisButton,
  sm_head_wrapper,
  clickable_User_Logo,
  user_info
) {
  $(window).mouseup(function (e) {
    /* For screen below 576px show the hidden header component as 
    sm-header-wrapper */
    if (window.innerWidth < 576) {
      headerResponseOnResize(sm_head_wrapper, user_info);

      if (
        !$(ellipsisButton).is(e.target) &&
        $(ellipsisButton).has(e.target).length === 0
      ) {
        if (
          !$(sm_head_wrapper).is(e.target) &&
          $(sm_head_wrapper).has(e.target).length === 0
        ) {
          // Outside click
          $(sm_head_wrapper).removeClass("active");
        } else {
          // Inside click
          // DO nothing {Real do nothing!!!}
        }
        // Outside click
      } else {
        // Inside click
        $(sm_head_wrapper).toggleClass("active");
      }
    } else if (window.innerWidth >= 576 && window.innerWidth < 768) {
      headerResponseOnResize(sm_head_wrapper, user_info);

      if (
        !$(clickable_User_Logo).is(e.target) &&
        $(clickable_User_Logo).has(e.target).length === 0
      ) {
        if (
          !$(user_info).is(e.target) &&
          $(user_info).has(e.target).length === 0
        ) {
          // Outside click
          $(user_info).removeClass("active");
        } else {
          // Inside click
          // DO nothing {Real do nothing!!!}
        }
        // Outside click
      } else {
        // Inside click
        $(user_info).toggleClass("active");
      }
    } else {
      $(user_info).removeClass("active");
      $(sm_head_wrapper).removeClass("active");
    }
  });
}

/**
 * This function remove active class to sm-header-wrapper and user-info on screen resize events.
 *
 * @param {*} sm_head_wrapper
 * @param {*} user_info
 */

function headerResponseOnResize(sm_head_wrapper, user_info) {
  $(window).resize(function () {
    $(user_info).removeClass("active");
    $(sm_head_wrapper).removeClass("active");
  });
}

/**
 * The function check the link clicked if was expanded it collapses it, while leaving
 * all othe link collapsed too. In cases where the link clicked was collpased before it
 *  expands it collpasing any expanded link.
 *
 * @param {*} arrayDropDownLinks the array of dropdown links.
 */

function dropdownLinkClickAction(arrayDropDownLinks) {
  arrayDropDownLinks.forEach(function (dropDownLink) {
    $(dropDownLink).click(function () {
      compareLinks(dropDownLink, arrayDropDownLinks);
    });
  });
}

/**
 * The function compares the individual dropdown link against the array of
 * dropdown links toggling it by either expanding it or collapsing it. If the
 * link was not expanded it expands it and collpases the currently expanded link
 * if is there.
 *
 *
 * @param {*} dropDownLink the individual link.
 * @param {*} arrayDropDownLinks the array of dropdown links.
 */

function compareLinks(dropDownLink, arrayDropDownLinks) {
  for (let i = 0; i < arrayDropDownLinks.length; i++) {
    if (dropDownLink == arrayDropDownLinks[i]) {
      $(dropDownLink.nextElementSibling).slideToggle();
      $(dropDownLink).toggleClass("active");
    } else {
      $(arrayDropDownLinks[i].nextElementSibling).slideUp();
      $(arrayDropDownLinks[i]).removeClass("active");
    }
  }
}

/**
 * This function manuaver all the actions of the hambuger button.
 *
 * @param {*} hambugerButton hambuger button ID
 * @param {*} sidebar sidebar ID
 * @param {*} appMain the app-main class
 */

function hambugerButtonClick(hambugerButton, sidebar, appMain, dropDownLinks) {
  $(hambugerButton).click(function (e) {
    e.preventDefault();
    $(this).toggleClass("is-active");

    // If above 1024px apply this
    if (window.innerWidth >= 1024) {
      // check if is-active and decrease sidebar size
      if ($(this).hasClass("is-active")) {
        $(sidebar).addClass("lg-active");
        $(appMain).addClass("lg-active");

        // Close all opened link childrens
        deactivateDropdownLinks(dropDownLinks);
      } else {
        $(sidebar).removeClass("lg-active");
        $(appMain).removeClass("lg-active");
      }
    } else {
      if ($(this).hasClass("is-active")) {
        $(sidebar).addClass("lg-active");
        prependModalBackdrop();

        // closes modal backdrop
        $("#activeBackdrop").click(function (e) {
          if ($(this).is(":visible")) {
            e.preventDefault();
            $(hambugerButton).removeClass("is-active");
            $(sidebar).removeClass("lg-active");
            removeModalBackdrop();
          }
        });
      } else {
        $(sidebar).removeClass("lg-active");
        removeModalBackdrop();
      }
    }
  });
}

/**
 *This function manuaver all the actions of the sidebar hover response.
 * Whenever the sidebar is collapsed on viewport greater than 1024px the
 * page will be able to open and close upon hover in and out respectively.
 *
 *  @param {*} hambugerButton hambuger button ID
 * @param {*} sidebar sidebar ID
 * @param {*} appMain the app-main class
 */

function sidebarHover(hambugerButton, sidebar, appMain, dropDownLinks) {
  $(sidebar).hover(
    function () {
      // If above 1024px apply this
      if (window.innerWidth >= 1024) {
        if ($(hambugerButton).hasClass("is-active")) {
          $(sidebar).removeClass("lg-active");
        }
      }
    },
    function () {
      if ($(hambugerButton).hasClass("is-active")) {
        $(sidebar).addClass("lg-active");
        $(appMain).addClass("lg-active");

        // Close all opened link childrens
        deactivateDropdownLinks(dropDownLinks);
      }
    }
  );
}

/**
 * This function read the viewport resize and if less than 1024px, it
 * removes class "is-active" of hambuger ID.
 *
 * @param {*} hambugerButton
 */

function resizeEvents(hambugerButton, sidebar, appMain) {
  $(window).resize(function () {
    if (window.innerWidth < 1024) {
      if ($(hambugerButton).hasClass("is-active")) {
        $(hambugerButton).removeClass("is-active");
        $(sidebar).removeClass("lg-active");

        // Remove backdrop modal if it is visible
        if ($("#activeBackdrop").is(":visible")) {
          removeModalBackdrop();
        }
      }
    } else {
      if ($("#activeBackdrop").is(":visible")) {
        // Remove backdrop modal if it is visible
        removeModalBackdrop();

        $(sidebar).removeClass("lg-active");
        $(appMain).removeClass("lg-active");
      }

      // Close the hambuger button if still opened
      if ($(hambugerButton).hasClass("is-active")) {
        $(hambugerButton).removeClass("is-active");
      }
    }
  });
}

/**
 * The function closes all opened dropdownlinks once either the hambuger button is
 * clicked or on the scenarios whereby there is hover on a collapsed sidebar.
 *
 * @param {*} dropDownLinks the class for the app-link__dropdown.
 *
 */

function deactivateDropdownLinks(dropDownLinks) {
  if ($(dropDownLinks).hasClass("active")) {
    $(dropDownLinks).removeClass("active").next().hide();
  }
}

function prependModalBackdrop() {
  // Show the backdrop
  $('<div id="activeBackdrop" class="modal-backdrop"></div>').appendTo(
    document.body
  );
  $(document.body).addClass("modal-open");
}

function removeModalBackdrop() {
  // Remove it (later)
  $("#activeBackdrop.modal-backdrop").remove();
  $(document.body).removeClass("modal-open");
}
