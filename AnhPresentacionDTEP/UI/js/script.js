$(function () {
    $(".tile").mousedown(function () {
        $(this).addClass("selecionado");
    });

    $(".tile").mouseup(function () {
        $(this).removeClass("selecionado");
    });
});

$('#modificarPrefijo').on('shown.bs.modal', function () {
    $('#modificarPrefijo').focus()
})

$('#modificarDependencia').on('shown.bs.modal', function () {
    $('#modificarDependencia').focus()
})