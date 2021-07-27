var DemoGraphVM = {
    DemographicTypeDtls: [],
    DemographicType: {
        DemoTypeId:0,
        TypeDescAr: "",
        TypeDescEn: "",

    }
};
var totalWeight = 0;

$(document).ready(function () {
    totalWeight = 0;
    loadData();

    
   
});

//Load Data function  
function loadData() {
    $.ajax({
        url: "/DemoGraphType/GetAll",
        type: "POST",
        dataType: "json",
        success: function (result) {
            console.log(result)
            var hide = 0;
            var show = 1;
            console.log($("#DemoGraphDTLTBody tr td input"))
            var index = 1;
            var html = '';
            result.forEach(item => {
                html += '<tr>';
                html += '<td>' + index + '</td>';
                html += '<td>' + item.typeDescAr + '</td>';
                html += '<td>' + item.typeDescEn + '</td>';
                html += '<td><button class="btn btn-primary" onclick="return getbyID(' + item.demoTypeId + ',' + true + ',' + hide + ')">View</button>  <button class="btn btn-primary" onclick="return getbyID(' + item.demoTypeId + ',' + false + ',' + show + ')">Edit</button>  <button class="btn btn-danger" onclick="Delete(' + item.demoTypeId + ')">Delete</button></td>';
                html += '</tr>';
                index++;
            });
            $('#mainTableTBody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function RemoveDataFromDemoGraph() {
    debugger;
    //if (DemoGraphVM.DemographicTypeDtls.length>0) {
    //    DemoGraphVM.DemographicTypeDtls.pop();
    //    $("#DemoGraphDTLTBody tr").last().remove();
    //}
    if ($("#DemoGraphDTLTBody tr").length > 0) {
        $("#DemoGraphDTLTBody tr").last().remove();
    }
}

function AddDataToDemoGraph() {
    debugger;
var res = validate();
    if (res == false) {
        return false;
    }
    
    var DemographicTypeDtlVM = {
        DemTypeDtlId: $('#ID').val(),
        ChoiceAr: $('.AnswerAR').last().val(),
        ChoiceEn: $('.AnswerEN').last().val(),
        WeightValue: $('.Weight').last().val(),
    };
    
    if ((DemographicTypeDtlVM.ChoiceAr == null || DemographicTypeDtlVM.ChoiceAr == "" || DemographicTypeDtlVM.ChoiceAr == undefined) && (DemographicTypeDtlVM.ChoiceEn == null || DemographicTypeDtlVM.ChoiceEn == "" || DemographicTypeDtlVM.ChoiceEn == undefined) && (DemographicTypeDtlVM.WeightValue == null || DemographicTypeDtlVM.WeightValue == "" || DemographicTypeDtlVM.WeightValue == undefined)) {
        return false;
    } else {
        var html = '';
        html += '<tr class="AnswerData">';
        html += '<td><input type="text" class="form-control AnswerAR"  placeholder="AnswerAR" /></td>';
        html += '<td><input type="text" class="form-control AnswerEN"  placeholder="AnswerEN"/></td>';
        html += '<td><input type="text" class="form-control Weight"  placeholder="Weight" /></td>';
        html += '</tr>';
        $("#DemoGraphDTLTBody").append(html);
    }
    
    
}

//Function for getting the Data Based upon Employee ID  
function getbyID(ID,status,toggle) {
    debugger;
    $.ajax({
        url: "/DemoGraphTypeDTL/GetAllByTypeID",
        type: "POST",
        dataType: "json",
        data: { id: ID },
        success: function (result) {
            debugger;
            if (result != null) {
                console.log(result);
                var html = '';
                var readonly = "";
                $("#DemoGraphDTLTBody").html('');
                $('#TypeAR').val(result[0].demoTypeVM.typeDescAr).attr("readonly", status);
                $('#TypeEN').val(result[0].demoTypeVM.typeDescEn).attr("readonly", status);
                if (status) {
                    readonly = "readonly";
                } else {
                    readonly=""
                }
                result.forEach(item => {
                    html += '<tr>';
                    //html += '<td>' + item.choiceAr + '</td>';
                    //html += '<td>' + item.choiceEn + '</td>';
                    //html += '<td>' + item.weightValue + '</td>';
                    html += '<td><input type="hidden" class="form-control DemoGraphTypeID" value="' + item.demoTypeId + '"><input type="hidden" class="form-control DemoGraphDTLTypeID" value="' + item.demTypeDtlId + '"><input type="text" class="form-control AnswerAR" value="' + item.choiceAr + '" placeholder="AnswerAR" ' + readonly + '></td>';
                    html += '<td><input type="text" class="form-control AnswerEN" value="' + item.choiceEn + '" placeholder="AnswerEN" '+ readonly +'></td>';
                    html += '<td><input type="text" class="form-control Weight" value="' + item.weightValue + '" placeholder="Weight"  '+ readonly +'></td>';
                    //html += '<td><button class="btn btn-danger" onclick="DeleteDTL(' + item.choiceAr+','+ item.choiceEn+','+ item.weightValue+ ')">Delete</button></td>';
                    html += '</tr>';
                });
                $("#DemoGraphDTLTBody").append(html);
                if (toggle==1) {
                    $("#saveBtn").show();
                    $("#addBtnRow").show();
                    $("#resetBtn").hide();
                    $("#myModalLabel").text("Edit Demo Graph Type Details");
                } else {
                    $("#saveBtn").hide();
                    $("#addBtnRow").hide();
                    $("#resetBtn").hide();
                    $("#myModalLabel").text("View Demo Graph Type Details");
                }

                $('#myModal').modal('show');
                
            } 
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

function getDTLbyID(ID) {
    debugger;
    $('#TypeAR').css('border-color', 'lightgrey');
    $('#TypeEN').css('border-color', 'lightgrey');
    $('#AnswerAR').css('border-color', 'lightgrey');
    $('#AnswerEN').css('border-color', 'lightgrey');
    $('#Weight').css('border-color', 'lightgrey');
    $.ajax({
        url: "/DemoGraphTypeDTL/GetByID",
        type: "POST",
        dataType: "json",
        data: { id: ID },
        success: function (result) {
            if (result != null) {
                console.log(result);
                $('#ID').val(result.demTypeDtlId);
                $('#TypeAR').val(result.demoTypeVM.typeDescAr);
                $('#TypeEN').val(result.demoTypeVM.typeDescEn);
                $('#AnswerAR').val(result.choiceAr);
                $('#AnswerEN').val(result.choiceEn);
                $('#Weight').val(result.weightValue);

                $('#myModal').modal('show');
                $("#myModalLabel").text("View Demo Graph Type Details");
            } else {
                alert("DemoGraphTypeDTL Not Found!")
            }

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

//Add Data Function
function AddORUpdateDemoGraph() {
    debugger;
    var res = validate();
    if (res == false) {
        return false;
    }
    totalWeight = 0;
    DemoGraphVM.DemographicTypeDtls = [];
    DemoGraphVM.DemographicType.DemoTypeId = $('.DemoGraphTypeID').first().val();
    DemoGraphVM.DemographicType.TypeDescAr = $('#TypeAR').val();
    DemoGraphVM.DemographicType.TypeDescEn = $('#TypeEN').val();

    $("#DemoGraphDTLTBody tr").each(function (index, el) {
        debugger;
        console.log(el.childNodes[0].childNodes[0])
        var DemographicTypeDtlVM = {
            DemoTypeId: el.childNodes[0].childNodes[0].value,
            DemTypeDtlId: el.childNodes[0].childNodes[1].value,
            ChoiceAr: el.childNodes[0].childNodes[2].value,
            ChoiceEn: el.childNodes[1].childNodes[0].value,
            WeightValue: el.childNodes[2].childNodes[0].value,
        };

        DemoGraphVM.DemographicTypeDtls.push(DemographicTypeDtlVM);
    })

    
    $(".Weight").each(function (index, el) {
        totalWeight = totalWeight + parseInt(el.value);
    })
    if (DemoGraphVM.DemographicTypeDtls != null && DemoGraphVM.DemographicTypeDtls != undefined) {
        if (totalWeight != 100) {
            if (isNaN(totalWeight)) {
                alert("Total Weight must be number & equal 100");
                return false;
            }
            alert("Total Weight must equal 100");
            return false;
        }
        
        $.ajax({
            url: "/DemoGraphTypeDTL/AddORUpdate",
            type: "POST",
            //dataType: "json",
            data: { demoGraphVM: DemoGraphVM },
            success: function (result) {
                DemoGraphVM.DemographicTypeDtls = [];
                totalWeight = 0;
                if (result != false) {
                    loadData();
                    $('#myModal').modal('hide');
                } else {
                    alert("Demo Graph Not Added! Something went wrong!")
                }


            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
  
}

//function for deleting Details record  
function Delete(ID) {
    debugger;
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/DemoGraphType/Delete",
            type: "POST",
            //dataType: "json",
            data: { id: ID },
            success: function (result) {
                debugger;
                if (result != false) {
                    loadData();
                } else {
                    alert("Delete is Failed!");
                }
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

//function for deleting Details record  
function DeleteDTL(chAR,chEN,Weight) {
    debugger;
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        if (chAR != null && chAR != null && chEN != null && chEN != null && Weight != null && Weight != undefined) {
            var newDemographicTypeDtls = DemoGraphVM.DemographicTypeDtls.filter(e => e.ChoiceAr != chAR &&e.ChoiceEn != chEN && e.WeightValue != Weight);
            DemoGraphVM.DemographicTypeDtls = newDemographicTypeDtls;
            $(this).parent().parent().remove();
        }
    }
}

//Function for clearing the textboxes  
function clearTextBox() {
    $('#ID').val("");
    $('.AnswerAR').val("");
    $('.AnswerEN').val("");
    $('.Weight').val("");
    
    $('.AnswerAR').css('border-color', 'lightgrey');
    $('.AnswerEN').css('border-color', 'lightgrey');
    $('.Weight').css('border-color', 'lightgrey');
}

function clearModal() {
    $("#myModalLabel").text("Add Demo Graph Type Details");
    totalWeight = 0;
    clearTextBox();
    DemoGraphVM.DemographicTypeDtls = [];
    $("#saveBtn").show();
    $("#addBtnRow").show();
    $("#resetBtn").show();
    $('#TypeAR').val('').attr("readonly", false);
    $('#TypeEN').val('').attr("readonly", false);
   
    $('#TypeAR').css('border-color', 'lightgrey');
    $('#TypeEN').css('border-color', 'lightgrey');
    $("#DemoGraphDTLTBody").html('');
    var html = '';
    html += '<tr>';
    html += '<td><input type="text" class="form-control AnswerAR"  placeholder="AnswerAR" /></td>';
    html += '<td><input type="text" class="form-control AnswerEN"  placeholder="AnswerEN"/></td>';
    html += '<td><input type="text" class="form-control Weight"  placeholder="Weight" /></td>';
    html += '</tr>';
    $("#DemoGraphDTLTBody").append(html);
    
}

//Validation using jquery  
function validate() {
    var isValid = true;
    if ($('#TypeAR').val().trim() == "") {
        $('#TypeAR').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#TypeAR').css('border-color', 'lightgrey');
    }
    if ($('#TypeEN').val().trim() == "") {
        $('#TypeEN').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#TypeEN').css('border-color', 'lightgrey');
    }
    if ($('.AnswerAR').val() == "") {
        $('.AnswerAR').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('.AnswerAR').css('border-color', 'lightgrey');
    }
    if ($('.AnswerEN').val() == "") {
        $('.AnswerEN').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('.AnswerEN').css('border-color', 'lightgrey');
    }
    if ($('.Weight').val() == "") {
        $('.Weight').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('.Weight').css('border-color', 'lightgrey');
    }
   
    return isValid;
}

