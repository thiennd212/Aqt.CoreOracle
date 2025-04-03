$(function () {
    var l = abp.localization.getResource('CoreOracle');
    var dataTable = $('#[EntityName]sTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(
                aqt.coreOracle.moduleName.entityName.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible: abp.auth.isGranted('[ModuleName].[EntityName]s.Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible: abp.auth.isGranted('[ModuleName].[EntityName]s.Delete'),
                                    confirmMessage: function (data) {
                                        return l(
                                            'ItemDeletionConfirmationMessage',
                                            data.record.name
                                        );
                                    },
                                    action: function (data) {
                                        aqt.coreOracle.moduleName.entityName
                                            .delete(data.record.id)
                                            .then(function () {
                                                abp.notify.info(
                                                    l('SuccessfullyDeleted')
                                                );
                                                dataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                },
                {
                    title: l('[EntityName]Code'),
                    data: "code"
                },
                {
                    title: l('[EntityName]Name'),
                    data: "name"
                },
                {
                    title: l('DisplayOrder'),
                    data: "displayOrder"
                },
                {
                    title: l('IsActive'),
                    data: "isActive",
                    render: function (data) {
                        return data ? '<i class="fas fa-check text-success"></i>' : '<i class="fas fa-times text-danger"></i>';
                    }
                }
            ]
        })
    );

    var createModal = new abp.ModalManager(abp.appPath + '[ModuleName]/[EntityName]s/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + '[ModuleName]/[EntityName]s/EditModal');

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#New[EntityName]Button').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
}); 