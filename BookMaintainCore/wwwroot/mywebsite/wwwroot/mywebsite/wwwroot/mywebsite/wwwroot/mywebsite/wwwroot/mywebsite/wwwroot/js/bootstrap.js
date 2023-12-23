//東西都是要分開避免高內聚
//Bootstarp有些東西需要拆分不要做一起
let systemData = {
    data: {},
    initPage: {
        startPage: 1,
        endPage: 3,
        pageTotal: 0
    },
    tableData: {},
    columnsData: {}
};

function cangeUrl(href) {
    //可以ajax驗證登入情況
    //e.preventDefault();

    //window.
    //document.
    //location.reload();
    location.href = href;
}

function createLable(key, labelName, gropName) {
    let labelAttr = {
        'for': key,
        'class': "form-label",
        'gropName': gropName
    };
    let virtualLable = $("<label></label>").attr(labelAttr).text(labelName);

    return virtualLable;
}

function createLable2(key, labelName, gropName) {
    let labelAttr = {
        'for': key,
        'gropName': gropName
    };
    let virtualLable = $("<label></label>").attr(labelAttr).text(labelName);

    return virtualLable;
}

function createAutoCompleteElement(key, name, labelName, placeholder, validationObj) {
    let inputAttr = {
        'id': key,
        'name': name,
        'type': "text",
        'placeholder': placeholder,
        'aria-label': labelName,
        'class': "form-control"
    };

    if (validationObj) {
        inputAttr["required"] = 'required';
        //virtualInput.attr('aria-describedby', "validationTooltipUsernamePrepend"); //有sqan再家
    }

    let virtualInput = $("<input></input>").attr(inputAttr);

    let buttonAttr = {
        'type': "button",
        'data-bs-toggle': "dropdown",
        'class': "btn btn-outline-secondary dropdown-toggle",
        'aria-expanded': "false"
    };

    let virtualButton = $("<button></button>").attr(buttonAttr).text(labelName);

    let ulAttr = {
        'id': "Ul" + key,
        'class': "dropdown-menu dropdown-menu-end"
    };

    let virtualUl = $("<ul></ul>").attr(ulAttr);

    let divAttr = {
        'id': "div" + key
    };
    let virtualDiv;

    //驗證與非驗證
    if (validationObj) {
        divAttr['class'] = "input-group has-validation";
        virtualDiv = $("<div></div>").attr(divAttr).append(virtualInput).append(virtualButton).append(virtualUl);

        let validationDivAttr = {
            'class': "invalid-tooltip invisible"
        };

        let validationDiv = virtualDiv.append($("<div></div>").attr(validationDivAttr).text(validationObj.message));
        return validationDiv;
    } else {
        divAttr['class'] = "input-group";
        virtualDiv = $("<div></div>").attr(divAttr).append(virtualInput).append(virtualButton).append(virtualUl);
        return virtualDiv;
    }
}

function createAutoCompleteElement2(key, name, labelName, placeholder, validationObj, type) {
    
    let inputAttr = {
        'id': key,
        'name': name,
        'type': type,
        'require': validationObj,
        'placeholder': placeholder,
        'aria-label': labelName,
        'class': "form-control",
        'list': key + "listOptions",
        'aria-label': "Recipient's " + key,
        'aria-describedby':"basic-"+key
    };

    if (type == "file") {
        inputAttr['multiple'] = 'multiple';
    }

    let virtualInput = $("<input></input>").attr(inputAttr);

    let spanAttr = {
        class:"input-group-text",
        id: "basic-" + key,
    };
    let virtualSpan = $("<span></span>").attr(spanAttr).text(labelName);

    let divAttr = {
        'id': "div" + key,
        'class': "input-group mb-3"
    };
    let virtualDiv = $("<div></div>").attr(divAttr).append(virtualInput).append(virtualSpan);
    
    return virtualDiv;
}

function createInput(key, name, classs, placeholder, type, method) {
    let inputAttr = {
        'id': key,
        'name': name,
        'type': type,
        'placeholder': placeholder,
        'class': classs,
        'value':""
    };
    
    if (method) {
        $.each(method, function (columnsMethod, methodVal) {
            //console.log(columnsMethod);
            //console.log(methodVal);
            inputAttr[columnsMethod] = methodVal;
        });
    }
    
    let virtualInput = $("<input></input>").attr(inputAttr);

    return virtualInput;
}

function clearFromData(data) {
    $.each(data, function (columnsKey, columnsVal) {
        //console.log(columnsVal.type);
        if (columnsVal.type == "datetime-local") {
            let createDateObj = createDate();
            $(columnsVal).val(createDateObj.year + '-' + createDateObj.month + '-' + createDateObj.day
                + " " + createDateObj.getHours + ":" + createDateObj.getMinutes);
        } else {
            $(columnsVal).val("");
        }
        //console.log(columnsKey);
        //console.log(columnsVal);
    });
}

function createDropDownListElement(key, name, labelName, validationObj) {
    let selectAttr = {
        'id': key,
        'name': name,
        'type': "text",
        'class': "form-select"
    };

    if (validationObj) {
        selectAttr['required'] = "required";
    }

    let virtualSelect = $("<select></select>").attr(selectAttr);

    let labelAttr = {
        'for': key,
        'class': "input-group-text"
    };
    let virtualLabel = $("<label></label>").attr(labelAttr).text(labelName);


    let divAttr = {
        'id': "div" + key
    };

    let virtualDiv;

    //驗證與非驗證
    if (validationObj) {
        divAttr['class'] = "input-group mb-3 has-validation";
        virtualDiv = $("<div></div>").attr(divAttr).append(virtualSelect).append(virtualLabel);

        let validationDivAttr = {
            'class': "invalid-tooltip invisible"
        };

        let validationDiv = virtualDiv.append($("<div></div>").attr(validationDivAttr).text(validationObj.message));
        return validationDiv;
    } else {
        divAttr['class'] = "input-group mb-3";
        virtualDiv = $("<div></div>").attr(divAttr).append(virtualSelect).append(virtualLabel);
        return virtualDiv;
    }
}

function createTextAreaElement(key, name, title, labelName, validationObj) {
    let textareaAttr = {
        'id': key,
        'name': name,
        'placeholder': title,
        'class': "form-control",
        'aria-label': "With textarea"
    };

    let virtualTextArea = $("<textarea></textarea>").attr(textareaAttr);

    if (validationObj) {
        virtualTextArea.attr('required', "");
    }

    let spanAttr = {
        'class': "input-group-text"
    };
    let virtualSpan = $("<span></span>").attr(spanAttr).text(labelName);

    let divAttr = {
        'id': "div" + key
    };
    let virtualDiv;

    //驗證與非驗證
    if (validationObj) {
        divAttr['class'] = "input-group has-validation";
        virtualDiv = $("<div></div>").attr(divAttr).append(virtualTextArea).append(virtualSpan);

        let validationDivAttr = {
            'class': "invalid-tooltip invisible"
        };

        let validationDiv = virtualDiv.append($("<div></div>").attr(validationDivAttr).text(validationObj.message));
        return validationDiv;
    } else {
        divAttr['class'] = "input-group";
        virtualDiv = $("<div></div>").attr(divAttr).append(virtualTextArea).append(virtualSpan);
        return virtualDiv;
    }
}

function createButtonElement(liKey, key, labelName, className, method, modelName) {
    let buttonAttr = {
        'id': key,
        'class': className,
        'style': 'margin:5px 5px 0px 0px;',
        "type": "button"
    };

    $.each(method, function (columnsKey, columnsVal) {
        buttonAttr[columnsKey] = columnsVal;
    });

    if (modelName) {
        buttonAttr['data-bs-toggle'] = "modal";
        buttonAttr['data-bs-target'] = "#" + modelName;
    }

    let virtualButton = $("<button></button>").attr(buttonAttr).text(labelName);

    let divAttr = {
        'id': liKey,
        'class': "col-12"
    };

    let virtualDiv = null;
    if ($("#" + liKey).length > 0) {
        $("#" + liKey).append(virtualButton);
    } else {
        virtualDiv = $("<div></div>").attr(divAttr).append(virtualButton);
    }

    return virtualDiv;
}

function createTableElement(key, columns) {
    let trObject = $("<tr></tr>");
    let thObject = null, thAttr = null;
    //let columnsWidth = 0;，累加目前不是答案
    for (let i = 0; i < columns.length; i++) {
        thAttr = { scope: "col", class: "table-primary" };

        if (columns[i].width) {
            if (columns[i].width) {
                thAttr['width'] = columns[i].width;
                //columnsWidth += parseInt(columns[i].width.replace("px",""));
            }
        }

        thObject = $("<th></th>").attr(thAttr).text(columns[i].title);
        trObject.append(thObject);
    }

    let tableAttr = {
        'class': "table",
        'style': "table-layout: fixed;margin: 0px;" //底下新增資料時不會跑版
    };

    //display: block; 不加會讓th值跑掉
    //<div class='table-responsive'><div>
    //tbody或thread需要拿掉
    $("#" + key).append($("<table></table>")
        .attr(tableAttr)
        .append($("<thread ></thread>").attr({ style: "display: block;" }).append(trObject))
        .append($("<tbody></tbody>").attr({ style: "display: block;" }))
    );
}

function createDataPicker(key, name, containerModal, template, type, value) {
    let labelAttr = {
        'for': key
    };

    let virtualLabel = $("<label></label>").attr(labelAttr);

    let inputAttr = {
        'type': type,
        'id': key, //底下新增資料時不會跑版
        'name': name,
        'class': "form-control",
        //value: "2022/03/13 下午 08:13"
    };


    let createDateObj = createDate();
    let virtualInput = $("<input></input>").attr(inputAttr).val(createDateObj.year + '-' + createDateObj.month + '-' + createDateObj.day
        + " " + createDateObj.getHours + ":" + createDateObj.getMinutes);
    //let virtualInput = $("<input></input>").attr(inputAttr).val("2022/03/13 下午 08:13");
    //let virtualInput = $("<input></input>").attr(inputAttr).text(value);
    let virtualDiv = $("<div></div>").append(virtualLabel).append(virtualInput);
    return virtualDiv;
}

function createDate() {
    //日期初始化上不去
    //yyyy-MM-ddThh:mm
    //replace(/\//g, "-");
    let date = new Date();
    let year = date.getFullYear();
    let month = date.getMonth() + 1;
    if (month < 10) {
        month = "0" + month;
    }
    let day = date.getDate();
    if (day < 10) {
        day = "0" + day;
    }
    let getHours = date.getHours();
    if (getHours < 10) {
        getHours = "0" + getHours;
    }
    let getMinutes = date.getMinutes();
    if (getMinutes < 10) {
        getMinutes = "0" + getMinutes;
    }
    return {
        'year': year,
        'month': month,
        'day': day,
        'getHours': getHours,
        'getMinutes': getMinutes
    };
}

function createWindowElement(key, title, template, buttonObj) {
    //close Button，右上角的
    let buttonAttr = {
        'type': "button",
        'class': "btn-close",
        'data-bs-dismiss': "modal",
        'aria-label': "Close"
    }
    let button = $("<button></button>").attr(buttonAttr);

    let titleHeaderAttr = {
        'class': "modal-title",
    }
    //標題文字
    let titleHeader = $("<div></div>").attr(titleHeaderAttr).text(title);
    let headerDivAttr = {
        'class': "modal-header",
    }
    let headerDiv = $("<div></div>").attr(headerDivAttr).append(titleHeader).append(button);

    //底部文字與按鈕
    let footerDivAttr = {
        'class': "modal-footer",
    }
    let footerDiv = $("<div></div>").attr(footerDivAttr);
    let footerButtonAttr = {};
    //處理按鍵
    $.each(buttonObj, function (columsKey, columnsVal) {
        footerButtonAttr['type'] = columnsVal.type;
        footerButtonAttr['class'] = columnsVal.class;
        footerButtonAttr['data-bs-dismiss'] = columnsVal.data_bs_dismiss;
        footerButtonAttr['aria-label'] = columnsVal.aria_label;
        if (columnsVal.method) {
            $.each(columnsVal.method, function (columnsMethod, methodVal) {
                //console.log(columnsMethod);
                //console.log(methodVal);
                footerButtonAttr[columnsMethod] = methodVal;
            });
        }
        button = $("<button></button>").attr(footerButtonAttr).text(columnsVal.text);
        footerDiv.append(button);
    });
    //文字內容修改
    let contentDivAttr = {
        class: "modal-content",
    }

    let bodyDivAttr = {
        class: "modal-body",
    }

    let bodyDiv = $("<div></div>").attr(bodyDivAttr).html(template);

    let contentDiv = $("<div></div>").attr(contentDivAttr).append(headerDiv).append(bodyDiv).append(footerDiv);

    let dialogDivAttr = {
        class: "modal-dialog",
    }
    let dialogDiv = $("<div></div>").attr(dialogDivAttr).append(contentDiv);

    let containerDivAttr = {
        class: "modal-dialog modal-dialog-centered modal-dialog-scrollable",
    };
    let containerDiv = $("<div></div>").attr(containerDivAttr).append(dialogDiv);

    $("#" + key).append(containerDiv);
    myModal[key] = new bootstrap.Modal('#' + key, {});
    return null;
}

function createObjet(createObjetItem, groupId) {
    //Group層
    let virtualObject, validationObject = null;
    let test = 0;//測試用
    let countObject = 0;
    console.log(createObjetItem);
    for (let i = 0; i < createObjetItem.length; i++) {
        test = 0;
        console.log('type:'+createObjetItem[i].type);
        switch (createObjetItem[i].objectName) {
            case "lable":
                virtualObject = createLable(createObjetItem[i].key, createObjetItem[i].labelName, createObjetItem[i].gropName);
                break;
            case "lable2":
                virtualObject = createLable2(createObjetItem[i].key, createObjetItem[i].labelName, createObjetItem[i].gropName);
                break;
            case "autoComplete":
                virtualObject = createAutoCompleteElement(createObjetItem[i].key, createObjetItem[i].name, createObjetItem[i].labelName, createObjetItem[i].placeholder,
                    createObjetItem[i].validation);
                break;
            case "autoComplete2":
                virtualObject = createAutoCompleteElement2(createObjetItem[i].key, createObjetItem[i].name, createObjetItem[i].labelName, createObjetItem[i].placeholder,
                    createObjetItem[i].validation, createObjetItem[i].type);
                break;
            case "window":
                virtualObject = createWindowElement(createObjetItem[i].key, createObjetItem[i].title
                    , createObjetItem[i].template, createObjetItem[i].button);
                break;
            case "input":
                virtualObject = createInput(createObjetItem[i].key, createObjetItem[i].name, createObjetItem[i].class
                    , createObjetItem[i].placeholder, createObjetItem[i].type, createObjetItem[i].method);
                break;
            case "dropDownList":
                virtualObject = createDropDownListElement(createObjetItem[i].key, createObjetItem[i].name, createObjetItem[i].labelName, createObjetItem[i].validation);
                break;

            case "textArea":
                virtualObject = createTextAreaElement(createObjetItem[i].key, createObjetItem[i].name, createObjetItem[i].placeholder,
                    createObjetItem[i].labelName, createObjetItem[i].validation);
                break;
            case "dateInput":
                virtualObject = createDataPicker(createObjetItem[i].key, createObjetItem[i].name, createObjetItem[i].labelName,
                    createObjetItem[i].validationMessage, createObjetItem[i].type, createObjetItem[i].value);
                test = 7;
                break;
            case "button":
                virtualObject = createButtonElement(createObjetItem[i].liKey, createObjetItem[i].key, createObjetItem[i].labelName,
                    createObjetItem[i].class, createObjetItem[i].method, createObjetItem[i].modelName);
                break;
            case "table":
                virtualObject = createTableElement(createObjetItem[i].key, createObjetItem[i].columns);
                break;
        }

        if ((virtualObject != null) && (virtualObject != undefined)) {
            if (createObjetItem[i].gropName) {
                countObject++;
                //綁定驗證
                if (validationObject == null) {
                    validationObject = $('<div class="' + createObjetItem[i].validation.validationDiv + '"></div>');
                    validationObject.append(virtualObject);
                } else if (countObject == 2) {
                    validationObject.append(virtualObject);
                    $('#' + groupId).append(validationObject);
                    countObject = 0;
                    validationObject = null;
                }
            } else {
                $('#' + groupId).append(virtualObject);
                switch (createObjetItem[i].objectName) {
                    case "autoComplete2":
                        $('#' + groupId).append($('<datalist id="' + createObjetItem[i].key +'listOptions"></datalist>'));
                        break;
                }
                if (test == 7) {
                    //測試DATEPICKER寫入值
                    /*
                    console.log(virtualObject);
                    console.log(createObjetItem[i].key);
                    console.log(typeof createObjetItem[i].key);
                    console.log(document.getElementById(createObjetItem[i].key));
                    console.log(virtualObject);
                    console.log(document.getElementById(createObjetItem[i].key).value);
                    */
                }
            }
        }
    }
}

function processObjet(indexkendoObjet) {
    for (let i = 0; i < indexkendoObjet.length; i++) {
        switch (indexkendoObjet[i].objectName) {
            case "autoComplete": case "autoComplete2": case "dropDownList":
                ajaxObject(indexkendoObjet[i].key, indexkendoObjet[i].link, indexkendoObjet[i].objectName, indexkendoObjet[i].data);
                break;
            case "button":
                ajaxObject(indexkendoObjet[i].key, indexkendoObjet[i].link, indexkendoObjet[i].objectName, indexkendoObjet[i].data);
                break;
            default:
                break;
        }
    }
}

function implementAutoComplete(key, data) {
    let aAttr = {
        class: "dropdown-item",
        href: "javascript:void(0)",

    }
    let aObject = null, liObject = null;
    for (let i = 0; i < data.length; i++) {
        bootStarpTmp.indexBookName.push(data[i].Text);
        //AutoCompleteTree(data[i].Text);//要分析寫在後端，演算法
        aObject = $("<a></a>").attr(aAttr).text(data[i].Text);
        liObject = $("<li></li>").append(aObject);
        $("#Ul" + key).append(liObject);
    }
}

function implementAutoComplete2(key, data) {
    for (let i = 0; i < data.length; i++) {
        $("#" + key + "listOptions").append('<option value="' + data[i].text + '">' + data[i].text +'</option>');
    }
}

function implementDropDownList(key, data) {
    let optionObject
    for (let i = 0; i < data.length; i++) {
        if (i == 0) {
            optionObject = $("<option></option>").attr({ value: "" }).text("請選擇");
            $("#" + key).append(optionObject);
        }
        optionObject = $("<option></option>").attr({ value: data[i].value }).text(data[i].text);
        $("#" + key).append(optionObject);
    }
}

function implementNewBook(key, data) {
    $("#" + key).append('<span class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger">'
                       +data+
                      '<span class="visually-hidden">unread messages</span></span >');
}

function ajaxObject(key, link, type, data) {
    if ((link == "") || (link == null) || (link == undefined)) {
        return "NotLink";
    }
    console.log(link);
    $.ajax({
        type: "post",
        url: link,
        data: data,
        dataType: "json",
        headers: { "__RequestVerificationToken": $("input[name='__RequestVerificationToken']").val() },
        async: false,
        global: false,//禁止觸發全域ajax事件
        success: function (successData) {
            console.log(type);
            console.log(key);
            console.log(successData);
            if (successData.message == 'success') {
                switch (type) {
                    /*
                    case "window":
                        implementKendoWindow(indexkendoObjet[i].key, indexkendoObjet[i].title);
                        break;
                        */
                    case "autoComplete":
                        implementAutoComplete(key, successData.data);
                        break;
                    case "autoComplete2":
                        implementAutoComplete2(key, successData.data);
                        break;
                    case "button":
                        implementNewBook(key, successData.data);
                        break;
                    /*
                    case "textBox":
                        implementKendoTextBox(indexkendoObjet[i].key, indexkendoObjet[i].placeholder);
                        break;
                    */
                    case "dropDownList":
                        implementDropDownList(key, successData.data);
                        break;
                    /*
                    case "textArea":
                        implementKendoTextArea(indexkendoObjet[i].key, indexkendoObjet[i].placeholder);
                        break;
                    case "dateInput":
                        implementKendoDateInput(indexkendoObjet[i].key);
                        break;
                    case "grid":
                        implementKendoGrid(indexkendoObjet[i].key, indexkendoObjet[i].columns);
                        break;
                        */
                }
            } else {
                $("#alertMessage").text(successData.message);
                ajaxBSError(successData);
            }
        },
        error: function (error) {
            ajaxBSError(error);
        }
    });
}

function ajaxBSError(error) {
    console.log(error);
    if (error) {
        if (error.message) {
            if (error.responseJSON) {
                //尚不知來源
                console.log("1");
                $("#alertMessage").text(error.responseJSON.message);
                myModal.alertModal.show();
            } else {
                //這是自己製造的errormodel
                console.log("2");
                $("#alertMessage").text(error.message);
                myModal.alertModal.show();
            }
        } else if (error.responseJSON) {
            console.log("3");
            $("#alertMessage").text(error.responseJSON.message);
            myModal.alertModal.show();
        } else {
            console.log("4");
            let startError = error.responseText.indexOf("<H1>");
            let endError = error.responseText.indexOf("<hr width=100% size=1 color=silver></H1>");
            //substr
            $("#alertMessage").text(error.responseText.substring(startError + 4, endError));
            myModal.alertModal.show();
        }
    } else {
        $("#alertMessage").text("系統錯誤請通知服務人員!");
        myModal.alertModal.show();
    }
}

function implementGrid(key, data, page, columnsData) {
    let trObject = null;
    //console.log("columnsData");
    //console.log(columnsData.columns);
    //localStorage.setItem("bookData",JSON.stringify(bookDataFromLocalStorage));
    //select
    let selectSize = true;

    //   pageSize
    let pageSize = null;
    if (columnsData.pageable) {
        if (columnsData.pageable.pageSize) {
            pageSize = columnsData.pageable.pageSize;
        } else {
            pageSize = data.length;
        }
    }
    if (data.length == 0) {
        return;
    }
    

    systemData.initPage.pageTotal = Math.ceil(data.length / pageSize);
    systemData.tableData[key] = data;
    systemData.columnsData[key] = columnsData;
    let templateObj = null, trAttr = null;
    let background_color_display = null, background_color = null;
    let startPage = 0, endPage = 0, startItemPage = 0;
    startItemPage = (columnsData.pageable.pageSize * page) - (columnsData.pageable.pageSize - 1);

    console.log("開始");
    console.log("page: "+page);
    console.log("systemData.initPage.startPage: " +systemData.initPage.startPage);
    console.log("systemData.initPage.endPage: " +systemData.initPage.endPage);
    if ((systemData.initPage.startPage-4) == page) {
        //從第一次之後往前點的情況
        systemData.initPage.startPage = systemData.initPage.startPage - 6;
        systemData.initPage.endPage = systemData.initPage.endPage - 6;
        startPage = systemData.initPage.startPage;
        endPage   = systemData.initPage.endPage;
    } else if (systemData.initPage.startPage > page) {
        //點擊中間的數目
        systemData.initPage.startPage = systemData.initPage.startPage - 3;
        systemData.initPage.endPage = systemData.initPage.endPage - 3;
        startPage = systemData.initPage.startPage;
        endPage   = systemData.initPage.endPage;

    } else if ((systemData.initPage.pageTotal == page) || (page == (systemData.initPage.startPage)) || (systemData.initPage.startPage == 1)) {
        //往後點的情況，與一開始初始的情況
        startPage = systemData.initPage.startPage;
        endPage   = systemData.initPage.endPage;
    }
    console.log("startPage: " + startPage);
    console.log("endPage: " + endPage);

    for (let i = startItemPage; i <= pageSize * page; i++) {
        if (selectSize) {
            trAttr = {};
            trObject = $("<tr></tr>").hover(function () {
                background_color_display = $(this).css("background-color");
                background_color = $(this).css("color");
                if ((background_color_display == 'rgb(255, 255, 0)') && (background_color == 'rgb(255, 0, 0)')) {
                    $(this).css({ "background-color": '', 'color': '' });
                } else {
                    $(this).css({ "background-color": 'yellow', 'color': 'red' });
                }
            });
        }

        $.each(columnsData.columns, function (columnsKey, columnsVal) {
            //需要增加新欄位這邊也需要設定
            if (columnsVal.field) {
                if (columnsVal.template) {
                    templateObj = { id: columnsVal.field, name: columnsVal.field, width: columnsVal.width, style: "overflow: hidden;overflow-wrap: break-word;", template: columnsVal.template };
                } else if (columnsVal.money) {
                    templateObj = { id: columnsVal.field, name: columnsVal.field, width: columnsVal.width, style: "overflow: hidden;overflow-wrap: break-word;", money: columnsVal.money };
                } else {
                    templateObj = { id: columnsVal.field, name: columnsVal.field, width: columnsVal.width, style: "overflow: hidden;overflow-wrap: break-word;" };
                }
                trObject.append($("<td></td>").attr(templateObj));
            } else if (columnsVal.title && columnsVal.command) {
                trObject.append($("<td></td>").attr({ width: columnsVal.width, style: "overflow: hidden;overflow-wrap: break-word;" })
                    .append($("<butoon></butoon>").attr({ class: columnsVal.class }).text(columnsVal.command.text).click(columnsVal.command.click)));
            }
        });

        //需要先實例化
        $("#" + key + " tbody").append(trObject);

        //style:"display: block;"
        //{ style: "overflow: hidden;" }
        //template
        //let moneyRegex1 = '/^\$?\d+(,\d{3})*(\.\d*)?$/';
        let moneyRegex = '/(\d:)/)';
        //都要-1因為是從1開始
        $.each(data[(i-1)], function (key, val) {
            if ((typeof $("#" + key).attr("money") !== 'undefined') && ($("#" + key).attr("money") !== false)) {
                //let也不會重複宣告
                //.split(moneyRegex)
                val = val.toString().concat("本");
            }

            if ((typeof $("#" + key).attr("template") !== 'undefined') && ($("#" + key).attr("template") !== false)) {
                $("#" + key).html($("#" + key).attr("template").replace("${" + key + "}", val)).removeAttr("template");
            } else {
                $("#" + key).text(val);
            }
        });

        $.each(columnsData.columns, function (columnsKey, columnsVal) {
            if (columnsVal.field) {
                $("#" + columnsVal.field).removeAttr("id");
            }
        });
    }

    if (columnsData.pageable) {
        $("#" + key).append(selfpagination(key, page, startPage, endPage));
    }
    //未來需要寫緩存
    //localStorage.setItem(key, data);
}

function selfpagination(key, page, startPage, endPage) {
    let spanAttrBer = {
        'aria-hidden': "true"
    };
    let tableSpanBer = $("<span></span>").attr(spanAttrBer).html("&laquo;");

    let spanAttrAft = {
        'aria-hidden': "true"
    };
    let tableSpanAft = $("<span></span>").attr(spanAttrAft).html("&raquo;");

    let aAttr = {
        'class': "page-link",
    };
    let tableA = $("<a></a>").attr(aAttr);

    let liAttr = {
        'class': "page-item"
    };
    let tableLi = $("<li></li>").attr(liAttr);

    let ulAttr = {
        'class': "pagination"
    };
    let tableUl = $("<ul></ul>").attr(ulAttr);

    for (let i = startPage; i <= endPage; i++) {
        if (i == startPage) {
            if (page == 1) {
                aAttr['aria-label'] = "Previous";
                liAttr['onclick'] = "changePage(" + key + ", " + i + ")";
                tableLi.attr(liAttr).append(tableA.attr(aAttr).append(tableSpanBer));
                tableUl.append(tableLi);
                tableA = $("<a></a>").attr(aAttr);
                tableLi = $("<li></li>").attr(liAttr);
            } else {
                aAttr['aria-label'] = "Previous";
                if ((i - 1) == 0) {
                    liAttr['onclick'] = "changePage(" + key + ", " + i + ")";
                } else {
                    liAttr['onclick'] = "changePage(" + key + ", " + (i - 1) + ")";
                }
                tableLi.attr(liAttr).append(tableA.attr(aAttr).append(tableSpanBer));
                tableUl.append(tableLi);
                tableA = $("<a></a>").attr(aAttr);
                tableLi = $("<li></li>").attr(liAttr);
            }

            liAttr['onclick'] = "changePage(" + key+ ", " + i + ")";
            tableLi.attr(liAttr).append(tableA.attr(aAttr).text(i).removeAttr('aria-label'));
            tableUl.append(tableLi);
            tableA = $("<a></a>").attr(aAttr);
            tableLi = $("<li></li>").attr(liAttr);
        } else {
            liAttr['onclick'] = "changePage(" + key+ ", " + i + ")";
            tableLi.attr(liAttr).append(tableA.attr(aAttr).text(i).removeAttr('aria-label'));
            tableUl.append(tableLi);
            tableA = $("<a></a>").attr(aAttr);
            tableLi = $("<li></li>").attr(liAttr);
            //console.log(systemData.initPage.endPage);
            //console.log(i);
            if ((i == systemData.initPage.endPage) || (systemData.initPage.pageTotal == i)) {
                aAttr['aria-label'] = "Next";

                //確定下一頁還會有
                if (systemData.initPage.pageTotal >= (i + 1)) {
                    liAttr['onclick'] = "changePage(" + key + ", " + (i + 1) + ")";
                } else {
                    liAttr['onclick'] = "changePage(" + key + ", " + systemData.initPage.pageTotal + ")";
                }

                systemData.initPage.startPage = (i + 1);
                systemData.initPage.endPage = (i + 3);

                tableLi.attr(liAttr).append(tableA.attr(aAttr).append(tableSpanAft));
                tableUl.append(tableLi);
            }
        }

        //可以用來過濾多餘的頁數，一定要放最下面
        if (systemData.initPage.pageTotal == i) {
            systemData.initPage.startPage = systemData.initPage.startPage - (systemData.initPage.pageTotal % 3);
            systemData.initPage.endPage   = systemData.initPage.endPage - (systemData.initPage.pageTotal % 3);
            break;
        }
    }

    console.log("結束");
    console.log("systemData.initPage.startPage"+systemData.initPage.startPage);
    console.log("systemData.initPage.endPage" +systemData.initPage.endPage);

    let tableAttr = {
        'aria-label': "Page navigation example"
    };
    let tableNav = $("<nav></nav>").attr(tableAttr).append(tableUl);
    return tableNav;
}

function changePage(key, page) {
    deleteAllGrid(key.id,"changePage");
    implementGrid(key.id, systemData.tableData[key.id], page, systemData.columnsData[key.id]);
}

function deleteAllGrid(tableName, original) {
    switch (original) {
        case "clearAll":
            $('#' + tableName + " tbody tr").remove();
            $('#' + tableName + " nav").remove();
            systemData.initPage.startPage = 1;
            systemData.initPage.endPage = 3;
            break;
        case "changePage":
            $('#' + tableName + " tbody tr").remove();
            $('#' + tableName + " nav").remove();
            break;
    }
}


function validation(validationObj) {
    //可以放cookie，localstorage看看，目前學vue的方法
    //驗證目前沒做，以後端為主
    //每次重刻需要部屬
    //console.log(validationObj);
    //console.log($.type(valObject)); //物件判定還是object
    /*$.each(validationObj, function (keyNum, valObject) {
        if ((valObject.localName == 'input') || (valObject.localName == 'textarea') || (valObject.localName == 'select')) {
            systemData.data[valObject.name] = $(valObject).val();
        }
    });*/
    systemData.data = new FormData();
    $.each(validationObj, function (keyNum, valObject) {
        if ((valObject.localName == 'input') || (valObject.localName == 'textarea') || (valObject.localName == 'select')) {
            console.log(valObject.type);
            //systemData.data[valObject.name] = $(valObject).val();
            //console.log(getObjUrl($(valObject).val()));
            if (valObject.type == "file") {
                console.log($(valObject).prop('files')[0]);
                systemData.data.append(valObject.name, $(valObject).prop('files')[0]);
            } else {
                console.log(valObject.name);
                console.log($(valObject).val());
                systemData.data.append(valObject.name, $(valObject).val());
            }
        }
    });
    systemData.data.append('MachineType', 'PhoneLoginState');
}

function getObjUrl(file) {
    var url = null;
    if (window.createObjcectURL != undefined) {
        url = window.createOjcectURL(file);
    } else if (window.URL != undefined) {
        url = window.URL.createObjectURL(file);
    } else if (window.webkitURL != undefined) {
        url = window.webkitURL.createObjectURL(file);
    }  
    return url;
}

function validation2(validationObj) {
    //console.log(validationObj);
    //console.log(validationObj.value);
    //console.log($('#' + validationObj.id).val());
    //jquery綁不上去要再看看validationObj.val()
    let validationObjJq = $('#' + validationObj.id);
    if (validationObjJq.val()) {
        validationObjJq.attr("class", "form-control is-valid");
    } else {
        validationObjJq.attr("class", "form-control is-invalid");
    }
}

function implementAjax(link, jsonTitle) {
    let type_insert = 'insert';
    let type_destory = 'destory';
    let type_detail = 'detail';
    let type_update = 'update';
    let type_login = 'login';
    let type_error = 'error';
    let json = {};
    json[jsonTitle] = JSON.stringify(systemData.data);
    console.log(json);
    console.log(link);
    console.log(systemData.data);
    console.log($("input[name='__RequestVerificationToken']").val());
    $.ajax({
        type: "post",
        url: link,
        //data: json,
        data: systemData.data,
        headers: { "__RequestVerificationToken": $("input[name='__RequestVerificationToken']").val() },
        processData: false, // important// 告訴jquery不要去處理發送的數據
        contentType: false, // important// 告訴Jquery不要去設定Content-type請求頭
        mimeType: 'multipart/form-data',
        //dataType: "json",
        success: function (successData) {
            console.log(successData);
            successData = JSON.parse(successData);
            if (successData.message) {
                console.log(successData);
                switch (successData.type) {
                    case type_insert:
                        ajaxBSError(successData);
                        clearFromData([
                            $("#insertBookName")[0],
                            $("#insertAuthor")[0],
                            $("#insertBookPublisher")[0],
                            $("#insertBookNote")[0],
                            $("#insertCodeId")[0],
                            $("#insertBookBoughtDate")[0],
                            $("#insertBookClassId")[0]]);
                        break;
                    case type_update:
                        ajaxBSError(successData);
                        break;
                    case type_destory:
                        if (successData.code == 0) {
                            removeTableData();
                        }
                        ajaxBSError(successData);
                        break;
                    case type_detail:
                        ajaxBSError(successData);
                        break;
                    case type_login:
                        ajaxBSError(successData);
                        //console.log($.cookie('ASPNetCroe.Cookies'));
                        break;
                    case type_error:
                        ajaxBSError(successData);
                        break;
                }
            } else {
                ajaxBSError(successData);
            }
        },
        error: function (error) {
            ajaxBSError(error);
        }
    });
}

/*
function table() {
    var rows = $table.find('find > tr').get();
    $.each(rows,function(index,row){
        row.sorKey = $(row).children("td").eq(column).text().toUpperCase();
    })
    rows.sort(function(a,b){
        if(a.sortKey < b.sortKey)
            return -1;
        if(a.sortKey > b.sortKey)
            return 1;
    })

    $.each(rows,function(index,row){
        $table.children('tbody').append(row);
        row.sortKey =null;
    })
    //要刪除sortKey避免記憶體洩漏


}
*/

function getDataSource(link, type, json, columnsData = null) {
    //因為要回來目前先從第一頁
    $.ajax({
        type: "post",
        url: link,
        data: json,
        dataType: "json",
        success: function (successData) {
            switch (type) {
                case "searchTable":
                    deleteAllGrid(type, "clearAll");
                    implementGrid(type, successData.data, 1, columnsData);
                    break;
                case "bookLendRecord":
                    break;
                case "updateData":

                    break;
            }
        },
        error: function (error) {
            ajaxBSError(error);
        }
    });
}