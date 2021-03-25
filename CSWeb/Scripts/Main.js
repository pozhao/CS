$(function () {
    $('.date-picker').datetimepicker({
        language: 'zh-TW',
        format: 'yyyy/mm/dd',
        todayHighlight: 1,
        minView: 2,
        autoclose: true,
        icons: {
            time: 'glyphicon glyphicon-time',
            date: 'glyphicon glyphicon-calendar',
            up: 'glyphicon glyphicon-chevron-up',
            down: 'glyphicon glyphicon-chevron-down',
            //previous: 'glyphicon glyphicon-chevron-left',
            previous: 'glyphicon glyphicon-backward',
            next: 'glyphicon glyphicon-chevron-right',
            today: 'glyphicon glyphicon-screenshot',
            clear: 'glyphicon glyphicon-trash',
            close: 'glyphicon glyphicon-remove'
        },
    });

    $('.table2').DataTable({
        searching: false,
        columnDefs: [{
            "targets": 'no-sort',
            "orderable": false,
            "order": []
        }],
        order: [[3, 'desc']],
        bLengthChange: false,
        language: {
            sLoadingRecords: "載入資料中...",
            emptyTable: "查無資料",
            oPaginate: {
                sFirst: "首頁",
                sPrevious: "上一頁",
                sNext: "下一頁",
                sLast: "末頁"
            }
        },
        info: false,
        fnRowCallback: function (nRow, aData, iDisplayIndex) {
            var index = iDisplayIndex + 1;
            $('td:eq(0)', nRow).html(index);
            return nRow;
        },
        /*targets: 0,*/
    });



    //$('.table2 thead tr').each(function () {
    //    $(this).prepend('<th>序號</th>');
    //});

    //$('.table2 tbody tr').each(function (i) {
    //    $(this).prepend('<th>' + (i+1) + '</th>');
    //});
});