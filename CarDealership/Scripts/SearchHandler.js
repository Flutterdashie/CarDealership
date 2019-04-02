$(document).ready(function () {
    $('#errorMessages').hide();
});

$('#searchForm').on('submit',
    function (event) {
        'use strict';
        event.preventDefault();
        event.stopPropagation();
        var urlString = '/api/';
        var buttonLabel = '';
        switch ($('#searchType').val()) {
            //These are all based on page title
            case 'Vehicles':
                urlString += 'Admin/Vehicles';
                buttonLabel = 'Edit';
                break;
            case 'New':
                urlString += 'Inventory/New';
                buttonLabel = 'Details';
                break;
            case 'Used':
                urlString += 'Inventory/Used';
                buttonLabel = 'Details';
                break;
            case 'Index':
                urlString += 'Sales/Index';
                buttonLabel = 'Purchase';
                break;
            default:
                alert('Invalid page setup. Please refresh the page and try again.');
                return;

        }
        $.ajax({
            type: 'POST',
            url: urlString,
            data: JSON.stringify({
                MakeModelYear: $('#makeModelYear').val(),
                MinYear: $('#minYear').val(),
                MaxYear: $('#maxYear').val(),
                MinPrice: $('#minPrice').val(),
                MaxPrice: $('#maxPrice').val()
            }),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            'dataType': 'json',
            success: function (data, status) {
                $('#vehicleList').text('');
                $.each(data, function (index, item) {
                    var detailsLink = '<button class="btn btn-primary" onclick="Activate(';
                    detailsLink += item.ID + ')">' + buttonLabel + '</button>';
                    var row = '<div class="row"><div class="col-3"><img src="/Images/inventory-' + item.ID + '.' + item.ImgExt + '" alt="Car Picture"/></div>';
                    row += '<div class="col-9"><table><tr><th>' + item.Year + ' ' + item.Make + ' ' + item.Model + '</th></tr>';
                    row += '<tr><td>Body Style: ' + item.Body + '</td><td>Interior: ' + item.Interior + '</td><td>Sale Price: ' + item.SalePrice + '</td></tr>';
                    row += '<tr><td>Trans: ' + item.Transmission + '</td><td>Mileage: ' + item.Mileage + '</td><td>MSRP: ' + item.MSRP + '</td></tr>';
                    row += '<tr><td>Color: ' + item.Color + '</td><td>VIN: ' + item.VIN + '</td><td>' + detailsLink + '</td></tr></div></div>';
                    $('#vehicleList').append(row);
                });
                $('#errorMessages').hide();
            },
            error: function (xHR) {
                $('#errorMessages').text(xHR.responseText);
                $('#errorMessages').show();
            }
        });
    });