function setRepeaterList(repeater, list)
{
    repeater.setList(list);
    // find selects that match the name and set the values

    // select convention
    // name: { type: "select", text: "text", value: "value" }

    // checkbox convention
    // name: { type: "checkbox", checked: value }
    for (let i in list)
    {
        if (Object.prototype.hasOwnProperty.call(list, i))
        {
            const item = list[i];
            for (let name in item)
            {
                if (Object.prototype.hasOwnProperty.call(item, name))
                {
                    const prop = item[name];

                    if (prop === null || prop === undefined)
                    {
                        continue;
                    }

                    let elementName;
                    let element;
                    switch (prop.type)
                    {
                        case "select":
                            elementName = `[${i}][${name}]`;
                            element = repeater.find(`[name="${elementName}"]`);
                            handleRepeaterSelect(element, prop);
                            break;
                        case "checkbox":
                            elementName = `[${i}][${name}][]`;
                            element = repeater.find(`[name="${elementName}"]`);
                            handleRepeaterCheckbox(element, prop);
                            break;
                    }
                }
            }
        }
    }


    function handleRepeaterSelect(element, prop)
    {
        if (prop.text !== null)
        {
            var option = new Option(prop.text, prop.value, true, true);
            element.append(option).trigger("change").trigger("select2:select");
        }
    }

    function handleRepeaterCheckbox(element, prop)
    {
        element.prop("checked", prop.checked);
    }
}