$(function () {
    var l = abp.localization.getResource('CoreOracle');
    var dataTable = $('#CategoryTypesTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(aqt.coreOracle.categories.categoryType.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible: abp.auth.isGranted('CoreOracle.CategoryTypes.Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible: abp.auth.isGranted('CoreOracle.CategoryTypes.Delete'),
                                    confirmMessage: function (data) {
                                        return l('CategoryTypeDeletionConfirmationMessage', data.record.name);
                                    },
                                    action: function (data) {
                                        aqt.coreOracle.categories.categoryType
                                            .delete(data.record.id)
                                            .then(function () {
                                                abp.notify.info(l('SuccessfullyDeleted'));
                                                dataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                },
                {
                    title: l('Code'),
                    data: "code"
                },
                {
                    title: l('Name'),
                    data: "name"
                },
                {
                    title: l('Description'),
                    data: "description"
                },
                {
                    title: l('IsActive'),
                    data: "isActive",
                    render: function (data) {
                        return data ? '<i class="fa fa-check text-success"></i>' : '<i class="fa fa-times text-danger"></i>';
                    }
                },
                {
                    title: l('AllowMultipleSelect'),
                    data: "allowMultipleSelect",
                    render: function (data) {
                        return data ? '<i class="fa fa-check text-success"></i>' : '<i class="fa fa-times text-danger"></i>';
                    }
                }
            ]
        })
    );

    var createModal = new abp.ModalManager(abp.appPath + 'Categories/CategoryTypes/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Categories/CategoryTypes/EditModal');

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewCategoryTypeButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
}); 