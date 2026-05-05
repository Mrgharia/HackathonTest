"use strict";
console.log("nomination.js loaded");
let rowIndex = 0;
let activeDateInput = null;
document.addEventListener("DOMContentLoaded", function () {

    console.log("DOM loaded");
    const pipelineSelect = document.getElementById("pipelineSelect");
    const shipperSelect = document.getElementById("shipperSelect");
    const headerForm = document.getElementById("headerForm");
    const showMineChk = document.getElementById("showMineOnly");
    const showMineForm = document.getElementById("showMineForm");
    const pageSizeSelect = document.getElementById("pageSizeSelect");
    const pageSizeForm = document.getElementById("pageSizeForm");
    const addRowBtn = document.getElementById("addRowBtn");
    console.log("addRowBtn:", addRowBtn);
    const deleteBtn = document.getElementById("deleteBtn");
    const avatar = document.getElementById("userAvatar");
    const dropdown = document.getElementById("profileDropdown");
    const resizer = document.getElementById("sidebarResizer");
    const sidebar = document.getElementById("sidebar");
    const menuOpenBtn = document.getElementById("menuOpenBtn");
    const menuCloseBtn = document.getElementById("menuCloseBtn");
    const overlay = document.getElementById("overlay");


    /* ── Top dropdowns ── */
    if (pipelineSelect && shipperSelect && window.jQuery && $.fn.select2) {
        $("#pipelineSelect, #shipperSelect").select2({
            width: "resolve",
            allowClear: false
        });
    }
    initStaticSelect2();
    reinitializeDynamicSelect2(document);
    initDatePickers(document);
    /* ── Sidebar nav toggle ── */
   
    $(window).on("load", function () {
        hideLoader();
    });

    function getCellValue(row, colIndex) {
        const cell = row.find("td").eq(colIndex);

        const input = cell.find("input, select, textarea").first();

        if (input.length) {
            return (input.val() || "").toString().trim();
        }

        return cell.text().trim();
    }

    function applyFilters() {
        const filterColumnMap = {
            nomStatus: 1,
            gisbStatus: 2,
            schedQty: 3,
            transType: 4,
            quantityTypeIndicator: 5,
            startedDate: 6,
            endDate: 7,
            createdDate: 8,
            cycle: 9,
            contractNumber: 10,
            rollNom: 11,
            recLocation: 12,
            recLocProp: 13,
            recLocId: 14,
            upName: 15,
            upIdProp: 16,
            upId: 17,
            upContractNumber: 18,
            recQty: 19,
            recRank: 20,
            delLoc: 21,
            delLocId: 22,
            delLocProp: 23,
            downName: 24,
            downIdProp: 25,
            downId: 26,
            downContractNumber: 27,
            delQuantity: 28,
            delRank: 29,
            dealType: 30,
            capacityBlockId: 31,
            pkgId: 32,
            fuelpercent: 33,
            shipperSpecificId: 34,
            nomTrackingId: 35,
            nomSubmittedDateTime: 36,
            nomQuickResponseDateTime: 37,
            referenceNumber: 38,
            agentDuns: 39   };

        $(".data-row").each(function () {
            let show = true;
            const row = $(this);

            $(".filter-select, .filter-input").each(function () {
                const filterName = $(this).data("filter");
                const rawFilterValue = $(this).val();

                if (!filterName || !rawFilterValue || rawFilterValue === "") return;

                const colIndex = filterColumnMap[filterName];
                const cellValue = getCellValue(row, colIndex);

                if (Array.isArray(rawFilterValue)) {
                    if (!rawFilterValue.includes(cellValue)) {
                        show = false;
                        return false;
                    }
                } else {
                    const filterValue = rawFilterValue.toString().trim();

                    if (cellValue !== filterValue) {
                        show = false;
                        return false;
                    }
                }
            });

            row.toggle(show);
        });
    }

    $(document).on("change keyup", ".filter-select, .filter-input", function () {
        applyFilters();
    });/* ── Mobile sidebar toggle ── */
    if (menuOpenBtn && menuCloseBtn && sidebar && overlay) {
        menuOpenBtn.addEventListener("click", function () {
            sidebar.classList.add("active");
            overlay.classList.add("active");
            document.body.classList.add("sidebar-open");
        });

        menuCloseBtn.addEventListener("click", function () {
            sidebar.classList.remove("active");
            overlay.classList.remove("active");
            document.body.classList.remove("sidebar-open");
        });

        overlay.addEventListener("click", function () {
            sidebar.classList.remove("active");
            overlay.classList.remove("active");
            document.body.classList.remove("sidebar-open");
        });
    }



    
    /* ── Show Mine Only checkbox ── */
    //if (showMineChk && showMineForm) {
    //    showMineChk.addEventListener("change", function () {
    //        showMineForm.submit();
    //    });
    //}

    /* ── Page size change ── */
    if (pageSizeSelect && pageSizeForm) {
        pageSizeSelect.addEventListener("change", function () {
            pageSizeForm.submit();
        });
    }

    /* ── Header form auto submit ── */
    if (pipelineSelect && headerForm) {
        pipelineSelect.addEventListener("change", function () {
            headerForm.submit();
        });
    }

    if (shipperSelect && headerForm) {
        shipperSelect.addEventListener("change", function () {
            headerForm.submit();
        });
    }

    /* ── Toolbar button loading state ── */
    const actionButtons = document.querySelectorAll(".btn[data-action]");
    actionButtons.forEach(function (btn) {
        btn.addEventListener("click", function () {
            const originalText = btn.innerHTML;
            const loadingText = btn.dataset.loading;

            if (!loadingText) return;

            btn.disabled = true;
            btn.innerHTML = '<span class="spinner"></span> ' + loadingText;

            setTimeout(function () {
                btn.innerHTML = originalText;
                btn.disabled = false;
            }, 1200);
        });
    });


    /* ── Auto-dismiss alerts ── */
    setTimeout(function () {
        document.querySelectorAll(".alert").forEach(function (a) {
            a.style.transition = "opacity 0.5s";
            a.style.opacity = "0";
            setTimeout(function () {
                a.remove();
            }, 500);
        });
    }, 3000);

    /* ── User profile dropdown ── */
    if (avatar && dropdown) {
        avatar.addEventListener("click", function (e) {
            e.stopPropagation();
            dropdown.style.display = dropdown.style.display === "block" ? "none" : "block";
        });

        document.addEventListener("click", function (e) {
            if (!e.target.closest(".user-profile")) {
                dropdown.style.display = "none";
            }
        });
    }

    /* ── Checkbox sync ── */
    $(document).on("change", "#selectAllRows", function () {
        const isChecked = $(this).is(":checked");
        $(".row-checkbox").prop("checked", isChecked);
        syncHeaderCheckboxState();
    });

    $(document).on("change", ".row-checkbox", function () {
        syncHeaderCheckboxState();
    });

    syncHeaderCheckboxState();

    /* ── Sidebar resizer (desktop only) ── */
    let isResizing = false;

    if (resizer && sidebar) {
        resizer.addEventListener("mousedown", function () {
            if (window.innerWidth <= 768) return;
            isResizing = true;
            document.body.style.cursor = "col-resize";
            document.body.style.userSelect = "none";
        });

        document.addEventListener("mousemove", function (e) {
            if (!isResizing) return;

            const newWidth = e.clientX;
            if (newWidth < 180 || newWidth > 360) return;

            sidebar.style.width = newWidth + "px";
            sidebar.style.minWidth = newWidth + "px";
        });

        document.addEventListener("mouseup", function () {
            isResizing = false;
            document.body.style.cursor = "";
            document.body.style.userSelect = "";
        });
    }




    $("#refreshBtn").on("click", function () {
        let table = $("#nomTable").DataTable();

        table.columns.adjust().draw(false);

        $(".select2-dynamic, .select2").each(function () {
            if ($(this).hasClass("select2-hidden-accessible")) {
                $(this).select2("close");
            }
        });
        table.columns.adjust().draw(false);

        resetAllCheckboxes();
    });
    function resetAllCheckboxes() {
        // uncheck all row checkboxes
        $(".row-checkbox").prop("checked", false);

        // uncheck header select all checkbox
        $("#selectAllRows")
            .prop("checked", false)
            .prop("indeterminate", false);
    }


    function isEmptyValue(value) {
        return (
            value === "" ||
            value === null ||
            value === undefined ||
            value === "--select--" ||
            value === "--Select--" ||
            value === "0"
        );
    }

    function validateField(row, selector) {
        const field = row.find(selector);

        if (!field.length) return true;

        const td = field.closest("td");
        const value = field.val();

        if (isEmptyValue(value)) {
            td.addClass("danger");
            return false;
        } else {
            td.removeClass("danger");
            return true;
        }
    }



    function validateField(rowId, fieldName) {
        const field = $("#NominationRecords_" + rowId + "__" + fieldName);
        const value = field.val();

        if (value === "" || value === "--Select--" || value == null) {
            field.closest("td").addClass("danger");
            return false;
        }

        field.closest("td").removeClass("danger");
        return true;
    }



    $("#validateBtn").off("click").on("click", function () {
        if ($(".add-row").length > 0) {
            if (validateAddRowBeforeSave()) {
                alert("All added rows are valid");
            }
            return;
        }

        var selectedRows = $(".row-checkbox:checked").closest("tr");

        if (selectedRows.length === 0) {
            alert("Please select rows to validate");
            return;
        }

        alert(selectedRows.length + " selected row(s) validated successfully");
    });



    $("#addRowBtn").on("click", function () {
        console.log("Add button clicked");

        $.ajax({
            url: "/Nomination/AddRow",
            type: "GET",

            success: function (html) {
                console.log("AddRow success");

                let table = $("#nomTable").DataTable();
                let newRow = $(html);

                table.row.add(newRow[0]).draw(false);
                table.columns.adjust();

                newRow.find("[name='NewRecord.CreatedBy']").val(getLoggedInUser());
                reinitializeDynamicSelect2(newRow);
                initDatePickers(newRow);
            },

            error: function (xhr) {
                console.log("AddRow error");
                console.log("Status:", xhr.status);
                console.log("Response:", xhr.responseText);
                alert("Error adding row");
            }
        });
    });









    $("#deleteBtn").on("click", function () {
        var ids = [];

        $(".row-checkbox:checked").each(function () {
            var row = $(this).closest("tr");
            var id = $(this).val();

            // unsaved add row → remove directly from UI
            if (row.hasClass("add-row")) {
                row.remove();
            }
            else if (id) {
                ids.push(id);
            }
        });

        console.log("DB IDs to delete:", ids);

        // only unsaved rows selected
        if (ids.length === 0) {
            return;
        }

        $.ajax({
            url: "/Nomination/DeleteRecords",
            type: "POST",
            traditional: true,
            data: {
                ids: ids,
                __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
            },
            success: function () {
                alert("Records deleted successfully");
                location.reload();
            },
            error: function (xhr) {
                console.log(xhr.responseText);
                alert("Error deleting records");
            }
        });
    });



    function validateAddRowBeforeSave() {
        $("td.danger").removeClass("danger");

        var rows = $(".add-row");

        if (rows.length === 0) {
            alert("Please add a row first");
            return false;
        }

        var requiredFields = [
            "NewRecord.StartedDate",
            "NewRecord.EndDate",
            "NewRecord.Cycle",
            "NewRecord.ContractNumber",
            "NewRecord.TransType",
            "NewRecord.RecLocation",
            "NewRecord.RecLocId",
            "NewRecord.RecQty",
            "NewRecord.RecRank",
            "NewRecord.DelLoc",
            "NewRecord.DelLocId",
            "NewRecord.DownId",
            "NewRecord.DelQuantity",
            "NewRecord.DelRank",
            "NewRecord.UpContractNumber",
            "NewRecord.CapacityBlockId"
        ];

        var isValid = true;
        var firstInvalidField = null;

        rows.each(function () {
            var row = $(this);

            requiredFields.forEach(function (name) {
                var field = row.find("[name='" + name + "']");
                var value = field.val();

                if (
                    value === "" ||
                    value === null ||
                    value === undefined ||
                    value === "--select--" ||
                    value === "--Select--"
                ) {
                    field.closest("td").addClass("danger");

                    if (!firstInvalidField) {
                        firstInvalidField = field;
                    }

                    isValid = false;
                }
            });
        });

        if (!isValid) {
            alert("Please fill required fields in all added rows");

            if (firstInvalidField && firstInvalidField.length) {
                firstInvalidField.focus();

                var wrapper = $(".table-scroll-wrap");
                if (wrapper.length) {
                    wrapper.animate({
                        scrollLeft: firstInvalidField.closest("td").position().left
                    }, 300);
                }
            }

            return false;
        }

        return true;
    }







    $("#saveBtn").on("click", function () {

        if (!validateAddRowBeforeSave()) {
            return;
        }

        var rows = $(".add-row");

        if (rows.length === 0) {
            alert("Please add at least one row");
            return;
        }

        var totalRows = rows.length;
        var savedCount = 0;
        var failedCount = 0;

        rows.each(function () {
            var row = $(this);
            var formData = new FormData();

            formData.append("Pipeline", $("#pipelineSelect").val());
            formData.append("Shipper", $("#shipperSelect").val());
            formData.append("ShowMineOnly", $("#showMineOnly").is(":checked"));
            formData.append("PageSize", $("#pageSizeSelect").val());

            formData.append("NewRecord.NomStatus", row.find("[name='NewRecord.NomStatus']").val());
            formData.append("NewRecord.GisbStatus", row.find("[name='NewRecord.GisbStatus']").val());
            formData.append("NewRecord.SchedQty", row.find("[name='NewRecord.SchedQty']").val());
            formData.append("NewRecord.TransType", row.find("[name='NewRecord.TransType']").val());
            formData.append("NewRecord.QuantityTypeIndicator", row.find("[name='NewRecord.QuantityTypeIndicator']").val());

            formData.append("NewRecord.StartedDate", row.find("[name='NewRecord.StartedDate']").val());
            formData.append("NewRecord.EndDate", row.find("[name='NewRecord.EndDate']").val());
            formData.append("NewRecord.CreatedDate", row.find("[name='NewRecord.CreatedDate']").val());

            formData.append("NewRecord.Cycle", row.find("[name='NewRecord.Cycle']").val());
            formData.append("NewRecord.ContractNumber", row.find("[name='NewRecord.ContractNumber']").val());
            formData.append("NewRecord.RollNom", row.find("[name='NewRecord.RollNom']").val());

            formData.append("NewRecord.RecLocation", row.find("[name='NewRecord.RecLocation']").val());
            formData.append("NewRecord.RecLocProp", row.find("[name='NewRecord.RecLocProp']").val());
            formData.append("NewRecord.RecLocId", row.find("[name='NewRecord.RecLocId']").val());

            formData.append("NewRecord.UpName", row.find("[name='NewRecord.UpName']").val());
            formData.append("NewRecord.UpIdProp", row.find("[name='NewRecord.UpIdProp']").val());
            formData.append("NewRecord.UpId", row.find("[name='NewRecord.UpId']").val());
            formData.append("NewRecord.UpContractNumber", row.find("[name='NewRecord.UpContractNumber']").val());

            formData.append("NewRecord.RecQty", row.find("[name='NewRecord.RecQty']").val());
            formData.append("NewRecord.RecRank", row.find("[name='NewRecord.RecRank']").val());

            formData.append("NewRecord.DelLoc", row.find("[name='NewRecord.DelLoc']").val());
            formData.append("NewRecord.DelLocId", row.find("[name='NewRecord.DelLocId']").val());
            formData.append("NewRecord.DelLocProp", row.find("[name='NewRecord.DelLocProp']").val());

            formData.append("NewRecord.DownName", row.find("[name='NewRecord.DownName']").val());
            formData.append("NewRecord.DownIdProp", row.find("[name='NewRecord.DownIdProp']").val());
            formData.append("NewRecord.DownId", row.find("[name='NewRecord.DownId']").val());
            formData.append("NewRecord.DownContractNumber", row.find("[name='NewRecord.DownContractNumber']").val());

            formData.append("NewRecord.DelQuantity", row.find("[name='NewRecord.DelQuantity']").val());
            formData.append("NewRecord.DelRank", row.find("[name='NewRecord.DelRank']").val());

            formData.append("NewRecord.DealType", row.find("[name='NewRecord.DealType']").val());
            formData.append("NewRecord.CapacityBlockId", row.find("[name='NewRecord.CapacityBlockId']").val());
            formData.append("NewRecord.PkgId", row.find("[name='NewRecord.PkgId']").val());

            formData.append("NewRecord.FuelPercent", row.find("[name='NewRecord.FuelPercent']").val());
            formData.append("NewRecord.CreatedBy", row.find("[name='NewRecord.CreatedBy']").val());

            formData.append("NewRecord.ShipperSpecificId", row.find("[name='NewRecord.ShipperSpecificId']").val());
            formData.append("NewRecord.NomTrackingId", row.find("[name='NewRecord.NomTrackingId']").val());

            formData.append("NewRecord.NomSubmittedDateTime", row.find("[name='NewRecord.NomSubmittedDateTime']").val());
            formData.append("NewRecord.NomQuickResponseDateTime", row.find("[name='NewRecord.NomQuickResponseDateTime']").val());

            formData.append("NewRecord.ReferenceNumber", row.find("[name='NewRecord.ReferenceNumber']").val());
            formData.append("NewRecord.AgentDuns", row.find("[name='NewRecord.AgentDuns']").val());

            $.ajax({
                url: "/Nomination/SaveRecord",
                type: "POST",
                data: formData,
                processData: false,
                contentType: false,

                success: function () {
                    savedCount++;

                    if (savedCount + failedCount === totalRows) {
                        if (failedCount === 0) {
                            alert("All records saved successfully");
                        } else {
                            alert(savedCount + " saved, " + failedCount + " failed");
                        }

                        location.reload();
                    }
                },

                error: function (xhr) {
                    failedCount++;
                    console.log(xhr.responseText);

                    if (savedCount + failedCount === totalRows) {
                        alert(savedCount + " saved, " + failedCount + " failed");
                        location.reload();
                    }
                }
            });
        });
    });





    function getCellValue(row, index) {
        const cell = row.find("td").eq(index);

        const field = cell.find("input, select, textarea").first();

        if (field.length > 0) {
            return (field.val() || "").toString().trim();
        }

        return cell.text().trim();
    }
    $("#sendBtn").off("click").on("click", function () {
        var rows = $(".row-checkbox:checked").closest("tr");

        if (rows.length === 0) {
            alert("Please select at least one row to send");
            return;
        }

        var records = [];

        rows.each(function () {
            var row = $(this);

            records.push({
                nomStatus: getCellValue(row, 1),
                gisbStatus: getCellValue(row, 2),
                schedQty: getCellValue(row, 3),
                transType: getCellValue(row, 4),
                quantityTypeIndicator: getCellValue(row, 5),
                startedDate: getCellValue(row, 6),
                endDate: getCellValue(row, 7),
                createdDate: getCellValue(row, 8),
                cycle: getCellValue(row, 9),
                contractNumber: getCellValue(row, 10),
                rollNom: getCellValue(row, 11),
                recLocation: getCellValue(row, 12),
                recLocProp: getCellValue(row, 13),
                recLocId: getCellValue(row, 14),
                upName: getCellValue(row, 15),
                upIdProp: getCellValue(row, 16),
                upId: getCellValue(row, 17),
                upContractNumber: getCellValue(row, 18),
                recQty: getCellValue(row, 19),
                recRank: getCellValue(row, 20),
                delLoc: getCellValue(row, 21),
                delLocId: getCellValue(row, 22),
                delLocProp: getCellValue(row, 23),
                downName: getCellValue(row, 24),
                downIdProp: getCellValue(row, 25),
                downId: getCellValue(row, 26),
                downContractNumber: getCellValue(row, 27),
                delQuantity: getCellValue(row, 28),
                delRank: getCellValue(row, 29),
                dealType: getCellValue(row, 30),
                capacityBlockId: getCellValue(row, 31),
                pkgId: getCellValue(row, 32),
                fuelPercent: getCellValue(row, 33),
                shipperSpecificId: getCellValue(row, 34),
                nomTrackingId: getCellValue(row, 35),
                nomSubmittedDateTime: getCellValue(row, 36),
                nomQuickResponseDateTime: getCellValue(row, 37),
                referenceNumber: getCellValue(row, 38),
                agentDuns: getCellValue(row, 39)
});
        });

        console.log("Sending records:", records);
        console.log(JSON.stringify(records, null, 2));

        $.ajax({
            url: "/Nomination/SendRecords",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(records),
            headers: {
                'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
            },
            success: function () {
                alert(records.length + " record(s) sent successfully");
            },
            error: function (xhr) {
                console.log(xhr.responseText);
                alert("Error while sending records");
            }
        });
    });
        $("#copyRowbtn").on("click", function () {

            var selectedRows = $(".row-checkbox:checked");

            if (selectedRows.length !== 1) {
                alert("Please select exactly one row to copy");
                return;
            }

            var selectedRow = selectedRows.closest("tr");

            // if unsaved row

            if (selectedRow.hasClass("add-row")) {

                // 1. Store current values by field name
                var values = {};
                selectedRow.find("input, select").each(function () {
                    var name = $(this).attr("name");
                    if (!name) return;

                    if ($(this).attr("type") === "checkbox") return;

                    values[name] = $(this).val();
                });

                // 2. Clone row
                var clonedRow = selectedRow.clone(false, false);

                // 3. Clean old Select2 UI
                clonedRow.find(".select2-container").remove();

                clonedRow.find("select").each(function () {
                    $(this)
                        .removeClass("select2-hidden-accessible")
                        .removeAttr("data-select2-id")
                        .removeAttr("aria-hidden")
                        .removeAttr("tabindex");
                });

                // 4. Apply copied values again
                clonedRow.find("input, select").each(function () {
                    var name = $(this).attr("name");
                    if (!name) return;

                    if ($(this).attr("type") === "checkbox") {
                        $(this).prop("checked", false);
                        return;
                    }

                    if (values[name] !== undefined) {
                        $(this).val(values[name]);
                    }
                });

                // 5. Add row
                $("#nomTableBody").append(clonedRow);

                reinitializeDynamicSelect2(newRow);
                resetAllCheckboxes();

                return;
            }







            // saved row copy
            $.ajax({
                url: "/Nomination/AddRow",
                type: "GET",

                success: function (html) {

                    $("#nomTableBody").append(html);

                    var newRow = $("#nomTableBody tr.add-row:last");
                    var oldCells = selectedRow.find("td");

                    newRow.find("td").each(function (tdIndex) {
                        if (tdIndex === 0) return; // checkbox column skip

                        var oldValue = $(oldCells[tdIndex]).text().trim();
                        var field = $(this).find("input, select");

                        if (!field.length) return;

                        if (field.attr("type") === "date") {
                            field.val(toDateInputValue(oldValue));
                        }
                        else if (field.is("select")) {
                            field.val(oldValue).trigger("change");
                        }
                        else {
                            field.val(oldValue);
                        }
                    });






                    reinitializeDynamicSelect2(newRow);
                },

                error: function () {
                    alert("Error copying row");
                }
            });
        });


        //date fixed
        function toDateInputValue(value) {
            if (!value) return "";

            value = value.trim();

            // MM-dd-yyyy or MM/dd/yyyy
            let parts = value.includes("-") ? value.split("-") : value.split("/");

            if (parts.length === 3) {
                let mm = parts[0].padStart(2, "0");
                let dd = parts[1].padStart(2, "0");
                let yyyy = parts[2].substring(0, 4);
                return `${yyyy}-${mm}-${dd}`;
            }

            return value;
        }





        //scroller fixed
        //$(".table-scroll-wrap").on("wheel", function () {
        //    $(".select2-dynamic").each(function () {
        //        if ($(this).hasClass("select2-hidden-accessible")) {
        //            $(this).select2("close");
        //        }
        //    });
        //    $(".select2").each(function () {
        //        if ($(this).hasClass("select2-hidden-accessible")) {
        //            $(this).select2("close");
        //        }
        //    });


        //      });

    $(".table-scroll-wrap, .dataTables_scrollBody").on("scroll wheel", function () {
        $(".select2-dynamic, .select2").each(function () {
            if ($(this).hasClass("select2-hidden-accessible")) {
                $(this).select2("close");
            }
        });

        if (activeDateInput) {
            $(activeDateInput).datepicker("hide");
            $(activeDateInput).blur();
            activeDateInput = null;
        }
    });





        function getLoggedInUser() {
            const el = document.getElementById("loggedInUserName");
            return el ? el.value : "Admin";
        }

        function initStaticSelect2() {
            if (!window.jQuery || !$.fn || !$.fn.select2) {
                console.log("Select2 not loaded");
                return;
            }

            $("select.filter-select, .filter-row select, .nom-table select.select2").each(function () {
                const $el = $(this);

                if ($el.hasClass("select2-hidden-accessible")) {
                    $el.select2("destroy");
                }

                $el.select2({
                    width: "100%",
                    allowClear: false,
                    closeOnSelect: false
                });
            });

            console.log("Filter Select2 initialized:", $("select.filter-select, .filter-row select").length);
        }
        //dates
    function initDatePickers(scope) {
        $(scope).find(".datepicker").each(function () {
            const $input = $(this);

            if ($input.hasClass("hasDatepicker")) {
                try {
                    $input.datepicker("destroy");
                } catch (e) { }
            }

            $input
                .removeClass("hasDatepicker")
                .removeAttr("id");

            $input.datepicker({
                dateFormat: "dd-mm-yy",
                appendTo: "body",

                onSelect: function (dateText) {
                    $(this).val(dateText);
                    $(this).trigger("input");
                    $(this).trigger("change");
                    $(this).blur();
                },

                beforeShow: function (input, inst) {
                    activeDateInput = input;

                    setTimeout(function () {
                        inst.dpDiv.css("z-index", 99999);
                    }, 0);
                },

                onClose: function () {
                    activeDateInput = null;
                }
            });
        });
    }
        function reinitializeDynamicSelect2(scope) {
            if (!window.jQuery || !$.fn || !$.fn.select2) return;

            const $scope = $(scope);

            $scope.find("select.select2-dynamic, select.select2").each(function () {
                const $el = $(this);

                if ($el.hasClass("select2-hidden-accessible")) {
                    $el.select2("destroy");
                }

                $el.select2({
                    width: "100%",
                    allowClear: false
                });
            });
        }
        function syncHeaderCheckboxState() {
            if (!window.jQuery) return;

            const total = $(".row-checkbox").length;
            const checked = $(".row-checkbox:checked").length;

            $("#selectAllRows").prop("checked", total > 0 && total === checked);
            $("#selectAllRows").prop("indeterminate", checked > 0 && checked < total);
        }

}); function showLoader() {
    $("#loading").show();
}

function hideLoader() {
    $("#loading").hide();
}

$(document).ajaxStart(function () {
    showLoader();
});

$(document).ajaxStop(function () {
    hideLoader();
});

$("#refreshBtn").off("click.loader").on("click.loader", function () {
    showLoader();

    setTimeout(function () {
        location.reload();
    }, 300);
});

$("#validateBtn, #copyRowbtn","#deleteBtn","#sendBtn","#saveBtn").off("click.loader").on("click.loader", function () {
    showLoader();

    setTimeout(function () {
        hideLoader();
    }, 600);
});

