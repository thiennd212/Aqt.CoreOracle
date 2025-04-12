$(function () {
    // Các biến global
    var ouService = aqt.coreOracle.application.organizationUnits.customOrganizationUnit; // Kiểm tra lại namespace chính xác
    var positionService = aqt.coreOracle.application.positions.position; // Kiểm tra lại namespace chính xác
    var membersTable = null;
    var selectedOuId = null;
    var permissions = ouPermissions; // Từ các permissions được serialize từ C#
    
    // Kiểm tra xem abp.ModalManager đã tồn tại chưa
    //console.log('Checking abp.ModalManager before init:', abp.ModalManager);
    
    // Các đối tượng DOM thường xuyên được sử dụng
    var $tree = $('#OrganizationUnitTree');
    var $ouDetails = $('#OrganizationUnitDetails');
    var $ouDetailsHeader = $('#OuDetailsHeader');
    var $ouDetailsActions = $('#OuDetailsActions');
    var $ouDetailsBody = $('#OuDetailsBody');
    var $assignPositionButton = $('#AssignPositionButton');
    var $newRootOuButton = $('#NewRootOuButton');  
    var $addChildOuButton = $('#AddChildOuButton');

    // Đối tượng modal để tái sử dụng
    var createOuModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'OrganizationStructure/CreateModal',
        //scriptUrl: abp.appPath + 'Pages/OrganizationStructure/CreateModal.js',
        modalClass: 'CreateOuModal'
    });
    
    var editOuModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'OrganizationStructure/EditModal',
        //scriptUrl: abp.appPath + 'Pages/OrganizationStructure/EditModal.js',
        modalClass: 'EditOuModal'
    });
    
    var assignPositionModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'OrganizationStructure/AssignPositionModal',
        //scriptUrl: abp.appPath + 'Pages/OrganizationStructure/AssignPositionModal.js',
        modalClass: 'AssignPositionModal'
    });

    // Khởi tạo
    initOrganizationUnitTree();
    registerEventHandlers();
    
    // Khởi tạo cây OU sử dụng jsTree
    function flatten(nodes, parentId = null) {
        var flat = [];

        nodes.forEach(function (node) {
            flat.push({
                id: node.id,
                parent: parentId ? parentId.toString() : '#',
                text: node.displayName
            });

            if (node.children && node.children.length > 0) {
                flat = flat.concat(flatten(node.children, node.id));
            }
        });

        return flat;
    }
    function initOrganizationUnitTree() {
        // Thay vì fetch trước, cấu hình jsTree để tự fetch qua function
        $tree.jstree({
            core: {
                // Cung cấp một function cho data để jsTree tự gọi khi cần
                data: function (node, cb) {
                    ouService.getOrganizationTree().then(function (result) {
                        // Map dữ liệu bên trong function này
                        var jstreeData = flatten(result.items); // Flatten toàn bộ cây
                        console.log('jstreeData:', jstreeData);
                        // Gọi callback của jsTree với dữ liệu đã map
                        cb(jstreeData);
                    }).catch(function(error) {
                        // Xử lý lỗi nếu có
                        console.error("Error fetching organization tree for jsTree:", error);
                        abp.notify.error(l('ErrorFetchingOrganizationTree'));
                        cb([]); // Trả về mảng rỗng khi có lỗi
                    });
                },
                check_callback: true, // Cho phép các thao tác khác như tạo, sửa, xóa (nếu dùng plugin tương ứng)
                themes: {
                    responsive: true,
                    dots: true
                }
            },
            types: {
                default: {
                    icon: 'fa fa-folder text-warning' 
                }
                // Bạn có thể định nghĩa thêm types nếu muốn icon khác nhau
            },
            plugins: ['types', 'themes'] // Giữ các plugin cần thiết
        });

        // Xử lý sự kiện chọn node (giữ nguyên)
        $tree.on('select_node.jstree', function (e, data) {
            selectedOuId = data.node.id;
            loadOrganizationUnitDetails(selectedOuId);
            loadMembers(selectedOuId);
            // Enable nút Assign Position khi node được chọn (nếu có permission)
            if (abp.auth.isGranted(permissions.assignPosition)) {
                $assignPositionButton.prop('disabled', false);
            }
        });
        
        // Xử lý sự kiện bỏ chọn node (deselect) - (Giữ nguyên, có thể bỏ comment nếu cần)
         $tree.on('deselect_node.jstree', function (e, data) {
             // selectedOuId = null;
             // $ouDetails.hide();
             // if (membersTable) membersTable.clear().draw();
             // $assignPositionButton.prop('disabled', true);
         });

         // Các sự kiện khác của jsTree như loaded.jstree, refresh.jstree có thể được thêm ở đây nếu cần
         $tree.on('loaded.jstree', function() {
             // Có thể thực hiện hành động gì đó sau khi cây tải xong lần đầu
             console.log('jsTree loaded initial data.');            
            
         });

         $tree.on('ready.jstree', function () {
            console.log('jsTree is fully ready');
        
            // Remove jstree-no-dots from all matching ul
            $('#OrganizationUnitTree ul.jstree-container-ul').removeClass('jstree-no-dots');
            $('#OrganizationUnitTree').jstree('open_node', '#'); // mở root
            $('#OrganizationUnitTree').find('li[aria-level="2"]').each(function () {
                $('#OrganizationUnitTree').jstree('open_node', this.id);
            });
        });

         $tree.on('refresh.jstree', function() {
             // Có thể thực hiện hành động gì đó sau mỗi lần refresh
             console.log('jsTree refreshed.');
             // Logic để chọn lại node đã chọn trước đó nếu cần
             if (selectedOuId) {
                 // Cần kiểm tra node có còn tồn tại không trước khi chọn lại
                 // $tree.jstree(true).select_node(selectedOuId); 
             }
         });
    }
    
    // Tải chi tiết OU khi một node được chọn
    function loadOrganizationUnitDetails(ouId) {
        if (!ouId) {
            $ouDetails.hide();
            $addChildOuButton.hide();
            return;
        }
        
        // Hiển thị loading nếu cần
        //$ouDetails.html('<div class="spinner-border spinner-border-sm" role="status"><span class="visually-hidden">Loading...</span></div>').show();
        $addChildOuButton.hide();
        
        // Lấy thông tin chi tiết từ service
        ouService.get(ouId).then(function (result) {
            // Hiển thị thông tin chi tiết
            $ouDetailsHeader.html('<h4>' + result.displayName + '</h4>');
            
            // Xóa các nút action cũ trước khi thêm lại
            $ouDetailsActions.empty(); 
            // Hiển thị nút Add Child nếu có quyền và OU được chọn
            if (abp.auth.isGranted(permissions.create)) {
                $addChildOuButton.show();
                $ouDetailsActions.append($addChildOuButton);
            }
            // Thêm các nút Edit/Delete vào $ouDetailsActions nếu cần
            
            var detailsBodyHtml = 
                '<div class="col-md-4"><strong>' + l('OrganizationUnit:SyncCode') + ':</strong> ' + (result.syncCode || '') + '</div>' +
                '<div class="col-md-4"><strong>' + l('OrganizationUnit:Address') + ':</strong> ' + (result.address || '') + '</div>' +
                '<div class="col-md-4"><strong>' + l('OrganizationUnit:OrganizationUnitType') + ':</strong> ' + getOrganizationUnitTypeText(result.organizationUnitType) + '</div>';
            
            $ouDetailsBody.html(detailsBodyHtml);
            $ouDetails.show();
            registerEventHandlersAfterSelectNode();
            
        }).fail(function() {
             $ouDetails.html('<span class="text-danger">' + l('ErrorLoadingOrganizationUnitDetails') + '</span>').show();
             $addChildOuButton.hide();
        });
    }
    
    // Khởi tạo và load bảng thành viên
    function loadMembers(ouId) {
        if (!ouId) {
            // Disable nút gán position nếu không có OU nào được chọn
            $assignPositionButton.prop('disabled', true);
            
            if (membersTable) {
                membersTable.clear().draw();
            }
            return;
        }
        
        // Enable nút gán position
        $assignPositionButton.prop('disabled', false);
        
        // Khởi tạo hoặc làm mới bảng Members
        if (membersTable) {
            membersTable.ajax.reload();
        } else {
            membersTable = $('#MembersTable').DataTable(abp.libs.datatables.normalizeConfiguration({
                processing: true,
                serverSide: true,
                paging: true,
                searching: false, // Vô hiệu hóa ô tìm kiếm mặc định của DataTables nếu không cần
                autoWidth: false,
                scrollCollapse: true,
                order: [[0, "asc"]], // Sắp xếp mặc định theo cột đầu tiên (ví dụ: UserName)
                ajax: abp.libs.datatables.createAjax(
                    ouService.getEmployeePositions, // <-- Dùng trực tiếp proxy service
                    function(input) { // <-- Hàm callback để tùy chỉnh input DTO
                        // 'input' đã có sẵn SkipCount, MaxResultCount, Sorting từ DataTables
                        input.organizationUnitId = selectedOuId; // Chỉ cần thêm OU ID
                        return input; // Trả về DTO đã được bổ sung
                    }
                    // Không cần responseCallback vì backend trả về PagedResultDto chuẩn
                ),
                columnDefs: [
                    {
                        title: l('UserName'),
                        data: "userName"
                    },
                    {
                        title: l('PositionName'), // Đổi thành PositionName nếu DTO trả về tên
                        data: "positionName"
                    },
                    {
                        title: l('StartDate'),
                        data: "startDate", // Giả sử dùng CreationTime làm StartDate
                        dataFormat: 'datetime' // Sử dụng dataFormat chuẩn của ABP
                    },
                    {
                        title: l('EndDate'),
                        data: "endDate", // Giả sử dùng CreationTime làm StartDate
                        dataFormat: 'datetime' // Sử dụng dataFormat chuẩn của ABP
                    },
                    {
                        title: l('Actions'),
                        rowAction: {
                            items: [
                                {
                                    text: l('Remove'),
                                    visible: abp.auth.isGranted(permissions.assignPosition), // Chỉ hiển thị nếu có quyền
                                    confirmMessage: function (data) {
                                        return l('RemovePositionConfirmationMessage', data.record.userName, data.record.positionName);
                                    },
                                    action: function (data) {
                                        ouService.removePositionFromUser(data.record.id) // Gọi API xóa
                                            .then(function () {
                                                membersTable.ajax.reload();
                                                abp.notify.success(l('SuccessfullyRemoved'));
                                            });
                                    }
                                }
                            ]
                        }
                    }
                ]
            }));
        }
    }
    
    // Đăng ký các event handlers cho các nút
    function registerEventHandlers() {
        // Nút "Tạo OU gốc"
        $newRootOuButton.click(function (e) {
            e.preventDefault();
            createOuModal.open();
        });
        
        // Nút "Gán Chức vụ"
        $assignPositionButton.click(function (e) {
            e.preventDefault();
            if (!selectedOuId) {
                abp.message.warn(l('SelectAnOrganizationUnit'));
                return;
            }
            
            assignPositionModal.open({
                organizationUnitId: selectedOuId
            });
        });
        
        // Nút "Thêm đơn vị/phòng ban con"
        $addChildOuButton.click(function (e) {
            e.preventDefault();
            if (!selectedOuId) {
                abp.message.warn(l('SelectAnOrganizationUnitFirst'));
                return;
            }
            // Mở modal tạo và truyền parentId
            createOuModal.open({ 
                parentId: selectedOuId 
            }); 
        });
        
        // Xử lý sau khi các modal được đóng (tạo/sửa/xóa)
        createOuModal.onResult(function () {
            refreshOrganizationUnitTree();
        });
        
        editOuModal.onResult(function () {
            refreshOrganizationUnitTree();
            if (selectedOuId) {
                loadOrganizationUnitDetails(selectedOuId);
            }
        });
        
        assignPositionModal.onResult(function () {
            if (selectedOuId) {
                loadMembers(selectedOuId);
            }
        });
    }

    // Đăng ký các event handlers cho các nút sau khi chọn node
    function registerEventHandlersAfterSelectNode()
    {
        // Nút "Thêm đơn vị/phòng ban con"
        $addChildOuButton.click(function (e) {
            e.preventDefault();
            if (!selectedOuId) {
                abp.message.warn(l('SelectAnOrganizationUnitFirst'));
                return;
            }
            // Mở modal tạo và truyền parentId
            createOuModal.open({
                parentId: selectedOuId
            });
        });
    }
    
    // Refresh cây OU
    function refreshOrganizationUnitTree() {
        $tree.jstree(true).refresh();
    }
    
    // Helper để chuyển đổi mã loại OU thành text
    function getOrganizationUnitTypeText(typeValue) {
        if (typeValue === 1) {
            return l('OrganizationUnit:Type:Unit');
        } else if (typeValue === 2) {
            return l('OrganizationUnit:Type:Department');
        }
        return '';
    }
    
    // Helper cho localization
    function l(key) {
        return abp.localization.getResource('CoreOracle')(key);
    }
}); 