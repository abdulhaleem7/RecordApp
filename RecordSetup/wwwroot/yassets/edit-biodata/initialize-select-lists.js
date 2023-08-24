var countrySelect = $("#countrySelect");
var stateSelect = $("#stateSelect");
var lgSelect = $("#lgSelect");
var msSelect = $("#msSelect");
var sexSelect = $("#sexSelect");
var religionSelect = $("#religionSelect");
var programSelect = $("#programSelect");
var departmentSelect = $("#departmentSelect");
var collegeSelect = $("#collegeSelect");
var levelSelect = $("#levelSelect");
var basisOfAdmissionSelect = $("#basisOfAdmissionSelect");
var bankSelect = $("#bankSelect");
var paymentPurposeSelect = $("#paymentPurposeSelect");
var collectionTypeSelect = $("#collectionTypeSelect");
var allProgramsSelect = $(".allProgramsSelect");
var sessionSelect = $(".sessionSelect");

var sessionByNamesSelect = $(".sessionByNamesSelect");
var oLevelSubjectsSelect = $(".oLevelSubjectsSelect");
var oLevelSubjectGradesSelect = $(".oLevelSubjectGradesSelect");
var oLevelExamTypesSelect = $(".oLevelExamTypesSelect");
var employmentTypesSelect = $(".employmentTypesSelect");
var employmentStatusesSelect = $(".employmentStatusesSelect");
var cadreCategoriesSelect = $(".cadreCategoriesSelect");
var cadresSelect = $(".cadresSelect");
var workFlowStateSelect = $("#workFlowStateSelect");
var sessionSemesterSelect = $("#sessionSemesterSelect");
var studentSelect = $("#studentSelect");
var hostelSelect = $(".hostelSelect");
var roomTypeSelect = $(".roomTypeSelect");
var amenitySelect = $(".amenitySelect");
var hostelRoomTypeSelect = $(".hostelRoomTypeSelect");
var payrollSelect = $(".payrollSelect");
var payrollItemSelect = $(".payrollItemSelect");
var staffSelect = $(".staffSelect");
var semesterSelect = $(".semesterSelect");
var semesterByNamesSelect = $(".semesterByNamesSelect");
var staffDisciplineStatusSelect = $(".staffDisciplineStatusSelect");
var staffExitTypeSelect = $(".staffExitTypeSelect");
var staffLeaveTypeSelect = $(".staffLeaveTypeSelect");
var bloodGroupSelect = $(".bloodGroupSelect");
var appointmentTypeSelect = $(".appointmentTypeSelect");
var bondAndSponsorshipSelect = $(".bondAndSponsorshipSelect");
var studentFullName = $(".studentFullName");
var collectionAccountSelect = $(".collectionAccountSelect");
var workFlowSelect = $(".workFlowSelect");

var customeraccountSelect = $(".customeraccountSelect");

var accountSelect = $(".accountSelect");
var collectionSelect = $(".collectionSelect");
var resultWorkFlowStateSelect = $("#resultWorkFlowStateSelect");
var gradingSystemSelect = $(".gradingSystemSelect");

msSelect.select2({
    placeholder: "Are they married?",
    width: "100%",
    allowClear: true
});

sexSelect.select2({
    placeholder: "What is their gender?",
    width: "100%",
    allowClear: true
});

religionSelect.select2({
    placeholder: "What is your religion?",
    width: "100%",
    allowClear: true
});

function initializeSelectWithPlaceholder(select, placeHolder)
{
    select.select2({
        placeholder: placeHolder,
        allowClear: true,
        width: "100%"
    });
}

function resetSelect(select)
{
    select.select2("destroy");
    select.empty();
    select.prepend("<option></option>");
}

function mapResults(data)
{
    var results = [];
    for (var i = 0; i < data.length; i++)
    {
        var result = data[i];
        results.push({
            id: result.id,
            text: result.name
        });
    }
    return {
        results
    };
}

function mapNameToName(data)
{
    var results = [];
    for (var i = 0; i < data.length; i++)
    {
        var result = data[i];
        results.push({
            id: result.name,
            text: result.name
        });
    }
    return {
        results
    };
}

function mapAllProgramsResults(data)
{
    var results = [];
    for (var i = 0; i < data.length; i++)
    {
        var result = data[i];

        var collegeIndex = selectArrayIndexOf(results, result.collegeCode);
        if (collegeIndex === -1)
        {
            results.push({
                text: result.collegeCode,
                children: [
                    {
                        text: result.departmentCode,
                        children: [
                            {
                                id: result.id,
                                text: result.name
                            }
                        ]
                    }
                ]
            });
            continue;
        }
        var departmentIndex = selectArrayIndexOf(results[collegeIndex].children, result.departmentCode);
        if (departmentIndex === -1)
        {
            results[collegeIndex].children.push({
                text: result.departmentCode,
                children: [
                    {
                        id: result.id,
                        text: result.name
                    }
                ]
            });
            continue;
        }

        results[collegeIndex].children[departmentIndex].children.push({
            id: result.id,
            text: result.name
        });
    }


    return {
        results
    };
}

function selectArrayIndexOf(array, text)
{
    for (var i in array)
    {
        if (array.hasOwnProperty(i) && array[i].text === text)
        {
            return i;
        }
    }

    return -1;
}

function setupLocalGovernmentSelects(lgUrl, statesUrl, nationsUrl)
{
    function setupStateSelectListeners()
    {
        stateSelect.on("select2:select",
            function(e2)
            {
                var selected = e2.params.data;
                resetSelect(lgSelect);
                lgSelect.select2({
                    placeholder: "Choose a local government",
                    allowClear: true,
                    width: "100%",
                    ajax: {
                        dataType: "json",
                        url: lgUrl + "?stateId=" + selected.id,
                        processResults: function(data)
                        {
                            return mapResults(data);
                        }
                    }
                });
            });

        stateSelect.on("select2:deselect",
            function(e2)
            {
                resetSelect(lgSelect);
                initializeSelectWithPlaceholder(lgSelect, "Choose a local government");
            });
    }

    countrySelect.on("select2:select",
        function(e)
        {
            var selected = e.params.data;
            resetSelect(stateSelect);
            stateSelect.select2({
                placeholder: "Choose a state",
                allowClear: true,
                width: "100%",
                ajax: {
                    dataType: "json",
                    url: statesUrl + "?nationId=" + selected.id,
                    processResults: function(data)
                    {
                        return mapResults(data);
                    }
                }
            });
            setupStateSelectListeners();
        });

    countrySelect.on("select2:deselect",
        function(e)
        {
            resetSelect(stateSelect);
            initializeSelectWithPlaceholder(stateSelect, "Choose a state");
        });


    countrySelect.select2({
        placeholder: "Choose a country",
        allowClear: true,
        width: "100%",
        ajax: {
            dataType: "json",
            url: nationsUrl,
            processResults: function(data)
            {
                return mapResults(data);
            }
        }
    });

    initializeSelectWithPlaceholder(stateSelect, "Choose a state");
    initializeSelectWithPlaceholder(lgSelect, "Choose a local government");
}


function setupProgramSelects(programUrl,
                             departmentUrl,
                             collegeUrl,
                             collegeSelector = null,
                             departmentSelector = null,
                             parent = null)
{
    var internalCollegeSelect = collegeSelect;
    var internalDepartmentSelect = departmentSelect;

    if (collegeSelector !== null)
    {
        if (parent !== null)
        {
            internalCollegeSelect = parent.find(collegeSelector);
        }
        else
        {
            internalCollegeSelect = $(collegeSelector);
        }
    }

    if (departmentSelector !== null)
    {
        if (parent !== null)
        {
            internalDepartmentSelect = parent.find(departmentSelector);
        }
        else
        {
            internalDepartmentSelect = $(departmentSelector);
        }
    }

    function setupDepartmentSelectListeners()
    {
        internalDepartmentSelect.on("select2:select",
            function(e2)
            {
                if (!e2.params) {
                    return;
                }
                var selected = e2.params.data;
                resetSelect(programSelect);
                programSelect.select2({
                    placeholder: "Choose a program",
                    allowClear: true,
                    width: "100%",
                    ajax: {
                        dataType: "json",
                        url: programUrl + "?departmentId=" + selected.id,
                        processResults: function(data)
                        {
                            return mapResults(data);
                        }
                    }
                });
            });

        internalDepartmentSelect.on("select2:deselect",
            function(e2)
            {
                resetSelect(programSelect);
                initializeSelectWithPlaceholder(programSelect, "Choose a program");
            });
    }

    internalCollegeSelect.on("select2:select",
        function(e)
        {
            if (!e.params)
            {
                return;
            }
            var selected = e.params.data;
            resetSelect(internalDepartmentSelect);
            internalDepartmentSelect.select2({
                placeholder: "Choose a department",
                allowClear: true,
                width: "100%",
                ajax: {
                    dataType: "json",
                    url: departmentUrl + "?collegeId=" + selected.id,
                    processResults: function(data)
                    {
                        return mapResults(data);
                    }
                }
            });
            setupDepartmentSelectListeners();
        });

    internalCollegeSelect.on("select2:deselect",
        function(e)
        {
            resetSelect(internalDepartmentSelect);
            initializeSelectWithPlaceholder(internalDepartmentSelect, "Choose a department");
        });


    internalCollegeSelect.select2({
        placeholder: "Choose a college",
        allowClear: true,
        width: "100%",
        ajax: {
            dataType: "json",
            url: collegeUrl,
            processResults: function(data)
            {
                return mapResults(data);
            }
        }
    });

    initializeSelectWithPlaceholder(internalDepartmentSelect, "Choose a department");
    initializeSelectWithPlaceholder(programSelect, "Choose a program");
}


function setupBasisOfAdmissionSelect(url, placeholder = "Basis of admission")
{
    setupBasicSelect(basisOfAdmissionSelect, url, placeholder);
}

function setupBankSelect(url, placeholder = "Select bank")
{
    setupBasicSelect(bankSelect, url, placeholder);
}

function setupOLevelSubjectsSelect(url, placeholder = "Select subject")
{
    setupBasicSelect(oLevelSubjectsSelect, url, placeholder);
}

function setupOLevelSubjectGradesSelect(url, placeholder = "Select grade")
{
    setupBasicSelect(oLevelSubjectGradesSelect, url, placeholder);
}

function setupOLevelExamTypesSelect(url, placeholder = "Select exam type")
{
    setupBasicSelect(oLevelExamTypesSelect, url, placeholder);
}



function setupWorkFlowStateSelect(url, placeholder = "Select workFlow State") {
    setupBasicSelect(workFlowStateSelect, url, placeholder);
}

function setupStudentSelect(url, placeholder = "Select Student") {
    setupBasicSelect(studentSelect, url, placeholder);
}

function setupHostelSelect(url, placeholder = "Select hostel") {
    setupBasicSelect(hostelSelect, url, placeholder);
}


function setupAmenitySelect(url, placeholder = "Select amenity") {
    setupBasicSelect(amenitySelect, url, placeholder);
}

function setupRoomTypeSelect(url, placeholder = "Select room type") {
    setupBasicSelect(roomTypeSelect, url, placeholder);
}

function setupHostelRoomTypeSelect(url, placeholder = "Select hostel room type") {
    setupBasicSelect(hostelRoomTypeSelect, url, placeholder);
}

function setupPayrollSelect(url, placeholder = "Select payroll") {
    setupBasicSelect(payrollSelect, url, placeholder);
}

function setupPayrollItemSelect(url, placeholder = "Select payroll Item") {
    setupBasicSelect(payrollItemSelect, url, placeholder);
}

function setupStaffSelect(url, placeholder = "Select staff") {
    setupBasicSelect(staffSelect, url, placeholder);
}

function setupCollectionAccountSelect(url, placeholder = "Select Bank Account") {
    setupBasicSelect(collectionAccountSelect, url, placeholder);
}

//function setupstaffDisciplineStatusSelect(url, placeholder = "Select staff Discipline Status") {
//    setupBasicSelect(staffDisciplineStatusSelect, url, placeholder);
//}

function setupCustomerAccountSelect(url, placeholder = "Select customer account") {
    setupBasicSelect(customeraccountSelect, url, placeholder);
}

function setupAccountSelect(url, placeholder = "Select account") {
    setupBasicSelect(accountSelect, url, placeholder);
}

function setupCollectionSelect(url, placeholder = "Select collection") {
    setupBasicSelect(collectionSelect, url, placeholder);
}


function setupStaffLeaveTypeSelect(url, placeholder = "Select staff leave Type") {
    setupBasicSelect(staffLeaveTypeSelect, url, placeholder);
}

function setupStaffExitSelect(url, placeholder = "Select staff Exit") {
    setupBasicSelect(staffExitTypeSelect, url, placeholder);
}

function setupBloodGroupSelect(url, placeholder = "Select bloodGroup") {
    setupBasicSelect(bloodGroupSelect, url, placeholder);
}

function setupResultWorkFlowStateSelect(url, placeholder = "Select workFlow States") {
    setupBasicSelect(resultWorkFlowStateSelect, url, placeholder);
}


function setupWorkFlowSelect(url, placeholder = "Select workFlow State") {
    setupBasicSelect(workFlowSelect, url, placeholder);
}

function setupGradingSystemSelect(url, placeholder = "Select grading System") {
    setupBasicSelect(gradingSystemSelect, url, placeholder);
}

function setupEmploymentTypesSelect(url, selector = null, parent = null, placeholder = "Select employment type")
{
    var internalEmploymentTypesSelect = employmentTypesSelect;

    if (selector !== null) {
        if (parent !== null) {
            internalEmploymentTypesSelect = parent.find(selector);
        }
        else {
            internalEmploymentTypesSelect = $(selector);
        }
    }
    setupBasicSelect(internalEmploymentTypesSelect, url, placeholder);
}



function setupStaffDisciplineStatusSelect(url, selector = null, parent = null, placeholder = "Select staff discipline Status") {
    var internalstaffDisciplineStatusSelect = staffDisciplineStatusSelect;

    if (selector !== null) {
        if (parent !== null) {
            internalstaffDisciplineStatusSelect = parent.find(selector);
        }
        else {
            internalstaffDisciplineStatusSelect = $(selector);
        }
    }
    setupBasicSelect(internalstaffDisciplineStatusSelect, url, placeholder);
}



function setupCadreCategoriesSelect(url, placeholder = "Select cadre category")
{
    setupBasicSelect(cadreCategoriesSelect, url, placeholder);
}

function setupCadresSelect(url, selector = null, parent = null, placeholder = "Select cadre")
{
    var internalCadresSelect = cadresSelect;

    if (selector !== null)
    {
        if (parent !== null)
        {
            internalCadresSelect = parent.find(selector);
        }
        else
        {
            internalCadresSelect = $(selector);
        }
    }
    setupBasicSelect(internalCadresSelect, url, placeholder);
}

function setupEmploymentStatusesSelect(url, placeholder = "Select employment status")
{
    setupBasicSelect(employmentStatusesSelect, url, placeholder);
}


function setupLevelSelect(url, placeholder = "Select level")
{
    setupBasicSelect(levelSelect, url, placeholder);
}


function setupSemesterSelect(url, placeholder = "Select semester")
{
    setupBasicSelect(semesterSelect, url, placeholder);
}
function setupSessionSemesterSelect(url, placeholder = "Select session semester") {
    setupBasicSelect(semesterSelect, url, placeholder);
}



function setupSemesterByNamesSelect(url, placeholder = "Select semester")
{
    semesterByNamesSelect.select2({
        placeholder: placeholder,
        allowClear: true,
        width: "100%",
        ajax: {
            dataType: "json",
            url: url,
            processResults: function (data) {
                return mapNameToName(data);
            }
        }
    });
}

function setupSessionSelect(url, placeholder = "Select session")
{
    setupBasicSelect(sessionSelect, url, placeholder);
}

function setupSessionByNamesSelect(url, placeholder = "Select session")
{
    sessionByNamesSelect.select2({
        placeholder: placeholder,
        allowClear: true,
        width: "100%",
        ajax: {
            dataType: "json",
            url: url,
            processResults: function (data) {
                return mapNameToName(data);
            }
        }
    });
}


function setupPaymentPurposeSelect(url, placeholder = "Select payment purpose")
{
    setupBasicSelect(paymentPurposeSelect, url, placeholder);
}


function setupCollectionTypeSelect(url, placeholder = "Select collection type")
{
    setupBasicSelect(collectionTypeSelect, url, placeholder);
}

function setupAllProgramsSelect(url, placeholder = "Select program")
{
    allProgramsSelect.select2({
        placeholder: placeholder,
        allowClear: true,
        width: "100%",
        ajax: {
            dataType: "json",
            url: url,
            processResults: function(data)
            {
                return mapAllProgramsResults(data);
            }
        }
    });
}

function setupBasicSelect(select, url, placeholder)
{
    select.select2({
        placeholder: placeholder,
        allowClear: true,
        width: "100%",
        ajax: {
            dataType: "json",
            url: url,
            processResults: function(data)
            {
                return mapResults(data);
            }
        }
    });

   
}



function setupStaffBondAndSponsorshipTypeSelect(url, selector = null, parent = null, placeholder = "Select type") {
    var internalstaffBondAndSponsorshipTypeSelect = bondAndSponsorshipSelect;

    if (selector !== null) {
        if (parent !== null) {
            internalstaffBondAndSponsorshipTypeSelect = parent.find(selector);
        }
        else {
            internalstaffBondAndSponsorshipTypeSelect = $(selector);
        }
    }
    setupBasicSelect(internalstaffBondAndSponsorshipTypeSelect, url, placeholder);
}


function setupAppointmentTypeSelect(url, selector = null, parent = null, placeholder = "Select type") {
    var internalappointmentTypeSelect = appointmentTypeSelect;

    if (selector !== null) {
        if (parent !== null) {
            internalappointmentTypeSelect = parent.find(selector);
        }
        else {
            internalappointmentTypeSelect = $(selector);
        }
    }
    setupBasicSelect(internalappointmentTypeSelect, url, placeholder);
}