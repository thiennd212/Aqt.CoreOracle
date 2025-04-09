$(function () {
    var l = abp.localization.getResource('CoreOracle');
    var positionService = aqt.coreOracle.application.positions.position;

    // Use the permissions object passed from the view
    // var permissions = {
    //     update: 'OrganizationManagement.Positions.Update',
    //     delete: 'OrganizationManagement.Positions.Delete',
    //     create: 'OrganizationManagement.Positions.Create'
    // };
    // Ensure pagePermissions is defined globally (from the cshtml)
    var permissions = pagePermissions || {}; // Fallback to empty object if not defined

    var createModal = new abp.ModalManager(abp.appPath + 'Positions/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Positions/EditModal'); // Restore initialization with viewUrl

    var dataTable = $('#PositionsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false, // Disable default search box if not needed
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(positionService.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible: abp.auth.isGranted(permissions.Update),
                                    action: function (data) {
                                        // Use the simpler open call, ModalManager will create query string
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible: abp.auth.isGranted(permissions.Delete), // Use property from passed object
                                    confirmMessage: function (data) {
                                        return l('PositionDeletionConfirmationMessage', data.record.name);
                                    },
                                    action: function (data) {
                                        positionService.delete(data.record.id)
                                            .then(function() {
                                                abp.notify.info(l('SuccessfullyDeleted'));
                                                dataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                },
                {
                    title: l('Position:Name'),
                    data: "name"
                },
                {
                    title: l('Position:Code'),
                    data: "code"
                },
                {
                    title: l('Position:Description'),
                    data: "description"
                }
            ]
        })
    );

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    // Check create permission when binding the button click event
    if (abp.auth.isGranted(permissions.Create)) { // Use property from passed object
        $('#NewPositionButton').click(function (e) {
            e.preventDefault();
            createModal.open();
        });
    } else {
        $('#NewPositionButton').hide();
    }
}); 