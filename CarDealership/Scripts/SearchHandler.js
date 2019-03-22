$(document).ready(function() {
    
});

$('#searchForm').on('submit',
    function(event) {
        event.preventDefault();
        event.stopPropagation();
        var urlString = "http://localhost:55792/api/";
        switch ($('#searchType').val) {
        case 'Admin':
            urlString += 'Admin/Vehicles'
        default:
        }
        $.ajax({
            type: 'GET',
            url: urlString,
            data: JSON.stringify('stuff'),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            'dataType': 'json',
            success: function (data, status) {
                $('#vehicleList').text('');
                $.each(data, function (index, item) {
                    var editLink = '<button class="btn btn-primary" onclick="Edit(';
                    editLink += item.ID + ')">Edit</button>';
                    var row = '<tr><td>' + item.Year + ' ' + item.Make + ' ' + item.Model + '</td>';
                    row += '<td>' + editLink + '</td></tr>';
                    $('#vehicleList').append(row);
                });
                $('#errorMessages').addClass('hidden');
            },
            error: function (xHR) {
                $('#errorMessages').text(xHR.responseText);
                $('#errorMessages').removeClass('hidden');
            }
        });
});