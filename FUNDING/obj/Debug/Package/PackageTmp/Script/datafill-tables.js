// ROTATE IN TABS
const company_info = document.getElementById('company-info');
const organisation = document.getElementById('organisation');
const business = document.getElementById('business');
const references = document.getElementById('references');
const shareholding = document.getElementById('shareholding');
const certification = document.getElementById('certification');
const declaration = document.getElementById('declaration');

// Buttons
const nextBtn = document.getElementById('ContentPlaceHolder1_nextBtn');
const previousBtn = document.getElementById('prevBtn');
var count = 0; //For counting on button click Previous <==> Next

let tabArray = [company_info, organisation, business, references, shareholding, certification, declaration];

// Hide all tabs
const hideAll = () => {
    for (let index = 0; index < tabArray.length; index++) {
        tabArray[index].style.display = "none";
        document.querySelectorAll('.tab-buttons .step-button')[index].classList.remove('active');
    }
};

const fixStepIndicator = (n) => {
    // This function removes the "active" class of all steps...
    let i, x = document.getElementsByClassName("step");
    for (i = 0; i < x.length; i++) {
        x[i].className = x[i].className.replace(" active", "");
        tabBtn[i].parentElement.classList.remove('active');
    }
    //... and adds the "active" class on the current step:
    x[n].className += " active";
    tabBtn[n].parentElement.classList.add('active');
};

const fixation = (count) => {
    if (count > 0) {
        previousBtn.removeAttribute('disabled');
    } else {
        previousBtn.disabled = 'true';
    }

    if (count == 6) {
        nextBtn.value = 'Submit';
    } else {
        nextBtn.value = 'Next';
    }
};

const removeActiveTab = () => {
    Array.from(tabBtn).forEach(tabs => {
        tabs.classList.remove('active');
        tabs.parentElement.classList.remove('active');
    });
};

// Show first tab only
tabArray[0].style.display = 'block';
// By default previous btn is disabled
if (count == 0) {
    previousBtn.disabled = 'true';
}
// By default step one is active
document.getElementsByClassName("step")[0].classList.add('active');

nextBtn.addEventListener('click', () => {
    if (count < 6) {
        count++;
        hideAll();
        document.querySelectorAll('.tab-buttons .step-button')[count].classList.add('active');
        tabArray[count].style.display = 'block';
        fixation(count);
        fixStepIndicator(count);
        //lets count 
        console.log(count);
    }
    if (nextBtn.textContent == 'Submit') {
       // document.getElementById("regForm").submit();
    }
});

previousBtn.addEventListener('click', () => {
    if (count > 0) {
        count--;
        hideAll();
        document.querySelectorAll('.tab-buttons .step-button')[count].classList.add('active');
        tabArray[count].style.display = 'block';
        fixation(count);
        fixStepIndicator(count);
    }
});



// TAB BUTTONS
const tabBtn = document.querySelectorAll('.tab-buttons .step-button');
// Default status
// tabBtn[0].classList.add('active');
// tabBtn[0].parentElement.classList.add('active');


Array.from(tabBtn).forEach(tabButton => {
    tabButton.addEventListener('click', e => {
        removeActiveTab();

        let stepBtn = null;

        if (e.target.tagName == 'H3') {
            stepBtn = e.target.parentElement;
        } else {
            stepBtn = e.target.parentElement.parentElement;
        }

        let tabBtn_txt = ['Company Info', 'Organisation & Employment', 'Business & Services', 'Experience', 'Shareholders', 'Proffesional Bodies', 'Declaration'];

        switch (stepBtn.children[0].textContent) {
            case tabBtn_txt[0]:
                hideAll();
                count = 0;
                tabArray[count].style.display = 'block';
                stepBtn.classList.add('active');
                fixation(count);
                fixStepIndicator(count);
                break;
            case tabBtn_txt[1]:
                hideAll();
                count = 1;
                tabArray[count].style.display = 'block';
                stepBtn.classList.add('active');
                fixation(count);
                fixStepIndicator(count);
                break;
            case tabBtn_txt[2]:
                hideAll();
                count = 2;
                tabArray[count].style.display = 'block';
                stepBtn.classList.add('active');
                fixation(count);
                fixStepIndicator(count);
                break;
            case tabBtn_txt[3]:
                hideAll();
                count = 3;
                tabArray[count].style.display = 'block';
                stepBtn.classList.add('active');
                fixation(count);
                fixStepIndicator(count);
                break;
            case tabBtn_txt[4]:
                hideAll();
                count = 4;
                tabArray[count].style.display = 'block';
                stepBtn.classList.add('active');
                fixation(count);
                fixStepIndicator(count);
                break;
            case tabBtn_txt[5]:
                hideAll();
                count = 5;
                tabArray[count].style.display = 'block';
                stepBtn.classList.add('active');
                fixation(count);
                fixStepIndicator(count);
                break;
            case tabBtn_txt[6]:
                hideAll();
                count = 6;
                tabArray[count].style.display = 'block';
                stepBtn.classList.add('active');
                fixation(count);
                fixStepIndicator(count);
                break;
        }
    });
});


// TURNOVER FLOW-WORK
const turnOverTable = document.querySelector('.turnover-table tbody');
const turnOver_saveBtn = document.querySelector('.saveBtn-turnOver');

// BY DEFAULT
checkToClearBtn('.turnover-table', 3);

turnOver_saveBtn.addEventListener('click', () => {
    // e.preventDefault();
    document.querySelector('.turnover-table tbody .active-class').children[1].children[0].innerHTML = document.getElementById('modal-financial-year').value.trim();
    document.querySelector('.turnover-table tbody .active-class').children[1].children[1].value = document.getElementById('modal-financial-year').value.trim();
    document.querySelector('.turnover-table tbody .active-class').children[2].children[0].innerHTML = document.getElementById('modal-usd-amount').value.trim();
    document.querySelector('.turnover-table tbody .active-class').children[2].children[1].value = document.getElementById('modal-usd-amount').value.trim();
});


turnOverTable.addEventListener('click', (e) => {

    // Delete Row
    if (e.target.classList.contains('btnDelete-Row')) {
        // if table row is 4 or 3 in path
        if (e.path[4].tagName == 'TR') e.path[4].remove();
        if (e.path[3].tagName == 'TR') e.path[3].remove();

        checkToClearBtn('.turnover-table', 3);
    }

    // Clear Row values
    if (e.target.classList.contains('btnClear-Row')) {

        // if table row is 4 or 3 in path
        if (e.path[3].tagName == 'TR') {
            Array.from(e.path[3].children).forEach((td) => {
                if (td.children[0].tagName != 'SPAN' && td.children[0].tagName != 'DIV') {
                    td.children[0].textContent = "";
                    td.children[1].value = "";
                }
            });
        }

        if (e.path[4].tagName == 'TR') {
            Array.from(e.path[4].children).forEach((td) => {
                if (td.children[0].tagName != 'SPAN' && td.children[0].tagName != 'DIV') {
                    td.children[0].textContent = "";
                    td.children[1].value = "";
                }
            });
        }
    }

    // Add Row
    if (e.target.classList.contains('btnAdd-Row')) {
        //Listen to add button click only
        let addBtn;
        if (e.target.tagName == 'I') {
            addBtn = e.path[1];
        } else {
            addBtn = e.path[0];
        }

        const newRow = `
        <tr>
            <td><span></span></td>
            <td>
                <p></p>
                <input type="text" class="d-none" name='financial-year' id="financial-year">
            </td>
            <td>
                <p></p>
                <input type="text" class="d-none" name='amount' id="usd-amount">
            </td>
            <td class="align-content-between">
                <div class="fixed-buttons">
                    <span class="btnEdit-Row" data-toggle="modal" data-target="#turnOverInfo" style="padding: 5px; cursor: pointer;" title="Edit this row">
                        <i class="fa btnEdit-Row fa-pencil color-muted m-r-5"></i>
                    </span>
                    <span class="btn btnClear-Row d-none" style="padding: 5px; cursor: pointer;" title="Delete this row">
                        <i
                    class="fa fa-eraser color-danger"></i></span>
                    <span class="btn btnDelete-Row" style="padding: 5px; cursor: pointer;" title="Delete this row">
                        <i class="fa btnDelete-Row fa-close color-danger"></i>
                    </span>
                    <span class="btn btnAdd-Row" style="padding: 5px; cursor: pointer;" title="Add new row">
                        <i class="fa btnAdd-Row fa-plus"></i>
                    </span>
                </div>
            </td>
        </tr>`;

        turnOverTable.innerHTML += newRow;

        checkToClearBtn('.turnover-table', 3);
    }

    // if

    // Function to remove active-classes
    const removeActiveClass = () => {
        Array.from(turnOverTable.children).forEach((row) => {
            if (row.classList.contains('active-class')) {
                row.classList.remove('active-class');
            }
        });
    };
    // Add class for picking
    if (e.target.classList.contains('btnEdit-Row')) {
        e.preventDefault();
        if (e.path[4].tagName == 'TR') {
            removeActiveClass();
            e.path[4].classList.add('active-class');
        }
        if (e.path[3].tagName == 'TR') {
            removeActiveClass();
            e.path[3].classList.add('active-class');
        }

        const activeRow = document.querySelector('.turnover-table tbody .active-class');

        document.getElementById('modal-financial-year').value = activeRow.children[1].textContent.trim();
        document.getElementById('modal-usd-amount').value = activeRow.children[2].textContent.trim();
    }
});



// EMPLOYEES TABLE
const employeesTable = document.querySelector('.employees-table tbody tr');
const employeeInfo_saveBtn = document.querySelector('.saveBtn-employeeInfo');

employeesTable.addEventListener('click', (e) => {

    // Clear Row values
    if (e.target.classList.contains('btnClear-Row')) {
        // if table row is 4 or 3 in path
        if (e.path[3].tagName == 'TR') {
            Array.from(e.path[3].children).forEach((td) => {
                if (td.children[0].tagName != 'SPAN' && td.children[0].tagName != 'DIV') {
                    td.children[0].textContent = "";
                    td.children[1].value = "";
                }
            });
        }
        if (e.path[4].tagName == 'TR') {
            Array.from(e.path[4].children).forEach((td) => {
                if (td.children[0].tagName != 'SPAN' && td.children[0].tagName != 'DIV') {
                    td.children[0].textContent = "";
                    td.children[1].value = "";
                }
            });
        }
    }

    const removeActiveClass = () => {
        Array.from(employeesTable.children).forEach((row) => {
            if (row.classList.contains('active-class')) {
                row.classList.remove('active-class');
            }
        });
    };

    // Add class for picking
    if (e.target.classList.contains('btnEdit-Row')) {
        e.preventDefault();

        const activeRow = document.querySelector('.employees-table tbody tr');

        document.getElementById('modal-total-employees').value = activeRow.children[0].textContent.trim();
        document.getElementById('modal-tz-employees').value = activeRow.children[1].textContent.trim();
        document.getElementById('modal-non-tz-employees').value = activeRow.children[2].textContent.trim();
        document.getElementById('modal-skilled-emp').value = activeRow.children[3].textContent.trim();
        document.getElementById('modal-semi-skilled-emp').value = activeRow.children[4].textContent.trim();
        document.getElementById('modal-unskilled-emp').value = activeRow.children[5].textContent.trim();
    }
});

// Employees Informations
inputNumber($('#modal-total-employees'));
inputNumber($('#modal-tz-employees'));
inputNumber($('#modal-non-tz-employees'));
inputNumber($('#modal-skilled-emp'));
inputNumber($('#modal-semi-skilled-emp'));
inputNumber($('#modal-unskilled-emp'));


// CLICK SAVE BTN FOR EMPLOYEES
employeeInfo_saveBtn.addEventListener('click', () => {
    // e.preventDefault();
    const activeRow = document.querySelector('.employees-table tbody tr');


    let idArray = ['modal-total-employees', 'modal-tz-employees', 'modal-non-tz-employees', 'modal-skilled-emp', 'modal-semi-skilled-emp', 'modal-unskilled-emp'];

    for (let i = 0; i < (activeRow.childElementCount - 1); i++) {
        activeRow.children[i].children[0].innerHTML = document.getElementById(idArray[i]).value.trim();
        activeRow.children[i].children[1].value = document.getElementById(idArray[i]).value.trim();
    }
});




// BRANCHES TABLE
const branchesTable = document.querySelector('.branches-table tbody tr');
const branchesInfo_saveBtn = document.querySelector('.saveBtn-branchesInfo');

branchesTable.addEventListener('click', (e) => {

    // Clear Row values
    if (e.target.classList.contains('btnClear-Row')) {
        // if table row is 4 or 3 in path
        if (e.path[3].tagName == 'TR') {
            Array.from(e.path[3].children).forEach((td) => {
                if (td.children[0].tagName != 'SPAN' && td.children[0].tagName != 'DIV') {
                    td.children[0].textContent = "";
                    td.children[1].value = "";
                }
            });
        }
        if (e.path[4].tagName == 'TR') {
            Array.from(e.path[4].children).forEach((td) => {
                if (td.children[0].tagName != 'SPAN' && td.children[0].tagName != 'DIV') {
                    td.children[0].textContent = "";
                    td.children[1].value = "";
                }
            });
        }
    }


    // Add class for picking
    if (e.target.classList.contains('btnEdit-Row')) {
        e.preventDefault();

        const activeRow = document.querySelector('.branches-table tbody tr');

        let idArray = ['modal-branches', 'modal-plants', 'modal-warehouses'];

        for (let index = 0; index < (activeRow.childElementCount - 1); index++) {
            document.getElementById(idArray[index]).value = activeRow.children[index].children[0].textContent.trim();
        }
    }
});

// Branches informations
inputNumber($('#modal-branches'));
inputNumber($('#modal-plants'));
inputNumber($('#modal-warehouses'));


// CLICK SAVE BTN FOR EMPLOYEES employee
branchesInfo_saveBtn.addEventListener('click', () => {
    // e.preventDefault();
    const activeRow = document.querySelector('.branches-table tbody tr');

    let idArray = ['modal-branches', 'modal-plants', 'modal-warehouses'];

    for (let i = 0; i < (activeRow.childElementCount - 1); i++) {
        activeRow.children[i].children[0].innerHTML = document.getElementById(idArray[i]).value.trim();
        activeRow.children[i].children[1].value = document.getElementById(idArray[i]).value.trim();
    }
});



// REFERENCES TABLE
const referenceTable = document.querySelector('.reference-table tbody tr');
const reference_saveBtn = document.querySelector('.saveBtn-References');

// Default clear button
checkToClearBtn('.reference-table', 9);

referenceTable.addEventListener('click', (e) => {

    // Clear Row values
    if (e.target.classList.contains('btnClear-Row')) {
        // if table row is 4 or 3 in path
        if (e.path[3].tagName == 'TR') {
            Array.from(e.path[3].children).forEach((td) => {
                if (td.children[0].tagName != 'SPAN' && td.children[0].tagName != 'DIV') {
                    td.children[0].textContent = "";
                    td.children[1].value = "";
                }
            });
        }
        if (e.path[4].tagName == 'TR') {
            Array.from(e.path[4].children).forEach((td) => {
                if (td.children[0].tagName != 'SPAN' && td.children[0].tagName != 'DIV') {
                    td.children[0].textContent = "";
                    td.children[1].value = "";
                }
            });
        }
    }


    // Add Row
    if (e.target.classList.contains('btnAdd-Row')) {
        //Listen to add button click only
        let addBtn;
        if (e.target.tagName == 'I') {
            addBtn = e.path[1];
        } else {
            addBtn = e.path[0];
        }

        const newRow = `
        <tr>
            <td><span></span></td>
            <td>
                <p></p>
                <textarea class="d-none" name="recent-biz-transaction" id="recent-biz-transaction" cols="30" rows="10"></textarea>
            </td>
            <td>
                <p></p>
                <input class="d-none" type="text" id="biz-date" name="biz-date">
            </td>
            <td>
                <p></p>
                <input class="d-none" type="text" id="biz-period" name="biz-period">
            </td>
            <td>
                <p></p>
                <input class="d-none" type="text" id="service-product" name="service-product">
            </td>
            <td>
                <p></p>
                <input class="d-none" type="text" id="usd-value" name="usd-value">
            </td>
            <td>
                <p></p>
                <input class="d-none" type="text" id="biz-buyer" name="biz-buyer">
            </td>
            <td>
                <p></p>
                <input class="d-none" type="text" id="buyer-contact" name="buyer-contact">
            </td>
            <td>
                <p></p>
                <input class="d-none" type="text" id="buyer-email" name="buyer-email">
            </td>
            <td class="align-content-between">
                <div class="fixed-buttons">
                    <span class="btnEdit-Row" data-toggle="modal" data-target="#referencesInfo" style="padding: 5px; cursor: pointer;" title="Edit this row">
                        <i class="fa btnEdit-Row fa-pencil color-muted m-r-5"></i>
                    </span>
                    <span class="btn btnClear-Row d-none" style="padding: 5px; cursor: pointer;" title="Clear this row">
                        <i class="fa btnClear-Row fa-eraser color-danger"></i>
                    </span>
                    <span class="btn btnDelete-Row" style="padding: 5px; cursor: pointer;" title="Delete this row">
                        <i class="fa btnDelete-Row fa-close color-danger"></i>
                    </span>
                    <span class="btn btnAdd-Row" style="padding: 5px; cursor: pointer;" title="Add new row">
                        <i class="fa btnAdd-Row fa-plus"></i>
                    </span>
                </div>
                </td>
            </tr>`;

        referenceTable.innerHTML += newRow;

        checkToClearBtn('.reference-table', 9);
    }

    // Delete Row
    if (e.target.classList.contains('btnDelete-Row')) {
        // if table row is 4 or 3 in path
        if (e.path[4].tagName == 'TR') e.path[4].remove();
        if (e.path[3].tagName == 'TR') e.path[3].remove();

        checkToClearBtn('.reference-table', 9);
    }

    // Function to remove active-classes
    const removeActiveClass = () => {
        Array.from(referenceTable.children).forEach((row) => {
            if (row.classList.contains('active-class')) {
                row.classList.remove('active-class');
            }
        });
    };

    // Add class for picking
    if (e.target.classList.contains('btnEdit-Row')) {
        e.preventDefault();

        if (e.path[4].tagName == 'TR') {
            removeActiveClass();
            e.path[4].classList.add('active-class');
        }
        if (e.path[3].tagName == 'TR') {
            removeActiveClass();
            e.path[3].classList.add('active-class');
        }

        const activeRow = document.querySelector('.reference-table tbody tr.active-class');

        let idArray = ['modal-recent-biz-transaction', 'modal-biz-date', 'modal-biz-period', 'modal-service-product', 'modal-usd-value',
            'modal-biz-buyer', 'modal-buyer-contact', 'modal-buyer-email'
        ];

        for (let index = 0; index < (activeRow.childElementCount - 2); index++) {
            document.getElementById(idArray[index]).value = activeRow.children[index + 1].children[0].textContent.trim();
        }
    }
});

// CLICK SAVE BTN FOR REFERENCE
reference_saveBtn.addEventListener('click', (e) => {
    e.preventDefault();

    const activeRow = document.querySelector('.reference-table tbody tr.active-class');

    let idArray = ['modal-recent-biz-transaction', 'modal-biz-date', 'modal-biz-period', 'modal-service-product', 'modal-usd-value',
        'modal-biz-buyer', 'modal-buyer-contact', 'modal-buyer-email'
    ];

    for (let i = 0; i < (activeRow.childElementCount - 2); i++) {
        activeRow.children[i + 1].children[0].innerHTML = document.getElementById(idArray[i]).value.trim();
        activeRow.children[i + 1].children[1].value = document.getElementById(idArray[i]).value.trim();
    }
});



// SHAREHOLDING TABLE
const shareholdingTable = document.querySelector('.table-shareholding tbody');
const shareholding_saveBtn = document.querySelector('.saveBtn-shareholdingInfo');

// default row with clearBtn
checkToClearBtn('.table-shareholding', 6);

shareholdingTable.addEventListener('click', (e) => {

    // Clear Row values
    if (e.target.classList.contains('btnClear-Row')) {
        // if table row is 4 or 3 in path
        if (e.path[3].tagName == 'TR') {
            Array.from(e.path[3].children).forEach((td) => {
                if (td.children[0].tagName != 'SPAN' && td.children[0].tagName != 'DIV') {
                    td.children[0].textContent = "";
                    td.children[1].value = "";
                }
            });
        }
        if (e.path[4].tagName == 'TR') {
            Array.from(e.path[4].children).forEach((td) => {
                if (td.children[0].tagName != 'SPAN' && td.children[0].tagName != 'DIV') {
                    td.children[0].textContent = "";
                    td.children[1].value = "";
                }
            });
        }
    }


    // Add Row
    if (e.target.classList.contains('btnAdd-Row')) {
        //Listen to add button click only
        let addBtn;
        if (e.target.tagName == 'I') {
            addBtn = e.path[1];
        } else {
            addBtn = e.path[0];
        }

        const newRow = `
        <tr>
            <td><span></span></td>
            <td>
                <p></p>
                <input class="d-none" type="text" id="shareholder-name" name="shareholder-name">
            </td>
            <td>
                <p></p>
                <input class="d-none" type="text" id="shareholder-dob" name="shareholder-dob">
            </td>
            <td>
                <p></p>
                <input class="d-none" type="text" id="shareholder-nationality" name="shareholder-nationality">
            </td>
            <td>
                <p></p>
                <input class="d-none" type="text" id="shareholder-position" name="shareholder-position">
            </td>
            <td>
                <p></p>
                <input class="d-none" type="text" id="shareholder-percent" name="shareholder-percent">
            </td>
            <td class="align-content-between">
                <div class="fixed-buttons">
                    <span class="btnEdit-Row" data-toggle="modal" data-target="#shareholdingInfo" style="padding: 5px; cursor: pointer;" title="Edit this row">
                        <i class="fa btnEdit-Row fa-pencil color-muted m-r-5"></i>
                    </span>
                    <span class="btn btnClear-Row d-none" style="padding: 5px; cursor: pointer;" title="Clear this row">
                        <i class="fa btnClear-Row fa-eraser color-danger"></i>
                    </span>
                    <span class="btn btnDelete-Row" style="padding: 5px; cursor: pointer;" title="Delete this row">
                        <i class="fa btnDelete-Row fa-close color-danger"></i>
                    </span>
                    <span class="btn btnAdd-Row" style="padding: 5px; cursor: pointer;" title="Add new row">
                        <i class="fa btnAdd-Row fa-plus"></i>
                    </span>
                </div>
            </td>
        </tr>`;

        shareholdingTable.innerHTML += newRow;


        checkToClearBtn('.table-shareholding', 6);

    }

    // Delete Row
    if (e.target.classList.contains('btnDelete-Row')) {
        // if table row is 4 or 3 in path
        if (e.path[4].tagName == 'TR') e.path[4].remove();
        if (e.path[3].tagName == 'TR') e.path[3].remove();

        checkToClearBtn('.table-shareholding', 6);

    }

    // Function to remove active-classes
    const removeActiveClass = () => {
        Array.from(shareholdingTable.children).forEach((row) => {
            if (row.classList.contains('active-class')) {
                row.classList.remove('active-class');
            }
        });
    };

    // Add class for picking
    if (e.target.classList.contains('btnEdit-Row')) {
        e.preventDefault();

        if (e.path[4].tagName == 'TR') {
            removeActiveClass();
            e.path[4].classList.add('active-class');
        }
        if (e.path[3].tagName == 'TR') {
            removeActiveClass();
            e.path[3].classList.add('active-class');
        }

        const activeRow = document.querySelector('.table-shareholding tbody tr.active-class');

        let idArray = ['modal-shareholder-name', 'modal-shareholder-dob', 'modal-shareholder-nationality',
            'modal-shareholder-position', 'modal-shareholder-percent'
        ];


        for (let index = 0; index < (activeRow.childElementCount - 2); index++) {
            document.getElementById(idArray[index]).value = activeRow.children[index + 1].children[0].textContent.trim();
        }
    }
});

// CLICK SAVE BTN FOR REFERENCE
shareholding_saveBtn.addEventListener('click', (e) => {
    e.preventDefault();

    const activeRow = document.querySelector('.table-shareholding tbody tr.active-class');

    let idArray = ['modal-shareholder-name', 'modal-shareholder-dob', 'modal-shareholder-nationality',
        'modal-shareholder-position', 'modal-shareholder-percent'
    ];

    for (let i = 0; i < (activeRow.childElementCount - 2); i++) {
        activeRow.children[i + 1].children[0].innerHTML = document.getElementById(idArray[i]).value.trim();
        activeRow.children[i + 1].children[1].value = document.getElementById(idArray[i]).value.trim();
    }
});


document.getElementById('modal-shareholder-percent').addEventListener('focusin', (e) => {
    // console.log(shareSummation());

    // if (isBtn100()) {
    //     console.log((shareSummation() - fValue) + parseFloat(e.target.value));
    // }
});


// shareholding percentages

function shareSummation() {
    const shareCells = document.querySelectorAll('.table-shareholding tbody tr');
    // const indShares = document.querySelector('.table-shareholding tbody tr.active-class').children[5].children[0].innerHTML;
    // return parseFloat(indShares);

    let num = 0;

    for (let index = 0; index < shareCells.length; index++) {
        num += parseFloat(shareCells[index].children[5].children[0].innerHTML);
    }
    return parseFloat(num);
}

function isBtn100() {
    if (shareSummation() <= 100) {
        return true;
    }
    return false;
}





// CERTIFICATION TABLE
const certificationTable = document.querySelector('.certification-table tbody');
const certification_saveBtn = document.querySelector('.saveBtn-certificationInfo');

// Default single row table display clear button
function checkToClearBtn(tableName, num) {
    if (document.querySelector(tableName + ' tbody tr').nextElementSibling == null) {
        document.querySelector(tableName + ' tbody tr').children[num].children[0].children[1].classList.remove('d-none');
        document.querySelector(tableName + ' tbody tr').children[num].children[0].children[2].classList.add('d-none');
    } else {
        document.querySelector(tableName + ' tbody tr').children[num].children[0].children[2].classList.remove('d-none');
        document.querySelector(tableName + ' tbody tr').children[num].children[0].children[1].classList.add('d-none');
    }
}

// check if only one row is there
checkToClearBtn('.certification-table', 4);

certificationTable.addEventListener('click', (e) => {

    // Clear Row values
    if (e.target.classList.contains('btnClear-Row')) {
        // if table row is 4 or 3 in path
        if (e.path[3].tagName == 'TR') {
            Array.from(e.path[3].children).forEach((td) => {
                if (td.children[0].tagName != 'SPAN' && td.children[0].tagName != 'DIV') {
                    td.children[0].textContent = "";
                    td.children[1].value = "";
                }
            });
        }
        if (e.path[4].tagName == 'TR') {
            Array.from(e.path[4].children).forEach((td) => {
                if (td.children[0].tagName != 'SPAN' && td.children[0].tagName != 'DIV') {
                    td.children[0].textContent = "";
                    td.children[1].value = "";
                }
            });
        }
    }


    // Add Row
    if (e.target.classList.contains('btnAdd-Row')) {
        //Listen to add button click only
        let addBtn;
        if (e.target.tagName == 'I') {
            addBtn = e.path[1];
        } else {
            addBtn = e.path[0];
        }

        const newRow = `
        <tr>
            <td><span></span></td>
            <td>
                <p></p>
                <input class="d-none" type="text" id="certificate-name" name="certificate-name">
            </td>
            <td>
                <p></p>
                <input class="d-none" type="text" id="certificate-number" name="certificate-number">
            </td>
            <td>
                <p></p>
                <input class="d-none" type="text" id="file-name" name="file-name">
            </td>
            <td class="align-content-between">
                <div class="fixed-buttons">
                <span class="btnEdit-Row" data-toggle="modal" data-target="#certificatioInfo" style="padding: 5px; cursor: pointer;" title="Edit this row">
                    <i class="fa btnEdit-Row fa-pencil color-muted m-r-5"></i>
                </span>
                <span class="btn btnClear-Row d-none" style="padding: 5px; cursor: pointer;" title="Clear this row">
                    <i class="fa btnClear-Row fa-eraser color-danger"></i>
                </span>
                <span class="btn btnDelete-Row" style="padding: 5px; cursor: pointer;" title="Delete this row">
                    <i class="fa btnDelete-Row fa-close color-danger"></i>
                </span>
                <span class="btn btnAdd-Row" style="padding: 5px; cursor: pointer;" title="Add new row">
                    <i class="fa btnAdd-Row fa-plus"></i></span>
                </div>
            </td>
        </tr>`;

        certificationTable.innerHTML += newRow;

        checkToClearBtn('.certification-table', 4);
    }

    // Delete Row
    if (e.target.classList.contains('btnDelete-Row')) {
        // if table row is 4 or 3 in path
        if (e.path[4].tagName == 'TR') e.path[4].remove();
        if (e.path[3].tagName == 'TR') e.path[3].remove();

        checkToClearBtn('.certification-table', 4);
    }

    // Function to remove active-classes
    const removeActiveClass = () => {
        Array.from(certificationTable.children).forEach((row) => {
            if (row.classList.contains('active-class')) {
                row.classList.remove('active-class');
            }
        });
    };

    // Add class for picking
    if (e.target.classList.contains('btnEdit-Row')) {
        e.preventDefault();

        if (e.path[4].tagName == 'TR') {
            removeActiveClass();
            e.path[4].classList.add('active-class');
        }
        if (e.path[3].tagName == 'TR') {
            removeActiveClass();
            e.path[3].classList.add('active-class');
        }

        const activeRow = document.querySelector('.certification-table tbody tr.active-class');

        let idArray = ['modal-certificate-name', 'modal-certificate-number', 'modal-file-name'];


        for (let index = 0; index < (activeRow.childElementCount - 2); index++) {
            document.getElementById(idArray[index]).value = activeRow.children[index + 1].children[0].textContent.trim();
        }
    }
});

// CLICK SAVE BTN FOR REFERENCE
certification_saveBtn.addEventListener('click', (e) => {
    e.preventDefault();

    const activeRow = document.querySelector('.certification-table tbody tr.active-class');

    let idArray = ['modal-certificate-name', 'modal-certificate-number', 'modal-file-name'];

    for (let i = 0; i < (activeRow.childElementCount - 2); i++) {
        activeRow.children[i + 1].children[0].innerHTML = document.getElementById(idArray[i]).value.trim();
        activeRow.children[i + 1].children[1].value = document.getElementById(idArray[i]).value.trim();
    }
});



function inputNumber(el) {

    var min = el.attr('min') || false;
    var max = el.attr('max') || false;

    var els = {};

    els.dec = el.prev();
    els.inc = el.next();

    el.each(function () {
        init($(this));
    });

    function init(el) {

        els.dec.on('click', decrement);
        els.inc.on('click', increment);

        function decrement() {
            var value = el[0].value;
            value--;
            if (!min || value >= min) {
                el[0].value = value;
            }
        }

        function increment() {
            var value = el[0].value;
            value++;
            if (!max || value <= max) {
                el[0].value = value++;
            }
        }
    }
}