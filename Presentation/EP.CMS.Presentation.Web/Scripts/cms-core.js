if ("undefined" === typeof EP) var EP = {};
EP.submitButton = function (action, isAjax, url, controller) {
    if (controller == undefined)
        controller = $("#hdnController").val();
    jQuery("[name='Action']").val(action);
    switch (action) {
        case "new":
            {
                window.location = GetUrl("Edit");
                break;
            }
        case "edit":
            {
                var checkedItem = GetCheckedSingleItem();
                if (checkedItem != undefined) {
                    var queryKey = "";
                    switch (controller) {
                        case "Info": {
                            queryKey = "infoId";
                            break;
                        }
                        case "News": {
                            queryKey = "newsId";
                            break;
                        }
                    }
                    window.location = GetUrl("Edit?" + queryKey + "=" + checkedItem);
                }
                break;
            }
        case "apply":
            {
                SaveForm();
                break;
            }
        case "save":
            {
                SaveForm();
                break;
            }
        case "save-new":
            {
                SaveForm();
                break;
            }
        case "cancel":
            {
                RedirectToolbar();
                break;
            }
        case "viewsite":
            {
                var url = $(".adminform:visible").find(".frontend_url").val();
                window.open(url, '_blank');
                break;
            }
        case "trash":
            {
                Sexy.confirm('<h1>Confirm to Delete?</h1>Deleting the item might cause to remove all that is linked to it.', {
                    onComplete: function (e) {
                        if (e) {
                            Trash(e, controller);
                        }
                    }
                });

                break;
            }
        case "publish":
            {
                Sexy.confirm('<h1>Confirm to publish?</h1>', {
                    onComplete: function (e) {
                        if (e) {
                            if (controller == "Menu") {
                                checkedItems = GetCheckedMenuItem();
                            }
                            else {
                                var checkedItems = GetCheckedItems();
                            }
                            if (checkedItems != undefined) {
                                PageLoader(true);
                                var idArray = new Array();
                                if (controller == "Menu") {
                                    idArray.push(checkedItems);
                                }
                                else {
                                    checkedItems.each(function (e) {
                                        idArray.push($(this).val());
                                    });
                                }
                                $.ajax({
                                    type: "POST",
                                    url: GetUrl("Publish"),
                                    dataType: "json",
                                    traditional: true,
                                    data: { idList: idArray },
                                    success: function (e) {
                                        PageLoader(false);
                                        Sexy.info("Published successfully", {
                                            onComplete: function (e) {
                                                window.location = GetUrl(window.location.search);
                                            }
                                        });
                                    },
                                    error: function (xhr, ajaxOptions, thrownError) {
                                        AjaxError();
                                    }

                                });
                            }
                        }
                    }
                });

                break;
            }

        case "unpublish":
            {
                Sexy.confirm('<h1>Confirm to Unpublish?</h1>', {
                    onComplete: function (e) {
                        if (e) {
                            if (controller == "Menu") {
                                checkedItems = GetCheckedMenuItem();
                            }
                            else {
                                var checkedItems = GetCheckedItems();
                            }
                            if (checkedItems != undefined) {
                                PageLoader(true);
                                var idArray = new Array();
                                if (controller == "Menu") {
                                    idArray.push(checkedItems);
                                }
                                else {
                                    checkedItems.each(function (e) {
                                        idArray.push($(this).val());
                                    });
                                }
                                $.ajax({
                                    type: "POST",
                                    url: GetUrl("Unpublish"),
                                    dataType: "json",
                                    traditional: true,
                                    data: { idList: idArray },
                                    success: function (e) {
                                        PageLoader(false);
                                        Sexy.info("Unpublished successfully", {
                                            onComplete: function (e) {
                                                window.location = GetUrl(window.location.search);
                                            }
                                        });
                                    },
                                    error: function (xhr, ajaxOptions, thrownError) {
                                        AjaxError();
                                    }

                                });
                            }
                        }
                    }
                });

                break;
            }

        case "menureorder":
            {
                var checkedItem = GetCheckedMenuItem();
                if (checkedItem != undefined) {
                    ReorderMenu(checkedItem);
                }
                break;
            }

        case "whats-new":
            {
                var checkedItem = GetCheckedSingleItem();
                if (checkedItem != undefined) {
                    WhatsNew(checkedItem);
                }
                break;
            }
        default: {
            console.log("No functionalities for action: " + action);
            break;
        }
    }
}

EP.loading = false;

function SaveForm() {
    form = $("#content-box form:visible");

    if (typeof tinyMCE != 'undefined')
        tinyMCE.triggerSave(true, true);
    form.validate({
        ignore: ""
    });
    if (form.valid()) {
        PageLoader(true);
        form.submit();
    }

}


function SaveFormSuccess() {
    Sexy.info("<h1>Saved item</h1><em>Note: The item shown in the screen is only saved.</em>", {
        onComplete: function (e) {
            PageLoader(false);
            action = jQuery("[name='Action']").val();
            switch (action) {
                case "apply":
                    {
                        break;
                    }
                case "save":
                    {
                        RedirectToolbar();
                        break;
                    }
                case "save-new":
                    {
                        window.location = GetUrl("Edit");
                        break;
                    }
            }
        }
    });

}

function HandleLoadResponse(responseText, textStatus, req) {
    if (textStatus == "error") {
        AjaxError();
    }
}


function AjaxError(XMLHttpRequest, textStatus, errorThrown) {
    PageLoader(false);
    Sexy.error("<h1>There has been an error. Please try again!!!</h1><em>Note: If the problem persists, please contact IT support.</em>");
}

function RedirectToolbar() {

    var returnUrl = "";
    var workflow = getQueryString("workflow");
    var query = "/" + getQueryString("menuid") + "?lang=" + getQueryString("lang");
    switch (workflow) {

        case "menu_article_add":
            {
                returnUrl = GetUrl("Index" + query, "Menu");
                break;
            }


        case "menu_article_edit":
            {
                returnUrl = GetUrl("Index" + query, "Menu");
                break;
            }

        case "menu_static_edit":
            {
                returnUrl = GetUrl("Static" + query, "Menu");
                break;
            }
    }
    if (returnUrl == undefined || returnUrl == '')
        window.location = GetUrl("");
    else
        window.location = returnUrl + (returnUrl.indexOf("?") > 0 ? "&" : "?") + "timestamp=" + new Date().getTime();
}

function Trash(e, controller) {

    if (controller == "Menu") {
        checkedItems = GetCheckedMenuItem();
    }
    else {
        var checkedItems = GetCheckedItems();
    }
    if (checkedItems != undefined) {
        PageLoader(true);
        var idArray = new Array();
        if (controller == "Menu") {
            idArray.push(checkedItems);
        }
        else {
            checkedItems.each(function (e) {
                idArray.push($(this).val());
            });
        }
        $.ajax({
            type: "POST",
            url: GetUrl("Delete"),
            dataType: "json",
            traditional: true,
            data: { idList: idArray },
            success: function (e) {
                PageLoader(false);
                Sexy.info("Deleted successfully", {
                    onComplete: function (e) {
                        window.location = GetUrl(window.location.search);
                    }
                });
            },
            error: function (xhr, ajaxOptions, thrownError) {
                AjaxError();
            }

        });
    }
}

function getQueryString(name) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}


function GetCheckedItems() {

    var checkedItems = $(".adminlist .cbox-form:checked");
    if (checkedItems.length == 0) {
        Sexy.error("<h1>Please first make a selection from the list</h1>");
    }
    else return checkedItems;
}

function GetCheckedSingleItem() {

    var checkedItems = $(".adminlist .cbox-form:checked");
    if (checkedItems.length == 0) {
        Sexy.error("Please first make a selection from the list");
    }
    else if (checkedItems.length > 1) {
        Sexy.error("Please select only one from the list");
    }
    else return $(checkedItems).val();
}

function GetUrl(relativeUrl, controller) {
    if (controller == undefined)
        controller = $("#hdnController").val();
    return $("#hdnBaseUrl").val() + controller + "/" + relativeUrl;
}

EP.checkAll = function (element) {
    $(".adminlist .cbox-form").prop('checked', $(element).prop('checked'));
};

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#imgPreview').attr('src', e.target.result);
        }
        if (input.files[0].type.indexOf("image") != -1) {
            $("#divPreview").show();
            reader.readAsDataURL(input.files[0]);
        }
        else {
            $("#divPreview").hide();
        }
    }
}

function SearchList() {
    window.location = GetUrl("?query=" + $("#filter_search").val());
}

function PageLoader(show) {
    if (show) {
        $("#loader_cont").fadeIn()
        EP.loading = true;
        timeout = setTimeout(function () {
            if (EP.loading) {
                $("#loader_cont").fadeOut();
                Sexy.error("<h1>There has been an error. Please try again!!!</h1>");
            }
        }, 40000)
    }
    else {
        $("#loader_cont").fadeOut();
        clearTimeout(timeout);
        EP.loading = false;
    }
}

function WhatsNew(id) {
    PageLoader(true);
    $.ajax({
        type: "POST",
        dataType: "json",
        url: GetUrl("WhatsNew"),
        data: { id: id },
        success: function (response) {
            PageLoader(false);
            if (response) {
                Sexy.info(response.Message, {
                    onComplete: function (e) {
                        window.location = GetUrl("");
                    }
                });
            }
            else {
                Sexy.error(response.Message);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            AjaxError();
        },
        traditional: true
    });
}



function ShowDialog(html, options) {
    $("#dialog-holder").html(html);

    var defaults = {
        modal: true,
        open: function () {
        },
        close: function () {
            $("#dialog-holder").dialog("destroy");
            $("#dialog-holder").html("");
        },
        height: 600,
        width: 1000,
        title: ''
    };
    var options = $.extend(defaults, options);
    $("#dialog-holder").dialog(options);
}


// ````````--------------````````MENU ````````--------------------````````//

function MenuNew() {
    var checkedItem = GetCheckedMenuItem();
    if (checkedItem != undefined) {

        if ($("#menu-tree .checked").siblings(".menu-leaf").val() == "0") {
            PageLoader(true);
            $("#add-menu").load(GetUrl("Edit/?parentId=") + checkedItem, function (responseText, textStatus, req) {
                HandleLoadResponse(responseText, textStatus, req);
                $("#add-menu").find(".btn-cancel").click(function (e) {
                    $("#add-menu").html("");
                    return false;
                });
                $("#add-menu").find(".btn-add").click(function (e) {
                    PageLoader(true);
                });

                PageLoader(false);
            });
        }
        else {
            Sexy.error("Cannot add a menu to a child node. Please select a parent node");
        }

    }
}

function MenuEdit() {
    var checkedItem = GetCheckedMenuItem();
    PageLoader(true);
    if (checkedItem != undefined) {
        $("#add-menu").load(GetUrl("Edit?menuId=") + checkedItem, function (responseText, textStatus, req) {
            HandleLoadResponse(responseText, textStatus, req);
            $("#add-menu").find(".btn-cancel").click(function (e) {
                $("#add-menu").html("");
                return false;
            });

            $("#add-menu").find(".btn-add").click(function (e) {
                PageLoader(true);
            });
            PageLoader(false);
        });
    }
}

function GetCheckedMenuItem() {

    var menuChecked = $("#menu-tree .checked");
    if (menuChecked.length == 1) {
        return menuChecked.attr("id").split("menu")[1];
    }
    else {
        Sexy.error("<h1>Select a menu</h1>");
    }
}

function ChangeInfoFromMenu(id, action) {

    var checkedItem = GetCheckedMenuItem();

    if (checkedItem != undefined) {
        id = $("#hdnMainInfoId").val();
        queryString = "?infoId=" + id + "&workflow=" + action + "&menuid=" + checkedItem + "&lang=" + GetLanguage();
        window.location = GetUrl("Edit" + queryString, "Info");

    }
}

function ReorderMenu(id) {

    if ($("#menu-tree .checked").siblings(".menu-leaf").val() == "0") {
        PageLoader(true);
        $.ajax({
            type: "POST",
            dataType: "html",
            url: GetUrl("Reorder"),
            data: { id: id },
            success: function (response) {
                if (response) {
                    PageLoader(false);
                    var options = {
                        minHeight: 450,
                        width: 300,
                        buttons: {
                            "Submit": function () {
                                SubmitMenuOrder();
                            },
                            "Cancel": function () {
                                $("#dialog-holder").dialog("close");
                            }
                        },
                        title: 'Menu Re-order'
                    };
                    ShowDialog(response, options);
                    var setSelector = "#menuList";
                    $(setSelector).sortable({
                        axis: "y",
                        cursor: "move"
                    });
                }
                else {
                    Sexy.error("Failed to load");
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                AjaxError();
            },
            traditional: true
        });
    }
    else {
        Sexy.error("Cannot reorder a leaf node. Please select it's parent node");
    }

}

function SubmitMenuOrder() {
    PageLoader(true);
    idArray = $("#menuList").sortable("toArray");
    $.ajax({
        type: "POST",
        dataType: "json",
        url: GetUrl("SubmitOrder"),
        data: { idList: idArray },
        success: function (response) {
            PageLoader(false);
            if (response.Success) {
                Sexy.alert(response.Message, {
                    onComplete: function (e) {
                        window.location = $("#hdnController").val() + "" + window.location.search;
                    }
                });
            }
            else {
                Sexy.error(response.Message);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            AjaxError();
        },
        traditional: true
    });
}

EP.paginate = function (itemsOnPage) {
    $(document).ready(function (e) {
        var items = $(".data-row").length;
        if (!itemsOnPage)
            itemsOnPage = 10;
        $(".data-row").hide();
        $(".data-row").slice(0, itemsOnPage).show();
        $(".pagination").pagination({
            items: items,
            itemsOnPage: itemsOnPage,
            cssStyle: 'light-theme',
            onPageClick: function (pageNumber, event) {
                $(".data-row").hide();
                var start = (pageNumber - 1) * itemsOnPage;
                var end = ((pageNumber) * itemsOnPage);
                $(".data-row").slice(start, end).show();
            }
        });
        var hash = window.location.hash;
        if (hash.indexOf("page") != -1) {
            var page = hash.split('-')[1];
            $(".pagination").pagination('selectPage', page);
        }
    });
}

// ````````--------------````````DIRECTORY ````````--------------------````````//


function DirectorySelected(element) {

    $("#directory li").removeClass("active");
    $(".lblDirectory").val(element);
    $("#" + element).parent().addClass('active');

}

function DirectorySelectionCompleted(element) {
    SqueezeBox.assign($$('a[rel=boxed]'));
}

function GetBanner(element, category) {
    baseUrl = $("#hdnController").val();
    $("#directory li").removeClass("active");
    $(".lblDirectory").val(category);
    $("#" + category).parent().addClass('active');
    $("#dir-content").load(GetUrl("BannerContent?directory=" + category), function (responseText, textStatus, req) {
        EP.languageSet(false);
        SqueezeBox.assign($$('a[rel=boxed]'));

    });
}

function GetActiveDirectory() {
    return $("#directory li.active a")[0].id;
}

function DirectoryAction(action) {
    var baseUrl = $("#hdnDirController").val();
    if ($("#directory li.active").length > 0) {
        var id = GetActiveDirectory();
        if (action == 'delete') {
            Sexy.confirm('<h1>Confirm to Delete?</h1>Deleting the folder might cause to remove all that is linked to it.', {
                onComplete: function (e) {
                    if (e) {
                        $.post(baseUrl + "Delete?id=" + id, { id: id }, function (response) {
                            if (response) {
                                Sexy.info("<h1>" + response.Message + "</h1>");
                                $("#directory").load(baseUrl + "Tree", function (responseText, textStatus, req) {
                                    HandleLoadResponse(responseText, textStatus, req);
                                });
                            }
                            else {
                                Sexy.error("<h1>" + response.Message + "</h1>");
                            }
                        });
                    }
                }
            });
        }
        else {
            $("#txtDirName").val(action == "rename" ? $("#directory li.active a").html() : "");
            $("#divDirSave").show('slow');
            $('#btnSaveDir').unbind('click');
            $("#btnSaveDir").click(function (e) {
                var name = $("#txtDirName").val();
                id = GetActiveDirectory();
                $.ajax({
                    type: "POST",
                    dataType: "json",
                    url: baseUrl + action,
                    data: { id: id, name: name },
                    success: function (response) {
                        if (response.Success) {
                            Sexy.info(response.Message);
                            $("#directory").load(baseUrl + "Tree", function (responseText, textStatus, req) {
                                HandleLoadResponse(responseText, textStatus, req);
                                $("#directory #" + name).click();
                            });
                        }
                        else {
                            Sexy.error(response.Message);
                        }
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        AjaxError();
                    },
                    traditional: true
                });
                $("#divDirSave").hide('fast');
            });
        }
    }
    else {
        Sexy.error("Select a directory");
    }
    return false;
}


// ````````--------------````````MEDIA ````````--------------------````````//


function SelectNewImage(element, callback) {
    var infoId = $(".adminform").filter(":visible").find(".hdnInfoId").val();
    if (infoId != 0) {
        var options = {
            open: function () {
                $(this).load(GetUrl("New", "Media"), function (responseText, textStatus, req) {
                    HandleLoadResponse(responseText, textStatus, req);
                    var dialogBox = this;
                    $(this).find(".btn-add").click(function (e) {
                        var name = $(dialogBox).find(".file-name").val();
                        if (name != "" && $(dialogBox).find("input:file").val() != "") {
                            var formOptions = {
                                success: function (response) {
                                    PageLoader(false);
                                    if (response.Success) {
                                        var parent = $(element).parents("ul");
                                        GetImagePopup($(dialogBox).find(".file-input")[0], parent.find(".img-holder"));
                                        var oldId = parent.find(".img-id").val();
                                        parent.find(".img-id").val(response.Id);
                                        parent.find(".img-name").html(name);
                                        $("#dialog-holder").dialog("destroy");
                                        $("#dialog-holder").html("");
                                        callback(response.Id, response.DisplayName, oldId);
                                    }
                                    else {
                                        Sexy.error("Media cannot be saved. Try again!!!");
                                    }
                                }// post-submit callback
                            };
                            PageLoader(true);
                            $("#formAddMedia").ajaxSubmit(formOptions);
                        }
                        else {
                            Sexy.error("<h1>Please enter display name and file.</h1>");
                        }
                        return false;
                    });
                    $(this).find(".btn-cancel").click(function (e) {
                        $("#dialog-holder").dialog("destroy");
                        $("#dialog-holder").html("");
                        return false;
                    });
                });
            },
            height: 250,
            width: 450,
            title: 'Gallery'
        }
        ShowDialog("", options);
    }
    else {
        Sexy.error("<h1>Please insert an article first</h1>");
    }
    return false;
}

function SelectGalleryImage(element, callback) {

    var infoId = $(".adminform").filter(":visible").find(".hdnInfoId").val();
    if (infoId != 0) {
        var options = {
            open: function () {
                $(this).load(GetUrl("Popup", "Media"), function (responseText, textStatus, req) {
                    HandleLoadResponse(responseText, textStatus, req);
                    $("#images").click();
                    $("#dir-content").on("click", ".cbox-form", function (e) {
                        var parent = $(element).parents("ul");
                        var id = $(this).val();
                        var oldId = parent.find(".img-id").val();
                        var imgSrc = ($(this).parents("li").find("img").attr("src"));
                        var name = ($(this).parents("li").find(".img-name").html());
                        parent.find(".img-id").val(id);
                        parent.find(".img-holder").attr("src", imgSrc);
                        parent.find(".img-name").html(name);
                        $("#dialog-holder").dialog("destroy");
                        $("#dialog-holder").html("");
                        callback(id, name, oldId);
                    });
                });
            },
            close: function () {
                $("#dialog-holder").dialog("close");
            },
            height: 600,
            width: 1000,
            title: 'Gallery'
        }
        ShowDialog("", options);
    }
    else {
        Sexy.error("<h1>Please insert an article first</h1>");
    }
    return false;

}

function NewsImageUpload(id, name) {
    Sexy.info("<h1>Media changed</h1>");
}

function MenuImageUpload(id, name, oldId) {
    PageLoader(true);
    var infoId = $(".adminform").filter(":visible").find(".hdnInfoId").val();
    var menuId = GetCheckedMenuItem();
    if (infoId != undefined) { // Check for Static pages having no infoes attached to it
        var url = GetUrl("UpdateInfoBanner", "Info");
        $.get(url, { infoId: infoId, imageId: id, menuId: menuId }, function (response) {
            PageLoader(false);
            if (response.Success) {
                Sexy.info("<h1>Media saved</h1>", {
                    onComplete: function (e) {
                        if (response.MenuComplete) {
                            location.reload();
                        }
                    }
                });

            }
        });
    }
    else {
        var url = GetUrl("UpdateImage", "Media");
        $.post(url, { oldId: oldId, newId: id }, function (e) {
            PageLoader(false);
            Sexy.info("<h1>Media saved</h1>");
        });
    }

}

function AddMedia(category) {
    $(".lblDirectory").val(GetActiveDirectory());
    $("#add-media").find(".hdnCategory").val(category);
    $("#add-media").find(".btn-add").click(function (e) {
        var name = $("#add-media").find(".file-name").val();
        if (name != "" && $("#add-media").find("input:file").val() != "") {
            PageLoader(true);
            var options = {
                success: function (response) {
                    if (response.Success) {
                        PageLoader(false);
                        Sexy.info("Media added successfully", {
                            onComplete: function (e) {
                                window.location = GetUrl("?directory=" + $(".lblDirectory").val());
                            }
                        });
                        callback(response.Id, response.DisplayName);
                    }
                    else {
                        Sexy.error("Media cannot be saved. Try again!!!");
                    }
                }// post-submit callback
            };
            $("#formAddMedia").ajaxSubmit(options);
        }
        else {
            Sexy.error("<h1>Please enter display name and file.</h1>");
        }
        return false;
    });
    $("#add-media").find(".btn-cancel").click(function (e) {
        $("#add-media").html("");
        return false;
    });
}

function NewMediaCancel() {
    $("#add-media").html("");
    return false;
}

function GetImagePopup(input, target) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            target.attr('src', e.target.result);
        }
        if (input.files[0].type.indexOf("image") != -1) {
            reader.readAsDataURL(input.files[0]);
        }
    }
}

// ````````--------------````LANGUAGE````````--------------------````````//

EP.languageSet = function (effects) {
    EP.languageChange($("#hdnLanguage").val(), effects);

};

EP.languageChange = function (language, effects) {
    $("#langbar li a").removeClass("active");
    $("#langbar li a.lang-" + language).addClass("active");
    if (language == "en") {
        toggleLanguage("english-edit", "arabic-edit", effects);
    }
    else {
        toggleLanguage("arabic-edit", "english-edit", effects);
    }
    $("#hdnLanguage").val(language);
    return false;
};

function toggleLanguage(showElement, hideElement, effects) {
    var pace = effects ? "slow" : ""
    $("#" + showElement).show(pace);
    $("#" + hideElement).hide(pace);
}


function GetLanguage() {
    return $("#hdnLanguage").val();
}

function SetLanguage(language) {
    if (!language) language = "en";
    $("#hdnLanguage").val(language);
}

