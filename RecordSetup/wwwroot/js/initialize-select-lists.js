var regionSelect = $(".regionSelect");
var subRegionRecordSelect = $(".subRegionRecordSelect");
var subRegionRecordTableSelect = $(".subRegionRecordTableSelect");
var tableSchemaSelect = $(".tableSchemaSelect");

function initializeSelectWithPlaceholder(select, placeHolder)
{
    select.select2({

        theme: 'bootstrap4'
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



function setupRegionSelect(url, placeholder = "Select region") {
    setupBasicSelect(regionSelect, url, placeholder);
}

function setupSubRegionRecordSelect(url, placeholder = "Select subRegion Record") {
    setupBasicSelect(subRegionRecordSelect, url, placeholder);
}

function setupSubRegionRecordTableSelect(url, placeholder = "Select Customer") {
    setupBasicSelect(subRegionRecordTableSelect, url, placeholder);
}

function setupTableSchemaSelect(url, placeholder = "Select engine status") {
    setupBasicSelect(tableSchemaSelect, url, placeholder);
}




function setupBasicSelect(select, url, placeholder)
{

    select.select2({
        theme: 'bootstrap5',
        allowClear: true,
        width: "100%",
        placeholder: placeholder,
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




