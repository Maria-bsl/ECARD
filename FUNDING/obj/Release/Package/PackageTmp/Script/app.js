$(document).ready(function () {
    toggleRequiredClass();

    openModalOnBtnClick('ajaxHelperCreateModal');
    openModalOnBtnClick('ajaxHelperUpdateModal');
});

function toggleRequiredClass() {
    var $formGroup;
    var $formGroupList = $('.form-group');
    var $requiredInput = $formGroupList.find('[data-val-required]');

    $.map($requiredInput, function (value, index) {
        $formGroup = $(value).parentsUntil('div.form-group').parent();
        $formGroup.find('label').addClass('field-required')
    });
}

function openModalOnBtnClick(modal) {
    $('button[data-bs-target="#' + modal + '"]').on('click', function (e) {
        e.preventDefault();
        $('#' + modal).modal('show')
    });

    closeModalOnBtnClick(modal);
}

function closeModalOnBtnClick(modal) {
    $('button[data-bs-dismiss="modal"]').on('click', function (e) {
        e.preventDefault();
        $('#' + modal).modal('hide')
    });
}