"use strict"

const getCustomDaysContainer = () => document.getElementById("FormCustomDate");
const getDatesLabel = () => document.getElementById("DatesLabel");

const createHiddenInput = (name, value) => {
    const form = document.getElementById("EditForm");

    const input = document.createElement('input');
    input.name = name;
    input.type = "hidden";
    input.value = value;

    form.appendChild(input);
}

const addCustomDayInput = (customDayContainer) => {
    const dateInput = document.createElement('input');
    dateInput.setAttribute('class', 'text-box');
    dateInput.setAttribute('class', 'single-line');
    dateInput.setAttribute('data-val', 'true');
    dateInput.setAttribute('type', 'date');

    dateInput.setAttribute('name', 'CustomDays');
    let today = new Date();
    let dd = String(today.getDate()).padStart(2, '0');
    let mm = String(today.getMonth() + 1).padStart(2, '0');
    let yyyy = today.getFullYear();
    today = yyyy + '-' + mm + '-' + dd;
    dateInput.setAttribute('value', today.toString());
    customDayContainer.appendChild(dateInput);

    if (sessionStorage.getItem("requestType") === "OnRemoteWork") {
        const planInput = document.createElement('input');
        planInput.setAttribute('name', 'RemotePlans');
        planInput.style.width = "50%";

        const planLabel = document.createElement('span');
        planLabel.textContent = "  План удалённой работы: "
        customDayContainer.appendChild(planLabel);
        customDayContainer.appendChild(planInput);
    }
}

const addCustomDate = (parentContainer) => {
    const firstDayContainer = document.createElement('div');
    firstDayContainer.setAttribute("class", "CustomDayContainer");
    const dayPrefix = document.createElement('span');
    dayPrefix.setAttribute('class', 'before');
    dayPrefix.textContent = "➕";

    const dayLabel = document.createElement('span');
    dayLabel.textContent = " Добавить день";

    firstDayContainer.appendChild(dayPrefix);
    firstDayContainer.appendChild(dayLabel);

    const requestStatus = document.getElementById("Status")?.value;
    if (requestStatus === undefined || requestStatus === "New" || requestStatus === "Canceled")
    {
        dayPrefix.addEventListener('click', () => {
            if (firstDayContainer.children[0].classList.contains('before-down')) {
                while (firstDayContainer.firstElementChild) {
                    firstDayContainer.removeChild(firstDayContainer.firstElementChild);
                }
                firstDayContainer.remove();
            } else {
                firstDayContainer.children[1].textContent = " Выберите дату: ";
                addCustomDayInput(firstDayContainer);
                addCustomDate(parentContainer);
                firstDayContainer.children[0].classList.toggle("before-down");
            }
        });
    }

    parentContainer.appendChild(firstDayContainer);
}

const drawCustomDays = (labelText) => {
    const customDaysContainer = getCustomDaysContainer();
    
    if (customDaysContainer.firstElementChild === null || customDaysContainer.firstElementChild?.tagName === "DIV") {
        const label = document.createElement('span');
        label.setAttribute('class', 'control-label');
        label.textContent = labelText;
        customDaysContainer.prepend(label);
        addCustomDate(customDaysContainer);
    } else {
        if (customDaysContainer.lastElementChild.firstElementChild.classList.contains("before-down"))
            addCustomDate(customDaysContainer);
    }
}

const hideCustomDays = () => {
    const customDaysContainer = getCustomDaysContainer();

    while (customDaysContainer.firstChild) {
        customDaysContainer.removeChild(customDaysContainer.firstChild);
    }
}


const updateRequestType = (requestType) => {
    const requestFormComment = document.getElementById("Comment");
    const requestFormReplacer = document.getElementById("Replacer");
    const requestFormApprover = document.getElementById("Approver");
    const requestFormVacationType = document.getElementById("VacationType");
    const requestFormDateFrom = document.getElementById("DateFrom");
    const requestFormDateTo = document.getElementById("DateTo");
    const requestFormIsShifting = document.getElementById("IsShifting");
    const datesLabel = getDatesLabel();
    const requestStatus = document.getElementById("Status")?.value;
    if (requestStatus === undefined)
        hideCustomDays();

    switch (requestType) {
        case "OnVacation":
            datesLabel.textContent = "Даты отпуска:";
            requestFormComment.disabled = false;
            requestFormReplacer.disabled = false;
            requestFormApprover.disabled = false;
            requestFormVacationType.disabled = false;
            requestFormDateFrom.disabled = false;
            requestFormDateTo.disabled = false;
            requestFormIsShifting.disabled = false;
            hideCustomDays();
            break;
        case "OnDayOffInsteadVacation":
            requestFormComment.disabled = false;
            requestFormReplacer.disabled = false;
            requestFormApprover.disabled = false;
            requestFormVacationType.disabled = true;
            requestFormDateFrom.disabled = true;
            requestFormDateTo.disabled = true;
            requestFormIsShifting.disabled = true;
            drawCustomDays("Выходные:");
            break;
        case "OnHoliday":
            requestFormComment.disabled = false;
            requestFormReplacer.disabled = false;
            requestFormApprover.disabled = false;
            requestFormVacationType.disabled = true;
            requestFormDateFrom.disabled = true;
            requestFormDateTo.disabled = true;
            requestFormIsShifting.disabled = true;
            drawCustomDays("Дни отгула:");
            break;
        case "OnDayOffInsteadOverworking":
            datesLabel.textContent = "Даты отработки:";
            requestFormComment.disabled = false;
            requestFormReplacer.disabled = false;
            requestFormApprover.disabled = false;
            requestFormVacationType.disabled = true;
            requestFormDateFrom.disabled = false;
            requestFormDateTo.disabled = false;
            requestFormIsShifting.disabled = true;
            drawCustomDays("Выходные:");
            break;
        case "OnRemoteWork":
            requestFormComment.disabled = false;
            requestFormReplacer.disabled = true;
            requestFormApprover.disabled = false;
            requestFormVacationType.disabled = true;
            requestFormDateFrom.disabled = true;
            requestFormDateTo.disabled = true;
            requestFormIsShifting.disabled = true;
            drawCustomDays("Дни удалённой работы:");
            break;
        default:
            console.log("Wrong request type!");
            break;
    }
}

$(document).ready(() => {
    const requestTypeContainer = document.getElementById("Type");
    const customDays = document.querySelectorAll(".CustomDayContainer");
    const statusInput = document.getElementById("Status");
    
    if (statusInput !== null && (statusInput.value === "New" || statusInput.value === "Canceled"))
    for (let i = 0; i < customDays.length; ++i)
        customDays[i].firstElementChild.addEventListener('click', () => {
            while (customDays[i].firstElementChild) {
                customDays[i].removeChild(customDays[i].firstElementChild);
            }
            customDays[i].remove();
        });
    
    requestTypeContainer.addEventListener('change', (event) => {
        updateRequestType(event.target.value);
        sessionStorage.setItem("requestType", event.target.value);
    });

    sessionStorage.setItem("requestType", requestTypeContainer.value);
    updateRequestType(document.getElementById("Type").value);
    
    
    if (statusInput !== null && statusInput.value !== "New" && statusInput.value !== "Canceled") {
        const fieldInputs = document.querySelectorAll(".form-control");
        const dateInputs = document.querySelectorAll(".single-line");
        const customDays = document.querySelectorAll(".CustomDayContainer");
        

        for (let i = 0; i < fieldInputs.length; ++i)
            fieldInputs[i].disabled = true;

        for (let i = 0; i < dateInputs.length; ++i)
            dateInputs[i].disabled = true;

        document.getElementById("SaveButton").remove();
        if (customDays.length !== 0) {
            customDays[customDays.length - 1].remove();
        }

            document.getElementById("IsShifting").disabled = true;

        const replacers = document.getElementById("Replacer");
        for (let i = 0; i < replacers.children.length; ++i)
            if (replacers.children[i].selected === true) {
                createHiddenInput("Replacer", replacers.children[i].value);
                break;
            }

        const approvers = document.getElementById("Approver");
        for (let i = 0; i < approvers.children.length; ++i)
            if (approvers.children[i].selected === true) {
                createHiddenInput("Approver", approvers.children[i].value);
                break;
            }

        const vacationType = document.getElementById("VacationType");
        for (let i = 0; i < vacationType.children.length; ++i)
            if (vacationType.children[i].selected === true) {
                createHiddenInput("VacationType", vacationType.children[i].value);
                break;
            }
    }
});
