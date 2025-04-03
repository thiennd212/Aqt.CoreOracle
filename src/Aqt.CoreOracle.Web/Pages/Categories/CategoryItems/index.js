$(function () {
    var l = abp.localization.getResource('CoreOracle');
    var dataTable = $('#CategoryItemsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[5, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(aqt.coreOracle.categories.categoryItem.getList, function() {
                return {
                    categoryTypeId: $("#CategoryTypeFilter").val(),
                    parentId: $("#ParentFilter").val(),
                    isActive: $("#IsActiveFilter").val()
                }
            }),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible: abp.auth.isGranted('CoreOracle.CategoryItems.Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible: abp.auth.isGranted('CoreOracle.CategoryItems.Delete'),
                                    confirmMessage: function (data) {
                                        return l('CategoryItemDeletionConfirmationMessage', data.record.name);
                                    },
                                    action: function (data) {
                                        abp.ui.setBusy();
                                        aqt.coreOracle.categories.categoryItem
                                            .delete(data.record.id)
                                            .then(function () {
                                                abp.notify.success(l('SuccessfullyDeleted'));
                                                dataTable.ajax.reload();
                                            })
                                            .catch(function(error) {
                                                abp.notify.error(error.message || l('ErrorWhileDeleting'));
                                            })
                                            .finally(function() {
                                                abp.ui.clearBusy();
                                            });
                                    }
                                }
                            ]
                    }
                },
                {
                    title: l('CategoryType'),
                    data: "categoryTypeName"
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
                    title: l('Parent'),
                    data: "parentName",
                    render: function(data) {
                        return data || l('None');
                    }
                },
                {
                    title: l('DisplayOrder'),
                    data: "displayOrder"
                },
                {
                    title: l('Status'),
                    data: "isActive",
                    render: function (data) {
                        return data ? '<i class="fa fa-check text-success"></i>' : '<i class="fa fa-times text-danger"></i>';
                    }
                },
                {
                    title: l('Value'),
                    data: "value",
                    render: function(data) {
                        return data || '';
                    }
                }
            ]
        })
    );

    var createModal = new abp.ModalManager(abp.appPath + 'Categories/CategoryItems/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Categories/CategoryItems/EditModal');

    createModal.onResult(function () {
        abp.notify.success(l('SuccessfullyCreated'));
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        abp.notify.success(l('SuccessfullyUpdated'));
        dataTable.ajax.reload();
    });

    $('#NewCategoryItemButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

    // Handle category type filter change
    $('#CategoryTypeFilter').on('change', function() {
        var categoryTypeId = $(this).val();
        var $parentFilter = $('#ParentFilter');

        // Clear and disable parent filter
        $parentFilter.empty().prop('disabled', true);
        $parentFilter.append($('<option>', {
            value: '',
            text: l('All')
        }));

        if (categoryTypeId) {
            abp.ui.setBusy($parentFilter);
            aqt.coreOracle.categories.categoryItem
                .getList({
                    categoryTypeId: categoryTypeId,
                    maxResultCount: 1000,
                    isActive: true
                })
                .then(function(result) {
                    result.items.forEach(function(item) {
                        $parentFilter.append($('<option>', {
                            value: item.id,
                            text: item.name
                        }));
                    });
                    $parentFilter.prop('disabled', false);
                })
                .finally(function() {
                    abp.ui.clearBusy($parentFilter);
                });
        }

        dataTable.ajax.reload();
    });

    // Handle parent filter change
    $('#ParentFilter').on('change', function() {
        dataTable.ajax.reload();
    });

    // Handle active status filter change  
    $('#IsActiveFilter').on('change', function() {
        dataTable.ajax.reload();
    });
}); 