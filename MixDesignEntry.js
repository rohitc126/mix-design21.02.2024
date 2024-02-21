

$('#tblList').on("blur", ".cal", function () {

    var ind = $(this).attr('id').split("_")[2];
    var SSD_Mix = $('#Material_Data_' + ind + '__SSD_Mix').val();
    var Correction_Value = $('#Material_Data_' + ind + '__Correction_Value').val();

    if (SSD_Mix == "") {
        SSD_Mix = "0"
    }

    if (Correction_Value == "") {
        Correction_Value = "0"
    }
    var Correct_Mix = parseFloat(SSD_Mix) + parseFloat(Correction_Value);
    $('#Material_Data_' + ind + '__Correct_Mix').val(Correct_Mix);
});

$('#tblList').on("click", "[id *= iAdd_]", function () {
    var ind = $(this).closest('tr').find('[id*=iAdd_]').attr('id').split("_")[1];

    var Material_Id = $('#Material_Data_' + ind + '__Material_Id').val();
    var Brand = $('#Material_Data_' + ind + '__Brand').val();
    var Unit = $('#Material_Data_' + ind + '__Unit').val();
    var SSD_Mix = $('#Material_Data_' + ind + '__SSD_Mix').val();
    var Absorption = $('#Material_Data_' + ind + '__Absorption').val();
    var Moisture = $('#Material_Data_' + ind + '__Moisture').val();
    var Correction_Value = $('#Material_Data_' + ind + '__Correction_Value').val();
    var Correct_Mix = $('#Material_Data_' + ind + '__Correct_Mix').val();
    var TRIAL_BATCH = $('#Material_Data_' + ind + '__TRIAL_BATCH').val();
    var MaterialBatch_No = $('#Material_Data_' + ind + '__MaterialBatch_No').val();

    var errCnt = 0;

    if (Material_Id == '') {
        $('#spnErr_ddlMaterial_' + ind).text('Select Material').show();
        errCnt = errCnt + 1;
    }
    else {
        $('#spnErr_ddlMaterial_' + ind).text('').hide();
    }

    if (SSD_Mix == '' || SSD_Mix == '0') {
        $('#spnErr_txtSSD_Mix_' + ind).text('Enter SSD Mix').show();
        errCnt = errCnt + 1;
    }
    else {
        $('#spnErr_txtSSD_Mix_' + ind).text('').hide();
    }

    if (errCnt == 0) {

        CreateMaterial_List(Material_Id, Brand, Unit, SSD_Mix, Absorption, Moisture, Correction_Value, Correct_Mix, TRIAL_BATCH, MaterialBatch_No);

        $('#Material_Data_' + ind + '__Material_Id').val('').change();
        $('#Material_Data_' + ind + '__Brand').val('');
        $('#Material_Data_' + ind + '__Unit').val('').change();
        $("#Material_Data_" + ind + "__SSD_Mix").val('');
        $("#Material_Data_" + ind + "__Absorption").val('');
        $("#Material_Data_" + ind + "__Moisture").val('');
        $("#Material_Data_" + ind + "__Correction_Value").val('');
        $("#Material_Data_" + ind + "__Correct_Mix").val('');
        $("#Material_Data_" + ind + "__TRIAL_BATCH").val('');
        $("#Material_Data_" + ind + "__MaterialBatch_No").val('');

    }
});

var CreateMaterial_List = function (Material_Id, Brand, Unit, SSD_Mix, Absorption, Moisture, Correction_Value, Correct_Mix, TRIAL_BATCH, MaterialBatch_No) {
    var i = $('#tbodyList [id*=iDel_]').length;
    var selectMaterial = $('#tbodyList [id*=Material_Data_0__Material_Id]');
    var selectUnit = $('#tbodyList [id*=Material_Data_0__Unit]');
    var body = '';
    body += '<tr>';

    //body += '<td data-title="Sr. No." style="text-align: center; padding-top: 17px;">';
    //body += '<span id="spnSrNo_' + i + '" >' + parseInt(i + 1) + '</span>';
    //body += '</td>';

    body += '<td data-title="Material">';
    body += '<select class="form-control" data-placeholder="Select Material" id="Material_Data_' + i + '__Material_Id" name="Material_Data[' + i + '].Material_Id"  style = "width:100%;" >';
    body += selectMaterial.html();
    body += '</select>';
    body += '<span id="spnErr_ddlMaterial_' + i + '" class="field-validation-error"></span>';
    body += '</td>';

    body += '<td data-title="Brand" >';
    body += '<input autocomplete="off" class="form-control" id="Material_Data_' + i + '__Brand" maxlength="250" name="Material_Data[' + i + '].Brand" type="text" value="' + Brand + '">'
    body += '</td>';

    body += '<td data-title="Unit">';
    body += '<select class="form-control" data-placeholder="Select Unit" id="Material_Data_' + i + '__Unit" name="Material_Data[' + i + '].Unit" style = "width:100%;" >';
    body += selectUnit.html();
    body += '</select>';
    body += '</td>';

    body += '<td data-title="SSD Mix" >';
    body += '<input autocomplete="off" class="form-control numeric" id="Material_Data_' + i + '__SSD_Mix" maxlength="12" name="Material_Data[' + i + '].SSD_Mix" type="text" value="' + SSD_Mix + '">'
    body += '<span id="spnErr_txtSSD_Mix_' + i + '" class="field-validation-error"></span>';
    body += '</td>';

    body += '<td data-title="Absorption" >';
    body += '<input autocomplete="off" class="form-control numeric" id="Material_Data_' + i + '__Absorption" maxlength="12" name="Material_Data[' + i + '].Absorption" type="text" value="' + Absorption + '">'
    body += '</td>';

    body += '<td data-title="Moisture" >';
    body += '<input autocomplete="off" class="form-control numeric" id="Material_Data_' + i + '__Moisture" maxlength="12" name="Material_Data[' + i + '].Moisture" type="text" value="' + Moisture + '">'
    body += '</td>';

    body += '<td data-title="Correction Value" >';
    body += '<input autocomplete="off" class="form-control" id="Material_Data_' + i + '__Correction_Value" maxlength="12" name="Material_Data[' + i + '].Correction_Value" type="text" step="any" type="number" value="' + Correction_Value + '">'
    body += '</td>';

    body += '<td data-title="Correct Mix" >';
    body += '<input autocomplete="off" class="form-control numeric" id="Material_Data_' + i + '__Correct_Mix" maxlength="12" name="Material_Data[' + i + '].Correct_Mix" type="text" value="' + Correct_Mix + '">'
    body += '</td>';

    body += '<td data-title="Trail Batch" >';
    body += '<input autocomplete="off" class="form-control numeric" id="Material_Data_' + i + '__TRIAL_BATCH" maxlength="12" name="Material_Data[' + i + '].TRIAL_BATCH" type="text" value="' + TRIAL_BATCH + '">'
    body += '</td>';

    body += '<td data-title="Material Batch No" >';
    body += '<input autocomplete="off" class="form-control" id="Material_Data_' + i + '__MaterialBatch_No" maxlength="30" name="Material_Data[' + i + '].MaterialBatch_No" type="text" value="' + MaterialBatch_No + '">'
    body += '</td>';


    body += '<td data-title="Action" style="font-size:20px;padding-top:17px;">';
    if (i == 0) {
        body += '<i class="fa fa-plus" aria-hidden="true" style="cursor:pointer;display:block;" id="iAdd_' + i + '"></i>';
        body += '<i class="fa fa-close" aria-hidden="true" style="cursor:pointer;display:none;color:red;" id="iDel_' + i + '"></i>';
    }
    else {
        body += '<i class="fa fa-plus" aria-hidden="true" style="cursor:pointer;display:none;" id="iAdd_' + i + '"></i>';
        body += '<i class="fa fa-close" aria-hidden="true" style="cursor:pointer;display:block;color:red;" id="iDel_' + i + '"></i>';
    }

    body += '</td>';
    body += '</tr>';

    $('#tbodyList').append(body);


    $('#Material_Data_' + i + '__Material_Id').select2().val(Material_Id).change();
    $('#Material_Data_' + i + '__Unit').select2().val(Unit).change();
}


//$('#tblList').on("click", "[id*=iDel_]", function () {
//    $(this).closest('tr').remove();
//    ArrangeIndex();
//});

$('#tbodyList').on("click", "[id*=iDel_]", function () {
    var ind = $(this).closest('tr').find('[id*=iDel_]').attr('id').split("_")[1];
    $(this).closest('tr').remove();

    var len = $('[id*=iDel_]').length;
    for (x = 0; x < len; x++) {
        if (x >= parseInt(ind)) {
            $('#Material_Data_' + (x + 1) + '__Material_Id').attr('id', 'Material_Data_' + x + '__Material_Id').attr('name', 'Material_Data[' + x + '].Material_Id');
            $('#Material_Data_' + (x + 1) + '__Brand').attr('id', 'Material_Data_' + x + '__Brand').attr('name', 'Material_Data[' + x + '].Brand');
            $('#Material_Data_' + (x + 1) + '__Unit').attr('id', 'Material_Data_' + x + '__Unit').attr('name', 'Material_Data[' + x + '].Unit');
            $('#Material_Data_' + (x + 1) + '__SSD_Mix').attr('id', 'Material_Data_' + x + '__SSD_Mix').attr('name', 'Material_Data[' + x + '].SSD_Mix');
            $('#Material_Data_' + (x + 1) + '__Absorption').attr('id', 'Material_Data_' + x + '__Absorption').attr('name', 'Material_Data[' + x + '].Absorption');

            $('#Material_Data_' + (x + 1) + '__Moisture').attr('id', 'Material_Data_' + x + '__Moisture').attr('name', 'Material_Data[' + x + '].Moisture');
            $('#Material_Data_' + (x + 1) + '__Correction_Value').attr('id', 'Material_Data_' + x + '__Correction_Value').attr('name', 'Material_Data[' + x + '].Correction_Value');
            $('#Material_Data_' + (x + 1) + '__Correct_Mix').attr('id', 'Material_Data_' + x + '__Correct_Mix').attr('name', 'Material_Data[' + x + '].Correct_Mix');
            $('#Material_Data_' + (x + 1) + '__TRIAL_BATCH').attr('id', 'Material_Data_' + x + '__TRIAL_BATCH').attr('name', 'Material_Data[' + x + '].TRIAL_BATCH');
            $('#Material_Data_' + (x + 1) + '__MaterialBatch_No').attr('id', 'Material_Data_' + x + '__MaterialBatch_No').attr('name', 'Material_Data[' + x + '].MaterialBatch_No');

            //$('#spnSrNo_' + (x + 1)).attr('id', 'spnSrNo_' + x).text(x + 1);
            $('#iAdd_' + (x + 1)).attr('id', 'iAdd_' + x);
            $('#iDel_' + (x + 1)).attr('id', 'iDel_' + x);
        }
    }
});

/*Remarks*/

$('#tblRemarksList').on("click", "[id *= iAddRemarks_]", function () {
    var ind = $(this).closest('tr').find('[id*=iAddRemarks_]').attr('id').split("_")[1];
    var Time = $('#Remarks_Data_' + ind + '__Time').val();
    //var Slump = $('#Remarks_Data_' + ind + '__Slump').val();
    //var IsSelected = $('#chkCollapse_' + ind).prop('checked');
    var IsSelected = $('#chkCollapse_' + ind).is(":checked");
    var Slump = '';
    var Unit = $('#Remarks_Data_' + ind + '__Unit').val();
    var Notes = $('#Remarks_Data_' + ind + '__Notes').val();
    //alert($('#chkCollapse_' + ind).is(":checked"));

    if ($('#chkCollapse_' + ind).is(":checked")) {
        Slump = 'Collapse';
        $('#Remarks_Data_' + ind + '__Slump').prop('readonly', true);
        $('#spnErr_txtSlump_' + ind).text('').hide();

    } else {
        $('#Remarks_Data_' + ind + '__Slump').prop('readonly', false);
        Slump = $(this).closest('tr').find('[id^=txtSlump_]').val();
        $('#spnErr_txtSlump_' + ind).text('Enter Slump/Flow Details').show();
      
        //if (Slump === '') {
        //    $('#spnErr_txtSlump_' + ind).text('Enter Slump/Flow Details').show();
        //    return;
        //} else {
        //    $('#spnErr_txtSlump_' + ind).text('').hide();
        //}
    }

    var errCnt = 0;
    if (Time == '') {
        $('#spnErr_ddlTime_' + ind).text('Select Time').show();
        errCnt = errCnt + 1;
    }
    else {
        $('#spnErr_ddlTime_' + ind).text('').hide();
    }


    if (!($('#chkCollapse_' + ind).is(":checked")) && Slump === '') {
        $('#spnErr_txtSlump_' + ind).text('Enter Slump/Flow Details').show();
        errCnt = errCnt + 1;
    } else {
        $('#spnErr_txtSlump_' + ind).text('').hide();
    }
    if (errCnt == 0) {

        CreateRemarks_List(Time, IsSelected, Slump, Unit, Notes);
        $('#Remarks_Data_' + ind + '__Time').val('').change();
        $('#chkCollapse_0').prop('checked', false);
        $('#txtSlump_0').val('').prop('readonly', false);
       //$('#Remarks_Data_' + ind + '__Slump').val('').prop('readonly', false);
        $('#Remarks_Data_' + ind + '__Unit').val('').change();
        $('#Remarks_Data_' + ind + '__Notes').val('');
    }
});

$('#tblRemarksList').on('click', '[id^=chkCollapse_]', function () {
    var ind = $(this).attr('id').split("_")[1];
    var IsSelected = $(this).is(':checked');
    var $slumpInput = $('#Remarks_Data_' + ind + '__Slump');
    var $errorSpan = $('#spnErr_txtSlump_' + ind);

    if (IsSelected) {
        $slumpInput.prop('readonly', true);
        $slumpInput.val('');
        $errorSpan.text('').hide();

        //$('#Remarks_Data_' + ind + '__Unit').select2().val('').attr("style", "pointer-events: none;");
        

    } else {
        $slumpInput.prop('readonly', false);
        if ($slumpInput.val() === '') {
            $errorSpan.text('Enter Slump/Flow Details').show();
            //$('#Remarks_Data_' + ind + '__Unit').select2().val('').attr("style", "pointer-events: all;");
        }
    }
});

var CreateRemarks_List = function (Time, IsSelected, Slump, Unit, Notes) {   

    var i = $('#tbodyRemarksList [id*=iDelRemarks_]').length;
    var selectUnit = $('#tbodyRemarksList [id*=Remarks_Data_0__Unit]');
    var selectTime = $('#tbodyRemarksList [id*=Remarks_Data_0__Time]');
    var body = '';
    body += '<tr>';
    body += '<td data-title="Sl. No." style="text-align: center; padding-top: 17px;">';
    body += '<span id="spnSrNo_' + i + '" >' + parseInt(i + 1) + '</span>';
    body += '</td>';

    body += '<td data-title="Time">';
    body += '<select class="form-control" data-placeholder="Select Time" id="Remarks_Data_' + i + '__Time" name="Remarks_Data[' + i + '].Time"  style = "width:100%;" >';
    body += selectTime.html();
    body += '</select>';
    body += '<span id="spnErr_ddlTime_' + i + '" class="field-validation-error"></span>';
    body += '</td>';


    body += '<td data-title="Slump/Flow">';
    //body += '<div class="form-check">';
    body += '<label class="col-sm-12 col-md-2 col-form-label" for="chkCollapse_' + i + '">Collapse</label>';
    if (IsSelected) {
        body += '<input class="form-check-input vmSTRATEGY ml-5" type="checkbox" id="chkCollapse_' + i + '" name="Remarks_Data[' + i + '].IsSelected" value="true" checked>';
    } else {
        body += '<input class="form-check-input vmSTRATEGY ml-5" type="checkbox" id="chkCollapse_' + i + '" name="Remarks_Data[' + i + '].IsSelected" value="false" >';
    }
   // body += '</div>';
    body += '</td>';


    body += '<td data-title="Slump/Flow">';
    if (Slump == 'Collapse') {
        body += '<input class="form-control" id="Remarks_Data_' + i + '__Slump" maxlength="250" name="Remarks_Data[' + i + '].Slump" type="text" value="">'
    } else {
        body += '<input class="form-control" id="Remarks_Data_' + i + '__Slump" maxlength="250" name="Remarks_Data[' + i + '].Slump" type="text" value="' + Slump + '">'

    }body += '</td>';


    body += '<td data-title="Unit">';
    body += '<select class="form-control" data-placeholder="Select Unit" id="Remarks_Data_' + i + '__Unit" name="Remarks_Data[' + i + '].Unit"  style = "width:100%;">';
    body += selectUnit.html();
    body += '</select>';
    body += '</td>';

    body += '<td data-title="Notes">';
    body += '<textarea autocomplete="off" class="form-control" cols="20" id="Remarks_Data_' + i + '__Notes" maxlength="250" name="Remarks_Data[' + i + '].Notes" rows="2" style="height:45px;">' + Notes + '</textarea>';
    body += '</td>';


    body += '<td data-title="Action" style="font-size:20px;padding-top:17px;">';
    if (i == 0) {
        body += '<i class="fa fa-plus" aria-hidden="true" style="cursor:pointer;display:block;" id="iAddRemarks_' + i + '"></i>';
        body += '<i class="fa fa-close" aria-hidden="true" style="cursor:pointer;display:none;color:red;" id="iDelRemarks_' + i + '"></i>';
    }
    else {
        body += '<i class="fa fa-plus" aria-hidden="true" style="cursor:pointer;display:none;" id="iAddRemarks_' + i + '"></i>';
        body += '<i class="fa fa-close" aria-hidden="true" style="cursor:pointer;display:block;color:red;" id="iDelRemarks_' + i + '"></i>';
    }

    body += '</td>';
    body += '</tr>';

    $('#tbodyRemarksList').append(body);
    $('#input[type=text][id*="Remarks_Data_0__Slump"]').val('');

    $('#Remarks_Data_' + i + '__Unit').select2().val(Unit).change();
    $('#Remarks_Data_0__Unit').select2().val('').change();

    $('#Remarks_Data_' + i + '__Time').select2().val(Time).change();
    $('#Remarks_Data_0__Time').select2().val('').change();
   
}

$('#tblRemarksList').on("click", "[id*=iDelRemarks_]", function () {
    $(this).closest('tr').remove();
    ArrangeIndex();
});
$('#tbodyRemarksList').on("click", "[id*=iDelRemarks_]", function () {
    var ind = $(this).closest('tr').find('[id*=iDelRemarks_]').attr('id').split("_")[1];
    $(this).closest('tr').remove();

    var len = $('[id*=iDelRemarks_]').length;
    for (x = 0; x < len; x++) {
        if (x >= parseInt(ind)) {
            $('#Remarks_Data_' + (x + 1) + '__Time').attr('id', 'Remarks_Data_' + x + '__Time').attr('name', 'Remarks_Data_[' + x + '].Time');
            $('#Remarks_Data_' + (x + 1) + '__Unit').attr('id', 'Remarks_Data_' + x + '__Unit').attr('name', 'Remarks_Data_[' + x + '].Unit');
            $('#Remarks_Data_' + (x + 1) + '__Notes').attr('id', 'Remarks_Data_' + x + '__Notes').attr('name', 'Remarks_Data_[' + x + '].Notes');
          
            $('#spnSrNo_' + (x + 1)).attr('id', 'spnSrNo_' + x).text(x + 1);
            $('#iAddRemarks_' + (x + 1)).attr('id', 'iAddRemarks_' + x);
            $('#iDelRemarks_' + (x + 1)).attr('id', 'iDelRemarks_' + x);
        }
    }
});


$('#btnSubmit').click(function (e) {
    var errCnt = ValidateTable();

    if (errCnt > 0) {
        e.preventDefault();
        return false;
    }

});

function ValidateTable() {
    var errCnt = 0;
    var rowsCount = $('#tblList [id*=iDel_]').length;
    if (rowsCount == 1) {
        $('#tblList [id*=iDel_]').each(function () {
            var ind = $(this).attr('id').split("_")[1];

            var Material_Id = $('#Material_Data_' + ind + '__Material_Id').val();
            var Brand = $('#Material_Data_' + ind + '__Brand').val();
            var Unit = $('#Material_Data_' + ind + '__Unit').val();
            var SSD_Mix = $('#Material_Data_' + ind + '__SSD_Mix').val();
            var Absorption = $('#Material_Data_' + ind + '__Absorption').val();
            var Moisture = $('#Material_Data_' + ind + '__Moisture').val();
            var Correction_Value = $('#Material_Data_' + ind + '__Correction_Value').val();
            var Correct_Mix = $('#Material_Data_' + ind + '__Correct_Mix').val();
            var TRIAL_BATCH = $('#Material_Data_' + ind + '__TRIAL_BATCH').val();
            var MaterialBatch_No = $('#Material_Data_' + ind + '__MaterialBatch_No').val();

            if (Material_Id == '') {
                $('#spnErr_ddlMaterial_' + ind).text('Select Material').show();
                errCnt = errCnt + 1;
            }
            else {
                $('#spnErr_ddlMaterial_' + ind).text('').hide();
            }

            if (SSD_Mix == '' || SSD_Mix == '0') {
                $('#spnErr_txtSSD_Mix_' + ind).text('Enter SSD Mix').show();
                errCnt = errCnt + 1;
            }
            else {
                $('#spnErr_txtSSD_Mix_' + ind).text('').hide();
            }

        });
    }
    else {
        $('#tblList [id*=iDel_]').each(function () {
            var ind = $(this).attr('id').split("_")[1];

            var Material_Id = $('#Material_Data_' + ind + '__Material_Id').val();
            var Brand = $('#Material_Data_' + ind + '__Brand').val();
            var Unit = $('#Material_Data_' + ind + '__Unit').val();
            var SSD_Mix = $('#Material_Data_' + ind + '__SSD_Mix').val();
            var Absorption = $('#Material_Data_' + ind + '__Absorption').val();
            var Moisture = $('#Material_Data_' + ind + '__Moisture').val();
            var Correction_Value = $('#Material_Data_' + ind + '__Correction_Value').val();
            var Correct_Mix = $('#Material_Data_' + ind + '__Correct_Mix').val();
            var TRIAL_BATCH = $('#Material_Data_' + ind + '__TRIAL_BATCH').val();
            var MaterialBatch_No = $('#Material_Data_' + ind + '__MaterialBatch_No').val();



            if (Material_Id != '' || SSD_Mix != '') {

                if (Material_Id == '') {
                    $('#spnErr_ddlMaterial_' + ind).text('Select Material').show();
                    errCnt = errCnt + 1;
                }
                else {
                    $('#spnErr_ddlMaterial_' + ind).text('').hide();
                }

                if (SSD_Mix == '' || SSD_Mix == '0') {
                    $('#spnErr_txtSSD_Mix_' + ind).text('Enter SSD Mix').show();
                    errCnt = errCnt + 1;
                }
                else {
                    $('#spnErr_txtSSD_Mix_' + ind).text('').hide();
                }

            }

        });
    }


    return errCnt;
}
