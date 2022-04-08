// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $("table").DataTable({
        searching: true,
        paging: true,
        info: false,
        "pagingType": "full_numbers",
        //dom: 'lrt',
        dom: 'Bfrtip',
        buttons: [ 'copy', 'excel', 'csv', 'pdf', 'print' ]

    })

})