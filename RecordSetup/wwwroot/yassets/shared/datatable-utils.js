function setupDatatableSearchFields(datatable)
{
    const datatableSearchElement = $(".datatable-search");
    datatableSearchElement.on("change",
        function()
        {
            datatable.search(this.value, this.name);
        });
}