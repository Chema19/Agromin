


function select2Call(nameId) {
    let id = '#' + nameId;
    $(id).select2();
}

function ajaxGeneralPostById(urlServicio, id, nameObject) {
    $.ajax({
        url: urlServicio,
        method: 'POST',
        async: false,
        data: { Id: id },
        success: function (data) {
            if (data.validacion) {
                Swal.fire({
                    type: 'success',
                    title: nameObject + ' eliminado',
                    timer: 1500
                })
                setTimeout(function () {
                    location.reload();
                }, 2000);
            } 
        }
    });
}

function ajaxDeleteObjectPostById(urlServicio, id, nameObject) {
    $.ajax({
        url: urlServicio,
        method: 'POST',
        async: false,
        data: { Id: id },
        success: function (data) {
            debugger;
            if (data.validacion) {
                Swal.fire({
                    type: 'success',
                    title: nameObject + ' eliminado',
                    timer: 1500
                })
                setTimeout(function () {
                    location.reload();
                }, 2000);
            } else {
                Swal.fire({
                    type: 'error',
                    title: nameObject + ' no eliminado',
                    timer: 1500
                })
            }
        }
    });
}

function ajaxNormalSelect2Id(nameId, placehold,urlService) {
    let id = '#' + nameId;
    $(id).select2({
        placeholder: placehold,
        allowClear: true,
        dataType: 'json',
        quietMillis: 100,
        minimumInputLength: 1,
        ajax: {
            url: urlService,
            data: function (params) {
                return { filtro: params.term };
            },
            processResults: function (data) {
                return { results: data };
            }

        }
    });
}

function ajaxNormalSelect2Class(nameClass, placehold,urlService) {
    let clase = '.' + nameClass;
    $(clase).select2({
        placeholder: placehold,
        allowClear: true,
        dataType: 'json',
        quietMillis: 100,
        minimumInputLength: 1,
        ajax: {
            url: urlService,
            data: function (params) {
                return { filtro: params.term };
            },
            processResults: function (data) {
                return { results: data };
            }

        }
    });
}

function ajaxAnidadoSelect2Id(nameId, placehold, urlService,parameter) {
    let id = '#' + nameId;
    $(id).select2({
        placeholder: placehold,
        allowClear: true,
        dataType: 'json',
        quietMillis: 100,
        minimumInputLength: 1,
        ajax: {
            url: urlService,
            data: function (params) {
                return { filtro: params.term, parameterId: parameter };
            },
            processResults: function (data) {
                return { results: data };
            }

        }
    });
}