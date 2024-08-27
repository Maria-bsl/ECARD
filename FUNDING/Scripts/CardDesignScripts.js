$(document).ready(function () {

    var $vPillsTabContent = $('#v-pills-tabContent');
    var $cardMainViewport = $('.card-main-viewport');
    var $tabToggleButton = $('.tab-toggle-button');

    $tabToggleButton.on('click', function () {
        if ($vPillsTabContent.hasClass('show')) {
            hidePillsTabContent($vPillsTabContent);

            fullCardMainViewport($cardMainViewport);

            //Change icon sign
            $tabToggleButton.find('i').removeClass('fa-outdent').addClass('fa-indent');
        } else {
            showPillsTabContent($vPillsTabContent);

            resizeCardMainViewport($cardMainViewport);

            //Change icon sign
            $tabToggleButton.find('i').removeClass('fa-indent').addClass('fa-outdent');
        }
    });


    var $cardItems = $('.card-item');

    cardItemFillTextInteraction($cardItems);
    fillTextFieldEditResponse($cardItems);

    fontsTabButtonHandler($cardItems, $vPillsTabContent, $cardMainViewport);
    colorsTabButtonHandler($cardItems, $vPillsTabContent, $cardMainViewport);

    //boldFontButtonHandler
    textStyleButtonHandler($('#bold-button'), $cardItems, 'bold-card-item');

    //italicFontButtonHandler
    textStyleButtonHandler($('#italic-button'), $cardItems, 'italic-card-item');

    //textDecorationFontButtonHandler
    textStyleButtonHandler($('#text-underline-button'), $cardItems, 'text-underline-card-item');

    textAlignmentHandler($cardItems);

    setTextColorHandler($cardItems);

    selectCardTemplateHandler();

    displayCardSizeHandler();
});

function displayCardSizeHandler() {
    var $showCardSize = $('#ShowCardSize');
    var $cardSizeDisplay = $('#card-size-temp');

    $showCardSize.prop('checked', false);

    $showCardSize.on('change', function () {
        if ($showCardSize.is(':checked')) {
            $cardSizeDisplay.show();
            draggableElementData.displayCardSize = true;
        } else {
            $cardSizeDisplay.hide();
            draggableElementData.displayCardSize = false;
        }

        showPreviewButtonHideDownloadButton();
    });


}

function removeFocuSToAllCardItems($cardItems) {
    $cardItems.removeClass('focus');
}

function addFocusClassToActiveCardItem($currentCardItem) {
    $currentCardItem.addClass('focus');
}

function showPillsTabContentHandler() {
    // Open the fill text tab (In case where the tab is hidden, open it)
    var $pillsfillText = $('#pills-fillText-tab');
    $pillsfillText.click();

    var $vPillsTabContent = $('#v-pills-tabContent');
    var $cardMainViewport = $('.card-main-viewport');
    showPillsTabContent($vPillsTabContent);
    resizeCardMainViewport($cardMainViewport);
}

function $pillsFillTextFieldsInteractionsHandler(cardItem) {
    var $pillsFillTextFields = $('#pills-fillText .form-group').children();

    // Todo: while switching to this tab, field focus is not triggered due to the delay of the tab field to be displayed
    $.each($pillsFillTextFields, function (index, field) {
        $currentField = $(field);

        if ($currentField.is('input') || $currentField.is('textarea') || $currentField.is('select')) {
            if ($(cardItem).attr('data-content') == $currentField.attr('data-content')) {
                $currentField.focus();
            }
        }
    });
}

function setLeftPositionToCurrentActiveCardFieldHandler($currentCardItem) {
    var leftPosition = $currentCardItem.css('left');
    getLeftPositionStyleField($currentCardItem.attr('data-parentField')).val(leftPosition);
}

function setTopPositionToCurrentActiveCardFieldHandler($currentCardItem) {
    var topPosition = $currentCardItem.css('top');
    getTopPositionStyleField($currentCardItem.attr('data-parentField')).val(topPosition);
}

function cardItemFillTextInteraction($cardItems) {
    $.each($cardItems, function (index, cardItem) {
        $(cardItem).draggable({
            snap: ".card-main-template",
            snapMode: "inner"
        });

        $cardItems.on('drag', function (e) {
            removeFocuSToAllCardItems($cardItems);

            $currentCardItem = $(this);
            addFocusClassToActiveCardItem($currentCardItem);
            setLeftPositionToCurrentActiveCardFieldHandler($currentCardItem);
            setTopPositionToCurrentActiveCardFieldHandler($currentCardItem);
        });

        $(cardItem).on('click', function (e) {
            e.preventDefault();

            removeFocuSToAllCardItems($cardItems);

            $currentCardItem = $(this);
            addFocusClassToActiveCardItem($currentCardItem);

            // Open the fill text tab (In case where the tab is hidden, open it)
            showPillsTabContentHandler();

            // Highlight the element with data corresponding the clicked card item
            $pillsFillTextFieldsInteractionsHandler(cardItem);
        })
    })
}

function fillTextFieldEditResponse($cardItems) {
    var $pillsFillTextFields = $('#pills-fillText .form-group').children();
    $.each($pillsFillTextFields, function (index, field) {
        $field = $(field);

        if ($field.is('input')) {

            // get data-content value of the specific field element.
            var dataContentAttribute = $field.attr('data-content');

            // initial fill of card item with data from tab field
            $cardItem = $('.card-item[data-content="' + dataContentAttribute + '"]');
            $cardItem.html('<pre class="m-0">' + $field.val() + '</pre>');


            // Trigger text change in the card item corresponding to the field element based on the data-content values.
            $field.on('input', function (e) {
                e.preventDefault();

                // set the [$(this)] to current field
                var $currentfield = $(this);

                $.each($cardItems, function (index, cardItem) {
                    if ($(cardItem).attr('data-content') == dataContentAttribute) {
                        $(cardItem).html('<pre class="m-0">' + $currentfield.val() + '</pre>');
                    }
                })
            });
        }
    })
}

function showPillsTabContent($vPillsTabContent) {
    $vPillsTabContent.addClass('show');
    $vPillsTabContent.removeClass('hide');
}
function hidePillsTabContent($vPillsTabContent) {
    $vPillsTabContent.removeClass('show');
    $vPillsTabContent.addClass('hide');
}

function fullCardMainViewport($cardMainViewport) {
    $cardMainViewport.removeClass('c-vw-resized');
    $cardMainViewport.addClass('c-vw-full');
}
function resizeCardMainViewport($cardMainViewport) {
    $cardMainViewport.removeClass('c-vw-full');
    $cardMainViewport.addClass('c-vw-resized');
}

function fontsTabButtonHandler($cardItems, $vPillsTabContent, $cardMainViewport) {
    $('#font-tab').on('click', function () {
        if ($cardItems.hasClass('focus')) {
            $('#pills-fonts-tab').click();

            showPillsTabContent($vPillsTabContent);
            resizeCardMainViewport($cardMainViewport);
        } else {
            alert('Please select a card item first.')
        }
    });
}

function colorsTabButtonHandler($cardItems, $vPillsTabContent, $cardMainViewport) {
    $('#colors-tab').on('click', function () {
        if ($cardItems.hasClass('focus')) {
            $('#pills-colors-tab').click();

            showPillsTabContent($vPillsTabContent);
            resizeCardMainViewport($cardMainViewport);
        } else {
            alert('Please select a card item first.');
        }
    });
}

function textStyleButtonHandler($textStyleButton, $cardItems, textStyleClass) {
    $textStyleButton.on('click', function () {
        var $button = $(this);

        if ($cardItems.hasClass('focus')) {
            $.each($cardItems, function (index, cardItem) {
                $cardItem = $(cardItem);
                if ($cardItem.hasClass('focus')) {
                    $cardItem.toggleClass(textStyleClass);

                    toggleBoldStyleCheck($button, $cardItem);
                    toggleItalicStyleCheck($button, $cardItem);
                    toggleUnderlineStyleCheck($button, $cardItem);

                    showPreviewButtonHideDownloadButton();
                }
            });
        } else {
            alert('Please select a card item first.')
        }
    });
}

function toggleBoldStyleCheck($button, $cardItem) {
    if ($button.prop('id') == 'bold-button') {
        if ($cardItem.hasClass('bold-card-item')) {
            $button.addClass('active');
            getBoldTextStyleField($cardItem.attr('data-parentField')).click();

            if ($cardItem.attr('data-content') == 'event-invitee') {
                draggableElementData.editableElement.FontDetails.Bold = true;
            }
            else if ($cardItem.attr('data-content') == 'card_size_label') {
                draggableElementData.cardSizeLabel.FontDetails.Bold = true;
            }
            else if ($cardItem.attr('data-content') == 'table_number_label') {
                draggableElementData.tableNumberLabel.FontDetails.Bold = true;
            }
            else if ($cardItem.attr('data-content') == 'table_number_digit') {
                draggableElementData.tableNumberDigit.FontDetails.Bold = true;
            }

        } else {
            $button.removeClass('active');
            getBoldTextStyleField($cardItem.attr('data-parentField')).click();

            if ($cardItem.attr('data-content') == 'event-invitee') {
                draggableElementData.editableElement.FontDetails.Bold = false;
            }
            else if ($cardItem.attr('data-content') == 'card_size_label') {
                draggableElementData.cardSizeLabel.FontDetails.Bold = false;
            }
            else if ($cardItem.attr('data-content') == 'table_number_label') {
                draggableElementData.tableNumberLabel.FontDetails.Bold = false;
            }
            else if ($cardItem.attr('data-content') == 'table_number_digit') {
                draggableElementData.tableNumberDigit.FontDetails.Bold = false;
            }
        }
    }
}

function toggleItalicStyleCheck($button, $cardItem) {
    if ($button.prop('id') == 'italic-button') {
        if ($cardItem.hasClass('italic-card-item')) {
            $button.addClass('active');
            getItalicTextStyleField($cardItem.attr('data-parentField')).click();

            if ($cardItem.attr('data-content') == 'event-invitee') {
                draggableElementData.editableElement.FontDetails.Italic = true;
            }
            else if ($cardItem.attr('data-content') == 'card_size_label') {
                draggableElementData.cardSizeLabel.FontDetails.Italic = true;
            }
            else if ($cardItem.attr('data-content') == 'table_number_label') {
                draggableElementData.tableNumberLabel.FontDetails.Italic = true;
            }
            else if ($cardItem.attr('data-content') == 'table_number_digit') {
                draggableElementData.tableNumberDigit.FontDetails.Italic = true;
            }
        } else {
            $button.removeClass('active');
            getItalicTextStyleField($cardItem.attr('data-parentField')).click();

            if ($cardItem.attr('data-content') == 'event-invitee') {
                draggableElementData.editableElement.FontDetails.Italic = false;
            } 
             else if ($cardItem.attr('data-content') == 'card_size_label') {
                draggableElementData.cardSizeLabel.FontDetails.Italic = false;
            }
            else if ($cardItem.attr('data-content') == 'table_number_label') {
                draggableElementData.tableNumberLabel.FontDetails.Italic = false;
            }
            else if ($cardItem.attr('data-content') == 'table_number_digit') {
                draggableElementData.tableNumberDigit.FontDetails.Italic = false;
            }
        }
    }
}

function toggleUnderlineStyleCheck($button, $cardItem) {
    if ($button.prop('id') == 'text-underline-button') {
        if ($cardItem.hasClass('text-underline-card-item')) {
            $button.addClass('active');
            getUnderlineTextStyleField($cardItem.attr('data-parentField')).click();
            //draggableElementData.editableElement.FontDetails.Underline = true;

            if ($cardItem.attr('data-content') == 'event-invitee') {
                draggableElementData.editableElement.FontDetails.Underline = true;
            }
            else if ($cardItem.attr('data-content') == 'table_number_label') {
                draggableElementData.tableNumberLabel.FontDetails.Underline = true;
            }
            else if ($cardItem.attr('data-content') == 'card_size_label') {
                draggableElementData.cardSizeLabel.FontDetails.Underline = true;
            }
            else if ($cardItem.attr('data-content') == 'table_number_digit') {
                draggableElementData.tableNumberDigit.FontDetails.Underline = true;
            }
        } else {
            $button.removeClass('active');
            getUnderlineTextStyleField($cardItem.attr('data-parentField')).click();
            //draggableElementData.editableElement.FontDetails.Underline = false;

            if ($cardItem.attr('data-content') == 'event-invitee') {
                draggableElementData.editableElement.FontDetails.Underline = false;
            }
            else if ($cardItem.attr('data-content') == 'card_size_label') {
                draggableElementData.cardSizeLabel.FontDetails.Underline = false;
            }
            else if ($cardItem.attr('data-content') == 'table_number_label') {
                draggableElementData.tableNumberLabel.FontDetails.Underline = false;
            }
            else if ($cardItem.attr('data-content') == 'table_number_digit') {
                draggableElementData.tableNumberDigit.FontDetails.Underline = false;
            }
        }
    }
}

function textAlignmentHandler($cardItems) {
    var $listOfTextAlignLink = $('#text-align-list a');
    $listOfTextAlignLink.on('click', function () {
        var $textAlignLink = $(this);

        if ($cardItems.hasClass('focus')) {
            $.each($cardItems, function (index, cardItem) {
                $cardItem = $(cardItem);
                if ($cardItem.hasClass('focus')) {
                    var textAlignValue = makeTextLinkActive($textAlignLink);
                    $cardItem.find('span').css('text-align', textAlignValue);

                    getTextAlignmentStyleField($cardItem.attr('data-parentField')).val(textAlignValue);

                    if ($cardItem.attr('data-content') == 'event-invitee') {
                        draggableElementData.editableElement.FontDetails.TextAlign = textAlignValue;
                    }
                    else if ($cardItem.attr('data-content') == 'card_size_label') {
                        draggableElementData.cardSizeLabel.FontDetails.TextAlign = textAlignValue;
                    }
                    else if ($cardItem.attr('data-content') == 'table_number_label') {
                        draggableElementData.tableNumberLabel.FontDetails.TextAlign = textAlignValue;
                    }
                    else if ($cardItem.attr('data-content') == 'table_number_digit') {
                        draggableElementData.tableNumberDigit.FontDetails.TextAlign = textAlignValue;
                    }

                    showPreviewButtonHideDownloadButton();
                }
            });
        } else {
            alert('Please select a card item first.')
        }
    });
}

function setTextColorHandler($cardItems) {
    var $listOfDefaultColors = $('#default-colors-list>.color-box');

    $listOfDefaultColors.on('click', function () {
        var $selectedColorBox = $(this);

        if ($cardItems.hasClass('focus')) {
            $.each($cardItems, function (index, cardItem) {
                $cardItem = $(cardItem);
                if ($cardItem.hasClass('focus')) {
                    var colorValue = $selectedColorBox.css('background-color');
                    $cardItem.find('span').css('color', colorValue);
                    setActiveColorToColorButton(colorValue);
                    getTextColorStyleField($cardItem.attr('data-parentField')).val(colorValue);

                    if ($cardItem.attr('data-content') == 'event-invitee') {
                        draggableElementData.editableElement.FontDetails.Color = colorValue;
                    }
                    else if ($cardItem.attr('data-content') == 'card_size_label') {
                        draggableElementData.cardSizeLabel.FontDetails.Color = colorValue;
                    }
                    else if ($cardItem.attr('data-content') == 'table_number_label') {
                        draggableElementData.tableNumberLabel.FontDetails.Color = colorValue;
                    }
                    else if ($cardItem.attr('data-content') == 'table_number_digit') {
                        draggableElementData.tableNumberDigit.FontDetails.Color = colorValue;
                    }

                    showPreviewButtonHideDownloadButton();
                }
            });
        } else {
            alert('Please select a card item first.')
        }
    });
}

function setActiveColorToColorButton(colorValue) {
    $('#colors-tab>span').css('background-color', colorValue)
}

function makeTextLinkActive($textAlignLink) {
    $textAlignLink.parent().parent().find('a>i.fas.fa-check').remove();
    $textAlignLink.append('<i class="fas fa-check"></i>');

    return $textAlignLink.attr('data-text-align');
}

function selectCardTemplateHandler() {
    var $allCardTemplates = $('.card-template-list').find('.image-container');

    $allCardTemplates.on('click', function () {
        var $cardTemplate = $(this).find('img');
        var cardTemplateFullPath = $cardTemplate.attr('src');

        changeCardTemplateFile(cardTemplateFullPath);
    });
}

function changeCardTemplateFile(cardTemplateFullPath) {
    var $cardMainTemplate = $('.card-main-template');
    var cardTemplateStyle = 'url(' + cardTemplateFullPath + ')';
    $cardMainTemplate.css('background-image', cardTemplateStyle);

    draggableElementData.cardTemplate = '~' + cardTemplateFullPath;
}

function convertColorFromHexToRGB(hex, alpha = null) {

    const r = parseInt(hex.slice(1, 3), 16);
    const g = parseInt(hex.slice(3, 5), 16);
    const b = parseInt(hex.slice(5, 7), 16);

    if (alpha) {
        return r + "," + g + "," + b + "," + alpha;
    } else {
        return r + "," + g + "," + b;
    }
}


/* Text field styles*/
function getFontFamilyStyleField(parentField) {
    return $("#" + parentField + "Styles_FontFamily");
}

function getFontSizeStyleField(parentField) {
    return $("#" + parentField + "Styles_FontSize");
}

function getTextColorStyleField(parentField) {
    return $("#" + parentField + "Styles_TextColor");
}

function getBoldTextStyleField(parentField) {
    return $("#" + parentField + "Styles_BoldTextStyle");
}

function getItalicTextStyleField(parentField) {
    return $("#" + parentField + "Styles_ItalicTextStyle");
}

function getUnderlineTextStyleField(parentField) {
    return $("#" + parentField + "Styles_UnderlineTextStyle");
}

function getTextAlignmentStyleField(parentField) {
    return $("#" + parentField + "Styles_TextAlignStyle");
}

function getLeftPositionStyleField(parentField) {
    return $("#" + parentField + "Styles_LeftPosition");
}

function getTopPositionStyleField(parentField) {
    return $("#" + parentField + "Styles_TopPosition");
}