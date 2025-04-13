$(function () {
    var l = abp.localization.getResource('CoreOracle');
    var provinceService = aqt.coreOracle.application.provinces.province;
    var dataTable = null; // Define globally

    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'Provinces/CreateModal',
        modalClass: 'provinceCreateModal'
    });

    var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'Provinces/EditModal',
        modalClass: 'provinceEditModal'
    });

    var getFilters = function() {
        return {
            filter: $('#SearchFilter').val(),
            countryId: $('#CountryIdFilter').val() || null
        };
    }

    function initializeDataTable() {
       if (dataTable) {
            dataTable.destroy(); // Destroy existing table if it exists
       }
       dataTable = $('#ProvincesTable').DataTable(
            abp.libs.datatables.normalizeConfiguration({
                serverSide: true,
                paging: true,
                order: [[2, "asc"]], // Default sort by Code
                searching: false,
                scrollX: true,
                ajax: abp.libs.datatables.createAjax(provinceService.getList, getFilters),
                columnDefs: [
                    {
                        title: l('Actions'),
                        rowAction: {
                            items: [
                                {
                                    text: l('Edit'),
                                    icon: "fa fa-pencil-alt",
                                    visible: abp.auth.isGranted('CoreOracle.Provinces.Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    icon: "fa fa-trash",
                                    visible: abp.auth.isGranted('CoreOracle.Provinces.Delete'),
                                    confirmMessage: function (data) {
                                        return l('AreYouSureToDeleteProvince',
                                            data.record.name || data.record.code);
                                    },
                                    action: function (data) {
                                        provinceService.delete(data.record.id)
                                            .then(function () {
                                                abp.notify.success(l('SuccessfullyDeleted'));
                                                dataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                        }
                    },
                    {
                        title: l('ProvinceCountry'),
                        data: "countryName",
                        orderable: true
                    },
                    {
                        title: l('ProvinceCode'),
                        data: "code",
                        orderable: true
                    },
                    {
                        title: l('ProvinceName'),
                        data: "name",
                        orderable: true
                    }
                ]
            })
        );
    }
    
    initializeDataTable(); // Initialize on page load

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewProvinceButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

    $('#SearchButton').click(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    // Reload table when filter inputs change or Enter is pressed
    $('#SearchFilter').on('keypress', function(e) {
        if(e.which === 13) { // Enter key
            dataTable.ajax.reload();
        }
    });

    $('#CountryIdFilter').change(function() {
        dataTable.ajax.reload();
    });
}); 